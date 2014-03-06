using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    public class EQWare : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            #region 针对删除
            //针对删除
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                //验证是否在其他表中被使用
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveUseEQWareInfo"), data).ToInt() > 0)
                {
                    msg = "库房被使用，不能被删除，只能进行停用操作！";
                    return "ok";
                }
                Opt op = OptContent.get("DelEQInfo");
                if (DaoTool.ExecuteNonQuery(dao, op, data) > 0)
                {
                    msg = "库房删除成功";
                    return "ok";
                }
                else
                {
                    throw new Exception("库房删除失败" + dao.ErrMsg);
                }

            }
            #endregion

            #region 针对停用
            //针对停用
            if (data.Sql != null && data.Sql.Equals("Disa"))
            {
                Opt op = OptContent.get("SaveEQInfo");
                if (data.Param.ContainsValue(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "停用库房信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("停用库房信息失败！" + dao.ErrMsg);
                    }
                }
            }
            #endregion

            #region 针对启用
            //针对启用
            if (data.Sql != null && data.Sql.Equals("Enab"))
            {
                Opt op = OptContent.get("SaveEQInfo");
                if (data.Param.ContainsValue(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "启用库房信息成功";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("启用库房信息失败！" + dao.ErrMsg);
                    }
                }
            }
            #endregion

            //针对保存
            if (data.Sql != null && data.Sql.Equals("Save"))
            {
                Opt op = OptContent.get("SaveEQInfo");
                //感觉就是一个小陷阱，原来是value   如果为修改
                if (data.Param.ContainsValue(op.Key))
                {
                    //更新设备库房信息
                    string kd = "0";
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("ModifyEQInfoIsRepeat"), data).ToInt();
                    if (repeat > 0)
                    {
                        msg = "已经存在该库房信息，不能修改成该名称！";
                        return "ok";
                    }

                    //判断是否存在该类别编码([DICTEQWAREDETAIL])  kindcode [如果存在则置为1]
                    int tr = DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveEQkindcode"), data).ToInt();
                    if (tr > 0)
                    {
                        kd = "1";
                    }
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = kd + "," + "保存库房信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存库房信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    //增加设备库房信息
                    string wd = null;
                    int repeat = DaoTool.ExecuteScalar(dao, OptContent.get("AddEQInfoIsRepeat"), data).ToInt();//添加时   名字是否重复了
                    if (repeat > 0)
                    {
                        msg = "已经存在该库房信息！";
                        return "ok";
                    }

                    int warecode_int = DaoTool.ExecuteScalar(dao, OptContent.get("SaveEQInfo_seq"), data).ToInt() + 1;//+1是新增一个
                    if (warecode_int == 100)
                    {
                        msg = "库房已满，不能继续添加！";
                        return "ok";
                    }
                    //用于转化库房编码，当库房编码小于10，则前面添加0   wd是将库房编码返回去？
                    if (warecode_int >= 0 && warecode_int < 10)
                    {
                        data.Param["warecode"] = "0" + warecode_int.ToString();
                        wd = "0" + warecode_int.ToString();
                    }
                    else
                    {
                        data.Param["warecode"] = warecode_int.ToString();
                        wd = warecode_int.ToString();
                    }

                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        msg = wd + "," + "添加成功";//作为返回
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加库房信息失败！" + dao.ErrMsg);
                    }

                }

            }
            else
            {
                throw new Exception("保存设备信息失败！" + dao.ErrMsg);
            }
        }

        #endregion
    }
}
