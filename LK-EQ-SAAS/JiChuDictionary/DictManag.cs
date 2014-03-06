using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using JiChuDictionary.form;
using YtPlugin;
using YtUtil.tool;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDictionary
{
    public partial class DictManag : Form,IPlug
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

        public DictManag()
        {
            InitializeComponent();
        }
        int rowid = 0;
        private void WZDictManag_Load(object sender, EventArgs e)//窗体加载
        {
            WJs.SetDictTimeOut();//什么意思？
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column22);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.Column14);

            TvList.newBind().add("平均年限法", "1").add("工作量法", "2").add("年折旧率", "3").add("手工折旧", "4").Bind(this.Column15);



            TvList.newBind().Load("EQDict_KindCode", new object[] { His.his.Choscode }).Bind(this.Column9);
            TvList.newBind().Load("EQDict_CountCode", new object[] { His.his.Choscode }).Bind(this.Column10); //这里修改了，查询所有

            TvList.newBind().Load("EQDict_SingerCode", null).Bind(this.Column12);
            TvList.newBind().Load("EQDict_WORKUNITCODE", null).Bind(this.Column13);
          
            ytTreeView_Main.vFiled = "KINDCODE";
            ytTreeView_Main.tFiled = "KINDNAME";
            ytTreeView_Main.pFiled = "SUPERCODE";
            ytTreeView_Main.sql = "ScanEQKind";
            this.ytTreeView_Main.FormatText += new YtWinContrl.com.events.TextFormatEventHandle(ytTreeView1_FormatText);


            this.ytTreeView_Main.reLoad(new object[] { His.his.Choscode });
         
        }
        void ytTreeView1_FormatText(YtWinContrl.com.events.TextFormatEvent e)//显示节点
        {

          
            e.FmtText = e.row["KINDNAME"].ToString();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)//浏览
        {

            ytTreeView_Main.sql = "ScanEQKind";
            this.ytTreeView_Main.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Main.ClearData();
          
        }
        public void ReLoadData()
        {
            DataRow r1 = this.ytTreeView_Main.getSelectRow();

            //ytTreeView1.sql = "ScanWZKind";
            //this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Main.Url = "FistScanEQDict";
            this.dataGView_Main.reLoad(new object[] { r1["KINDCODE"], His.his.Choscode });
            if (this.dataGView_Main.RowCount <=rowid)
            {
                this.dataGView_Main.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Main.setFocus(rowid, 0);
            }
        }

        public void ReLoadData( string KindCode)
        {
           // DataRow r1 = this.ytTreeView_Main.getSelectRow();

            ////ytTreeView1.sql = "ScanWZKind";
           // //this.ytTreeView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView_Main.Url = "FistScanEQDict";
           // this.dataGView_Main.reLoad(new object[] { r1["KINDCODE"], His.his.Choscode });
            this.dataGView_Main.reLoad(new object[] { KindCode, His.his.Choscode });
           
            if (this.dataGView_Main.RowCount <= rowid)
            {
                this.dataGView_Main.setFocus(0, 0);

            }
            else
            {
                this.dataGView_Main.setFocus(rowid, 0);
            }
        }
       
      

        private void toolStripButton1_Click(object sender, EventArgs e)//新增
        {
            DataRow r= this.ytTreeView_Main.getSelectRow();

              if (r != null)
            {
                    EQDict_Add ks = new EQDict_Add(r, true);
                    ks.WZDM = this;
                    ks.ShowDialog();
            }

              else
              {
                  WJs.alert("请选择要增加的设备类别！");
              }
            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//修改
        {
            
            DataRow r = this.dataGView_Main.GetRowData();
          
            if (r != null)
            {
                rowid = this.dataGView_Main.CurrentRow.Index;
                EQDict_Add ks = new EQDict_Add(r, false);
                ks.WZDM = this;
                ks.ShowDialog();
              
            }
           
            else
            {
                WJs.alert("请选择要编辑的设备信息");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)//成功返回事件
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
         
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
           
            DataRow r7 = this.dataGView_Main.GetRowData();
            if (r7 != null)
            {
                if (WJs.confirmFb("确定要删除选择的设备信息吗？\n删除后不能恢复!"))
                {


                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDictSvr";
                    ac.Sql = "DelEQDictInfo";
                    ac.Add("EQID", r7["EQID"]);
                    ac.Add("IFUSE", r7["IFUSE"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.Add("flag", "0");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备信息");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//启用
        {
            DataRow r8 = this.dataGView_Main.GetRowData();
            if (r8 != null)
            {
                if (WJs.confirmFb("是否启用？"))
                {
                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDictSvr";
                    ac.Sql = "StartEQDictInfo";
                    ac.Add("EQID", r8["EQID"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要启用的设备信息");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//停用
        {
            DataRow r9 = this.dataGView_Main.GetRowData();
            if (r9 != null)
            {
                if (WJs.confirmFb("是否停用？"))
                {
                    rowid = this.dataGView_Main.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDictSvr";
                    ac.Sql = "StopEQDictInfo";
                    ac.Add("EQID", r9["EQID"]);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要停用的设备信息");
            }
        }

        private void button2_Click(object sender, EventArgs e)//清空
        {

            this.yTxtBox_Name.Text = "";
       
        }

        private void button1_Click(object sender, EventArgs e)//查询
        {
            SqlStr sql = SqlStr.newSql();//创建SqlStr对象
            this.dataGView_Main.Url = "FindEQDict";
          
          
          
            if (this.yTxtBox_Name.Text.Trim().Length > 0)
            {
                //添加查询条件及其参数
                sql.Add("and ( a.EQID like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.EQNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or upper(a.PYCODE) like upper(?)", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or upper(a.WBCODE) like upper(?)", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.SHORTCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASNAME like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add("or a.ALIASCODE like ?", "%" + this.yTxtBox_Name.Text.Trim() + "%");
                sql.Add(")");
            }

            this.dataGView_Main.reLoad(new object[] { His.his.Choscode }, sql);


        }

        private void ytTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataRow r = this.ytTreeView_Main.getSelectRow();
                      
            if (r != null )
            {
              
                this.dataGView_Main.Url = "ScanEQDict";
                if(r["IFEND"].ToString()=="1")
                this.dataGView_Main.reLoad(new object[] { r["KINDCODE"], His.his.Choscode });
            }
        }

        private void button3_Click(object sender, EventArgs e) //过滤
        {
            DataRow r12 = this.ytTreeView_Main.getSelectRow();
          
            if (r12 != null)
            {
                
               
                    this.dataGView_Main.Url = "FilterEQDictInfo";
                    this.dataGView_Main.reLoad(new object[] { r12["KINDCODE"].ToString()+"%", His.his.Choscode });
                
            }
            else
            {
                WJs.alert("请选择要过滤的设备信息");
            }
        }

        private void dataGView_Main_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow r = this.dataGView_Main.GetRowData();

            if (r != null)
            {
                rowid = this.dataGView_Main.CurrentRow.Index;
                EQDict_Add ks = new EQDict_Add(r, false, true);
                ks.WZDM = this;
                ks.ShowDialog();

            }

            else
            {
                WJs.alert("请选择要浏览的设备信息");
            }
        }


    }
}
