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
    public partial class EQIn_Add : Form
    {
        Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        IAppContent app;
        string str1 = "2";
        string str2 = "0";
        public EQIn_Add(string inware_id,string inware_name,IAppContent app)
        {  
            this.app = app;
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public EQIn_Add(Dictionary<string, ObjItem> dr, int isAdd,IAppContent app)
        {
             this.app = app;
            this.isAdd = isAdd;
            this.dr = dr;
            InitializeComponent();
        }
        private TvList dwList;
        public EQIn Main;
        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["设备ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【设备】！");
                this.dataGView1.setFocus(e.Row.Index, "设备");
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
            if (!WJs.IsNum(e.Data["发票单价"].ToString()) || e.Data["发票单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【发票单价】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "发票单价");
                return;
            }
            if (!WJs.IsNum(e.Data["运杂费"].ToString()) || e.Data["运杂费"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【运杂费】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "运杂费");
                return;
            }
            if (e.Data["生产日期"].ToDateTime() > DateTime.Now)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【生产日期】不能大于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "生产日期");
                return;
            }
            if (e.Data["生产日期"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("请设置第" + (e.Row.Index + 1) + "行【生产日期】，且不能大于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "生产日期");
                return;
            }
            if (e.Data["有效期"].ToDateTime() < DateTime.Now)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【有效期】不能小于当前日期！");
                this.dataGView1.setFocus(e.Row.Index, "有效期");
                return;
            }
           
            if (!e.Data["备注"].IsNull && e.Data["备注"].ToString().Length > 500)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【备注】最多只允许500个字符！");
                this.dataGView1.setFocus(e.Row.Index, "备注");
                return;
            }
            if (!e.Data["条形码"].IsNull && e.Data["条形码"].ToString().Length >100)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【条形码】最多只允许100个字符！");
                this.dataGView1.setFocus(e.Row.Index, "条形码");
                return;
            }
            if (!e.Data["生产厂家"].IsNull && e.Data["生产厂家"].ToString().Length > 100)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【生产厂家】最多只允许100个字符！");
                this.dataGView1.setFocus(e.Row.Index, "生产厂家");
                return;
            }
        }

      
        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //判断是否选择库房
            if (this.InWare_selTextInpt.Value ==null)
            {
                WJs.alert("请选择入库库房！");
                InWare_selTextInpt.Focus();
                return;
            }
            //判断是否选择入库方式
            if (this.InWay_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库方式！");
                InWay_selTextInpt.Focus();
                return;
            }

            Dictionary<string, object> de = new Dictionary<string, object>();
            de["生产日期"] = WJs.getDate(His.his.WebDate);
                      
            
            de["数量"] = 0;
            de["发票单价"] = "0";
            de["运杂费"] = "0";
            de["成本单价"] = "0";
            de["成本金额"] = "0";
            de["有效期"] = WJs.getDate(His.his.WebDate).AddYears(1);


            string wareid = this.InWare_selTextInpt.Value;
            string ifall = LData.Es("EQWareIfall", null, new object[] { wareid });
            //string wz_s = "物资";
           // string wzid_s="";
            if (ifall.Equals("1"))
            {
               // this.InWZ_selTextInpt.Sql = "GetInWZ0";
                dataGView1.addSql("GetInEQ0", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
                //this.InWZ_selTextInpt.SelParam = His.his.Choscode + "|" + wareid + "|{key}|{key}";
               // dataGView1.addSql("",wz_s,wzid_s,
            }
            else
            {
               // this.InWZ_selTextInpt.Sql = "GetInWZ";
                dataGView1.addSql("GetInEQ", "设备", "", His.his.Choscode + "|" + wareid + "|{key}|{key}|{key}|{key}");
              
            }
          
          // this.dataGView1.addSql("GetSupply", "生产厂家", "", His.his.Choscode + "|{key}|{key}|{key}|{key}");
           // TvList.newBind().SetCacheKey("XmDw").Load("GetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);

          // this.dataGView1.CellEndEdit += new DataGridViewCellEventHandler(dataGView1_CellEndEdit);
           
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.CurrentRow.Cells[eq.Index].ReadOnly = false;


        }

       

        
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)//还有些问题
        {
            if (this.dataGView1.ReadOnly) return;
           
            Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);

            //if (e.ColumnIndex == supply.Index)
            //{
            //    try
            //    {
            //        if (this.dataGView1.CurrentRow.Cells[supply.Index].Value != null)
            //        {
            //            this.dataGView1.CurrentRow.Cells[supplyid.Index].Value = data["生产厂家"].ToString();
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}



            if ((e.ColumnIndex == yzf.Index || e.ColumnIndex == num.Index || e.ColumnIndex == lsprice.Index) && e.RowIndex > -1)
            {
                decimal dec;
                //if(!decimal.TryParse( this.dataGView1.CurrentRow.Cells[yzf.Index].Value.ToString(),out dec))
                //{
                //    WJs.alert("请输入大于零的整数或者小数");
                //    this.dataGView1.CurrentRow.Cells[yzf.Index].Value = 0;
                //    return ;
                //}

             
                int it1= Convert.ToInt32(str1);
 ////////////////////////////////////////////////////////////////////////////             
               //this.dataGView1.CurrentRow.Cells[yzf.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[yzf.Index].Value), it1);
               
               // this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[lsprice.Index].Value), it1);
              
               // if (str2 == "1")
               //{
               //    this.dataGView1.CurrentRow.Cells[yzf.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[yzf.Index].Value);
               //    this.dataGView1.CurrentRow.Cells[lsprice.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[lsprice.Index].Value);
               //}
                 try
                {

                    this.dataGView1.jsBds("发票金额=数量*发票单价");
                    this.dataGView1.jsBds("成本单价=发票单价+运杂费/数量");
                    this.dataGView1.jsBds("成本金额=成本单价*数量");
                    //this.dataGView1.CurrentRow.Cells[rate.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[rate.Index].Value), it1);
                    //this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value), it1);
                    //this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = Math.Round(Convert.ToDecimal(this.dataGView1.CurrentRow.Cells[cbdj.Index].Value), it1);
                  
                    ////统计金额
                    this.totalmoney_yTextBox.Text =  Math.Round(Convert.ToDecimal(dataGView1.Sum("成本金额")),it1).ToString();
                    this.lstotalmoney_yTextBox.Text = Math.Round(Convert.ToDecimal(dataGView1.Sum("发票金额")), it1).ToString();
                    this.yTextBox_YzfMoney.Text = Math.Round(Convert.ToDecimal(dataGView1.Sum("运杂费")), it1).ToString();

                    //if (str2 == "1")
                    //{
                    //    this.dataGView1.CurrentRow.Cells[rate.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[rate.Index].Value);
                    //    this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[lsmoney.Index].Value);
                    //    this.dataGView1.CurrentRow.Cells[cbdj.Index].Value = String.Format("{0:N" + str1 + "}", this.dataGView1.CurrentRow.Cells[cbdj.Index].Value);
                    //    this.totalmoney_yTextBox.Text = String.Format("{0:N" + str1 + "}", dataGView1.Sum("成本金额").ToString());
                    //    this.lstotalmoney_yTextBox.Text = String.Format("{0:N" + str1 + "}", dataGView1.Sum("发票金额").ToString());
                    //    this.yTextBox_YzfMoney.Text = String.Format("{0:N" + str1 + "}", dataGView1.Sum("运杂费").ToString());
                    //}

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
                WJs.alert("请选择入库方式！");
                this.InWay_selTextInpt.Focus();
                return false;
            }
            if (this.InWare_selTextInpt.Value==null)
            {
                WJs.alert("请选择入库库房！");
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
                    WJs.alert("请添加入库设备");
                    return;
                }
                if (this.totalmoney_yTextBox.Text.Trim().Length > 11)
                {
                    WJs.alert("总金额太大(不能超过100000.0000)！请减少该批次设备");
                    return;
                }
                if (this.lstotalmoney_yTextBox.Text.Trim().Length > 11)
                {
                    WJs.alert("发票金额太大(不能超过100000.0000)！请减少该批次设备");
                    return;
                }
                if (this.yTextBox_YzfMoney.Text.Trim().Length > 11)
                {
                    WJs.alert("运杂费金额太大(不能超过100000.0000)！请减少该批次设备");
                    return;
                }
                string str = this.dataGView1.GetDataToXml();
                if (str != null)
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                    ac.Sql = "RuKuDanSave";
                    //ac.Add("过单日期", this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    ac.Add("IOID", this.InWay_selTextInpt.Value);
                    ac.Add("WARECODE", this.InWare_selTextInpt.Value);

                    ac.Add("SHDH", this.SHDH_yTextBox.Text);
                    ac.Add("SUPPLYID", this.gys_selTextInpt.Value);
                    ac.Add("SUPPLYNAME", this.gys_selTextInpt.Text);
                    ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                    ac.Add("INVOICEDATE", this.dateTimePicker1.Value);
                    ac.Add("INVOICECODE", this.fpcode_yTextBox.Text);
                    ac.Add("INVOICEMONEY", this.lstotalmoney_yTextBox.Text);
                    ac.Add("OTHERMONEY", this.yTextBox_YzfMoney.Text);
                    ac.Add("INDATE", this.dateTimePicker2.Value);
                    ac.Add("BUYUSER", this.yTextBox_CGpeople.Text);

                    ac.Add("MEMO", this.memo_yTextBox.Text);
                   
                    ac.Add("USERID", His.his.UserId);
                    ac.Add("USERNAME", His.his.UserName);
                    ac.Add("RECDATE", DateTime.Now);
                    ac.Add("CHOSCODE", His.his.Choscode);
                   
                    //if (this.plan_selTextInpt.Value != null)
                    //{
                    //    //该入库单是从采购计划中获取
                    //    ac.Add("PLANNO", this.plan_selTextInpt.Text);
                    //}
                    ac.Add("DanJuMx", str);
                    if (isAdd == 2)
                    {
                        ac.Add("INID", dr["入库ID"].ToString());
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
            //this.InWare_selTextInpt.Text = "";
           // this.InWare_selTextInpt.Value = "";
            this.yTextBox_CGpeople.Text = "";
            this.fpcode_yTextBox.Text = "";
            this.SHDH_yTextBox.Text = "";
            this.memo_yTextBox.Text = "";
            this.gys_selTextInpt.Text = "";
            this.gys_selTextInpt.Value = "";
            this.totalmoney_yTextBox.Text = "0.00";
            this.lstotalmoney_yTextBox.Text = "0.00";
            this.yTextBox_YzfMoney.Text = "0.00";

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
                    if (WJs.confirmFb("您确定要删除选择的入库设备信息吗？"))
                    {

                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                        this.dataGView1.jsBds("发票金额=数量*发票单价");
                        this.dataGView1.jsBds("成本单价=发票单价+运杂费/数量");
                        this.dataGView1.jsBds("成本金额=成本单价*数量");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView1.Sum("成本金额").ToString("0.00");
                        this.lstotalmoney_yTextBox.Text = dataGView1.Sum("发票金额").ToString("0.00");
                        this.yTextBox_YzfMoney.Text = dataGView1.Sum("运杂费").ToString("0.00");
                        if (!doc["流水号"].IsNull && !doc["流水号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                            ac.Sql = "RuKuDanEQdelete";
                            ac.Add("DETAILNO", doc["流水号"].ToString());
                            //ac.Add("WZID", doc["物资ID"].ToString());
                            //获取入库ID
                            string inwareid = LData.Es("EQGetInWareId", null, new object[] { doc["流水号"].ToString() });

                            ac.Add("INID", inwareid);


                            ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);
                            ac.Add("INVOICEMONEY", this.lstotalmoney_yTextBox.Text);
                            ac.Add("OTHERMONEY", this.yTextBox_YzfMoney.Text);
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

       

        //void plan_selTextInpt_TextChanged(object sender, EventArgs e)
        //{
        //    this.button1.Enabled = true;
        //}

      
        private void button1_Click(object sender, EventArgs e)
        {
           
        
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
            this.InWay_selTextInpt.Sql = "EQGetInWareWay";
            this.InWay_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            this.gys_selTextInpt.Sql = "EQGetGYS";
            this.gys_selTextInpt.SelParam = "{key}|{key}|{key}|{key}";
            this.dataGView1.Url = "EQInDetailSearch";
            //TvList.newBind().SetCacheKey("XmDw").Load("EQGetSupply", new object[] { His.his.Choscode }).Bind(this.supply);
            this.dataGView1.addSql("EQGetSupply", "生产厂家", "", His.his.Choscode + "|" +  "|{key}|{key}|{key}|{key}");
 
            TvList.newBind().SetCacheKey("XmDw1").Load("EQIn_DanWeiBianMa", null).Bind(this.Column3);

            
            //dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
            //dwList.Bind(this.unit);
            if (isAdd !=1 )
            {
                this.InWay_selTextInpt.Text = LData.Exe("EQGetInWareWayName", null, new object[] { His.his.Choscode, dr["入库方式ID"].ToString() });
                this.InWare_selTextInpt.Text = LData.Exe("EQGetInWareName", null, new object[] { His.his.Choscode, dr["入库库房编码"].ToString() });
                this.InWay_selTextInpt.Value = dr["入库方式ID"].ToString();
                this.InWare_selTextInpt.Value = dr["入库库房编码"].ToString();
                this.InWare_selTextInpt.Enabled=false;
                this.SHDH_yTextBox.Text = dr["随货单号"].ToString();
                this.fpcode_yTextBox.Text = dr["发票号码"].ToString();
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();
                this.lstotalmoney_yTextBox.Text = dr["发票金额"].ToString();
                this.yTextBox_YzfMoney.Text = dr["运杂费金额"].ToString();
                if (!dr["发票日期"].IsNull)
                {
                    this.dateTimePicker1.Value = dr["发票日期"].ToDateTime();
                }
                if (!dr["采购人"].IsNull)
                {
                    this.yTextBox_CGpeople.Text = dr["采购人"].ToString();
                }
                //dwList = TvList.newBind().SetCacheKey("XmDw").Load("GetWZUnit2", new object[] { His.his.Choscode });
                //dwList.Bind(this.unit);
                this.gys_selTextInpt.Text = dr["供货商名称"].ToString();
                this.gys_selTextInpt.Value = dr["供货商ID"].ToString();
                this.memo_yTextBox.Text = dr["备注"].ToString();
                this.dataGView1.Columns[eq.Index].ReadOnly = true;
                this.dataGView1.reLoad(new object[] { dr["入库ID"].ToString(), His.his.Choscode });
                this.cancel_toolStripButton.Enabled = false;
                this.fromplan_toolStripButton.Enabled = false;
                if (isAdd == 3)
                {
                    this.toolStrip1.Enabled = true;
                    this.add_toolStripButton.Visible = false;
                    this.fromplan_toolStripButton.Visible = false;
                    this.save_toolStripButton.Visible = false;
                    this.DeleButton.Visible = false;
                    this.cancel_toolStripButton.Visible = false;
                    this.toolStripButton_Pass.Visible = false;
                    this.toolStripButton_Down.Visible = false;
                 

                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;
                    this.gys_selTextInpt.Enabled = false;
                    this.fpcode_yTextBox.ReadOnly = true;
                    this.dateTimePicker1.Enabled = false;
                    this.SHDH_yTextBox.ReadOnly = true;
                    this.memo_yTextBox.ReadOnly = true;
                }
                if (isAdd == 4)
                {
                    this.toolStrip1.Enabled = true;
                    this.add_toolStripButton.Visible = false;
                    this.fromplan_toolStripButton.Visible = false;
                    this.save_toolStripButton.Visible = false;
                    this.DeleButton.Visible = false;
                    this.cancel_toolStripButton.Visible = false;
                    this.toolStripButton_Pass.Visible = true;
                    this.toolStripButton_Down.Visible = true;
                    this.dataGView1.ReadOnly = true;
                    this.InWare_selTextInpt.Enabled = false;
                    this.InWay_selTextInpt.Enabled = false;
                    this.gys_selTextInpt.Enabled = false;
                    this.fpcode_yTextBox.ReadOnly = true;
                    this.dateTimePicker1.Enabled = false;
                    this.SHDH_yTextBox.ReadOnly = true;
                    this.memo_yTextBox.ReadOnly = true;
                }
               
              
                
                 this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                 //BindUnit();
               
            }

          
            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
       
        }

        private void fromplan_toolStripButton_Click(object sender, EventArgs e)
        {
           
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null)
            {
               

                this.KeyPreview = true;
                //判断是否选择库房
                if (this.InWare_selTextInpt.Value == null)
                {
                    WJs.alert("请选择入库库房！");
                    InWare_selTextInpt.Focus();
                    return;
                }
                //判断是否选择入库方式
                if (this.InWay_selTextInpt.Value == null)
                {
                    WJs.alert("请选择入库方式！");
                    InWay_selTextInpt.Focus();
                    return;
                }
                
         
                this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                //  this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                this.productdate.DataPropertyName = null;
                //this.validate_temp.DataPropertyName = null;
                this.validate.DataPropertyName = null;
                this.dataGView1.Url = "EQPurchasePlanDetailSearch_EQIn";
                this.dataGView1.reLoad(new object[] { inware_id,His.his.Choscode });


                //BindUnit2();
                if (this.dataGView1.RowCount == 0)
                {
                    WJs.alert("该采购计划没有明细数据，不能生成入库单");
                    return;
                }
                else
                {

                    this.cancel_toolStripButton.Visible = false;
                }
                this.dataGView1.jsBds("发票金额=数量*发票单价");
                this.dataGView1.jsBds("成本单价=发票单价+运杂费/数量");
                this.dataGView1.jsBds("成本金额=成本单价*数量");
                ////统计金额
                this.totalmoney_yTextBox.Text = dataGView1.Sum("成本金额").ToString();
                this.lstotalmoney_yTextBox.Text = dataGView1.Sum("发票金额").ToString();
                this.yTextBox_YzfMoney.Text = dataGView1.Sum("运杂费").ToString();

                //this.plan_selTextInpt.Enabled = false;
                this.dataGView1.Columns[eq.Index].ReadOnly = true;
                this.fromplan_toolStripButton.Enabled = false;
                //this.cancel_toolStripButton.Enabled = false;
                //this.button1.Enabled = false;



                this.add_toolStripButton.Enabled = false;

                foreach (DataGridViewRow r in this.dataGView1.Rows)
                {
                    Dictionary<string, ObjItem> data = this.dataGView1.getRowData(r);
                    if (r != null)
                    {
                        r.Cells[productdate.Index].Value = DateTime.Now;
                        r.Cells[validate.Index].Value = WJs.getDate(His.his.WebDate).AddYears(1);
                        //if (!r.Cells[validate_temp.Index].Value.ToString().Equals(""))
                        //{
                        //    r.Cells[validate.Index].Value = Convert.ToDateTime(r.Cells[productdate.Index].Value).AddMonths(Convert.ToInt32(r.Cells[validate_temp.Index].Value));
                        //}
                        //else
                        //{
                        //    Convert.ToDateTime(r.Cells[productdate.Index].Value).AddMonths(12);
                        //}

                    }
                }
                
            }
            else
            {
                WJs.alert("该入库单已经有明细数据，不能从采购计划中生成明细信息！");
            }
       
        }
        void ac0_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            // WJs.alert(e.Msg.Msg);

        }



        void ac3_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {

            //更新入库细表的库存流水号
            string flowno = e.Msg.Msg.Split('+')[0];
            string DETAILNO = e.Msg.Msg.Split('+')[1];
            ActionLoad ac2 = new ActionLoad();
            ac2.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
            ac2.Sql = "UpdateEQInDetailInfo";
            ac2.Add("DETAILNO", DETAILNO);
            ac2.Add("STOCKFLOWNO", flowno);
            ac2.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
            ac2.Post();
        }
        private void toolStripButton_Pass_Click(object sender, EventArgs e)
        {
            DataTable datatable = LData.LoadDataTable("EQInDetailSearch", new object[] { dr["入库ID"].ToString(), His.his.Choscode });
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
                        ac0.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                        ac0.Add("EQID", r["EQID"].ToString());
                        DataTable stock_table = LData.LoadDataTable("EQIn_GetSTOCKTable", new object[] { His.his.Choscode, r["EQID"].ToString(), dr["入库库房编码"].ToString() });
                        if (stock_table != null)
                        {
                            //库存总表中存在该库房对应的这种物资，只需更新库存量
                            before_num = Convert.ToDecimal(stock_table.Rows[0]["NUM"]);

                            after_num = before_num + num;
                            ac0.Sql = "UpdateEQStock";

                            ac0.Add("STOCKID", stock_table.Rows[0]["STOCKID"]);
                            ac0.Add("NUM", after_num);


                        }
                        else
                        {
                            //新增库存总表记录
                            ac0.Sql = "SaveEQStock";
                            ac0.Add("WARECODE", dr["入库库房编码"].ToString());
                            ac0.Add("NUM", num);
                            ac0.Add("UNITCODE", r["UNITCODE"]);

                            ac0.Add("CHOSCODE", His.his.Choscode);
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
                        ac1.Add("EQID", r["EQID"].ToString());
                        ac1.Add("INID", r["INID"].ToString());
                        ac1.Add("WARECODE", dr["入库库房编码"].ToString());
                        string stockid_s;
                        // string befornum = "0";
                        if (stock_table != null)
                        {
                            stockid_s = stock_table.Rows[0]["STOCKID"].ToString();

                        }
                        else
                        {
                            stockid_s = LData.Es("EQGetSTOCKId", null, new object[] { His.his.Choscode, r["EQID"].ToString(), dr["入库库房编码"].ToString() });
                        }
                        ac1.Add("STOCKID", stockid_s);
                        ac1.Add("BEFORENUM", before_num);
                        ac1.Add("NUM", num);

                        ac1.Add("UNITCODE", r["UNITCODE"]);
                        ac1.Add("OUTNUM", 0);
                        ac1.Add("CARDNUM",0);
                        ac1.Add("GG", r["GG"]);
                        ac1.Add("XH", r["XH"]);
                        ac1.Add("CD", r["CD"]);
                        ac1.Add("PRICE", r["PRICE"]);
                        ac1.Add("MONEY", r["MONEY"]);
                        ac1.Add("OTHERMONEY", r["OTHERMONEY"]);

                        ac1.Add("TOTALPRICE", r["TOTALPRICE"]);
                        ac1.Add("TOTALMONEY", r["TOTALMONEY"]);
                        ac1.Add("SUPPLYID", r["SUPPLYID"]);
                        ac1.Add("SUPPLYNAME", r["SUPPLYNAME"]);
                        ac1.Add("PRODUCTDATE", Convert.ToDateTime(r["PRODUCTDATE"]));
                        ac1.Add("VALIDDATE", Convert.ToDateTime(r["VALIDDATE"]));
                        ac1.Add("MEMO", r["MEMO"]);
                        ac1.Add("TXM", r["TXM"]);
                        

                        ac1.Add("RECIPECODE", dr["单据号"].ToString());
                        ac1.Add("SHDH", dr["随货单号"].ToString());
                        ac1.Add("GHSUPPLYID", dr["供货商ID"].ToString());
                        ac1.Add("GHSUPPLYNAME", dr["供货商名称"].ToString());
                        ac1.Add("INDATE", DateTime.Now);
                        ac1.Add("CHOSCODE", His.his.Choscode);


                        ac1.Add("DETAILNO", r["DETAILNO"]);//用于更新入库细表

                        ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac3_ServiceLoad);
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
                //if (dr["采购计划ID"] != null)
                //{
                //    ActionLoad ac3 = new ActionLoad();
                //    ac3.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                //    ac3.Sql = "UpdateWZPlanDan";
                //    ac3.Add("PLANID", dr["采购计划流水号"].ToString());
                //    ac3.Add("STATUS", 6);
                //    ac3.Add("SHINDATE", DateTime.Now);
                //    ac3.Add("SHINUSERID", His.his.UserId);
                //    ac3.Add("SHINUSERNAME", His.his.UserName);
                //    ac3.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac0_ServiceLoad);
                //    ac3.Post();

                //}

                ActionLoad ac = new ActionLoad();
                ac .Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                ac.Sql = "RuKuDanUpdate";
                ac.Add("SHINDATE", DateTime.Now);
                ac.Add("STATUS", 6);
                ac.Add("INID", dr["入库ID"].ToString());
                ac.Add("SHINUSERID", His.his.UserId);
                ac.Add("SHINUSERNAME", His.his.UserName);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_SHTG);
                ac.Post();
     
            }

        }

        private void toolStripButton_Down_Click(object sender, EventArgs e)
        {
            ActionLoad ac = new ActionLoad();
            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
            ac.Sql = "RuKuDanUpdate";
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("STATUS", 2);
            ac.Add("INID", dr["入库ID"].ToString());
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

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label14.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
      
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (inware_id == null)
            {
                ActionLoad ld = ActionLoad.Conn();
                DataTable tb = ld.Find("EQInMainDaYinQuery", new object[] { dr["入库ID"].ToString() });


                if (tb != null && tb.Rows.Count > 0)
                {
                    // setDefalutStock(tb.Rows[0], r);
                    string KuFangName = LData.Exe("EQGetInWareName", "LKEQ", new object[] { His.his.Choscode, tb.Rows[0]["WARECODE"].ToString() });
                    string RuKuFangShi = LData.Exe("EQGetInWareWayName", "LKEQ", new object[] { His.his.Choscode, tb.Rows[0]["IOID"].ToString() });
                    Dictionary<string, object> pp = new Dictionary<string, object>();
                    pp.Add("BiaoTi", tb.Rows[0][0].ToString() + "  【" + KuFangName + "】  " + "入库单");
                    pp.Add("FangXiang", "入库到  【" + KuFangName + "】");
                    pp.Add("Time", "时间：" + tb.Rows[0]["RECDATE"].ToString());
                    pp.Add("LeiBie", "类别：" + RuKuFangShi);
                    pp.Add("BianHao", "单据号：" + tb.Rows[0]["RECIPECODE"].ToString());
                    pp.Add("HeJi", "供应商：" + tb.Rows[0]["SUPPLYNAME"].ToString());
                    pp.Add("JinE1", tb.Rows[0]["TOTALMONEY"].ToString());
                  //  pp.Add("JinE2", tb.Rows[0]["LSTOTALMONEY"].ToString());
                    pp.Add("Name1", "操作员：" + tb.Rows[0]["USERNAME"].ToString());
                    pp.Add("Name2", "审核员：" + tb.Rows[0]["SHUSERNAME"].ToString());
                    pp.Add("Name3", "备注：" + tb.Rows[0]["MEMO"].ToString());
                   // pp.Add("Name4", "入库科室：" + tb.Rows[0]["DEPTNAME"].ToString());
                    pp.Add("ID", dr["入库ID"].ToString());
                    app.LoadPlug("RepEdit.RepView", new object[] { "CLEQRKDYCS001", pp, false }, false);
                }
            }
            else
            {
                MessageBox.Show("请先保存数据！");
            }


        }
 





  

       
        

       
    }
}
