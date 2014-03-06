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
using JiChuDictionary.form;
using YiTian.db;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDictionary
{
    public partial class FixKindManag : Form, IPlug 
    {
        int rowid = 0;
        public FixKindManag()
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

        public bool unLoad()
        {
            return true;
        }

        #endregion



        private void FixKindManag_Load(object sender, EventArgs e)//加载窗体
        {

            
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column8);
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.Column7);
            TvList.newBind().add("维修ID", "1").add("名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找", "5").Bind(this.Search_ytComboBox1);
            this.Search_ytComboBox1.SelectedIndex = 4;
          
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//新增
        {
            FixKind_Add form = new FixKind_Add();
            form.Main = this;
            form.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//浏览
        {
            this.dataGView1.Url = "EQFixKind_Scan";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
        }
        public void ReLoadData()
        {
            this.dataGView1.Url = "EQFixKind_Scan";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            if (this.dataGView1.RowCount <= rowid)
            {
                this.dataGView1.setFocus(0, 0);

            }
            else
            {
                this.dataGView1.setFocus(rowid, 0);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//修改
        {
         
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                    rowid = this.dataGView1.CurrentRow.Index;
                    FixKind_Add form = new FixKind_Add(dr);
                    form.Main = this;
                    form.ShowDialog();
             
            }
            else
            {
                WJs.alert("请选择要编辑的维修类别信息！");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
              if (WJs.confirmFb("确定要删除选择的维修类别信息吗？\n删除后不能恢复!"))
                {


                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQFixKindSvr";
                    ac.Sql = "DelFixKindInfo";
                    ac.Add("REPAIRCODE", dr["类别编码"].ToString());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的维修类别信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
        }

        private void button1_Click(object sender, EventArgs e)//查询
        {
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            //if (this.textBox1.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and REPAIRCODE like ?", "%" + this.textBox1.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_Name.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and REPAIRNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_PY.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and upper(PYCODE) like upper(?)", "%" + this.yTxtBox_PY.Text.Trim() + "%");
            //}
            //if (this.yTxtBox_WB.Text.Trim().Length > 0)
            //{
            //    //添加查询条件及其参数
            //    sql.Add("and upper(WBCODE) like upper(?)", "%" + this.yTxtBox_WB.Text.Trim() + "%");
            //}

            if (this.Search_yTextBox1.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox1.SelectedIndex > -1)
                {
                    strF = this.Search_yTextBox1.Text.Trim();
                    if (this.Search_ytComboBox1.SelectedIndex == 0)
                    {
                        sql.Add("and (REPAIRCODE =?)", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 1)
                    {
                        sql.Add("and (REPAIRNAME =?)", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 2)
                    {
                        sql.Add("and (upper(pycode) =upper(?))", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 3)
                    {
                        sql.Add("and (upper(wbcode) =upper(?))", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 4)
                    {
                        strF = "%" + this.Search_yTextBox1.Text.Trim() + "%";
                        sql.Add(" and (REPAIRCODE like ? or REPAIRNAME like ? or upper(pycode) like upper(?) or upper(wbcode) like upper(?))", strF, strF, strF, strF);
                    }

                }
                else
                {
                    WJs.alert("请选择查询方式！");
                }

            }
            
            
            
            //加载查询数据
            this.dataGView1.Url = "EQFindFixKindInfo";
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sql);


            if (this.dataGView1.RowCount > rowid)
            {
                this.dataGView1.setFocus(0, 1);
            }

            this.dataGView1.setFocus(rowid, 1);
            rowid = 0;
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//停用
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
               if (WJs.confirmFb("是否停用？"))
                {
                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQFixKindSvr";
                    ac.Sql = "StopEQFixKindInfo";
                    ac.Add("REPAIRCODE", dr["类别编码"].ToString());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的厂商信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//启用
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                
                if (WJs.confirmFb("是否启用？"))
                {
                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQFixKindSvr";
                    ac.Sql = "StartEQFixKindInfo";
                    ac.Add("REPAIRCODE", dr["类别编码"].ToString());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的厂商信息");
            }
        }

        //private void button2_Click(object sender, EventArgs e)//清除按钮131216
        //{
        //    this.textBox1.Text = "";
        //    this.yTxtBox_Name.Text = "";
        //    this.yTxtBox_PY.Text = "";
        //    this.yTxtBox_WB.Text = "";
        //}
    


    }
}
