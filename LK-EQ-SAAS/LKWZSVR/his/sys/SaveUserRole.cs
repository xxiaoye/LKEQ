using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using YtService.action;

namespace LKWZSVR.his.sys
{
    public class SaveUserRole:IEx
    {

        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string ac = data.Sql;
            msg = "";
            if ("setRole".Equals(ac))
            {
                string dataRole = data.Param["roles"].ToString(); 
                string userid = data.Param["userid"].ToString();
                if (userid == null || userid.Trim().Length == 0)
                    throw new Exception("无效用户ID！");
                int i = 0;
                if (data.Param["have"].ToString().Equals("1"))
                    i = dao.ExecuteNonQuery("DELETE FROM T_HIS_RELUSERRIGHT WHERE userid=" + userid) ;
                if ( i >=0 )
                {
                    if (dataRole !=null && dataRole.Length > 0)
                    {
                        string[] rs = dataRole.Split(',');
                        foreach (string r in rs)
                        {
                            if (dao.ExecuteNonQuery("INSERT INTO T_HIS_RELUSERRIGHT (userid,roleid) VALUES (" + userid + "," + r + ")") == -1)
                            {
                                throw new Exception("保存用户角色信息失败！");
                            }
                        }
                    }
                    msg = "成功保存用户角色！";
                }
                else
                {
                    throw new Exception("删除原用户角色信息失败！");
                }
            }
            else if ("setOpKF".Equals(ac))
            {
                string dataKF = data.Param["kfID"].ToString(); 
                string userid = data.Param["userid"].ToString();
                string choscode = data.Param["choscode"].ToString();
                if (userid == null || userid.Trim().Length == 0)
                    throw new Exception("无效用户ID！");
                int i = 0 ;
                if (data.Param["have"].ToString().Equals("1"))
                  i = dao.ExecuteNonQuery("DELETE FROM 可操作库房 WHERE userid=" + userid + " and choscode='" + choscode+"'") ;

                if (i>=0)
                {
                    if (dataKF != null && dataKF.Length > 0)
                    {
                        string[] rs = dataKF.Split(',');
                        foreach (string r in rs)
                        {
                            if (dao.ExecuteNonQuery("INSERT INTO 可操作库房 (userid,kfid,choscode) VALUES (" + userid + "," + r + ",'"+choscode+"')") == -1)
                            {
                                throw new Exception("保存可操作库房信息失败！");
                            }
                        }
                    }
                    msg = "成功保存可操作库房！";
                }
                else
                {
                    throw new Exception("删除原可操作库房信息失败！");
                }
                
            }
            return "ok";
        }

        #endregion
    }
}
