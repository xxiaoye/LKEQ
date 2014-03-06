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

    public partial class EQCheckManagEdit : Form
    {
        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag; //0 浏览  1 编辑   2 新增
        int Status;//1 报修状态  2  维修状态  6 交付状态   10 编辑全能【其余的浏览和新增也是如此】
        string str;
        bool isOk;
        DateTime dt;
        public EQCheckManagEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
           

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
           
        }

        private void LoadInfo_EQFixManagEdit()  //如果为修改或者浏览时执行
        {

            Checkid_textBox.Text = drX["检查ID"].ToString();
            memo_textBox.Text = drX["备注"].ToString();
            Status_ytComboBox1.Value = drX["状态"].ToString();
            NextCheckdate_dateTimePicker.Value = drX["下次检定日期"].ToDateTime();
            CHECKRESULT_textBox.Text = drX["检查结论"].ToString();
            CheckPeople_textBox.Text = drX["检查人"].ToString();
            this.textBox_CheckUnit.Text = drX["检查单位"].ToString();
            this.dateTimePicker_CheckDate.Value = drX["检查日期"].ToDateTime();

            CheckThing_textBox.Text = drX["简要记录"].ToString();
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



            if (this.textBox_CheckUnit.Text.Trim() == "")
            {
                WJs.alert("请填写检查单位");
                this.textBox_CheckUnit.Focus();
                return;


            }
            if (this.CheckPeople_textBox.Text.Trim() == "")
            {
                WJs.alert("请填写检查人");
                this.CheckPeople_textBox.Focus();
                return ;
            }


           string str1= LData.Es("IfTheJiLiang_EQKeepFit",null,new object[]{  str,His.his.Choscode});
             if (str1 == "1")
            {
                dt = this.NextCheckdate_dateTimePicker.Value;
                if (this.NextCheckdate_dateTimePicker.Value ==this.dateTimePicker_CheckDate.Value)
                {
                    WJs.alert("请填写下次检定日期！");
                    this.NextCheckdate_dateTimePicker.Focus();
                    return;
                }
            }
            

         
            
            //下面开始保存了
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQCheckManagSvr";

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
                ac.Add("CHECKID", Checkid_textBox.Text.Trim().ToString());
            }
            else
            {
                ac.Add("CHECKID", "");
            }

            ac.Add("CARDID", cardId_textBox.Value.Trim());
            ac.Add("DEPTID", deptid_selTextInpt.Value);
            ac.Add("CHECKUNIT", this.textBox_CheckUnit.Text.ToString());
            ac.Add("CHECKDATE", this.dateTimePicker_CheckDate.Value);
            ac.Add("CHECKMAN", this.CheckPeople_textBox.Text.Trim());
            ac.Add("CHECKTHING", this.CheckThing_textBox.Text);
            ac.Add("CHECKRESULT", this.CHECKRESULT_textBox.Text);
            if (dt.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                ac.Add("NEXTCHECKDATE", this.NextCheckdate_dateTimePicker.Value);

            }
            else
            {
                ac.Add("NEXTCHECKDATE", null);

            }
           
         
            ac.Add("MEMO", memo_textBox.Text);
            ac.Add("USERID", Userid_textBox.Text);
            ac.Add("USERNAME", Username_textBox.Text);
            ac.Add("CHOSCODE", choscode_textBox.Text);
            ac.Add("RECDATE", recdate_dateTimePicker.Value);

            ac.Add("STATUS", Status_ytComboBox1.Value);
            
           
        }

        private void EQCheckManagEdit_Load(object sender, EventArgs e)
        {
          
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



    }
}
