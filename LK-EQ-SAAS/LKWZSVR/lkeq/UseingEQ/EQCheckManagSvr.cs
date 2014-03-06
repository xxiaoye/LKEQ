using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQCheckManagSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null)
            {
                msg = "无效的SQL参数，无法继续执行！";
                return "ok";
            }
            if (data.Sql.Equals("ModifyOrAddInfo"))
            {
                //新增的
                if (data.Param["CHECKID"] == null || data.Param["CHECKID"].ToString() == "")
                {
                    data.Param["CHECKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQCheck");
                    if (DaoTool.Save(dao, OptContent.get("SaveEQMaintainInfo_EQCheck"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备检查记录失败！" + dao.ErrMsg);
                    }
                    msg = "新增检查记录成功！";
                    return "ok";
                }

                if (DaoTool.Update(dao, OptContent.get("SaveEQMaintainInfo_EQCheck"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备检查记录失败！" + dao.ErrMsg);
                }
                msg = "修改检查记录成功！";
                return "ok";
            }

            else
            {
                msg = "系统出错，请与管理员联系！";
                return "ok";
            }
        }

        #endregion
    }
}
