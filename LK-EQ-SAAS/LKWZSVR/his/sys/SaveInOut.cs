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
    public class SaveInOut : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            //SysValueManag
            if (data.Sql == null)
            {
                throw new Exception("Sql内容为空！");
            }
            else if (data.Sql.Equals("Del"))
            {
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("InOutIsSy"), data).ToInt();
                if (count > 0)
                {
                   // throw new Exception("此入出类型已经被系统使用，不能删除！");
                    msg = "此入出类型已经被系统使用，不能删除！";
                    return "ok";
                }
                    
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelInOutInfo"), data) < 0)
                {
                    throw new Exception("删除此入出类型失败！");
                }
                msg = "此入出类型已删除！";
                return "ok";
            }
            else if (data.Sql.Equals("Add"))
            {
                   Opt op = OptContent.get("SaveInOutInfo");
                   data.Param["IOID"] = DaoTool.Seq(dao, "LKWZ.SEQWZIO"); ;
                   data.Param["RECDATE"] = DateTime.Now;
                   if (data.Param["IFDEFAULT"] != null && data.Param["IFDEFAULT"].ToString() == "1")
                   {
                       dao.ExecuteNonQuery("update LKWZ.DICTWZINOUT set IFDEFAULT=0 where CHOSCODE='" + data.Param["CHOSCODE"].ToString() + "' and IOFALG=" + data.Param["IOFLAG"].ToString() + " and OPFLAG=" + data.Param["OPFLAG"].ToString());
                   }
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
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
                Opt op = OptContent.get("SaveInOutInfo");
                data.Param["RECDATE"] = DateTime.Now;
                if (data.Param["IFDEFAULT"] != null && data.Param["IFDEFAULT"].ToString() == "1")
                {
                    dao.ExecuteNonQuery("update LKWZ.DICTWZINOUT set IFDEFAULT=0 where CHOSCODE='" + data.Param["CHOSCODE"].ToString() + "'");
                }
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
                Opt op = OptContent.get("SetInOutUnUsed");
                if (DaoTool.ExecuteNonQuery(dao, op, data) != 1)
                {
                    throw new Exception("停用失败！" + dao.ErrMsg);
                }

                msg = "此入出类型停用成功！";
                return "ok";
            }
            else if(data.Sql.Equals("QiYong"))
            {
                Opt op = OptContent.get("SetInOutUsed");
                if (DaoTool.ExecuteNonQuery(dao, op, data) != 1)
                {
                    throw new Exception("启用失败！" + dao.ErrMsg);
                }

                msg = "此入出类型启用成功！";
                return "ok";

            }
            else if (data.Sql.Equals("Copy"))
            {
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("CheckInOutIsNull"), data).ToInt();
                if (count == 0)
                {
                    DataTable dt = DaoTool.FindDT(dao, OptContent.get("FindInOutCcode0"), data);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            data.Param["IOID"] = DaoTool.Seq(dao, "LKWZ.SEQWZIO");
                            data.Param["IONAME"] = r["IONAME"];
                            data.Param["PYCODE"] = r["PYCODE"];
                            data.Param["WBCODE"] = r["WBCODE"];
                            data.Param["IFUSE"] = r["IFUSE"];
                            data.Param["RECIPECODE"] = r["RECIPECODE"];
                            data.Param["RECIPELENGTH"] = r["RECIPELENGTH"];
                            data.Param["RECIPEYEAR"] = r["RECIPEYEAR"];
                            data.Param["RECIPEMONTH"] = r["RECIPEMONTH"];
                            data.Param["MEMO"] = r["MEMO"];
                            data.Param["IOFLAG"] = r["IOFLAG"];
                            data.Param["USEST"] = r["USEST"];
                            data.Param["USEND"] = r["USEND"];
                            data.Param["USERD"] = r["USERD"];
                            data.Param["OPFLAG"] = r["OPFLAG"];
                            data.Param["IFDEFAULT"] = r["IFDEFAULT"];
                            data.Param["RECDATE"] = DateTime.Now;
                            if (DaoTool.Save(dao, OptContent.get("SaveInOutInfo"), data) != 1)
                            {
                                throw new Exception("复制失败，！" + dao.ErrMsg);
                            }
                        }
                    }

                    msg = "复制成功！";
                    return "ok";
                }
                else
                {
                    msg = "已存在本医疗结构的出入类型，复制失败！";
                    return "ok";
                }

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
