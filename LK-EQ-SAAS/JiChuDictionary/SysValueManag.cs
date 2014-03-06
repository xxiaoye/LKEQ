using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtClient;
using ChSys;
using YtUtil.tool;
using JiChuDictionary.form;
using YtWinContrl.com.datagrid;

namespace JiChuDictionary
{
    public partial class SysValueManag : Form, IPlug
    {
        DataTable mzd=null;
        public SysValueManag()
        {
            InitializeComponent();
    
        }
        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }
        private void init()
        {

        }
        public void initPlug(IAppContent app, object[] param)
        {
            
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        private void reLoad()
        {
            ActionLoad ld = ActionLoad.Conn();
            ld.Action = "Find";
            ld.Sql = "EQFindSysValue";
            ld.SetParams(new object[] { His.his.Choscode });
            
            ld.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld_ServiceLoadcSF);

            ld.Post();

        }
        private void SysValueManag_Load(object sender, EventArgs e)
        {
            TvList.newBind().add("是，多步维修", "1").add("否，单步维修", "0").Bind(this.ytComboBox_MoreStype);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_BuildCard);
            TvList.newBind().add("是", "1").add("否", "0").Bind(this.ytComboBox_OnlyOneCard);
            TvList.newBind().add("使用", "1").add("不使用", "0").Bind(this.ytComboBox_UseQian);
            TvList.newBind().add("启用", "1").add("不启用", "0").Bind(this.ytComboBox_AutoUse);
            TvList.newBind().add("允许", "1").add("不允许", "0").Bind(this.ytComboBox_AllowDropOut);
             reLoad();
            if(this.yTextBox_XSWS.Text.Trim().Length<=0 )
            {
                this.yTextBox_XSWS.Text = "2";
            }
         
           
        }

        private void ld_ServiceLoadcSF(object sender, YtClient.data.events.LoadEvent e)
        {
            mzd = e.Msg.GetDataTable();
            if (mzd != null)
            {
                if (mzd.Rows.Count >= 1)
                {
                    
                   
                    reSetValue();
                   
                }
            }
        }
        void reSetValue()
        {
            DataRow[] r = mzd.Select("id=2200");//
            if (r != null && r.Length == 1)
            {
                this.ytComboBox_MoreStype.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);
            }
            r = mzd.Select("id=2201");//
            if (r != null && r.Length == 1)
            {
                this.ytComboBox_BuildCard.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);
            }
            r = mzd.Select("id=2202");//
            if (r != null && r.Length == 1)
            {
                this.ytComboBox_OnlyOneCard.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);
            }
            r = mzd.Select("id=2203");//
            if (r != null && r.Length == 1)
            {
                this.yTextBox_XSWS.Text = Convert.ToInt32(r[0]["SYSVALUE"]).ToString();
            }
            r = mzd.Select("id=2204");//
            if (r != null && r.Length == 1)
            {

                this.ytComboBox_UseQian.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);

            }
            r = mzd.Select("id=2205");//
            if (r != null && r.Length == 1)
            {
                this.ytComboBox_AutoUse.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);
            }
            r = mzd.Select("id=2206");//
            if (r != null && r.Length == 1)
            {
                this.yTextBox_CardsLong.Text =(r[0]["SYSVALUE"]).ToString();
            }
            r = mzd.Select("id=2207");//
            if (r != null && r.Length == 1)
            {
                this.ytComboBox_AllowDropOut.SelectedIndex = Convert.ToInt32(r[0]["SYSVALUE"]);
            }
        
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
        }

        private void button1_Click(object sender, EventArgs e)  //修改
        {
            if (this.yTextBox_XSWS.Text.Trim().Length >= 1)
            { 
                int a;
                if (!int.TryParse(this.yTextBox_XSWS.Text, out a))
                {
                    WJs.alert("小数位数必须是大于或等于零的整数");
                    this.yTextBox_XSWS.Focus();
                    return;
                }
            }
            if (this.yTextBox_CardsLong.Text.Trim().Length >= 1)
            {
                int a;
                if (!int.TryParse(this.yTextBox_CardsLong.Text, out a))
                {
                    WJs.alert("卡号长度必须是大于或等于零的整数");
                    this.yTextBox_CardsLong.Focus();
                    return;
                }
            
            }


            ActionLoad ld = ActionLoad.Conn();

            ld.Action = "LKWZSVR.lkeq.JiChuDictionary.SysValue";
            ld.Sql = "Add";

         
            if (mzd==null || mzd.Rows.Count == 0)
            {

                ld.SetParams(new object[] { 2200, "设备维修是否采用多个步骤", this.ytComboBox_MoreStype.SelectedIndex.ToString(), "0：单步维修 1：多步维修 “报修→维修→交付”", His.his.Choscode, 
                2201, "是否设备领用时建卡", this.ytComboBox_BuildCard.SelectedIndex.ToString(), "0：否 1：是", His.his.Choscode, 
                2202, "是否一台设备建立一张卡片", this.ytComboBox_OnlyOneCard.SelectedIndex.ToString(), "0：否 1：是", His.his.Choscode, 
                2203, "小数位数",this.yTextBox_XSWS.Text, "设定在系统中显示单价、金额信息时，所使用的小数位数,默认是2", His.his.Choscode, 
                2204, "是否使用千分位符号",this.ytComboBox_UseQian.SelectedIndex, "0：不使用 1：使用", His.his.Choscode ,
                2205, "建卡时，是否自动启用设备",this.ytComboBox_AutoUse.SelectedIndex, "0：不启用 1：启用", His.his.Choscode ,
                2206, "设备卡的卡号长度", this.yTextBox_CardsLong.Text, "设置设备卡片的卡号长度，不含前缀字符的数字长度", His.his.Choscode ,
                2207, "是否允许负库存出库",this.ytComboBox_AllowDropOut.SelectedIndex, "0：不允许 1：允许", His.his.Choscode 
                });

                ld.Sql = "Add";
            }
            else
            {
                ld.SetParams(new object[] { this.ytComboBox_MoreStype.SelectedIndex.ToString(), His.his.Choscode, this.ytComboBox_BuildCard.SelectedIndex.ToString(), His.his.Choscode, this.ytComboBox_OnlyOneCard.SelectedIndex.ToString(), His.his.Choscode, this.yTextBox_XSWS.Text, His.his.Choscode, this.ytComboBox_UseQian.SelectedIndex, His.his.Choscode 
                , this.ytComboBox_AutoUse.SelectedIndex, His.his.Choscode, this.yTextBox_CardsLong.Text, His.his.Choscode, this.ytComboBox_AllowDropOut.SelectedIndex, His.his.Choscode
                });
                ld.Sql = "Update";
            }
    

            ld.ServiceLoad += new YtClient.data.events.LoadEventHandle(ld_ServiceLoad);

                ld.Post();
        }

        private void ld_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            WJs.alert(e.Msg.Msg);
            reLoad();
        }

        private void button2_Click(object sender, EventArgs e) //取消(还原)
        {
            reSetValue();
        }
        
    }
}
