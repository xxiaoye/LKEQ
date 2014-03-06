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
    public partial class FixKind_Add : Form
    {
        public FixKind_Add()
        {
            InitializeComponent();
        }

        Dictionary<string, ObjItem> Info;
        public FixKind_Add(Dictionary<string, ObjItem> Info)
        {
            this.Info = Info;
            isAdd = false;
            InitializeComponent();
        }


        public FixKindManag Main;
        private bool isAdd = true;
        public bool isSc = false;

        private void FixKind_Add_Load(object sender, EventArgs e)//加载窗体
        {

            this.yTextBox_Name.TextChanged += new EventHandler(yTextBox_Name_TextChanged);
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ytComboBox_ifUse);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfDefault);
            this.yTextBox_FixKindCode.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_ChCode.ReadOnly = true;
             this.ytDateTime_RECDATE.Enabled = false;
             this.ytComboBox_ifUse.SelectedIndex = 0;
            if (!isAdd)
            {
                this.yTextBox_Name.Text = Info["类别名称"].ToString();
                this.yTextBox_UserID.Text = Info["操作员ID"].ToString();
                this.yTextBox_User.Text = Info["操作员姓名"].ToString();
                this.ytComboBox_ifUse.Value = Info["是否使用"].ToString();
                this.yTextBox_ChCode.Text = Info["医疗机构编码"].ToString();
                this.ytComboBox_IfDefault.Value = Info["是否默认值"].ToString();
                this.yTextBox_FixKindCode.Text = Info["类别编码"].ToString();
                this.ytDateTime_RECDATE.Value = Convert.ToDateTime(Info["修改时间"].ToString());
                if (Info["拼音码"] != null)
                {
                    this.yTextBox_PY.Text = Info["拼音码"].ToString();
                }
                if (Info["五笔码"] != null)
                {
                    this.yTextBox_WB.Text = Info["五笔码"].ToString();
                }
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


        void yTextBox_Name_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_Name.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_PY.Text = PyWbCode.getPyCode(n).ToLower();
                this.yTextBox_WB.Text = PyWbCode.getWbCode(n).ToLower();
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
           
            if (this.ytComboBox_ifUse.SelectedIndex == -1)
            {
                WJs.alert("请选择是否使用！");
                ytComboBox_ifUse.Focus();
                return;
            }
            if (this.ytComboBox_IfDefault.SelectedIndex == -1)
            {
                WJs.alert("请选择是否默认值！");
                ytComboBox_IfDefault.Focus();
                return;
            }
            string str = LData.Es("EQMostLarge_REPAIRCODE", null, new object[] { His.his.Choscode });
            //if (str != null)
            //{
                if (str == "99")
                {
                    WJs.alert("编码已达到最大数，不允许新增操作！");

                    return;

                }

            //}
            //else
            //{
            //    return;
            //}
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQFixKindSvr";
            ac.Sql = "SaveFixKindInfo";
            ac.Add("IFDEFAULT", TvList.getValue(this.ytComboBox_IfDefault).ToInt());
            ac.Add("IFUSE", TvList.getValue(this.ytComboBox_ifUse).ToInt());
            ac.Add("REPAIRNAME", this.yTextBox_Name.Text);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("PYCODE", this.yTextBox_PY.Text);
            ac.Add("WBCODE", this.yTextBox_WB.Text);
            ac.Add("MEMO", this.yTextBox_Rec.Text);

            if (!isAdd)
            {
                ac.Add("REPAIRCODE", Info["类别编码"].ToInt());
            }
            else            
            {
                ac.Add("REPAIRCODE", null);
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
            if (!isAdd || !WJs.confirm("是否继续添加维修类别信息？"))
            {
                isSc = true;
                this.Close();
            }
            else
            {
                this.ytComboBox_IfDefault.SelectedIndex = -1;
                this.ytComboBox_ifUse.SelectedIndex = 0;
                this.yTextBox_Name.Clear();
                this.yTextBox_PY.Clear();
                this.yTextBox_WB.Clear();
                this.yTextBox_Rec.Clear();

            }
        }


       
      

     
    }
}
