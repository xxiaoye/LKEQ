using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using EQWareManag.form;
using ChSys;
using YtUtil.tool;
using YtClient;
using YiTian.db;
using YtWinContrl.com.datagrid;


namespace EQWareManag
{
    public partial class EQPanDian : Form, IPlug
    {
        public EQPanDian()
        {
            InitializeComponent();
        }

        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {
           
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        int  rowid=0;
       
        private void toolStripButton8_Click(object sender, EventArgs e)  //工具栏新建按钮点击事件
        {

            if (this.Ware_selTextInpt.Value == null)
            {
                WJs.alert("请选择盘点库房！");
                return;
            
            }
            DataRow r1 = this.dataGView1.GetRowData();


            EQPanDian_Select eps = new EQPanDian_Select(this.Ware_selTextInpt.Value);


            eps.Main = this;
            eps.ShowDialog();




            //if (ks.isSc)
            //{
            //    toolStripButton4_Click(null, null);

            //}
            //Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

        }


        public void toolStripButton4_Click(object sender, EventArgs e)  //工具栏浏览按钮点击事件（原来功能但现在被调用该方法）
        {

           
            this.dataGView2.ClearData();
            button2_Click(null, null);  //窗体查找按钮函数方法
            this.dataGView1.setFocus(rowid, 0);
            //this.dataGView1.Url = "ScanBusinessManag_WZPanDicanZ";
            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            //this.dataGView1.setFocus(rowid, 0);
        }


        public void callback(string a)//子窗体成功保存后，调用改方法
        {


            this.Ware_selTextInpt.Value = a;
            this.comboBox1.Value = "1";
            button2_Click(null, null);
            this.dataGView1.setFocus(rowid, 0);
            //this.dataGView1.Url = "ScanBusinessManag_WZPanDicanZ";
            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            //this.dataGView1.setFocus(rowid, 0);
        }  

        //public  void ReLoadData()
        //{
        //    DataRow r1 = this.dataGView1.GetRowData();
        //    this.dataGView2.Url = "WZPanDianDetailInfo";
        //    this.dataGView2.reLoad(new object[] { r1["PDID"], His.his.Choscode });
        //    this.dataGView2.setFocus(rowid, 0);
        //}

        private void toolStripButton5_Click(object sender, EventArgs e) //工具栏修改按钮点击事件
        {
            DataRow r1 = this.dataGView1.GetRowData();
           
            if (r1!= null)
            {
                if (r1["STATUS"].ToString() == "1")
                {

                    rowid = this.dataGView1.CurrentRow.Index;
                    EQPanDian_DetailScan ks = new EQPanDian_DetailScan(r1, false);
                    ks.Main2 = this;
                    ks.ShowDialog();
                    //if (ks.isSc)
                    //{
                    //    toolStripButton4_Click(null, null);

                    //}
                }
                else 
                {
                    WJs.alert("只能对状态为等待审核的信息进行修改！");
                }
             
                
            }
            else
            {
                WJs.alert("请选择要编辑的盘点信息");
            }
        }

        void ac_ServiceLoad_ShenHe(object sender, YtClient.data.events.LoadEvent e)  //审核不通过成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            this.comboBox1.Value ="3";
            toolStripButton4_Click(null, null);
        }
     
        void ac_ServiceLoad_JieZhuang(object sender, YtClient.data.events.LoadEvent e)  //冲销成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            this.comboBox1.Value = "6";
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)  //保存成功返回触发
        {
            WJs.alert(e.Msg.Msg);
            toolStripButton4_Click(null, null);
        }
        void ac_ServiceDetailDeleteLoad(object sender, YtClient.data.events.LoadEvent e)//删除成功返回触发
        {
            WJs.alert(e.Msg.Msg);
             toolStripButton4_Click(null, null);
        
        }
        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e) //提交失败返回后触发
        {
            WJs.alert(e.Msg.Msg);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)//工具栏查看按钮点击事件
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 == null)

            {
                WJs.alert("请选择一行要查看的数据！");
                return;
            }
            EQPanDian_DetailScan ks = new EQPanDian_DetailScan(r1);
            ks.Main2 = this;
            ks.ShowDialog();
        }

        private void WZPanDian_Load(object sender, EventArgs e)//窗体加载事件
        {
            this.WindowState = FormWindowState.Maximized;
            // toolStripButton4_Click(null, null);


            TvList.newBind().add("全部", "9").add("已冲销", "7").add("已审核", "6").add("审核被拒", "2").add("等待审核", "1").add("作废", "0").Bind(this.comboBox1);
            TvList.newBind().add("全部", "9").add("已冲销", "7").add("已审核", "6").add("审核被拒", "2").add("等待审核", "1").add("作废", "0").Bind(this.Column4);
            this.Ware_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.Ware_selTextInpt.Sql = "LKEQ_GetWare_ye";
            //TvList.newBind().Load("EQPDFind_WARE", new object[] { His.his.Choscode }).Bind(ytComboBox_Status);
            TvList.newBind().Load("EQPDFind_WARE", new object[] { His.his.Choscode }).Bind(this.Column2);
            this.dateTimeDuan2.InitCorl();
            this.dateTimeDuan2.SelectedIndex = -1;

            this.dateTimeDuan2.SelectChange += new EventHandler(dateTimeDuan2_SelectChange);   
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.comboBox1.SelectedIndex = 0;
           // this.ytComboBox_Status.SelectedIndex = -1;
            Ware_selTextInpt.Focus();

        }

        void dateTimeDuan2_SelectChange(object sender, EventArgs e) //dateTimeDuan2选项发生改变时触发该事件
        {
            
            this.dateTimePicker3= this.dateTimeDuan2.Start;
            this.dateTimePicker4 = this.dateTimeDuan2.End;
            button2_Click(null, null);

        }

        
      

        private void button2_Click(object sender, EventArgs e)//查询按钮点击事件
        {
            this.dataGView2.ClearData();
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            if (this.comboBox1.Value != null)
            {
                if (this.comboBox1.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    if (this.comboBox1.Value != "9")
                        sql.Add("and STATUS = ?", this.comboBox1.Value.Trim());

                }
            }
            if (Ware_selTextInpt.Value != null)
            {
                if (Ware_selTextInpt.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and WARECODE = ?", Ware_selTextInpt.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择库房！");
                return;
            }
          

            //添加查询条件及其参数
            sql.Add("and PDDATE <= ?", this.dateTimePicker4.Value);

            sql.Add("and PDDATE >= ?", this.dateTimePicker3.Value);


            //加载查询数据
            this.dataGView1.Url = "FindEQPandianMainInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);
            this.dataGView1.setFocus(0, 0);

            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";  
            

        }

        

        private void dataGView1CellClick() //单击表格触发事件（原功能）
        {


            DataRow r1 = this.dataGView1.GetRowData();
            this.dataGView2.Url = "EQPanDianDetailInfo";
            this.dataGView2.reLoad(new object[] { r1["PDID"], His.his.Choscode });

        }



        private void toolStripButton1_Click(object sender, EventArgs e)//审核
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString()=="1")
                {
                    if (WJs.confirm ("审核是否通过？"))
                    //审核通过
                    {
                        dataGView1CellClick();//保证打taGview2 中有数据
                        if (this.dataGView2.GetData() != null)
                        {

                            foreach (Dictionary<string, ObjItem> dr in this.dataGView2.GetData())
                            {
                                if (dr["库存流水号"].ToString().Trim().Length > 0)
                                {

                                    if (dr["单价"].IsNull || dr["单价"].ToString().Trim() == "")
                                    {
                                        WJs.alert("流水号为" + dr["库存流水号"] + "的单价为空！");
                                        return;
                                    }
                                   
                                }
                                else
                                {
                                    break;
                                }

                            }


                            //rowid = this.dataGView1.CurrentRow.Index;
                            ActionLoad ac = ActionLoad.Conn();
                            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                            ac.Sql = "ShenHeSucPD";
                            ac.Add("PDID", r1["PDID"]);
                            ac.Add("CHOSCODE", His.his.Choscode);
                            ac.Add("STATUS", "6");
                            ac.Add("SHUSERID", His.his.UserId);
                            ac.Add("SHUSERNAME", His.his.UserName);


                            ac.Add("WARECODE", r1["WARECODE"].ToString());
                            ac.Add("MEMO", r1["MEMO"].ToString());
                            ac.Add("CHOSCODE", His.his.Choscode);
                            ac.Add("USERID", His.his.UserId);
                            ac.Add("USERNAME", His.his.UserName);

                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_JieZhuang);
                            ac.Post();
                        }
                        else
                        {
                            WJs.alert("盘点细表中没有数据，不能结转！");
                        }


                    }
                    else//审核不通过
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                        ac.Sql = "ShenHeLost";
                        ac.Add("PDID", r1["PDID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.Add("STATUS", "2");
                       

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_ShenHe);
                        ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
                        ac.Post();
                      
                    
                    }
                }
                else
                {
                    WJs.alert("只能对状态为待审核的数据进行审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的盘点信息");
            }
        }

      

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
             DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (r1["STATUS"].ToString() == "6")
                {
                    if (WJs.confirmFb("确定冲销吗？"))
                    {

                        dataGView1CellClick();//保证打taGview2 中有数据
                        if (this.dataGView2.GetData() != null)
                        {

                           


                            //rowid = this.dataGView1.CurrentRow.Index;
                            ActionLoad ac = ActionLoad.Conn();
                            ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                            ac.Sql = "EQCXPD";
                            ac.Add("PDID", r1["PDID"]);
                            ac.Add("CHOSCODE", His.his.Choscode);
                            ac.Add("STATUS", "0");
                            ac.Add("CXUSERID", His.his.UserId);
                            ac.Add("CXUSERNAME", His.his.UserName);
                       
                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_JieZhuang);
                            ac.Post();
                        }
                        else
                        {
                            WJs.alert("盘点细表中没有数据，不能冲销！");
                        }
                    }
                }
                else
                {
                    WJs.alert("只能对状态为已审核的数据进行冲销！");
                }
            }
            else
            {
                WJs.alert("请选择要冲销的盘点信息");
            }
       
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (WJs.confirmFb("确定要删除选择的盘点信息吗？"))
                {
                    if (r1["STATUS"].ToString() == "1")
                    {
                        if (this.dataGView1.RowCount == this.dataGView1.CurrentRow.Index+1)
                            rowid = 0;
                        else
                           rowid = this.dataGView1.CurrentRow.Index;
                       // rowid =rowid+1;
                       // if (this.dataGView1[1, rowid].Value == null || this.dataGView1[1, rowid].Value.ToString().Trim().Length<=0)//判断是否为最后一行
                       // {
                         //   rowid = 0;
                       // }
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                        ac.Sql = "DelEQPanDianInfo";
                        ac.Add("PDID", r1["PDID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能删除状态为等待审核的数据！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要删除的盘点信息");
            }
        }

       

        private void toolStripButton_Submit_Click(object sender, EventArgs e)
        {

            DataRow r1 = this.dataGView1.GetRowData();
            if (r1 != null)
            {
                if (WJs.confirmFb("确定要提交选择的盘点信息吗？"))
                {
                    if (r1["STATUS"].ToString() == "2")
                    {

                        rowid = this.dataGView1.CurrentRow.Index;
  
                        if (this.dataGView1[1, rowid].Value == null || this.dataGView1[1, rowid].Value.ToString().Trim().Length <= 0)//判断是否为最后一行
                        {
                            rowid = 0;
                        }
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQPanDianSvr";
                        ac.Sql = "SubmitEQPD";
                        ac.Add("PDID", r1["PDID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能提交状态为审核被拒的数据！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要提交的盘点信息");
            }
        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton3_Click(null, null);//查看
        
        }

       

       


    }
}
