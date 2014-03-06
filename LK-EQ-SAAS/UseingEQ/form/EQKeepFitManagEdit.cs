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

    public partial class EQKeepFitManagEdit : Form
    {
        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag; //0 浏览  1 编辑   2 新增
        int Status;//1 报修状态  2  维修状态  6 交付状态   10 编辑全能【其余的浏览和新增也是如此】

        bool isOk;

        public EQKeepFitManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
           

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
           
        }

        private void LoadInfo_EQFixManagEdit()  //如果为修改或者浏览时执行
        {

            Maintainid_textBox.Text = drX["保养ID"].ToString();
            memo_textBox.Text = drX["备注"].ToString();
            Status_ytComboBox1.Value = drX["状态"].ToString();

            //Maintaincode_selText.Text = drX["保养类别名称"].ToString();
            Maintaincode_selText.Value = drX["保养类型"].ToString();
            this.Maintaincode_selText.Text = LData.Es("GetEQMaintainName_KeepFit", null, new object[] { drX["保养类型"].ToString() });


            Maintaindate_dateTimePicker.Value = drX["保养日期"].ToDateTime();
            MaintainThing_textBox.Text = drX["保养情况"].ToString();
            //reportman_textBox.Text = drX["报修人"].ToString();

            BaoyangPeople_textBox.Text = drX["保养人"].ToString();
            textBox_RepairPeople.Text = drX["维修人"].ToString();

            RepairDeptId_selText.Value = drX["维修科室"].ToString();
           // RepairDeptId_selText.Text = drX["维修科室名称"].ToString();
            this.RepairDeptId_selText.Text = LData.Es("GetEQAskBuy_KSName_KeepFit", null, new object[] {His.his.Choscode, drX["维修科室"].ToString() });

   
            CLfare_textBox.Text = drX["材料费"].ToString();
            Maintainfare_textBox.Text = drX["保养费"].ToString();
            OtherFare_textBox.Text = drX["其它"].ToString();
 
            ChangeParts_textBox.Text = drX["更换部件"].ToString();
            recdate_dateTimePicker.Value = drX["修改时间"].ToDateTime();


            this.Username_textBox.Text = drX["操作员姓名"].ToString();
            this.Userid_textBox.Text = drX["操作员ID"].ToString();
            this.choscode_textBox.Text = drX["医疗机构编码"].ToString();

        }
        private void EQFixManagEdit_Load(object sender, EventArgs e)//窗体加载
        {

            TvList.newBind().add("作废", "0").add("有效", "1").Bind(Status_ytComboBox1);
            this.Status_ytComboBox1.SelectedIndex = 1;
            this.Status_ytComboBox1.Enabled = false;
            this.Userid_textBox.ReadOnly = true;
            this.Username_textBox.ReadOnly = true;
            this.choscode_textBox.ReadOnly = true;

            Maintaincode_selText.Sql = "GetMainTainCode_KeepFit";
            Maintaincode_selText.SelParam = His.his.Choscode + "|{key}|{key}";

            RepairDeptId_selText.Sql = "DeptidBindInfo_EQFixManagEdit";
            RepairDeptId_selText.SelParam = His.his.Choscode + "|{key}|{key}";

            cardId_textBox.Value = drZ["卡片ID"].ToString();
            cardId_textBox.Text = drZ["卡片ID"].ToString();

            
            this.deptid_selTextInpt.Value = drZ["使用科室"].ToString();
            this.deptid_selTextInpt.Text = LData.Es("GetEQAskBuy_KSName_KeepFit", null, new object[] { His.his.Choscode, drZ["使用科室"].ToString() });

            if (isFlag == 0 || isFlag == 1)
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


        }

     
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void Save_button_Click(object sender, EventArgs e)//保存
        {
       

            //if (this.cardId_textBox.Value == null)
            //{
            //    WJs.alert("请填写卡片");
            //    this.cardId_textBox.Focus();
            //    return;


            //}
            if (this.Maintaincode_selText.Value == null)
            {
                WJs.alert("请填保养类别");
                this.Maintaincode_selText.Focus();
                return;


            }
            if (this.BaoyangPeople_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写保养人");
                this.BaoyangPeople_textBox.Focus();
                return ;
            }
            if (this.Maintaincode_selText.Text.Trim() != "")
            {
                decimal dec1;
                if (!decimal.TryParse(this.Maintainfare_textBox.Text, out dec1) || dec1<0)
                {
                    WJs.alert("保养费只能为大于或等于零的数字");
                    this.Maintainfare_textBox.Focus();
                    return;
                }
                
              
            }
            if (this.CLfare_textBox.Text.Trim() != "")
            {
                decimal dec2;
                if (!decimal.TryParse(this.CLfare_textBox.Text, out dec2) || dec2 < 0)
                {
                    WJs.alert("材料费只能为大于或等于零的数字");
                    this.Maintaincode_selText.Focus();
                    return;
                }


            }
            if (this.CLfare_textBox.Text.Trim() != "")
            {
                decimal dec3;
                if (!decimal.TryParse(this.OtherFare_textBox.Text, out dec3) || dec3 < 0)
                {
                    WJs.alert("其它费用只能为大于或等于零的数字");
                    this.OtherFare_textBox.Focus();
                    return;
                }


            }


               

          

            
            //下面开始保存了
            ActionLoad ac = ActionLoad.Conn();
            ac.Action="LKWZSVR.lkeq.UseingEQ.EQKeepFitManagSvr";

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
                ac.Add("MAINTAINID", Maintainid_textBox.Text.Trim().ToString());
            }
            else
            {
                ac.Add("MAINTAINID", "");
            }

            ac.Add("CARDID", cardId_textBox.Value.Trim());
            ac.Add("DEPTID", deptid_selTextInpt.Value);
            ac.Add("MAINTAINTYPE", Maintaincode_selText.Value);
            ac.Add("MAINTAINMAN", this.BaoyangPeople_textBox.Text.Trim());
            ac.Add("MAINTAINTHING", MaintainThing_textBox.Text.Trim());
            ac.Add("CHANGETHING", ChangeParts_textBox.Text);
            ac.Add("MAINTAINFARE",Math.Round( decimal.Parse(Maintainfare_textBox.Text),4));
            ac.Add("CLFARE", Math.Round(decimal.Parse(CLfare_textBox.Text), 4));
            ac.Add("OTHERFARE", Math.Round(decimal.Parse(OtherFare_textBox.Text), 4));
            ac.Add("MAINTAINDATE", Maintaindate_dateTimePicker.Value);
            ac.Add("REPAIRMAN", this.textBox_RepairPeople.Text);

            ac.Add("REPAIRDEPTID", this.RepairDeptId_selText.Value);
            ac.Add("MEMO", memo_textBox.Text);
            ac.Add("USERID", Userid_textBox.Text);
            ac.Add("USERNAME", Username_textBox.Text);
            ac.Add("CHOSCODE", choscode_textBox.Text);
            ac.Add("RECDATE", recdate_dateTimePicker.Value);

            ac.Add("STATUS", Status_ytComboBox1.Value);
            
           
        }


    }
}
