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
    class EQFixKindSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            
            msg = "维修类别信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem Obj, Obj1;
            if ("SaveFixKindInfo".Equals(ac))
            {

                pa["IFDEFAULT"] = Convert.ToDecimal(data.Param["IFDEFAULT"]);
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["REPAIRNAME"] = data.Param["REPAIRNAME"].ToString();
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
              
             
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["IFDEFAULT"].ToString() == "1")
                {
                    Opt EQIfHaveValue_FixKindInfo = OptContent.get("EQIfHaveValue_FixKindInfo");

                   ObjItem robjitm= DaoTool.ExecuteScalar(dao, EQIfHaveValue_FixKindInfo, data);

                   if (!robjitm.IsNull)
                   {
                       Opt EQIfOnlyOne_FixKindInfo = OptContent.get("EQIfOnlyOne_FixKindInfo");

                       int rownum = DaoTool.ExecuteNonQuery(dao, EQIfOnlyOne_FixKindInfo, pa);

                       if (rownum<1)
                       {
                           throw new Exception("修改设备类别信息的默认值失败！");
                       }
                   }
                
                }

                if (data.Param["REPAIRCODE"] == null)//新增
                {

                    Opt saveInfo_REPAIRCODE = OptContent.get("EQSaveFixKindInfo_REPAIRCODE");
                   
                    Obj = DaoTool.ExecuteScalar(dao, saveInfo_REPAIRCODE, pa);
                    if (Obj.IsNull)
                    {
                        pa["REPAIRCODE"] = "01";
                    }
                    else if ((Obj.ToInt() + 1).ToString().Trim().Length == 1)
                    {
                        pa["REPAIRCODE"] = "0" + (Obj.ToInt() + 1).ToString();

                    }
                    else
                    {
                        pa["REPAIRCODE"] = (Obj.ToInt() + 1).ToString();


                    }


                    Opt saveInfo = OptContent.get("EQSaveFixKindInfo");
                    Opt name = OptContent.get("EQSaveFixKindInfo_Name");

                    Obj = DaoTool.ExecuteScalar(dao, name, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加设备类别信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else   //修改
                {
                    pa["REPAIRCODE"] = Convert.ToDecimal(data.Param["REPAIRCODE"]);
                    Opt updataInfo = OptContent.get("EQUpdataFixKindInfo");
                    Opt updataname = OptContent.get("EQUpdataFixKindInfo_Name");


                    Obj = DaoTool.ExecuteScalar(dao, updataname, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改维修类别信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }


            }
             if ("DelFixKindInfo".Equals(ac))
            {
                Opt Del = OptContent.get("DelFixKindInfo");
                

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["REPAIRCODE"] = data.Param["REPAIRCODE"].ToString();

                Opt values1 = OptContent.get("EQDelFixKindInfo_Use1");
       
                Obj1 = DaoTool.ExecuteScalar(dao, values1, pa);

                if ( !Obj1.IsNull)
                {
                    msg = "不能删除已使用的维修类别，只能停用！";
                    return "ok";
                }
              
                if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                    throw new Exception("删除维修类别失败！");
                msg = "删除成功！";
                return "ok";
               
            }
             if ("StopEQFixKindInfo".Equals(ac))
             {
                 Opt stop = OptContent.get("EQStartOrStopFixKindInfo");
                
                 pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                 pa["REPAIRCODE"] = data.Param["REPAIRCODE"].ToString();
                 pa["IFUSE"] = 0;
                 if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                     throw new Exception("停用该维修类别信息失败！");
                 msg = "停用成功！";
                 return "ok";
             
             }
             if ("StartEQFixKindInfo".Equals(ac))
             {
                 Opt start = OptContent.get("EQStartOrStopFixKindInfo");

                 pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                 pa["REPAIRCODE"] = data.Param["REPAIRCODE"].ToString();
                 pa["IFUSE"] = 1;
                 if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                     throw new Exception("启用该维修类别信息失败");
                 msg = "启用成功！";
                 return "ok";

             }
            return "ok";
        }

        

        #endregion
    }
}