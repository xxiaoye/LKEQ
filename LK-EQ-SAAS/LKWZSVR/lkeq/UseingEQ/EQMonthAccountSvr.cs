using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQMonthAccountSvr : IEx
    {
        /// <summary>
        /// 这里均为套用前面的设备折旧的保存
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="data"></param>
        /// <returns></returns>

        bool saveRunDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> XiList = ObjConvert.GetParamsByStr(data.Param["XmlDataList"].ToString());
            Opt opt2 = OptContent.get("SaveEQDepreciationDetailInfo_EQDepreciation");
            foreach (Dictionary<string, object> d in XiList)
            {
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
                //全部为新增过来的折旧ID（固定一个）
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
            return true;
        }

        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql.Equals("") || data.Sql == null)
            {
                msg = "SQL参数错误，无法继续进行操作！";
                return "ok";
            }
            if (data.Sql.Equals("QueRenYueJie"))
            {
                //月结的时候就是新增自动折旧【所以 折旧ID肯定为空】
                data.Param["DEPREID"] = DaoTool.Seq(dao, "LKEQ.SEQEQDepre");
                if (DaoTool.Save(dao, OptContent.get("SaveEQDepreciationInfo_EQDepreciation"), data) > -1)
                {
                    saveRunDetail(dao, data);
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
                msg = "系统出错，请与管理员联系！";
                return "ok";
            }
        }

        #endregion
    }
}
