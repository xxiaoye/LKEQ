using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using YtService.action;
using YtService.util;
using YtService.config;

namespace LKWZSVR.his.sys
{
    public class SaveGroup :IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Param.ContainsKey("dicgrpid"))
            {
                if (DaoTool.Update(dao, OptContent.get("SaveDictGrounp"), data) > 0)
                {
                    msg = "更新成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存失败！");
                }
            }
            else {
                int i = DaoTool.ExecuteScalar(dao, OptContent.get("GetMaxDictGrounpId"), data).ToInt() + 1;
                data.Param["dicgrpid"] = i;
                if (DaoTool.Save(dao, OptContent.get("SaveDictGrounp"), data) > 0)
                {
                    msg = "保存成功！";
                    return "ok";
                }
                else {
                    throw new Exception("保存失败！");
                }
            }
        }

        #endregion
    }
}
