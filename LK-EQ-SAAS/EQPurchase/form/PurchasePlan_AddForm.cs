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
using YiTian.db;

namespace EQPurchase
{
    public partial class PurchasePlan_AddForm : Form,IPlug
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
        string warecode = "";
        public PurchasePlan_AddForm(string warecode)
        {
            InitializeComponent();
            this.warecode = warecode;
        }
        int rowid = 0;
        
        public PurchasePlan_Add Main;
        private void WZDictManag_Load(object sender, EventArgs e)//窗体加载
        {
            this.WindowState = FormWindowState.Maximized;
            WJs.SetDictTimeOut();//什么意思？
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
            Search_button_Click(null, null);//浏览

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
       
      

    



    
      


        private void Search_button_Click(object sender, EventArgs e) //浏览//细节还需要进行权限判断
        {
           
            this.dataGView_Main.ClearData();

            SqlStr sql = SqlStr.newSql();//创建SqlStr对象

            //if (this.selTextInpt_KSID.Value != null) // 权限判断，要不要这个功能
            //{
            //    if (selTextInpt_KSID.Value.Trim().Length > 0)
            //    {
            //        //添加查询条件及其参数
            //        sql.Add("and DEPTID = ?", selTextInpt_KSID.Value.Trim());

            //    }
            //}
            //else
            //{
            //    WJs.alert("请选择科室！");
            //    return;
            //}


            //添加查询条件及其参数
            if (this.selTextInpt_KSID.Value != null)
            {
                sql.Add("and a.DEPTID = ?", this.selTextInpt_KSID.Value);
            }
            sql.Add("and a.PLANDATE <= ?", this.dateTimePicker2.Value);

            sql.Add("and a.PLANDATE >= ?", this.dateTimePicker1.Value);


            //加载查询数据
            this.dataGView_Main.Url = "EQAskBuy_ConditionScan1";
            this.dataGView_Main.reLoad(new object[] { warecode, His.his.Choscode, His.his.Choscode }, sql);
            this.dataGView_Main.setFocus(0, 0);
       

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
         
                if (WJs.confirmFb("是否确定不在请购单中选择而直接退出？"))
                {
                    this.Close();
                }
          
     

        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            //Dictionary<string, ObjItem> dict = null;
            //List<Dictionary<string, ObjItem>> listdata = null;
            //DataTable dt = null;
            //// for(int i=0;i<this.dataGView_Main.RowCount;i++)
            //{
            //    if (this.dataGView_Main[0, i].Value !=null && bool.Parse(this.dataGView_Main[0, i].Value.ToString()))
            //    {

            //        DataGridViewRow dgvr = this.dataGView_Main.Rows[i];
            //         dict=this.dataGView_Main.getRowData(dgvr);
            //         listdata.Add(dict);
                     

            //    }
                
            
            //}
             Main.GetForm(this.dataGView_Main.GetData());
             this.Close();
        }




    }
}
