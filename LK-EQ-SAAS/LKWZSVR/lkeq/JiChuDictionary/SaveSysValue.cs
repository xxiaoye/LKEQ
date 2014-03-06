using System;
using System.Data;
using System.Configuration;
using System.Linq;

using System.Xml.Linq;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    public class SysValue : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "系统参数更改";
            if (data.Sql == null)
            {
                throw new Exception("Sql内容为空！");
            }
            else if (data.Sql.Equals("Add"))
            {
                

                //if (dao.ExecuteNonQuery(OptContent.get("InsertSysValue").Sql, data.Params) == 5)
                String[] s=OptContent.get("EQInsertSysValue").Sql.Split('?');
                    String sql = "";
                    for (int i = 0; i < s.Length-1; i++)
                    {
                        if(data.Params[i].GetType().ToString().ToUpper()=="SYSTEM.INT32")
                        sql += s[i] + data.Params[i].ToString();
                        else
                        sql+=s[i] + "'" + data.Params[i].ToString() + "'";
                    }
                    sql += s[s.Length - 1];
                    s = sql.Split(';');
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (dao.ExecuteNonQuery(s[i]) != 1)
                        {
                            throw new Exception("修改失败，在系统参数表中插入失败！" + dao.ErrMsg);
                        }
                    }
                    msg = "修改成功！";
                    return "ok";
            }
            else if (data.Sql.Equals("Update"))
            {
                String[] s = OptContent.get("EQUpdateSysValue").Sql.Split('?');
                String sql = "";
                for (int i = 0; i < s.Length-1; i++)
                {
                    if (data.Params[i].GetType().ToString().ToUpper() == "SYSTEM.INT32")
                        sql += s[i] + data.Params[i].ToString();
                    else
                        sql += s[i] + "'" + data.Params[i].ToString() + "'";
                }
                sql += s[s.Length - 1];
                s = sql.Split(';');
                for (int i = 0; i < s.Length; i++)
                {
                    if (dao.ExecuteNonQuery(s[i]) != 1)
                    {
                        throw new Exception("修改失败，在系统参数表中修改失败！" + dao.ErrMsg);
                    }
                }
                msg = "修改成功！";
            }
            else if (data.Sql.Equals("Find"))
            {

                //FindSysValue
                msg = "查找成功！";
                return "ok";
            }
          
            return "ok";
        }

        #endregion
    }
}
