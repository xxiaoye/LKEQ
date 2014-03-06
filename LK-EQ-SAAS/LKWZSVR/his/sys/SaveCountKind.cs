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
    public class SaveCountKind : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
           
            if (data.Sql == null)
            {
                throw new Exception("Sql内容为空！");
            }
            else if (data.Sql.Equals("Del"))
            {
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("CkIsSy"), data).ToInt();
                if (count > 0)
                {
                   // throw new Exception("此统计类别已经被系统使用，不能删除！");
                    msg = "此统计类别已经被系统使用，不能删除！";
                    return "ok";
                }
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelCkInfo"), data) < 0)
                {
                    throw new Exception("删除统计类别信息失败！");
                }
                if (DaoTool.ExecuteScalar(dao, OptContent.get("GetCk_seq"), data).ToInt() == 0)
                {
                    data.Param["COUNTCODE"] = data.Param["SUPERCODE"];
                    DaoTool.ExecuteNonQuery(dao, OptContent.get("SetSuperCkEnd"), data);
                }
                msg = "此统计类别已删除！";
                return "ok";
            }
            else if (data.Sql.Equals("Add"))
            {
                   Opt op = OptContent.get("SaveCkInfo");
                   data.Param["COUNTCODE"] = (DaoTool.ExecuteScalar(dao, OptContent.get("GetCk_seq"), data).ToInt() + 1).ToString("00");
                   if (!data.Param["SUPERCODE"].ToString().Equals("0"))
                       data.Param["COUNTCODE"] = data.Param["SUPERCODE"].ToString() + data.Param["COUNTCODE"].ToString();
                   
                    data.Param["RECDATE"] = DateTime.Now;

                    

                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        Opt op1 = OptContent.get("SetSuperCkNotEnd");
                        data.Param["COUNTCODE"] = data.Param["SUPERCODE"];
                        if (data.Param["COUNTCODE"].ToString() !="0")
                        {
                            if (DaoTool.ExecuteNonQuery(dao, op1, data) != 1)
                            {
                                throw new Exception("更新父节点状态失败！" + dao.ErrMsg);
                            }
                        }

                        msg = "统计类别添加成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("统计类别添加失败！" + dao.ErrMsg);
                    }
                
            }
            else if (data.Sql.Equals("Update"))
            {
                Opt op = OptContent.get("SaveCkInfo");
                //data.Param["COUNTCODE"] = DaoTool.ExecuteScalar(dao, OptContent.get("GetCk_seq"), data).ToInt() + 1;
                data.Param["RECDATE"] = DateTime.Now;
                if (DaoTool.Update(dao, op, data) > -1)
                {
                    msg = "统计类别更新成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("统计类别更新失败！" + dao.ErrMsg);
                }
            }
            else if(data.Sql.Equals("TingYong"))
            {
                Opt op = OptContent.get("SetCkUnUsed");
                if (DaoTool.ExecuteNonQuery(dao, op, data) != 1)
                {
                    throw new Exception("停用失败！" + dao.ErrMsg);
                }

                msg = "统计类别停用成功！";
                return "ok";
            }
            else if(data.Sql.Equals("QiYong"))
            {
                Opt op = OptContent.get("SetCkUsed");
                if (DaoTool.ExecuteNonQuery(dao, op, data) != 1)
                {
                    throw new Exception("启用失败！" + dao.ErrMsg);
                }

                msg = "统计类别启用成功！";
                return "ok";

            }
            else if (data.Sql.Equals("Find"))
            {
                msg = "查找成功！";
                return "ok";
            }
            msg = "成功！";
            return "ok";
        }

        #endregion
    }
}
