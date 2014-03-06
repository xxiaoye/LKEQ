using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using YiTian.db;
using YtUtil.tool;
using YtClient;
using ChSys;
using YtWinContrl.com;

namespace JiChuDictionary.form
{
    public partial class SetEQDetail : Form
    {
        public EQWareManager Main;
        //private DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
        string wd;

        public SetEQDetail()
        {
            InitializeComponent();
        }

        //public SetEQDetail(DataGView gv, Dictionary<string, ObjItem> dr, string wd)
        public SetEQDetail(Dictionary<string, ObjItem> dr, string wd)
        {
            this.wd = wd;//库房编码
            this.dr = dr;//参数集合
            // this.dataGViewPL = gv;
            InitializeComponent();
        }

        private void InitForm()
        {
            this.kindcode_selTextInpt.Text = "";
        }

        private void SetEQDetail_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            this.warecode_yTextBox.Text = wd;
            this.choscode_yTextBox.Text = His.his.Choscode;
            this.kindcode_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.kindcode_selTextInpt.Sql = "GetEQKindCode";
            //数据的加载[查询]
            this.dataGView1.Url = "ManageEQKind";
            this.dataGView1.reLoad(new object[] { His.his.Choscode, wd });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.kindcode_selTextInpt.Text.Trim().Length == 0)
            {
                WJs.alert("请输入类别编码！");
                this.kindcode_selTextInpt.Focus();
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.SetEQManagKind";
            ac.Sql = "Save";
            ac.Add("WARECODE", this.warecode_yTextBox.Text);
            ac.Add("CHOSCODE", this.choscode_yTextBox.Text);
            ac.Add("KINDCODE", this.kindcode_selTextInpt.Value);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();

        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            this.dataGView1.reLoad(new object[] { His.his.Choscode, wd });
            InitForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (WJs.confirmFb("你确定要删除选择的设备类别吗?"))
                {
                    decimal isusekindcode = Convert.ToDecimal(LData.Es("IsUseEQKindCode", null, new object[] { dr["类别编码"].ToString(), this.warecode_yTextBox.Text }));
                    if (isusekindcode > 0)
                    {
                        WJs.alert("该类别已被使用，不能删除！");
                        return;
                    }
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.JiChuDictionary.SetEQManagKind";
                    ac.Sql = "Delete";
                    ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                    ac.Add("WARECODE", this.warecode_yTextBox.Text);
                    ac.Add("KINDCODE", dr["类别编码"].ToString());
                    ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    ac.Post();
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备类别信息");
            }
        }
    }
}
