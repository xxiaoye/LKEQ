using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using ChSys;
using YtClient;

namespace JiChuDictionary.form
{
    public partial class AddEQInOut : Form
    {
        Dictionary<string, ObjItem> dr;
        bool isAdd;
        bool isOk;

        public AddEQInOut()
        {
            InitializeComponent();
        }

        public AddEQInOut(Dictionary<string, ObjItem> dr, bool isAdd)
        {
            InitializeComponent();
            this.dr = dr;
            this.isAdd = isAdd;
        }

        private void AddEQInOut_Load(object sender, EventArgs e)
        {
            //
            TvList.newBind().add("停用", "0").add("启用", "1").Bind(this.ifuse_ytComboBox);
            TvList.newBind().add("入库", "0").add("出库", "1").Bind(this.ioflag_ytComboBox);
            TvList.newBind().add("否", "0").add("是", "1").Bind(this.ifdefault_ytComboBox);
            TvList.newBind().add("不包含", "0").add("包含", "1").Bind(this.recipemonth_ytComboBox);
            TvList.newBind().add("不包含", "0").add("包含", "1").Bind(this.recipeyear_ytComboBox);
            TvList.newBind().add("普通", "0").add("调拨", "1").add("申领", "2").add("盘点", "3").Bind(this.opflag_ytComboBox);
            //this.dateTimePicker1.Value = DateTime.Now;
            this.ioname_textBox.TextChanged += new EventHandler(ioname_textBox_TextChanged);
            initForm();
        }

        private void initForm()
        {
            this.userid_textBox.Text = His.his.UserId.ToString();
            this.username_textBox.Text = His.his.UserName;
            this.choscode_textBox.Text = His.his.Choscode;
            this.dateTimePicker1.Value = DateTime.Now;

            if (!isAdd)
            {
                //若为修改的话，是否使用不能修改
                this.ioid_textBox.Text = dr["入出ID"].ToString().Trim();
                this.ioname_textBox.Text = dr["入出名称"].ToString().Trim();
                this.pycode_textBox.Text = dr["拼音码"].ToString().Trim();
                this.wbcode_textBox.Text = dr["五笔码"].ToString().Trim();
                this.recipecode_textBox.Text = dr["单据前缀"].ToString().Trim();
                this.recipelength_textBox.Text = dr["单据编码长度"].ToString();
                this.recipemonth_ytComboBox.SelectedIndex = dr["单据月份"].ToInt();
                this.recipeyear_ytComboBox.SelectedIndex = dr["单据年份"].ToInt();
                this.ioflag_ytComboBox.SelectedIndex = dr["入出标志"].ToInt();
                this.opflag_ytComboBox.SelectedIndex = dr["操作标志"].ToInt();
                this.ifdefault_ytComboBox.SelectedIndex = dr["是否默认值"].ToInt();
                this.ifuse_ytComboBox.SelectedIndex = dr["是否使用"].ToInt();
                this.memo_textBox.Text = dr["备注"].ToString();

                ifuse_ytComboBox.Enabled = false;
            }
            else
            {
                //初始化一些信息
                this.ioname_textBox.Text = "";
                this.pycode_textBox.Text = "";
                this.wbcode_textBox.Text = "";
                this.recipecode_textBox.Text = "";
                this.recipelength_textBox.Text = "10";
                this.recipemonth_ytComboBox.SelectedIndex = 1;
                this.recipeyear_ytComboBox.SelectedIndex = 1;
                this.ioflag_ytComboBox.SelectedIndex = 0;
                this.opflag_ytComboBox.SelectedIndex = 0;
                this.ifdefault_ytComboBox.SelectedIndex = 0;
                this.ifuse_ytComboBox.SelectedIndex = 1;
                this.memo_textBox.Text = "";
            }
        }


        void ioname_textBox_TextChanged(object sender, EventArgs e)
        {
            this.wbcode_textBox.Text = PyWbCode.getWbCode(this.ioname_textBox.Text).ToLower();
            this.pycode_textBox.Text = PyWbCode.getPyCode(this.ioname_textBox.Text).ToLower();
        }


        #region 保存  -- 针对新增和修改
        /// <summary>
        /// 保存  -- 针对新增和修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ioname_textBox.Text.Trim().Length == 0)
            {
                WJs.alert("请填写入出名称！");
                return;
            }
            if (this.recipecode_textBox.Text.Trim().Length == 0)
            {
                WJs.alert("请填写单据前缀！");
                return;
            }
            if (this.recipeyear_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择单据年份！");
                return;
            }
            if (this.recipemonth_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择单据月份！");
                return;
            }
            if (this.recipelength_textBox.Text.Trim().Length == 0 || Convert.ToInt32(this.recipelength_textBox.Text.Trim()) <= 0)
            {
                WJs.alert("请填写单据编码长度，注意其长度必须大于0！");
                return;
            }

            if (this.ioflag_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择入出标志！");
                return;
            }
            if (this.opflag_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择操作标志！");
                return;
            }
            if (this.ifuse_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否使用！");
                return;
            }
            if (this.ifdefault_ytComboBox.SelectedIndex < 0)
            {
                //这里需要做一个数据库的校验
                WJs.alert("请选择是否默认值！");
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQInOutSvr";
            //17个
            ac.Add("IONAME", this.ioname_textBox.Text.Trim());
            ac.Add("PYCODE", this.pycode_textBox.Text.Trim());
            ac.Add("WBCODE", this.wbcode_textBox.Text.Trim());
            ac.Add("IFUSE", this.ifuse_ytComboBox.SelectedIndex.ToString());
            ac.Add("RECIPECODE", this.recipecode_textBox.Text.Trim());
            ac.Add("RECIPELENGTH", this.recipelength_textBox.Text.Trim());
            ac.Add("RECIPEYEAR", this.recipeyear_ytComboBox.SelectedIndex.ToString());
            ac.Add("RECIPEMONTH", this.recipemonth_ytComboBox.SelectedIndex.ToString());
            ac.Add("MEMO", this.memo_textBox.Text.Trim());
            ac.Add("IOFLAG", this.ioflag_ytComboBox.SelectedIndex.ToString());//ioflag_ytComboBox.SelectedValue 这样其实才是正规的
            ac.Add("OPFLAG", this.opflag_ytComboBox.SelectedIndex.ToString());
            ac.Add("IFDEFAULT", this.ifdefault_ytComboBox.SelectedIndex.ToString());
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("RECDATE", this.dateTimePicker1.Value);
            if (!isAdd)
            {
                ac.Add("IOID", this.ioid_textBox.Text.Trim());
                ac.Sql = "Modify";
            }
            else
            {
                ac.Sql = "Add";
            }
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();

            if (isOk)
            {
                if (isAdd)
                {
                    if (WJs.confirmFb("新增设备入出库记录成功，是否继续添加？"))
                    {
                        initForm();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    WJs.alert("保存设备入出库记录成功！");
                    this.Close();
                }
            }

        } 
        #endregion

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("修改该条设备入出库记录成功！") || (e.Msg.Msg.Equals("新增设备入出库记录成功！")))
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
