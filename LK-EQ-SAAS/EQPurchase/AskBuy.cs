using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using EQPurchase.form;
using YtPlugin;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;

namespace EQPurchase
{
    public partial class AskBuy : Form,IPlug
    {

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

        public AskBuy()
        {
            InitializeComponent();
        }
        int rowid = 0;
        private void WZDictManag_Load(object sender, EventArgs e)//窗体加载
        {
            WJs.SetDictTimeOut();//什么意思？
            TvList.newBind().add("全部", "9").add("已审核", "6").add("审核被拒", "2").add("等待审核", "1").add("作废", "0").Bind(this.comboBox1);
            TvList.newBind().Load("EQUnitCodeAskBuy_Add", null).Bind(this.Column12);
            TvList.newBind().Load("AskBuy_FindKSName",new object[]{His.his.Choscode}).Bind(this.Column3);

            TvList.newBind().add("已审核", "6").add("审核被拒", "2").add("待审核", "1").add("已删除", "0").Bind(this.Column13);
            this.selTextInpt_KSID.Sql = "AskBuy_FindKSID";

            this.selTextInpt_KSID.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;

            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

        }

        void dateTimeDuan1_SelectChange(object sender, EventArgs e) //dateTimeDuan2选项发生改变时触发该事件
        {

            this.dateTimePicker1 = this.dateTimeDuan1.Start;
            this.dateTimePicker2 = this.dateTimeDuan1.End;
            toolStripButton4_Click(null, null);//浏览

        }
      
        private void toolStripButton4_Click(object sender, EventArgs e)//查看
        {
            dataGView_Main_DoubleClick(null, null);

      

        }
        public void ReLoadData()
        {

            this.dataGView_Main.Url = "EQAskBuy_Scan";
            this.dataGView_Main.reLoad(new object[] {  His.his.Choscode });
            if (this.dataGView_Main.RowCount <=rowid)
            {
                this.dataGView_Main.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Main.setFocus(rowid, 0);
            }
        }
       
      

        private void toolStripButton1_Click(object sender, EventArgs e)//新增
        {
          
                AskBuy_Add ks = new AskBuy_Add(null, true);
                ks.AskB = this;
               ks.ShowDialog();

            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//修改
        {
            
            DataRow r = this.dataGView_Main.GetRowData();
          
            if (r != null)
            {
                if (r["STATUS"].ToString() == "1" || r["STATUS"].ToString() == "2")
                {

                    rowid = this.dataGView_Main.CurrentRow.Index;
                    AskBuy_Add ks = new AskBuy_Add(r, false);
                    ks.AskB = this;
                    ks.ShowDialog();
                }
                else
                {
                    WJs.alert("状态错误，只能修改状态为等待审核或者审核被拒的数据！");
                }

               
              
            }
           
            else
            {
                WJs.alert("请选择要编辑的请购信息");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)//成功返回事件
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
         
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
           
            DataRow r = this.dataGView_Main.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("确定要删除选择的请购信息吗？"))
                {


                    if (r["STATUS"].ToString() == "1" || r["STATUS"].ToString() == "2")
                    {

                        rowid = this.dataGView_Main.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQPurchase.EQAskBuySvr";
                        ac.Sql = "DelEQAskBuyInfo";
                        ac.Add("APPLYID", r["APPLYID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能删除状态为等待审核或者审核被拒的数据！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要删除的请购信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//提交
        {
            DataRow r= this.dataGView_Main.GetRowData();
            if (r != null)
            {
                if (WJs.confirmFb("是否提交？"))
                {
                    if ( r["STATUS"].ToString() == "2")
                    {
                        rowid = this.dataGView_Main.CurrentRow.Index;
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.EQPurchase.EQAskBuySvr";
                        ac.Sql = "Submit_EQAskBuyInfo";
                        ac.Add("APPLYID", r["APPLYID"]);
                        ac.Add("CHOSCODE", His.his.Choscode);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能对状态为审核被拒的数据进行再次提交！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要提交的请购信息！");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//审核
        {

            DataRow r = this.dataGView_Main.GetRowData();

            if (r != null)
            {
               
                    if (r["STATUS"].ToString() == "1")
                    {
                        rowid = this.dataGView_Main.CurrentRow.Index;
                        AskBuy_Add ks = new AskBuy_Add(r, false, "SH");
                        ks.AskB = this;
                        ks.ShowDialog();
                    }
                    else
                    {
                        WJs.alert("状态错误，只能对状态为待审核的数据进行审核！");
                    }
               
            }

            else
            {
                WJs.alert("请选择要审核的请购信息");
            }
        }



        private void Search_button_Click(object sender, EventArgs e) //浏览//细节还需要进行权限判断
        {
           
            this.dataGView_Main.ClearData();

            SqlStr sql = SqlStr.newSql();//创建SqlStr对象

            if (this.selTextInpt_KSID.Value != null)         // 权限判断，要不要这个功能（暂时还未做权限判断）
            {
                if (selTextInpt_KSID.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    sql.Add("and DEPTID = ?", selTextInpt_KSID.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择科室！");
                return;
            }


            //添加查询条件及其参数
            if (this.comboBox1.Value != null)
            {
                if (this.comboBox1.Value.Trim().Length > 0)
                {
                    //添加查询条件及其参数
                    if (this.comboBox1.Value != "9")
                        sql.Add("and STATUS = ?", this.comboBox1.Value.Trim());

                }
            }
            else
            {
                WJs.alert("请选择状态！");
                return;
            }
            sql.Add("and PLANDATE <= ?", this.dateTimePicker2.Value);

            sql.Add("and PLANDATE >= ?", this.dateTimePicker1.Value);


            //加载查询数据
            this.dataGView_Main.Url = "EQAskBuy_ConditionScan";
            this.dataGView_Main.reLoad(new object[] { His.his.Choscode }, sql);
            this.dataGView_Main.setFocus(0, 0);
            this.TiaoSu.Text = this.dataGView_Main.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView_Main.Sum("预计总金额").ToString() + "元";
            
        }

        private void dataGView_Main_DoubleClick(object sender, EventArgs e)
        {
            

            DataRow r = this.dataGView_Main.GetRowData();

            if (r != null)
            {
                rowid = this.dataGView_Main.CurrentRow.Index;
                AskBuy_Add ks = new AskBuy_Add(r, false, "CK");
                ks.AskB = this;
                ks.ShowDialog();

            }

            else
            {
                WJs.alert("请选择请购信息");
            }


        
        }

       

        

    }
}
