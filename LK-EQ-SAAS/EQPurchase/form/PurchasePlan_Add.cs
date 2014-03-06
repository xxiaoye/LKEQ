using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using ChSys;
using YtWinContrl.com.datagrid;
using YtClient;
using YtWinContrl.com;
using YtUtil.tool;

namespace EQPurchase.form
{
    public partial class PurchasePlan_Add : Form
    {
        Dictionary<string, ObjItem> dr;
        private int isAdd = 0;
        private string inware_id;
        private string inware_name;
        public PurchasePlan Main;
        public PurchasePlan_Add(string inware_id, string inware_name)
        {
            this.isAdd = 1;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        public PurchasePlan_Add(Dictionary<string, ObjItem> dr, int isAdd, string inware_id, string inware_name)
        {
            this.isAdd = isAdd;
            this.dr = dr;
            this.inware_id = inware_id;
            this.inware_name = inware_name;
            InitializeComponent();
        }
        private TvList dwList;

        private void PurchasePlan_Add_Load(object sender, EventArgs e) //窗体加载
        {
            this.WindowState = FormWindowState.Maximized;
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                
            TvList.newBind().add("其它(临时计划)", "0").add("月度计划", "1").add("季度计划", "2").add("年度计划", "3").Bind(this.ytComboBox_Plan);
            TvList.newBind().Load("EQUnitCodeAskBuy_Add", null).Bind(this.unitcode);
         

            this.InWare_selTextInpt.Text = inware_name;
            this.InWare_selTextInpt.Value = inware_id;
            this.InWare_selTextInpt.Enabled = false;
            this.toolStripButton_Pass.Visible = false;
            this.toolStripButton_No.Visible = false;

            this.dataGView1.Url = "EQPurchasePlanDetailSearch";
           
            if (isAdd != 1)
            {
                this.totalmoney_yTextBox.Text = dr["总金额"].ToString();
                this.ytComboBox_Plan.Value = dr["计划类型"].ToString();
                if (!dr["制定日期"].IsNull)
                {
                    this.dateTimePicker1.Value = dr["制定日期"].ToDateTime();
                }
                this.memo_yTextBox.Text = dr["备注"].ToString();

                this.dataGView1.Columns[eqname.Index].ReadOnly = true;//是否可以再新增数据

                this.dataGView1.reLoad(new object[] { dr["采购计划ID"].ToString(), His.his.Choscode });

                this.cancel_toolStripButton.Enabled = false;
               
                if (isAdd == 3)
                {
                    this.toolStrip1.Enabled = false;
                    this.dataGView1.ReadOnly = true;
                    this.dateTimePicker1.Enabled = false;
                    this.memo_yTextBox.ReadOnly = true;
                }
                if (isAdd == 4)
                {
                    this.add_toolStripButton.Visible = false;
                    this.Get_toolStripButton.Visible = false;
                    this.DeleButton.Visible = false;
                    this.save_toolStripButton.Visible = false;
                    this.toolStripButton_Pass.Visible = true;
                    this.toolStripButton_No.Visible = true;
                    this.cancel_toolStripButton.Visible = false;
                    
                }

                this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
                //BindUnit();
            }
            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
        }

        void dataGView1_RowToXml(RowToXmlEvent e) //将所有数据生产xml类型数据
        {
            if (e.Data["设备ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【设备名称】！");
                this.dataGView1.setFocus(e.Row.Index + 1, "设备名称");
                return;
            }
            if (e.Data["国别"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【国别】！");
                this.dataGView1.setFocus(e.Row.Index, "国别");
                return;
            }
            if (!WJs.IsZs(e.Data["采购数量"].ToString()) || e.Data["采购数量"].ToDouble() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【采购数量】只能输入整数，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
            if (!WJs.MaxNumOver(e.Data["采购数量"].ToString(), "第" + (e.Row.Index + 1) + "行【采购数量】设定值太大，请更改"))
            {
                e.IsValid = false;
                this.dataGView1.setFocus(e.Row.Index, "采购数量");
                return;
            }
            if (!WJs.IsNum(e.Data["采购单价"].ToString()) || e.Data["采购单价"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【采购单价】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "采购单价");
                return;
            }
           
            if (!e.Data["备注"].IsNull && e.Data["备注"].ToString().Length > 500)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行输入的【备注】最多只允许500个字符！");
                this.dataGView1.setFocus(e.Row.Index, "备注");
                return;
            }
        }


        private void add_toolStripButton_Click(object sender, EventArgs e)//新增
        {
            this.KeyPreview = true;
           
            Dictionary<string, object> de = new Dictionary<string, object>();
          
            de["采购数量"] = 0;
            de["采购单价"] = "0.0000";
            de["采购金额"] = "0.0000";
           
            string wareid = this.InWare_selTextInpt.Value;
            string ifall = LData.Es("EQWareIfall", null, new object[] { wareid });

            if (ifall.Equals("1"))
            {

                dataGView1.addSql("EQPurchasePlan_GetInEQ0", "设备名称","", wareid + "|" + His.his.Choscode + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");

            }
            else
            {

                dataGView1.addSql("EQPurchasePlan_GetInEQ", "设备名称", "", wareid + "|" + His.his.Choscode + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");

            }
            dataGView1.addSql("CountryAskBuy_Add", "国别", "", "{key}|{key}|{key}");

            dataGView1.addSql("EQPurchasePlan_GetCompany", "生产商", "", His.his.Choscode + "|{key}|{key}|{key}");
            dataGView1.addSql("EQPurchasePlan_GetCompany1", "供应商", "", His.his.Choscode + "|{key}|{key}|{key}");
            TvList.newBind().Load("EQDict_SingerCode", null).Bind(this.unitcode);
            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged);
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.CurrentRow.Cells[eqname.Index].ReadOnly = false;
            //this.dataGView1.CurrentRow.Cells[rowno.Index].Value = this.dataGView1.RowCount ;
        }
       
        void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)//单元格内容发生改变
        {
            if (this.dataGView1.ReadOnly) return;
            Dictionary<string, ObjItem> data = this.dataGView1.getRowData(this.dataGView1.Rows[e.RowIndex]);
            //string unit_o;
            if (e.ColumnIndex == nownum.Index)
            {
                
                    //BindUnit();
                    //this.dataGView1.CurrentRow.Cells[unitcode.Index].Value = data["单位编码"].ToString();
                    //this.dataGView1.CurrentRow.Cells[eqid.Index].Value = data["设备ID"].ToString();
                    //this.dataGView1.CurrentRow.Cells[gg.Index].Value = data["规格"].ToString();
                    //this.dataGView1.CurrentRow.Cells[xh.Index].Value = data["型号"].ToString();
                    //this.dataGView1.CurrentRow.Cells[txm.Index].Value = data["条形码"].ToString();
                if (data["当前库存数量"].IsNull)
                {
                    this.dataGView1.CurrentRow.Cells[nownum.Index].Value = 0;
                }
                else
                {
                    this.dataGView1.CurrentRow.Cells[nownum.Index].Value = data["当前库存数量"].ToString();
                }

                    
            }

            if ((e.ColumnIndex == unitcode.Index || e.ColumnIndex == num.Index || e.ColumnIndex == price.Index ) && e.RowIndex > -1)
            {


                    this.dataGView1.jsBds("采购金额=采购数量*采购单价");
            
                    this.totalmoney_yTextBox.Text = dataGView1.Sum("采购金额").ToString("0.0000");



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
        private void save_toolStripButton_Click(object sender, EventArgs e)//保存
        {
            if (this.ytComboBox_Plan.Value ==null)
            {
                WJs.alert("请选择计划类型！");
                return;
            }
            if (this.dataGView1.RowCount == 0)
            {
                WJs.alert("请添加采购设备");
                return;
            }
            if (this.totalmoney_yTextBox.Text.Trim().Length>11)
            {
                WJs.alert("总金额太大(不能超过100000.0000)！请减少该批次设备");
                return;
            }
           
            string str = this.dataGView1.GetDataToXml();
            if (str != null)
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                ac.Sql = "PlanDanSave";
                

                ac.Add("WARECODE", this.InWare_selTextInpt.Value);
            
                ac.Add("PLANDATE", DateTime.Now);
              
                ac.Add("MEMO", this.memo_yTextBox.Text);
               
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);
                ac.Add("STATUS", "1");

        
               
                ac.Add("TOTALMONEY", this.totalmoney_yTextBox.Text);

                ac.Add("PLANTYPE", this.ytComboBox_Plan.Value);
                ac.Add("DanJuMx", str);

                if (isAdd == 2)
                {
                    ac.Add("PLANID", dr["采购计划ID"].ToString());

                }
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);

                ac.Post();

            }
        }

        private void cancel_toolStripButton_Click(object sender, EventArgs e)//取消
        {
            if (WJs.confirm("是否放弃保存，暂存数据将清空！"))
            {
                InitInWare();

            }
        }
        void InitInWare()
        {
            this.dataGView1.Rows.Clear();
            this.memo_yTextBox.Text = "";
            this.totalmoney_yTextBox.Text = "0.0000";
            this.ytComboBox_Plan.SelectedIndex=-1;

        }

        private void DeleButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> doc = this.dataGView1.getRowData();
            if (doc != null)
            {
                if (doc["设备名称"].IsNull)
                {
                    this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                }
                else
                {
                    if (WJs.confirmFb("您确定要删除选择的采购设备信息吗？"))
                    {

                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                        this.dataGView1.jsBds("采购金额=采购数量*采购单价");
                        ////统计金额
                        this.totalmoney_yTextBox.Text = dataGView1.Sum("采购金额").ToString("0.0000");
                        
                        if (!doc["行号"].IsNull && !doc["行号"].ToString().Equals(""))
                        {
                            //数据库中已经存在该记录，需要删除数据库中的记录

                            ActionLoad ac = new ActionLoad();
                            ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                            ac.Sql = "PlanDanEQdelete";
                            ac.Add("ROWNO", doc["行号"].ToString());
                            ac.Add("PLANID", doc["采购计划ID"].ToString());
                            
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

        private void Get_toolStripButton_Click(object sender, EventArgs e)//从请购单生成
        {

            PurchasePlan_AddForm AddForm = new PurchasePlan_AddForm(inware_id);
            AddForm.Main = this;
            AddForm.ShowDialog();
        }
        public void GetForm(List<Dictionary<string,ObjItem>>listdata)//获取请购单处理函数
        {
             foreach (Dictionary<string, ObjItem> dict in listdata)
            {
                 if(!dict["选择"].IsNull && bool.Parse(dict["选择"].ToString()))
                 {
               Dictionary<string, object> datarows = new Dictionary<string, object>();
                datarows["设备ID"] = dict["设备ID"];
                datarows["设备名称"] = dict["设备名称"];
                datarows["规格"] = dict["规格"];
                datarows["型号"] = dict["型号"];
                datarows["国别"] = dict["国别"];
                datarows["单位编码"] = dict["单位编码"].ToString();
                datarows["采购数量"] = dict["请购数量"];
                datarows["采购单价"] = dict["预计单价"];
                datarows["条形码"] = dict["条形码"];
                datarows["请购ID"] = dict["请购ID"];
                if (dict["当前库存数量"].IsNull)
                {
                    datarows["当前库存数量"] = 0;
                }
                else
                {
                    datarows["当前库存数量"] = dict["当前库存数量"];
                }
                  int number=this.dataGView1.RowCount;
                for (int i = 0; i < number; i++)
                {
                    if ( number !=0 && datarows["设备ID"].ToString() == this.dataGView1[0, i].Value.ToString())
                    {
                        this.dataGView1[7, i].Value = Convert.ToInt32(datarows["采购数量"].ToString()) + Convert.ToInt32(this.dataGView1[7, i].Value.ToString());

                    }
                    else
                    {
                  
                        this.dataGView1.AddRow(datarows, 0);
                    }
                
                }
                if (number == 0)
                {
                    this.dataGView1.AddRow(datarows, 0);
                }
                    
              
                 }
            
            }
        
        }



        private void toolStripButton_Pass_Click(object sender, EventArgs e)//审核通过
        {
            if (WJs.confirmFb("是否审核通过吗？"))
            {


                ActionLoad ac = new ActionLoad();
                ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                ac.Sql = "PlanDanUpdate_SH";
                ac.Add("SHDATE", DateTime.Now);
                ac.Add("STATUS", 6);
                ac.Add("PLANID", dr["采购计划ID"].ToString());
                ac.Add("SHUSERID", His.his.UserId.ToString());
                ac.Add("SHUSERNAME", His.his.UserName);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_Return);
                ac.Post();



            }
        }

        private void toolStripButton_No_Click(object sender, EventArgs e)//审核不通过
        {
            if (WJs.confirmFb("是否审核不通过吗？"))
            {


                ActionLoad ac = new ActionLoad();
                ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                ac.Sql = "PlanDanUpdate_SH";
                ac.Add("SHDATE", DateTime.Now);
                ac.Add("STATUS", 2);
                ac.Add("PLANID", dr["采购计划ID"].ToString());
                ac.Add("SHUSERID", His.his.UserId.ToString());
                ac.Add("SHUSERNAME", His.his.UserName);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_Return);
                ac.Post();



            }
        }
        void ac_ServiceLoad_Return(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            Main.CallBack();
            
            this.Close();
           
        }

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label12.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
      
        }

       
    }
}
