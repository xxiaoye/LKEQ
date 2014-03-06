using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQChangeManagSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null || data.Sql.ToString() == "")
            {
                msg = "无效的SQL参数，无法继续进行操作！";
                return "ok";
            }
            if (data.Sql.Equals("ModifyOrAddInfo"))
            {
                //新增的
                if (data.Param["CHANGEID"] == null || data.Param["CHANGEID"].ToString() == "")
                {
                    data.Param["CHANGEID"] = DaoTool.Seq(dao, "LKEQ.SEQEQChange");
                    if (DaoTool.Save(dao, OptContent.get("SaveEQChangeInfo_EQChangeManag"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备变动记录失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    return "ok";
                }

                if (DaoTool.Update(dao, OptContent.get("SaveEQChangeInfo_EQChangeManag"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备变动记录失败！" + dao.ErrMsg);
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
    }

        #endregion
}

