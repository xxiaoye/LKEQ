using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtUtil.tool;
using YtWinContrl.com.datagrid;
using ChSys;
using EQPurchase.form;
using YiTian.db;
using YtClient;

namespace EQPurchase
{
    public partial class PurchasePlan : Form, IPlug
    {
        public PurchasePlan()
        {
            InitializeComponent();
        }
        #region IPlug 成员

        public Form getMainForm()
        {


            return this;
        }
        private void init()
        {

        }
        public void initPlug(IAppContent app, object[] param)
        {

        }

        public bool unLoad()
        {
            return true;
        }


        //  private Panel[] plis = null;

        #endregion
       

        void dataGView1_DoubleClick(object sender, EventArgs e)
        {
            scan_toolStripButton_Click(null, null);
        }
        void dateTimeDuan1_SelectChange(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }

        

        public void CallBack()
        {

            Search_button_Click(null, null);
           
           
        }
        
        private void add_toolStripButton_Click(object sender, EventArgs e)//新增
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择库房！");
                return;
            }
            PurchasePlan_Add form = new PurchasePlan_Add(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text);
            form.ShowDialog();
      
            Search_button_Click(null, null);
        }

        private void ModifyButton_Click(object sender, EventArgs e)//修改
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1 || dr["状态"].ToInt() == 2)
                {

                    PurchasePlan_Add form = new PurchasePlan_Add(dr, 2, this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text);//编辑
                    form.ShowDialog();
                    //this.InStatus_ytComboBox.SelectedIndex = 0;
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该该采购计划已删除，不能再修改！");
                }
              
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该该采购计划已审核，不能再修改！");
                }
            }
            else
            {
                WJs.alert("请选择要修改的采购计划信息！");
            }
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)//查看
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                PurchasePlan_Add form = new PurchasePlan_Add(dr, 3, this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text);//浏览
                form.ShowDialog();
                Search_button_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要查看的采购计划信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }
        private void DeleButton_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1 || dr["状态"].ToInt() == 2)
                {
                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                    ac.Sql = "EQPurchasePlanDelete";
                    ac.Add("PLANID", dr["采购计划ID"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    Search_button_Click(null, null);
                    // WJs.alert("删除采购计划id=" + dr["采购计划id"].ToString() + "成功！");
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该计划已删除，不能再次删除！");
                }
               
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该计划已审核，不能删除！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的采购计划信息！");
            }
        }

        private void check_toolStripButton_Click(object sender, EventArgs e) //审核
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1)
                {

                    PurchasePlan_Add form = new PurchasePlan_Add(dr, 4, this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text);//浏览
                    form.Main= this;
                    form.ShowDialog();
                    Search_button_Click(null, null);



                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该计划已删除，不能审核！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该计划已审核被拒，不能再审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该计划已审核，不需要再审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的采购计划信息！");
            }
        }

        private void PurchasePlan_Load(object sender, EventArgs e)
        {
             
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "EQPurchasePlan_MainSearch";

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            TvList.newBind().add("等待审核", "1").add("已删除", "0").add("审核被拒", "2").add("已审核", "6").Bind(this.Column7);
            TvList.newBind().add("其它(临时计划)", "0").add("月度计划", "1").add("季度计划", "2").add("年度计划", "3").Bind(this.Column3);
          
            //TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column8);
           
           // this.dataGView2.Url = "WZPlanDetailSearch";

            //this.dataGView1.reLoad(new object[] { His.his.Choscode });
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.InWare_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.InWare_selTextInpt.Sql = "EQGetInWare_PurchasePlan";
            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
            this.dataGView1.DoubleClick += new EventHandler(dataGView1_DoubleClick);
           
       
        }

        private void Search_button_Click(object sender, EventArgs e) //浏览
        {
            
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择库房！");
                return;
            }

            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.InWare_selTextInpt.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value });
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
        }

        private void Submit_toolStripButton_Click(object sender, EventArgs e) //提交
        {

            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if ( dr["状态"].ToInt() == 2)
                {
                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkeq.EQPurchase.EQPurchasePlanSvr";
                    ac.Sql = "PlanDanUpdata_Submit";
                    ac.Add("PLANID", dr["采购计划ID"].ToString());
                    ac.Add("STATUS", 1);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    Search_button_Click(null, null);
                 
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该计划已删除，不能提交！");
                }
                if (dr["状态"].ToInt() == 1)
                {
                    WJs.alert("该计划已是等待审核，不需提交！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该计划已审核，不需再提交！");
                }
            }
            else
            {
                WJs.alert("请选择要提交的采购计划信息！");
            }
        }


    }
}
