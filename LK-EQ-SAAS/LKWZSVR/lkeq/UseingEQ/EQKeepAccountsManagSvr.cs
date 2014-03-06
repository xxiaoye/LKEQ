using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.lkeq.UseingEQ
{
    public class EQKeepAccountsManagSvr : IEx
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
                if (data.Param["NOID"] == null || data.Param["NOID"].ToString() == "")
                {
                    data.Param["NOID"] = DaoTool.Seq(dao, "LKEQ.SEQEQXZNo");
                    Opt op = OptContent.get("SaveEQAccount");

                    if (DaoTool.Save(dao,op,data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备下账记录失败！" + dao.ErrMsg);
                    }
                    msg = "新增检查记录成功！";
                    return "ok";
                }

                if (DaoTool.Update(dao, OptContent.get("SaveEQAccount"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备下账记录失败！" + dao.ErrMsg);
                }
                msg = "修改下账记录成功！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("JDPass"))
            {
                data.Param["JDDATE"] = DateTime.Now;
                if (DaoTool.Update(dao, OptContent.get("SaveEQAccount"), data) > -1)
                {
                    msg = "鉴定下账信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("鉴定下账信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("JDNo"))
            {

                if (DaoTool.Update(dao, OptContent.get("SaveEQAccount"), data) > -1)
                {
                    msg = "鉴定下账信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("鉴定下账信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("SHPass"))
            {
                Opt op = OptContent.get("ChangTheStatus_EQAccount");
                data.Param["SHDATE"] = DateTime.Now;
                if (DaoTool.Update(dao, OptContent.get("SaveEQAccount"), data) > -1)
                {
                    if (DaoTool.ExecuteNonQuery(dao, op, data) > 0)
                    {
                        msg = "审核下账信息成功！";

                        return "ok";
                    }
                    else
                    {
                        throw new Exception("修改卡片设备状态失败！" + dao.ErrMsg);
                    }
                   
                }
                else
                {
                    throw new Exception("审核下账信息失败！" + dao.ErrMsg);
                }
            }

            if (data.Sql != null && data.Sql.Equals("SHNo"))
            {

                if (DaoTool.Update(dao, OptContent.get("SaveEQAccount"), data) > -1)
                {
                    msg = "审核下账信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("审核下账信息失败！" + dao.ErrMsg);
                }
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
