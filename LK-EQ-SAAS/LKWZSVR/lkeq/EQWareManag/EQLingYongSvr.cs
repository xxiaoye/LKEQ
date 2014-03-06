using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Data;
using System.Windows.Forms;

namespace LKWZSVR.lkeq.WareManag
{
    public class EQLingYongSvr : IEx
    {
        #region IEx 成员

        string OutIdHelp;
        string InIdHelp;
        string InDetailNoHelp;
        string StockIdHelp;

        string RecipeCodeInMain;
        object SHDHHelp;
        object SUPPLYIDHelp;
        object SUPPLYNAMEHelp;




        string OutIdCXHelp;//冲销时的出库主表ID
        string InIdCXHelp;
        private static string Recipecode(string BefoRecipe, int year, int month, int length)
        {
            string str = BefoRecipe;
            int Dlength = length - 4;

            if (year == 0 && month == 0)
            {
                str += 0.ToString("D" + (length - str.Length));
            }
            if (year == 1 && month == 1)
            {

                str += DateTime.Now.Year.ToString("D4") + DateTime.Now.Month.ToString("D2");
            }
            if (year == 1 && month == 0)
            {
                Dlength = Dlength - 4;
                str += DateTime.Now.Year.ToString("D4");
                str += 0.ToString("D" + Dlength);
            }
            if (year == 0 && month == 1)
            {
                Dlength = Dlength - 2;
                str += 0.ToString("D" + Dlength) + DateTime.Now.Month.ToString("D2");
            }
            return str;
        }


        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null)
            {
                msg = "缺省SQL参数，无法继续进行操作！";
                return "ok";
            }
            if (data.Sql.Equals("ModifyOrAddZhu"))
            {
                //只有当新增主表的时候才需要生成对应的recipecode
                if (data.Param["RECIPECODE"] == null || data.Param["RECIPECODE"].ToString() == "")
                {
                    DataTable dt = DaoTool.FindDT(dao, OptContent.get("GetRecipeCodeInEQOutInfo"), data);
                    if (dt.Rows.Count <= 0 || dt == null)
                    {
                        throw new Exception("无法查找到单据前缀" + dao.ErrMsg);
                    }
                    DataRow row = dt.Rows[0];
                    data.Param["RECIPECODE"] = Recipecode(row["RECIPECODE"].ToString(), Convert.ToInt32(row["RECIPEYEAR"]), Convert.ToInt32(row["RECIPEMONTH"]), Convert.ToInt32(row["RECIPELENGTH"]));
                    data.Param.Add("RECIPECODEHelp", data.Param["RECIPECODE"].ToString() + "%");
                    data.Param["RECIPECODE"] = data.Param["RECIPECODE"] + DaoTool.ExecuteScalar(dao, OptContent.get("GetrecipeCodeNo"), data).ToInt().ToString("D2");
                }
                if (data.Param["OUTID"] == null || data.Param["OUTID"].ToString() == "")
                {
                    data.Param["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOut");
                    OutIdHelp = data.Param["OUTID"].ToString();

                    if (DaoTool.Save(dao, OptContent.get("ModifyOutMainInfo"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("新增设备领用的主表信息失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    return "ok";
                }
                if (DaoTool.Update(dao, OptContent.get("ModifyOutMainInfo"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备领用的出库主表信息失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }


            if (data.Sql.Equals("ModifyOrAddXi"))
            {
                //如果出库Id为空的话，肯定操作为新增，肯定都是新增的细表行。 首先获取生成的主表OUTID  
                if (data.Param["OUTID"] == null || data.Param["OUTID"].ToString() == "")
                {
                    data.Param["OUTID"] = OutIdHelp;
                    data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                    if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("修改设备领用的出库细表失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    return "ok";
                }
                else//操作为修改
                {
                    if (data.Param["DETAILNO"] == null || data.Param["DETAILNO"].ToString().Trim() == "")
                    {
                        //为新增的数据  新增完之后返回
                        data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                        if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                        {
                            msg = "执行失败！";
                            throw new Exception("修改设备领用的出库细表失败！" + dao.ErrMsg);
                        }
                        msg = "执行成功！";
                        return "ok";
                    }
                    if (DaoTool.Update(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("修改设备领用的出库细表失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    //能够retrun ok 说明 要么是前面新增的对了，要么是后面的修改正确，不然都是throw出去
                    return "ok";
                }
            }

            if (data.Sql.Equals("SHOutMainInfo"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("UpdateStatusInLingYong"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("更新审核状态失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //===================================审核部分=====================================
            //入库主表
            if (data.Sql.Equals("InsertInMain"))
            {
                data.Param["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");

                InIdHelp = data.Param["INID"].ToString();

                DataTable dt = DaoTool.FindDT(dao, OptContent.get("GetRecipeCodeInEQOutInfo"), data);
                if (dt.Rows.Count <= 0 || dt == null)
                {
                    throw new Exception("无法查找到单据前缀" + dao.ErrMsg);
                }
                DataRow row = dt.Rows[0];
                data.Param["RECIPECODE"] = Recipecode(row["RECIPECODE"].ToString(), Convert.ToInt32(row["RECIPEYEAR"]), Convert.ToInt32(row["RECIPEMONTH"]), Convert.ToInt32(row["RECIPELENGTH"]));
                data.Param.Add("RECIPECODEHelp", data.Param["RECIPECODE"].ToString() + "%");
                data.Param["RECIPECODE"] = data.Param["RECIPECODE"] + DaoTool.ExecuteScalar(dao, OptContent.get("GetrecipeCodeNo"), data).ToInt().ToString("D2");

                RecipeCodeInMain = data.Param["RECIPECODE"].ToString();

                SHDHHelp = data.Param["SHDH"];
                SUPPLYIDHelp = data.Param["SUPPLYID"];
                SUPPLYNAMEHelp = data.Param["SUPPLYNAME"];

                if (DaoTool.Save(dao, OptContent.get("SaveEQInMainInfoCX"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("入库主表的信息插入失败！" + dao.ErrMsg);
                }
                //更新对应生成的INID至当前的出库主表
                dao.ExecuteNonQuery("UPDATE LKEQ.EQOUTMAIN SET INID=" + data.Param["INID"].ToString() + " WHERE CHOSCODE=" + data.Param["CHOSCODE"] + "  AND OUTID=" + data.Param["OUTID"].ToString());
                msg = "执行成功！";
                return "ok";
            }


            //入库细表
            if (data.Sql != null && data.Sql.Equals("InsertEQInDetail"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                InDetailNoHelp = data.Param["DETAILNO"].ToString();
                data.Param["INID"] = InIdHelp;
                if (DaoTool.Save(dao, OptContent.get("SaveEQInDetail"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("入库细表信息执行失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //库存主表   可能不会执行  【只有在库存主表，不存在该设备的信息】
            if (data.Sql != null && data.Sql.Equals("InsertStockMain"))
            {
                data.Param["STOCKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCK");
                //在每执行一次对库存总表的插入，都会更新一次stockid,否则，就是针对   一个stockid的插入流水
                StockIdHelp = data.Param["STOCKID"].ToString();
                if (DaoTool.Save(dao, OptContent.get("SaveEQStock"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("库存主表信息执行失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }
            //库存流水
            if (data.Sql != null && data.Sql.Equals("InsertStockDetail"))
            {
                data.Param["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");

                data.Param["INID"] = InIdHelp;
                data.Param["RECIPECODE"] = RecipeCodeInMain;
                data.Param["SHDH"] = SHDHHelp;
                data.Param["GHSUPPLYID"] = SUPPLYIDHelp;
                data.Param["GHSUPPLYNAME"] = SUPPLYNAMEHelp;

                if (data.Param["STOCKID"] == null || data.Param["STOCKID"].ToString() == "")
                {
                    data.Param["STOCKID"] = StockIdHelp;
                }
                if (DaoTool.Save(dao, OptContent.get("SaveEQStockDetail"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("库存流水信息执行失败！" + dao.ErrMsg);
                }
                //更新至入库细表
                dao.ExecuteNonQuery("UPDATE LKEQ.EQINDETAIL SET STOCKFLOWNO=" + data.Param["FLOWNO"].ToString() + " WHERE CHOSCODE=" + data.Param["CHOSCODE"] + "  AND DETAILNO=" + InDetailNoHelp);
                msg = "执行成功！";
                return "ok";
            }

            //冲销操作=================================================================================
            if (data.Sql != null && data.Sql.Equals("ChongXiaoRuChu"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("ChongXiaoChuKuStatusChange"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改出库主表冲销状态失败！" + dao.ErrMsg);
                }

                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("ChongXiaoRuKuStatusChange"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改入库主表冲销状态失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //插入出库主表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoZhuChuKu"))
            {
                data.Param["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOut");
                OutIdCXHelp = data.Param["OUTID"].ToString();
                if (DaoTool.Save(dao, OptContent.get("ModifyOutMainInfo"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("插入冲销出库主表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //出库细表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoXiChuKu"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                data.Param["OUTID"] = OutIdCXHelp;
                if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("插入冲销出库细表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }


            //入库主表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoZhuRuKu"))
            {
                data.Param["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");
                InIdCXHelp = data.Param["INID"].ToString();
                if (DaoTool.Save(dao, OptContent.get("SaveEQInMainInfoCX"), data) < 0)
                {
                    throw new Exception("插入冲销入库主表失败！" + dao.ErrMsg);
                }

                dao.ExecuteNonQuery("UPDATE LKEQ.EQOUTMAIN SET INID=" + data.Param["INID"].ToString() + "  WHERE CHOSCODE= " + data.Param["CHOSCODE"].ToString() + "  AND OUTID=" + OutIdCXHelp);
                msg = "执行成功！";
                return "ok";
            }

            //入库细表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoXiRuKu"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                data.Param["INID"] = InIdCXHelp;
                if (DaoTool.Save(dao, OptContent.get("SaveEQInDetail"), data) < 0)
                {
                    throw new Exception("插入冲销细表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }



            else
            {
                msg = "系统出错，请与管理人员联系！";
                return "ok";
            }
        }

        #endregion
    }
}
