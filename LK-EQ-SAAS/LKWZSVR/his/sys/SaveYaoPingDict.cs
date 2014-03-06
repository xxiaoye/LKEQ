using System;
using System.Data;
using System.Configuration;
using System.Linq;

using System.Xml.Linq;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.his.sys
{
    public class SaveYaoPingDict : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("YaoPingIsSy"), data).ToInt();
                if (count > 0)
                    throw new Exception("药品已经被系统使用，不能删除！");
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelYaoPingInfo"), data) < 0)
                {
                    throw new Exception("删除药品信息失败！");
                }
                msg = "药品已删除！";
                return "ok";
            }
            else
            {
                Opt op = OptContent.get("SaveYPDictInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "保存项目信息成功！";
                        return "ok";
                    }
                    else {
                        throw new Exception("保存项目信息失败！"+dao.ErrMsg);
                    }
                }
                else
                {
                    data.Param["药品编码"] = DaoTool.ExecuteScalar(dao, OptContent.get("SaveYPDictInfo_seq"), data).ToInt() + 1;
                    if (DaoTool.Save(dao, op, data) > -1) {
                        msg = "保存药品信息成功";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存药品信息失败！" + dao.ErrMsg);
                    }
                }
            }
        }

        #endregion
    }
}
