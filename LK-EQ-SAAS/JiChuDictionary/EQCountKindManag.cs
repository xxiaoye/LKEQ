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
using ChSys;
using YtWinContrl.com.datagrid;
using YtClient;
using JiChuDictionary.form;

namespace JiChuDictionary
{
    public partial class EQCountKindManag : Form, IPlug
    {

        private string CurrentFindContent = "";//当前输入的搜索关键字
        private int CurrentFindType = 0;// 什么类型去查找
        private TreeNode CurrentPnode = null;//找到的节点

        public EQCountKindManag()
        {
            InitializeComponent();
        }

        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {

        }

        private void init()
        {
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        private void EQCountKindManag_Load(object sender, EventArgs e)
        {
            //五笔码 编码 名称  拼音码
            WJs.SetDictTimeOut();
            this.Search_toolStripComboBox.SelectedIndex = 2;
            ytTreeView1.vFiled = "COUNTCODE";//本级的值
            ytTreeView1.tFiled = "COUNTNAME";//本级文本

            ytTreeView1.pFiled = "SUPERCODE";//父节点的值
            ytTreeView1.pValue = "";
            ytTreeView1.sql = "FindEQCountKindInfo";
            this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);
            ReLoadData();
        }
        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {
            string a = "";
            string b = "";
            if (e.row["IFUSE"].ToString() == "1")
            {
                a = "启用";
            }
            if (e.row["IFUSE"].ToString() == "0")
            {
                a = "停用";
            }
            if (e.row["IFEND"].ToString() == "1")
            {
                b = "末节点";
            }
            else
            {
                b = "非末节点";
            }
            e.FmtText = e.row["COUNTNAME"].ToString() + "          |" + a;// +"          |" + b;
        }

        public void ReLoadData()
        {
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            this.ytTreeView1.ExpandAll();
        }

        //针对删除
        private void Del_toolStripButton_Click(object sender, EventArgs e)
        {
            DataRow row = this.ytTreeView1.getSelectRow();
            if (row != null)
            {
                if (WJs.confirmFb("您确定要删除该统计类别信息？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQCountKindSvr";
                    ac.Sql = "Del";
                    ac.Add("CHOSCODE", row["CHOSCODE"].ToString());
                    ac.Add("COUNTCODE", row["COUNTCODE"].ToString());
                    ac.Add("SUPERCODE", row["SUPERCODE"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备统计信息！");
            }
        }

        private void Enable_toolStripButton_Click(object sender, EventArgs e)
        {
            //针对启用
            DataRow dr = this.ytTreeView1.getSelectRow();
            if (dr != null)
            {
                if (dr["IFUSE"].ToString() == "0")
                {
                    if (WJs.confirmFb("您确定要启用选择的统计类别信息吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQCountKindSvr";
                        ac.Sql = "QiYong";
                        ac.Add("IFUSE", "1");
                        ac.Add("CHOSCODE", His.his.Choscode.ToString());
                        ac.Add("COUNTCODE", dr["COUNTCODE"]);
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                }
                else
                {
                    WJs.alert("该统计类别信息已经启用了！");
                }
            }
            else
            {
                WJs.alert("请选择要启用的统计类别信息！");
            }
        }
        private void Stop_toolStripButton_Click(object sender, EventArgs e)
        {
            //停用
            DataRow dr = this.ytTreeView1.getSelectRow();
            if (dr != null)
            {
                if (WJs.confirmFb("您确定要停用该统计类别信息吗？"))
                {
                    if (dr["IFUSE"].ToString() == "1")
                    {
                        if (dr["IFEND"].ToString() == "1")
                        {
                            ActionLoad ac = ActionLoad.Conn();
                            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQCountKindSvr";
                            ac.Sql = "TingYong";
                            ac.Add("IFUSE", "0");
                            ac.Add("CHOSCODE", His.his.Choscode.ToString());
                            ac.Add("COUNTCODE", dr["COUNTCODE"]);
                            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                            ac.Post();
                        }
                        else
                        {
                            WJs.alert("只能停用末节点【即该节点不再包含子节点】的统计类别信息！");
                        }
                    }
                    else
                    {
                        WJs.alert("该统计类别信息已经停用了！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要停用的统计类别信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
        }



        private void Add_toolStripButton_Click(object sender, EventArgs e)
        {
            object[] r = null;
            //若选中的当前节点不为null[即不为初始的根节点]
            if (this.ytTreeView1.getSelectRow() != null)
            {
                r = this.ytTreeView1.getSelectRow().ItemArray;
                if (r[6].ToString() == "0")
                {
                    WJs.alert("已经停用的节点无法继续添加子节点,请修改后操作！");
                    return;
                }

                r[1] = r[0];
                //COUNTCODE,SUPERCODE,COUNTNAME,PYCODE,WBCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
                r[0] = r[1].ToString() + (this.ytTreeView1.SelectedNode.GetNodeCount(false) + 1).ToString("00");
            }
            else
            {
                r = new object[] { "", "0", "", "", "", 1, 0, "", His.his.UserId, His.his.UserName, DateTime.Now.ToString(), His.his.Choscode };
                r[0] = (this.ytTreeView1.SelectedNode.GetNodeCount(false) + 1).ToString("00");
            }

            r[2] = "";
            r[3] = "";
            r[4] = "";
            r[5] = 1;
            r[6] = 1;
            r[7] = "";
            r[8] = His.his.UserId;
            r[9] = His.his.UserName;
            r[10] = DateTime.Now.ToString();
            r[11] = His.his.Choscode;

            AddEQCountKind form = new AddEQCountKind(r, true);
            form.ShowDialog();
            ReLoadData();

        }

        private void Edit_toolStripButton_Click(object sender, EventArgs e)
        {
            if (ytTreeView1.SelectedNode == ytTreeView1.Nodes[0])
            {
                WJs.alert("不能编辑初始根节点！");
                return;
            }
            object[] r = null;
            r = this.ytTreeView1.getSelectRow().ItemArray;
            if (r != null)
            {
                //编辑的时候，直接将现在的数据传递过去
                AddEQCountKind form = new AddEQCountKind(r, false);
                form.ShowDialog();
                ReLoadData();
            }
            else
            {
                WJs.alert("请选择要编辑的设备统计信息！");
            }
        }


        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            CurrentFindContent = this.toolStripTextBox1.Text.Trim();
            //搜索时从顶部开始循环
            CurrentPnode = this.ytTreeView1.TopNode;
        }
        private void Search_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            CurrentFindType = this.Search_toolStripComboBox.SelectedIndex;
            //搜索时从顶部开始循环
            CurrentPnode = this.ytTreeView1.TopNode;

        }


        #region 搜索功能 - SearchtoolStripButton1_Click
        private void SearchtoolStripButton1_Click(object sender, EventArgs e)
        {
            FindNextNode();
        }

        //从最顶节点开始搜索  而当前节点就是顶级节点下的第一个节点
        void FindNextNode()
        {
            if (CurrentPnode.Nodes.Count > 0)
            {
                CurrentPnode = CurrentPnode.Nodes[0];
            }
            else
            {
                //若当前节点下再无子节点[末节点]
                CurrentPnode = GetUnderNode(CurrentPnode);
                if (CurrentPnode == null)
                {
                    WJs.confirm("搜索完毕，没有发现新的符合项，将从头开始");
                    CurrentPnode = this.ytTreeView1.TopNode;
                    return;
                }
            }
            DataRow row = this.ytTreeView1.GetRow(CurrentPnode);
            if (row[CurrentFindType].ToString().Contains(CurrentFindContent))
            {
                this.ytTreeView1.SelectedNode = CurrentPnode;
            }
            else
            {
                FindNextNode();
            }
        }
        //找下级节点
        TreeNode GetUnderNode(TreeNode tn)
        {
            //当前的节点为空，直接返回null
            if (tn == null)
            {
                return null;
            }
            //若同级节点不为空，则返回下一个同级节点
            if (tn.NextNode != null)
            {
                return CurrentPnode.NextNode;
            }
            else
            {
                //返回父级节点
                CurrentPnode = tn.Parent;
                return GetUnderNode(tn.Parent);
            }
        }

        #endregion

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            if (this.toolStripTextBox1.Text.Trim() == "查找关键字")
            {
                this.toolStripTextBox1.Text = "";
            }
        }
        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            if (this.toolStripTextBox1.Text.Trim() == "")
            {
                this.toolStripTextBox1.Text = "查找关键字";
            }
        }
    }
}
