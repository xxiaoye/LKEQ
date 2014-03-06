using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;
using YiTian.db;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    class EQSupplySvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            
            msg = "厂商信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem Obj, Obj1, Obj2,  Obj4;
            if ("SaveChangShangInfo".Equals(ac))
            {
                
                pa["IFFACTORY"] = Convert.ToDecimal(data.Param["IFFACTORY"]);
                pa["IFSUPPLY"] = Convert.ToDecimal(data.Param["IFSUPPLY"]);
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["SUPPLYNAME"] = data.Param["SUPPLYNAME"].ToString();
                pa["USERNAME"] = data.Param["USERNAME"].ToString();
                pa["RECDATE"] =DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["PYCODE"] != null)
                {
                    pa["PYCODE"] = data.Param["PYCODE"].ToString();
                }
                else
                {
                    pa["PYCODE"] =null;
                }
                if (data.Param["WBCODE"] != null)
                {
                    pa["WBCODE"] = data.Param["WBCODE"].ToString();
                }
                else
                {
                    pa["WBCODE"] = null;
                }
                if (data.Param["UNITPROPERTY"] != null)
                {
                    pa["UNITPROPERTY"] = data.Param["UNITPROPERTY"].ToString();
                }
                else
                {
                    pa["UNITPROPERTY"] = null;
                }
                if (data.Param["QYDM"] != null)
                {
                    pa["QYDM"] = data.Param["QYDM"].ToString();
                }
                else
                {
                    pa["QYDM"] = null;
                }
                if (data.Param["FRDB"] != null)
                {
                    pa["FRDB"] = data.Param["FRDB"].ToString();
                }
                else
                {
                    pa["FRDB"] = null;
                }
                if (data.Param["UNITBANK"] != null)
                {
                    pa["UNITBANK"] = data.Param["UNITBANK"].ToString();
                }
                else
                {
                    pa["UNITBANK"] = null;
                }
                if (data.Param["BANKACCOUNT"] != null)
                {
                    pa["BANKACCOUNT"] = data.Param["BANKACCOUNT"].ToString();
                }
                else
                {
                    pa["BANKACCOUNT"] = null;
                }
                if (data.Param["ADDRESS"] != null)
                {
                    pa["ADDRESS"] = data.Param["ADDRESS"].ToString();
                }
                else
                {
                    pa["ADDRESS"] = null;
                }
                if (data.Param["RELMAN"] != null)
                {
                    pa["RELMAN"] = data.Param["RELMAN"].ToString();
                }
                else
                {
                    pa["RELMAN"] = null;
                }
                if (data.Param["RELPHONE"] != null)
                {
                    pa["RELPHONE"] = data.Param["RELPHONE"].ToString();
                }
                else
                {
                    pa["RELPHONE"] = null;
                }
                if (data.Param["POST"] != null)
                {
                    pa["POST"] = data.Param["POST"].ToString();
                }
                else
                {
                    pa["POST"] = null;
                }
                if (data.Param["FAX"] != null)
                {
                    pa["FAX"] = data.Param["FAX"].ToString();
                }
                else
                {
                    pa["FAX"] = null;
                }
                if (data.Param["JYXKZ"] != null)
                {
                    pa["JYXKZ"] = data.Param["JYXKZ"].ToString();
                }
                else
                {
                    pa["JYXKZ"] = null;
                }
                if (data.Param["YYZZ"] != null)
                {
                    pa["YYZZ"] = data.Param["YYZZ"].ToString();
                }
                else
                {
                    pa["YYZZ"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["TAXCODE"] != null)
                {
                    pa["TAXCODE"] = data.Param["TAXCODE"].ToString();
                }
                else
                {
                    pa["TAXCODE"] = null;
                }
                if (data.Param["SUPPLYID"] == null)//新增
                {
                    pa["SUPPLYID"] = DaoTool.Seq(dao, "LKEQ.SEQEQSUPPLY");
                    Opt saveInfo = OptContent.get("EQSaveChangShangInfo");
                  Opt  name = OptContent.get("EQSaveChangShangInfo_Name");

                    Obj = DaoTool.ExecuteScalar(dao, name, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加厂商信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else   //修改
                {
                    pa["SUPPLYID"] = Convert.ToDecimal(data.Param["SUPPLYID"]);
                    Opt updataInfo = OptContent.get("EQUpdataChangShangInfo");
                   Opt  updataname = OptContent.get("EQUpdataChangShangInfo_Name");


                    Obj = DaoTool.ExecuteScalar(dao, updataname, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改厂商信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }


            }
             if ("EQDelChangShangInfo".Equals(ac))
            {
                Opt Del = OptContent.get("EQDelChangShangInfo");
                

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["SUPPLYID"] = data.Param["SUPPLYID"].ToString();

                Opt values1 = OptContent.get("EQDelChangShangInfo_Use1");
                Opt values2 = OptContent.get("EQDelChangShangInfo_Use2");
                //Opt values3 = OptContent.get("EQDelChangShangInfo_Use3");
                Opt values4 = OptContent.get("EQDelChangShangInfo_Use4");
                Obj1 = DaoTool.ExecuteScalar(dao, values1, pa);
                Obj2 = DaoTool.ExecuteScalar(dao, values2, pa);
                //Obj3 = DaoTool.ExecuteScalar(dao, values3, pa);|| !Obj3.IsNull
                Obj4 = DaoTool.ExecuteScalar(dao, values4, pa);
                if ( !Obj1.IsNull  || !Obj2.IsNull  || !Obj4.IsNull)
                {
                    msg = "不能删除已使用的厂商，只能停用！";
                    return "ok";
                }
                // //DaoTool.
                //if (Obj.ToString()=="1")
                //{
                //    msg = "已使用过,不能删除只能停用！";
                //    return "ok";
                //}

                //string values1 = LData.Es("DelChangShangInfo_Use1", "LKWZ", new object[] { dr["厂商ID"], His.his.Choscode });
                //string values2 = LData.Es("DelChangShangInfo_Use2", null, new object[] { dr["厂商ID"], His.his.Choscode });
                //string values3 = LData.Es("DelChangShangInfo_Use3", null, new object[] { dr["厂商ID"], His.his.Choscode });
                //string values4 = LData.Es("DelChangShangInfo_Use4", null, new object[] { dr["厂商ID"], His.his.Choscode });
              
                if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                    throw new Exception("删除厂商信息失败！");
                msg = "删除成功！";
                return "ok";
               
            }
             if ("StopWZSupplyInfo".Equals(ac))
             {
                 Opt stop = OptContent.get("EQStartOrStopChangShangInfo");
                
                 pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                 pa["SUPPLYID"] = data.Param["SUPPLYID"].ToString();
                 pa["IFUSE"] = 0;
                 if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                     throw new Exception("停用厂商信息失败！");
                 msg = "停用成功！";
                 return "ok";
             
             }
             if ("StartWZSupplyInfo".Equals(ac))
             {
                 Opt start = OptContent.get("EQStartOrStopChangShangInfo");

                 pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                 pa["SUPPLYID"] = data.Param["SUPPLYID"].ToString();
                 pa["IFUSE"] = 1;
                 if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                     throw new Exception("启用厂商信息失败！");
                 msg = "启用成功！";
                 return "ok";

             }
            return "ok";
        }

        

        #endregion
    }
}