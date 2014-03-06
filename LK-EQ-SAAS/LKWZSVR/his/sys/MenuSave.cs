using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;

namespace LKWZSVR.his.sys
{
    public class MenuSave : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string sql = "update SYSCLOBDICT set dictvalue=? where dictname=?";
            int  i = dao.ExecuteNonQuery(sql, new object[] { data.Param["dictvalue"], "menuinfo" });
            if (i < 0) throw new Exception("更新菜单信息失败！");
            msg = "保存成功！";
            return "ok";
        }

        #endregion
    }
}
