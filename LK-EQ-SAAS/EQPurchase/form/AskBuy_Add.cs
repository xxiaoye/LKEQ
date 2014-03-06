using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using ChSys;
using YtWinContrl.com;
using YtClient;

namespace EQPurchase.form
{
    public partial class AskBuy_Add : Form
    {
        public AskBuy_Add()
        {
            InitializeComponent();
        }
        DataRow r;
        private bool isAdd=false;
        private string Status="";
        int a;
        int QGNum;
        public AskBuy_Add(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public AskBuy_Add(DataRow r, bool _isAdd, string _Status)
        {
            Status = _Status;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public AskBuy AskB;

        void selTextInpt_EQID_TextChanged(object sender, EventArgs e)
        {
           DataTable dt= LData.LoadDataTable("EQIDOtherAskBuy_Add", new object[] { His.his.Choscode, this.selTextInpt_EQID.Text });
          if(dt!=null)
          {
            DataRow dr = dt.Rows[0];
            if (dr != null)
            {
                this.yTextBox_Name.Text = dr["EQNAME"].ToString();
                this.yTextBox_GuiG.Text = dr["GG"].ToString();
                this.yTextBox_XH.Text = dr["XH"].ToString();
                this.ytComboBox_Unitcode.Value = dr["UNITCODE"].ToString();
            }
          }
        }
        void EQYJMONEY(object sender, EventArgs e)
        {
            if (this.selTextInpt_EQID.Text.Trim().Length <= 0)
            {
                //WJs.alert("请先选择设备ID！"); //避免没有先输入设备ID而导致的查询错误。
                this.yTextBox_QGNum.Clear();
                this.selTextInpt_EQID.Focus();
                return;
            }
            if (!int.TryParse(this.yTextBox_QGNum.Text, out QGNum))
            {
                this.yTextBox_QGNum.Clear();
                this.yTextBox_QGNum.Focus();
                WJs.alert("请购的数量必须是大于零的整数！");
                return;
            }
            else if (QGNum <= 0)
            {
                this.yTextBox_QGNum.Clear();
                this.yTextBox_QGNum.Focus();
                WJs.alert("请购的数量必须是大于零的整数！");
                return;
            }
            if (this.yTextBox_YJPRICE.Text.Trim().Length <= 0)
            {
                string price = LData.Es("EQFindPrice_AskBuy_Add", null, new object[] { His.his.Choscode, this.selTextInpt_EQID.Text });

                
                
                if (price != null)
                {
                    this.yTextBox_YJMONEY.Text = (int.Parse(this.yTextBox_QGNum.Text) * int.Parse(price)).ToString();
                }
                else
                {
                    this.yTextBox_YJMONEY.Text = (int.Parse(this.yTextBox_QGNum.Text) * 0).ToString();
              
                
                }
            }
            else
            {
                decimal dec;
                if (decimal.TryParse(this.yTextBox_YJPRICE.Text.Trim(), out dec))
                {
                    if (dec < 0)
                    {
                        WJs.alert("预计单价必须是大于零的实数！");
                        yTextBox_YJPRICE.Focus();
                        return;

                    }
                }

                this.yTextBox_YJMONEY.Text = (int.Parse(this.yTextBox_QGNum.Text) * int.Parse(this.yTextBox_YJPRICE.Text)).ToString();

            }
         
        }
        private void WZDict_Add_Load(object sender, EventArgs e)
        {

            this.selTextInpt_EQID.TextChanged+=new EventHandler(selTextInpt_EQID_TextChanged);
            this.yTextBox_QGNum.TextChanged += new EventHandler(EQYJMONEY);
            this.yTextBox_YJPRICE.TextChanged += new EventHandler(EQYJMONEY);
            this.selTextInpt_Country.Text = "中国";
            TvList.newBind().add("已审核", "6").add("审核被拒", "2").add("待审核", "1").add("已删除", "0").Bind(this.ytComboBox_Status);
            TvList.newBind().Load("EQUnitCodeAskBuy_Add",null).Bind(this.ytComboBox_Unitcode);
         
            this.selTextInpt_EQID.SelParam = His.his.Choscode + "|{key}|{key}|{key}";//为什么要这样写？
            selTextInpt_EQID.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_EQID.Sql = "EQIDAskBuy_Add";
            
            //这里更改了单位编码

            this.selTextInpt_QGKSID.SelParam = His.his.Choscode + "|{key}|{key}|{key}";//为什么要这样写？
            selTextInpt_QGKSID.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_QGKSID.Sql = "AskBuy_FindKSID";

            this.selTextInpt_Country.SelParam = "{key}|{key}|{key}|";//为什么要这样写？
            selTextInpt_Country.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_Country.Sql = "CountryAskBuy_Add";

            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_Choscode.ReadOnly = true;
            this.ytDateTime_RECDATE.Enabled = false;
            this.ytDateTime_SHTime.Enabled = false;     
            this.yTextBox_SHUserID.ReadOnly = true;
            this.yTextBox_SHName.ReadOnly = true;
            this.button_CG.Visible = false;
            this.button_SB.Visible = false;
            this.yTextBox_QGID.ReadOnly = true;

            this.yTextBox_Name.ReadOnly = true;
            this.yTextBox_GuiG.ReadOnly = true;
            this.ytComboBox_Unitcode.Enabled = false;
            this.yTextBox_XH.ReadOnly = true;
            this.yTextBox_YJMONEY.ReadOnly = true;
            this.ytComboBox_Status.Enabled = false;
            this.yTextBox_SPNum.ReadOnly = true;

            this.ytDateTime_SHTime.Visible = false;
            this.yTextBox_SHName.Visible = false;
            this.yTextBox_SHUserID.Visible = false;
            this.label16.Visible = false;
            this.label21.Visible = false;
            this.label22.Visible = false;
            
            if (!this.isAdd)//编辑
            {
                this.yTextBox_QGID.Text = r["APPLYID"].ToString();
                this.selTextInpt_QGKSID.Text = LData.Exe("GetEQAskBuy_KSName", null, new object[] { His.his.Choscode, r["DEPTID"].ToString() });
                this.selTextInpt_QGKSID.Value = r["DEPTID"].ToString();

                //this.selTextInpt_EQID.Text = LData.Exe("GetEQAskBuy_EQName", null, new object[] { His.his.Choscode, r["EQID"].ToString() });
               
                this.selTextInpt_EQID.Text = r["EQID"].ToString();
                this.selTextInpt_EQID.Value = r["EQNAME"].ToString();

                this.yTextBox_Name.Text = r["EQNAME"].ToString();

                if (r["GG"] != null)
                {
                    this.yTextBox_GuiG.Text = r["GG"].ToString();
                } if (r["XH"] != null)
                {
                    this.yTextBox_XH.Text = r["XH"].ToString();
                }


                this.selTextInpt_Country.Text = r["COUNTRY"].ToString();
                this.selTextInpt_Country.Value = LData.Exe("GetEQAskBuy_EQCountry", null, new object[] { His.his.Choscode, r["COUNTRY"].ToString() });

     

                this.ytComboBox_Unitcode.Value = r["UNITCODE"].ToString();


                this.yTextBox_QGNum.Text = r["APPLYNUM"].ToString();

           
                if (r["YJPRICE"] != null)
                {
                    this.yTextBox_YJPRICE.Text = r["YJPRICE"].ToString();
                }

                this.yTextBox_YJMONEY.Text= r["YJMONEY"].ToString();

                if (r["REASON"] != null)
                {
                    this.yTextBox_REASON.Text = r["REASON"].ToString();
                }
                if (r["XYFX"] != null)
                {
                    this.yTextBox_XYFX.Text = r["XYFX"].ToString();
                }
                if (r["PTTJ"] != null)
                {
                    this.yTextBox_PTTJ.Text = r["PTTJ"].ToString();
                }
                this.ytDateTime_MakeTime.Value = Convert.ToDateTime( r["PLANDATE"]);


                this.ytComboBox_Status.Value = r["STATUS"].ToString();

                if (r["MEMO"] != null)
                {
                    this.yTextBox_Rec.Text = r["MEMO"].ToString();
                }
                this.yTextBox_UserID.Text = r["USERID"].ToString();
               
                if (r["USERNAME"] != null)
                {
                    this.yTextBox_User.Text = r["USERNAME"].ToString();
                }

                this.ytDateTime_RECDATE.Value = Convert.ToDateTime(r["RECDATE"]);

         

                this.yTextBox_Choscode.Text = r["CHOSCODE"].ToString();
              

            }
            else if (this.isAdd) //新增
            {
                this.yTextBox_User.Text = His.his.UserName;
               
                this.yTextBox_UserID.Text = His.his.UserId.ToString();
              
                this.yTextBox_Choscode.Text = His.his.Choscode;
                this.ytComboBox_Status.SelectedIndex = 2;
            }

            if (this.Status == "CK")
            {
                if (r["SHNUM"] != null)
                {
                    this.yTextBox_SPNum.Text = r["SHNUM"].ToString();
                }
                if (this.yTextBox_SPNum.Text.Trim().Length > 0)
                {
                    this.ytDateTime_SHTime.Visible = true;
                    this.yTextBox_SHName.Visible = true;
                    this.yTextBox_SHUserID.Visible = true;
                    this.label16.Visible = true;
                    this.label21.Visible = true;
                    this.label22.Visible = true;
                    if (r["SHUSERID"] != null)
                    {
                        this.yTextBox_SHUserID.Text = r["SHUSERID"].ToString();
                    }
                    if (r["SHDATE"] != null)
                    {
                        this.ytDateTime_SHTime.Value = Convert.ToDateTime( r["SHDATE"].ToString());
                    }
                    if (r["SHUSERNAME"] != null)
                    {
                        this.yTextBox_SHName.Text = r["SHUSERNAME"].ToString();
                    }
                  
                   
                }
                this.btn_Save.Enabled = false;

            }

            if (this.Status == "SH")
            {
                this.ytDateTime_SHTime.Visible = true;
                this.yTextBox_SHName.Visible = true;
                this.yTextBox_SHUserID.Visible = true;
                this.label16.Visible = true;
                this.label21.Visible = true;
                this.label22.Visible = true;
                this.label18.ForeColor =System.Drawing.Color.Blue;
                this.btn_Save.Visible = false;
                this.button_CG.Visible = true;
                this.button_SB.Visible = true;
                this.yTextBox_SHName.Text = His.his.UserName;
                this.yTextBox_SPNum.ReadOnly = false;
                this.yTextBox_SHUserID.Text = His.his.UserId.ToString();

                this.ytDateTime_SHTime.Value = DateTime.Now;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.selTextInpt_QGKSID.Value==null || this.selTextInpt_QGKSID.Value.Trim().Length == 0)
            {
                WJs.alert("请输入设备ID！");
                this.yTextBox_Name.Focus();
                return;
            }
           
            if (this.selTextInpt_QGKSID.Value == null)
            {
                WJs.alert("请选择请购科室！");
                selTextInpt_QGKSID.Focus();
                return;
            }
            if (this.selTextInpt_Country.Text.Trim().ToString() != "中国" && this.selTextInpt_Country.Value==null)
            {
                WJs.alert("请选择国别！");
                selTextInpt_Country.Focus();
                return;
            }
            if (this.yTextBox_QGNum.Text.Trim().Length <= 0)
            {
                WJs.alert("请输入请购数量！");
                yTextBox_QGNum.Focus();
                return;


            }
            else 
            {
                if (!int.TryParse(this.yTextBox_QGNum.Text, out a) || a<0)
                {
                    WJs.alert("请购数量必须是大于零的整数！");
                    yTextBox_QGNum.Focus();
                    return;
                }
            
            }
            if (this.yTextBox_YJPRICE.Text.Trim().Length > 0)
            { 
                decimal dec;
                if (decimal.TryParse(this.yTextBox_YJPRICE.Text.Trim(), out dec))
                {
                    if (dec < 0)
                    {
                        WJs.alert("预计单价必须是大于零的实数！");
                        yTextBox_YJPRICE.Focus();
                        return;

                    }
                }
                else
                {
                    WJs.alert("预计单价必须是大于零的实数！");
                    yTextBox_YJPRICE.Focus();
                    return;
                }
             
            }

            if (this.yTextBox_YJMONEY.Text.Trim().Length<=0)
            {
                WJs.alert("系统错误，请重新开始！");
                return;
            }


          
           
         
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.EQPurchase.EQAskBuySvr";
            ac.Sql = "SaveEQAskBuyInfo";
            ac.Add("DEPTID", this.selTextInpt_QGKSID.Value);
            ac.Add("EQID", this.selTextInpt_EQID.Text);
            ac.Add("EQNAME", this.yTextBox_Name.Text);
            ac.Add("GG", this.yTextBox_GuiG.Text);
            ac.Add("XH", this.yTextBox_XH.Text);

            ac.Add("COUNTRY", this.selTextInpt_Country.Text);
            ac.Add("UNITCODE", TvList.getValue(this.ytComboBox_Unitcode).ToString());
            ac.Add("APPLYNUM", this.yTextBox_QGNum.Text);

            ac.Add("YJPRICE", this.yTextBox_YJPRICE.Text);

            ac.Add("YJMONEY", this.yTextBox_YJMONEY.Text);

            ac.Add("REASON", this.yTextBox_REASON.Text);
            ac.Add("XYFX", this.yTextBox_XYFX.Text);
            ac.Add("PTTJ", this.yTextBox_PTTJ.Text);
            ac.Add("PLANDATE", this.ytDateTime_MakeTime.Value);
            
            ac.Add("STATUS", TvList.getValue(this.ytComboBox_Status).ToString());
            ac.Add("MEMO", this.yTextBox_Rec.Text);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", this.ytDateTime_RECDATE.Value);
            ac.Add("CHOSCODE", His.his.Choscode);


            if (!isAdd)
            {
                ac.Add("APPLYID", r["APPLYID"]);

            }
            else 
            {
                ac.Add("APPLYID", null);
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
           
            AskB.ReLoadData();
            if (!isAdd || !WJs.confirm("是否继续添加设备请购信息？"))
            {
                this.Close();
            }
            else
            {
                this.yTextBox_QGID.Text=null;
                this.selTextInpt_QGKSID.Value = null;
                this.selTextInpt_QGKSID.Text = null;

                this.selTextInpt_EQID.Value = null;
                this.selTextInpt_EQID.Text = null;

                this.yTextBox_Name.Clear();
                this.yTextBox_GuiG.Clear();
                this.yTextBox_XH.Clear();
                this.selTextInpt_Country.Text = "中国";
                this.selTextInpt_Country.Value = null;
                this.ytComboBox_Unitcode.Value=null;
                this.ytComboBox_Unitcode.Text = null;
                this.yTextBox_QGNum.Clear();

                this.yTextBox_YJPRICE.Clear();
                this.yTextBox_YJMONEY.Clear();

                this.yTextBox_REASON.Clear();
                this.yTextBox_PTTJ.Clear();
                this.yTextBox_XYFX.Clear();
                this.yTextBox_Rec.Clear();

             
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            isSc = false;//什么标志位
            this.Close();
        }

        private void button_CG_Click(object sender, EventArgs e)
        {
            if (this.yTextBox_SPNum.Text.Trim().Length <= 0)
            {
                WJs.alert("请输入审批数量！");
                yTextBox_SPNum.Focus();
                return;


            }
            else
            {
                if (!int.TryParse(this.yTextBox_SPNum.Text, out a) || a < 0)
                {
                    WJs.alert("审批数量必须是大于零的整数！");
                    yTextBox_SPNum.Focus();
                    return;
                }
                else if(a>int.Parse(this.yTextBox_QGNum.Text))
                {
                    WJs.alert("审批数量必须小于请购数量！");
                    yTextBox_SPNum.Focus();
                    return;
                }

            }

            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.EQPurchase.EQAskBuySvr";
            ac.Sql = "EQAskBuyInfo_SHCG";
            ac.Add("APPLYID", r["APPLYID"]);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("RECDATE", this.ytDateTime_RECDATE.Value);
            ac.Add("SHDATE", this.ytDateTime_SHTime.Value);
            ac.Add("SHUSERID", His.his.UserId);
            ac.Add("SHNUM", this.yTextBox_SPNum.Text);

            ac.Add("SHUSERNAME", His.his.UserName);
            ac.Add("SHDATE", this.ytDateTime_SHTime.Value);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_SH);
            ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            ac.Post();
        }

        private void button_SB_Click(object sender, EventArgs e)
        {

            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.EQPurchase.EQAskBuySvr";
            ac.Sql = "EQAskBuyInfo_SHSB";
            ac.Add("APPLYID", r["APPLYID"]);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("RECDATE", this.ytDateTime_RECDATE.Value);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad_SH);
            ac.ServiceFaiLoad += new YtClient.data.events.LoadFaiEventHandle(ac_ServiceFaiLoad);
            ac.Post();
        }

        void ac_ServiceLoad_SH(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
           
            AskB.ReLoadData();
            this.Close();
        }


 
    }
}
