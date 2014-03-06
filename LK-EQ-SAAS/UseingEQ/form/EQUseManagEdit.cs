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

namespace UseingEQ.form
{
    public partial class EQUseManagEdit : Form
    {
        public EQUseManagEdit()
        {
            InitializeComponent();
        }

        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag;//0 为浏览  1 为编辑   2 为新增
        bool isOK;
        public EQUseManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(ytComboBox1);
            deptid_selTextInpt.Sql = "DeptidBindInfo_EQUseManagEdit";
            deptid_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OK_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadInfoEQUse()
        {
            this.useid_textBox.Text = drX["使用ID"].ToString();

            //this.cardId_textBox.Text = drZ["设备名称"].ToString();
            //this.cardId_textBox.Value = drX["卡片ID"].ToString();

            this.CLfare_textBox.Text = drX["材料费"].ToString();
            this.usenum_textBox.Text = drX["使用次数"].ToString();
            this.NHfare_textBox.Text = drX["能耗费"].ToString();
            this.usething_textBox.Text = drX["使用情况"].ToString();
            this.OtherFare_textBox.Text = drX["其他"].ToString();
            this.usedate_dateTimePicker.Value = drX["使用日期"].ToDateTime();
            this.Income_textBox.Text = drX["营业收入"].ToString();
            this.useman_textBox.Text = drX["使用人"].ToString();
            this.memo_textBox.Text = drX["备注"].ToString();


            this.ytComboBox1.Value = drX["状态"].ToString();

            this.Userid_textBox.Text = drX["操作员ID"].ToString();
            this.Username_textBox.Text = drX["操作员姓名"].ToString();
            this.recdate_dateTimePicker.Value = drX["修改时间"].ToDateTime();
            this.choscode_textBox.Text = drX["医疗机构编码"].ToString();
        }

        private void EQUseManagEdit_Load(object sender, EventArgs e)
        {
            if (isFlag == 0 || isFlag == 1)
            {
                LoadInfoEQUse();
                if (isFlag == 0)
                {
                    this.Save_button.Visible = false;
                    this.Save_button.Enabled = false;
                    this.cancel_button.Visible = false;
                    this.cancel_button.Enabled = false;
                    foreach (Control item in groupBox1.Controls)
                    {
                        if (item is Button || item is Label)
                        {
                            continue;
                        }
                        item.Enabled = false;
                    }
                }
                if (isFlag == 1)
                {
                    this.OK_button.Visible = false;
                    this.OK_button.Enabled = false;
                }
            }
            if (isFlag == 2)
            {
                this.OK_button.Visible = false;
                this.OK_button.Enabled = false;
                this.ytComboBox1.SelectedIndex = 1;

                this.Userid_textBox.Text = His.his.UserId.ToString();
                this.Username_textBox.Text = His.his.UserName;
                this.recdate_dateTimePicker.Value = DateTime.Now;
                this.choscode_textBox.Text = His.his.Choscode;
            }

            //添加过来默认为有效[添加无效的使用状态是不可取的]  编辑过来的状态也不可改变
            ytComboBox1.Enabled = false;

            this.deptid_selTextInpt.Text = drZ["使用科室ID_Text"].ToString();
            this.deptid_selTextInpt.Value = drZ["使用科室ID"].ToString();

            //this.cardId_textBox.Text = drZ["设备名称"].ToString();
            this.cardId_textBox.Text = drZ["设备名称"].ToString();
            this.cardId_textBox.Value = drZ["卡片ID"].ToString();

        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (deptid_selTextInpt.Text == "" || deptid_selTextInpt.Value == "")
            {
                WJs.alert("请选择一个科室");
                deptid_selTextInpt.Focus();
                return;
            }
            if (ytComboBox1.SelectedIndex < 0)
            {
                WJs.alert("请选择一个状态！");
                ytComboBox1.Focus();
                return;
            }
            if (useman_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写使用人！");
                useman_textBox.Focus();
                return;
            }
            if (usenum_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写使用次数！");
                usenum_textBox.Focus();
                return;
            }

            if (!WJs.IsD0Zs(usenum_textBox.Text) || Convert.ToInt32(usenum_textBox.Text) < 0)
            {
                WJs.alert("使用次数：请输入一个正整数！");
                usenum_textBox.Focus();
                return;
            }
            if (CLfare_textBox.Text.Trim() != "" && CLfare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(CLfare_textBox.Text.Trim()) || Convert.ToDouble(CLfare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("材料费必须为正数！");
                    CLfare_textBox.Focus();
                    return;
                }
            }
            if (NHfare_textBox.Text.Trim() != "" && NHfare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(NHfare_textBox.Text.Trim()) || Convert.ToDouble(NHfare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("能耗费必须为正数！");
                    NHfare_textBox.Focus();
                    return;
                }
            }
            if (OtherFare_textBox.Text.Trim() != "" && OtherFare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(OtherFare_textBox.Text.Trim()) || Convert.ToDouble(OtherFare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("其他费用必须为正数！");
                    OtherFare_textBox.Focus();
                    return;
                }
            }
            if (Income_textBox.Text.Trim() != "" && Income_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(Income_textBox.Text.Trim()) || Convert.ToDouble(Income_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("营业收入必须为正数！");
                    Income_textBox.Focus();
                    return;
                }
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQUseManagSvr";
            ac.Sql = "AddOrSaveEQUseManage";
            AddInfoForSave(ac);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOK)
            {
                WJs.alert("保存使用信息成功，即将关闭本窗口……");
                this.Close();
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK = true;
            }
            else
            {
                isOK = false;
                WJs.alert(e.Msg.Msg);
            }
        }


        private void AddInfoForSave(ActionLoad ac)
        {
            if (isFlag == 1)
            {
                ac.Add("USEID", useid_textBox.Text.Trim());
            }
            else
            {
                ac.Add("USEID", "");
            }
            ac.Add("CARDID", cardId_textBox.Value.Trim());
            ac.Add("DEPTID", deptid_selTextInpt.Value);
            ac.Add("USENUM", usenum_textBox.Text.Trim());
            ac.Add("INCOME", Income_textBox.Text.Trim());
            ac.Add("USETHING", usething_textBox.Text);
            ac.Add("CLFARE", CLfare_textBox.Text.Trim());
            ac.Add("NHFARE", NHfare_textBox.Text.Trim());
            ac.Add("OTHERFARE", OtherFare_textBox.Text.Trim());
            ac.Add("USEMAN", useman_textBox.Text.Trim());
            ac.Add("USEDATE", usedate_dateTimePicker.Value);
            ac.Add("MEMO", memo_textBox.Text.Trim());
            ac.Add("STATUS", ytComboBox1.Value);
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("RECDATE", DateTime.Now);
        }

        private void usenum_textBox_Leave(object sender, EventArgs e)
        {
            double result;
            if (drZ["收费标准"].IsNull == true || drZ["收费标准"].ToString() == "" || double.TryParse(drZ["收费标准"].ToString(), out result) == false)
            {
                return;
            }
            if (usenum_textBox.Text.Trim() != "" && WJs.IsD0Zs(Username_textBox.Text.Trim()))
            {
                this.Income_textBox.Text = (drZ["收费标准"].ToDouble() * Convert.ToInt32(usenum_textBox.Text)).ToString();
            }
        }

    }
}
