using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using YtClient;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtWinContrl.com;
using ChSys;

namespace JiChuDictionary.form
{
    public partial class AddEQ : Form
    {
        //单例模式:?
        //public EQWareManager Main;
        //private DataGView dataGViewPL;
        Dictionary<string, ObjItem> dr;
        int isFlag;

        //增加还是修改设备信息
        public bool isOK = false;
        string wd;//库房编码
        string ms;//Msg

        //新增设备库房业务
        public AddEQ()
        {
            InitializeComponent();
        }
        //修改设备库房业务
        public AddEQ(Dictionary<string, ObjItem> dr, int isFlag)
        {
            this.dr = dr;
            this.isFlag = isFlag;
            InitializeComponent();
        }

        private void AddEQ_Load(object sender, EventArgs e)
        {
            ControlUtil.RegKeyEnter(this);
            this.selTextInpt1.Sql = "FindEQKSID";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            this.choscode_yTextBox1.Text = His.his.Choscode.ToString();
            this.userid_yTextBox.Text = His.his.UserId.ToString();
            this.username_yTextBox.Text = His.his.UserName;
            TvList.newBind().add("启用", "1").add("停用", "0").Bind(ifuse_ytComboBox);//这里的1与0是对应的键值对，下标对应为0,1
            TvList.newBind().add("是", "1").add("否", "0").Bind(ifall_ytComboBox);

            warename_yTextBox1.TextChanged += new EventHandler(warename_yTextBox1_TextChanged);

            if (isFlag == 2)
            {
                //根据初始化构造函数，判断是否为添加或修改
                this.ifuse_ytComboBox.SelectedIndex = 0;
                this.ifall_ytComboBox.SelectedIndex = 0;
            }
            if (isFlag == 1 || isFlag == 0)
            {
                LoadInfoInEQWare();
                if (isFlag == 0)
                {
                    groupBox1.Enabled = false;
                    btnSave.Enabled = false;
                }
                ifuse_ytComboBox.Enabled = false;
            }
        }

        void warename_yTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.warename_yTextBox1.Text.Trim().Length > 0)
            {
                pycode_yTextBox.Text = PyWbCode.getPyCode(this.warename_yTextBox1.Text.Trim()).ToLower();
                wbcode_yTextBox.Text = PyWbCode.getWbCode(this.warename_yTextBox1.Text.Trim()).ToLower();
            }
        }
        private void LoadInfoInEQWare()
        {
            this.warecode_yTextBox1.Text = dr["库房编码"].ToString();
            this.warename_yTextBox1.Text = dr["库房名称"].ToString();

            string depid = LData.Es("EQWare_GetDepID", null, new object[] { dr["医疗机构编码"].ToString(), dr["库房编码"].ToString() });
            this.selTextInpt1.Text = dr["科室"].ToString();
            this.selTextInpt1.Value = depid;

            if (dr["是否使用"].ToString() == "1")
            {
                this.ifuse_ytComboBox.SelectedIndex = 0;
            }
            else
            {
                this.ifuse_ytComboBox.SelectedIndex = 1;
            }
            if (dr["IFALL"].ToString() == "1")
            {
                this.ifall_ytComboBox.SelectedIndex = 0;
            }
            else
            {
                this.ifall_ytComboBox.SelectedIndex = 1;
            }

            this.memo_yTextBox.Text = dr["备注"].ToString();
            this.wbcode_yTextBox.Text = dr["拼音码"].ToString();
            this.pycode_yTextBox.Text = dr["拼音码"].ToString();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (this.warename_yTextBox1.Text.Trim().Length == 0)
            {
                WJs.alert("请输入库房名称！");
                warename_yTextBox1.Focus();
                return;
            }
            if (this.selTextInpt1.Value == "" || selTextInpt1.Text == "")
            {
                WJs.alert("请输入科室！");
                selTextInpt1.Focus();
                return;
            }
            if (this.ifall_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择是否管理所有设备类别！");
                ifall_ytComboBox.Focus();
                return;
            }
            if (this.ifuse_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请设置设备是否使用！");
                ifuse_ytComboBox.Focus();
                return;
            }

            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.JiChuDictionary.EQWare";

            //执行保存操作
            ac.Sql = "Save";
            AddEQWareInfoForSave(ac);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOK)
            {
                // if (!this.isAdd)
                if (isFlag == 1)
                {
                    wd = dr["库房编码"].ToString();
                }
                //是否管理所有设备类别
                if (this.ifall_ytComboBox.SelectedItem.ToString() == "否")
                {
                    //wd is kindcode
                    //SetEQDetail form = new SetEQDetail(null, dr, wd);
                    //form.Main = this.Main;

                    SetEQDetail form = new SetEQDetail(dr, wd);
                    form.ShowDialog();
                }
                if (isFlag == 1)
                {
                    WJs.alert("修改成功！");
                    this.Close();
                }
                if (isFlag == 2)
                {
                    if (!WJs.confirm("保存成功，是否继续添加？"))
                    {
                        this.Close();
                    }
                    else
                    {
                        InitForm();
                    }
                }
                else
                {
                    this.Close();//修改完之后关闭
                }
            }
            else
            {
                WJs.alert("保存设备库房信息失败！");
            }
        }


        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("已经存在该库房信息，不能修改成该名称！") || e.Msg.Msg.Equals("已经存在该库房信息！"))
            {
                isOK = false;
                WJs.alert(e.Msg.Msg);
            }
            else
            {
                isOK = true;
                wd = e.Msg.Msg.ToString().Split(',')[0];//类别编码
                ms = e.Msg.Msg.ToString().Split(',')[1];//返回信息
            }
        }

        private void AddEQWareInfoForSave(ActionLoad ac)
        {
            ac.Add("choscode", this.choscode_yTextBox1.Text);
            ac.Add("warename", this.warename_yTextBox1.Text);
            ac.Add("deptid", this.selTextInpt1.Value);
            ac.Add("pycode", this.pycode_yTextBox.Text);
            ac.Add("wbcode", this.wbcode_yTextBox.Text);

            if (this.ifuse_ytComboBox.SelectedIndex > -1)
            {
                if (this.ifuse_ytComboBox.SelectedItem.ToString() == "启用")
                {
                    ac.Add("ifuse", 1);
                }
                else
                {
                    ac.Add("ifuse", 0);
                }
            }
            if (this.ifall_ytComboBox.SelectedIndex > -1)
            {
                if (this.ifall_ytComboBox.SelectedItem.ToString() == "是")
                {
                    ac.Add("ifall", 1);
                }
                else
                {
                    ac.Add("ifall", 0);
                }
            }

            ac.Add("userid", this.userid_yTextBox.Text);
            ac.Add("username", this.username_yTextBox.Text);
            ac.Add("recdate", this.dateTimePicker1.Value);
            ac.Add("memo", this.memo_yTextBox.Text);
            if (isFlag == 1)
            {
                //修改时需要自己获取库房编码，否则自动生成
                ac.Add("warecode", dr["库房编码"].ToString());
                ac.SetKeyValue("warecode,choscode");
            }
        }
        private void InitForm()
        {
            this.ifall_ytComboBox.SelectedItem = "";
            this.ifuse_ytComboBox.SelectedItem = "";

            this.warecode_yTextBox1.Text = "";
            this.warename_yTextBox1.Text = "";
            this.pycode_yTextBox.Text = "";
            this.wbcode_yTextBox.Text = "";
            this.memo_yTextBox.Text = "";

            this.selTextInpt1.Text = "";
            this.selTextInpt1.Value = "";

            this.ifuse_ytComboBox.SelectedIndex = 0;
            this.ifall_ytComboBox.SelectedIndex = 0;
            this.warename_yTextBox1.Focus();
        }


    }
}
