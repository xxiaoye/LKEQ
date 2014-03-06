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
using YtClient;
using YtUtil.tool;
using YiTian.db;

namespace JiChuDictionary.form
{
    public partial class DateDivide_Add : Form
    {
        public DateDivide_Add()
        {
            InitializeComponent();
        }

        Dictionary<string, ObjItem> Info;
        public DateDivide_Add(Dictionary<string, ObjItem> Info)
        {
            this.Info = Info;
            isAdd = false;
            InitializeComponent();
        }


        public DateDivideManag Main;
        private bool isAdd = true;
        public bool isSc = false;
        string str;

        void ytDateTime_Start_TextChanged(object sender, EventArgs e)
        {
            this.yTextBox_Name.Text = this.ytDateTime_Start.Value.Year + "年" + this.ytDateTime_Start.Value.Month+ "月";
        }
        private void DateDivide_Add_Load(object sender, EventArgs e)//加载窗体
        {

          this.ytDateTime_Start.TextChanged+=new EventHandler(ytDateTime_Start_TextChanged);
            this.yTextBox_ID.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_ChCode.ReadOnly = true;
             this.ytDateTime_RECDATE.Enabled = false;
             this.ytDateTime_Start.Text = null;
             this.ytDateTime_End.Text= null;
            if (!isAdd)
            {
                
                this.yTextBox_UserID.Text = Info["操作员ID"].ToString();
                this.yTextBox_User.Text = Info["操作员姓名"].ToString();
                this.yTextBox_ChCode.Text = Info["医疗机构编码"].ToString();
                this.yTextBox_ID.Text = Info["期间划分ID"].ToString();
                this.ytDateTime_RECDATE.Value = Convert.ToDateTime(Info["修改时间"].ToString());
                this.ytDateTime_Start.Value = Convert.ToDateTime(Info["开始日期"].ToString());
                this.ytDateTime_End.Value = Convert.ToDateTime(Info["结束日期"].ToString());
                this.yTextBox_Name.Text = Info["期间划分名称"].ToString();
                if (Info["备注"] != null)
                {
                    this.yTextBox_Rec.Text = Info["备注"].ToString();
                }
                
               

            }
            else
            {
                this.yTextBox_User.Text = His.his.UserName.ToString();
                this.yTextBox_UserID.Text = His.his.UserId.ToString();
                this.yTextBox_ChCode.Text = His.his.Choscode.ToString();
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            isSc = false;//什么标志位
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (this.yTextBox_Name.Text.Trim().Length == 0)
            {
                WJs.alert("请输入类别名称！");
                this.yTextBox_Name.Focus();
                return;
            }

            if (this.ytDateTime_Start.Value == null)
            {
                WJs.alert("请选择开始日期！");
                ytDateTime_Start.Focus();
                return;
            }
            if (this.ytDateTime_End.Value == null)
            {
                WJs.alert("请选择结束日期！");
                ytDateTime_End.Focus();
                return;
            }
            if (this.ytDateTime_End.Value.Date <= this.ytDateTime_Start.Value.Date)
            {
                WJs.alert("开始日期必须大于结束日期！");
                ytDateTime_Start.Focus();
                return;
            }
            /////////////////////////////////////////////////////////////////////////

            if (!isAdd)
            {

                str = LData.Es("EQIfIsTheDate_DateDivide_Add1", null, new object[] { His.his.Choscode,Info["期间划分ID"].ToInt() });
            }
            else
            {
              str = LData.Es("EQIfIsTheDate_DateDivide_Add", null, new object[] { His.his.Choscode });
            }

            if (str != null)
            {
                if (ytDateTime_Start.Value < DateTime.Parse(str))
                {
                    WJs.alert("开始日期必须大于上一个期间划分里的结束日期！");
                    ytDateTime_Start.Focus();
                    return;
                }
            }

            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDateDivideSvr";
            ac.Sql = "SaveDateDivideInfo";
            ac.Add("DATENAME", this.yTextBox_Name.Text);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);

            ac.Add("BEGINDATE", this.ytDateTime_Start.Value.Date.ToShortDateString());
            ac.Add("ENDDATE", this.ytDateTime_End.Value.Date.ToShortDateString());
            ac.Add("MEMO", this.yTextBox_Rec.Text);

            if (!isAdd)
            {
                ac.Add("DATEID", Info["期间划分ID"].ToInt());
            }
            else
            {
                ac.Add("DATEID", null);
            }
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            ac.Post();
        }

        void ac_ServiceFaiLoad(object sender, YtClient.data.events.LoadFaiEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {

            WJs.alert(e.Msg.Msg);
            Main.ReLoadData();
            if (!isAdd || !WJs.confirm("是否继续添加期间划分信息？"))
            {
                isSc = true;
                this.Close();
            }
            else
            {

                this.yTextBox_Name.Text = this.ytDateTime_Start.Value.Year + "年" + this.ytDateTime_Start.Value.Month + "月";

                //this.yTextBox_Name.Clear();

                this.yTextBox_Rec.Clear();

            }
        }

       
      

     
    }
}
