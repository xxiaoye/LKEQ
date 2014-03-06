using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;

namespace LKWZSVR.his.sys
{
    public class OpTsDeposit : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string A = data.Sql;
            msg = "";
            if ("Add".Equals(A))
            {
                if (dao.ExecuteNonQuery("insert into 特殊预交金表(类型,编码,最低预交金,choscode) values(" +
                                        data.Param["type"].ToString() + ",'" + data.Param["code"].ToString() + "'," +
                                        data.Param["deposit"].ToString() + ",'" +data.Param["choscode"].ToString() + "')") < 0)
                    throw new Exception("");

            }
            else if ("Edit".Equals(A))
            {  
                if (dao.ExecuteNonQuery("update 特殊预交金表 set 类型=?,编码=?,最低预交金=? where choscode=? and 类型=? and 编码=?",
                    new object[] { data.Param["type"], data.Param["code"], data.Param["deposit"], data.Param["choscode"], 
                                   data.Param["type"], data.Param["oldcode"]}) < 0) 
                  throw new Exception("更改错误。");

            }
            else if ("Del".Equals(A))
            {
                if (dao.ExecuteNonQuery("delete 特殊预交金表 where choscode='" + data.Param["choscode"].ToString() + "' and " +
                     "类型=" + data.Param["type"] + " and 编码='" + data.Param["code"]+"'") < 0)
                {
                    throw new Exception("");
                }

            }
            return "ok";
        }

        #endregion
    }
}
