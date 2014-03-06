using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtWinContrl.com.datagrid;
using YiTian.db;
using YtUtil.tool;
using YtClient;

namespace EQWareManag.form
{
    public partial class EQPanDian_DetailScan : Form
    {
        public EQPanDian_DetailScan()
        {
            InitializeComponent();
        }
        DataRow r;
       
        string PDID1="";
        string PDNAME = "";
        private bool isAdd;// 保存（true），修改（false）标志位
        private bool KClargeZero=false; 
        private bool isScan=false;  //查看标志位
        private string wareName = "";
        private string wareValue = "";  
        //DataTable dt;
        string [] sr;
        public EQPanDian_DetailScan(string[] sr,string wareval,string _ware, bool _isAdd,bool _KClargeZero)
        {
            isAdd = _isAdd;
            wareName = _ware;
            wareValue = wareval;
            this.sr = sr;
            this.KClargeZero = _KClargeZero;
            InitializeComponent();
        }
        public EQPanDian_DetailScan(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public EQPanDian_DetailScan(DataRow r)
        {
            isScan = true;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public EQPanDian Main2;
        public EQPanDian_Select EQSlect;
        // private TvList dwList;

       
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发（保存）
        {
                    
            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //提交成功返回后触发
        {


            string[] t = e.Msg.Msg.Split('|');
            PDID1 = t[1];
            WJs.alert(t[0]);
            //WZPDZ.ReLoadData();
            //Main.toolStripButton4_Click(null, null);
            EQSlect.callback(this.selTextInpt_Ware.Value);
            isSc = true;
            this.Close();

        }

        private void button1_Click_2(object sender, EventArgs e) 
        {

           
          
            
           
         
            this.dataGView1.ClearData();



            if (!isAdd)
            {

                if (r != null)
                {

                    this.dataGView1.Url = "EQPD_EditAndScanStockDetail";
                    this.dataGView1.reLoad(new object[] { r["PDID"].ToString(),wareValue, His.his.Choscode });
                }

            }
            else
            {
                SqlStr sql = SqlStr.newSql();
                sql.Add(" and (e.KINDNAME ='KINDNAME' ");
                for (int i = 0; i < this.sr.Length; i++)
                {
                    if (sr[i].Length > 0)
                    {
                        sql.Add("or e.KINDNAME = ?", sr[i]);

                        int j = 1;

                        if (j < 4)
                        {
                            PDNAME = PDNAME + sr[i];
                        }
                        else
                        {
                            PDNAME = PDNAME + "······";
                        }
                    }
                }

                sql.Add(" ) ");
                if (this.KClargeZero)
                {
                    sql.Add("and a.NUM-a.OUTNUM>0");
                }
                PDNAME = PDNAME + DateTime.Now.ToString("yyyy年MM月dd日");
                this.yTextBox_Name.Text = PDNAME;
                this.dataGView1.Url = "EQPD_NewStockDetail";
                this.dataGView1.reLoad(new object[] { wareValue, His.his.Choscode }, sql);


            }

     
          
                if (this.dataGView1.RowCount > 0)
                {
                    for (int i = 0; i < this.dataGView1.RowCount; i++)
                    {
                        if (this.dataGView1["Column18", i].Value != null && this.dataGView1["Column26", i].Value != null)
                        {
                            this.dataGView1["Column12", i].Value = int.Parse(this.dataGView1["Column18", i].Value.ToString()) - int.Parse(this.dataGView1["Column26", i].Value.ToString());
                        }
                        if (this.dataGView1["Column2", i].Value ==null)
                        {
                            this.dataGView1["Column2", i].Value = this.dataGView1["Column12", i].Value;
                        }

                    }
                }

        } //点击查询按钮触发事件

      
     

        

        void ac_ServiceDetailDeleteLoad(object sender, YtClient.data.events.LoadEvent e) //提交成功返回后触发(删除）
        {
            WJs.alert(e.Msg.Msg);
            button1_Click_2(null, null);  //查询数据
        }


        private void toolStripButton_Del_Click(object sender, EventArgs e)
        {


            DataRow row = this.dataGView1.GetRowData();

            if (row != null)
            {
                if (WJs.confirmFb("确定要删除选择的盘点详细信息吗？"))
                {

                    this.dataGView1.SelectedRows[0].Cells["Column9"].Value = null;
                    this.dataGView1.SelectedRows[0].Cells["Column2"].Value = this.dataGView1.SelectedRows[0].Cells["Column12"].Value;

                    if (!isAdd && r["PDID"] != null)
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                        ac.Sql = "DelEQPanDianDetailInfo";
                        ac.Add("PDID", r["PDID"].ToString());
                        ac.Add("ROWNO", Convert.ToDecimal(row["ROWNO"]));
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceDetailDeleteLoad);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                        ac.Post();
                    }

                }
            }
            else
            {
                WJs.alert("请选择要删除的盘点信息");
            }
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {

            if (this.yTextBox_Name.Text.Trim().Length <= 0)
            {
                WJs.alert("盘点名字不能为空！");
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            if (!isAdd)
            {
                //修改盘点主表信息

                 if (r != null)
                {
                    ac.Add("PDID", r["PDID"].ToString());
                }


                ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                ac.Sql = "UpdataWZPanDianInfo";
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("WARECODE", this.selTextInpt_Ware.Value);
                ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);




            }
            else
            {
                //保存盘点主表信息
                ac.Add("PDID", null);
                ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                ac.Sql = "SaveEQPanDianInfo";
                ac.Add("WARECODE", this.selTextInpt_Ware.Value);
                ac.Add("PDNAME", this.yTextBox_Name.Text);
             
                ac.Add("MEMO", this.yTextBox_Rec.Text.ToString());
                ac.Add("USERID", His.his.UserId);
                ac.Add("USERNAME", His.his.UserName);
             
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("STATUS", "1");


            }


             string str = this.dataGView1.GetDataToXml();
             if (str != null)
             {
                 //保存盘点细表信息
                 if (this.dataGView1.RowCount > 0)
                 {


                     int cout = 1;
                     for (int i = 0; i < this.dataGView1.RowCount; i++)
                     {


                         if (this.dataGView1["Column9", i].Value != null && this.dataGView1["Column12", i].Value.ToString().Length > 0)
                         {
                             ac.Add("EQID" + cout, this.dataGView1["Column16", i].Value);
                             ac.Add("STOCKFLOWNO" + cout, this.dataGView1["Column15", i].Value);
                             ac.Add("FACTNUM" + cout, this.dataGView1["Column2", i].Value);
                             ac.Add("YKNUM" + cout, this.dataGView1["Column9", i].Value);
                             cout++;
                         }
                     }
                     ac.Add("MyCount", cout);

                     ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                     ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                     ac.Post();
                 }
             }
             else
             {
                 WJs.alert("详细盘点数据不能为空！");
                 return;
             }
               
               
             
          

        }

        private void toolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            if (!this.isScan)
            {
                if (WJs.confirmFb("是否确定不保存退出？"))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();

            }
        }

        private void EQPanDian_DetailScan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TvList.newBind().Load("Chang_LSDanWeiBianMa",null).Bind(this.Column24);
       
            //this.dataGView1.Url = "WZPD_ScanStockDetail";
            //this.dataGView1.reLoad(new object[] { r["WARECODE"].ToString(), His.his.Choscode });
            //this.ytComboBox_PDWareNum.Enabled = false;
            //this.ytComboBox_PDWareNum.Text = wareName;
            //this.ytComboBox_PDWareNum.Value = wareValue;
            this.selTextInpt_Ware.Value = wareValue;
            this.selTextInpt_Ware.Text = wareName;
            this.selTextInpt_Ware.Enabled = false;
           // this.ytComboBox_PDWareNum.Text = LData.Es("EQPDFind_WARENAME", null, new object[] { His.his.Choscode, this.ytComboBox_PDWareNum.Value });

          //  TvList.newBind().SetCacheKey("pdkfbm").Load("EQPDFind_WARE", new object[] { His.his.Choscode }).Bind(this.ytComboBox_PDWareNum);
           
            if (!this.isAdd)
            {

               // this.ytComboBox_PDWareNum.Value = r["WARECODE"].ToString();
                this.selTextInpt_Ware.Value = r["WARECODE"].ToString();
                wareValue = r["WARECODE"].ToString();
                this.yTextBox_Name.Text=r["PDNAME"].ToString();
                this.selTextInpt_Ware.Text = LData.Es("EQPDFind_WARENAME", null, new object[] { His.his.Choscode, wareValue });
                if (r["MEMO"] != null)
                {
                    this.yTextBox_Rec.Text = r["MEMO"].ToString();
                }
               
              
              
            }
            if (this.isScan)
            {
                this.toolStripButton_Save.Enabled = false;
                this.toolStripButton_Del.Enabled = false;
                this.selTextInpt_Ware.Enabled = false;
                this.yTextBox_Rec.Enabled = false;
                this.toolStripButton_Cancel.Enabled = false;

              
            }
            button1_Click_2(null, null);  //查询数据
       
        }

        private void dataGView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) //表格中的单元格结束编辑时触发事件
        {


            if (this.dataGView1.CurrentCell.ReadOnly == false && this.dataGView1.CurrentRow.Cells["Column12"].Value != null)
            {
                var a = this.dataGView1.CurrentCell.Value;
                int Count = 0;
                if (a != null)
                {

                    if (int.TryParse(a.ToString().Trim(), out Count) && Count >= 0)
                    {
                        int Value = Count - Convert.ToInt32(this.dataGView1.CurrentRow.Cells["Column12"].Value);
                        //if (Value != 0)
                        //{
                        this.dataGView1.CurrentRow.Cells["Column9"].Value = Value;
                        //}
                        //else
                        //{
                        //    this.dataGView1.CurrentRow.Cells["Column9"].Value = null;
                        //}
                    }
                    else
                    {
                        WJs.alert("输入的实际数量有误，请输入大于零的整数。");
                    }
                }
            }
        }

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.TiaoSu.Text =  this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
       
        }

       

      




      
    }
}

