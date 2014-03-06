using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtUtil.tool;
using YtWinContrl.com.datagrid;
using YtClient;

namespace EQWareManag.form
{
    public partial class EQPanDian_Select : Form
    {
        public EQPanDian_Select()
        {
            InitializeComponent();
        }
        string ware = "";
        public EQPanDian_Select(string val)
        {
          this.ware = val;
            InitializeComponent();
        }
        string a;
        public EQPanDian Main;

        public void callback(string a)//子窗体成功保存后，调用改方法
        {
            Main.callback(a);
        }

        private void EQPanDian_Select_Load(object sender, EventArgs e)
        {
             // WJs.SetDictTimeOut();//什么意思？
            ytTreeView1.vFiled = "KINDCODE";
            ytTreeView1.tFiled = "KINDNAME";
            ytTreeView1.pFiled = "SUPERCODE";
         
            string b = LData.Es("EQPDFind_WARENAME", null, new object[] { His.his.Choscode, ware });
          this.yTextBox_Ware.Text = b;
          this.yTextBox_Ware.ReadOnly = true;
            ytTreeView1.sql = "ScanEQKind";
            this.ytTreeView1.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);
            this.ytTreeView1.reLoad(new object[] { His.his.Choscode });

          
        }

        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)
        {


            //显示所有信息
            //e.FmtText = e.row["KINDNAME"].ToString() + "       |上级编码=" + e.row["SUPERCODE"] + "  |类别名称=" + e.row["KINDCODE"] + "  |拼音码=" + e.row["PYCODE"] + "  |五笔码=" + e.row["WBCODE"] + "  |是否末节点=" + e.row["IFEND"] + "  |是否使用=" + e.row["IFUSE"] + "  |操作员ID=" + e.row["USERID"] + "  |操作员姓名=" + e.row["USERNAME"] + "  |修改时间=" + e.row["RECDATE"] + "  |医疗机构编码=" + e.row["CHOSCODE"];

           
            e.FmtText = e.row["KINDNAME"].ToString() ;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

            if (WJs.confirmFb("是否确定退出新增？"))
            {
                this.Close();
            }


        }


        //子节点选中，则父节点也选中
        private void ChangeParentNodeCheckIfChildCheck(TreeNode tn)
        {
            
            if ((tn == null) || (tn.Parent == null))
            {
                return;
            }
            if (tn.Checked)
            {
                tn.Parent.Checked = true;
                ChangeParentNodeCheckIfChildCheck(tn.Parent);
            }
        }
        //当前节点不选中则所有子节点都不选中
        private void ChangeParentNodeCheckIfChildCheck1(TreeNode tn)
        {
           
            //参数检测
            if ((tn == null) || (tn.Parent == null))
            {
                return;
            }
            if (tn.Checked == false)
            {
                foreach (TreeNode ChildNode in tn.Nodes)
                {
                    ChildNode.Checked = false;
                    ChangeParentNodeCheckIfChildCheck1(ChildNode);

                }

            }
           
        }
        //当前节点不选中则如果所有兄弟节点都不选中，则父节点不选中
                private void ChangeParentNodeCheckIfChildCheck3(TreeNode tn)
        {
           
            //参数检测
            if ((tn == null) || (tn.Parent == null))
            {
                return;
            }
            if (tn.Checked == false)
            {
                foreach (TreeNode brotherNode in tn.Parent.Nodes)
                {
                    if (!brotherNode.Checked)
                    {
                        tn.Parent.Checked = false;
                    }
                    else
                    {
                        tn.Parent.Checked = true;
                        break;
                    }

                }
                if (!tn.Parent.Checked)
                {
                    ChangeParentNodeCheckIfChildCheck3(tn.Parent);
                }

            }
           
        }
       
        //父节点选中则所有子节点都选中
        private void ChangeParentNodeCheckIfChildCheck2(TreeNode tn)
        {

            //参数检测
            if ((tn == null) || (tn.Parent == null))
            {
                return;
            }
            if (tn.Checked == true)
            {

                foreach (TreeNode ChildNode in tn.Nodes)
                {
                    ChildNode.Checked = true;
                    ChangeParentNodeCheckIfChildCheck2(ChildNode);
                }
            }

        }



        //获取树节点中所有被选中节点的字符串组合
        private string GetTreeNodeCheckNodes(TreeNode tn)
        {
            //参数检测
            if (tn == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            if (tn.Checked)
            {
                if (tn.Nodes.Count == 0)
                {
                    sb.Append(tn.Text + "$");
                }
            }
            foreach (TreeNode childNode in tn.Nodes)
            {
                sb.Append(GetTreeNodeCheckNodes(childNode));
            }
            return sb.ToString();
        }

       

  private void ytTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
     
      ChangeParentNodeCheckIfChildCheck1(e.Node);
      ChangeParentNodeCheckIfChildCheck(e.Node);
      ChangeParentNodeCheckIfChildCheck2(e.Node);
      ChangeParentNodeCheckIfChildCheck3(e.Node);
  }

  private void btn_Save_Click(object sender, EventArgs e)
  {

      string b = GetTreeNodeCheckNodes(this.ytTreeView1.SelectedNode);
      if (b == "")
      {
          WJs.alert("请选择需要盘点的类别！");
          return;
      
      }
         string[] sr= b.Split('$');
         if (this.checkBox_IsZero.Checked == true)
         {
             EQPanDian_DetailScan ks = new EQPanDian_DetailScan(sr, ware, this.yTextBox_Ware.Text, true, true);
             ks.EQSlect = this;
             ks.ShowDialog();
         }
         else
         {
             EQPanDian_DetailScan ks = new EQPanDian_DetailScan(sr, ware, this.yTextBox_Ware.Text, true, false);
             ks.EQSlect = this;
             ks.ShowDialog();
         }
      
      // ks.Main = this;
         this.Close();
     
      
  }


    }
}
