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
using YtWinContrl.com.datagrid;
using ChSys;
using YiTian.db;
using YtClient;
using JiChuDictionary.form;

namespace JiChuDictionary
{
    public partial class EQInOutManag : Form, IPlug
    {
        public EQInOutManag()
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

        private void EQInOutManag_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            this.dataGView1.IsPage = true;
            TvList.newBind().add("入出ID", "0").add("入出名称", "1").add("拼音码", "2").add("五笔码", "3").add("模糊搜索", "4").Bind(this.serachKind_ytComboBox);
            TvList.newBind().add("全部", "0").add("入库", "1").add("出库", "2").Bind(this.Filter_ytComboBox);
            TvList.newBind().add("入库", "0").add("出库", "1").Bind(this.IOFlagColumn);
            TvList.newBind().add("普通", "0").add("调拨", "1").add("申领", "2").add("盘点", "3").Bind(this.OPFlagColumn);
            this.serachKind_ytComboBox.SelectedIndexChanged += new EventHandler(serachKind_ytComboBox_SelectedIndexChanged);
            this.dataGView1.Url = "FindEQInOutInfo";
            initform();
        }

        private void initform()
        {
            this.serachKind_ytComboBox.SelectedIndex = 4;
            this.Filter_ytComboBox.SelectedIndex = 0;
            this.search_yTextBox.Text = "";
        }

        void serachKind_ytComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.search_yTextBox.Text = "";
        }

        //查询函数
        private void button1_Click(object sender, EventArgs e)
        {
            SqlStr strs = SqlStr.newSql();
            string strF = null;

            if (this.search_yTextBox.Text.Trim().Length > 0)
            {
                strF = this.search_yTextBox.Text.Trim();
                if (this.serachKind_ytComboBox.SelectedIndex == 0)
                {
                    strs.Add("AND (IOID=?) ", strF);
                }

                if (this.serachKind_ytComboBox.SelectedIndex == 1)
                {
                    strs.Add("AND (IONAME=?) ", strF);
                }

                if (this.serachKind_ytComboBox.SelectedIndex == 2)
                {
                    strs.Add("AND (upper(PYCODE)=upper(?)) ", strF);
                }

                if (this.serachKind_ytComboBox.SelectedIndex == 3)
                {
                    strs.Add("AND (WBCODE=?) ", strF);
                }

                if (this.serachKind_ytComboBox.SelectedIndex == 4)
                {
                    strF = "%" + this.search_yTextBox.Text.Trim() + "%";
                    strs.Add("AND (IOID LIKE ? OR IONAME LIKE ? or upper(PYCODE) like upper(?) or upper(WBCODE) like upper(?)) ", strF, strF, strF, strF);
                }
            }
            if (this.Filter_ytComboBox.SelectedIndex == 1)
            {
                strs.Add("AND (IOFLAG=0)");
            }
            if (this.Filter_ytComboBox.SelectedIndex == 2)
            {
                strs.Add("AND (IOFLAG=1)");
            }

            this.dataGView1.reLoad(new object[] { His.his.Choscode }, strs);
        }

        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            //先初始化，再直接加载  
            initform();
            button1_Click(null, null);
        }

        //停用该条设备入出库管理
        private void disable_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirm("您确定要停用选中的设备入出库记录吗？"))
                {
                    if (dr["是否使用"].ToString() == "1")
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Sql = "TingYong";
                        ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQInOutSvr";
                        ac.Add("CHOSCODE", His.his.Choscode.ToString());
                        ac.Add("IOID", dr["入出ID"].ToString());
                        ac.Add("IFUSE", "0");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("该设备入出库记录已经被停用，无需修改！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要停用的设备出入库类型记录！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            refresh_toolStrip_Click(null, null);
        }

        //启用该条设备入出库管理
        private void enable_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirm("您确定要启用选中的设备入出库记录吗？"))
                {
                    if (dr["是否使用"].ToString() == "0")
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Sql = "QiYong";
                        ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQInOutSvr";
                        ac.Add("CHOSCODE", His.his.Choscode.ToString());
                        ac.Add("IOID", dr["入出ID"].ToString());
                        ac.Add("IFUSE", "1");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        WJs.alert("该设备入出库记录已经被启用，无需修改！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要启用的设备出入库类型记录！");
            }
        }

        private void del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirm("您确定要删除选中的设备入出库记录吗？"))
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Sql = "Del";
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQInOutSvr";
                    ac.Add("CHOSCODE", His.his.Choscode.ToString());
                    ac.Add("IOID", dr["入出ID"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备出入库类型记录！");
            }
        }

        private void modify_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                AddEQInOut form = new AddEQInOut(dr, false);
                form.ShowDialog();
                refresh_toolStrip_Click(null, null);
            }
            else
            {
                WJs.alert("请选择要修改的设备出入库记录！");
            }
        }

        private void add_toolstrip_Click(object sender, EventArgs e)
        {
            AddEQInOut form = new AddEQInOut(null, true);
            form.ShowDialog();
            refresh_toolStrip_Click(null, null);
        }

        private void copy_toolStrip_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("您确定要复制系统提供的出入库数据到本医疗机构吗？"))
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Sql = "CopyChoscodeData";
                ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQInOutSvr";
                ac.Add("IOID", null);
                ac.Add("IONAME", null);
                ac.Add("PYCODE", null);
                ac.Add("WBCODE", null);
                ac.Add("IFUSE", null);
                ac.Add("RECIPECODE", null);
                ac.Add("RECIPELENGTH", null);
                ac.Add("RECIPEYEAR", null);
                ac.Add("RECIPEMONTH", null);
                ac.Add("MEMO", null);
                ac.Add("IOFLAG", null);
                ac.Add("OPFLAG", null);
                ac.Add("IFDEFAULT", null);
                ac.Add("RECDATE", null);
                ac.Add("USERID", His.his.UserId.ToString());
                ac.Add("USERNAME", His.his.UserName);
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac.Post();
            }
        }
    }
}
