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
    public partial class EQFixManag : Form, IPlug
    {

        int IfSingleOrMultStatus;

        public EQFixManag()
        {
            InitializeComponent();

            this.selTextInpt1.Sql = "FindDeptidInCard";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            EQKind_ytTreeView.vFiled = "KINDCODE";
            EQKind_ytTreeView.tFiled = "KINDNAME";
            EQKind_ytTreeView.pFiled = "SUPERCODE";
            EQKind_ytTreeView.pValue = "";
            EQKind_ytTreeView.sql = "FindEQKindInCard";
            EQKind_ytTreeView.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(EQKind_ytTreeView_FormatText);
            EQKind_ytTreeView.reLoad(new object[] { His.his.Choscode.ToString() });

            IfSingleOrMultStatus = GetSysDanWei(2200);
            //从设备使用共享过来
            this.dataGView1.Url = "LoadZhuBiaoInfoSelect_EQUseManag";
            this.dataGView2.Url = "LoadXiBiaoInfoSelect_EQFixManag";
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

        //共享过来的系统参数  [0 单步模式  1  多部模式]
        private int GetSysDanWei(int Id)
        {
            return Convert.ToInt32(LData.Es("GetSysDanWeiInEQDiaoBoEdit", null, new object[] { Id }));
        }

        private void EQFixManag_Load(object sender, EventArgs e)
        {
            EQKind_ytTreeView.SelectedNode = EQKind_ytTreeView.Nodes[0];
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_Column);
            TvList.newBind().add("作废", "0").add("报修状态", "1").add("维修状态", "2").add("交付状态", "6").Bind(UseManagStatu_Column);



            BindStatusCode();
            DeptidBind();
            //如果为单步模式，那么只需要维修填写
            if (IfSingleOrMultStatus == 0)
            {

                BX_toolStrip.Visible = false;
                WX_toolStrip.Visible = false;
                JF_toolStrip.Visible = false;
                TipStatus_toolStrip.Text = "当前系统设置为单步维修管理模式！";
            }
            if (IfSingleOrMultStatus == 1)
            {
                Add_toolStrip.Visible = false;
                TipStatus_toolStrip.Text = "当前系统设置为多步维修管理模式！";
            }

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        //[来自设备使用]
        //使用状态的绑定
        void BindStatusCode()
        {
            TvList tv = TvList.newBind();
            DataTable dt = LData.LoadDataTable("FindUseStatusInCard", null, new object[] { His.his.Choscode.ToString() });
            ((DataGridViewComboBoxColumn)Statuscode_Column).Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tv.add(dr["STATUSNAME"].ToString(), dr["STATUSCODE"].ToString());
                }
                tv.Bind(Statuscode_Column);
            }
        }
        //科室ID的绑定
        void DeptidBind()
        {
            DataTable dt = LData.LoadDataTable("FindDeptIDNameInBuildCard", null, new object[] { His.his.Choscode.ToString() });
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Deptid_Column).Items.Clear();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tv.add(dr[1].ToString(), dr[0].ToString());
                }
                tv.Bind(this.Deptid_Column);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlStr sqls = SqlStr.newSql();//进行过滤的条件

            if (this.selTextInpt1.Value == null || this.selTextInpt1.Value.Trim() == "")
            {
                WJs.alert("请选择一个科室！");
                selTextInpt1.Focus();
                return;
            }
            DataRow dr1 = EQKind_ytTreeView.getSelectRow();

            if (EQKind_ytTreeView.SelectedNode != EQKind_ytTreeView.Nodes[0])
            {
                sqls.Add(" AND EQID IN (SELECT EQID FROM LKEQ.DICTEQ WHERE KINDCODE= " + dr1["KINDCODE"].ToString() + ")");
            }

            if (this.dateTimePicker1.Value.CompareTo(this.dateTimePicker2.Value) > 0)
            {
                WJs.alert("起始日期必须小于末尾日期！");
                return;
            }
            //select * from LKEQ.EQCARDREC WHERE  CHOSCODE =? AND DEPTID=? [Condition] AND OUTDATE&gt;? AND OUTDATE &lt;?
            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt1.Value, dateTimePicker1.Value, dateTimePicker2.Value }, sqls);
            this.dataGView1.setFocus(0, 1);
            dataGView1_SelectionChanged(null, null);
        }

        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                // SELECT DISTINCT a.*,b.Repairname,c.名称  FROM  LKEQ.EQCARDREPAIRREC a, LKEQ.DICTEQREPAIR b,HIS.科室表 c  WHERE  a.DEPTID=c.ID AND a.REPAIRCODE=b.REPAIRCODE AND a.Cardid=? AND a.CHOSCODE=?
                this.dataGView2.reLoad(new object[] { dr["卡片ID"].ToString(), His.his.Choscode });
            }
            else
            {
                this.dataGView2.reLoad(null);
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

        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null)
            {
                WJs.alert("请在细表中选择要删除的设备维修信息！");
                dataGView2.Focus();
                return;
            }
            else
            {
                if (drX["状态"].ToString() == "0")
                {
                    WJs.alert("该设备维修信息已经为无效状态，无需删除！");
                    return;
                }
                else
                {
                    LData.Exe("UpdateStatusEQFix_EQFixManag", null, new object[] { drX["维修ID"].ToString(), His.his.Choscode });
                    WJs.alert("删除成功！");
                    refresh_toolStrip_Click(null, null);
                }
            }
        }

        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要查看的设备维修信息！");
                return;
            }
            //浏览 全能 状态
            EQFixManagEdit form = new EQFixManagEdit(drZ, drX, 0, 10);
            form.ShowDialog();
        }

        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要编辑的设备维修信息！");
                return;
            }

            if (drX["状态"].ToString() == "0")
            {
                WJs.alert("无法对作废的设备维修信息进行修改！");
                return;
            }
            //编辑  全能状态
            EQFixManagEdit form = new EQFixManagEdit(drZ, drX, 1, 10);
            form.ShowDialog();
            refresh_toolStrip_Click(null, null);

        }

        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            if (drZ == null)
            {
                WJs.alert("请选择要新增维修信息的设备卡片信息！");
                return;
            }
            if (drZ["状态"].ToString() == "2")
            {
                //新增  全能  状态
                EQFixManagEdit form = new EQFixManagEdit(drZ, null, 2, 10);//以新增进去
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对已启用的卡片设备添加维修信息！");
            }
        }

        private void BX_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            if (drZ == null)
            {
                WJs.alert("请需要报修的设备卡片信息！");
                return;
            }
            //新增   报修  状态
            //EQFixManagEdit form = new EQFixManagEdit(drZ, null, 2, 1);//以新增进去
            //form.ShowDialog();
            //refresh_toolStrip_Click(null, null);
            if (drZ["状态"].ToString() == "2")
            {
                EQFixManagEdit form = new EQFixManagEdit(drZ, null, 2, 1);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对已启用的卡片设备添加维修信息！");
            }
        }

        private void WX_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要编辑的设备维修信息！");
                return;
            }
            if (drX["状态"].ToString() == "1")
            {
                //修改维修 状态
                EQFixManagEdit form = new EQFixManagEdit(drZ, drX, 1, 2);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对报修状态的设备进行维修处理！");
                return;
            }
        }

        private void JF_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null || drZ == null)
            {
                WJs.alert("请选择要编辑的设备维修信息！");
                return;
            }
            if (drX["状态"].ToString() == "2")
            {
                //修改  交付处理
                EQFixManagEdit form = new EQFixManagEdit(drZ, drX, 1, 6);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对维修状态的设备进行交付处理！");
            }
        }

        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }
    }
}
