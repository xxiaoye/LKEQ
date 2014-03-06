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
    class EQDateDivideSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            
            msg = "维修类别信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem Obj, Obj1;
            if ("SaveDateDivideInfo".Equals(ac))
            {

                pa["DATENAME"] = data.Param["DATENAME"].ToString();
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["USERNAME"] = data.Param["USERNAME"].ToString();
                pa["RECDATE"] =DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["BEGINDATE"] = DateTime.Parse(Convert.ToDateTime(data.Param["BEGINDATE"]).ToString("yyyy-MM-dd"));
                pa["ENDDATE"] = DateTime.Parse(Convert.ToDateTime(data.Param["ENDDATE"]).ToString("yyyy-MM-dd"));
               
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }

                if (data.Param["DATEID"] == null)//新增
                {

                    Opt saveInfo_REPAIRCODE = OptContent.get("EQSaveDateDivideInfo_ID");
                   
                    Obj = DaoTool.ExecuteScalar(dao, saveInfo_REPAIRCODE, pa);
                    if (Obj.IsNull)
                    {
                        pa["DATEID"] = 1;
                    }
                    else 
                    {
                        pa["DATEID"] = Obj.ToInt() + 1;

                    }
                    

                    Opt saveInfo = OptContent.get("EQSaveDateDivideInfo");
                    Opt name = OptContent.get("EQSaveDateDivideInfo_Name");

                    //Opt Date = OptContent.get("EQSaveDateDivideInfo_Date");//判断日期是否重复？
                    Obj = DaoTool.ExecuteScalar(dao, name, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("添加新的期间划分信息失败！");
                    msg = "添加成功！";
                    return "ok";
                }
                else   //修改
                {
                    pa["DATEID"] = Convert.ToDecimal(data.Param["DATEID"]);
                    Opt updataInfo = OptContent.get("EQUpdataDateDivideInfo");
                    Opt updataname = OptContent.get("EQUpdataDateDivideInfo_Name");

                    //Opt Date = OptContent.get("EQSaveDateDivideInfo_Date");//判断日期是否重复？

                    Obj = DaoTool.ExecuteScalar(dao, updataname, pa);
                    if (!Obj.IsNull)
                    {
                        msg = "名称重复！";
                        return "ok";
                    }
                    if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                        throw new Exception("修改期间划分信息失败！");
                    msg = "修改成功！";
                    return "ok";
                }


            }
             if ("DelDateDivideInfo".Equals(ac))
            {
                Opt Del = OptContent.get("DelDateDivideInfo");
                

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["DATEID"] = data.Param["DATEID"].ToString();

                Opt values1 = OptContent.get("EQDelDateDivideInfo_Use1");
       
                Obj1 = DaoTool.ExecuteScalar(dao, values1, pa);

                if ( !Obj1.IsNull)
                {
                    msg = "不能删除已使用的期间划分，只能停用！";
                    return "ok";
                }
              
                if (DaoTool.ExecuteNonQuery(dao, Del, pa) < 0)
                    throw new Exception("删除期间划分失败！");
                msg = "删除成功！";
                return "ok";
               
            }
            
            
            return "ok";
        }

        

        #endregion
    }
}