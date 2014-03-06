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
using YtUtil.tool;
using ChSys;
using YiTian.db;
using YtClient;
using UseingEQ.form;

namespace UseingEQ
{
    public partial class EQChangeManag : Form, IPlug
    {

        bool isOk;
        public EQChangeManag()
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

            //从设备使用共享过来
            this.dataGView1.Url = "LoadZhuBiaoInfoSelect_EQUseManag";
            this.dataGView2.Url = "LoadXiBiaoInfoIn_EQChangeManag";
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

        private void EQChangeManag_Load(object sender, EventArgs e)
        {
            EQKind_ytTreeView.SelectedNode = EQKind_ytTreeView.Nodes[0];
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_Column);
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").Bind(ChangeManagStatu_Column);
            TvList.newBind().add("所属权变更", "1").add("原值变更", "2").add("使用状态变更", "3").Bind(ChangeTypeColumn);
            BindStatusCode();
            DeptidBind();

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



        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }

        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                // SELECT  a.*, b.名称 AS 原使用科室 ,c.Statusname AS 原使用状态,
                //(SELECT 名称 FROM HIS.科室表 WHERE ID=a.Usedeptid) AS 现使用科室,
                //(SELECT STATUSNAME FROM LKEQ.DICTEQSTATUS WHERE STATUSCODE=a.Newstatuscode) AS 新使用状态
                //FROM LKEQ.EQCARDCHANGEREC a,HIS.科室表 b,LKEQ.DICTEQSTATUS c
                //WHERE a.CARDID=? AND a.CHOSCODE=? AND a.Olddeptid=b.Id AND a.Oldstatuscode=c.Statuscode
                this.dataGView2.reLoad(new object[] { dr["卡片ID"].ToString(), His.his.Choscode });
            }
            else
            {
                this.dataGView2.reLoad(null);
            }
        }

        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null)
            {
                WJs.alert("请选择要删除的设备变动数据！");
                dataGView2.Focus();
                return;
            }
            if (drX["状态"].ToString() == "1" || drX["状态"].ToString() == "2")
            {
                //UPDATE LKEQ.EQCARDCHANGEREC SET STATUS=1 WHERE CHANGEID=1 AND CHOSCODE=01050303
                LData.Exe("UpdateStatusInfo_EQChangeManag", null, new object[] { "0", drX["变动ID"].ToString(), His.his.Choscode });
                WJs.alert("删除成功！");
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能删除状态为等待审核与审核被拒的设备变动数据！");
                return;
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

        private void SubmitCheck_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drX == null)
            {
                WJs.alert("请选择要提交的设备变动数据！");
                dataGView2.Focus();
                return;
            }

            if (drX["状态"].ToString() == "2")
            {
                //UPDATE LKEQ.EQCARDCHANGEREC SET STATUS=1 WHERE CHANGEID=1 AND CHOSCODE=01050303
                LData.Exe("UpdateStatusInfo_EQChangeManag", null, new object[] { "1", drX["变动ID"].ToString(), His.his.Choscode });
                WJs.alert("提交审核成功！");
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能提交状态为审核被拒的设备变动数据！");
                return;
            }
        }

        private void Submited_toolStrip_Click(object sender, EventArgs e)
        {

            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();

            if (drX == null)
            {
                WJs.alert("请选择要审核的设备变动数据！");
                dataGView2.Focus();
                return;
            }

            if (drX["状态"].ToString() == "1")
            {
                DialogResult DialResult = MessageBox.Show("该条设备变动数据经过您的审核，是否通过？", "信息提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (DialResult == DialogResult.Yes)
                {
                    //等待审核
                    //UPDATE LKEQ.EQCARDREC SET DEPTID=?  ,BGPEOPLE=?  WHERE CHOSCODE=? AND CARDID=?
                    //UPDATE LKEQ.EQCARDREC SET YPRICE=?  WHERE CHOSCODE=? AND CARDID=?
                    //UPDATE LKEQ.EQCARDREC SET STATUSCODE=?  WHERE CHOSCODE=? AND CARDID=?
                    if (drX["变动类型"].ToString() == "1")
                    {
                        LData.Exe("UpdateInfoForDPBG_EQChangeManag", null, new object[] { drX["现使用科室ID"].ToString(), drX["现保管员"].ToString(), His.his.Choscode, drX["卡片ID"].ToString() });

                    }
                    if (drX["变动类型"].ToString() == "2")
                    {
                        LData.Exe("UpdateInfoForYPrice_EQChangeManag", null, new object[] { drX["调整后原值"].ToString(), His.his.Choscode, drX["卡片ID"].ToString() });
                    }
                    if (drX["变动类型"].ToString() == "3")
                    {
                        LData.Exe("UpdateInfoForStatusCode_EQChangeManag", null, new object[] { drX["新使用状态编码"].ToString(), His.his.Choscode, drX["卡片ID"].ToString() });
                    }
                    //UPDATE LKEQ.EQCARDCHANGEREC SET STATUS=6,SHUSERID=?,SHUSERNAME=?,SHDATE=? WHERE CHANGEID=? AND CHOSCODE=?
                    LData.Exe("UpdateStatusForSH_EQChangeManag", null, new object[] { "6", His.his.UserId.ToString(), His.his.UserName, DateTime.Now, drX["变动ID"].ToString(), His.his.Choscode });
                    WJs.alert("审核成功！");
                    refresh_toolStrip_Click(null, null);
                }
                else if (DialResult == DialogResult.No)
                {
                    LData.Exe("UpdateStatusForSH_EQChangeManag", null, new object[] { "2", "", "", "", drX["变动ID"].ToString(), His.his.Choscode });
                    WJs.alert("已设置为审核被拒！");
                }
                else
                {
                    return;
                }
            }
            else
            {
                WJs.alert("只能对等待审核的设备变动数据进行审核处理！");
                return;

            }
        }

        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drZ == null || drX == null)
            {
                WJs.alert("请选择要浏览的设备变动信息！");
                return;
            }
            EQChangeManagEdit form = new EQChangeManagEdit(drZ, drX, 0);
            form.ShowDialog();
        }

        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            Dictionary<string, ObjItem> drX = dataGView2.getRowData();
            if (drZ == null || drX == null)
            {
                WJs.alert("请选择要编辑的设备变动信息！");
                return;
            }
            if (drX["状态"].ToString() == "1" || drX["状态"].ToString() == "2")
            {
                EQChangeManagEdit form = new EQChangeManagEdit(drZ, drX, 1);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对等待审核或审核被拒的设备变动信息进行编辑！");
                return;
            }
        }

        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drZ = dataGView1.getRowData();
            if (drZ == null)
            {
                WJs.alert("请选择要新增设备变动的卡片信息！");
                return;
            }
            //新增  全能  状态
            if (drZ["状态"].ToString() == "2")
            {
                EQChangeManagEdit form = new EQChangeManagEdit(drZ, null, 2);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("只能对已启用的卡片设备添加变动信息！");
                return;
            }
        }
    }
}
