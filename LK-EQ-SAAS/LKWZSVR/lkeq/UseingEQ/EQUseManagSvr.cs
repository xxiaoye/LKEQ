using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQUseManagSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null || data.Sql == "")
            {
                msg = "没有传入有效的SQL参数，无法进行操作！";
                return "ok";
            }

            if (data.Sql.Equals("AddOrSaveEQUseManage"))
            {
                if (data.Param["USEID"] == null || data.Param["USEID"].ToString() == "")
                {
                    data.Param["USEID"] = DaoTool.Seq(dao, "LKEQ.SEQEQUse");
                    if (DaoTool.Save(dao, OptContent.get("SaveEQCardUseInfo_UseManagEdit"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备使用记录失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    return "ok";
                }
                if (DaoTool.Update(dao, OptContent.get("SaveEQCardUseInfo_UseManagEdit"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备使用记录失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
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
