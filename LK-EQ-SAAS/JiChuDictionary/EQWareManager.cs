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
using YtWinContrl.com;
using ChSys;
using YtClient;
using YtUtil.tool;
using YiTian.db;
using JiChuDictionary.form;


namespace JiChuDictionary
{
    public partial class EQWareManager : Form, IPlug
    {  
      

        public EQWareManager()
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

        private void EQWareManager_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            //注意这里要写好对应的opt
            this.dataGView1.Url = "FindEQWares";
            TvList.newBind().add("库房编码", "1").add("库房名称", "2").add("拼音码", "3").add("五笔码", "4").add("模糊查找", "5").Bind(this.Search_ytComboBox1);
            this.Search_ytComboBox1.SelectedIndex = 4;
            this.dataGView1.IsPage = true;
            this.Search_ytComboBox1.TextChanged += new EventHandler(Search_ytComboBox1_TextChanged);

        }

        void Search_ytComboBox1_TextChanged(object sender, EventArgs e)
        {
            this.Search_yTextBox1.Text = "";
        }

        private void AddtoolStripButton_Click(object sender, EventArgs e)
        {

            AddEQ form = new AddEQ(null, 2);
            form.ShowDialog();
            button1_Click(null, null);
        }

        private void ModifytoolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddEQ form = new AddEQ(dr, 1);
                form.ShowDialog();
                button1_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要修改的设备库房信息！");
            }
        }

        private void ReftoolStripButton_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReftoolStripButton_Click(null, null);
        }

        public void reLoad()
        {
            SqlStr sqlc = SqlStr.newSql();
            this.dataGView1.Url = "FindEQWares";

            if (this.Search_yTextBox1.Text.Trim().Length > 0)
            {
                string strF = null;
                if (this.Search_ytComboBox1.SelectedIndex > -1)
                {
                    strF = this.Search_yTextBox1.Text.Trim();
                    //在获取用户输入的前提下，加入下拉框中的内容 类似stringbuilder
                    if (this.Search_ytComboBox1.SelectedIndex == 0)
                    {
                        sqlc.Add("and (warecode =?)", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 1)
                    {
                        sqlc.Add("and (warename =?)", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 2)
                    {
                        sqlc.Add("and (upper(pycode) =upper(?))", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 3)
                    {
                        sqlc.Add("and (upper(wbcode) =upper(?))", strF);
                    }
                    if (this.Search_ytComboBox1.SelectedIndex == 4)
                    {
                        strF = "%" + this.Search_yTextBox1.Text.Trim() + "%";
                        sqlc.Add(" and (warecode like ? or warename like ? or upper(PYCODE) like upper(?) or upper(WBCODE) like upper(?))", strF, strF, strF, strF);
                    }
                }
                else
                {
                    WJs.alert("请选择查询条件！");
                }
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode }, sqlc);
        }

        private void ViewtoolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                AddEQ form = new AddEQ(dr, 0);
                form.ShowDialog();
            }
            else
            {
                WJs.alert("请选择需要查看的库房信息！");
            }
        }

        private void DeltoolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirmFb("您确定要删除选择的设备库房吗？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQWare";
                    ac.Sql = "Del";
                    ac.Add("CHOSCODE", His.his.Choscode.ToString());
                    ac.Add("WARECODE", dr["库房编码"].ToString());
                    ac.SetKeyValue("WARECODE,CHOSCODE");
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                    reLoad();
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备库房信息！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        private void StoptoolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "1")
                {
                    if (WJs.confirmFb("您确定要停用选择的设备库房吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQWare";
                        ac.Sql = "Disa";
                        ac.Add("ifuse", "0");//这里的0指代停用
                        ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("warecode", dr["库房编码"].ToString());

                        ac.SetKeyValue("warecode,choscode");

                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该设备库房已经停用了");
                }
            }
            else
            {
                WJs.alert("请选择要停用的设备库房信息！");
            }
        }

        private void UsetoolStripButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["是否使用"].ToString() == "0")
                {
                    if (WJs.confirmFb("您确定要启用选择的设备库房吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQWare";
                        ac.Sql = "Enab";
                        ac.Add("ifuse", "1");
                        ac.Add("choscode", His.his.Choscode.ToString());
                        ac.Add("warecode", dr["库房编码"].ToString());

                        ac.SetKeyValue("warecode,choscode");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        reLoad();
                    }
                }
                else
                {
                    WJs.alert("该设备库房已经启用了");
                }
            }
            else
            {
                WJs.alert("请选择要启用的设备库房信息！");
            }
        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewtoolStripButton_Click(null, null);
        }


    }
}
