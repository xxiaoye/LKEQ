using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Windows.Forms;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    public class EQCountKindSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            //针对启用
            if (data.Sql != null && data.Sql.Equals("QiYong"))
            {
                Opt op = OptContent.get("QiTingEQCountKind");
                if (DaoTool.ExecuteNonQuery(dao, op, data) > 0)
                {
                    msg = "启用该统计类别信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("启用该统计类别信息失败！");
                }
            }
            if (data.Sql != null && data.Sql.Equals("TingYong"))
            {
                Opt op = OptContent.get("QiTingEQCountKind");
                if (DaoTool.ExecuteNonQuery(dao, op, data) > 0)
                {
                    msg = "停用该统计类别信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("停用该统计类别信息失败");
                }
            }

            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                Opt op = OptContent.get("IfHaveUse_EQCountKindManag");
                if (DaoTool.ExecuteScalar(dao, op, data).ToInt() > 0)
                {
                    //被使用
                    msg = "该设备统计类别已被使用，不能删除，只能停用！";
                    return "ok";
                }
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelEQCKInfo_EQCountKindManag"), data) < 0)
                {
                    throw new Exception("删除设备统计类别信息失败");
                }
                //在配置端已经设置好为  当前COUNTCODE作为SUPERCODE的子目录[IsHaveChildCK]
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveChildCK"), data).ToInt() == 0)
                {
                    //检验一次，若节点下没有子节点，则更新其为末节点  SetCKSuperEnd
                    DaoTool.ExecuteNonQuery(dao, OptContent.get("SetSuperEQCKEnd_EQCountKindManag"), data);
                }
                msg = "删除设备统计类别信息成功！";
                return "ok";
            }


            if (data.Sql != null && data.Sql.Equals("Add"))
            {
                Opt op = OptContent.get("SaveCountKind");


                //当上级节点确定，获取下级节点的个数+1为下级节点的一部分
                data.Param["COUNTCODE"] = (DaoTool.ExecuteScalar(dao, OptContent.get("GetEQCountCode"), data).ToInt() + 1).ToString("00");
                //当上级节点不为0[根节点]   其实在客户端就已经写好了，这里只是校验一下
                if (!data.Param["SUPERCODE"].ToString().Equals("0"))
                {
                    data.Param["COUNTCODE"] = data.Param["SUPERCODE"].ToString() + data.Param["COUNTCODE"].ToString();
                }

                data.Param["RECDATE"] = DateTime.Now;
                if (DaoTool.ExecuteScalar(dao, OptContent.get("isRepeatCountName"), data).ToInt() == 0)
                {
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        //保存了之后，我们必须将对应的父节点设置为非末节点
                        Opt op1 = OptContent.get("SetSuperEQCKNotEnd");
                        data.Param["COUNTCODE"] = data.Param["SUPERCODE"];
                        if (data.Param["COUNTCODE"].ToString() != "0")
                        {
                            if (DaoTool.ExecuteNonQuery(dao, op1, data) != 1)
                            {
                                throw new Exception("更新父节点状态失败！" + dao.ErrMsg);
                            }
                        }
                        msg = "设备统计类别添加成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("设备统计类别添加失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    msg = "已经存在该统计类别名称！";
                    return "ok";
                }
            }
            if (data.Sql != null && data.Sql.Equals("Modify"))
            {
                Opt op = OptContent.get("SaveCountKind");
                data.Param["RECDATE"] = DateTime.Now;
                if (DaoTool.ExecuteScalar(dao, OptContent.get("isRepeatCountName"), data).ToInt() == 0)
                {
                    //当设置为末节点时，检查其是否包含子节点
                    if (data.Param["IFEND"].ToString() == "1")
                    {
                        if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveChildCK"), data).ToInt() > 0)
                        {
                            msg = "该节点下包含子节点，不能设置其为末节点！";
                            return "ok";
                        }
                        else
                        {
                            if (DaoTool.Update(dao, op, data) > -1)
                            {
                                msg = "更新设备统计类别信息成功！";
                                return "ok";
                            }
                            else
                            {
                                throw new Exception("更新设备统计类别失败！" + dao.ErrMsg);
                            }
                        }
                    }
                    else
                    {
                        if (DaoTool.Update(dao, op, data) > -1)
                        {
                            msg = "更新设备统计类别信息成功！";
                            return "ok";
                        }
                        else
                        {
                            throw new Exception("更新设备统计类别失败！" + dao.ErrMsg);
                        }
                    }

                }
                else
                {
                    msg = "已经存在该统计类别名称！";
                    return "ok";
                }
            }

            else
            {
                msg = "系统出错，请联系管理员！";
                return "ok";
            }
        }

        #endregion
    }
}
