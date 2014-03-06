using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JiChuDictionary.form;
using YtUtil.tool;
using YtClient;
using YtPlugin;
using ChSys;
using YtWinContrl.com.datagrid;


namespace JiChuDictionary
{
    public partial class KindManag : Form,IPlug
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


        public KindManag()
        {
            InitializeComponent();
        }
        string tr = "";
        string a;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();

            Kind_Add ks = new Kind_Add(r, true);
            ks.pr = this;
            ks.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {


                tr = this.ytTreeView1.SelectedNode.Text;
                string[] strr1 = tr.Split('|');
                tr = strr1[0];
                tr = tr.Trim();
                Kind_Add ks = new Kind_Add(r, false);
                ks.pr = this;
                ks.ShowDialog();
               
            }
            else
            {
                WJs.alert("请选择要编辑的设备信息");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {


                if (WJs.confirmFb("确定要删除选择的设备类别吗？\n删除后不能恢复!"))
                {
                    string values = LData.Es("EQKind_DelScan", null, new object[] { r["KINDCODE"], r["CHOSCODE"] });
                    if (values != null)
                    {
                        WJs.alert("不能删除已使用的设备，只能停用!");
                        return;
                    }

                
                    tr = this.ytTreeView1.SelectedNode.Parent.Text;//获得其父节点为选中节点
                    string[] str = tr.Split('|');
                    tr = str[0];
                    tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQKind";
                    ac.Sql = "DelEQKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("SUPERCODE", r["SUPERCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的物资类别！");
                return;
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)//执行成功返回事件
        {
            WJs.alert(e.Msg.Msg);
            toolStripButton4_Click_1(null, null);
            GetAllNodeText(this.ytTreeView1.Nodes);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)//复制
        {
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQKind";
            ac.Sql = "CopyEQKindInfo";
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post(); 
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//启用
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("是否启用？"))
                {
                    tr = this.ytTreeView1.SelectedNode.Text;
                    string[] str = tr.Split('|');
                    tr = str[0];
                    tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQKind";
                    ac.Sql = "StartEQKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的设备类别");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//停用
        {
            DataRow r = this.ytTreeView1.getSelectRow();
            if (r != null)
            {
                if (WJs.confirmFb("是否停用？"))
                {

                    tr = this.ytTreeView1.SelectedNode.Text;
                    string[] str = tr.Split('|');
                    tr = str[0];
                    tr = tr.Trim();
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQKind";
                    ac.Sql = "StopEQKindInfo";
                    ac.Add("KINDCODE", r["KINDCODE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的设备类别");
            }
        }

        //private void button2_Click(object sender, EventArgs e)//清除
        //{
        //    this.textBox1.Text = "";
        //    this.yTxtBox_Name.Text = "";
        //    this.yTxtBox_PY.Text = "";
        //    this.yTxtBox_WB.Text = "";
        //}

        private void button1_Click(object sender, EventArgs e)//查询
        {

            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            ytTreeView1.sql = "ScanEQKind";
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });

            string strF = this.Search_yTextBox1.Text.Trim();
            string strF1 = null;
            string strF2 = null;
            string strF3 = null;
            string strF4 = null;
            if (this.Search_yTextBox1.Text.Trim().Length > 0)
            {

                if (this.Search_ytComboBox1.SelectedIndex > -1)
                {

                    //在获取用户输入的前提下，加入下拉框中的内容 类似stringbuilder
                    if (this.Search_ytComboBox1.SelectedIndex == 0)
                    {
                        strF1 = strF;
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 1)
                    {
                        strF2 = strF;
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 2)
                    {
                        strF3 = strF;
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 3)
                    {
                        strF4 = strF;
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 4)
                    {
                        strF1 = "%" + strF + "%"; strF2 = "%" + strF + "%"; strF3 = "%" + strF + "%"; strF4 = "%" + strF + "%";
                      
                    }
                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }
            }
            

            string text = LData.Es("FindEQKind", null, new object[] { His.his.Choscode, strF1, strF2, strF3, strF4 });

            FindNode(this.ytTreeView1.Nodes, text);
         
        }

        void FindNode(TreeNodeCollection tnc, string str)//遍历整棵数，如果Text相等则被选中node.Text
        {
            foreach (TreeNode node in tnc)
            {


                if (node.Nodes.Count != 0)
                    FindNode(node.Nodes, str);
                if (strsplit(node.Text) == str)
                {
                    this.ytTreeView1.SelectedNode = node;
                    break;

                };

            }

        }

        private void EQKindManag_Load(object sender, EventArgs e)//加载窗体
        {
            ytTreeView1.vFiled = "KINDCODE";
            ytTreeView1.tFiled = "KINDNAME";
            ytTreeView1.pFiled = "SUPERCODE";
            tr = null;

            ytTreeView1.sql = "ScanEQKind";
            this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);

            TvList.newBind().add("编码", "1").add("名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找", "5").Bind(this.Search_ytComboBox1);
            this.Search_ytComboBox1.SelectedIndex = 4;
            this.Search_ytComboBox1.TextChanged += new EventHandler(Search_ytComboBox1_TextChanged);

            toolStripButton4_Click_1(null, null);


        }

        void Search_ytComboBox1_TextChanged(object sender, EventArgs e)
        {
            this.Search_yTextBox1.Text = "";
        }


        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)//函数
        {

            if (e.row["KINDNAME"] != null)
            {
                toolStripButton1.Enabled = false;
            }

            //显示所有信息
            //e.FmtText = e.row["KINDNAME"].ToString() + "       |上级编码=" + e.row["SUPERCODE"] + "  |类别名称=" + e.row["KINDCODE"] + "  |拼音码=" + e.row["PYCODE"] + "  |五笔码=" + e.row["WBCODE"] + "  |是否末节点=" + e.row["IFEND"] + "  |是否使用=" + e.row["IFUSE"] + "  |操作员ID=" + e.row["USERID"] + "  |操作员姓名=" + e.row["USERNAME"] + "  |修改时间=" + e.row["RECDATE"] + "  |医疗机构编码=" + e.row["CHOSCODE"];

            if (e.row["IFUSE"].ToString() == "1")
            {
                a = "启用";
            }
            if (e.row["IFUSE"].ToString() == "0")
            {
                a = "停用";
            }
            e.FmtText = e.row["KINDNAME"].ToString() + "        |" + a;

        }

        private string strsplit(string x)
        {
            string[] strr1 = x.Split('|');
            x = strr1[0];
            x = x.Trim();
            return x;

        }
        void GetAllNodeText(TreeNodeCollection tnc)//遍历整棵数，如果Text相等则被选中node.Text
        {
            foreach (TreeNode node in tnc)
            {


                if (node.Nodes.Count != 0)
                    GetAllNodeText(node.Nodes);
                if (strsplit(node.Text) == tr)
                {
                    this.ytTreeView1.SelectedNode = node;
                    break;

                };

            }

        }

        public void ReLoadData()
        {
            toolStripButton4_Click_1(null, null);
            GetAllNodeText(this.ytTreeView1.Nodes);
        }
        public void ReLoadData(string str)
        {
            tr = str;
            toolStripButton4_Click_1(null, null);
            GetAllNodeText(this.ytTreeView1.Nodes);
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            ytTreeView1.sql = "ScanEQKind";
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
        }

    }
}
