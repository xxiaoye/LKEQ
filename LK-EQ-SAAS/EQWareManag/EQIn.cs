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
using EQWareManag.form;
using YiTian.db;
using YtClient;
using YtWinContrl.com;

namespace EQWareManag
{
    public partial class EQIn : Form, IPlug
    {

        public EQIn()
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
            this.app = app;
            if (param != null && param.Length > 0)
            {
                // ty = (param[0].ToString());
            }
        }

        public bool unLoad()
        {
            return true;
        }
        //  private Panel[] plis = null;

        #endregion
        IAppContent app;


       

        void dataGView1_DoubleClick(object sender, EventArgs e)//查看
        {
            scan_toolStripButton_Click(null, null);
        }
        void dateTimeDuan1_SelectChange(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }

        int status = 0;
        private void add_toolStripButton_Click(object sender, EventArgs e)
        {
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库库房！");
                return;
            }
            EQIn_Add form = new EQIn_Add(this.InWare_selTextInpt.Value, this.InWare_selTextInpt.Text,app);
         
            form.ShowDialog();
            this.InStatus_ytComboBox.SelectedIndex = 0;
            Search_button_Click(null, null);
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            if (this.InStatus_ytComboBox.Text.Trim().Length == 0)
            {
                WJs.alert("请选择入库状态！");
                return;
            }
            if (this.InWare_selTextInpt.Value == null)
            {
                WJs.alert("请选择入库库房！");
                return;
            }

            this.dataGView1.reLoad(new object[] { His.his.Choscode, TvList.getValue(this.InStatus_ytComboBox).ToInt(),this.InWare_selTextInpt.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value });
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";
            this.RuKuJinEHeJi.Text = this.dataGView1.Sum("运杂费金额").ToString() + "元";
            this.label6.Text = this.dataGView1.Sum("发票金额").ToString() + "元";

        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToInt() == 1 || dr["状态"].ToInt() ==2)
                {

                    EQIn_Add form = new EQIn_Add(dr, 2,app);//编辑
                    form.ShowDialog();
                    this.InStatus_ytComboBox.SelectedIndex = 0;
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再修改！");
                }
                if (dr["状态"].ToInt() == 7)
                {
                    WJs.alert("该入库信息已冲销，不能再修改！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已审核，不能再修改！");
                }
            }
            else
            {
                WJs.alert("请选择要修改的入库信息！");
            }
        }

        private void scan_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                EQIn_Add form = new EQIn_Add(dr, 3,app);//浏览
                form.ShowDialog();
                //this.InStatus_ytComboBox.SelectedIndex = 0;
                Search_button_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要浏览的入库信息！");
            }
        }

        private void DeleButton_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1 || dr["状态"].ToInt() == 2)
                {

                    if (WJs.confirmFb("您确定要删除入库信息id=" + dr["入库ID"].ToString() + "吗？"))
                    {
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                        ac.Sql = "RuKuDanDelete";
                        ac.Add("INID", dr["入库ID"].ToString());
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        this.InStatus_ytComboBox.SelectedIndex = 1;
                        Search_button_Click(null, null);
                      
                    }

                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再删除！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该入库信息已审核，不能再删除！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再删除！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的入库信息！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)//删除成功返回事件
        {
            WJs.alert(e.Msg.Msg);
        }

        public void Reload(int a)//删除成功返回事件
        {
            status = a;
        }

        private void check_toolStripButton_Click(object sender, EventArgs e)//审核
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
          //  List<Dictionary<string, ObjItem>> list = this.dataGView2.GetData();
            if (dr != null)
            {

                if (dr["状态"].ToInt() == 1)
                {


                    EQIn_Add form = new EQIn_Add(dr, 4,app);//编辑
                    form.Main = this;
                    form.ShowDialog();
                   
                    this.InStatus_ytComboBox.SelectedIndex = status;

                   // this.InStatus_ytComboBox.SelectedIndex = 2;
                    //WJs.alert("入库信息id=" + dr["入库ID"].ToString() + "审核成功！");
                    Search_button_Click(null, null);
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能再审核！");
                }
                if (dr["状态"].ToInt() == 2)
                {
                    WJs.alert("该入库信息已审核，不能再次审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已入库，不能再审核！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的入库信息！");
            }
        }

        private void UnCheck_toolStripButton_Click(object sender, EventArgs e)//提交
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {

                if (dr["状态"].ToInt() == 2)
                {
                    if (WJs.confirmFb("您确定要提交审核入库信息id=" + dr["入库ID"].ToString() + "吗？"))
                    {
                        ActionLoad ac = new ActionLoad();
                        ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                        ac.Sql = "RuKuDanUpdate";
                        
                        ac.Add("STATUS", 1);
                        ac.Add("INID", dr["入库ID"].ToString());
                     
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        this.InStatus_ytComboBox.SelectedIndex =0;
                        Search_button_Click(null, null);
                    }
                    
                }
                if (dr["状态"].ToInt() == 0)
                {
                    WJs.alert("该入库信息已作废，不能提交审核！");
                }
                if (dr["状态"].ToInt() == 1)
                {
                    WJs.alert("该入库信息还未审核，不需要再次提交审核！");
                }
                if (dr["状态"].ToInt() == 6)
                {
                    WJs.alert("该入库信息已审核，不能再提交审核！");
                }
            }
            else
            {
                WJs.alert("请选择要提交审核的入库信息！");
            }
        }

        private void InWare_toolStripButton_Click(object sender, EventArgs e)//冲销
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();

            if (dr != null)
            {

                if (dr["状态"].ToInt() == 6)
                {


                    //EQIn_Add form = new EQIn_Add(dr,5);//编辑
                    //form.ShowDialog();
                    //this.InStatus_ytComboBox.SelectedIndex = 3;
                    //Search_button_Click(null, null);



                    ActionLoad ac = new ActionLoad();
                    ac.Action = "LKWZSVR.lkeq.EQWareManag.EQInSvr";
                    ac.Sql = "CXData";
                    ac.Add("CXDATE", DateTime.Now);
                    ac.Add("STATUS", 0);
                    ac.Add("INID", dr["入库ID"].ToString());
                    ac.Add("CXUSERID", His.his.UserId.ToString());
                    ac.Add("CXUSERNAME", His.his.UserName);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    this.InStatus_ytComboBox.SelectedIndex = 4;
                    Search_button_Click(null, null);
                }
                else
                {
                    WJs.alert("只能对已审核的数据进行冲销！");
                }
               
            }
            else
            {
                WJs.alert("请选择要冲销的入库信息！");
            }
            
        }

        private void EQIn_Load(object sender, EventArgs e)//加载数据
        {
            
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "EQInMainSearch";



            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            //this.dateTimeDuan1.SelectedIndex=3;
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").Bind(this.Column7);
            TvList.newBind().add("等待审核", "1").add("作废", "0").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").Bind(this.InStatus_ytComboBox);

            //this.dataGView2.Url = "WZInDetailSearch";
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.InWare_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.InWare_selTextInpt.Sql = "EQGetInWare";
            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
            this.dataGView1.DoubleClick += new EventHandler(dataGView1_DoubleClick);
            TvList.newBind().Load("EQIn_Ware", new object[] { His.his.Choscode }).Bind(this.Column4);
            TvList.newBind().Load("EQIn_IONAME", new object[] { His.his.Choscode }).Bind(this.Column2);

        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.scan_toolStripButton_Click(null, null);
        }



        
       
    }
}
