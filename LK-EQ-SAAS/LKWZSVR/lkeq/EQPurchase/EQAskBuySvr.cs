using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.EQPurchase
{
    class EQAskBuySvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            msg = "请购信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            //ObjItem Obj,Obj1,Obj2,Obj3,Obj4;
            if ("SaveEQAskBuyInfo".Equals(ac))
            {
                pa["DEPTID"] = Convert.ToDecimal(data.Param["DEPTID"]);
                pa["EQID"] = Convert.ToDecimal(data.Param["EQID"]);
                pa["EQNAME"] = data.Param["EQNAME"].ToString();



                pa["COUNTRY"] =data.Param["COUNTRY"].ToString();
                pa["UNITCODE"] =data.Param["UNITCODE"].ToString();
                pa["APPLYNUM"] = Convert.ToDecimal(data.Param["APPLYNUM"]);
                if (data.Param["YJPRICE"] != null && data.Param["YJPRICE"].ToString().Trim().Length>0 )
                {

                    pa["YJPRICE"] = Math.Round(Convert.ToDecimal(data.Param["YJPRICE"]), 2);
                }
                else
                {
                    pa["YJPRICE"] = null;
                }

               
                pa["YJMONEY"] = Math.Round(Convert.ToDecimal(data.Param["YJMONEY"]), 2);

                pa["PLANDATE"] = Convert.ToDateTime(data.Param["PLANDATE"]);
                pa["STATUS"] = Convert.ToDecimal(data.Param["STATUS"]);

                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                if (data.Param["GG"] != null)
                {

                    pa["GG"] = data.Param["GG"].ToString();
                }
                else
                {
                    pa["GG"] = null;
                }

                if (data.Param["XH"] != null)
                {
                    pa["XH"] = data.Param["XH"].ToString();
                }
                else
                {
                    pa["XH"] = null;
                }




                if (data.Param["REASON"] != null)
                {
                    pa["REASON"] = data.Param["REASON"].ToString();
                }
                else
                {
                    pa["REASON"] = null;
                }
                if (data.Param["XYFX"] != null)
                {
                    pa["XYFX"] = data.Param["XYFX"].ToString();
                }
                else
                {
                    pa["XYFX"] = null;
                }
                if (data.Param["PTTJ"] != null)
                {
                    pa["PTTJ"] = data.Param["PTTJ"].ToString();
                }
                else
                {
                    pa["PTTJ"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }


                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }

                if (data.Param["APPLYID"] == null)
                {
                    pa["APPLYID"] = DaoTool.Seq(dao, "LKEQ.SEQEQApply");

                    Opt saveInfo = OptContent.get("SaveEQAskBuyInfo");
                   
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加请购信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else
                {
                    pa["APPLYID"] = Convert.ToDecimal(data.Param["APPLYID"]);
                    Opt updataInfo = OptContent.get("UpdataEQAskBuyInfo");

                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改请购信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }

             
            }
            if ("DelEQAskBuyInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                pa["APPLYID"] = Convert.ToDecimal(data.Param["APPLYID"]);
                pa["STATUS"] = 0;
                Opt Del = OptContent.get("DelEQAskBuyInfo");


                    if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                        throw new Exception("删除请购信息失败！");

                   
                    msg = "删除成功！";
                    return "ok";
               
          
                }

            if ("Submit_EQAskBuyInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                pa["APPLYID"] = Convert.ToDecimal(data.Param["APPLYID"]);
                pa["STATUS"] = 1;
                Opt Del = OptContent.get("Submit_EQAskBuyInfo");


                    if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                        throw new Exception("提交请购信息失败！");

                   
                    msg = "提交成功！";
                    return "ok";
               
          
                }
            if ("EQAskBuyInfo_SHCG".Equals(ac))
            {
                Opt stop = OptContent.get("EQAskBuyInfo_SHCG");
                pa["APPLYID"] = Convert.ToDecimal(data.Param["APPLYID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["SHNUM"] = Convert.ToDecimal(data.Param["SHNUM"]);
                pa["RECDATE"] = DateTime.Now;
                pa["SHDATE"] = Convert.ToDateTime(data.Param["SHDATE"]);
                pa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                pa["STATUS"] = 6;
                if (data.Param["SHUSERNAME"] != null)
                {
                    pa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                }
                else
                {
                    pa["SHUSERNAME"] = null;
                }

                if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                    throw new Exception("审核操作失败！");
                msg = "审核操作成功！";
                return "ok";

            }
            if ("EQAskBuyInfo_SHSB".Equals(ac))
            {
                Opt start = OptContent.get("EQAskBuyInfo_SHSB");

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["APPLYID"] = Convert.ToDecimal(data.Param["APPLYID"]);
                pa["STATUS"] = 2;
                if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                    throw new Exception("审核操作失败！");
                msg = "审核操作成功！";
                return "ok";

            }
            return "ok";

        }
        #endregion
    }
}
