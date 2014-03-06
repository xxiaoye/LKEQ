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

namespace ChSys
{
    public partial class WarrantInput : Form
    {
        public bool okflag = false;
        public string v_pass ;
        //public string tel;
        
        public WarrantInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!nametxt.Text.Equals("冯刚") && !nametxt.Text.Equals("施若"))
            {
                WJs.alert("授权人姓名录入错误。");
                return;
            }
            if (!teltxt.Text.Equals("13985000355") && !teltxt.Text.Equals("13985159896"))
            {
                WJs.alert("授权人电话录入错误。");
                return;
            }
            v_pass = passtxt.Text.Trim();
            if (v_pass.Equals(""))
            {
                WJs.alert("密码不允许为空。");
                return;
            }
            okflag = true;
            this.Close();
        }
 

        private void button2_Click(object sender, EventArgs e)
        { 
            this.Close();
        }
 
    }
}
