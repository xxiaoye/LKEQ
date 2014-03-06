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
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;
using YiTian.db;
using EQWareManag.form;

namespace EQWareManag
{
    public partial class EQBulidCard : Form, IPlug
    {
        public EQBulidCard()
        {
            InitializeComponent();
            this.selTextInpt1.Sql = "FindDeptidInCard";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            //设置当前与上级节点对应的字段及显示的文本信息
            //WJs.SetDictTimeOut();
            EQKind_ytTreeView.vFiled = "KINDCODE";
            EQKind_ytTreeView.tFiled = "KINDNAME";
            EQKind_ytTreeView.pFiled = "SUPERCODE";
            EQKind_ytTreeView.pValue = "";
            EQKind_ytTreeView.sql = "FindEQKindInCard";
            EQKind_ytTreeView.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(EQKind_ytTreeView_FormatText);

            UseStatus_ytTreeView.vFiled = "STATUSCODE";
            UseStatus_ytTreeView.tFiled = "STATUSNAME";
            UseStatus_ytTreeView.pFiled = "EQID";//这是在是被逼的没办法想的招
            UseStatus_ytTreeView.pValue = "";
            UseStatus_ytTreeView.sql = "FindUseStatusInCard";
            UseStatus_ytTreeView.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(UseStatus_ytTreeView_FormatText);

            ReLoad();
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

        void UseStatus_ytTreeView_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
            e.FmtText = "[" + e.row["STATUSCODE"].ToString() + "]" + e.row["STATUSNAME"].ToString();
        }
        void EQKind_ytTreeView_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
            e.FmtText = "[" + e.row["KINDCODE"].ToString() + "]" + e.row["KINDNAME"].ToString();
        }
        void ReLoad()
        {
            EQKind_ytTreeView.reLoad(new object[] { His.his.Choscode.ToString() });
            UseStatus_ytTreeView.reLoad(new object[] { His.his.Choscode.ToString() });
        }

        private void EQBulidCard_Load(object sender, EventArgs e)
        {
            EQKind_ytTreeView.SelectedNode = EQKind_ytTreeView.Nodes[0];
            UseStatus_ytTreeView.SelectedNode = UseStatus_ytTreeView.Nodes[0];
            this.dataGView1.Url = "LoadCardInfo";
            BindStatusCode();
            DeptidBind();
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_Column);
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").add("全部", "10").Bind(ytComboBox1);
            this.ytComboBox1.SelectedIndex = 5;
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
        //查询
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
            DataRow dr2 = UseStatus_ytTreeView.getSelectRow();
            if (EQKind_ytTreeView.SelectedNode != EQKind_ytTreeView.Nodes[0])
            {
                sqls.Add(" AND EQID IN (SELECT EQID FROM LKEQ.DICTEQ WHERE KINDCODE= " + dr1["KINDCODE"].ToString() + ")");
            }

            if (UseStatus_ytTreeView.SelectedNode != UseStatus_ytTreeView.Nodes[0])
            {
                sqls.Add(" AND STATUSCODE=" + dr2["STATUSCODE"].ToString());
            }

            if (this.ytComboBox1.SelectedIndex == 0)
            {
                sqls.Add(" AND STATUS=0 ");

            }
            if (this.ytComboBox1.SelectedIndex == 1)
            {
                sqls.Add(" AND STATUS=1 ");

            }
            if (this.ytComboBox1.SelectedIndex == 2)
            {
                sqls.Add(" AND STATUS=2 ");

            }
            if (this.ytComboBox1.SelectedIndex == 3)
            {
                sqls.Add(" AND STATUS=6 ");

            }
            if (this.ytComboBox1.SelectedIndex == 4)
            {
                sqls.Add(" AND STATUS=7 ");

            }
            else
            {
                sqls.Add(" AND  1=1 ");
            }

            this.dataGView1.reLoad(new object[] { this.selTextInpt1.Value, His.his.Choscode.ToString() }, sqls);
            
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("价格").ToString() + "元";
            this.label6.Text = this.dataGView1.Sum("原值").ToString() + "元";
        }
        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
        //删除（只能删除状态为1的卡片数据；只需要将要删除的设备卡片记录的状态设置为0即可。）
        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要设置为无效的设备卡片记录！");
                return;
            }
            if (dr["状态"].ToString() == "1")
            {
                if (WJs.confirm("您确定要将该条设备卡片记录置为无效么？"))
                {
                    LData.Exe("UpdateCardInfoStatusToDel", null, new object[] { "0", His.his.UserName, DateTime.Now, dr["卡片ID"].ToString(), His.his.Choscode.ToString() });
                    WJs.alert("执行成功！");
                    refresh_toolStrip_Click(null, null);
                }
            }
            else
            {
                WJs.alert("只能将状态为未启用的设备卡片记录置为无效！");
            }
        }
        private void QiYong_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要启用的设备卡片记录！");
                return;
            }
            if (dr["状态"].ToString() == "1")
            {
                if (WJs.confirm("您确定要启用该条设备卡片记录么？"))
                {
                    LData.Exe("UpdateCardInfoStatus", null, new object[] { "2", His.his.UserName, DateTime.Now, dr["卡片ID"].ToString(), His.his.Choscode.ToString() });
                    WJs.alert("执行成功！");
                    refresh_toolStrip_Click(null, null);
                }
            }
            else
            {
                WJs.alert("只能启用状态为未启用的设备卡片记录！");
            }
        }
        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要浏览的设备卡片记录！");
                return;
            }
            //0 为浏览
            AddEQBuildCard form = new AddEQBuildCard(dr, 0);
            form.ShowDialog();
        }
        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要编辑的设备卡片记录");
                return;
            }
            if (dr["状态"].ToString() != "0" && dr["状态"].ToString() != "7")
            {
                AddEQBuildCard form = new AddEQBuildCard(dr, 1);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("状态为无效或者已冲销的数据无法进行编辑！");
                return;
            }
        }
        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            AddEQBuildCard form = new AddEQBuildCard(null, 2);
            form.ShowDialog();
            if (selTextInpt1.Text != "" || selTextInpt1.Value != null)
            {
                refresh_toolStrip_Click(null, null);
            }
        }
        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }
    }
}
