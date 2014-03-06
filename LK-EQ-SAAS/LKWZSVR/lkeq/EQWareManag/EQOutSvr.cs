using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Data;
using YiTian.db;

namespace LKWZSVR.lkeq.EQWareManag
{
    class EQOutSvr : IEx
    {
        #region IEx 成员
        bool saveRunDetail(YiTian.db.Dao dao, YtService.data.OptData data)
        {
            List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["DanJuMx"].ToString());
            Opt opt2 = OptContent.get("SaveEQOutDetailInfo");
            foreach (Dictionary<string, object> d in mxli)
            {
                //d["DETAILNO"] = DaoTool.Seq(dao, "LKWZ.SEQWZInDetail");
                d["OUTID"] = data.Param["OUTID"];
                d["EQID"] = d["设备ID"];
                d["UNITCODE"] = d["单位编码"];
                d["GG"] = d["规格"];
                d["XH"] = d["型号"];
                d["CD"] = d["产地"];
                d["NUM"] = d["数量"];
                d["PRICE"] = d["单价"];
                d["MONEY"] = d["金额"];
                d["OTHERMONEY"] = d["运杂费"];
                d["TOTALPRICE"] = d["成本单价"];
                d["TOTALMONEY"] = d["成本金额"];
                d["SUPPLYID"] = d["生产厂家ID"];
                d["SUPPLYNAME"] = d["生产厂家"];
                d["STOCKFLOWNO"] = d["库存流水号"];
                d["PRODUCTDATE"] = Convert.ToDateTime(d["生产日期"]);

                d["VALIDDATE"] = Convert.ToDateTime(d["有效期"]);
                d["MEMO"] = d["备注"];
                d["TXM"] = d["条形码"];
                d["CHOSCODE"] = data.Param["CHOSCODE"];

      
                if (!d["流水号"].ToString().Equals(""))
                {
                    d["DETAILNO"] = d["流水号"].ToString();
                    if (DaoTool.Update(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }

                }
                else
                {
                    d["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                    if (DaoTool.Save(dao, opt2, d) < 0)
                    {
                        throw new Exception("保存单据明细失败！");
                    }
                }

            }
            return true;
        }
        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql != null && data.Sql.Equals("UpdateEQStock"))
            {
                if (DaoTool.Update(dao, OptContent.get("SaveEQStockInfo1"), data) > -1)
                {
                    msg = "保存库存信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("保存库存信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("SaveEQStock"))
            {
                data.Param["STOCKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQStock"); ;
                if (DaoTool.Save(dao, OptContent.get("SaveEQStockInfo1"), data) < 0)
                {
                    // msg = flowno+'+'+DETAILNO;
                    throw new Exception("添加物资库存总表信息失败！" + dao.ErrMsg);


                }
                
            }
            if (data.Sql != null && data.Sql.Equals("ChuKuDanEQdelete"))
            {

                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("DeleteEQOutDetailInfo"), data) < 0)
                {
                    throw new Exception("删除出库物资信息失败！");

                }
                if (DaoTool.Update(dao, OptContent.get("SaveEQOutMainInfo"), data) < 0)
                {
                    throw new Exception("保存出库信息失败！" + dao.ErrMsg);
                }
                
                msg = "删除出库物资信息成功！";
                return "ok";
            }
            if (data.Sql != null && data.Sql.Equals("CHUKuDanDelete"))
            {
                data.Param["STATUS"] = 0;
                if (DaoTool.Update(dao, OptContent.get("SaveEQOutMainInfo"), data) > -1)
                {
                    msg = "删除出库信息成功！";
                    return "ok";
                }
                else
                {
                    throw new Exception("删除出库信息失败！" + dao.ErrMsg);
                }
            }
            if (data.Sql != null && data.Sql.Equals("ChuKuDanUpdate"))
            {
                Opt op = OptContent.get("SaveEQOutMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                       // saveRunDetail(dao, data);
                        if (data.Param["STATUS"].ToString() == "2")
                        {
                            msg = "审核信息成功！";
                        }
                        else if (data.Param["STATUS"].ToString() == "1")
                        {
                            msg = "提交审核入库信息成功！";
                        }
                        else if (data.Param["STATUS"].ToString() == "6")
                        {
                            msg = "审核入库信息成功！";
                        }
                        else 
                        {
                            msg = "入库成功！";
                        }
                        
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存入库信息失败！" + dao.ErrMsg);
                    }
                }
            }



            if (data.Sql != null && data.Sql.Equals("CXData"))
            {
                Opt op = OptContent.get("SaveEQOutMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                       
                            msg = "冲销成功！";
                      
                    }
                    else
                    {
                        throw new Exception("保存出库信息失败！" + dao.ErrMsg);
                    }
                }
                Dictionary<string, object> dic1 = new Dictionary<string, object>();
                dic1["OUTID"] = data.Param["OUTID"].ToString();
                dic1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                Opt op1 = OptContent.get("EQOutMainInfo_CX");
               Dictionary<string,object> dic= DaoTool.Get(dao, op1, dic1);

               dic["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOUT");
               dic["STATUS"] = 7;
               dic["TOTALMONEY"] = "-" + dic["TOTALMONEY"].ToString();
              
               Opt op2 = OptContent.get("SaveEQOutMainInfo");

               if (DaoTool.Save(dao, OptContent.get("SaveEQOutMainInfo"),dic) > -1)//生成出库主表
               {
                   Opt op3 = OptContent.get("EQOutDetailInfo_CX");
                  List<Dictionary<string,object>> lst= DaoTool.Find(dao, op3, data);
                  if (lst!=null)
                   {
                       foreach (Dictionary<string, object> dc in lst)
                       {

                           dc["NUM"] = "-" + dc["NUM"].ToString();
                           dc["MONEY"] = "-" + dc["MONEY"].ToString();
                           dc["OTHERMONEY"] = "-" + dc["OTHERMONEY"].ToString();
                           dc["TOTALPRICE"] = "-" + dc["TOTALPRICE"].ToString();
                           dc["TOTALMONEY"] = "-" + dc["TOTALMONEY"].ToString();
                           dc["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOutDetail");
                           dc["OUTID"] = dic["OUTID"];
                           Opt opt4 = OptContent.get("SaveEQOutDetailInfo");
                           if (DaoTool.Save(dao, opt4, dc) < 0)  //生成入库细表
                           {
                               throw new Exception("保存单据明细失败！");
                           }
                           Dictionary<string, object> dic2 = new Dictionary<string, object>();
                           Opt op5 = OptContent.get("EQOut_SearchEQStockNum1");

                       Dictionary<string, object> dic3 = new Dictionary<string, object>();
                          dic3 = DaoTool.Get(dao, op5, dc);

                           dic2["NUM"] = Convert.ToInt32(dic3["NUM"].ToString())-Convert.ToInt32(dc["NUM"].ToString());
                           dic2["STOCKID"] = dic3["STOCKID"].ToString();

                           if (DaoTool.Update(dao, OptContent.get("SaveEQStockInfo1"), dic2) > -1)//修改库存总表
                           {

                               Dictionary<string, object> dic4 = new Dictionary<string, object>();

                               Opt op7 = OptContent.get("EQOut_SearchEQStockOutNum");

                              
                               ObjItem  objitm= DaoTool.ExecuteScalar(dao, op7, dc);

                               dic4["FLOWNO"] = dc["STOCKFLOWNO"];
                               dic4["OUTNUM"] = Convert.ToInt32(objitm.ToString()) + Convert.ToInt32(dc["NUM"].ToString());
                               Opt op6 = OptContent.get("SaveEQStockDetailInfo1");

                               if (DaoTool.Update(dao, op6, dic4) > -1)//修改库存细表
                                   {
                                       
                                   }
                                   else
                                   {
                                       throw new Exception("保存设备库存流水信息失败！" + dao.ErrMsg);
                                   }
                             


                           }
                           else
                           {
                               throw new Exception("保存库存信息失败！" + dao.ErrMsg);
                           }

                       }




                   
                   }

               }
               else
               {
                   throw new Exception("保存入库单据失败！");
               }


               msg = "冲销成功！";
               return msg;

            }



             if (data.Sql != null && data.Sql.Equals("SaveEQStockDetail"))
            {
                Opt op = OptContent.get("SaveEQStockDetailInfo1");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        msg = "保存设备库存流水信息成功！";

                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存设备库存流水信息失败！" + dao.ErrMsg);
                    }
                }
                //else 
                //{
                //    data.Param["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQStockDetail");
                //   string flowno = data.Param["FLOWNO"].ToString();
                //   string DETAILNO = data.Param["DETAILNO"].ToString();
                //    if (DaoTool.Save(dao, op, data) <0 )
                //    {
                //       // msg = flowno+'+'+DETAILNO;
                //       throw new Exception("保存物资库存流水信息失败！" + dao.ErrMsg);
                        
                       
                //    }

                //return msg = flowno + '+' + DETAILNO;

                //}

            }
   
             if (data.Sql != null && data.Sql.Equals("ChuKuDanSave"))
            {
                Opt op = OptContent.get("SaveEQOutMainInfo");
                if (data.Param.ContainsKey(op.Key))
                {
                    if (DaoTool.Update(dao, op, data) > -1)
                    {
                        saveRunDetail(dao,data);
                        
                        msg = "保存入库信息成功！";
                        return "ok";
                    }
                    else
                    {
                        throw new Exception("保存入库信息失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    DataTable dt = dao.Fd(OptContent.get("EQSearchDicEQInOut").Sql, new object[] { data.Param["IOID"].ToString() });
                    data.Param["OPFLAG"] = dt.Rows[0]["OPFLAG"].ToString();
                    data.Param["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOut");

                    string recipe = dt.Rows[0]["RECIPECODE"].ToString();

                    if (Convert.ToDecimal(dt.Rows[0]["RECIPEYEAR"]) == 1)
                    {
                        recipe += DateTime.Now.Year.ToString();
                    }
                    if (Convert.ToDecimal(dt.Rows[0]["RECIPEMONTH"]) == 1)
                    {
                        if (DateTime.Now.Month < 10)
                        {
                            recipe = recipe + "0" + DateTime.Now.Month.ToString();
                        }
                        else
                        {
                            recipe += DateTime.Now.Month.ToString();
                        }

                    }


                    decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("EQGetOutRecipeNo1").Sql, new object[] { recipe + '%' })) + 1;
                    if (recipeno > 0 && recipeno < 10)
                    {
                        recipe = recipe + "0" + recipeno.ToString();
                    }
                    else
                    {
                        recipe = recipe + recipeno.ToString();
                    }

                    data.Param["RECIPECODE"] = recipe;
                    data.Param["INDATE"] = DateTime.Now;
                    data.Param["STATUS"] = 1;

                    if (DaoTool.Save(dao, OptContent.get("SaveEQOutMainInfo"), data) > -1)
                    {
                       
                        saveRunDetail(dao,data);
                       
                    }
                    else
                    {
                        throw new Exception("保存入库单据失败！");
                    }
                }
            }
            msg = "保存成功！";
            return "ok";
        }

        #endregion
    }
}
