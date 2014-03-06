using System;
using System.Data;
using System.Configuration;
using System.Linq;


using System.Xml.Linq;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.sys
{
    public class SaveGhDw:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string A = data.Sql;
            Opt op = OptContent.get("SaveYPGYSInfo");
            if ("Del".Equals(A)) {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelGhDwInfo"), data) < 0)
                {
                    throw new Exception("删除供货商信息失败！");
                }
                msg = "供货商信息已删除！";
                return "ok";
            }
            else if ("Save".Equals(A))
            {
                if (DaoTool.ExecuteScalar(dao, OptContent.get("HaveGhDwInfo"), data).ToInt() > 0)
                    throw new Exception("编号为【" + data.Param["编号"].ToString() + "】的单位已经存在！");
                if (DaoTool.Save(dao, op, data) > -1)
                {
                    msg = "保存成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存失败！");
                }
            
            }
            else if ("Update".Equals(A))
            {
                if (DaoTool.Update(dao, op, data) > -1)
                {
                    msg = "保存成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存失败！");
                }
            }
            msg = "ok";
            return "ok";
        }

        #endregion
    }
}
