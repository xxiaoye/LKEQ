using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using ChSys;
using YtClient;
using YtWinContrl.com.datagrid;

namespace JiChuDictionary.form
{
    public partial class AddEQCountKind : Form
    {
        bool isAdd;
        object[] r;
        bool isOk = false;
        bool isOk1 = false;//针对子节点判断的值

        public AddEQCountKind()
        {
            InitializeComponent();
        }

        public AddEQCountKind(object[] r, bool isAdd)
        {
            InitializeComponent();
            this.r = r;
            this.isAdd = isAdd;
        }

        private void AddEQCountKind_Load(object sender, EventArgs e)
        {
            TvList.newBind().add("否", "0").add("是", "1").Bind(ifend_ytComboBox);
            TvList.newBind().add("否", "0").add("是", "1").Bind(ifuse_ytComboBox);
            InitAddEQCountKind();
            if (isAdd)
            {
                //新增过来的不允许改变是否末节点，一律为末节点
                ifend_ytComboBox.Enabled = false;
            }
            if (!isAdd)
            {
                //编辑过来的是否使用一律不允许更改
                ifuse_ytComboBox.Enabled = false;
            }
            this.countname_yTextBox.TextChanged += new EventHandler(countname_yTextBox_TextChanged);
        }
        void countname_yTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.countname_yTextBox.Text.Trim().Length > 0)
            {
                this.wbcode_yTextBox.Text = PyWbCode.getWbCode(this.countname_yTextBox.Text).ToLower();
                this.pycode_yTextBox.Text = PyWbCode.getPyCode(this.countname_yTextBox.Text).ToLower();
            }
        }

        private void InitAddEQCountKind()
        {
            //COUNTCODE,SUPERCODE,COUNTNAME,PYCODE,WBCODE,IFEND,IFUSE,MEMO,USERID,USERNAME,RECDATE,CHOSCODE
            this.countcode_yTextBox.Text = r[0].ToString();
            this.supercode_yTextBox.Text = r[1].ToString();
            this.countname_yTextBox.Text = r[2].ToString();
            this.pycode_yTextBox.Text = r[3].ToString();
            this.wbcode_yTextBox.Text = r[4].ToString();
            this.memo_yTextBox.Text = r[7].ToString();
            this.userid_yTextBox.Text = r[8].ToString();
            this.username_yTextBox.Text = r[9].ToString();
            this.reedittime_yTextBox.Text = r[10].ToString();
            this.choscode_yTextBox.Text = r[11].ToString();

            this.ifend_ytComboBox.SelectedIndex = Convert.ToInt32(r[5]);
            this.ifuse_ytComboBox.SelectedIndex = Convert.ToInt32(r[6]);
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (this.countname_yTextBox.Text.Trim().Length == 0)
            {
                WJs.alert("请输入统计类别名称！");
                return;
            }
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQCountKindSvr";
            if (isAdd)
            {
                ac.Sql = "Add";
            }
            else
            {
                ac.Sql = "Modify";
            }
            ac.Add("COUNTCODE", this.countcode_yTextBox.Text.Trim());
            ac.Add("SUPERCODE", this.supercode_yTextBox.Text.Trim());
            ac.Add("COUNTNAME", this.countname_yTextBox.Text.Trim());
            ac.Add("PYCODE", this.pycode_yTextBox.Text.Trim());
            ac.Add("WBCODE", this.wbcode_yTextBox.Text.Trim());
            ac.Add("IFEND", this.ifend_ytComboBox.SelectedIndex);
            ac.Add("IFUSE", this.ifuse_ytComboBox.SelectedIndex);
            ac.Add("MEMO", this.memo_yTextBox.Text);
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", this.reedittime_yTextBox.Text);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOk)
            {
                if (isAdd)
                {
                    if (!WJs.confirm("添加统计类别信息成功，是否继续添加？"))
                    {
                        this.Close();
                    }
                    else
                    {
                        InitAddEQCountKind();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                WJs.alert("保存设备统计类别信息失败！");
            }
        }
        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("已经存在该统计类别名称！") || (e.Msg.Msg.Equals("更新设备统计类别失败！")) || (e.Msg.Msg.Equals("该节点下包含子节点，不能设置其为末节点！")))
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
            else
            {
                isOk = true;
                WJs.alert(e.Msg.Msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
