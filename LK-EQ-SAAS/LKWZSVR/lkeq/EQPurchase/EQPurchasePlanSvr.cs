using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.lkeq.EQPurchase
{
    class EQPurchasePlanSvr : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("EQPurchasePlanDelete"))//删除主表
            {
                data.Param["STATUS"] = 0;
                if (DaoTool.Update(dao, OptContent.get("SaveEQPlanMainInfo"), data) > -1)
                {
                    msg = "删除采购计划信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除采购计划信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanUpdate_SH"))
            {
                Opt op = OptContent.get("SaveEQPlanMainInfo");

                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        // saveRunDetail(dao, data);
                        //if (data.Param["STATUS"].ToString() == "2")
                        //{
                            
                        //}

                        msg = "审核采购计划信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("审核采购计划信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanUpdata_Submit"))
            {
                Opt op = OptContent.get("SaveEQPlanMainInfo");

                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
               
                        msg = "提交采购计划信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("提交采购计划信息失败！" + dao.ErrMsg);
                    }
                }
            }
            if (data.Sql != null && data.Sql.Equals("PlanDanEQdelete"))
            {
                //删除采购计划细表记录
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DeleteEQPlanDetailInfo"), data) < 0)
                {
                    throw new Exception("删除采购设备信息失败！");

                }
                //更新采购计划主表信息
                if (DaoTool.Update(dao, OptContent.get("SaveEQPlanMainInfo"), data) < 0)
                {
                    throw new Exception("保存采购计划单失败！" + dao.ErrMsg);
                }
                msg = "删除采购设备信息成功！";
                return "ok";

            }
            //if (data.Sql != null && data.Sql.Equals("UpdateWZPlanDan"))
            //{
            //    if (DaoTool.Update(dao, OptContent.get("SaveWZPlanMainInfo"), data) > -1)
            //    {
            //        //savePlanDetail(dao, data);
            //        msg = "保存采购计划信息成功！";
            //        return "ok";
            //    }
            //    else
            //    {
            //        throw new Exception("保存采购计划信息失败！" + dao.ErrMsg);
            //    }
            //}

            if (data.Sql != null && data.Sql.Equals("PlanDanSave"))
            {
                Opt op = OptContent.get("SaveEQPlanMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        savePlanDetail(dao, data);
                        msg = "保存设备计划信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存设备计划信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    data.Param["PLANID"] = DaoTool.Seq(dao, "LKEQ.SEQEQPlan");
                    if (DaoTool.Save(dao, op, data) > -1)
                    {
                        savePlanDetail(dao, data);
                        msg = "添加设备计划成功!";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("添加设备计划失败！" + dao.ErrMsg);
                    }
                }
            }
            msg = "保存成功！";
            return "ok";
        }


        bool savePlanDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
            Opt opt2 = OptContent.get("SaveEQPlanDetailInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                //d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                d["PLANID"] = data.Param["PLANID"];
                d["APPLYID"] = d["请购ID"];
                d["EQID"] = d["设备ID"];
                d["EQNAME"] = d["设备名称"];
                d["GG"] = d["规格"];
                d["XH"] = d["型号"];
                d["XH"] = d["型号"];
                d["COUNTRY"] = d["国别"];
                d["UNITCODE"] = d["单位编码"];
                d["NOWNUM"] = d["当前库存数量"];
                d["NUM"] = d["采购数量"];
                d["PRICE"] = d["采购单价"];
                d["MONEY"] = d["采购金额"];
                d["SCS"] = d["生产商"];
                d["GYS"] = d["供应商"];
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];
                d["STOCKFLOWNO"] = d["对应的库存流水表的流水号"];
                if (!d["行号"].ToString().Equals(""))
                {
                    d["ROWNO"] = d["行号"].ToString();   
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }

                }
                else
                {
                    object rw = dao.Es(OptContent.get("EQGetROWNO").Sql, new object[] { data.Param["PLANID"] });
                    //data.Param["ROWNO"] = Convert.ToDecimal(rw) + 1;
                    d["ROWNO"] = Convert.ToDecimal(rw) + 1;
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("添加单据明细失败！");
                    }
                }

            }
            return true;
        }
        #endregion
    }
}
