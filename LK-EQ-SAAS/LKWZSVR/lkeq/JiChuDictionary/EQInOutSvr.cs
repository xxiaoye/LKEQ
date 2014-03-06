using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Windows.Forms;
using System.Data;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    public class EQInOutSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("TingYong"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("QiTingEQInOutInfo_EQInOutManag"), data) > 0)
                {
                    msg = "停用该条设备入出库记录成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("停用该条设备入出库信息失败！" + dao.ErrMsg);
                }
            }


            if (data.Sql != null && data.Sql.Equals("QiYong"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("QiTingEQInOutInfo_EQInOutManag"), data) > 0)
                {
                    msg = "启用该条设备入出库记录成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("启用该条设备入出库记录失败！" + dao.ErrMsg);
                }
            }


            if (data.Sql != null && data.Sql.Equals("Del"))
            {
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IfHaveUseEQInOut"), data).ToInt() > 0)
                {
                    msg = "该条设备入出库记录已被使用，无法删除！";
                    return "ok";
                }
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DelEQInOutInfo_EQInOutManag"), data) > 0)
                {
                    msg = "删除该条设备入出库记录成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除该条设备入出库记录失败！" + dao.ErrMsg);
                }
            }


            //----------------------------------这里的代码有些重复，有时间再进行复用操作【关键是msg 这里怎么办？】-------------------------------


            if (data.Sql != null && data.Sql.Equals("Modify"))
            {
                //惯例： 名称是否重复 // 相同的医疗机构编码、入出标志、操作标志的记录里，只能有一条记录的该值为1
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveRepeatInOutName"), data).ToInt() > 0)
                {
                    msg = "存在相同的设备入出库名称，请修改后再进行操作";
                    return "ok";
                }
                //MessageBox.Show("执行到修改2");
                if (data.Param["IFDEFAULT"].ToString() == "1" && DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveMoreDefault_EQInOutManag"), data).ToInt() > 0)
                {
                    msg = "相同的医疗机构编码、入出标志、操作标志的记录里，只能有一条记录的默认值为是！";
                    return "ok";
                }
                //MessageBox.Show("执行到修改3");
                //验证：对于一个医疗机构来说，其操作类别为（1：调拨；2：申领；3：盘点）的出入库类型记录
                //其入库和出库的类型记录，分别各有且只能有一条记录；
                //如医疗机构甲，其调拨的入库方式，只能有一种方式，其调拨的出库方式，也只能有一种方式；
                if (data.Param["OPFLAG"].ToString() != "0" && DaoTool.ExecuteScalar(dao, OptContent.get("IsSingleInOutOfCHOS"), data).ToInt() > 0)
                {
                    //针对1,2,3
                    msg = "操作为调拨，申领，盘点的记录，其出库与入库的记录只能各有一条，请修改后再进行操作";
                    return "ok";
                }
                //MessageBox.Show("执行到修改4");

                if (DaoTool.Update(dao, OptContent.get("SaveEQInOutInfo"), data) < 0)
                {
                    throw new Exception("修改该条设备入出库记录失败！");
                }
                msg = "修改该条设备入出库记录成功！";
                return "ok";
            }




            if (data.Sql != null && data.Sql.Equals("Add"))
            {
                //首先设置好ID：
                data.Param["IOID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIO");

                //验证是否重名
                if (DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveRepeatInOutName"), data).ToInt() > 0)
                {
                    msg = "存在相同的设备入出库名称，请修改后再进行操作";
                    return "ok";
                }

                if (data.Param["IFDEFAULT"].ToString() == "1" && DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveMoreDefault_EQInOutManag"), data).ToInt() > 0)
                {
                    msg = "相同的医疗机构编码、入出标志、操作标志的记录里，只能有一条记录的默认值为是！";
                    return "ok";
                }
                if (data.Param["OPFLAG"].ToString() != "0" && DaoTool.ExecuteScalar(dao, OptContent.get("IsSingleInOutOfCHOS"), data).ToInt() > 0)
                {
                    //针对1,2,3
                    msg = "操作为调拨，申领，盘点的记录，其出库与入库的记录只能各有一条，请修改后再进行操作";
                    return "ok";
                }
                if (DaoTool.Save(dao, OptContent.get("SaveEQInOutInfo"), data) < 0)
                {
                    throw new Exception("新增设备入出库记录失败！");

                }
                msg = "新增设备入出库记录成功！";
                return "ok";
            }

            if (data.Sql != null && data.Sql.Equals("CopyChoscodeData"))
            {
                //验证本机构是否包含数据
                int count = DaoTool.ExecuteScalar(dao, OptContent.get("IsHaveAnyDataEQInOut_EQInOutManag"), data).ToInt();
                if (count == 0)
                {
                    DataTable dt = DaoTool.FindDT(dao, OptContent.get("FindEQInOutcode0_EQInOutManag"), data);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            int statuscode_int = DaoTool.ExecuteScalar(dao, OptContent.get("GetEQInOutIDMax"), data).ToInt() + 1;
                            data.Param["IOID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIO");
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
                            data.Param["OPFLAG"] = r["OPFLAG"];
                            data.Param["IFDEFAULT"] = r["IFDEFAULT"];
                            data.Param["RECDATE"] = DateTime.Now;
                            if (DaoTool.Save(dao, OptContent.get("SaveEQInOutInfo"), data) < 0)
                            {
                                throw new Exception("复制失败！" + dao.ErrMsg);
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
            else
            {
                msg = "系统故障，请与管理员联系！";
                return "ok";
            }
        }

        #endregion

    }


}
