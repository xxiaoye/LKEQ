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

    public partial class EQKeepAccountsManagEdit : Form
    {
        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag; //0 浏览  1 编辑   2 新增 3 鉴定  4 审核 
        int Status;//1 报修状态  2  维修状态  6 交付状态   10 编辑全能【其余的浏览和新增也是如此】
        string str;
        bool isOk;
        DateTime dt;
        public EQKeepAccountsManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
           

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
           
        }

        private void LoadInfo_EQFixManagEdit()  //如果为修改或者浏览时执行
        {

            NOID_textBox.Text = drX["下账ID"].ToString();
            memo_textBox.Text = drX["备注"].ToString();
            Status_ytComboBox1.Value = drX["状态"].ToString();
            this.IOID_selText.Value = drX["下账类别"].ToString();
            this.IOID_selText.Text = LData.Es("GetEQMaintainName_EQKeepAccount", null, new object[] {His.his.Choscode, drX["下账类别"].ToString() });
            this.QLFARE_textBox.Text = drX["清理费"].ToString();
            this.CVALUE_textBox.Text = drX["残值"].ToString();
            this.NOREASON_textBox.Text = drX["下账原因"].ToString();
            this.reportman_selText.Text = drX["申请人"].ToString();
            this.repotruserId_textBox.Text = drX["申请人ID"].ToString();
            this.reportman_selText.Value = drX["申请人ID"].ToString();
            if (drX["鉴定日期"].ToString().Trim() != "")
            {
                this.dateTimePicker_REPORTDATE.Value = drX["鉴定日期"].ToDateTime();
            }
            this.textBox_JDADVICE.Text = drX["鉴定意见"].ToString();
            this.textBox_JDMAN.Text = drX["鉴定人"].ToString();
            this.textBox_JDUSERID.Text = drX["鉴定人ID"].ToString();
            if (drX["鉴定日期"].ToString().Trim() != "")
            {
                this.dateTimePicker_JDDATE.Value = drX["鉴定日期"].ToDateTime();
            }
            this.textBox_SHADVICE.Text = drX["审批意见"].ToString();
            this.textBox_SHMAN.Text = drX["审批人"].ToString();
            this.textBox_SHUSERID.Text = drX["审批人ID"].ToString();
            if (drX["审批日期"].ToString().Trim() != "")
            {
                this.dateTimePicker_SHDATE.Value = drX["审批日期"].ToDateTime();
            }
            recdate_dateTimePicker.Value = drX["修改时间"].ToDateTime();
            this.Username_textBox.Text = drX["操作员姓名"].ToString();
            this.Userid_textBox.Text = drX["操作员ID"].ToString();
            this.choscode_textBox.Text = drX["医疗机构编码"].ToString();
             str=drZ["设备名称"].ToString();
        }
       

     
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void Save_button_Click(object sender, EventArgs e)//保存
        {



            if (this.IOID_selText.Value == null)
            {
                WJs.alert("请填写下账类别");
                this.IOID_selText.Focus();
                return;


            }
            if (this.QLFARE_textBox.Text.Trim() != "")
            {
                decimal dec1;
                if (!decimal.TryParse(this.QLFARE_textBox.Text, out dec1) || dec1 < 0)
                {
                    WJs.alert("清理费只能为大于或等于零的数字");
                    this.QLFARE_textBox.Focus();
                    return;
                }


            }
            if (this.CVALUE_textBox.Text.Trim() != "")
            {
                decimal dec1;
                if (!decimal.TryParse(this.CVALUE_textBox.Text, out dec1) || dec1 < 0)
                {
                    WJs.alert("残值只能为大于或等于零的数字");
                    this.CVALUE_textBox.Focus();
                    return;
                }


            }
            //if (this.QLFARE_textBox.Text.Trim() == "")
            //{
            //    WJs.alert("请填写清理费");
            //    this.QLFARE_textBox.Focus();
            //    return ;
            //}
            if (this.CVALUE_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写残值");
                this.CVALUE_textBox.Focus();
                return;
            }
            if (this.NOREASON_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写下账原因");
                this.NOREASON_textBox.Focus();
                return;
            }
            if (this.reportman_selText.Value== null)
            {
                WJs.alert("请填写下申请人");
                this.reportman_selText.Focus();
                return;
            }


            //下面开始保存了
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQKeepAccountsManagSvr";

            ac.Sql = "ModifyOrAddInfo";
           
                AddRepairCardInfo(ac);
           
            
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
           
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
          
                WJs.alert(e.Msg.Msg);
                this.Close();
          
            
        }

        private void AddRepairCardInfo(ActionLoad ac)
        {
            if (isFlag == 1)
            {
                ac.Add("NOID", NOID_textBox.Text.Trim().ToString());
            }
            else
            {
                ac.Add("NOID", "");
            }

            ac.Add("CARDID", cardId_textBox.Value.Trim());
            ac.Add("DEPTID", deptid_selTextInpt.Value);
            ac.Add("IOID", this.IOID_selText.Value);

            ac.Add("QLFARE", this.QLFARE_textBox.Text);
            ac.Add("CVALUE", this.CVALUE_textBox.Text.Trim());

            ac.Add("NOREASON", this.NOREASON_textBox.Text);
            ac.Add("REPORTUSERID", this.reportman_selText.Value);
            ac.Add("REPORTMAN", this.reportman_selText.Text);
            ac.Add("REPORTDATE", this.dateTimePicker_REPORTDATE.Value);
            ac.Add("MEMO", memo_textBox.Text);
            ac.Add("USERID", Userid_textBox.Text);
            ac.Add("USERNAME", Username_textBox.Text);
            ac.Add("CHOSCODE", choscode_textBox.Text);
            ac.Add("RECDATE", recdate_dateTimePicker.Value);

            ac.Add("STATUS", Status_ytComboBox1.Value);
            
           
        }

        void reportman_selText_TextChanged(object sender, EventArgs e)
        {
           this.repotruserId_textBox.Text=this.reportman_selText.Value;
        
        }

             private void EQKeepAccountsManagEdit_Load(object sender, EventArgs e)
        {
            this.toolStrip1.Visible = false;
          
                 this.reportman_selText.TextChanged+=new EventHandler(reportman_selText_TextChanged);
                 
            IOID_selText.Sql = "GetKind_EQAccount";
            IOID_selText.SelParam = His.his.Choscode + "|{key}|{key}";

            reportman_selText.Sql = "YongHuMingInfo_EQAccount";
            reportman_selText.SelParam = His.his.Choscode + "|{key}|{key}";


          //this.NextCheckdate_dateTimePicker.Value = dt;
            TvList.newBind().add("作废", "0").add("有效", "1").Bind(Status_ytComboBox1);
            this.Status_ytComboBox1.SelectedIndex = 1;
            this.Status_ytComboBox1.Enabled = false;
            this.Userid_textBox.ReadOnly = true;
            this.Username_textBox.ReadOnly = true;
            this.choscode_textBox.ReadOnly = true;


            cardId_textBox.Value = drZ["卡片ID"].ToString();
            cardId_textBox.Text = drZ["卡片ID"].ToString();

            
            this.deptid_selTextInpt.Value = drZ["使用科室"].ToString();
            this.deptid_selTextInpt.Text = LData.Es("GetEQAskBuy_KSName_KeepFit", null, new object[] { His.his.Choscode, drZ["使用科室"].ToString() });

            if (isFlag == 0 || isFlag == 1 || isFlag == 3 || isFlag == 4 )
            {
                LoadInfo_EQFixManagEdit();
                if (isFlag == 0)
                {
                    this.Save_button.Enabled = false;


                }
            }
            else
            {
                recdate_dateTimePicker.Value = DateTime.Now;
                Userid_textBox.Text = His.his.UserId.ToString();
                Username_textBox.Text = His.his.UserName;
                choscode_textBox.Text = His.his.Choscode;
            }

            if (isFlag == 3)
            {
                this.Save_button.Enabled = false;
                this.toolStrip1.Visible = true;
                this.toolStrip_JDPass.Visible = true;
                this.toolStrip_JDNo.Visible = true;
                this.toolStrip_SHPass.Visible = false;
                this.toolStrip_SHNo.Visible = false;
                this.textBox_JDUSERID.Text = His.his.UserId.ToString();
                this.textBox_JDMAN.Text = His.his.UserName;

            }
            if (isFlag == 4)
            {
                this.Save_button.Enabled = false;
                this.toolStrip1.Visible = true;
                this.toolStrip_JDPass.Visible = false;
                this.toolStrip_JDNo.Visible = false;
                this.toolStrip_SHPass.Visible = true;
                this.toolStrip_SHNo.Visible = true;
                this.textBox_SHUSERID.Text = His.his.UserId.ToString();
                this.textBox_SHMAN.Text = His.his.UserName;
            }  
       
      
        }

             private void toolStrip_JDPass_Click(object sender, EventArgs e)
             {
                 if (this.textBox_JDADVICE.Text.Trim() == "")
                 {
                     WJs.alert("请填写鉴定意见！");
                     this.textBox_JDADVICE.Focus();
                     return;
                 }

                 ActionLoad ac = ActionLoad.Conn();
                 ac.Action = "LKWZSVR.lkeq.UseingEQ.EQKeepAccountsManagSvr";

                 ac.Sql = "JDPass";
                 ac.Add("NOID", NOID_textBox.Text.ToString());
                 ac.Add("JDUSERID",His.his.UserId);
                 ac.Add("JDMAN", His.his.UserName);
                 ac.Add("JDADVICE", this.textBox_JDADVICE.Text);
                 //ac.Add("JDDATE", recdate_dateTimePicker.Value);

                 ac.Add("STATUS", 4);

                 ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                 ac.Post();
             }

             private void toolStrip_JDNo_Click(object sender, EventArgs e)
             {
                 ActionLoad ac = ActionLoad.Conn();
                 ac.Action = "LKWZSVR.lkeq.UseingEQ.EQKeepAccountsManagSvr";
                 ac.Add("NOID", NOID_textBox.Text.Trim().ToString());
                 ac.Sql = "JDNo";
                 ac.Add("STATUS", 2);
                 ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                 ac.Post();
             }

             private void toolStrip_SHPass_Click(object sender, EventArgs e)
             {
                 if (this.textBox_SHADVICE.Text.Trim() == "")
                 {
                     WJs.alert("请填写审批意见！");
                     this.textBox_SHADVICE.Focus();
                     return;
                 }

                 ActionLoad ac = ActionLoad.Conn();
                 ac.Action = "LKWZSVR.lkeq.UseingEQ.EQKeepAccountsManagSvr";

                 ac.Sql = "SHPass";
                 ac.Add("NOID", NOID_textBox.Text.ToString());
                 ac.Add("SHUSERID", His.his.UserId);
                 ac.Add("SHMAN", His.his.UserName);
                 ac.Add("SHADVICE", this.textBox_JDADVICE.Text);
                 ac.Add("CARDID", drZ["卡片ID"].ToString());


                 ac.Add("STATUS", 6);

                 ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                 ac.Post();
             }

             private void toolStrip_SHNo_Click(object sender, EventArgs e)
             {
                 ActionLoad ac = ActionLoad.Conn();
                 ac.Action = "LKWZSVR.lkeq.UseingEQ.EQKeepAccountsManagSvr";

                 ac.Sql = "SHNo";
                 ac.Add("NOID", NOID_textBox.Text.ToString());
                 ac.Add("STATUS", 2);
                 ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                 ac.Post();
             }

       



    }
}
