using System;
using System.Data;
using System.Configuration;
using System.Linq;


using System.Xml.Linq;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.sys
{
    public class SaveHisParam:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "ok";
            string A = data.Sql;
            string choscode = data.Param["cHosCode"].ToString();
            if ("Find".Equals(A)) {
                object[] objs = new object[1];
                objs[0] = dao.find("select ID,sysvalue,choscode,sysexplain from 系统参数 t where (choscode is null or choscode=?) and not exists" +
                                   "(select * from 系统参数 where choscode=? and t.id=id and t.choscode is null)",
                                        new object[] { choscode,choscode }); 
                return objs; 
            }
            else if ("Save".Equals(A))
            {
                if (data.Param.ContainsKey("密码"))
                {
                    string pwd = data.Param["密码"].ToString();
                    object p = dao.ExecuteScalar("select passwd from 密码 where id=3");
                    if (p != null)
                    {
                        if (!p.ToString().Equals(pwd))
                        {
                            throw new Exception("输入的密码无效请重新输入！");
                        }
                    }
                    else
                    {
                        throw new Exception("输入的密码无效请重新输入！");
                    }
                }

                string sql = "" ,edsql = "",insql = "";
                int num = int.Parse(data.Param["num"].ToString());
                for (int i = 1; i <= num; i++)
                {
                    if (data.Param.ContainsKey("ID" + i.ToString()))
                    {
                        if (data.Param.ContainsKey("note" + i.ToString()))
                        {
                            insql = "'" + data.Param["note" + i.ToString()].ToString() + "'";
                            edsql = ",sysexplain=" + insql ;                            
                        }
                        else
                        {
                            edsql = "";
                            insql = "''";
                        }

                        if (!data.Param.ContainsKey("TYPE" + i.ToString()))
                        {
                            if (!choscode.Equals(""))
                                sql = "UPDATE 系统参数 SET sysvalue='" + data.Param["ID" + i.ToString()].ToString() + "'"+ edsql + " WHERE ID=" + i.ToString() + " and choscode='" + choscode + "'";
                            else
                                sql = "UPDATE 系统参数 SET sysvalue='" + data.Param["ID" + i.ToString()].ToString() + "' WHERE ID=" + i.ToString() + " and choscode is null ";
                        }
                        else
                            sql = "insert into 系统参数 values(" + i.ToString() + ",'" + data.Param["TYPE" + i.ToString()].ToString() + "','" +
                                                                 data.Param["ID" + i.ToString()].ToString() + "'," + insql + ",'" + choscode + "')";

                        if (dao.ExecuteNonQuery(sql,new object[] {}) < 0)
                            throw new Exception("更新系统参数失败！");
                    }
                } 
            } 
            return "ok";
        }

        #endregion
    }
}
