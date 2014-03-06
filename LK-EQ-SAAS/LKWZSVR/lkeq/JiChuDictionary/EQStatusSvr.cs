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
    public class EQStatusSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
           
            
            
            #region 停用，启用，删除
            if (data.Sql != null && data.Sql.Equals("TingYong"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("QiTingEQStatus_EQStatusManag"), data) < 0)
                {
                    throw new Exception("停用设备状态信息失败！" + dao.ErrMsg);
                }
                msg = "停用设备状态信息成功！";
                return "ok";
            }

            if (data.Sql != null && data.Sql.Equals("QiYong"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("QiTingEQStatus_EQStatusManag"), data) < 0)
                {
                    throw new Exception("启用设备状态信息失败！" + dao.ErrMsg);
                }
                msg = "启用设备状态信息成功！";
                return "ok";

            }
            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                //检查是否被其他表所使用，这里是EQCARDREC
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IfHaveUseEQStatus"), data).ToInt() > 0)
                {
                    msg = "该条设备状态信息被其他数据所使用，无法删除";
                    return "ok";
                }

                //如果能够执行到这儿的话，说明不被其他数据所使用
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelEQStatusInfo"), data) > 0)
                {
                    msg = "删除该条设备状态信息成功";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除该条设备状态信息失败" + dao.ErrMsg);
                }
            } 
            #endregion


            if (data.Sql != null && data.Sql.Equals("Add"))
            {
                //求状态编码[等于0 表示没有数据 且至少成功。]
                int statuscode_int = DaoTool.ExecuteScalar(dao, OptContent.get("GetStatusCodeMax_EQStatusManag"), data).ToInt() + 1;
                if (statuscode_int > 0 && statuscode_int < 10)
                {
                    data.Param["STATUSCODE"] = "0" + statuscode_int.ToString();
                }
                if (statuscode_int >= 100)
                {
                    msg = "状态编码已满，不能继续添加！";
                    return "ok";
                }
                if (statuscode_int >= 10 && statuscode_int < 100)
                {
                    data.Param["STATUSCODE"] = statuscode_int.ToString();
                }

                //判断名称是否重复
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveRepetEQStatusName"), data).ToInt() > 0)
                {
                    msg = "已经存在该状态名称，请修改后提交！";
                    return "ok";
                }

                if (DaoTool.Save(dao, OptContent.get("SaveEQStatusInfo"), data) > -1)
                {
                    msg = "增加设备状态信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("增加设备状态信息失败" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("Modify"))
            {
                //判断名称是否重复
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveRepetEQStatusName"), data).ToInt() > 0)
                {
                    msg = "已经存在该状态名称，请修改后提交！";
                    return "ok";
                }
                if (DaoTool.Update(dao, OptContent.get("SaveEQStatusInfo"), data) > -1)
                {
                    msg = "修改设备状态信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("修改设备状态信息失败" + dao.ErrMsg);
                }
            }
            else
            {
                msg = "系统错误，请与管理员取得联系！";
                return "ok";
            }
        }

        #endregion
    }
}
