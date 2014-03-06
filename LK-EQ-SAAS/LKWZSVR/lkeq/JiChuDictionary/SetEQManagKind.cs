using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    public class SetEQManagKind : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            if (data.Sql != null && data.Sql.Equals("Delete"))
            {
                Opt opt = OptContent.get("DeleteEQKind");
                if (DaoTool.ExecuteNonQuery(dao, opt, data) < 0)
                {
                    throw new Exception("删除设备类别信息失败！" + dao.ErrMsg);
                }
                msg = "设备类别已删除";
                return "ok";
            }

            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                Opt opt = OptContent.get("SaveSetEQDetail");
                if (DaoTool.Save(dao, opt, data) > -1)
                {
                    msg = "设置管理的设备类别成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("设置管理的设备类别失败" + dao.ErrMsg);
                }
            }

            else
            {
                throw new Exception("设置管理的设备类别失败" + dao.ErrMsg);
            }
        }

        #endregion
    }
}
