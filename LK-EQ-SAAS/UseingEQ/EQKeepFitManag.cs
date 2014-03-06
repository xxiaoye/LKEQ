using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtWinContrl.com.datagrid;
using YtClient;
using ChSys;
using YtUtil.tool;
using YiTian.db;
using UseingEQ.form;

namespace UseingEQ
{
    public partial class EQKeepFitManag : Form, IPlug
    {

        int IfSingleOrMultStatus;

        public EQKeepFitManag()
        {
            InitializeComponent();

            this.selTextInpt1.Sql = "EQKeep_FindKSID";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            //this.dateDuanSel1.Start = DateTime.Now.AddMonths(-1);
            //this.dateDuanSel1.End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


            EQKind_ytTreeView.vFiled = "KINDCODE";
            EQKind_ytTreeView.tFiled = "KINDNAME";
            EQKind_ytTreeView.pFiled = "SUPERCODE";
            EQKind_ytTreeView.pValue = "";
            EQKind_ytTreeView.sql = "FindEQKindInCard";
            EQKind_ytTreeView.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(EQKind_ytTreeView_FormatText);
            EQKind_ytTreeView.reLoad(new object[] { His.his.Choscode.ToString() });

           
           
            this.dataGView1.Url = "LoadZhuBiaoInfoSelect_EQKeepFit";
            this.dataGView2.Url = "LoadXiBiaoInfoSelect_EQKeepFit";
        }

        void EQKind_ytTreeView_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
            e.FmtText = e.row["KINDNAME"].ToString();
        }

        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {

        }

        public void init()
        {

        }

        public bool unLoad()
        {
            return true;
        }

        #endregion


        void dateTimeDuan2_SelectChange(object sender, EventArgs e) //dateTimeDuan2选项发生改变时触发该事件
        {

            this.dateTimePicker3 = this.dateTimeDuan2.Start;
            this.dateTimePicker4 = this.dateTimeDuan2.End;
            button1_Click(null, null);

        }


        private void EQFixManag_Load(object sender, EventArgs e) //窗体加载
        {
            EQKind_ytTreeView.SelectedNode = EQKind_ytTreeView.Nodes[0];
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_Column);

            TvList.newBind().add("作废", "0").add("有效", "1").Bind(this.UseManagStatu_Column);
            TvList.newBind().Load("FindKSName_EQKeepFit",new object[]{His.his.Choscode}).Bind(this.Deptid_Column);
            TvList.newBind().Load("GetEQMaintainName_KeepFit1", null).Bind(this.Column72);
            TvList.newBind().Load("FindKSName_EQKeepFit", new object[] { His.his.Choscode }).Bind(this.Column65);
            TvList.newBind().Load("FindUseStatusInCard_EQKeepFit", new object[] { His.his.Choscode }).Bind(this.Statuscode_Column);

            this.dateTimeDuan2.InitCorl();
            this.dateTimeDuan2.SelectedIndex = -1;

            this.dateTimeDuan2.SelectChange += new EventHandler(dateTimeDuan2_SelectChange);
            this.dateTimePicker3.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
         
        }
        ////[来自设备使用]
        ////使用状态的绑定
        //void BindStatusCode()
        //{
        //    TvList tv = TvList.newBind();
        //    DataTable dt = LData.LoadDataTable("FindUseStatusInCard", null, new object[] { His.his.Choscode.ToString() });
        //    ((DataGridViewComboBoxColumn)Statuscode_Column).Items.Clear();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            tv.add(dr["STATUSNAME"].ToString(), dr["STATUSCODE"].ToString());
        //        }
        //        tv.Bind(Statuscode_Column);
        //    }
        //}
      

        private void button1_Click(object sender, EventArgs e)//查询
        {
            SqlStr sqls = SqlStr.newSql();//进行过滤的条件

            if (this.selTextInpt1.Value == null || this.selTextInpt1.Value.Trim() == "")
            {
                WJs.alert("请选择一个科室！");
                return;
            }
            DataRow dr1 = EQKind_ytTreeView.getSelectRow();

            if (EQKind_ytTreeView.SelectedNode != EQKind_ytTreeView.Nodes[0])
            {
                sqls.Add(" AND EQID IN (SELECT EQID FROM LKEQ.DICTEQ WHERE KINDCODE= " + dr1["KINDCODE"].ToString() + ")");
            }

            if (this.dateTimePicker3.Value.CompareTo(this.dateTimePicker4.Value) > 0)
            {
                WJs.alert("起始日期必须小于结束日期！");
                return;
            } 
            //select * from LKEQ.EQCARDREC WHERE  CHOSCODE =? AND DEPTID=? [Condition] AND OUTDATE&gt;? AND OUTDATE &lt;?
           // this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt1.Value, dateDuanSel1.Start, dateDuanSel1.End }, sqls);
            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt1.Value, this.dateTimePicker3.Value, this.dateTimePicker4.Value }, sqls);

            this.dataGView1.setFocus(0, 1);
            this.dataGView2.ClearData();
            dataGView1_SelectionChanged(null, null);
        }

        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                // SELECT DISTINCT a.*,b.Repairname,c.名称  FROM  LKEQ.EQCARDREPAIRREC a, LKEQ.DICTEQREPAIR b,HIS.科室表 c
                // WHERE  a.DEPTID=c.ID AND a.REPAIRCODE=b.REPAIRCODE AND a.Cardid=? AND a.CHOSCODE=?
                this.dataGView2.reLoad(new object[] { dr["卡片ID"].ToString(), His.his.Choscode });
            }
            else
            {
                //this.dataGView2.reLoad(null);
                return;
            }
        }

        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            if (selTextInpt1.Text != "" || selTextInpt1.Value != "")
            {
                button1_Click(null, null);
            }
            else
            {
                WJs.alert("请先选择一个科室！");
            }
        }

        private void Del_toolStrip_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null)
            {
                WJs.alert("请在细表中选择要删除的设备保养信息！");
                dataGView2.Focus();
                return;
            }
            else
            {
                if (drX["状态"].ToString() == "0")
                {
                    WJs.alert("该设备保养信息已经为无效状态，无需删除！");
                    return;
                }
                else
                {
                    LData.Exe("UpdateStatus_EQKeepFitManag", null, new object[] { drX["保养ID"].ToString(), His.his.Choscode });
                    WJs.alert("删除成功！");
                    refresh_toolStrip_Click(null, null);
                }
            }
        }

        private void View_toolStrip_Click(object sender, EventArgs e)//浏览
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要查看的设备保养信息！");
                return;
            }
            //浏览 全能 状态
            EQKeepFitManagEdit form = new EQKeepFitManagEdit(drZ, drX, 0);
            form.ShowDialog();
        }

        private void Edit_toolStrip_Click(object sender, EventArgs e)//编辑
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要编辑的设备保养信息！");
                return;
            }

            if (drX["状态"].ToString() == "0")
            {
                WJs.alert("无法对作废的设备保养信息进行修改！");
                return;
            }
            //编辑  全能状态
            EQKeepFitManagEdit form = new EQKeepFitManagEdit(drZ, drX, 1);
            form.ShowDialog();
            refresh_toolStrip_Click(null, null);

        }

        private void Add_toolStrip_Click(object sender, EventArgs e)//新增
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            if (drZ == null)
            {
                WJs.alert("请选择要新增保养信息的设备卡片信息！");
                return;
            }
            if (drZ["状态"].ToString() !="2")
            {
                WJs.alert("只能对状态为已启用的卡片设备新增保养信息！");
                return;
            }
            //新增  全能  状态
            EQKeepFitManagEdit form = new EQKeepFitManagEdit(drZ, null, 2);//以新增进去
            form.ShowDialog();
            refresh_toolStrip_Click(null, null);
        }

     

     

        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }


    }
}
