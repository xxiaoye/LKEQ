using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using ChSys;
using YtWinContrl.com.datagrid;
using YiTian.db;
using YtUtil.tool;
using YtClient;
using UseingEQ.form;

namespace UseingEQ
{
    public partial class EQUseManag : Form, IPlug
    {
        public EQUseManag()
        {
            InitializeComponent();
            //这个是从共享建卡过来的
            this.selTextInpt1.Sql = "FindDeptidInCard";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            EQKind_ytTreeView.vFiled = "KINDCODE";
            EQKind_ytTreeView.tFiled = "KINDNAME";
            EQKind_ytTreeView.pFiled = "SUPERCODE";
            EQKind_ytTreeView.pValue = "";
            EQKind_ytTreeView.sql = "FindEQKindInCard";
            EQKind_ytTreeView.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(EQKind_ytTreeView_FormatText);
            EQKind_ytTreeView.reLoad(new object[] { His.his.Choscode.ToString() });

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

        private void EQUseManag_Load(object sender, EventArgs e)
        {
            this.dataGView1.Url = "LoadZhuBiaoInfoSelect_EQUseManag";
            this.dataGView2.Url = "LoadXiBiaoInfo_EQUseManag";


            EQKind_ytTreeView.SelectedNode = EQKind_ytTreeView.Nodes[0];
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_Column);
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(UseManagStatu_Column);
            BindStatusCode();
            DeptidBind();

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }



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
        }

        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                //SELECT DISTINCT a.*,b.名称  FROM  LKEQ.EQCARDUSEREC a, HIS.科室表 b WHERE a.CARDID=02 AND a.Deptid=b.Id AND a.CHOSCODE=01050303
                this.dataGView2.reLoad(new object[] { dr["卡片ID"].ToString(), His.his.Choscode });
            }
            else
            {
                this.dataGView2.reLoad(null);
            }
        }

        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView2.getRowData();
            if (dr == null)
            {
                WJs.alert("请选择需要删除的设备使用信息！");
                return;
            }
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1")
                {
                    if (WJs.confirm("您确认要删除该条设备使用信息吗？"))
                    {
                        LData.Exe("DeleteEQUseInfo_EQUseManag", null, new object[] { His.his.Choscode, dr["使用ID"].ToString() });
                        WJs.alert("删除成功！");
                        if (selTextInpt1.Text != "" && selTextInpt1.Value != "")
                        {
                            refresh_toolStrip_Click(null, null);
                        }
                    }
                }
                else
                {
                    WJs.alert("无效的设备使用信息无需删除！");
                }
            }
        }

        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = this.dataGView2.getRowData();
            Dictionary<string, ObjItem> drZ = this.dataGView1.getRowData();
            if (drX == null)
            {
                WJs.alert("请选择需要修改的设备使用信息！");
                return;
            }
            if (drX != null)
            {
                if (drX["状态"].ToString() == "1")
                {
                    EQUseManagEdit form = new EQUseManagEdit(drZ, drX, 1);
                    form.ShowDialog();
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("无效的设备使用信息不能再进行修改！");
                }
            }
        }

        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = this.dataGView2.getRowData();
            Dictionary<string, ObjItem> drZ = this.dataGView1.getRowData();
            if (drX == null)
            {
                WJs.alert("请选择需要修改的设备使用信息！");
                return;
            }
            if (drX != null)
            {
                EQUseManagEdit form = new EQUseManagEdit(drZ, drX, 0);
                form.ShowDialog();
            }
        }

        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = this.dataGView1.getRowData();
            if (drZ == null)
            {
                WJs.alert("请在信息表中选择需要增加使用信息的设备ID！");
                dataGView1.Focus();
                return;
            }
            if (drZ["状态"].ToString() == "2")
            {
                EQUseManagEdit form = new EQUseManagEdit(drZ, null, 2);
                form.ShowDialog();
                if (selTextInpt1.Text != "" || selTextInpt1.Value != "")
                {
                    refresh_toolStrip_Click(null, null);
                }
            }
            else
            {
                WJs.alert("只能对已启用的卡片设备添加使用信息！");
            }
        }


        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            if (selTextInpt1.Text != "" || selTextInpt1.Value != "")
            {
                button1_Click(null, null);
                this.dataGView1.setFocus(0, 1);
                dataGView1_SelectionChanged(null, null);
            }
            else
            {
                WJs.alert("请先选择一个科室！");
            }
        }

        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }
    }
}

