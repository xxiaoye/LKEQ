using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.log;
using System.Text.RegularExpressions;
using YtUtil.tool;
using YtClient;

namespace CHisMain
{
    public partial class InfoFrm : Form
    {
        private string Info;
        public InfoFrm(string I_info)
        {
            InitializeComponent();
            this.Info = I_info;
        }

        private void LogLook_Load(object sender, EventArgs e)
        {
            this.InfoTxt.Text = "\r\n\r\n" + Info + 
                    "\r\n\r\n\r\n\r\n                                        联科软件客户服务中心"
                               + "\r\n                                        热线电话：4006613506";
            if (!button1.Focused)
                button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
    }
}
