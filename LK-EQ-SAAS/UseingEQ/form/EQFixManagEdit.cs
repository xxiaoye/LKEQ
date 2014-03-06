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

    public partial class EQFixManagEdit : Form
    {
        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag; //0 浏览  1 编辑   2 新增
        int Status;//1 报修状态  2  维修状态  6 交付状态   10 编辑全能【其余的浏览和新增也是如此】

        bool isOk;

        public EQFixManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag, int Status)
        {
            InitializeComponent();
            TvList.newBind().add("作废", "0").add("报修状态", "1").add("维修状态", "2").add("交付状态", "6").Bind(Status_ytComboBox1);
            //deptid_selTextInpt.Sql = "DeptidBindInfo_EQFixManagEdit";
            //deptid_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}";


            repaircode_selText.Sql = "GetRePairCode_EQFixManagEdit";
            repaircode_selText.SelParam = His.his.Choscode + "|{key}|{key}";


            RepairDeptId_selText.Sql = "DeptidBindInfo_EQFixManagEdit";
            RepairDeptId_selText.SelParam = His.his.Choscode + "|{key}|{key}|{key}";


            reportmanId_selText.Sql = "YongHuMingInfo_EQFixManagEdit";
            reportmanId_selText.SelParam = His.his.Choscode + "|{key}|{key}";

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
            this.Status = Status;
            this.Status_ytComboBox1.SelectedIndex = 3;
        }

        private void DisableControl()
        {
            foreach (Control item in groupBox3.Controls)
            {
                if (item is Label)
                {
                    continue;
                }
                item.Enabled = false;
            }

            foreach (Control item in groupBox4.Controls)
            {
                if (item is Label)
                {
                    continue;
                }
                item.Enabled = false;
            }
            foreach (Control item in groupBox5.Controls)
            {
                if (item is Label)
                {
                    continue;
                }
                item.Enabled = false;
            }
            PayDate_dateTime.Enabled = false;
        }

        private void LoadInfo_EQFixManagEdit()
        {
            repairid_textBox.Text = drX["维修ID"].ToString();
            memo_textBox.Text = drX["备注"].ToString();
            Status_ytComboBox1.Value = drX["状态"].ToString();

            repaircode_selText.Text = drX["维修类别名称"].ToString();
            repaircode_selText.Value = drX["维修类别编码"].ToString();
            Fault_textBox.Text = drX["故障现象"].ToString();

            Reportdate_dateTime.Value = drX["报修日期"].ToDateTime();
            FaultThing_textBox.Text = drX["故障情况"].ToString();
            //reportman_textBox.Text = drX["报修人"].ToString();
            reportmanId_selText.Text = drX["报修人ID"].ToString();
            reportmanId_selText.Value = drX["报修人"].ToString();
            repotruser_textBox.Text = drX["报修人"].ToString();

            RepairDeptId_selText.Value = drX["维修科室ID"].ToString();
            RepairDeptId_selText.Text = drX["维修科室名称"].ToString();
            repairMan_textBox.Text = drX["维修人"].ToString();
            RepairTime_textBox.Text = drX["工时"].ToString();
            CLfare_textBox.Text = drX["材料费"].ToString();
            Repairfare_textBox.Text = drX["维修费"].ToString();
            OtherFare_textBox.Text = drX["其他"].ToString();
            RepairPass_textBox.Text = drX["维修经过"].ToString();
            RepairResult_textBox.Text = drX["维修结果"].ToString();
            ChangeParts_textBox.Text = drX["更换部件"].ToString();
            Repairdate_dateTimePicker.Value = drX["维修日期"].ToDateTime();

            PayDate_dateTime.Value = drX["交付日期"].ToDateTime();

            Userid_textBox.Text = drZ["操作员ID"].ToString();
            Username_textBox.Text = drZ["操作员姓名"].ToString();
            choscode_textBox.Text = drZ["医疗机构编码"].ToString();
            recdate_dateTimePicker.Value = DateTime.Now;
        }
        private void EQFixManagEdit_Load(object sender, EventArgs e)
        {
            if (isFlag == 0 || isFlag == 1)
            {
                LoadInfo_EQFixManagEdit();
                if (isFlag == 0)
                {
                    this.Save_button.Visible = false;
                    this.Save_button.Enabled = false;
                    this.cancel_button.Visible = false;
                    this.cancel_button.Enabled = false;
                    DisableControl();
                }

                if (isFlag == 1)
                {
                    //编辑 维修 状态
                    if (Status == 2)
                    {
                        Status_ytComboBox1.SelectedIndex = 2;//维修状态
                        groupBox4.Enabled = false;
                        groupBox6.Enabled = false;

                    }
                    //编辑  交付 状态
                    if (Status == 6)
                    {
                        Status_ytComboBox1.SelectedIndex = 3;//交付状态
                        groupBox4.Enabled = false;
                        groupBox5.Enabled = false;
                    }
                    Status_ytComboBox1.Enabled = false;
                    this.OK_button.Visible = false;
                    this.OK_button.Enabled = false;
                }
            }

            if (isFlag == 2)
            {
                //新增报修信息
                if (Status == 1)
                {
                    Status_ytComboBox1.SelectedIndex = 1;//报修状态
                    groupBox5.Enabled = false;
                    groupBox6.Enabled = false;
                }

                Userid_textBox.Text = His.his.UserId.ToString();
                Username_textBox.Text = His.his.UserName;
                choscode_textBox.Text = His.his.Choscode;
                recdate_dateTimePicker.Value = DateTime.Now;
                Status_ytComboBox1.Enabled = false;
                this.OK_button.Visible = false;
                this.OK_button.Enabled = false;
            }

            cardId_textBox.Value = drZ["卡片ID"].ToString();
            cardId_textBox.Text = drZ["设备名称"].ToString();
            //cardId_textBox.Text = drZ["卡片ID"].ToString();

            this.deptid_selTextInpt.Text = drZ["使用科室ID_Text"].ToString();
            this.deptid_selTextInpt.Value = drZ["使用科室ID"].ToString();

            reportmanId_selText.TextChanged += new EventHandler(reportmanId_selText_TextChanged);

        }

        void reportmanId_selText_TextChanged(object sender, EventArgs e)
        {
            if (reportmanId_selText.Value == "" || reportmanId_selText.Text == null)
            {
                return;
            }
            repotruser_textBox.Text = reportmanId_selText.Value;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int AddBXYanZheng()
        {
            if (repaircode_selText.Text.Trim() == "" || repaircode_selText.Value.Trim() == "")
            {
                WJs.alert("报修必填项：维修类别编码！");
                repaircode_selText.Focus();
                return 1;
            }
            if (Fault_textBox.Text.Trim() == "")
            {
                WJs.alert("报修必填项：故障现象！");
                Fault_textBox.Focus();
                return 1;
            }
            if (FaultThing_textBox.Text.Trim() == "")
            {
                WJs.alert("报修必填项：故障情况！");
                FaultThing_textBox.Focus();
                return 1;
            }
            if (repotruser_textBox.Text.Trim() == "" || repotruser_textBox.Text.Trim() == null)
            {
                WJs.alert("报修必填项：报修人！");
                repotruser_textBox.Focus();
                return 1;
            }
            if (Reportdate_dateTime.Value == null)
            {
                WJs.alert("报修必填项：保修时间！");
                Reportdate_dateTime.Focus();
                return 1;
            }
            return 0;
        }

        private int ModifyWXYanZheng()
        {

            if (RepairDeptId_selText.Text.Trim() == "" || RepairDeptId_selText.Value.Trim() == "")
            {
                WJs.alert("维修必填项：维修科室ID！");
                RepairDeptId_selText.Focus();
                return 1;
            }

            if (repairMan_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：维修人");
                repairMan_textBox.Focus();
                return 1;
            }
            if (RepairTime_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：维修工时");
                RepairTime_textBox.Focus();
                return 1;
            }
            if (!WJs.IsNum(RepairTime_textBox.Text.Trim()) || Convert.ToDouble(RepairTime_textBox.Text.Trim()) < 0)
            {
                WJs.alert("维修工时必须为正数！");
                RepairTime_textBox.Focus();
                return 1;

            }
            if (Repairfare_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：维修费");
                Repairfare_textBox.Focus();
                return 1;
            }

            if (Repairfare_textBox.Text.Trim() != "" && Repairfare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(Repairfare_textBox.Text.Trim()) || Convert.ToDouble(Repairfare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("维修费必须为正数！");
                    Repairfare_textBox.Focus();
                    return 1;
                }
            }
            if (CLfare_textBox.Text.Trim() != "" && CLfare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(CLfare_textBox.Text.Trim()) || Convert.ToDouble(CLfare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("材料费必须为正数！");
                    CLfare_textBox.Focus();
                    return 1;
                }
            }
            if (OtherFare_textBox.Text.Trim() != "" && OtherFare_textBox.Text.Trim() != null)
            {
                if (!WJs.IsNum(OtherFare_textBox.Text.Trim()) || Convert.ToDouble(OtherFare_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("其他费用必须为正数！");
                    OtherFare_textBox.Focus();
                    return 1;
                }
            }

            if (CLfare_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：材料费");
                CLfare_textBox.Focus();
                return 1;
            }

            if (OtherFare_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：其他");
                OtherFare_textBox.Focus();
                return 1;
            }
            if (RepairPass_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：维修经过");
                RepairPass_textBox.Focus();
                return 1;
            }
            if (RepairResult_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：维修结果");
                RepairResult_textBox.Focus();
                return 1;
            }
            if (ChangeParts_textBox.Text.Trim() == "")
            {
                WJs.alert("维修必填项：更换部件");
                ChangeParts_textBox.Focus();
                return 1;
            }
            if (Repairdate_dateTimePicker.Value == null)
            {
                WJs.alert("维修必填项：维修日期");
                Repairdate_dateTimePicker.Focus();
                return 1;
            }
            return 0;
        }

        private int ModifyJFYanZheng()
        {
            if (PayDate_dateTime.Value == null)
            {
                WJs.alert("交付必填项：交付时间");
                return 1;
            }
            return 0;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Status_ytComboBox1.SelectedIndex < 0)
            {
                WJs.alert("请选择一个状态信息！");
            }

            if (isFlag == 2 && Status == 1)
            {
                if (AddBXYanZheng() == 1)
                {
                    return;
                }
            }

            if (isFlag == 1 && Status == 2)
            {
                if (ModifyWXYanZheng() == 1)
                {
                    return;
                }
            }
            if (isFlag == 1 && Status == 6)
            {
                if (ModifyJFYanZheng() == 1)
                {
                    return;
                }
            }

            if (Status == 10)
            {
                if (AddBXYanZheng() == 1)
                {
                    return;
                }
                if (ModifyWXYanZheng() == 1)
                {
                    return;
                }
                if (ModifyJFYanZheng() == 1)
                {
                    return;
                }
            }
            //下面开始保存了
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQFixManagSvr";
            ac.Sql = "ModifyOrAddInfo";

            AddRepairCardInfo(ac);
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

        private void AddRepairCardInfo(ActionLoad ac)
        {
            if (isFlag == 1)
            {
                ac.Add("REPAIRID", repairid_textBox.Text.Trim().ToString());
            }
            else
            {
                ac.Add("REPAIRID", "");
            }

            ac.Add("CARDID", cardId_textBox.Value.Trim());
            ac.Add("DEPTID", deptid_selTextInpt.Value);
            ac.Add("REPAIRCODE", repaircode_selText.Value.ToString());
            ac.Add("FAULT", Fault_textBox.Text.Trim());
            ac.Add("FAULTTHING", FaultThing_textBox.Text.Trim());
            ac.Add("REPORTDATE", Reportdate_dateTime.Value);
            ac.Add("REPORTUSERID2", reportmanId_selText.Text);
            ac.Add("REPORTMAN", repotruser_textBox.Text.Trim());
            ac.Add("REPAIRPASS", RepairPass_textBox.Text);
            ac.Add("REPAIRRESULT", RepairResult_textBox.Text);
            ac.Add("CHANGEPARTS", ChangeParts_textBox.Text);
            ac.Add("REPAIRDEPTID", RepairDeptId_selText.Value);
            ac.Add("REPAIRMAN", repairMan_textBox.Text);
            ac.Add("REPAIRTIME", RepairTime_textBox.Text);
            ac.Add("CLFARE", CLfare_textBox.Text);
            ac.Add("REPAIRFARE", Repairfare_textBox.Text);
            ac.Add("OTHERFARE", OtherFare_textBox.Text);
            ac.Add("REPAIRDATE", Repairdate_dateTimePicker.Value);
            ac.Add("MEMO", memo_textBox.Text);
            ac.Add("PAYDATE", PayDate_dateTime.Value);
            ac.Add("STATUS", Status_ytComboBox1.Value);
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("RECDATE", DateTime.Now);
        }
    }
}
