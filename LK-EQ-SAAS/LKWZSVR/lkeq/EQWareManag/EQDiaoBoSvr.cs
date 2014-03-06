using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Windows.Forms;
using System.Data;
using YiTian.db;

namespace LKWZSVR.lkeq.WareManag
{
    class EQDiaoBoSvr : IEx
    {
        #region IEx 成员

        bool SaveDiaoBoXiInfo(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["XiBiaoInfo"].ToString());
            Opt opt2 = OptContent.get("ModifyOutXiBiaoInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                d["OUTID"] = data.Param["OUTID"];//出库ID肯定是和主表一样
                d["EQID"] = d["设备ID"];
                d["NUM"] = d["数量"];
                d["UNITCODE"] = d["单位编码"];
                d["PRICE"] = d["单价"];
                d["MONEY"] = d["金额"];
                d["GG"] = d["规格"];
                d["XH"] = d["型号"];
                d["CD"] = d["产地"];
                d["OTHERMONEY"] = d["运杂费"];
                d["TOTALPRICE"] = d["成本单价"];
                d["TOTALMONEY"] = d["成本金额"];
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];
                d["STOCKFLOWNO"] = d["库存流水号"];
                d["SUPPLYID"] = d["生产厂家ID"];
                d["SUPPLYNAME"] = d["库存流水号"];
                if (d["生产日期"].ToString().Equals(""))
                {
                    d["PRODUCTDATE"] = "";
                }
                else
                {
                    d["PRODUCTDATE"] = Convert.ToDateTime(d["生产日期"]);
                }

                if (d["有效期"].ToString().Equals(""))
                {
                    d["VALIDDATE"] = "";
                }
                else
                {
                    d["VALIDDATE"] = Convert.ToDateTime(d["有效期"]);
                }
                // d["PRODUCTDATE"] = d["生产日期"];
                // d["VALIDDATE"] = d["有效期"];
                if (!d["流水号"].ToString().Equals(""))
                {
                    d["DETAILNO"] = d["流水号"].ToString();
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存出库明细失败！");
                    }
                }
                else
                {
                    d["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存出库明细失败！");
                    }
                }
            }
            return true;
        }


        //前半部分  除去顺序码
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

        string StockId = "0";
        string RecipeCodeInMain = "";
        string OutId = "0";
        string StockFlowNo = "0";
        string OutIdHelp;
        string InIdHelp;


        string OutIdCX = "0";
        string InIdCX = "0";
        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {

            #region 前期的一些绑定
            if (data.Sql == null)
            {
                msg = "缺省SQL参数，无法再服务端进行操作！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("BindWareName"))
            {

                msg = "绑定成功";
                return DaoTool.FindDT(dao, OptContent.get("FindWareNameInEQDiaoBo"), data);
            }

            if (data.Sql != null && data.Sql.Equals("BindOutName"))
            {
                msg = "绑定成功";
                return DaoTool.FindDT(dao, OptContent.get("FindOutNameInEQDiaoBo"), data);

            }
            if (data.Sql != null && data.Sql.Equals("BindUnitCode"))
            {

                msg = "绑定成功";
                return DaoTool.FindDT(dao, OptContent.get("FindUnitNameInEQDiaoBo"), data);
            }
            if (data.Sql != null && data.Sql.Equals("BindEQIDInfo"))
            {
                msg = "绑定成功";
                return DaoTool.FindDT(dao, OptContent.get("FindEQIdNameInEQDiaoBo"), data);
            }

            //包含 提交审核与软删除
            if (data.Sql != null && data.Sql == "SubmitShenHeOrDel")
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("submitShenHeInfo"), data) < 0)
                {
                    throw new Exception("更改设备调拨的审核状态信息失败！");
                }
                msg = "执行成功！";
                return "ok";
            }

            #endregion

            //对主表的操作
            if (data.Sql != null && data.Sql.Equals("ModifyZhuOrAdd"))
            {
                //1、对OUTMAIN进行更新操作
                if (data.Param["RECIPECODE"] == null || data.Param["RECIPECODE"].ToString() == "")
                {
                    DataTable dt = DaoTool.FindDT(dao, OptContent.get("GetRecipeCodeInEQOutInfo"), data);
                    if (dt.Rows.Count <= 0 || dt == null)
                    {
                        throw new Exception("无法查找到单据前缀，是否调拨方式数据错误" + dao.ErrMsg);
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
                        throw new Exception("新增设备调拨的主表信息失败！" + dao.ErrMsg);
                    }
                    SaveDiaoBoXiInfo(dao, data);
                    msg = "执行成功！";
                    return "ok";
                }
                if (DaoTool.Update(dao, OptContent.get("ModifyOutMainInfo"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("修改设备调拨的主表信息失败！" + dao.ErrMsg);
                }
                SaveDiaoBoXiInfo(dao, data);
                msg = "执行成功！";
                return "ok";
            }




            // ModifyOrAddXi
            if (data.Sql.Equals("ModifyOrAddXi"))
            {
                //如果出库ID为空的话  肯定是新增操作
                if (data.Param["OUTID"].ToString().Trim() == "" || data.Param["OUTID"] == null)
                {
                    data.Param["OUTID"] = OutIdHelp;
                    data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                    if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                    {
                        throw new Exception("修改设备调拨的出库细表失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    return "ok";
                }
                else
                {
                    if (data.Param["DETAILNO"] == null || data.Param["DETAILNO"].ToString().Trim() == "")
                    {
                        //该行为新增的数据  新增完之后返回
                        data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                        if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                        {
                            msg = "执行失败！";
                            throw new Exception("修改设备调拨的出库细表失败！" + dao.ErrMsg);
                        }
                        msg = "执行成功！";
                        return "ok";
                    }
                    if (DaoTool.Update(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                    {
                        msg = "执行失败！";
                        throw new Exception("修改设备调拨的出库细表失败！" + dao.ErrMsg);
                    }
                    msg = "执行成功！";
                    //能够retrun ok 说明 要么是前面新增的对了，要么是后面的修改正确，不然都是throw出去
                    return "ok";
                }
            }

            if (data.Sql != null && data.Sql.Equals("ShenHeOutEQInfo"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("ShenHeEQOutAndDetailInfo"), data) > 0)
                {
                    msg = "执行成功！";
                    return "ok";
                }
                else
                {
                    msg = "执行失败！";
                    throw new Exception("审核过程中，数据库操作失败！" + dao.ErrMsg);
                }
            }

            if (data.Sql.Equals("SendOutId"))
            {
                OutId = data.Param["OUTID"].ToString();
                msg = "执行成功！";
                return "ok";
            }



            //===================================审核部分===============================

            //入库主表
            if (data.Sql != null && data.Sql.Equals("InsertInMain"))
            {
                data.Param["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");

                InIdHelp = data.Param["INID"].ToString();
                DataTable dt = DaoTool.FindDT(dao, OptContent.get("GetRecipeCodeInEQOutInfo"), data);
                if (dt.Rows.Count <= 0 || dt == null)
                {
                    throw new Exception("无法查找到单据前缀，是否调拨方式数据错误" + dao.ErrMsg);
                }
                DataRow row = dt.Rows[0];
                data.Param["RECIPECODE"] = Recipecode(row["RECIPECODE"].ToString(), Convert.ToInt32(row["RECIPEYEAR"]), Convert.ToInt32(row["RECIPEMONTH"]), Convert.ToInt32(row["RECIPELENGTH"]));
                data.Param.Add("RECIPECODEHelp", data.Param["RECIPECODE"].ToString() + "%");
                data.Param["RECIPECODE"] = data.Param["RECIPECODE"] + DaoTool.ExecuteScalar(dao, OptContent.get("GetrecipeCodeNo"), data).ToInt().ToString("D2");

                //一次审核，只生成一次入库主表的单据号
                RecipeCodeInMain = data.Param["RECIPECODE"].ToString();

                if (DaoTool.Save(dao, OptContent.get("SaveEQInMainInfoCX"), data) < 0)
                {
                    throw new Exception("入库主表信息执行失败" + dao.ErrMsg);
                }
                //记得更新对应出库主表的INID            
                dao.ExecuteNonQuery("UPDATE LKEQ.EQOUTMAIN SET INID=" + data.Param["INID"].ToString() + " WHERE CHOSCODE=" + data.Param["CHOSCODE"] + "  AND OUTID=" + OutId);
                msg = "执行成功！";
                return "ok";
            }


            //库存主表   可能不会执行  【只有在库存主表，不存在该设备的信息】
            if (data.Sql != null && data.Sql.Equals("InsertStockMain"))
            {
                data.Param["STOCKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCK");
                //在每执行一次对库存总表的插入，都会更新一次stockid,否则，就是针对一个stockid的插入流水了
                StockId = data.Param["STOCKID"].ToString();

                if (DaoTool.Save(dao, OptContent.get("SaveEQStock"), data) < 0)
                {
                    throw new Exception("库存主表信息执行失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //库存流水
            if (data.Sql != null && data.Sql.Equals("InsertStockDetail"))
            {
                data.Param["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");
                StockFlowNo = data.Param["FLOWNO"].ToString();
                data.Param["RECIPECODE"] = RecipeCodeInMain;
                if (data.Param["STOCKID"] == null || data.Param["STOCKID"].ToString() == "")
                {
                    data.Param["STOCKID"] = StockId;//在客户端已经经过了判断，要么存在，要么不存在就是库存总表进行了新的插入，直接获取
                }

                if (DaoTool.Save(dao, OptContent.get("SaveEQStockDetail"), data) < 0)
                {
                    throw new Exception("库存流水信息执行失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //入库细表
            if (data.Sql != null && data.Sql.Equals("InsertEQInDetail"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                data.Param["STOCKFLOWNO"] = StockFlowNo.ToString();
                data.Param["INID"] = InIdHelp;
                if (DaoTool.Save(dao, OptContent.get("SaveEQInDetail"), data) < 0)
                {
                    throw new Exception("入库细表信息执行失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }



            //=================================关于冲销的操作==========================

            //原始数据的状态更改
            if (data.Sql != null && data.Sql.Equals("ChongXiaoRuChu"))
            {
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("ChongXiaoChuKuStatusChange"), data) < 0)
                {
                    throw new Exception("修改出库主表冲销状态失败！" + dao.ErrMsg);
                }
                data.Param.Add("INID", dao.Es("SELECT INID FROM LKEQ.EQOUTMAIN WHERE OUTID=" + data.Param["OUTID"].ToString()).ToString());
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("ChongXiaoRuKuStatusChange"), data) < 0)
                {
                    throw new Exception("修改入库主表冲销状态失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //插入出库主表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoZhuChuKu"))
            {
                data.Param["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOut");
                OutIdCX = data.Param["OUTID"].ToString();
                if (DaoTool.Save(dao, OptContent.get("ModifyOutMainInfo"), data) < 0)
                {
                    throw new Exception("插入冲销出库主表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //出库细表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoXiChuKu"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                data.Param["OUTID"] = OutIdCX;
                if (DaoTool.Save(dao, OptContent.get("ModifyOutXiBiaoInfo"), data) < 0)
                {
                    throw new Exception("插入冲销出库细表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            //入库主表
            if (data.Sql != null && data.Sql.Equals("ChongXiaoZhuRuKu"))
            {
                data.Param["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");
                InIdCX = data.Param["INID"].ToString();
                if (DaoTool.Save(dao, OptContent.get("SaveEQInMainInfoCX"), data) < 0)
                {
                    throw new Exception("插入冲销入库主表失败！" + dao.ErrMsg);
                }

                dao.ExecuteNonQuery("UPDATE LKEQ.EQOUTMAIN SET INID=" + data.Param["INID"].ToString() + "  WHERE CHOSCODE= " + data.Param["CHOSCODE"].ToString() + "  AND OUTID=" + OutIdCX);
                msg = "执行成功！";
                return "ok";
            }

            //入库细表

            if (data.Sql != null && data.Sql.Equals("ChongXiaoXiRuKu"))
            {
                data.Param["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                data.Param["INID"] = InIdCX;
                if (DaoTool.Save(dao, OptContent.get("SaveEQInDetail"), data) < 0)
                {
                    throw new Exception("插入冲销细表失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }

            else
            {
                msg = "系统错误，请与管理员进行联系！";
                return "ok";
            }

        }

        #endregion
    }
}
