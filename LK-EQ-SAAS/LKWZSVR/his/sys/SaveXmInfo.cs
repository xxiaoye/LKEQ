using System;

using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.sys
{
    public class SaveXmInfo : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql!=null && data.Sql.Equals("Del"))
            {
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("XmIsSy"), data).ToInt();
                if (count > 0)
                    throw new Exception("项目信息已经被系统使用，不能删除！");
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelXmInfo"), data) < 0)
                {
                    throw new Exception("删除项目信息失败！");
                }
                msg = "项目信息已删除！";
                return "ok";
            }
            else if (data.Sql != null && data.Sql.Equals("CheckNHCode"))
            {

                msg = "项目信息已删除！";
                return "ok";
            }
            else
            {
                Opt op = OptContent.get("SaveXmInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        /* if (DaoTool.ExecuteNonQuery(dao, OptContent.get("UpdateZYCFMXInfo"), data) < 0)
                         {
                             throw new Exception("更新住院处方明细失败！");
                         }
                         if (DaoTool.ExecuteNonQuery(dao, OptContent.get("UpdateMZCFMXInfo"), data) < 0)
                         {
                             throw new Exception("更新门诊处方明细失败！");
                         } */

                        msg = "保存项目信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存项目信息失败！");
                    }
                }
                else
                {

                    data.Param[op.Key] = DaoTool.ExecuteScalar(dao, OptContent.get("XmiInfo_seq"), data).ToInt() + 1;
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        msg = "保存项目信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存项目信息失败！");
                    }
                }
            }


        }

        #endregion
    }
}
