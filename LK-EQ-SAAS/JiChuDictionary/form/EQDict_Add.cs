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

namespace JiChuDictionary.form
{
    public partial class EQDict_Add : Form
    {
        public EQDict_Add()
        {
            InitializeComponent();
        }
        DataRow r;
        private bool isAdd;
        private bool isScan=false;
        public EQDict_Add(DataRow r, bool _isAdd)
        {
            isAdd = _isAdd;
            this.r = r;
            InitializeComponent();
        }
        public EQDict_Add(DataRow r, bool _isAdd,bool _isScan)
        {
            isScan = _isScan;
            this.r = r;
            InitializeComponent();
        }
        public bool isSc = false;
        public DictManag WZDM;
        void yTextBox_Name_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_Name.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_PY.Text = PyWbCode.getPyCode(n).ToLower();
                if (this.yTextBox_PY.Text.Trim().Length >10)
                {
                    this.yTextBox_PY.Text = this.yTextBox_PY.Text.Substring(0, 10);
                }
                this.yTextBox_WB.Text = PyWbCode.getWbCode(n).ToLower();
                if (this.yTextBox_WB.Text.Trim().Length > 10)
                {
                    this.yTextBox_WB.Text = this.yTextBox_WB.Text.Substring(0, 10);
                }
              
            }
        }
        void yTextBox_JC_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_JC.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_JM.Text = PyWbCode.getPyCode(n).ToUpper();
              
            }
        }
        void yTextBox_BM_TextChanged(object sender, EventArgs e)
        {
            string n = this.yTextBox_BM.Text.Trim();
            if (n.Length > 0)
            {
                this.yTextBox_BMJM.Text = PyWbCode.getPyCode(n).ToUpper();

            }
        }
        private void WZDict_Add_Load(object sender, EventArgs e)
        {
            this.yTextBox_Name.TextChanged += new EventHandler(yTextBox_Name_TextChanged);
            this.yTextBox_JC.TextChanged += new EventHandler(yTextBox_JC_TextChanged);
            this.yTextBox_BM.TextChanged += new EventHandler(yTextBox_BM_TextChanged);

            TvList.newBind().add("启用", "1").add("停用", "0").Bind(this.ytComboBox_ifUse);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_IFJL);
            TvList.newBind().add("平均年限法", "1").add("工作量法", "2").add("年折旧率", "3").add("手工折旧", "4").Bind(this.ytComboBox_ZJTYPE);
            TvList.newBind().Load("EQDict_KindCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_KindCode);
            TvList.newBind().Load("EQDict_CountCode", new object[] { His.his.Choscode }).Bind(this.ytComboBox_CountCode); //这里修改了，查询所有
            TvList.newBind().Load("EQDict_SingerCode", null).Bind(this.ytComboBox_SingerCode);
            TvList.newBind().Load("EQDict_WORKUNITCODE", null).Bind(this.ytComboBox_WORKUNITCODE);
          
            
            //这里更改了单位编码
            //TvList.newBind().SetCacheKey("sfxmbm").Load("WZDict_SFXMBM",new object[]{ "010705"}).Bind(this.ytComboBox_SFXMBM);
            this.selTextInpt_PRODUCTPLACE.SelParam = "{key}|{key}|{key}|" + His.his.Choscode;//为什么要这样写？
            selTextInpt_PRODUCTPLACE.BxSr = false;//必须输入查询关键字
            //this.selTextInpt1.textBox1.ReadOnly = true;
            this.selTextInpt_PRODUCTPLACE.Sql = "EQDict_PRODUCTPLAC";
          
            this.yTextBox_UserID.ReadOnly = true;
            this.yTextBox_User.ReadOnly = true;
            this.yTextBox_EQID.ReadOnly = true;
            this.yTextBox_Choscode.ReadOnly = true;
            this.ytDateTime_RECDATE.Enabled = false;
            this.ytComboBox_ifUse.SelectedIndex = 0;
            if (this.isScan)
            {
                this.btn_Save.Enabled = false;
            
            }
            if (!this.isAdd)
            {
                
                
               
                this.yTextBox_EQID.Text = r["EQID"].ToString();
                this.yTextBox_Name.Text = r["EQNAME"].ToString();
                this.ytComboBox_KindCode.Value = r["KINDCODE"].ToString();
                this.ytComboBox_CountCode.Value = r["COUNTCODE"].ToString();
                this.ytComboBox_SingerCode.Value = r["UNITCODE"].ToString();


                this.ytComboBox_WORKUNITCODE.Value = r["WORKUNITCODE"].ToString();
                this.ytComboBox_ZJTYPE.Value = r["ZJTYPE"].ToString();
                this.yTextBox_ZJRATE.Text = r["ZJRATE"].ToString();
                this.ytComboBox_IFJL.Value = r["IFJL"].ToString();
                this.ytComboBox_ifUse.Value = r["IFUSE"].ToString();
                this.yTextBox_UserID.Text = r["USERID"].ToString();
                this.yTextBox_User.Text = r["USERNAME"].ToString();
                this.selTextInpt_PRODUCTPLACE.textBox1.Text = r["PRODUCTPLACE"].ToString();
               
                this.yTextBox_Choscode.Text = r["CHOSCODE"].ToString();
                this.ytDateTime_RECDATE.Value = Convert.ToDateTime(r["RECDATE"].ToString());
                if (r["PYCODE"] != null)
                {
                    this.yTextBox_PY.Text = r["PYCODE"].ToString();
                }
                if (r["WBCODE"] != null)
                {
                    this.yTextBox_WB.Text = r["WBCODE"].ToString();
                } if (r["SHORTNAME"] != null)
                {
                    this.yTextBox_JC.Text = r["SHORTNAME"].ToString();
                }
                if (r["SHORTCODE"] != null)
                {
                    this.yTextBox_JM.Text = r["SHORTCODE"].ToString();
                } if (r["ALIASNAME"] != null)
                {
                    this.yTextBox_BM.Text = r["ALIASNAME"].ToString();
                }
                if (r["ALIASCODE"] != null)
                {
                    this.yTextBox_BMJM.Text = r["ALIASCODE"].ToString();
                }
                if (r["GG"] != null)
                {
                    this.yTextBox_GuiG.Text = r["GG"].ToString();
                } if (r["XH"] != null)
                {
                    this.yTextBox_XH.Text = r["XH"].ToString();
                }
                if (r["TXM"] != null)
                {
                    this.yTextBox_TiaoXM.Text = r["TXM"].ToString();
                } if (r["MEMO"] != null)
                {
                    this.yTextBox_Rec.Text = r["MEMO"].ToString();
                }
                
                //ControlUtil.SetSelectRadioValue(this.groupBox2, r["WZID"]);

            }
            else
            {
                this.ytComboBox_KindCode.Value = r["KINDCODE"].ToString();
                this.yTextBox_User.Text = His.his.UserName;
               
                this.yTextBox_UserID.Text = His.his.UserId.ToString();
              
                this.yTextBox_Choscode.Text = His.his.Choscode;
        
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.yTextBox_Name.Text.Trim().Length == 0)
            {
                WJs.alert("请输入设备名称！");
                this.yTextBox_Name.Focus();
                return;
            }
            if (this.ytComboBox_KindCode.SelectedIndex == -1)
            {
                WJs.alert("请选择所属类别！");
                ytComboBox_KindCode.Focus();
                return;
            }
            if (this.ytComboBox_SingerCode.SelectedIndex == -1)
            {
                WJs.alert("请选择单位编码！");
                ytComboBox_SingerCode.Focus();
                return;
            }
            if (this.ytComboBox_IFJL.SelectedIndex == -1)
            {
                WJs.alert("请选择是否计量设备！");
                ytComboBox_IFJL.Focus();
                return;
            }
            if (this.ytComboBox_ifUse.SelectedIndex == -1)
            {
                WJs.alert("请选择是否使用！");
                ytComboBox_ifUse.Focus();
                return;
            }
            if (this.ytComboBox_ZJTYPE.SelectedIndex == -1)
            {
                WJs.alert("请选择折旧方式！");
                ytComboBox_SingerCode.Focus();
                return;
            }
            //if (this.yTextBox_ZJRATE.Text.Trim().Length == 0)
            //{
            //    WJs.alert("请输入折旧百分比！");
            //    this.yTextBox_ZJRATE.Focus();
            //    return;
            //}
            if (this.ytComboBox_ZJTYPE.SelectedIndex == 1)
            {
                if (this.ytComboBox_WORKUNITCODE.SelectedIndex == -1)
                {
                    WJs.alert("折旧方式为工作量法时必须输入工作量单位编码！");
                    this.ytComboBox_WORKUNITCODE.Focus();
                    return;
                }
            }

            if (this.yTextBox_PY.Text.Trim().Length > 10) //保证字符串长度不超过10
            {
                this.yTextBox_PY.Text = this.yTextBox_PY.Text.Substring(0, 10);
            }

            if (this.yTextBox_WB.Text.Trim().Length > 10)//保证字符串长度不超过10
            {
                this.yTextBox_WB.Text = this.yTextBox_WB.Text.Substring(0, 10);
            }
            ActionLoad ac = ActionLoad.Conn();

            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQDictSvr";
            ac.Sql = "SaveDictEQInfo";
            ac.Add("EQNAME", this.yTextBox_Name.Text);
            ac.Add("PYCODE", this.yTextBox_PY.Text);
            ac.Add("WBCODE", this.yTextBox_WB.Text);
            ac.Add("SHORTNAME", this.yTextBox_JC.Text);
            ac.Add("SHORTCODE", this.yTextBox_JM.Text);
            ac.Add("ALIASNAME", this.yTextBox_BM.Text);
            ac.Add("ALIASCODE", this.yTextBox_BMJM.Text);
            ac.Add("KINDCODE", TvList.getValue(this.ytComboBox_KindCode).ToString());
            ac.Add("COUNTCODE", TvList.getValue(this.ytComboBox_CountCode).ToString());
            if (this.selTextInpt_PRODUCTPLACE.Value != null)
            {
                ac.Add("PRODUCTPLACE", this.selTextInpt_PRODUCTPLACE.Text);
            }
            else
            {
                ac.Add("PRODUCTPLACE", this.selTextInpt_PRODUCTPLACE.textBox1.Text);
            }

            ac.Add("GG", this.yTextBox_GuiG.Text);
            ac.Add("XH", this.yTextBox_XH.Text);
            ac.Add("UNITCODE", TvList.getValue(this.ytComboBox_SingerCode).ToString());
            ac.Add("WORKUNITCODE", TvList.getValue(this.ytComboBox_WORKUNITCODE).ToString());
            ac.Add("IFJL", TvList.getValue(this.ytComboBox_IFJL).ToString());
            ac.Add("ZJTYPE", TvList.getValue(this.ytComboBox_ZJTYPE).ToString());
            ac.Add("ZJRATE", this.yTextBox_ZJRATE.Text);
            ac.Add("TXM", this.yTextBox_TiaoXM.Text);
            ac.Add("IFUSE", TvList.getValue(this.ytComboBox_ifUse).ToInt());
            ac.Add("MEMO", this.yTextBox_Rec.Text);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);


            if (!isAdd)
            {
                ac.Add("EQID", r["EQID"]);

            }
            else 
            {
                ac.Add("EQID",null);
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

         
            if (!isAdd || !WJs.confirm("是否继续添加物资信息？"))
            {
                WZDM.ReLoadData(TvList.getValue(this.ytComboBox_KindCode).ToString());
                this.Close();
            }
            else
            {
                this.ytComboBox_CountCode.SelectedIndex = -1;
                this.ytComboBox_ifUse.SelectedIndex = 0;
                this.ytComboBox_WORKUNITCODE.SelectedIndex = -1;
                this.selTextInpt_PRODUCTPLACE.Value = null;
                this.selTextInpt_PRODUCTPLACE.Text = null;
                this.ytComboBox_SingerCode.SelectedIndex = -1;
                this.ytComboBox_IFJL.SelectedIndex = -1;
                this.ytComboBox_ZJTYPE.SelectedIndex = -1;
                this.yTextBox_BM.Clear();
                this.yTextBox_BMJM.Clear();
                this.yTextBox_GuiG.Clear();
                this.yTextBox_ZJRATE.Clear();
                this.yTextBox_XH.Clear();
                this.yTextBox_JC.Clear();
                this.yTextBox_JM.Clear();
                this.yTextBox_Name.Clear();
                this.yTextBox_PY.Clear();
                this.yTextBox_Rec.Clear();
                this.yTextBox_TiaoXM.Clear();
                this.yTextBox_WB.Clear();
                this.yTextBox_EQID.Clear();
             
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            isSc = false;//什么标志位
            this.Close();
        }

    }
}
