using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtWinContrl.com.datagrid;
using ChSys;
using YtUtil.tool;
using YtClient;

namespace JiChuDictionary.form
{
    public partial class AddEQStatus : Form
    {
        bool isOk;
        bool isAdd;
        Dictionary<string, ObjItem> dr;

        public AddEQStatus(Dictionary<string, ObjItem> dr, bool isAdd)
        {
            InitializeComponent();
            this.dr = dr;
            this.isAdd = isAdd;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddEQStatus_Load(object sender, EventArgs e)
        {
            TvList.newBind().add("否", "0").add("是", "1").Bind(this.ifdefault_ytComboBox);
            TvList.newBind().add("否", "0").add("是", "1").Bind(this.ifdepreciation_ytComboBox);
            TvList.newBind().add("停用", "0").add("启用", "1").Bind(this.ifuse_ytComboBox);
            InitForm();
            this.statusname_yTextBox.TextChanged += new EventHandler(statusname_yTextBox_TextChanged);
        }


        void statusname_yTextBox_TextChanged(object sender, EventArgs e)
        {
            this.wbcode_yTextBox.Text = PyWbCode.getWbCode(this.statusname_yTextBox.Text).ToLower();
            this.pycode_yTextBox.Text = PyWbCode.getPyCode(this.statusname_yTextBox.Text).ToLower();
        }
        private void InitForm()
        {
            this.choscode_yTextBox.Text = His.his.Choscode;
            this.userid_yTextBox.Text = His.his.UserId.ToString();
            this.username_yTextBox.Text = His.his.UserName;
            if (!isAdd)
            {
                //当为修改时
                this.statuscode_yTextBox.Text = dr["状态编码"].ToString();
                this.statusname_yTextBox.Text = dr["状态名称"].ToString();
                this.pycode_yTextBox.Text = dr["拼音码"].ToString();
                this.wbcode_yTextBox.Text = dr["五笔码"].ToString();
                this.memo_yTextBox.Text = dr["备注"].ToString();
                if (dr["是否默认值"].ToString() == "1")
                {
                    this.ifdefault_ytComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ifdefault_ytComboBox.SelectedIndex = 0;
                }

                if (dr["是否使用"].ToString() == "1")
                {
                    this.ifuse_ytComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ifuse_ytComboBox.SelectedIndex = 0;
                }
                if (dr["是否计提折旧"].ToString() == "1")
                {
                    this.ifdepreciation_ytComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ifdepreciation_ytComboBox.SelectedIndex = 0;
                }
                ifuse_ytComboBox.Enabled = false;
            }
            else
            {
                //当为增加时 初始化[只是为了符合数据库的默认值]
                this.statusname_yTextBox.Text = "";
                this.pycode_yTextBox.Text = "";
                this.wbcode_yTextBox.Text = "";
                this.memo_yTextBox.Text = "";
                this.ifdefault_ytComboBox.SelectedIndex = 0;
                this.ifuse_ytComboBox.SelectedIndex = 1;
                this.ifdepreciation_ytComboBox.SelectedIndex = 0;
                this.dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.statusname_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请填写设备状态名称！");
                return;
            }
            if (this.ifuse_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否使用！");
                return;
            }
            if (this.ifdefault_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否默认值！");
                return;
            }
            if (this.ifdepreciation_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否计提折旧！");
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQStatusSvr";
            //STATUSCODE,STATUSNAME,PYCODE,WBCODE,IFUSE,IFDEPRECIATION,IFDEFAULT,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
            ac.Add("STATUSNAME", this.statusname_yTextBox.Text.Trim());
            ac.Add("PYCODE", this.pycode_yTextBox.Text.Trim());
            ac.Add("WBCODE", this.wbcode_yTextBox.Text.Trim());
            ac.Add("IFUSE", this.ifuse_ytComboBox.SelectedIndex.ToString());
            ac.Add("IFDEPRECIATION", this.ifdepreciation_ytComboBox.SelectedIndex.ToString());
            ac.Add("IFDEFAULT", this.ifdefault_ytComboBox.SelectedIndex.ToString());
            ac.Add("MEMO", this.memo_yTextBox.Text.Trim());
            ac.Add("USERID", this.userid_yTextBox.Text.Trim());
            ac.Add("USERNAME", this.username_yTextBox.Text.Trim());
            ac.Add("RECDATE", this.dateTimePicker1.Value);
            ac.Add("CHOSCODE", His.his.Choscode);
            if (isAdd)
            {
                ac.Sql = "Add";
            }
            else
            {
                ac.Sql = "Modify";
                //修改则需传递状态编码
                ac.Add("STATUSCODE", this.statuscode_yTextBox.Text.ToString());
            }
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOk)
            {
                if (isAdd)
                {
                    if (WJs.confirmFb("添加设备状态信息成功，是否继续添加？"))
                    {
                        InitForm();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    WJs.alert("保存设备状态信息成功！");
                    this.Close();
                }
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("增加设备状态信息成功！") || (e.Msg.Msg.Equals("修改设备状态信息成功！")))
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
        }
    }
}
