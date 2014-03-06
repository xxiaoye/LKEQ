using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQKeepFitManagSvr : IEx
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
                if (data.Param["MAINTAINID"] == null || data.Param["MAINTAINID"].ToString() == "")
                {
                    data.Param["MAINTAINID"] = DaoTool.Seq(dao, "LKEQ.SEQEQMaintain");
                    if (DaoTool.Save(dao, OptContent.get("SaveEQMaintainInfo_EQKeepFitManag"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备保养记录失败！" + dao.ErrMsg);
                    }
                    msg = "新增保养记录成功！";
                    return "ok";
                }

                if (DaoTool.Update(dao, OptContent.get("SaveEQMaintainInfo_EQKeepFitManag"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备保养记录失败！" + dao.ErrMsg);
                }
                msg = "修改保养记录成功！";
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
