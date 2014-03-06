using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtUtil.tool;
using YtClient;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtWinContrl.com;
using YtPlugin;

namespace EQWareManag.form
{
    public partial class EQOut_Add : Form
    {
        Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        string str1 = "2";
        string str2 = "0";
        IAppContent app;
        public EQOut_Add(string inware_id, string inware_name, IAppContent app)
        {
            this.app = app;
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public EQOut_Add(Dictionary<string, ObjItem> dr, int isAdd, IAppContent app)
        {
            this.app = app;
            this.isAdd = isAdd;
            this.dr = dr;
            InitializeComponent();
        }
        public EQOut Main;
       
        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["设备ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【设备】！");
                this.dataGView1.setFocus(e.Row.Index, "设备");
                return;
            }
            if (e.Data["库存流水号"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【库存流水号】！");
                this.dataGView1.setFocus(e.Row.Index, "库存流水号");
                return;
            }
            if (!WJs.IsZs(e.Data["数量"].ToString()) || e.Data["数量"].ToDouble() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】只能输入整数，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
            if (!WJs.MaxNumOver(e.Data["数量"].ToString(), "第" + (e.Row.Index + 1) + "行【数量】"))
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】太大，请重新输入！");
                this.dataGView1.setFocus(e.Row.Index, "数量");

                return;
            }
            if (e.Data["现有库存量"].ToDouble() < e.Data["数量"].ToDouble())
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】不能大于其当前的库存量！");
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
          

        }

      
        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //判断是否选择库房
            if (this.InWare_selTextInpt.Value ==null)
            {
                WJs.alert("请选择出库库房！");
                InWare_selTextInpt.Focus();
                return;
            }
            //判断是否选择入库方式
            if (this.InWay_selTextInpt.Value == null)
            {
                WJs.alert("请选择出库方式！");
                InWay_selTextInpt.Focus();
                return;
            }

            Dictionary<string, object> de = new Dictionary<string, object>();
      
            de["数量"] = 0;
            de["医疗机构编码"] = His.his.Choscode;
            string IfCanOut = LData.Es("findEQUse_IfCanOut", null, new object[] { His.his.Choscode });
            string wareid = this.InWare_selTextInpt.Value;
            string ifall = LData.Es("EQWareIfall", null, new object[] { wareid });
            //string wz_s = "物资";
           // string wzid_s="";
            if (ifall.Equals("1"))
            {
                if (IfCanOut == "0")
                {
                    dataGView1.addSql("GetOutEQ0", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                }
                else
                {
                    dataGView1.addSql("GetOutEQ01", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                }
            }
            else
            {

                if (IfCanOut == "0")
                {
                    dataGView1.addSql("GetOutEQ1", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                }
                else
                {
                    dataGView1.addSql("GetOutEQ11", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                }
              
            }
          
          // this.dataGView1.addSql("GetSupply", "生产厂家", "", His.his.Choscode + "|{key}|{key}|{key}|{key}");
           // TvList.newBind().SetCacheKey("XmDw").Load("GetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);

          // this.dataGView1.CellEndEdit += new DataGridViewCellEventHandler(dataGView1_CellEndEdit);



           
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.CurrentRow.Cells[eq.Index].ReadOnly = false;


        }


        void ChangeStyle(object sender, DataGridViewCellEventArgs e)
        {

            int it1 = Convert.ToInt32(str1);
            this.dataGView1.CurrentRow.Cells[rate.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[rate.Index].Value), it1);
            this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value), it1);
            this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[cbdj.Index].Value), it1);
            this.dataGView1.CurrentRow.Cells[yzf.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[yzf.Index].Value), it1);

            ////统计金额
            this.totalmoney_yTextBox.Text = Math.Round(Convert.ToDecimal(dataGView1.Sum("金额")), it1).ToString();


            if (str2 == "1")
            {
                this.dataGView1.CurrentRow.Cells[rate.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[rate.Index].Value);
                this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value);
                this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[cbdj.Index].Value);
                this.dataGView1.CurrentRow.Cells[yzf.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[yzf.Index].Value);
                this.totalmoney_yTextBox.Text = String.Format("{0:N" + str1 + "}", dataGView1.Sum("金额").ToString());

            }
        
        
        }
        
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)//还有些问题
        {
            if (this.dataGView1.ReadOnly) return;
           
            Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);

           

            if (e.ColumnIndex == Column1.Index && e.RowIndex > -1)
            {
                this.dataGView1.CurrentRow.Cells[num.Index].Value = "0";
            }
            if ((e.ColumnIndex == Column5.Index || e.ColumnIndex == price.Index || e.ColumnIndex == yzf.Index || e.ColumnIndex == num.Index || e.ColumnIndex == cbdj.Index) && e.RowIndex > -1)
            {
                decimal dec;
                //if(!decimal.TryParse( this.dataGView1.CurrentRow.Cells[yzf.Index].Value.ToString(),out dec))
                //{
                //    WJs.alert("请输入大于零的整数或者小数");
                //    this.dataGView1.CurrentRow.Cells[yzf.Index].Value = 0;
                //    return ;
                //}

             
               // int it1= Convert.ToInt32(str1);
              
               //this.dataGView1.CurrentRow.Cells[yzf.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[yzf.Index].Value), it1);
               //this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[cbdj.Index].Value), it1);
               //this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value), it1);
               //this.dataGView1.CurrentRow.Cells[rate.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[rate.Index].Value), it1);
               ////if (str2 == "1")
               //{
               //    this.dataGView1.CurrentRow.Cells[yzf.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[yzf.Index].Value);
               //    this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[cbdj.Index].Value);
               //    this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value);
               //    this.dataGView1.CurrentRow.Cells[rate.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[rate.Index].Value);
            
               //}
                 try
                {
                    this.dataGView1.jsBds("金额=数量*单价");
                    this.dataGView1.jsBds("运杂率=原运杂费/(入库数-出库数)");
                    this.dataGView1.jsBds("运杂费=运杂率*数量");
                    this.dataGView1.jsBds("成本单价=单价+运杂费/数量");
                    this.dataGView1.jsBds("成本金额=成本单价*数量");
                    ////统计金额
                    this.totalmoney_yTextBox.Text = dataGView1.Sum("金额").ToString();

                }
                catch
                {

                }
            
            }
           
            
        }
        private bool valid()
        {
            if (this.InWay_selTextInpt.Value==null)
            {
                WJs.alert("请选择出库方式！");
                this.InWay_selTextInpt.Focus();
                return false;
            }
            if (this.InWare_selTextInpt.Value==null)
            {
                WJs.alert("请选择出库库房！");
                this.InWare_selTextInpt.Focus();
                return false;
            }
           
            return true;
        }
        private void save_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.valid())
            {
                if (this.dataGView1.RowCount == 0)
                {
                    WJs.alert("请添加出库设备");
                    return;
                }
                if (this.totalmoney_yTextBox.Text.Trim().Length > 11)
                {
                    WJs.alert("总金额太大(不能超过100000.0000)！请减少该批次设备");
                    return;
                }
               
                string str = this.dataGView1.GetDataToXml();
                if (str != null)
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
                    ac.Sql = "ChuKuDanSave";
                    //ac.Add("过单日期", this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    ac.Add("IOID", this.InWay_selTextInpt.Value);
                    ac.Add("WARECODE", this.InWare_selTextInpt.Value);

                 
                   
                    ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                    ac.Add("OUTDATE", this.dateTimePicker2.Value);

                    ac.Add("MEMO", this.memo_yTextBox.Text);
                   
                    ac.Add("USERID", His.his.UserId);
                    ac.Add("USERNAME", His.his.UserName);
                    ac.Add("RECDATE", DateTime.Now);
                    ac.Add("CHOSCODE", His.his.Choscode);
                   
                  
                    ac.Add("DanJuMx", str);
                    if (isAdd == 2)
                    {
                        ac.Add("OUTID", dr["出库ID"].ToString());
                    }
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    
                    ac.Post();

                }
            }

        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            this.Close();
        }
        void ac2_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            // this.Close();
        }
        void InitInWare()
        {
            this.dataGView1.Rows.Clear();
            this.InWay_selTextInpt.Text = "";
            this.InWay_selTextInpt.Value = "";
           
            this.memo_yTextBox.Text = "";
           
            this.totalmoney_yTextBox.Text = "0";
           

        }

        private void cancel_toolStripButton_Click(object sender, EventArgs e)
        {
            if(WJs.confirm("是否放弃保存，暂存数据将清空！"))
            {
                InitInWare();
                
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> doc = this.dataGView1.getRowData();
            if (doc != null)
            {
                if (doc["设备"].IsNull)
                {
                    this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                }
                else
                {
                    if (WJs.confirmFb("您确定要删除选择的出库设备信息吗？"))
                    {

                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);

                        this.dataGView1.jsBds("金额=数量*单价");
                        this.dataGView1.jsBds("运杂率=原运杂费/(入库数-出库数");
                        this.dataGView1.jsBds("运杂费=运杂率*数量");
                        this.dataGView1.jsBds("成本单价=单价+运杂费/数量");
                        this.dataGView1.jsBds("成本金额=成本单价*数量");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView1.Sum("金额").ToString();

                        if (!doc["流水号"].IsNull && !doc["流水号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
                            ac.Sql = "ChuKuDanEQdelete";
                            ac.Add("DETAILNO", doc["流水号"].ToString());
                            //ac.Add("WZID", doc["物资ID"].ToString());
                            //获取入库ID
                            string inwareid = LData.Es("EQGetInWareId", null, new object[] { doc["流水号"].ToString() });

                            ac.Add("OUTID", inwareid);


                            ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                           
                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac2_ServiceLoad);
                            ac.Post();


                        }
                    }
                }    

            }
            else
            {
                WJs.alert("请选择需要删除的行！");
            }
           
        }

       


      


        private void EQIn_Add_Load(object sender, EventArgs e)//加载窗体
        {
            this.WindowState = FormWindowState.Maximized;
            str2 = LData.Es("EQSystemValue_2204", null, new object[] { His.his.Choscode });
            str1 = LData.Es("EQSystemValue_2203", null, new object[] { His.his.Choscode });
            
            this.toolStripButton_Pass.Visible = false;
            this.toolStripButton_Down.Visible = false;
            if (isAdd == 1)
            {
                this.InWare_selTextInpt.Text = inware_name;
                this.InWare_selTextInpt.Value = inware_id;
                this.InWare_selTextInpt.Enabled = false;
            }
            this.InWay_selTextInpt.Sql = "EQGetOutWareWay";
            this.InWay_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";




            this.dataGView1.Url = "EQOutDetailSearch";

            this.dataGView1.addSql("GetOutEQ_StockFlow", "库存流水号", "", His.his.Choscode + "|" + "|{key}|{key}|{key}|{key}");
 
            TvList.newBind().SetCacheKey("XmDw1").Load("EQIn_DanWeiBianMa", null).Bind(this.Column3);

           
            if (isAdd !=1 )
            {
                this.InWay_selTextInpt.Text = LData.Exe("EQGetOutWareWayName", null, new object[] { His.his.Choscode, dr["出库方式ID"].ToString() });
                this.InWare_selTextInpt.Text = LData.Exe("EQGetOutWareName", null, new object[] { His.his.Choscode, dr["出库库房编码"].ToString() });
                this.InWay_selTextInpt.Value = dr["出库方式ID"].ToString();
                this.InWare_selTextInpt.Value = dr["出库库房编码"].ToString();

                inware_id = dr["出库库房编码"].ToString();
                inware_name = dr["出库库房编码_Text"].ToString();
                this.InWare_selTextInpt.Enabled=false;
              
               
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();

               this.dateTimePicker2.Value = dr["制单日期"].ToDateTime();
               this.dataGView1.reLoad(new object[] { dr["出库ID"].ToString(), His.his.Choscode });
             
                if (isAdd == 2)
               {
                   for (int i = 0; i < this.dataGView1.RowCount; i++)
                   {
                       dataGView1.addSql("GetOutEQ_StockFlow", "库存流水号", "", this.dataGView1.Rows[i].Cells[eqid.Index].Value.ToString() +
                        "|" + this.inware_id+"|"+His.his.Choscode);
                       if (this.dataGView1.Rows[i].Cells[yzf.Index].Value.ToString().Trim() !="" && this.dataGView1.Rows[i].Cells[num.Index].Value.ToString() != "0")
                       {
                           this.dataGView1.Rows[i].Cells[Column5.Index].Value = decimal.Parse(this.dataGView1.Rows[i].Cells[yzf.Index].Value.ToString()) / decimal.Parse(this.dataGView1.Rows[i].Cells[num.Index].Value.ToString());

                       }
                      // this.dataGView1.Rows[i].Cells[Column9.Index].Value = LData.Es("GetOutEQ_StockNum", null, new object[] { this.dataGView1.Rows[i].Cells[eqid.Index].Value.ToString(), inware_id, His.his.Choscode });
                   }
               }
                for (int i = 0; i < this.dataGView1.RowCount; i++)
                {
                    this.dataGView1.Rows[i].Cells[Column9.Index].Value = LData.Es("GetOutEQ_StockNum", null, new object[] { this.dataGView1.Rows[i].Cells[eqid.Index].Value.ToString(), inware_id, His.his.Choscode });
                }
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.dataGView1.Columns[eq.Index].ReadOnly = true;
              
                this.cancel_toolStripButton.Enabled = false;
                if (isAdd == 3)
                {
                    this.toolStrip1.Enabled = true;
                    this.add_toolStripButton.Visible = false;
                    this.save_toolStripButton.Visible = false;
                    this.DeleButton.Visible = false;
                    this.cancel_toolStripButton.Visible = false;
                    this.toolStripButton_Pass.Visible = false;
                    this.toolStripButton_Down.Visible = false;
                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;
                    this.dateTimePicker2.Enabled = false;
                    this.memo_yTextBox.ReadOnly = true;
                }
                if (isAdd == 4)
                {
                    this.toolStrip1.Enabled = true;
                    this.add_toolStripButton.Visible = false;
                  
                    this.save_toolStripButton.Visible = false;
                    this.DeleButton.Visible = false;
                    this.cancel_toolStripButton.Visible = false;
                    this.toolStripButton_Pass.Visible = true;
                    this.toolStripButton_Down.Visible = true;
                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;              
                    this.dateTimePicker2.Enabled = false;
                    this.memo_yTextBox.ReadOnly = true;
                }
               
              
                
                 this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                 //BindUnit();
               
            }

          
            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
       
        }

       
        void ac0_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            // WJs.alert(e.Msg.Msg);

        }



        //void ac3_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        //{

        //    //更新出库细表的库存流水号
        //    string flowno = e.Msg.Msg.Split('+')[0];
        //    string DETAILNO = e.Msg.Msg.Split('+')[1];
        //    ActionLoad ac2 = new ActionLoad();
        //    ac2.Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
        //    ac2.Sql = "UpdateEQInDetailInfo";
        //    ac2.Add("DETAILNO", DETAILNO);
        //    ac2.Add("STOCKFLOWNO", flowno);
        //    ac2.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
        //    ac2.Post();
        //}
        private void toolStripButton_Pass_Click(object sender, EventArgs e)
        {
            DataTable datatable = LData.LoadDataTable("EQOutDetailSearch", new object[] { dr["出库ID"].ToString(), His.his.Choscode });
            if (datatable != null)
            {
                foreach (DataRow r in datatable.Rows)
                {
                   
                    if (r != null)
                    {
                        //更新库存总表信息
                        decimal before_num = 0;
                        decimal num = Convert.ToDecimal(r["NUM"]);
                        decimal after_num = 0;

                        ActionLoad ac0 = new ActionLoad();
                        ac0.Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
                        ac0.Add("EQID", r["EQID"].ToString());
                        DataTable stock_table = LData.LoadDataTable("EQOut_GetSTOCKTable", new object[] { His.his.Choscode, r["EQID"].ToString(), dr["出库库房编码"].ToString() });
                        if (stock_table != null)
                        {
                            //库存总表中存在该库房对应的这种物资，只需更新库存量
                            before_num = Convert.ToDecimal(stock_table.Rows[0]["NUM"]);

                            after_num = before_num - num;
                            if (after_num < 0)
                            {
                                WJs.alert("设备" + r["设备"]+"的现有库存数已经低于出库数，审核应为不通过！");
                                return;
                            
                            }
                            ac0.Sql = "UpdateEQStock";

                            ac0.Add("STOCKID", stock_table.Rows[0]["STOCKID"]);
                            ac0.Add("NUM", after_num);


                        }
                      
                        ac0.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                        ac0.Post();
                        //LData.Exe("GetSTOCKID", null, new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                        //string stockid_1 = LData.Es("GetSTOCKID", null, new object[] { His.his.Choscode, r["WZID"].ToString(), dr["入库库房编码"].ToString() });
                        // string dep =  r["DETAILNO"].ToString();

                        //更新库存流水表
                        ActionLoad ac1 = new ActionLoad();
                        ac1.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                        ac1.Sql = "SaveEQStockDetail";
                        ac1.Add("FLOWNO", r["STOCKFLOWNO"].ToString());
                        string outnum = LData.Es("EQGetOutNum", null, new object[] { r["STOCKFLOWNO"].ToString() });
                        decimal num1 = Convert.ToDecimal(outnum) + num;
                        ac1.Add("OUTNUM", num1);

                        //ac1.Add("OUTID", r["OUTID"].ToString());
                        //ac1.Add("WARECODE", dr["出库库房编码"].ToString());
                        //string stockid_s;
                        //// string befornum = "0"; 
                        //if (stock_table != null)
                        //{
                        //    stockid_s = stock_table.Rows[0]["STOCKID"].ToString();

                        //}
                        //else
                        //{
                        //    stockid_s = LData.Es("EQGetSTOCKId", null, new object[] { His.his.Choscode, r["EQID"].ToString(), dr["出库库房编码"].ToString() });
                        //}
                        //ac1.Add("STOCKID", stockid_s);
                        //ac1.Add("BEFORENUM", before_num);
                        //ac1.Add("NUM", num);

                        //ac1.Add("UNITCODE", r["UNITCODE"]);
                        //ac1.Add("OUTNUM", 0);
                        //ac1.Add("CARDNUM",0);
                        //ac1.Add("GG", r["GG"]);
                        //ac1.Add("XH", r["XH"]);
                        //ac1.Add("CD", r["CD"]);
                        //ac1.Add("PRICE", r["PRICE"]);
                        //ac1.Add("MONEY", r["MONEY"]);
                        //ac1.Add("OTHERMONEY", r["OTHERMONEY"]);

                        //ac1.Add("TOTALPRICE", r["TOTALPRICE"]);
                        //ac1.Add("TOTALMONEY", r["TOTALMONEY"]);
                        //ac1.Add("SUPPLYID", r["SUPPLYID"]);
                        //ac1.Add("SUPPLYNAME", r["SUPPLYNAME"]);
                        //ac1.Add("PRODUCTDATE", Convert.ToDateTime(r["PRODUCTDATE"]));
                        //ac1.Add("VALIDDATE", Convert.ToDateTime(r["VALIDDATE"]));
                        //ac1.Add("MEMO", r["MEMO"]);
                        //ac1.Add("TXM", r["TXM"]);
                        

                        //ac1.Add("RECIPECODE", dr["单据号"].ToString());
                        //ac1.Add("SHDH", dr["随货单号"].ToString());
                        //ac1.Add("GHSUPPLYID", dr["供货商ID"].ToString());
                        //ac1.Add("GHSUPPLYNAME", dr["供货商名称"].ToString());
                        //ac1.Add("INDATE", DateTime.Now);
                        //ac1.Add("CHOSCODE", His.his.Choscode);


                        //ac1.Add("DETAILNO", r["DETAILNO"]);//用于更新出库细表

                        ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                        ac1.Post();
                        ////更新入库细表的库存流水号
                        //ActionLoad ac2 = new ActionLoad();
                        //ac2.Action = "LKWZSVR.lkwz.WZIn.WZInDan";
                        //ac2.Sql = "UpdateWZInDetailInfo";
                        //ac2.Add("DETAILNO", r["DETAILNO"].ToString());
                        //ac2.Add("STOCKFLOWNO", r["FLOWNO"].ToString());
                        //if (dr["采购计划流水号"] != null)
                        //{
                        //    ActionLoad ac3 = new ActionLoad();
                        //    ac3.Action = "LKWZSVR.lkwz.WZPlan.WZPlanMain";
                        //    ac3.Sql = "Save";
                        //    ac3.Add("PLANID", dr["采购计划流水号"].ToString());
                        //    ac3.Add("STATUS", 6);
                        //    ac3.Add("SHINDATE", DateTime.Now);
                        //    ac3.Add("SHINUSERID", His.his.UserId);
                        //    ac3.Add("SHINUSERNAME", His.his.UserName);
                        //    ac3.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        //    ac3.Post();
                        //}
                    }
                }


                ActionLoad ac = new ActionLoad();
                ac .Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
                ac.Sql = "ChuKuDanUpdate";
                ac.Add("SHINDATE", DateTime.Now);
                ac.Add("STATUS", 6);
                ac.Add("OUTID", dr["出库ID"].ToString());
                ac.Add("SHINUSERID", His.his.UserId);
                ac.Add("SHINUSERNAME", His.his.UserName);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_SHTG);
                ac.Post();
     
            }

        }

        private void toolStripButton_Down_Click(object sender, EventArgs e)
        {
            ActionLoad ac = new ActionLoad();
            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQOutSvr";
            ac.Sql = "ChuKuDanUpdate";
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("STATUS", 2);
            ac.Add("OUTID", dr["出库ID"].ToString());
            ac.Add("SHUSERID", His.his.UserId.ToString());
            ac.Add("SHUSERNAME", His.his.UserName);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_SHBTG);
            ac.Post();


        }
        void ac_ServiceLoad_SHTG(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            Main.Reload(3);
            this.Close();
        }
        void ac_ServiceLoad_SHBTG(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            Main.Reload(2);
            this.Close();
        }

        private void dataGView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
             
            if (e.ColumnIndex == eqid.Index || e.ColumnIndex == Column1.Index)
            {
                if (this.dataGView1.CurrentRow.Cells[eqid.Index].Value != null)
                    dataGView1.addSql("GetOutEQ_StockFlow", "库存流水号", "", this.dataGView1.CurrentRow.Cells[eqid.Index].Value.ToString() +
                        "|" + this.inware_id + "|" + His.his.Choscode);


                this.dataGView1.CurrentRow.Cells[eq.Index].ReadOnly = true;

            }
       
        }

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.TiaoSu.Text =  this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; 
           
            this.JinEHeJi.Text = this.dataGView1.Sum("金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("运杂费").ToString() + "元";
            this.label3.Text = this.dataGView1.Sum("原运杂费").ToString() + "元";
            this.label5.Text = this.dataGView1.Sum("成本金额").ToString() + "元";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isAdd == 3)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("EQOutMainDaYinQuery", new object[] { dr["出库ID"].ToString() });
                if (tb != null && tb.Rows.Count > 0)
                {
                    // setDefalutStock(tb.Rows[0], r);

                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + inware_name + "】  " + "出库单");
                    pp.Add("FangXiang", "出库库房：  【" + inware_name + "】");
                    pp.Add("Time", "时间：" + tb.Rows[0][4].ToString());
                    pp.Add("LeiBie", "类别：出库单");
                    pp.Add("BianHao", "单据号：" + tb.Rows[0][6].ToString());
                    //pp.Add("HeJi", "合计");
                    pp.Add("JinE1", tb.Rows[0][7].ToString());
                    //pp.Add("JinE2", tb.Rows[0][10].ToString());
                    pp.Add("Name1", "操作员：" + tb.Rows[0][1].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0][2].ToString());
                    pp.Add("Name3", "");
                   // pp.Add("Name4", "出库员：" + tb.Rows[0][3].ToString());
                    pp.Add("HeJi", "备注：" + tb.Rows[0]["MEMO"].ToString());
                    pp.Add("ID", dr["出库ID"].ToString());
                    app.LoadPlug("RepEdit.RepView", new object[] { "CLEQDBDYCS001", pp, false }, false);
                }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }


        }

      






 





  

       
        

       
    }
}
