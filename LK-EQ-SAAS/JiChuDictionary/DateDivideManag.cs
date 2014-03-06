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
    public partial class DateDivideManag : Form, IPlug 
    {
        int rowid = 0;
        public DateDivideManag()
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



        private void DateDivideManag_Load(object sender, EventArgs e)//加载窗体
        {

           
     
          
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//新增
        {
            DateDivide_Add form = new DateDivide_Add();
            form.Main = this;
            form.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//浏览
        {
            this.dataGView1.Url = "EQDateDivide_Scan";
            this.dataGView1.reLoad(new object[] { His.his.Choscode });
        }
        public void ReLoadData()
        {
            this.dataGView1.Url = "EQDateDivide_Scan";
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
                    DateDivide_Add form = new DateDivide_Add(dr);
                    form.Main = this;
                    form.ShowDialog();
             
            }
            else
            {
                WJs.alert("请选择要编辑的期间信息！");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//删除
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
              if (WJs.confirmFb("确定要删除选择的期间信息吗？\n删除后不能恢复!"))
                {


                    rowid = this.dataGView1.CurrentRow.Index;
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDateDivideSvr";
                    ac.Sql = "DelDateDivideInfo";
                    ac.Add("DATEID", dr["期间划分ID"].ToString());
                    ac.Add("CHOSCODE", His.his.Choscode);
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的期间信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            ReLoadData();
        }



    }
}
