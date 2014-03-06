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
using JiChuDictionary.form;

namespace JiChuDictionary
{
    public partial class EQStatusManag : Form, IPlug
    {
        public EQStatusManag()
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

        public void init()
        {

        }

        #endregion

        private void EQStatusManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.Url = "FindStatusManagInfo";
            this.dataGView1.IsPage = true;
            TvList.newBind().add("状态编码", "0").add("状态名称", "1").add("拼音码", "2").add("五笔码", "3").add("模糊查询", "4").Bind(this.Search_ytComboBox1);
            this.Search_ytComboBox1.SelectedIndex = 4;
            this.Search_ytComboBox1.SelectedIndexChanged += new EventHandler(Search_ytComboBox1_SelectedIndexChanged);

            this.dataGView1.Url = "FindStatusManagInfo";
        }

        void Search_ytComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Search_yTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void Refresh_toolStripButton_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            //STATUSCODE,STATUSNAME,PYCODE,WBCODE,IFUSE,IFDEPRECIATION,IFDEFAULT,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
            SqlStr sqls = SqlStr.newSql();
            if (this.Search_yTextBox1.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox1.SelectedIndex > -1)
                {
                    strF = this.Search_yTextBox1.Text.Trim();
                    if (this.Search_ytComboBox1.SelectedIndex == 0)
                    {
                        sqls.Add(" and ( STATUSCODE =? )", strF);
                    }

                    if (this.Search_ytComboBox1.SelectedIndex == 1)
                    {
                        sqls.Add(" and ( STATUSNAME =? )", strF);
                    }

                    if (this.Search_ytComboBox1.SelectedIndex == 2)
                    {
                        sqls.Add(" and ( upper(PYCODE) =upper(?) )", strF);
                    }

                    if (this.Search_ytComboBox1.SelectedIndex == 3)
                    {
                        sqls.Add(" and ( WBCODE= ? )", strF);
                    }

                    if (this.Search_ytComboBox1.SelectedIndex == 4)
                    {
                        strF = "%" + this.Search_yTextBox1.Text.Trim() + "%";
                        sqls.Add(" and ( STATUSCODE like ? or STATUSNAME like ? or upper(PYCODE) like upper(?) or upper(WBCODE) like upper(?) )", strF, strF, strF, strF);
                    }
                }
                else
                {
                    WJs.alert("选择查询条件");
                }
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqls);
        }

        private void Stop_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "1")
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Sql = "TingYong";
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQStatusSvr";
                    ac.Add("STATUSCODE", dr["状态编码"].ToString());
                    ac.Add("CHOSCODE", His.his.Choscode.ToString());
                    ac.Add("IFUSE", "0");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
                else
                {
                    WJs.alert("该设备状态已经为停用设置，无需更改！");
                }
            }
            else
            {
                WJs.alert("请选择需要停用的设备状态信息！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            this.Refresh_toolStripButton_Click(null, null);
        }

        private void Enable_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "0")
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Sql = "QiYong";
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQStatusSvr";
                    ac.Add("STATUSCODE", dr["状态编码"].ToString());
                    ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                    ac.Add("IFUSE", "1");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
                else
                {
                    WJs.alert("该设备状态已经为启用状态，无需更改！");
                }
            }
            else
            {
                WJs.alert("请选择需要启用的设备状态信息！");
            }
        }

        private void Del_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Sql = "Del";
                ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQStatusSvr";
                ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                ac.Add("STATUSCODE", dr["状态编码"].ToString());
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac.Post();
                this.Refresh_toolStripButton_Click(null, null);
            }
            else
            {
                WJs.alert("请选择需要删除的设备状态信息！");
            }
        }

        private void Edit_toolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddEQStatus form = new AddEQStatus(dr, false);
                form.ShowDialog();
                this.Refresh_toolStripButton_Click(null, null);
            }
            else
            {
                WJs.alert("请选择需要修改的设备状态信息！");
            }
        }

        private void Add_toolStripButton_Click(object sender, EventArgs e)
        {
            AddEQStatus form = new AddEQStatus(null, true);
            form.ShowDialog();
            this.Refresh_toolStripButton_Click(null, null);
        }
    }
}
