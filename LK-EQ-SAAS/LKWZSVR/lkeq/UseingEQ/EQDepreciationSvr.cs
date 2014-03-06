using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;
using System.Windows.Forms;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQDepreciationSvr : IEx
    {
        #region IEx 成员
        bool saveRunDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> XiList = ObjConvert.GetParamsByStr(data.Param["XiBiaoXML"].ToString());
            Opt opt2 = OptContent.get("SaveEQDepreciationDetailInfo_EQDepreciation");
            foreach (Dictionary<string, object> d in XiList)
            {
                //不管折旧ID是否为空，我都先取值
                d["DEPREID"] = d["折旧ID"];
                d["CARDID"] = d["卡片ID"];
                d["TOTALZJ"] = d["累计折旧"];
                d["MONTHZJ"] = d["本月折旧"];
                d["TOTALWORK"] = d["总工作量"];
                d["TOTALEDWORK"] = d["累计工作量"];
                d["MONTHWORK"] = d["本月工作量"];
                d["MEMO"] = d["备注"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];
                d["USERID"] = data.Param["USERID"];
                d["USERNAME"] = data.Param["USERNAME"];
                d["RECDATE"] = DateTime.Now;

                //如果不为空，肯定是编辑 那么就是进行更新即可
                if (!d["折旧ID"].ToString().Equals(""))
                {
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存折旧明细失败！" + dao.ErrMsg);
                    }
                }
                else//折旧ID为空，那么这里的卡片id全部是新增的[包含编辑或者是新增过来]
                {
                    //d["DEPREID"] = d["折旧ID"];
                    d["DEPREID"] = data.Param["DEPREID"];
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("新增折旧明细失败！" + dao.ErrMsg);
                    }

                    //这里对这些新增的卡片折旧进行数据更新[可能存在原本的折旧，工作量为空的情况]
                    //if ((d["TOTALEDWORK"] != null || !d["TOTALEDWORK"].ToString().Equals("")) && (d["TOTALZJ"] != null || d["TOTALZJ"].ToString().Equals("")))
                    if ((!d["TOTALEDWORK"].ToString().Equals("")) && (!d["TOTALZJ"].ToString().Equals("")))
                    {
                        // MessageBox.Show("1 全部不为null");
                        dao.ExecuteNonQuery("UPDATE LKEQ.EQCARDREC SET TOTALZJ=TOTALZJ +" + Convert.ToDouble(d["MONTHZJ"]) + " ,TOTALEDWORK=TOTALEDWORK + " + Convert.ToDouble(d["MONTHWORK"]) + "  WHERE CARDID=" + d["CARDID"].ToString() + " AND CHOSCODE=" + d["CHOSCODE"].ToString());
                    }
                    if ((!d["TOTALEDWORK"].ToString().Equals("")) && (d["TOTALZJ"].ToString().Equals("")))
                    {
                        //  MessageBox.Show("2 折旧为null");
                        dao.ExecuteNonQuery("UPDATE LKEQ.EQCARDREC SET TOTALZJ=" + Convert.ToDouble(d["MONTHZJ"]) + " ,TOTALEDWORK=TOTALEDWORK + " + Convert.ToDouble(d["MONTHWORK"]) + "  WHERE CARDID=" + d["CARDID"].ToString() + " AND CHOSCODE=" + d["CHOSCODE"].ToString());
                    }

                    if ((d["TOTALEDWORK"].ToString().Equals("")) && (!d["TOTALZJ"].ToString().Equals("")))
                    {
                        // MessageBox.Show("3 工作量为null");
                        dao.ExecuteNonQuery("UPDATE LKEQ.EQCARDREC SET TOTALZJ=TOTALZJ +" + Convert.ToDouble(d["MONTHZJ"]) + " ,TOTALEDWORK=" + Convert.ToDouble(d["MONTHWORK"]) + "  WHERE CARDID=" + d["CARDID"].ToString() + " AND CHOSCODE=" + d["CHOSCODE"].ToString());
                    }

                    if ((d["TOTALEDWORK"].ToString().Equals("")) && (d["TOTALZJ"].ToString().Equals("")))
                    {
                        // MessageBox.Show("4 全部为null");
                        dao.ExecuteNonQuery("UPDATE LKEQ.EQCARDREC SET TOTALZJ=" + Convert.ToDouble(d["MONTHZJ"]) + " ,TOTALEDWORK= " + Convert.ToDouble(d["MONTHWORK"]) + "  WHERE CARDID=" + d["CARDID"].ToString() + " AND CHOSCODE=" + d["CHOSCODE"].ToString());
                    }
                }
            }
            return true;
        }

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null)
            {
                msg = "无效的SQL参数，不能继续执行操作！";
                return "ok";
            }
            if (data.Sql.Equals("ModifyOrAddInfo"))
            {
                if (data.Param["DEPREID"] == null || data.Param["DEPREID"].ToString() == "")
                {
                    //新增
                    data.Param["DEPREID"] = DaoTool.Seq(dao, "LKEQ.SEQEQDepre");
                    if (DaoTool.Save(dao, OptContent.get("SaveEQDepreciationInfo_EQDepreciation"), data) > -1)
                    {
                        saveRunDetail(dao, data);//2代表新增
                        msg = "执行成功！";
                        return "ok";
                    }
                    else
                    {
                        msg = "执行失败！";
                        throw new Exception("新增折旧主表信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    //修改
                    if (DaoTool.Update(dao, OptContent.get("SaveEQDepreciationInfo_EQDepreciation"), data) > -1)
                    {
                        saveRunDetail(dao, data);
                    }
                    else
                    {
                        msg = "执行失败！";
                        throw new Exception("修改折旧主表信息失败！" + dao.ErrMsg);
                    }
                }
                msg = "执行成功！";
                return "ok";
            }

            else
            {
                msg = "系统出错，请与管理员联系！";
                return "ok";
            }
        }

        #endregion
    }
}
