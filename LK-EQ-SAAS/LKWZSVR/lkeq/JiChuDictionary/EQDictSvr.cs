using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    class EQDictSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            msg = "设备信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem Obj,Obj1,Obj2,Obj3,Obj4,Obj5,Obj6,Obj8;
            if ("SaveDictEQInfo".Equals(ac))
            {
                pa["EQNAME"] = data.Param["EQNAME"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                pa["COUNTCODE"] = data.Param["COUNTCODE"].ToString();
                pa["UNITCODE"] = data.Param["UNITCODE"].ToString();
                pa["IFJL"] = Convert.ToDecimal(data.Param["IFJL"]);
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["ZJTYPE"] = Convert.ToDecimal(data.Param["ZJTYPE"]);
                pa["ZJRATE"] = Math.Round( Convert.ToDecimal(data.Param["ZJRATE"]),2);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                if (data.Param["WORKUNITCODE"] != null)
                {
                    pa["WORKUNITCODE"] = data.Param["WORKUNITCODE"].ToString();
                }
                else
                {
                    pa["WORKUNITCODE"] = null;
                }
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }
              
          

                if (data.Param["PYCODE"] != null)
                {
                    pa["PYCODE"] = data.Param["PYCODE"].ToString();
                }
                else
                {
                    pa["PYCODE"] = null;
                }
                if (data.Param["WBCODE"] != null)
                {
                    pa["WBCODE"] = data.Param["WBCODE"].ToString();
                }
                else
                {
                    pa["WBCODE"] = null;
                }
                if (data.Param["SHORTNAME"] != null)
                {
                    pa["SHORTNAME"] = data.Param["SHORTNAME"].ToString();
                }
                else
                {
                    pa["SHORTNAME"] = null;
                }
                if (data.Param["SHORTCODE"] != null)
                {
                    pa["SHORTCODE"] = data.Param["SHORTCODE"].ToString();
                }
                else
                {
                    pa["SHORTCODE"] = null;
                }
                if (data.Param["ALIASNAME"] != null)
                {
                    pa["ALIASNAME"] = data.Param["ALIASNAME"].ToString();
                }
                else
                {
                    pa["ALIASNAME"] = null;
                }
                if (data.Param["ALIASCODE"] != null)
                {
                    pa["ALIASCODE"] = data.Param["ALIASCODE"].ToString();
                }
                else
                {
                    pa["ALIASCODE"] = null;
                }
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

                if (data.Param["PRODUCTPLACE"] != null)
                {
                    pa["PRODUCTPLACE"] = data.Param["PRODUCTPLACE"].ToString();
                }
                else
                {
                    pa["PRODUCTPLACE"] = null;
                }
                if (data.Param["TXM"] != null)
                {
                    pa["TXM"] = data.Param["TXM"].ToString();
                }
                else
                {
                    pa["TXM"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["EQID"] == null)
                {
                    pa["EQID"] = DaoTool.Seq(dao, "LKEQ.SEQEQ");
                    Opt saveInfo = OptContent.get("SaveDictEQInfo");
                    Opt name = OptContent.get("SaveDictEQInfo_Name");

                    Obj = DaoTool.ExecuteScalar(dao, name, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加设备信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else
                {
                    pa["EQID"] = Convert.ToDecimal(data.Param["EQID"]);
                    Opt updataInfo = OptContent.get("UpdataDictEQInfoInfo");
                    Opt updataname = OptContent.get("UpdataDictEQInfoInfo_Name");


                    Obj = DaoTool.ExecuteScalar(dao, updataname, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改设备信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }

             
            }
            if ("DelEQDictInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
               
                pa["EQID"] = Convert.ToDecimal(data.Param["EQID"]);

                Opt Del = OptContent.get("DelEQDictInfo");

                    Opt values1 = OptContent.get("DelEQDictInfo_Use1");
                    Opt values2 = OptContent.get("DelEQDictInfo_Use2");
                    Opt values3 = OptContent.get("DelEQDictInfo_Use3");
                    Opt values4 = OptContent.get("DelEQDictInfo_Use4");
                    Opt values5 = OptContent.get("DelEQDictInfo_Use5");
                    Opt values6 = OptContent.get("DelEQDictInfo_Use6");
                    //Opt values7 = OptContent.get("DelWZDictInfo_Use7");//有可能这里还有涉及到其他表的可能。 || !Obj7.IsNull
                    Opt values8 = OptContent.get("DelEQDictInfo_Use8");
                    Obj1 = DaoTool.ExecuteScalar(dao, values1, pa);
                    Obj2 = DaoTool.ExecuteScalar(dao, values2, pa);
                    Obj3 = DaoTool.ExecuteScalar(dao, values3, pa);
                    Obj4 = DaoTool.ExecuteScalar(dao, values4, pa);
                    Obj5 = DaoTool.ExecuteScalar(dao, values5, pa);
                    Obj6 = DaoTool.ExecuteScalar(dao, values6, pa);
                    //Obj7 = DaoTool.ExecuteScalar(dao, values7, pa);
                    Obj8 = DaoTool.ExecuteScalar(dao, values8, pa);
                    if (!Obj1.IsNull || !Obj2.IsNull || !Obj3.IsNull || !Obj4.IsNull || !Obj5.IsNull || !Obj6.IsNull || !Obj8.IsNull)
                    {
                        msg = "不能删除已使用的设备，只能停用！";
                        return "ok";
                    }





                    if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                        throw new Exception("删除设备信息失败！");

                   
                    msg = "删除成功！";
                    return "ok";
               
          
                }

           
            if ("StopEQDictInfo".Equals(ac))
            {
                Opt stop = OptContent.get("StartOrStopEQDictInfo");

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["EQID"] = data.Param["EQID"].ToString();
                pa["IFUSE"] = 0;
                if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                    throw new Exception("停用设备信息失败！");
                msg = "停用成功！";
                return "ok";

            }
            if ("StartEQDictInfo".Equals(ac))
            {
                Opt start = OptContent.get("StartOrStopEQDictInfo");

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["EQID"] = data.Param["EQID"].ToString();
                pa["IFUSE"] = 1;
                if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                    throw new Exception("启用设备信息失败！");
                msg = "启用成功！";
                return "ok";

            }
            return "ok";

        }
        #endregion
    }
}
