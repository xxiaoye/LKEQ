using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtUtil.tool;
using ChSys;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;


namespace StatisticQuery
{
    public partial class EQCardQuery : Form, IPlug
    {
        public EQCardQuery()
        {
            InitializeComponent();
            this.dataGView2.Enabled = false;
            this.dataGView3.Enabled = false;
            foreach (Control item in CardCC_tab.Controls)
            {
                if (item is Label)
                {
                    continue;
                }
                item.Enabled = false;
            }
            foreach (Control item in CardExplain_tab.Controls)
            {
                if (item is Label)
                {
                    continue;
                }
                item.Enabled = false;
            }
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
        //  private Panel[] plis = null;

        #endregion

        Byte[] bytPicLoad;
        Dictionary<string, ObjItem> dr = null;
        void dateTimeDuan1_SelectChange(object sender, EventArgs e)
        {
            Search_button_Click(null, null);
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            if (this.selTextInpt_KS.Value == null)
            {
                WJs.alert("请选择科室");
                return;
            }

            SqlStr sql = new SqlStr();


            if (!this.selTextInpt_KS.Text.Equals(""))
            {
                sql.Add(" and (DEPTID= ?)", this.selTextInpt_KS.Value);
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.dateTimePicker1.Value, this.dateTimePicker2.Value }, sql);

            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";

        }



        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要查询的设备卡片记录！");
                return;
            }

            if (e.TabPage == CardFJ_tab)
            {
                this.dataGView2.ClearData();
                FJFormLoad(dr);

            }
            if (e.TabPage == CardJLRec_tab)
            {
                this.dataGView3.ClearData();
                JLFormLoad(dr);

            }
            if (e.TabPage == CardCC_tab)
            {
                foreach (Control item in CardCC_tab.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                CCFromLoad(dr);
            }

            if (e.TabPage == CardExplain_tab)
            {

                foreach (Control item in CardExplain_tab.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    pictureBox1.Image = null;
                }
                SMFormLoad(dr);
            }
        }

        private void EQCardQuery_Load(object sender, EventArgs e)
        {
            WJs.SetDictTimeOut();
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Column47);
            TvList.newBind().Load("FindSTATUNAME_EQCardQuery", new object[] { His.his.Choscode }).Bind(this.Column7);
            TvList.newBind().Load("FindKSName_EQCardQuery", new object[] { His.his.Choscode }).Bind(this.Column8);

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

            this.dataGView1.Url = "LoadCardInfo_EQCardQuery";
            this.selTextInpt_KS.Sql = "StatQuery_EQOutQueryKS";
            this.selTextInpt_KS.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            this.dateTimeDuan1.SelectChange += new EventHandler(dateTimeDuan1_SelectChange);
        }


        //附件表加载
        void FJFormLoad(Dictionary<string, ObjItem> dr)
        {
            dr = this.dataGView1.getRowData();
            this.dataGView2.Url = "LoadFJInfoInCardEdit";
            this.dataGView2.reLoad(new object[] { His.his.Choscode.ToString(), dr["卡片ID"].ToString() });
            this.label18.Text = this.dataGView2.RowCount.ToString() + "条";
            this.label14.Text = this.dataGView2.Sum("金额").ToString() + "元";
        }
        //计量表加载
        void JLFormLoad(Dictionary<string, ObjItem> dr)
        {
            dr = this.dataGView1.getRowData();
            this.dataGView3.Url = "LoadJLInfoInCardEdit";
            this.dataGView3.reLoad(new object[] { His.his.Choscode.ToString(), dr["卡片ID"].ToString() });
            this.label7.Text = this.dataGView3.RowCount.ToString() + "条";
          
        }
        //财产加载
        void CCFromLoad(Dictionary<string, ObjItem> dr)
        {
            dr = this.dataGView1.getRowData();
            DataTable dtCC = LData.LoadDataTable("LoadCCInfoInCardEdit", null, new object[] { His.his.Choscode.ToString(), dr["卡片ID"].ToString() });
            if (dtCC != null)
            {
                if (dtCC.Rows.Count <= 0)
                {
                    return;
                }
                foreach (DataRow row in dtCC.Rows)
                {
                    //12
                    CCcardid_textBox.Text = row["CARDID"].ToString();
                    CCprice_textBox.Text = row["PRICE"].ToString();
                    CCcc_textBox.Text = row["CCDATE"].ToString();
                    CCcsname_textBox.Text = row["CSNAME"].ToString();

                    DateTime result;
                    if (!DateTime.TryParse(row["BUYDATE"].ToString(), out result))
                    {
                        CCbuydate_textBox.Value = DateTime.Now;
                    }
                    else
                    {
                        CCbuydate_textBox.Value = result;
                    }
                    CCnewold_textBox.Text = row["NEWOLD"].ToString();
                    CCsupply_textBox.Text = row["SUPPLY"].ToString();
                    CCother_textBox.Text = row["OTHER"].ToString();
                    CCuseridtextBox.Text = row["USERID"].ToString();
                    CCusername_textBox.Text = row["USERNAME"].ToString();
                    CCrecdate_textBox.Text = row["RECDATE"].ToString();
                    CCchosode_textBox.Text = row["CHOSCODE"].ToString();
                }
            }
        }
        #region 说明界面加载
        void LoadPicFile(Dictionary<string, ObjItem> dr)
        {
            string strSQL = "SELECT PICFILE FROM LKEQ.EQCARDEXPLAINREC WHERE CHOSCODE=" + dr["医疗机构编码"].ToString() + " AND CARDID=" + dr["卡片ID"].ToString();
            string cnnstr = ConfigurationManager.AppSettings["LKEQ_Conn"].ToString();
            //string cnnstr = "Provider = OraOLEDB.Oracle.1; Data Source = orcl; User ID = HIS; Password =linker;Persist Security Info=True";
            //string cnnstr = System.Configuration.ConfigurationManager.ConnectionStrings["LKEQ_Conn"].ToString();
            OleDbConnection con = new OleDbConnection(cnnstr);
            try
            {
                con.Open();
            }
            catch
            { }
            OleDbCommand cmd = new OleDbCommand(strSQL, con);
            System.Data.OleDb.OleDbDataReader drSM = cmd.ExecuteReader();
            while (drSM.Read())
            {
                if (drSM["PICFILE"] != DBNull.Value)//照片字段里有值才能进到方法体显示图片，否则清空pb     
                {
                    MemoryStream ms = new MemoryStream((byte[])drSM["PICFILE"]);//把照片读到MemoryStream里   
                    bytPicLoad = (byte[])drSM["PICFILE"];
                    Image imageBlob = Image.FromStream(ms, true);//用流创建Image     
                    pictureBox1.Image = imageBlob;//输出图片     
                }
                else//照片字段里没值，清空pb     
                {
                    pictureBox1.Image = null;
                }
            }
        }

        //说明文档加载
        void SMFormLoad(Dictionary<string, ObjItem> dr)
        {
            //这里的图片是个什么意思，我不是很懂  单独加载  不能用LoadDataTable  ↑
            LoadPicFile(dr);
            DataTable dtSM = LData.LoadDataTable("LoadSMInfoInCardEdit", null, new object[] { dr["卡片ID"].ToString(), His.his.Choscode.ToString() });
            if (dtSM != null)
            {
                if (dtSM.Rows.Count <= 0)
                {
                    return;
                }
                foreach (DataRow row in dtSM.Rows)
                {
                    SMcardid_textBox.Text = row["CARDID"].ToString();
                    SMexplainNum_textBox.Text = row["EXPLAINNUM"].ToString();
                    SMteachnum_textBox.Text = row["TECHNUM"].ToString();
                    SMext_textBox.Text = row["EXT"].ToString();
                    SMboxthing_textBox.Text = row["BOXTHING"].ToString();
                    SMcertificate_textBox.Text = row["CERTIFICATE"].ToString();
                    SMother_textBox.Text = row["OTHER"].ToString();
                    SMusername_textBox.Text = row["USERNAME"].ToString();
                    SMusrtid_textBox.Text = row["USERID"].ToString();
                    SMchoscode_textBox.Text = row["CHOSCODE"].ToString();
                    dateTimePicker1.Text = row["RECDATE"].ToString();
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                WJs.alert("请选择需要下载图纸文件的的设备卡片！");
                return;
            }
            tabControl1.SelectTab(CardExplain_tab);
            if (pictureBox1.Image == null)
            {
                WJs.alert("该设备卡片下没有添加图纸，请进入设备建卡处进行添加！");
                return;

            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.pictureBox1.Image.Save(saveFileDialog1.FileName + this.SMext_textBox.Text);
                WJs.alert("下载图纸文件成功！");
            }
        }

        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
                  
            dr = this.dataGView1.getRowData();
            if (dr == null || dr.Count == 0)
            {
                return;
            }
            tabControl1.SelectTab(CardFJ_tab);
            FJFormLoad(dr);
        
        }
    }
}
