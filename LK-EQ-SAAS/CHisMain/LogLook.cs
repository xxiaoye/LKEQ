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
    public partial class LogLook : Form
    {
        public LogLook()
        {
            InitializeComponent();
        }

        private void LogLook_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Log.get();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
 
    }
}
