using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YiTian.db;
using YtService.util;
using YtService.config;
using System.Data;

namespace LKWZSVR.lkeq.EQWareManag
{
    class EQPanDianSvr:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            msg = "盘点信息";
            Dictionary<string, object> pa1 = new Dictionary<string, object>();
            Dictionary<string, object> pa = new Dictionary<string, object>();
            Dictionary<string, object> aa = new Dictionary<string, object>();
            Dictionary<string, object> bb = new Dictionary<string, object>();
            Dictionary<string, object> cc = new Dictionary<string, object>();
            Dictionary<string, object> dd = new Dictionary<string, object>();
            Dictionary<string, object> ee = new Dictionary<string, object>();
            Dictionary<string, object> ff = new Dictionary<string, object>();
            Dictionary<string, object> gg = new Dictionary<string, object>();
            Dictionary<string, object> hh = new Dictionary<string, object>();
            Dictionary<string, object> ii = new Dictionary<string, object>();
            Dictionary<string, object> jj = new Dictionary<string, object>();
             Dictionary<string, object> kk = new Dictionary<string, object>();
             Dictionary<string, object> ll = new Dictionary<string, object>();
             Dictionary<string, object> mm = new Dictionary<string, object>();
             Dictionary<string, object> nn = new Dictionary<string, object>();
            List<Dictionary<string, object>> listout = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> listin = new List<Dictionary<string, object>>();
            
            string ac = data.Sql;
          //  ObjItem Obj;
            if ("SaveEQPanDianInfo".Equals(ac))
            {
               
                pa["PDDATE"] = DateTime.Now;
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["PDNAME"] = data.Param["PDNAME"].ToString();
                pa["STATUS"] = Convert.ToDecimal(data.Param["STATUS"]);
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);


                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }
               
                    pa["PDID"] = DaoTool.Seq(dao, "LKEQ.SEQEQPD");
                    Opt saveInfo = OptContent.get("SaveEQPanDianInfo");
                   
                  
                    if (DaoTool.Save(dao, saveInfo, pa) < 0)
                        throw new Exception("新建盘点主表失败！");



                    //List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(data.Param["MyCount"].ToString());
                //    Opt opt2 = OptContent.get("SaveEQOutDetailInfo");



                 if (data.Param["MyCount"] != null && data.Param["STOCKFLOWNO" + 1] !=null)
                    {
                        Opt saveInfodedetail = OptContent.get("SaveEQPanDianDetailInfo");
                        Opt FindEQStockDetail = OptContent.get("EQPZ_FindEQStockDetailInfo");
                        for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                        {
                            pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                            pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO"+i]);
                            Dictionary<string, object> r = DaoTool.Get(dao, FindEQStockDetail, pa1);
                            r.Add("ROWNO", i);
                            r["PDID"] = pa["PDID"];
                            r["FACTNUM"] = Convert.ToDecimal(data.Param["FACTNUM" + i]);
                           r["YKNUM"] = Convert.ToDecimal(data.Param["YKNUM" + i]);

                           r["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            
                            r["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                          

                            if (DaoTool.Save(dao, saveInfodedetail, r) < 0)
                                throw new Exception("添加盘点细表" + r["EQNAME"] + "信息失败！");
                        }
                    }






                    //Opt saveInfodedetail = OptContent.get("SaveWZPanDianDetailInfo");
                    //int i = 1;
                    //foreach (Dictionary<string, object> d in mxli)
                    //{
                    //    d["PDID"] = pa["PDID"];
                    //    d["ROWNO"] = i;
                    //    d["STOCKFLOWNO"] = d["流水号"];
                    //    d["STOCKID"] = d["库存ID"];
                    //    d["STOCKNUM"] = d["库存数量"];
                    //    d["FACTNUM"] = d["实际数量"];
                    //    d["YKNUM"] = d["盈亏数量"];
                    //    d["UNITCODE"] = d["单位编码"];
                    //    d["PRICE"] = d["单价"];
                    //    d["GG"] = d["规格"];
                    //    d["XH"] = d["型号"];
                    //    d["CD"] = d["产地"];

                    //    d["SUPPLYID"] = d["生产厂家ID"];
                    //    d["SUPPLYNAME"] = d["生产厂家名称"];
                    //    d["PRODUCTDATE"] = d["生产日期"];

                    //    d["VALIDDATE"] = d["有效期"];
                    //    d["INDATE"] = d["入库时间"];
                    //    d["RECIPECODE"] = d["入库单据号"];
                    //    d["SHDH"] = d["随货单号"];
                    //    d["GHSUPPLYID"] = d["供货商ID"];
                    //    d["GHSUPPLYNAME"] = d["供货商名称"];
                    //    d["MEMO"] = d["备注"];
                    //    d["TXM"] = d["条形码"];
                    //    d["CHOSCODE"] = d["医疗机构编码"];


                    //      if (DaoTool.Save(dao, saveInfodedetail, d) < 0)
                    //        {
                    //            throw new Exception("保存单据明细失败！");
                    //        }
                        
                    //    i++;
                    //}
                         


                    //Opt wyInfo = OptContent.get("WYWZPanDianDetailInfo");
                    //if (!DaoTool.ExecuteScalar(dao, wyInfo, pa).IsNull)
                    //{
                    //    msg = "表中已经有该物资的盘点数据，不能再新增，请在表中修改！";
                    //    return "ok";
                    //}


                    msg = "添加盘点信息成功！|" + pa["PDID"];
                    return "ok";
                }
    

            if ("UpdataEQPanDianInfo".Equals(ac))
            {
                pa["PDNAME"] = data.Param["PDNAME"].ToString();
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["WARECODE"] = data.Param["WARECODE"].ToString();
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["USERNAME"] != null)
                {
                    pa["USERNAME"] = data.Param["USERNAME"].ToString();
                }
                else
                {
                    pa["USERNAME"] = null;
                }


                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt updataInfo = OptContent.get("UpdataEQPanDianInfo");



                if (DaoTool.ExecuteNonQuery(dao, updataInfo, pa) < 0)
                    throw new Exception("修改盘点信息失败！");
 

                //pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                if (data.Param["MyCount"] != null)
                {
                    //Opt ifsaveorupdata = OptContent.get("IfSaveOrUpdataWZPanDianDetailInfo");

                    Opt saveInfodedetail = OptContent.get("SaveEQPanDianDetailInfo");
                    Opt FindEQStockDetail = OptContent.get("EQPZ_FindEQStockDetailInfo");

                    for (int i = 1; i < Convert.ToInt32(data.Param["MyCount"]); i++)
                    {
                        pa1["EQID"] = Convert.ToDecimal(data.Param["EQID" + i]);
                        pa1["STOCKFLOWNO"] = Convert.ToDecimal(data.Param["STOCKFLOWNO" + i]);
                        pa1["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                        pa1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        Dictionary<string, object> r = DaoTool.Get(dao, FindEQStockDetail, pa1);

                     

                        //  ObjItem num = DaoTool.ExecuteScalar(dao, ifsaveorupdata, pa1);
                        
                        //if (!num.IsNull)
                        //{
                           
                            r["STOCKNUM"] = Convert.ToDecimal(r["NUM"]);
                            r["FACTNUM"] = Convert.ToDecimal(data.Param["FACTNUM" + i]);
                            r["YKNUM"] = Convert.ToDecimal(data.Param["YKNUM" + i]);
                           r["CHOSCODE"] = r["CHOSCODE"].ToString();
                           r["STOCKFLOWNO"] = Convert.ToDecimal(r["FLOWNO"]);
                            r["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                            Opt updatadetailInfo = OptContent.get("UpdataeEQPanDicanDetailInfo");
                            if (DaoTool.ExecuteNonQuery(dao, updatadetailInfo, pa) < 0)
                                throw new Exception("修改盘点细表" + r["EQNAME"] + "信息失败！！");
                        //}
                       

                    }




                    msg = "修改成功！|" +pa["PDID"];
                    return "ok";

                }
            }

            if ("SubmitEQPD".Equals(ac))
            {
              pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["STATUS"] =1;
                Opt delInfo = OptContent.get("SubmitEQPanDianInfo");

                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("提交盘点信息失败！");
                msg = "提交成功！";
                return "ok";
              
            }
            if ("DelEQPanDianInfo".Equals(ac))
            {
              pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt delInfo = OptContent.get("DelEQPanDianInfo");

                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除盘点信息失败！");
                msg = "删除成功！";
                return "ok";
              
            }
            if ("DelEQPanDianDetailInfo".Equals(ac))
            {


                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();


                pa["ROWNO"] = Convert.ToDecimal(data.Param["ROWNO"]);

                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                Opt delInfo = OptContent.get("DelEQPanDianDetailInfo");



                if (DaoTool.ExecuteNonQuery(dao, delInfo, pa) < 0)
                    throw new Exception("删除盘点详细信息失败！");
                msg = "删除成功！";
                return "ok";

            }

            if ("ShenHeLost".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();


                Opt saveInfo = OptContent.get("ShenHeLostEQPanDianInfo");        
                    if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                        throw new Exception("审核失败！");
                    msg = "审核成功！";
                    return "ok";
                }
          
            if ("ShenHeSucPD".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                pa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                pa["SHDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("ShenHeSucEQPanDianInfo");        
                    if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                        throw new Exception("审核失败！");

                Opt getpdin = OptContent.get("GetPDIDEQPanDianInfoStockIn");                
                 listin = DaoTool.Find(dao, getpdin, pa);
                if(listin!=null && listin.Count>0)
                {
                     //生成入库主表
                    aa["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");
                    Opt getioid = OptContent.get("GetIOIDEQPanDianInfoStockIn");
                    ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid,pa);
                    aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                    //单据号
                    DataTable dt = dao.Fd(OptContent.get("EQSearchDicEQInOut").Sql, new object[] { aa["IOID"].ToString() });
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


                  decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("EQGetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                  if (recipeno > 0 && recipeno < 10)
                  {
                      recipe = recipe + "0" + recipeno.ToString();
                  }
                  else
                  {
                      recipe = recipe + recipeno.ToString();
                  }

                  //aa["DEPTID"] = null;
                  aa["RECIPECODE"] = recipe;
                  aa["OPFLAG"] = 3;
                  aa["MEMO"] = data.Param["MEMO"].ToString();
                  aa["RECDATE"] =DateTime.Now;
                  aa["STATUS"] = 6;
                  aa["WARECODE"] = data.Param["WARECODE"].ToString();
                  aa["INDATE"] = DateTime.Now;
                  aa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                  aa["USERNAME"] = data.Param["USERNAME"].ToString();
                  aa["SHDATE"] = DateTime.Now;
                  aa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                  aa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                  aa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
               
                //得到总金额和零售总金额
                decimal total = 0;
                decimal lstotal = 0;
                decimal yzf_money = 0;
                     
                foreach (var indata in listin)
                {
                   
                 Dictionary<string,object> GetYzf=DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"),indata);


                 if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                 {
                     decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                     decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                     decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                     decimal d1 = 0;
                     d1 = (a1 / (b1 - c1)) * Convert.ToDecimal(indata["YKNUM"]);
                     bb["OTHERMONEY"] = Math.Round(d1, 2);
                     bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(indata["PRICE"]) + a1 / (b1 - c1), 4);
                     bb["TOTALMONEY"] = Math.Round(Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(indata["YKNUM"]), 2);
                 }
                 else
                 {
                     bb["OTHERMONEY"] = null;
                     bb["TOTALPRICE"] = null;
                     bb["TOTALMONEY"] = null;
                 }
                 total += Convert.ToDecimal(bb["TOTALMONEY"]);

                 lstotal += Convert.ToDecimal(bb["TOTALPRICE"]);
                 yzf_money += Convert.ToDecimal(bb["OTHERMONEY"]);
                    //if (indata["TOTALMONEY"] == null || indata["TOTALMONEY"].ToString() == "")//保证有价格
                    //{
                    //    total = 0;
                    //}
                    //else
                    //{
                    //    total += Convert.ToDecimal(indata["TOTALMONEY"]);
                    //}

                    //if (indata["MONEY"] == null || indata["MONEY"].ToString() == "")
                    //{
                    //    lstotal = 0;
                    //}
                    //else
                    //{
                    //    lstotal += Convert.ToDecimal(indata["INVOICEMONEY"]);
                    //}
                    //if (indata["OTHERMONEY"] == null || indata["OTHERMONEY"].ToString() == "")
                    //{
                    //    lstotal = 0;
                    //}
                    //else
                    //{
                    //    yzf_money += Convert.ToDecimal(indata["OTHERMONEY"]);
                    //}

                }
                aa["TOTALMONEY"] = total;
                aa["LSTOTALMONEY"] = lstotal;
                aa["OTHERMONEY"] = yzf_money;

                Opt savewzinmain = OptContent.get("SaveEQInMain_EQPanDianIn");              
                if (DaoTool.Save(dao, savewzinmain, aa) < 0)
                    throw new Exception("生成入库主表失败！");



                //向盘点主表中加入入库单序号

                kk["PDID"] = Convert.ToDecimal( Convert.ToDecimal(data.Param["PDID"]));
                kk["INID"] = aa["INID"];
                kk["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                Opt pdwz_updataed1 = OptContent.get("UpdataInEQPanDianInfo_In");
                if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed1, kk) < 0)
                    throw new Exception("向盘点主表添加入库单序号失败！");

                //生成入库细表

                bb["INID"] = aa["INID"];
                foreach (Dictionary<string, object> indata in listin)
                {
                   
                    bb["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                    bb["EQID"] = Convert.ToDecimal(indata["EQID"]);
                
                    bb["UNITCODE"] = indata["UNITCODE"];//盘盈数据里的最小编码？无编码

                    bb["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                    if(indata["PRICE"]!=null&&indata["PRICE"].ToString().Trim()!="")
                    {
                    bb["PRICE"] = Math.Round( Convert.ToDecimal(indata["PRICE"]),2);
                    bb["MONEY"] =Math.Round( Convert.ToDecimal(indata["PRICE"]) * Convert.ToDecimal(indata["YKNUM"]),2);
                    }
                  Dictionary<string,object> GetYzf=DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"), indata);

                  if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                  {
                      decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                      decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                      decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                      decimal d1 = 0;
                      if (b1 - c1 == 0)
                      {
                          d1 = 0;
                          bb["TOTALPRICE"] = 0;
                      }
                      else
                      {
                          d1 = (a1 / (b1 - c1)) * Convert.ToDecimal(indata["YKNUM"]);
                          bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(indata["PRICE"]) + a1 / (b1 - c1), 4);
                      }
                      bb["OTHERMONEY"] = Math.Round(d1, 2);
                     
                      bb["TOTALMONEY"] = Math.Round( Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(indata["YKNUM"]),2);
                  }
                  else
                  {
                       bb["OTHERMONEY"] = 0;
                       bb["TOTALPRICE"]=0;
                       bb["TOTALMONEY"] = 0;
                  }

                    bb["PRODUCTDATE"] = Convert.ToDateTime(indata["PRODUCTDATE"]);
                    bb["CHOSCODE"] = indata["CHOSCODE"].ToString();
                    bb["STOCKFLOWNO"] = indata["STOCKFLOWNO"];  
                    bb["GG"] = indata["GG"];
                    bb["XH"] = indata["XH"];
                    bb["CD"] = indata["CD"];
                    bb["SUPPLYID"] = indata["SUPPLYID"];
                    bb["SUPPLYNAME"] = indata["SUPPLYNAME"];
                    bb["VALIDDATE"] = indata["VALIDDATE"];
                   
                    bb["MEMO"] = indata["MEMO"];
                    bb["TXM"] = indata["TXM"];

                    Opt saveeqindetail = OptContent.get("SaveEQInDetailEQPanDianIn");
                    if (DaoTool.Save(dao, saveeqindetail, bb) < 0)
                        throw new Exception("生成入库细表失败！");

                    //向盘点细表中加入入库流水号
                    ii["ROWNO"] = Convert.ToDecimal(indata["ROWNO"]);
                    ii["PDID"] = Convert.ToDecimal(indata["PDID"]);
                    ii["DETAILNO"] = bb["DETAILNO"];
                    ii["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    Opt pdwz_updatadetail = OptContent.get("UpdataInDetailEQPanDianInfo_In");
                    if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail, ii) < 0)
                        throw new Exception("向盘点细表添加入库流水号失败！");

                    //生成库存总表

                    bb["WARECODE"] = aa["WARECODE"];
                    Opt stocknum = OptContent.get("GetNumInStock_EQPanDianIn");//获得库存数量
                    ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);
                    

                    cc["WARECODE"] = aa["WARECODE"].ToString();
                    cc["EQID"] = Convert.ToDecimal(indata["EQID"]);
                    cc["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    cc["DEPTID"] = null;
                    cc["UNITCODE"] = indata["UNITCODE"];
                    Opt ifexist = OptContent.get("IfExistInStockEQPanDianIn");
                    ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                    if (objtem2 != null)
                    {
                        //Opt iflsunitcode = OptContent.get("iflsunitcodeInStockWZPanDianIn");//是否为最小单位编码
                        //ObjItem objtem3 = DaoTool.ExecuteScalar(dao, iflsunitcode, aa);
                           cc["STOCKID"] = objtem2.ToInt();
                            cc["NUM"] = objtem4.ToInt() + Convert.ToInt32(indata["YKNUM"].ToString());

                        dd["BEFORENUM"] = objtem4.ToInt();
                        Opt updatastocktable = OptContent.get("UpdataInStockNum_EQPanDianIn");
                        if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                            throw new Exception("生成库存总表失败!");
                    }
                    else
                    {
                        cc["STOCKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCK");
                        cc["NUM"] = Convert.ToInt32(indata["YKNUM"].ToString());
                        dd["BEFORENUM"] = 0;
                        Opt stocktable = OptContent.get("SaveInStockEQPanDianIn");
                        if (DaoTool.Save(dao, stocktable, cc) < 0)
                            throw new Exception("生成库存总表失败！");

                    }

                
                   
                    //生成库存细表
                    dd["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");
                    dd["INID"] = Convert.ToDecimal(aa["INID"].ToString());
                    dd["WARECODE"] = aa["WARECODE"].ToString();
                    dd["EQID"] = Convert.ToDecimal(indata["EQID"]);
                    dd["STOCKID"] = Convert.ToDecimal(indata["STOCKID"]);
                    dd["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                    dd["UNITCODE"] = indata["UNITCODE"];
                    dd["OUTNUM"] = 0;
                    dd["CARDNUM"] = 0;
                    dd["PRICE"] = bb["PRICE"];
                    dd["MONEY"] = bb["MONEY"];
                    dd["OTHERMONEY"] = bb["OTHERMONEY"];
                    dd["TOTALPRICE"] = bb["TOTALPRICE"];
                    dd["TOTALMONEY"] = bb["TOTALMONEY"];
                    dd["SUPPLYID"] = bb["SUPPLYID"];
                    dd["SUPPLYNAME"] = bb["SUPPLYNAME"];
                    dd["PRODUCTDATE"] = bb["PRODUCTDATE"];
                    dd["VALIDDATE"] = bb["VALIDDATE"];
                    dd["MEMO"] = bb["MEMO"];
                    dd["TXM"] = bb["TXM"];
                    dd["RECIPECODE"] = indata["RECIPECODE"];
                    dd["SHDH"] = indata["SHDH"];
                    dd["GHSUPPLYID"] = indata["GHSUPPLYID"];
                    dd["INDATE"] = DateTime.Now;
                    dd["GHSUPPLYNAME"] = indata["GHSUPPLYNAME"];
                    dd["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                    Opt stockdetailtable = OptContent.get("SaveInStockDetailEQPanDianIn");
                    if (DaoTool.Save(dao, stockdetailtable, dd) < 0)
                        throw new Exception("生成库存细表失败！");


                    //向入库细表中加入对应的库存流水表的流水号
                   
                    mm["EQID"] = Convert.ToDecimal(indata["EQID"]);
                    mm["DETAILNO"] = bb["DETAILNO"];
                    mm["STOCKFLOWNO"] = dd["FLOWNO"];
                    mm["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    Opt EQPD_Stockflow = OptContent.get("EQPD_updataIndetailStockflow");
                    if (DaoTool.ExecuteNonQuery(dao, EQPD_Stockflow, mm) < 0)
                        throw new Exception("向入库细表添加库存流水号失败！");

                 }
  
                }





                Opt getpdout = OptContent.get("GetPDIDEQPanDianInfoOut");
                listout = DaoTool.Find(dao, getpdout, pa);
                if (listout!=null && listout.Count > 0)
                {
                  
                    //生成出库主表
                    aa["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOUT");

                    Opt getioid = OptContent.get("GetIOIDEQPanDianInfoStockOut");
                    ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid, pa);
                    aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                    //单据号
                    DataTable dt = dao.Fd(OptContent.get("EQSearchDicEQInOut").Sql, new object[] { aa["IOID"].ToString() });
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


                    decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("GetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                    if (recipeno > 0 && recipeno < 10)
                    {
                        recipe = recipe + "0" + recipeno.ToString();
                    }
                    else
                    {
                        recipe = recipe + recipeno.ToString();
                    }

                    aa["RECIPECODE"] = recipe;
                    aa["OPFLAG"] = 3;
                    aa["MEMO"] = data.Param["MEMO"].ToString();
                    aa["RECDATE"] = DateTime.Now;
                    aa["STATUS"] = 6;
                    aa["WARECODE"] =data.Param["WARECODE"].ToString();
                    aa["OUTDATE"] = DateTime.Now;
                    aa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                    aa["USERNAME"] = data.Param["USERNAME"].ToString();
                    aa["SHDATE"] = DateTime.Now;
                    aa["SHUSERID"] = Convert.ToDecimal(data.Param["SHUSERID"]);
                    aa["SHUSERNAME"] = data.Param["SHUSERNAME"].ToString();
                    aa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();

                    //得到总金额和零售总金额
                    decimal total = 0;
                  
                    foreach (var outdata in listout)
                    {
                        if (outdata["PRICE"] == null || outdata["PRICE"].ToString() == "")//保证有价格
                        {
                            total = 0;
                        }
                        else
                        {
                            total += Convert.ToDecimal(outdata["PRICE"]) * Math.Abs(Convert.ToDecimal(outdata["YKNUM"]));
                        }
                    }
                    aa["TOTALMONEY"] = total;

                    Opt savewzoutmain = OptContent.get("SaveEQOutMainEQPanDianOut");
                    if (DaoTool.Save(dao, savewzoutmain, aa) < 0)
                        throw new Exception("生成出库主表失败！");



                    //向盘点主表中加入出库单序号

                    ll["PDID"] = Convert.ToDecimal(Convert.ToDecimal(data.Param["PDID"]));
                    ll["OUTID"] = aa["OUTID"];
                    ll["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    Opt pdwz_updataed2 = OptContent.get("UpdataInEQPanDianInfo_Out");
                    if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed2, ll) < 0)
                        throw new Exception("向盘点主表添加出库单序号失败！");




                    //生成出库细表

                    bb["OUTID"] = aa["OUTID"];
                    data.Param.Add("EQID", null); //不知这两句话什么意思？
                    data.Param.Add("STOCKID",null);
                    foreach (Dictionary<string,object> outdata in listout)
                    {
                        bb["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOUTDETAIL");
                        bb["EQID"] = Convert.ToDecimal(outdata["EQID"]);
                       
                        bb["UNITCODE"] = outdata["UNITCODE"];//盘盈数据里的最小编码？无编码

                        bb["NUM"] = Convert.ToDecimal(outdata["YKNUM"]) * (-1);
                        bb["PRICE"] =Math.Round( Convert.ToDecimal(outdata["PRICE"]),4);
                        bb["MONEY"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]) * Convert.ToDecimal(bb["NUM"]), 4);
                        Dictionary<string, object> GetYzf = DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"), outdata);

                        if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                        {
                            decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                            decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                            decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                            decimal d1 = 0;
                            if (b1 - c1 == 0)
                            {
                                d1 = 0;
                                bb["TOTALPRICE"] = 0;
                            }
                            else

                            {
                                d1 = -(a1 / (b1 - c1)) * Convert.ToDecimal(outdata["YKNUM"]);
                                bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]) + (-a1 / (b1 - c1)), 4);
                            }
                            bb["OTHERMONEY"] = Math.Round(d1, 2);
                          
                            bb["TOTALMONEY"] = Math.Round(Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(outdata["YKNUM"]), 2);
                        }
                        else
                        {
                            
                            bb["OTHERMONEY"] = 0;
                            bb["TOTALPRICE"] = 0;
                            bb["TOTALMONEY"] = 0;
                        }
                        bb["PRODUCTDATE"] = Convert.ToDateTime(outdata["PRODUCTDATE"]);
                        bb["CHOSCODE"] = outdata["CHOSCODE"].ToString();
                        bb["STOCKFLOWNO"] = outdata["STOCKFLOWNO"];
                        bb["GG"] = outdata["GG"];
                        bb["XH"] = outdata["XH"];
                        bb["CD"] = outdata["CD"];
                        bb["SUPPLYID"] = outdata["SUPPLYID"];
                        bb["SUPPLYNAME"] = outdata["SUPPLYNAME"];
                        bb["VALIDDATE"] = outdata["VALIDDATE"];
                        bb["MEMO"] = outdata["MEMO"];
                        bb["TXM"] = outdata["TXM"];


                        Opt saveeqoutdetail = OptContent.get("SaveEQOutDetailEQPanDianOut");
                        if (DaoTool.Save(dao, saveeqoutdetail, bb) < 0)
                            throw new Exception("生成出库细表失败！");

                        //向盘点细表中加入出库流水号
                        jj["ROWNO"] = Convert.ToDecimal(outdata["ROWNO"]);
                        jj["PDID"] = Convert.ToDecimal(outdata["PDID"]);
                        jj["DETAILNO"] = bb["DETAILNO"];
                        jj["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                        Opt pdwz_updatadetail2 = OptContent.get("UpdataInDetailDEQPanDianInfo_Out");
                        if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail2, jj) < 0)
                            throw new Exception("向盘点细表添加出库流水号失败！");




                        //生成库存总表
                        bb["WARECODE"] =data.Param["WARECODE"].ToString();
                        Opt stocknum = OptContent.get("GetNumInStock_EQPanDianIn");//获得库存数量
                        ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);


                        Opt ifexist = OptContent.get("IfExistInStockEQPanDianIn");
                    ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                        cc["STOCKID"] = objtem2.ToInt();
                        cc["WARECODE"] = aa["WARECODE"];
                        cc["EQID"] = Convert.ToDecimal(outdata["EQID"]);
                        cc["CHOSCODE"] = aa["CHOSCODE"];
                        cc["DEPTID"] = null;
                        cc["UNITCODE"] = outdata["UNITCODE"];
                            
                       cc["NUM"] = objtem4.ToInt() - Convert.ToInt32(outdata["YKNUM"].ToString()) * (-1);



                       Opt updatastocktable = OptContent.get("UpdataInStockNum_EQPanDianIn");
                            if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                                throw new Exception("生成库存总表失败！");

                        //生成库存细表

                            data.Param["EQID"] = Convert.ToDecimal(outdata["EQID"]);
                        data.Param["STOCKID"]= objtem2.ToInt();
                        data.Param["STOCKFLOWNO"] = Convert.ToDecimal(outdata["STOCKFLOWNO"]);

                        Opt stockdetailinfo = OptContent.get("StockDetailInfoEQPanDianOut");//获得库存细表信息
                        DataTable datatable = DaoTool.FindDT(dao, stockdetailinfo, data);
                        
                        DataRow dr = datatable.Rows[0];
          
                        dd["OUTNUM"] =Convert.ToInt32(dr["OUTNUM"]) + Convert.ToInt32(outdata["YKNUM"].ToString())*(-1);
                             


                        dd["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");

                        dd["INID"] =dr["INID"];
                        dd["WARECODE"] = dr["WARECODE"];
                        dd["DEPTID"] = dr["DEPTID"];
                        dd["EQID"] = dr["EQID"];
                        dd["STOCKID"] = dr["STOCKID"];
                        dd["NUM"] = dr["NUM"];
                        dd["BEFORENUM"] = dr["BEFORENUM"];
                        dd["UNITCODE"] = dr["UNITCODE"];
                        dd["OUTNUM"] = dr["OUTNUM"];
                        dd["CARDNUM"] = dr["CARDNUM"];
                        dd["GG"] = dr["GG"];
                        dd["XH"] = dr["XH"];
                        dd["CD"] = dr["CD"];
                        dd["PRICE"] = dr["PRICE"];
                        dd["MONEY"] = dr["MONEY"];
                        dd["OTHERMONEY"] = dr["OTHERMONEY"];
                        dd["TOTALPRICE"] = dr["TOTALPRICE"];
                        dd["TOTALMONEY"] = dr["TOTALMONEY"];
                        dd["SUPPLYID"] = dr["SUPPLYID"];
                        dd["SUPPLYNAME"] = dr["SUPPLYNAME"];
                        dd["PRODUCTDATE"] = dr["PRODUCTDATE"];
                        dd["VALIDDATE"] = dr["VALIDDATE"];
                      
                        dd["MEMO"] = dr["MEMO"];
                        dd["TXM"] = dr["TXM"];
                        dd["RECIPECODE"] = dr["RECIPECODE"];
                        dd["SHDH"] = dr["SHDH"];
                        dd["GHSUPPLYID"] = dr["GHSUPPLYID"];
                        dd["GHSUPPLYNAME"] = dr["GHSUPPLYNAME"];
                        dd["CHOSCODE"] = dr["CHOSCODE"];
                        dd["INDATE"] = dr["INDATE"];
                        Opt stockoutdetailtable = OptContent.get("SaveInStockDetailEQPanDianIn");
                        if (DaoTool.Save(dao, stockoutdetailtable, dd) < 0)
                            throw new Exception("生成库存细表失败！");
                    }

                }
        
               msg = "审核成功！";
                    return "ok";
                }
        
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ("EQCXPD".Equals(ac))
            {
                pa["PDID"] = Convert.ToDecimal(data.Param["PDID"]);
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["STATUS"] = data.Param["STATUS"].ToString();
                pa["CXUSERID"] = Convert.ToDecimal(data.Param["CXUSERID"]);
                pa["CXUSERNAME"] = data.Param["CXUSERNAME"].ToString();
                pa["CXDATE"] = DateTime.Now;

                Opt saveInfo = OptContent.get("ChongXiaoSucEQPanDianInfo");
                if (DaoTool.ExecuteNonQuery(dao, saveInfo, pa) < 0)
                    throw new Exception("冲销失败！");


                Dictionary<string, object> dic1 = new Dictionary<string, object>();
                dic1["PDID"] = data.Param["PDID"].ToString();
                dic1["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                Opt op1 = OptContent.get("EQPDMainInfo_CX");
                Dictionary<string, object> dic = DaoTool.Get(dao, op1, dic1);

                dic["PDID"] = DaoTool.Seq(dao, "LKEQ.SEQEQPD");
                dic["STATUS"] = 7;

                if (DaoTool.Save(dao, OptContent.get("SaveEQPanDianInfo_CX"), dic) > -1)//生成冲销数据
                {
                    Opt op3 = OptContent.get("EQPDDetailInfo_CX");
                    List<Dictionary<string, object>> lst = DaoTool.Find(dao, op3, data);
                    if (lst != null)
                    {
                        foreach (Dictionary<string, object> dc in lst)
                        {

                            dc["YKNUM"] =  0- decimal.Parse(dc["YKNUM"].ToString());
                            dc["FACTNUM"] = decimal.Parse(dc["STOCKNUM"].ToString()) + decimal.Parse(dc["YKNUM"].ToString());
                            dc["PDID"]=dic["PDID"];
                            Opt opt4 = OptContent.get("SaveEQPanDianDetailInfo");
                            if (DaoTool.Save(dao, opt4, dc) < 0)     //生成盘点细表
                                throw new Exception("生成盘点细表失败！");
                        }
                    }
                }

                    Opt getpdin = OptContent.get("GetPDIDEQPanDianInfoStockIn");
                    listin = DaoTool.Find(dao, getpdin, dic);
                    if (listin != null && listin.Count > 0)
                    {
                        //生成入库主表
                        aa["INID"] = DaoTool.Seq(dao, "LKEQ.SEQEQIN");
                        Opt getioid = OptContent.get("GetIOIDEQPanDianInfoStockIn");
                        ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid, dic);
                        aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                        //单据号
                        DataTable dt = dao.Fd(OptContent.get("EQSearchDicEQInOut").Sql, new object[] { aa["IOID"].ToString() });
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


                        decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("EQGetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                        if (recipeno > 0 && recipeno < 10)
                        {
                            recipe = recipe + "0" + recipeno.ToString();
                        }
                        else
                        {
                            recipe = recipe + recipeno.ToString();
                        }

                        //aa["DEPTID"] = null;
                        aa["RECIPECODE"] = recipe;
                        aa["OPFLAG"] = 3;
                        aa["MEMO"] = dic["MEMO"];
                        aa["RECDATE"] = DateTime.Now;
                        aa["STATUS"] = 6;
                        aa["WARECODE"] = dic["WARECODE"];
                        aa["INDATE"] = DateTime.Now;
                        aa["USERID"] = Convert.ToDecimal(dic["USERID"]);
                        aa["USERNAME"] = dic["USERNAME"];
                        aa["SHDATE"] = DateTime.Now;
                        aa["SHUSERID"] =dic["SHUSERID"];
                        aa["SHUSERNAME"] = dic["SHUSERNAME"];
                        aa["CHOSCODE"] = dic["CHOSCODE"];

                        //得到总金额和零售总金额
                        decimal total = 0;
                        decimal lstotal = 0;
                        decimal yzf_money = 0;
                        foreach (var indata in listin)
                        {
                            Dictionary<string, object> GetYzf = DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"), indata);


                            if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                            {
                                decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                                decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                                decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                                decimal d1 = 0;
                                if (b1 - c1 == 0)
                                {
                                    d1 = 0;
                                    bb["TOTALPRICE"] = 0;
                                }
                                else
                                {
                                    d1 = (a1 / (b1 - c1)) * Convert.ToDecimal(indata["YKNUM"]);
                                    bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(indata["PRICE"]) + a1 / (b1 - c1), 4);
                                }
                                bb["OTHERMONEY"] = Math.Round(d1, 2);

                                bb["TOTALMONEY"] = Math.Round(Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(indata["YKNUM"]), 2);
                            }
                            else
                            {
                                bb["OTHERMONEY"] = 0;
                                bb["TOTALPRICE"] = 0;
                                bb["TOTALMONEY"] = 0;
                            }
                            total += Convert.ToDecimal(bb["TOTALMONEY"]);

                            lstotal += Convert.ToDecimal(bb["TOTALPRICE"]);
                            yzf_money += Convert.ToDecimal(bb["OTHERMONEY"]);

                        }
                        aa["TOTALMONEY"] = total;
                        aa["LSTOTALMONEY"] = lstotal;
                        aa["OTHERMONEY"] = yzf_money;

                        Opt savewzinmain = OptContent.get("SaveEQInMain_EQPanDianIn");
                        if (DaoTool.Save(dao, savewzinmain, aa) < 0)
                            throw new Exception("生成入库主表失败！");



                        //向盘点主表中加入入库单序号

                        kk["PDID"] = Convert.ToDecimal(Convert.ToDecimal(dic["PDID"]));
                        kk["INID"] = aa["INID"];
                        kk["CHOSCODE"] = dic["CHOSCODE"];
                        Opt pdwz_updataed1 = OptContent.get("UpdataInEQPanDianInfo_In");
                        if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed1, kk) < 0)
                            throw new Exception("向盘点主表添加入库单序号失败！");

                        //生成入库细表

                        bb["INID"] = aa["INID"];
                        foreach (Dictionary<string, object> indata in listin)
                        {

                            bb["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQINDETAIL");
                            bb["EQID"] = Convert.ToDecimal(indata["EQID"]);

                            bb["UNITCODE"] = indata["UNITCODE"];//盘盈数据里的最小编码？无编码

                            bb["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                            if (indata["PRICE"] != null && indata["PRICE"].ToString().Trim() != "")
                            {
                                bb["PRICE"] = Math.Round(Convert.ToDecimal(indata["PRICE"]), 2);
                                bb["MONEY"] = Math.Round(Convert.ToDecimal(indata["PRICE"]) * Convert.ToDecimal(indata["YKNUM"]), 2);
                            }
                            Dictionary<string, object> GetYzf = DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"), indata);

                            if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                            {
                                decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                                decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                                decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                                decimal d1 = 0;
                                if (b1 - c1 == 0)
                                {
                                    d1 = 0;
                                    bb["TOTALPRICE"] = 0;
                                }
                                else
                                {
                                    d1 = (a1 / (b1 - c1)) * Convert.ToDecimal(indata["YKNUM"]);
                                    bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(indata["PRICE"]) + a1 / (b1 - c1), 4);
                                }
                                bb["OTHERMONEY"] = Math.Round(d1, 2);

                                bb["TOTALMONEY"] = Math.Round(Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(indata["YKNUM"]), 2);
                            }
                            else
                            {
                                bb["OTHERMONEY"] = 0;
                                bb["TOTALPRICE"] = 0;
                                bb["TOTALMONEY"] = 0;
                            }

                            bb["PRODUCTDATE"] = Convert.ToDateTime(indata["PRODUCTDATE"]);
                            bb["CHOSCODE"] = indata["CHOSCODE"].ToString();
                            bb["STOCKFLOWNO"] = indata["STOCKFLOWNO"];
                            bb["GG"] = indata["GG"];
                            bb["XH"] = indata["XH"];
                            bb["CD"] = indata["CD"];
                            bb["SUPPLYID"] = indata["SUPPLYID"];
                            bb["SUPPLYNAME"] = indata["SUPPLYNAME"];
                            bb["VALIDDATE"] = indata["VALIDDATE"];

                            bb["MEMO"] = indata["MEMO"];
                            bb["TXM"] = indata["TXM"];

                            Opt saveeqindetail = OptContent.get("SaveEQInDetailEQPanDianIn");
                            if (DaoTool.Save(dao, saveeqindetail, bb) < 0)
                                throw new Exception("生成入库细表失败！");

                            //向盘点细表中加入入库流水号
                            ii["ROWNO"] = Convert.ToDecimal(indata["ROWNO"]);
                            ii["PDID"] = Convert.ToDecimal(indata["PDID"]);
                            ii["DETAILNO"] = bb["DETAILNO"];
                            ii["CHOSCODE"] = dic["CHOSCODE"];
                            Opt pdwz_updatadetail = OptContent.get("UpdataInDetailEQPanDianInfo_In");
                            if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail, ii) < 0)
                                throw new Exception("向盘点细表添加入库流水号失败！");

                            //生成库存总表

                            bb["WARECODE"] = aa["WARECODE"];
                            Opt stocknum = OptContent.get("GetNumInStock_EQPanDianIn");//获得库存数量
                            ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);


                            cc["WARECODE"] = aa["WARECODE"].ToString();
                            cc["EQID"] = Convert.ToDecimal(indata["EQID"]);
                            cc["CHOSCODE"] = dic["CHOSCODE"];
                            cc["DEPTID"] = null;
                            cc["UNITCODE"] = indata["UNITCODE"];
                            Opt ifexist = OptContent.get("IfExistInStockEQPanDianIn");
                            ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                            if (objtem2 != null)
                            {
                                //Opt iflsunitcode = OptContent.get("iflsunitcodeInStockWZPanDianIn");//是否为最小单位编码
                                //ObjItem objtem3 = DaoTool.ExecuteScalar(dao, iflsunitcode, aa);
                                cc["STOCKID"] = objtem2.ToInt();
                                cc["NUM"] = objtem4.ToInt() + Convert.ToInt32(indata["YKNUM"].ToString());

                                dd["BEFORENUM"] = objtem4.ToInt();
                                Opt updatastocktable = OptContent.get("UpdataInStockNum_EQPanDianIn");
                                if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                                    throw new Exception("生成库存总表失败!");
                            }
                            else
                            {
                                cc["STOCKID"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCK");
                                cc["NUM"] = Convert.ToInt32(indata["YKNUM"].ToString());
                                dd["BEFORENUM"] = 0;
                                Opt stocktable = OptContent.get("SaveInStockEQPanDianIn");
                                if (DaoTool.Save(dao, stocktable, cc) < 0)
                                    throw new Exception("生成库存总表失败！");

                            }



                            //生成库存细表
                            dd["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");
                            dd["INID"] = Convert.ToDecimal(aa["INID"].ToString());
                            dd["WARECODE"] = aa["WARECODE"].ToString();
                            dd["EQID"] = Convert.ToDecimal(indata["EQID"]);
                            dd["STOCKID"] = Convert.ToDecimal(indata["STOCKID"]);
                            dd["NUM"] = Convert.ToDecimal(indata["YKNUM"]);
                            dd["UNITCODE"] = indata["UNITCODE"];
                            dd["OUTNUM"] = 0;
                            dd["CARDNUM"] = 0;
                            dd["PRICE"] = bb["PRICE"];
                            dd["MONEY"] = bb["MONEY"];
                            dd["OTHERMONEY"] = bb["OTHERMONEY"];
                            dd["TOTALPRICE"] = bb["TOTALPRICE"];
                            dd["TOTALMONEY"] = bb["TOTALMONEY"];
                            dd["SUPPLYID"] = bb["SUPPLYID"];
                            dd["SUPPLYNAME"] = bb["SUPPLYNAME"];
                            dd["PRODUCTDATE"] = bb["PRODUCTDATE"];
                            dd["VALIDDATE"] = bb["VALIDDATE"];
                            dd["MEMO"] = bb["MEMO"];
                            dd["TXM"] = bb["TXM"];
                            dd["RECIPECODE"] = indata["RECIPECODE"];
                            dd["SHDH"] = indata["SHDH"];
                            dd["GHSUPPLYID"] = indata["GHSUPPLYID"];
                            dd["INDATE"] = DateTime.Now;
                            dd["GHSUPPLYNAME"] = indata["GHSUPPLYNAME"];
                            dd["CHOSCODE"] = dic["CHOSCODE"];

                            Opt stockdetailtable = OptContent.get("SaveInStockDetailEQPanDianIn");
                            if (DaoTool.Save(dao, stockdetailtable, dd) < 0)
                                throw new Exception("生成库存细表失败！");


                            //向入库细表中加入对应的库存流水表的流水号

                            mm["EQID"] = Convert.ToDecimal(indata["EQID"]);
                            mm["DETAILNO"] = bb["DETAILNO"];
                            mm["STOCKFLOWNO"] = dd["FLOWNO"];
                            mm["CHOSCODE"] = dic["CHOSCODE"];
                            Opt EQPD_Stockflow = OptContent.get("EQPD_updataIndetailStockflow");
                            if (DaoTool.ExecuteNonQuery(dao, EQPD_Stockflow, mm) < 0)
                                throw new Exception("向入库细表添加库存流水号失败！");

                        }

                    }





                    Opt getpdout = OptContent.get("GetPDIDEQPanDianInfoOut");
                    listout = DaoTool.Find(dao, getpdout, dic);
                    if (listout != null && listout.Count > 0)
                    {

                        //生成出库主表
                        aa["OUTID"] = DaoTool.Seq(dao, "LKEQ.SEQEQOUT");

                        Opt getioid = OptContent.get("GetIOIDEQPanDianInfoStockOut");
                        ObjItem objitem = DaoTool.ExecuteScalar(dao, getioid, dic);
                        aa["IOID"] = Convert.ToDecimal(objitem.ToString());
                        //单据号
                        DataTable dt = dao.Fd(OptContent.get("EQSearchDicEQInOut").Sql, new object[] { aa["IOID"].ToString() });
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


                        decimal recipeno = Convert.ToDecimal(dao.ExecuteScalar(OptContent.get("GetRecipeNo").Sql, new object[] { recipe + '%' })) + 1;
                        if (recipeno > 0 && recipeno < 10)
                        {
                            recipe = recipe + "0" + recipeno.ToString();
                        }
                        else
                        {
                            recipe = recipe + recipeno.ToString();
                        }

                        aa["RECIPECODE"] = recipe;
                        aa["OPFLAG"] = 3;
                        aa["MEMO"] =dic["MEMO"];
                        aa["RECDATE"] = DateTime.Now;
                        aa["STATUS"] = 6;
                        aa["WARECODE"] = dic["WARECODE"];
                        aa["OUTDATE"] = DateTime.Now;
                        aa["USERID"] = dic["USERID"];
                        aa["USERNAME"] = dic["USERNAME"];
                        aa["SHDATE"] = DateTime.Now;
                        aa["SHUSERID"] = dic["SHUSERID"];
                        aa["SHUSERNAME"] = dic["SHUSERNAME"];
                        aa["CHOSCODE"] = dic["CHOSCODE"];

                        //得到总金额和零售总金额
                        decimal total = 0;

                        foreach (var outdata in listout)
                        {
                            if (outdata["PRICE"] == null || outdata["PRICE"].ToString() == "")//保证有价格
                            {
                                total = 0;
                            }
                            else
                            {
                                total += Convert.ToDecimal(outdata["PRICE"]) * Math.Abs(Convert.ToDecimal(outdata["YKNUM"]));
                            }
                        }
                        aa["TOTALMONEY"] = total;

                        Opt savewzoutmain = OptContent.get("SaveEQOutMainEQPanDianOut");
                        if (DaoTool.Save(dao, savewzoutmain, aa) < 0)
                            throw new Exception("生成出库主表失败！");



                        //向盘点主表中加入出库单序号

                        ll["PDID"] = Convert.ToDecimal(Convert.ToDecimal(dic["PDID"]));
                        ll["OUTID"] = aa["OUTID"];
                        ll["CHOSCODE"] = dic["CHOSCODE"];
                        Opt pdwz_updataed2 = OptContent.get("UpdataInEQPanDianInfo_Out");
                        if (DaoTool.ExecuteNonQuery(dao, pdwz_updataed2, ll) < 0)
                            throw new Exception("向盘点主表添加出库单序号失败！");




                        //生成出库细表

                        bb["OUTID"] = aa["OUTID"];
                        data.Param.Add("EQID", null); //不知这两句话什么意思？
                        data.Param.Add("STOCKID", null);
                        foreach (Dictionary<string, object> outdata in listout)
                        {
                            bb["DETAILNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQOUTDETAIL");
                            bb["EQID"] = Convert.ToDecimal(outdata["EQID"]);

                            bb["UNITCODE"] = outdata["UNITCODE"];//盘盈数据里的最小编码？无编码

                            bb["NUM"] = Convert.ToDecimal(outdata["YKNUM"]) * (-1);
                            bb["PRICE"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]), 4);
                            bb["MONEY"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]) * Convert.ToDecimal(bb["NUM"]), 4);
                            Dictionary<string, object> GetYzf = DaoTool.Get(dao, OptContent.get("EQPanDian_GetStockDetailInfo"), outdata);

                            if (GetYzf["OTHERMONEY"] != null && GetYzf["NUM"] != null && GetYzf["OUTNUM"] != null)
                            {
                                decimal a1 = decimal.Parse(GetYzf["OTHERMONEY"].ToString());
                                decimal b1 = decimal.Parse(GetYzf["NUM"].ToString());
                                decimal c1 = decimal.Parse(GetYzf["OUTNUM"].ToString());
                                decimal d1 = 0;
                                if (b1 - c1 == 0)
                                {
                                    d1 = 0;
                                    bb["TOTALPRICE"] = 0;
                                }
                                else
                                {
                                    d1 = -(a1 / (b1 - c1)) * Convert.ToDecimal(outdata["YKNUM"]);
                                    bb["TOTALPRICE"] = Math.Round(Convert.ToDecimal(outdata["PRICE"]) + (-a1 / (b1 - c1)), 4);
                                }
                                bb["OTHERMONEY"] = Math.Round(d1, 2);

                                bb["TOTALMONEY"] = Math.Round(Convert.ToDecimal(bb["TOTALPRICE"]) * Convert.ToDecimal(outdata["YKNUM"]), 2);
                            }
                            else
                            {

                                bb["OTHERMONEY"] = 0;
                                bb["TOTALPRICE"] = 0;
                                bb["TOTALMONEY"] = 0;
                            }
                            bb["PRODUCTDATE"] = Convert.ToDateTime(outdata["PRODUCTDATE"]);
                            bb["CHOSCODE"] = outdata["CHOSCODE"].ToString();
                            bb["STOCKFLOWNO"] = outdata["STOCKFLOWNO"];
                            bb["GG"] = outdata["GG"];
                            bb["XH"] = outdata["XH"];
                            bb["CD"] = outdata["CD"];
                            bb["SUPPLYID"] = outdata["SUPPLYID"];
                            bb["SUPPLYNAME"] = outdata["SUPPLYNAME"];
                            bb["VALIDDATE"] = outdata["VALIDDATE"];
                            bb["MEMO"] = outdata["MEMO"];
                            bb["TXM"] = outdata["TXM"];


                            Opt saveeqoutdetail = OptContent.get("SaveEQOutDetailEQPanDianOut");
                            if (DaoTool.Save(dao, saveeqoutdetail, bb) < 0)
                                throw new Exception("生成出库细表失败！");

                            //向盘点细表中加入出库流水号
                            jj["ROWNO"] = Convert.ToDecimal(outdata["ROWNO"]);
                            jj["PDID"] = Convert.ToDecimal(outdata["PDID"]);
                            jj["DETAILNO"] = bb["DETAILNO"];
                            jj["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                            Opt pdwz_updatadetail2 = OptContent.get("UpdataInDetailDEQPanDianInfo_Out");
                            if (DaoTool.ExecuteNonQuery(dao, pdwz_updatadetail2, jj) < 0)
                                throw new Exception("向盘点细表添加出库流水号失败！");




                            //生成库存总表
                            bb["WARECODE"] = dic["WARECODE"];
                            Opt stocknum = OptContent.get("GetNumInStock_EQPanDianIn");//获得库存数量
                            ObjItem objtem4 = DaoTool.ExecuteScalar(dao, stocknum, bb);


                            Opt ifexist = OptContent.get("IfExistInStockEQPanDianIn");
                            ObjItem objtem2 = DaoTool.ExecuteScalar(dao, ifexist, bb);//是否在库存表中已存在

                            cc["STOCKID"] = objtem2.ToInt();
                            cc["WARECODE"] = aa["WARECODE"];
                            cc["EQID"] = Convert.ToDecimal(outdata["EQID"]);
                            cc["CHOSCODE"] = aa["CHOSCODE"];
                            cc["DEPTID"] = null;
                            cc["UNITCODE"] = outdata["UNITCODE"];

                            cc["NUM"] = objtem4.ToInt() - Convert.ToInt32(outdata["YKNUM"].ToString()) * (-1);



                            Opt updatastocktable = OptContent.get("UpdataInStockNum_EQPanDianIn");
                            if (DaoTool.ExecuteNonQuery(dao, updatastocktable, cc) < 0)
                                throw new Exception("生成库存总表失败！");

                            //生成库存细表

                            data.Param["EQID"] = Convert.ToDecimal(outdata["EQID"]);
                            data.Param["STOCKID"] = objtem2.ToInt();
                            data.Param["STOCKFLOWNO"] = Convert.ToDecimal(outdata["STOCKFLOWNO"]);
                            data.Param["CHOSCODE"] = dic["CHOSCODE"];
                            Opt stockdetailinfo = OptContent.get("StockDetailInfoEQPanDianOut");//获得库存细表信息
                            DataTable datatable = DaoTool.FindDT(dao, stockdetailinfo, data);

                            DataRow dr = datatable.Rows[0];

                            dd["OUTNUM"] = Convert.ToInt32(dr["OUTNUM"]) + Convert.ToInt32(outdata["YKNUM"].ToString()) * (-1);



                            dd["FLOWNO"] = DaoTool.Seq(dao, "LKEQ.SEQEQSTOCKDETAIL");

                            dd["INID"] = dr["INID"];
                            dd["WARECODE"] = dr["WARECODE"];
                            dd["DEPTID"] = dr["DEPTID"];
                            dd["EQID"] = dr["EQID"];
                            dd["STOCKID"] = dr["STOCKID"];
                            dd["NUM"] = dr["NUM"];
                            dd["BEFORENUM"] = dr["BEFORENUM"];
                            dd["UNITCODE"] = dr["UNITCODE"];
                            dd["OUTNUM"] = dr["OUTNUM"];
                            dd["CARDNUM"] = dr["CARDNUM"];
                            dd["GG"] = dr["GG"];
                            dd["XH"] = dr["XH"];
                            dd["CD"] = dr["CD"];
                            dd["PRICE"] = dr["PRICE"];
                            dd["MONEY"] = dr["MONEY"];
                            dd["OTHERMONEY"] = dr["OTHERMONEY"];
                            dd["TOTALPRICE"] = dr["TOTALPRICE"];
                            dd["TOTALMONEY"] = dr["TOTALMONEY"];
                            dd["SUPPLYID"] = dr["SUPPLYID"];
                            dd["SUPPLYNAME"] = dr["SUPPLYNAME"];
                            dd["PRODUCTDATE"] = dr["PRODUCTDATE"];
                            dd["VALIDDATE"] = dr["VALIDDATE"];

                            dd["MEMO"] = dr["MEMO"];
                            dd["TXM"] = dr["TXM"];
                            dd["RECIPECODE"] = dr["RECIPECODE"];
                            dd["SHDH"] = dr["SHDH"];
                            dd["GHSUPPLYID"] = dr["GHSUPPLYID"];
                            dd["GHSUPPLYNAME"] = dr["GHSUPPLYNAME"];
                            dd["CHOSCODE"] = dr["CHOSCODE"];
                            dd["INDATE"] = dr["INDATE"];
                            Opt stockoutdetailtable = OptContent.get("SaveInStockDetailEQPanDianIn");
                            if (DaoTool.Save(dao, stockoutdetailtable, dd) < 0)
                                throw new Exception("生成库存细表失败！");
                        }

                    }

                   
                
                msg = "冲销成功！";
                return "ok";


            }
            
            
            return "ok";


        }
        #endregion
    }
}
