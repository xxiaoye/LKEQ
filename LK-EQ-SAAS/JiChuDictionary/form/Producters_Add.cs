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
    public partial class Producters_Add : Form
    {
        public Producters_Add()
        {
            InitializeComponent();
        }

        Dictionary<string, ObjItem> Info;
        public Producters_Add(Dictionary<string, ObjItem> Info)
        {
            this.Info = Info;
            isAdd = false;
            InitializeComponent();
        }


        public ProducterManag Main;
        private bool isAdd = true;
        public bool isSc = false;

        private void Producters_Add_Load(object sender, EventArgs e)//加载窗体
        {

            this.yTextBox_Name.TextChanged += new EventHandler(yTextBox_Name_TextChanged);
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ytComboBox_ifUse);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfMake);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IfAfford);
            TvList.newBind().SetCacheKey("KsData").Load("EQFindHisInfoDict", null).Bind(this.ytComboBox_Pperty);
            this.yTextBox_SupplyID.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_ChCode.ReadOnly = true;
            this.ytComboBox_ifUse.SelectedIndex = 0;
            // this.dateTimePicker1.Enabled = false;
            if (!isAdd)
            {
                this.yTextBox_Name.Text = Info["厂商名称"].ToString();
                this.yTextBox_User.Text = Info["操作员姓名"].ToString();
                this.ytComboBox_ifUse.Value = Info["是否使用"].ToString();
                this.yTextBox_ChCode.Text = Info["医疗机构编码"].ToString();
                this.ytComboBox_IfMake.Value = Info["是否生产厂家"].ToString();
                this.ytComboBox_IfAfford.Value = Info["是否供应商"].ToString();
                this.yTextBox_SupplyID.Text = Info["厂商ID"].ToString();
              
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
                if (Info["地址"] != null)
                {
                    this.yTextBox_Address.Text = Info["地址"].ToString();
                }
                if (Info["企业性质"] != null)
                {
                    this.ytComboBox_Pperty.Value = Info["企业性质"].ToString();
                } if (Info["开户银行"] != null)
                {
                    this.yTextBox_Bank.Text = Info["开户银行"].ToString();
                }
                if (Info["开户账号"] != null)
                {
                    this.yTextBox_BANKACCOUNT.Text = Info["开户账号"].ToString();
                }
                if (Info["传真号码"] != null)
                {
                    this.yTextBox_Fax.Text = Info["传真号码"].ToString();
                }
                if (Info["法人代表"] != null)
                {
                    this.yTextBox_FRDB.Text = Info["法人代表"].ToString();
                } if (Info["经营许可证"] != null)
                {
                    this.yTextBox_JYXKZ.Text = Info["经营许可证"].ToString();
                }
                if (Info["联系电话"] != null)
                {
                    this.yTextBox_Phone.Text = Info["联系电话"].ToString();
                }
                if (Info["邮政编码"] != null)
                {
                    this.yTextBox_POST.Text = Info["邮政编码"].ToString();
                }
                if (Info["企业代码"] != null)
                {
                    this.yTextBox_QYDM.Text = Info["企业代码"].ToString();
                } if (Info["联系人"] != null)
                {
                    this.yTextBox_RELMAN.Text = Info["联系人"].ToString();
                }
                if (Info["营业执照"] != null)
                {
                    this.yTextBox_YYZZ.Text = Info["营业执照"].ToString();
                }
                if (Info["税务登记号"] != null)
                {
                    this.yTextBox_SWDJH.Text = Info["税务登记号"].ToString();
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
                WJs.alert("请输入厂商名称！");
                this.yTextBox_Name.Focus();
                return;
            }
            if (this.ytComboBox_IfMake.SelectedIndex == -1)
            {
                WJs.alert("请选择是否生产厂家！");
                ytComboBox_IfMake.Focus();
                return;
            }
            if (this.ytComboBox_IfAfford.SelectedIndex == -1)
            {
                WJs.alert("请选择是否供应商！");
                ytComboBox_IfAfford.Focus();
                return;
            }
            if (this.ytComboBox_ifUse.SelectedIndex == -1)
            {
                WJs.alert("请选择是否使用！");
                ytComboBox_ifUse.Focus();
                return;
            }
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQSupplySvr";
            ac.Sql = "SaveChangShangInfo";
            ac.Add("IFFACTORY",TvList.getValue(this.ytComboBox_IfMake).ToInt());
            ac.Add("IFSUPPLY", TvList.getValue(this.ytComboBox_IfAfford).ToInt());
            ac.Add("IFUSE", TvList.getValue(this.ytComboBox_ifUse).ToInt());
            ac.Add("SUPPLYNAME", this.yTextBox_Name.Text);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("PYCODE", this.yTextBox_PY.Text);
            ac.Add("WBCODE", this.yTextBox_WB.Text);
            ac.Add("UNITPROPERTY", TvList.getValue(this.ytComboBox_Pperty).ToInt());
            ac.Add("QYDM", this.yTextBox_QYDM.Text);
            ac.Add("FRDB", this.yTextBox_FRDB.Text);
            ac.Add("UNITBANK", this.yTextBox_Bank.Text);
            ac.Add("BANKACCOUNT", this.yTextBox_BANKACCOUNT.Text);
            ac.Add("ADDRESS", this.yTextBox_Address.Text);
            ac.Add("RELMAN", this.yTextBox_RELMAN.Text);
            ac.Add("RELPHONE", this.yTextBox_Phone.Text);
            ac.Add("POST", this.yTextBox_POST.Text);
            ac.Add("FAX", this.yTextBox_Fax.Text);
            ac.Add("JYXKZ", this.yTextBox_JYXKZ.Text);
            ac.Add("YYZZ", this.yTextBox_YYZZ.Text);
            ac.Add("MEMO", this.yTextBox_Rec.Text);
            ac.Add("TAXCODE", this.yTextBox_SWDJH.Text);

            if (!isAdd)
            {
                ac.Add("SUPPLYID", Info["厂商ID"].ToInt());
            }
            else            
            {
                ac.Add("SUPPLYID",null);
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
            if (!isAdd || !WJs.confirm("是否继续添加厂商信息？"))
            {
                isSc = true;
                this.Close();
            }
            else
            {
                this.ytComboBox_IfAfford.SelectedIndex = -1;
                this.ytComboBox_IfMake.SelectedIndex = -1;
                this.ytComboBox_ifUse.SelectedIndex = 0;
                this.ytComboBox_Pperty.SelectedIndex = -1;
                this.yTextBox_Address.Clear();
                this.yTextBox_Bank.Clear();
                this.yTextBox_BANKACCOUNT.Clear();
                this.yTextBox_Fax.Clear();
                this.yTextBox_FRDB.Clear();
                this.yTextBox_JYXKZ.Clear();
                this.yTextBox_Name.Clear();
                this.yTextBox_Phone.Clear();
                this.yTextBox_POST.Clear();
                this.yTextBox_PY.Clear();
                this.yTextBox_QYDM.Clear();
                this.yTextBox_Rec.Clear();
                this.yTextBox_RELMAN.Clear();
                this.yTextBox_SupplyID.Clear();
                this.yTextBox_WB.Clear();
                this.yTextBox_YYZZ.Clear();
                this.yTextBox_SWDJH.Clear();
            }
        }

       
      

     
    }
}
