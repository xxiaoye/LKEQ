using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using ChSys;
using YiTian.db;
using YtUtil.tool;
using YtClient;

namespace UseingEQ.form
{
    public partial class EQChangeManagEdit : Form
    {
        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag; //0 为浏览  1 为编辑  2 为新增
        bool isOk;

        public EQChangeManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;

            this.ChangeType_ytComboBox.SelectedIndex = -1;

            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").Bind(Status_ytComboBox);
            TvList.newBind().add("所属权变更", "1").add("原值变更", "2").add("使用状态变更", "3").Bind(ChangeType_ytComboBox);

            NewDeptid_selText.Sql = "DeptidBindInfo_EQChangeManagEdit";
            NewDeptid_selText.SelParam = His.his.Choscode + "|{key}|{key}|{key}";


            NewUseStatus_selText.Sql = "UseStatusBindInfo_EQChangeManagEdit";
            NewUseStatus_selText.SelParam = His.his.Choscode + "|{key}|{key}";
        }

        private void AddEQChangeManagInfo()
        {
            ChangeId_textBox.Text = drX["变动ID"].ToString();
            ChangeType_ytComboBox.Value = drX["变动类型"].ToString();
            Status_ytComboBox.Value = drX["状态"].ToString();

            CardId_selText.Value = drX["卡片ID"].ToString();
            CardId_selText.Text = drZ["设备名称"].ToString();

            OldYZ_textBox.Text = drX["调整前原值"].ToString();
            NewYZ_textBox.Text = drX["调整后原值"].ToString();

            OldUseStatus_selText.Text = drX["原使用状态"].ToString();
            OldUseStatus_selText.Value = drX["原使用状态编码"].ToString();

            OldDeptid_selText.Value = drX["原使用科室ID"].ToString();
            OldDeptid_selText.Text = drX["原使用科室"].ToString();

            OldMan_textBox.Text = drX["原保管员"].ToString();

            NewUseStatus_selText.Text = drX["新使用状态"].ToString();
            NewUseStatus_selText.Value = drX["新使用状态编码"].ToString();

            NewDeptid_selText.Text = drX["现使用科室"].ToString();
            NewDeptid_selText.Value = drX["现使用科室ID"].ToString();

            NewMan_textBox.Text = drX["现保管员"].ToString();

            UserId_textBox.Text = drX["操作员ID"].ToString();
            UserName_textBox.Text = drX["操作员姓名"].ToString();
            Recdate_dateTime.Text = drX["修改时间"].ToString();
            SHUserId_textBox.Text = drX["审核操作员ID"].ToString();
            SHUserName_textBox.Text = drX["审核操作员名称"].ToString();
            SHdateTime.Text = drX["审核时间"].ToString();
            Choscode_textBox.Text = drX["医疗机构编码"].ToString();
            Memo_textBox.Text = drX["备注"].ToString();

            //old

        }

        private void EQChangeManagEdit_Load(object sender, EventArgs e)
        {

            if (isFlag == 0 || isFlag == 1)
            {
                AddEQChangeManagInfo();
                if (isFlag == 0)
                {
                    foreach (Control item in groupBox1.Controls)
                    {
                        if (item is Label || item is Button)
                        {
                            continue;
                        }
                        item.Enabled = false;
                    }

                    SaveBtn.Visible = false;
                    CancelBtn.Visible = false;
                }
                if (isFlag == 1)
                {
                    OkBtn.Visible = false;
                    Status_ytComboBox.Enabled = false;
                    //如果编辑进来，就判断当前的改变类型为什么，因此对应改变可读性
                    ChangeType_ytComboBox_SelectedIndexChanged(null, null);
                }
            }
            if (isFlag == 2)
            {
                this.Status_ytComboBox.SelectedIndex = 1;
                this.Status_ytComboBox.Enabled = false;
                OkBtn.Visible = false;
                CardId_selText.Value = drZ["卡片ID"].ToString();
                CardId_selText.Text = drZ["设备名称"].ToString();

                OldDeptid_selText.Text = drZ["使用科室ID_Text"].ToString();
                OldDeptid_selText.Value = drZ["使用科室ID"].ToString();

                OldMan_textBox.Text = drZ["保管人"].ToString();

                OldYZ_textBox.Text = drZ["原值"].ToString();

                OldUseStatus_selText.Text = drZ["使用状态_Text"].ToString();
                OldUseStatus_selText.Value = drZ["使用状态"].ToString();


                UserId_textBox.Text = His.his.UserId.ToString();
                UserName_textBox.Text = His.his.UserName;
                Choscode_textBox.Text = His.his.Choscode;
                Recdate_dateTime.Text = DateTime.Now.ToString();
            }
            this.ChangeType_ytComboBox.SelectedIndexChanged += new EventHandler(ChangeType_ytComboBox_SelectedIndexChanged);

        }

        void ChangeType_ytComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ChangeType_ytComboBox.SelectedIndex == 0)
            {
                NewDeptid_selText.Enabled = true;
                NewMan_textBox.ReadOnly = false;

                NewYZ_textBox.Text = OldYZ_textBox.Text;
                NewYZ_textBox.ReadOnly = true;

                NewUseStatus_selText.Text = OldUseStatus_selText.Text;
                NewUseStatus_selText.Value = OldUseStatus_selText.Value;
                NewUseStatus_selText.Enabled = false;
            }
            if (this.ChangeType_ytComboBox.SelectedIndex == 1)
            {
                NewYZ_textBox.ReadOnly = false;

                NewDeptid_selText.Value = OldDeptid_selText.Value;
                NewDeptid_selText.Text = OldDeptid_selText.Text;
                NewDeptid_selText.Enabled = false;

                NewMan_textBox.Text = OldMan_textBox.Text;
                NewMan_textBox.ReadOnly = true;

                NewUseStatus_selText.Text = OldUseStatus_selText.Text;
                NewUseStatus_selText.Value = OldUseStatus_selText.Value;
                NewUseStatus_selText.Enabled = false;
            }
            if (this.ChangeType_ytComboBox.SelectedIndex == 2)
            {
                NewUseStatus_selText.Enabled = true;

                NewDeptid_selText.Value = OldDeptid_selText.Value;
                NewDeptid_selText.Text = OldDeptid_selText.Text;
                NewDeptid_selText.Enabled = false;

                NewMan_textBox.Text = OldMan_textBox.Text;
                NewMan_textBox.ReadOnly = true;

                NewYZ_textBox.Text = OldYZ_textBox.Text;
                NewYZ_textBox.ReadOnly = true;
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (YanZhengInfo() == 1)
            {
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQChangeManagSvr";
            ac.Sql = "ModifyOrAddInfo";
            AddEQChangeInfo(ac);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOk)
            {
                WJs.alert("保存信息成功，即将关闭本窗口……");
                this.Close();
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
        }

        private int YanZhengInfo()
        {
            if (ChangeType_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择变动类型！");
                ChangeType_ytComboBox.Focus();
                return 1;
            }
            if (Status_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择状态！");
                Status_ytComboBox.Focus();
                return 1;
            }
            if (OldYZ_textBox.Text.Trim() != "")
            {
                if (NewYZ_textBox.Text.Trim() == "")
                {
                    WJs.alert("请填入原值");
                    NewYZ_textBox.Focus();
                    return 1;
                }
                if (!WJs.IsNum(NewYZ_textBox.Text.Trim()) || Convert.ToDouble(NewYZ_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("原值必须为正数！");
                    NewYZ_textBox.Focus();
                    return 1;
                }
            }
            if (OldUseStatus_selText.Value != "" || OldUseStatus_selText.Text != "")
            {
                if (NewUseStatus_selText.Value == "" || NewUseStatus_selText.Text == "")
                {
                    WJs.alert("请选择现使用状态！");
                    NewUseStatus_selText.Focus();
                    return 1;
                }
            }

            if (OldDeptid_selText.Value != "" || OldDeptid_selText.Text != "")
            {
                if (NewDeptid_selText.Value == "" || NewDeptid_selText.Text == "")
                {
                    WJs.alert("请选择现使用科室！");
                    NewDeptid_selText.Focus();
                    return 1;
                }
            }


            if (OldMan_textBox.Text.Trim() != "")
            {
                if (NewMan_textBox.Text.Trim() == "")
                {
                    WJs.alert("请填写现保管人！");
                    NewMan_textBox.Focus();
                    return 1;
                }
            }
            return 0;
        }

        private void AddEQChangeInfo(ActionLoad ac)
        {
            if (isFlag == 1)
            {
                ac.Add("CHANGEID", ChangeId_textBox.Text);
            }
            else
            {
                ac.Add("CHANGEID", "");
            }
            ac.Add("CARDID", CardId_selText.Value);
            ac.Add("CHANGETYPE", ChangeType_ytComboBox.Value);
            ac.Add("OLDDEPTID", OldDeptid_selText.Value);
            ac.Add("OLDMAN", OldMan_textBox.Text);
            ac.Add("USEMAN", NewMan_textBox.Text);
            ac.Add("USEDEPTID", NewDeptid_selText.Value);
            ac.Add("OLDVALUE", OldYZ_textBox.Text);
            ac.Add("NOWVALUE", NewYZ_textBox.Text);
            ac.Add("OLDSTATUSCODE", OldUseStatus_selText.Value);
            ac.Add("NEWSTATUSCODE", NewUseStatus_selText.Value);
            ac.Add("MEMO", Memo_textBox.Text);
            ac.Add("STATUS", Status_ytComboBox.Value);
            ac.Add("USERID", UserId_textBox.Text);
            ac.Add("USERNAME", UserName_textBox.Text);
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("SHUSERID", SHUserId_textBox.Text);
            ac.Add("SHUSERNAME", SHUserName_textBox.Text);
            ac.Add("SHDATE", "");
            ac.Add("CHOSCODE", His.his.Choscode);
        }


    }
}
