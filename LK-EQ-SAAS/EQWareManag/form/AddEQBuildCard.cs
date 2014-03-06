using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using ChSys;
using YtWinContrl.com.datagrid;
using YtClient;
using YtUtil.tool;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace EQWareManag.form
{
    public partial class AddEQBuildCard : Form
    {
        int isFlag;//0为浏览  1为编辑  2 为新增   10为审核建卡过程

        int Num = 0; //行数提示是否输入
        Dictionary<string, ObjItem> dr;//最重要的主表


        Dictionary<string, ObjItem> drZZ;
        Dictionary<string, ObjItem> drXX;//关于领用界面传过来的

        int IfOneToOne = 1;//默认一张卡片管理相同科室的1个相同设备
        int XiaoShuWei = 2;//为2
        int QianFenWei = 0;//为不使用千分位
        int IfAutoStar = 0;//是否自动启用
        int EqNumDefault = 1;//默认最小设备数量为1

        int NumPrev = 0;

        int IsUpdateFJ;
        int IsUpdateJL;

        Byte[] bytPic;
        Byte[] bytPicLoad;

        bool isOK;
        DateTime dinggou_dateTimePicker11;
        DateTime CCSJdateTimePicker11;

        DateTime LYSJ_dateTimePicker11;
        DateTime AZSJ_dateTimePicker11;

        DateTime CCbuydate_textBox11;
        DateTime BFRQ_dateTimePicker11;
        //SELECT SYSVALUE FROM HIS.系统参数 WHERE ID=?
        private int GetSysDanWei(int Id)
        {
            return Convert.ToInt32(LData.Es("GetSysDanWeiInEQDiaoBoEdit", null, new object[] { Id }));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public AddEQBuildCard()
        {
            InitializeComponent();
        }
        //这个是专为领用准备
        public AddEQBuildCard(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
            this.drZZ = drZ;
            this.drXX = drX;
            this.isFlag = isFlag;
        }


        public AddEQBuildCard(Dictionary<string, ObjItem> dr, int isFlag)
        {
            InitializeComponent();
            this.dr = dr;
            this.isFlag = isFlag;
        }

        #region 设备ID&NAME 单位编码的绑定
        void EQNameBind()
        {
            this.eqname_ytComboBox.Items.Clear();
            DataTable dt = LData.LoadDataTable("EQNameBindInCardEdit", null, new object[] { His.his.Choscode });
            if (dt != null)
            {
                if (dt.Rows.Count <= 0)
                {
                    return;
                }
                eqname_ytComboBox.Items.Clear();
                TvList tv = TvList.newBind();
                foreach (DataRow drow in dt.Rows)//EQNAME EQID
                {
                    tv.add(drow[0].ToString(), drow[1].ToString());
                }
                tv.Bind(eqname_ytComboBox);
            }
        }
        //单位编码[复制]
        void UnitCodeBind()
        {
            ActionLoad acUnitcode = ActionLoad.Conn();
            acUnitcode.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acUnitcode.Sql = "BindUnitCode";
            acUnitcode.ServiceLoad += new YtClient.data.events.LoadEventHandle(acUnitcode_ServiceLoad);
            acUnitcode.Post();
        }
        void acUnitcode_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            DataTable EQUnitInfo = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.FJ_unitColumn).Items.Clear();
            if (EQUnitInfo != null)
            {
                foreach (DataRow r in EQUnitInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
                tv.Bind(this.FJ_unitColumn);
            }
        }

        #endregion

        //附件表加载
        void FJFormLoad()
        {
            this.dataGView1.Url = "LoadFJInfoInCardEdit";
            this.dataGView1.reLoad(new object[] { His.his.Choscode.ToString(), dr["卡片ID"].ToString() });
        }
        //计量表加载
        void JLFormLoad()
        {
            this.dataGView2.Url = "LoadJLInfoInCardEdit";
            this.dataGView2.reLoad(new object[] { His.his.Choscode.ToString(), dr["卡片ID"].ToString() });
        }
        //财产加载
        void CCFromLoad()
        {
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
        void LoadPicFile()
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
        void SMFormLoad()
        {
            //这里的图片是个什么意思，我不是很懂  单独加载  不能用LoadDataTable  ↑
            LoadPicFile();
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

        #region 浏览时加载主界面两个部分的数据（2个方法）
        //必填项
        private void ZBiTianXiangLoad()
        {
            //21
            this.cardcode_txtbox.Text = dr["卡号"].ToString();
            this.cardId_txtbox.Text = dr["卡片ID"].ToString();
            this.stockid_txtbox.Text = dr["库存ID"].ToString();
            this.eqId_txtbox.Text = dr["设备ID"].ToString();
            this.eqnum_txtbox.Text = dr["设备数量"].ToString();


            this.eqname_ytComboBox.Value = dr["设备名称"].ToString();
            this.Status_ytComboBox.Value = dr["状态"].ToString();


            this.statuscode_selText.Value = dr["使用状态"].ToString();
            this.statuscode_selText.Text = dr["使用状态_Text"].ToString();

            this.stockflowno_ytComboBox.Value = dr["库存流水号"].ToString();
            this.stockflowno_ytComboBox.Text = dr["库存流水号"].ToString();

            this.Deptid_selText.Value = dr["使用科室ID"].ToString();
            this.Deptid_selText.Text = dr["使用科室ID_Text"].ToString();

            this.userid_txtbox.Text = dr["操作员ID"].ToString();
            this.username_txtbox.Text = dr["操作员姓名"].ToString();
            this.choscode_txtbox.Text = dr["医疗机构编码"].ToString();
            this.recdate_txtbox.Text = dr["制卡时间"].ToString();

            this.totalwork_txtbox.Text = dr["总工作量"].ToString();
            this.totaledwork_txtbox.Text = dr["累计工作量"].ToString();

            this.fdvalue_txtbox.Text = dr["分度值"].ToString();
            this.jdlevel_txtbox.Text = dr["精度等级"].ToString();
            this.checklevel_txtbox.Text = dr["检定等级"].ToString();
            this.checkrange_txtbox.Text = dr["测量范围"].ToString();
            if (dr["检定周期"].IsNull == true)
            {
                this.checkZQ_txtbox.Text = "";
            }
            else
            {
                this.checkZQ_txtbox.Text = (dr["检定周期"].ToDouble() / 12).ToString();
            }



            DataTable KeyValueTable = LData.LoadDataTable("FindEQNAMEInCard", null, new object[] { His.his.Choscode, dr["设备ID"].ToString() });

            //SELECT EQNAME,IFJL,ZJTYPE FROM LKEQ.DICTEQ WHERE CHOSCODE=? AND EQID=?

            if (KeyValueTable.Rows[0]["IFJL"].ToString() != "1")
            {
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = true;

            }
            if (KeyValueTable.Rows[0]["ZJTYPE"].ToString() != "2")
            {
                groupBox3.Enabled = false;
            }
            else
            {
                groupBox3.Enabled = true;
            }
        }
        //选填项 25 启用人  启用日期没加
        private void ZXuanTianXiangLoad()
        {
            DateTime result;
            this.guobie_textBox.Text = dr["国别"].ToString();
            this.txm_textBox.Text = dr["条形码"].ToString();
            this.hetonghao_textBox.Text = dr["合同号"].ToString();
            this.jiage_textBox.Text = dr["价格"].ToString();
            this.yuanzhi_textBox.Text = dr["原值"].ToString();
            this.memo_textBox.Text = dr["备注"].ToString();
            this.shoufeibiaozhun_textBox.Text = dr["收费标准"].ToString();
            this.leijizhejiu_textBox.Text = dr["累计折旧"].ToString();
            this.canzhilv_textBox.Text = dr["残值率"].ToString();
            this.zhuyaoyongtu_textBox.Text = dr["主要用途"].ToString();
            this.zhiliangqingkuang_textBox.Text = dr["质量情况"].ToString();
            this.yanshoujilu_textBox.Text = dr["验收记录"].ToString();

            if (dr["使用年限"].IsNull == true)
            {
                this.synx_textBox.Text = "";
            }
            else
            {
                this.synx_textBox.Text = (dr["使用年限"].ToDouble() / 12).ToString();
            }

            if (dr["已使用年限"].IsNull == true)
            {
                this.YSYNX_textBox.Text = "";
            }
            else
            {
                this.YSYNX_textBox.Text = (dr["已使用年限"].ToDouble() / 12).ToString();
            }

            //this.synx_textBox.Text = (dr["使用年限"].ToDouble() / 12).ToString();
            //this.YSYNX_textBox.Text = (dr["已使用年限"].ToDouble() / 12).ToString();
            this.baoguanren_textBox.Text = dr["保管人"].ToString();
            if (!DateTime.TryParse(dr["订购时间"].ToString(), out result))
            {
                this.dinggou_dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                this.dinggou_dateTimePicker1.Value = result;
            }
            this.CCH_textBox.Text = dr["出厂号"].ToString();

            if (!DateTime.TryParse(dr["出厂时间"].ToString(), out result))
            {
                this.CCSJdateTimePicker2.Value = DateTime.Now;
            }
            else
            {
                this.CCSJdateTimePicker2.Value = result;
            }
            this.LYR_textBox.Text = dr["领用人"].ToString();

            if (!DateTime.TryParse(dr["领用时间"].ToString(), out result))
            {
                this.LYSJ_dateTimePicker.Value = DateTime.Now;
            }
            else
            {
                this.LYSJ_dateTimePicker.Value = result;
            }

            this.AZR_textBox.Text = dr["安装人"].ToString();

            if (!DateTime.TryParse(dr["安装时间"].ToString(), out result))
            {
                this.AZSJ_dateTimePicker.Value = DateTime.Now;
            }
            else
            {
                this.AZSJ_dateTimePicker.Value = result;
            }

            this.BFR_textBox.Text = dr["报废人"].ToString();

            if (!DateTime.TryParse(dr["报废日期"].ToString(), out result))
            {
                this.BFRQ_dateTimePicker.Value = DateTime.Now;
            }
            else
            {
                this.BFRQ_dateTimePicker.Value = result;
            }

            this.yanshourenyuan_txtBox.Text = dr["验收人员"].ToString();
        }

        #endregion

        #region 浏览时，设置只可读

        private void DisableControls()
        {
            foreach (Control ctr in groupBox2.Controls)
            {
                if (ctr is Label || ctr is GroupBox)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            foreach (Control ctr in groupBox3.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            foreach (Control ctr in groupBox4.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            foreach (Control ctr in groupBox11.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            foreach (Control ctr in CardExplain_tab.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            foreach (Control ctr in CCNO_groupBo.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }

            foreach (Control ctr in CCGet_groupBox.Controls)
            {
                if (ctr is Label)
                {
                    continue;
                }
                ctr.Enabled = false;
            }
            this.button1.Enabled = false;
            this.button3.Enabled = false;
            this.dataGView1.ReadOnly = true;
            this.dataGView2.ReadOnly = true;
            this.FJ_addtoolStrip.Enabled = false;
            this.FJ_DeltoolStrip.Enabled = false;
            this.JL_AddtoolStrip.Enabled = false;
            this.JL_DeltoolStrip.Enabled = false;

        }

        #endregion

        private void AddEQBuildCard_Load(object sender, EventArgs e)
        {
            this.Deptid_selText.Sql = "FindDeptidInCard";
            this.Deptid_selText.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            this.statuscode_selText.Sql = "BindUseStatusInCardEdit";
            this.statuscode_selText.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            this.stockflowno_ytComboBox.Sql = "KuCunLiuShuiBindInCard";
            this.stockflowno_ytComboBox.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            UnitCodeBind();
            EQNameBind();
            TvList.newBind().add("无效", "0").add("未启用", "1").add("已启用", "2").add("已报废", "6").add("已冲销", "7").Bind(Status_ytComboBox);
            Status_ytComboBox.SelectedIndex = 1;
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(JL_StatusColumn);
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(FJ_Status);

            XiaoShuWei = GetSysDanWei(2203);//金额的小数位
            //QianFenWei = GetSysDanWei(2204);//是否按千分位
            IfAutoStar = GetSysDanWei(2205);//是否建卡时自动启用

            groupBox4.Enabled = false;
            groupBox3.Enabled = false;//预先设置为不可写，当时编辑过来，若为对应的设备，则改为可写，编辑时由所选的设备而改变，保存进行验证

            if (isFlag == 0 || isFlag == 1)//浏览  /编辑
            {
                ZBiTianXiangLoad();
                ZXuanTianXiangLoad();
                FJFormLoad();
                JLFormLoad();
                CCFromLoad();
                SMFormLoad();
                if (isFlag == 0)
                {
                    DisableControls();
                }
            }
            if (isFlag == 2)//新增
            {
                this.userid_txtbox.Text = His.his.UserId.ToString();
                this.username_txtbox.Text = His.his.UserName.ToString();
                this.choscode_txtbox.Text = His.his.Choscode.ToString();
                this.recdate_txtbox.Text = DateTime.Now.ToString();
                if (IfAutoStar == 1)
                {
                    this.Status_ytComboBox.SelectedIndex = 2;
                    this.Status_ytComboBox.Enabled = false;
                }
                this.Status_ytComboBox.SelectedIndex = 1;
            }
            if (isFlag == 10)
            {
                userid_txtbox.Text = His.his.UserId.ToString();
                username_txtbox.Text = His.his.UserName;
                choscode_txtbox.Text = His.his.Choscode;
                recdate_txtbox.Text = DateTime.Now.ToString();

                //建卡是针对领用该设备的科室，所以为目的科室 也就是入库库房的科室
                Deptid_selText.Text = drZZ["出库目的科室"].ToString();
                Deptid_selText.Value = drZZ["出库目的科室ID"].ToString();
                stockflowno_ytComboBox.Text = drXX["库存流水号"].ToString();
                stockflowno_ytComboBox.Value = drXX["库存流水号"].ToString();

                eqname_ytComboBox.Value = drXX["设备ID"].ToString();
                eqId_txtbox.Text = drXX["设备ID"].ToString();
                eqnum_txtbox.Text = drXX["数量"].ToString();
                jiage_textBox.Text = drXX["单价"].ToString();
                yuanzhi_textBox.Text = drXX["成本单价"].ToString();
                txm_textBox.Text = drXX["条形码"].ToString();

                stockid_txtbox.Text = LData.LoadDataTable("AllReLoadInCard", null, new object[] { His.his.Choscode, stockflowno_ytComboBox.Value.ToString() }).Rows[0]["STOCKID"].ToString();

                DataTable KeyValueTable = LData.LoadDataTable("FindEQNAMEInCard", null, new object[] { His.his.Choscode, drXX["设备ID"].ToString() });

                //SELECT EQNAME,IFJL,ZJTYPE FROM LKEQ.DICTEQ WHERE CHOSCODE=? AND EQID=?

                if (KeyValueTable.Rows[0]["IFJL"].ToString() != "1")
                {
                    groupBox4.Enabled = false;
                }
                else
                {
                    groupBox4.Enabled = true;

                }
                if (KeyValueTable.Rows[0]["ZJTYPE"].ToString() != "2")
                {
                    groupBox3.Enabled = false;
                }
                else
                {
                    groupBox3.Enabled = true;
                }

                this.eqnum_txtbox.Enabled = false;
                this.Deptid_selText.Enabled = false;
                this.stockflowno_ytComboBox.Enabled = false;
            }
            //dinggou_dateTimePicker11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //CCSJdateTimePicker11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            //LYSJ_dateTimePicker11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //AZSJ_dateTimePicker11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            //CCbuydate_textBox11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //BFRQ_dateTimePicker11 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            dinggou_dateTimePicker11 = DateTime.Now;
            CCSJdateTimePicker11 = DateTime.Now;
            LYSJ_dateTimePicker11 = DateTime.Now;
            AZSJ_dateTimePicker11 = DateTime.Now;
            CCbuydate_textBox11 = DateTime.Now;
            BFRQ_dateTimePicker11 = DateTime.Now;

            //几个触发更改绑定事件
            Deptid_selText.TextChanged += new EventHandler(Deptid_selText_TextChanged);
            stockflowno_ytComboBox.TextChanged += new EventHandler(stockflowno_ytComboBox_Leave);
            eqnum_txtbox.TextChanged += new EventHandler(eqnum_txtbox_TextChanged);
            // eqname_ytComboBox.SelectedIndexChanged += new EventHandler(eqname_ytComboBox_SelectedIndexChanged);
        }


        #region 触发事件

        void stockflowno_ytComboBox_Leave(object sender, EventArgs e)
        {
            if (Deptid_selText.Text != "" && Deptid_selText.Value != null)
            {
                if (stockflowno_ytComboBox.Text != "" && stockflowno_ytComboBox.Value != null)
                {
                    DataTable reLoadDataInCard = LData.LoadDataTable("AllReLoadInCard", null, new object[] { His.his.Choscode.ToString(), stockflowno_ytComboBox.Value.ToString() });
                    if (reLoadDataInCard != null)
                    {
                        if (reLoadDataInCard.Rows.Count <= 0)
                        {
                            return;
                        }

                        //绑定 库存ID,设备名 ，设备ID，默认数，金额，原值，条形码
                        DataRow drBind = reLoadDataInCard.Rows[0];
                        stockid_txtbox.Text = drBind["STOCKID"].ToString();
                        eqId_txtbox.Text = drBind["EQID"].ToString();

                        DataTable KeyValueTable = LData.LoadDataTable("FindEQNAMEInCard", null, new object[] { His.his.Choscode, drBind["EQID"].ToString() });
                        eqname_ytComboBox.Items.Clear();
                        //SELECT EQNAME,IFJL,ZJTYPE FROM LKEQ.DICTEQ WHERE CHOSCODE=? AND EQID=?
                        TvList.newBind().add(KeyValueTable.Rows[0]["EQNAME"].ToString(), drBind["EQID"].ToString()).Bind(eqname_ytComboBox);
                        eqname_ytComboBox.SelectedIndex = 0;

                        //若为计量则可填写，否则不准予填写内容
                        if (KeyValueTable.Rows[0]["IFJL"].ToString() == "1")
                        {
                            groupBox4.Enabled = true;
                            fdvalue_txtbox.Text = "";
                            jdlevel_txtbox.Text = "";
                            jdlevel_txtbox.Text = "";
                            checkrange_txtbox.Text = "";
                            checkZQ_txtbox.Text = "";
                            totaledwork_txtbox.Text = "";
                            totalwork_txtbox.Text = "";
                        }
                        else
                        {
                            groupBox4.Enabled = false;
                        }
                        if (KeyValueTable.Rows[0]["ZJTYPE"].ToString() == "2")
                        {
                            groupBox3.Enabled = true;
                            fdvalue_txtbox.Text = "";
                            jdlevel_txtbox.Text = "";
                            jdlevel_txtbox.Text = "";
                            checkrange_txtbox.Text = "";
                            checkZQ_txtbox.Text = "";
                            totaledwork_txtbox.Text = "";
                            totalwork_txtbox.Text = "";
                        }
                        else
                        {
                            groupBox3.Enabled = false;
                        }

                        EqNumDefault = Convert.ToInt32(drBind["NUM"]) - Convert.ToInt32(drBind["OUTNUM"]) - Convert.ToInt32(drBind["CARDNUM"]);
                        eqnum_txtbox.Text = EqNumDefault.ToString();
                        jiage_textBox.Text = drBind["PRICE"].ToString();
                        yuanzhi_textBox.Text = drBind["TOTALPRICE"].ToString();
                        txm_textBox.Text = drBind["TXM"].ToString();
                    }
                }
            }
            else
            {
                if (NumPrev < 1)
                {
                    WJs.alert("请先选择科室ID！");
                    NumPrev++;
                }
                Deptid_selText.Focus();
                stockflowno_ytComboBox.Text = "";
                stockflowno_ytComboBox.Value = null;
                NumPrev = 0;

            }
        }

        void Deptid_selText_TextChanged(object sender, EventArgs e)
        {

            if (Deptid_selText.Text != "" && Deptid_selText.Value != null)
            {
                stockflowno_ytComboBox.Text = "";
                stockflowno_ytComboBox.Value = null;

                stockflowno_ytComboBox.Sql = "ReKuCunLiuShuiBindInCard";
                stockflowno_ytComboBox.SelParam = His.his.Choscode + "|" + Deptid_selText.Value.ToString() + "|{key}|{key}";
            }
        }

        void eqnum_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (Deptid_selText.Text != "" && Deptid_selText.Value != null && stockflowno_ytComboBox.Text != "" && stockflowno_ytComboBox.Value != null)
            {
                if (eqnum_txtbox.Text != "" && eqnum_txtbox.Text != null)
                {
                    if (EqNumDefault < Convert.ToInt32(eqnum_txtbox.Text))
                    {
                        WJs.alert("建卡数，不能超过库存流水里的总未建卡数！");
                        eqnum_txtbox.Text = EqNumDefault.ToString();
                        return;
                    }
                }
            }
        }

        private void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == FJ_NUMColumn.Index)
            {
                DataGridViewCell dgvc = dataGView1.Rows[e.RowIndex].Cells["FJ_NUMColumn"];
                if (dgvc.Value != null)
                {

                    int Result = 0;
                    if (!int.TryParse(dgvc.Value.ToString().Trim(), out Result))
                    {
                        WJs.alert("数量必须为正整数,已设置为默认值1！");
                        dgvc.Value = "1";
                        return;
                    }
                    //WJs.IsZs(dgvc.Value.ToString())||WJs.IsD0Zs(dgvc.Value.ToString()
                    if (Convert.ToDouble(dgvc.Value) % 1 != 0 || Convert.ToDouble(dgvc.Value) < 0)
                    {
                        WJs.alert("数量必须为正整数，已设置为默认值1 ！");
                        dgvc.Value = "1";
                        return;
                    }
                    if (dataGView1.Rows[e.RowIndex].Cells["FJ_PriceColumn"].Value != null)
                    {
                        double result1;
                        if (Double.TryParse(dataGView1.Rows[e.RowIndex].Cells["FJ_PriceColumn"].Value.ToString(), out result1))
                        {
                            this.dataGView1.jsBds("金额=数量*单价");
                            // dataGView1.Rows[e.RowIndex].Cells["FJ_MoneyColumn"].Value = (Convert.ToDouble(dataGView1.Rows[e.RowIndex].Cells["FJ_PriceColumn"].Value) * Convert.ToDouble(dgvc.Value)).ToString("f" + XiaoShuWei);
                        }
                    }
                }
            }
            if (e.ColumnIndex == FJ_PriceColumn.Index)
            {
                DataGridViewRow dgvr = dataGView1.Rows[e.RowIndex];
                double Result = 0;
                if (!Double.TryParse(dgvr.Cells["FJ_PriceColumn"].Value.ToString().Trim(), out Result))
                {
                    WJs.alert("价格必须为数字,已设置为默认值！");
                    dgvr.Cells["FJ_PriceColumn"].Value = "0.00";
                    return;
                }
                if (dgvr.Cells["FJ_NUMColumn"].Value != null && dgvr.Cells["FJ_PriceColumn"].Value != null)
                {
                    if (Double.TryParse(dgvr.Cells["FJ_NUMColumn"].Value.ToString(), out Result))
                    {
                        this.dataGView1.jsBds("金额=数量*单价");
                        //dgvr.Cells["FJ_MoneyColumn"].Value = (Convert.ToDouble(dgvr.Cells["FJ_NUMColumn"].Value) * Convert.ToDouble(dgvr.Cells["FJ_PriceColumn"].Value)).ToString("f" + XiaoShuWei);
                    }
                }
            }
        }
        #endregion

        #region 将文件转换成流
        //public byte[] SetImageToByteArray(string fileName, ref string fileSize)
        /// <summary>
        /// 将文件转换成流
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <returns></returns>
        private byte[] SetImageToByteArray(string fileName)
        {
            byte[] image = null;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                FileInfo fileInfo = new FileInfo(fileName);
                //fileSize = Convert.ToDecimal(fileInfo.Length / 1024).ToString("f2") + " K";
                int streamLength = (int)fs.Length;
                image = new byte[streamLength];
                fs.Read(image, 0, streamLength);
                fs.Close();
                return image;
            }
            catch
            {
                return image;
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {

            //(*.bmp, *.jpg)|*.bmp;*.jpg 
            openFileDialog1.Filter = "图片文件(*.jpg,*.png)|*.jpg;*.bmp";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                this.pictureBox1.Load(fileName);
                this.SMext_textBox.Text = fileName.Substring(fileName.LastIndexOf('.'));
                Byte[] imgByte = SetImageToByteArray(fileName);
                bytPic = imgByte;//专门保存传更新的图片
            }
        }

        //JL_RowNOColumn     JL_CardIdColumn
        private void FJ_DeltoolStrip_Click(object sender, EventArgs e)
        {

            if (this.dataGView1.CurrentRow != null)
            {
                DataGridViewRow dgr1 = this.dataGView1.CurrentRow;
                if (dgr1.Cells["FJ_Status"].Value.ToString() == "0")
                {
                    WJs.alert("该附件已经无效，无需删除！");
                    return;
                }
                //如果为编辑， 新增fujian都加上cardid  会没有rowid  就直接删除行  否则更改状态
                //如果行号为空，肯定是编辑或者新增过来添加的新行，直接删除
                //行号不为空，则必须是编辑界面已存在  
                if (dgr1.Cells["FJ_RowNoColumn"].Value != null && dgr1.Cells["FJ_RowNoColumn"].Value.ToString() != "")
                {
                    if (WJs.confirm("你确定要删除选中的卡片附件信息，设置其为无效？"))
                    {
                        LData.Exe("UpdateFJStatusInCard", null, new object[] { dgr1.Cells["FJ_CardIdCloumn"].Value.ToString(), dgr1.Cells["FJ_RowNoColumn"].Value.ToString(), His.his.Choscode.ToString() });
                        FJFormLoad();
                    }
                }
                else
                {
                    dataGView1.Rows.Remove(dgr1);
                }
            }
            else
            {
                WJs.alert("请选择要删除的卡片附件信息！");
            }
        }
        private void JL_DeltoolStrip_Click(object sender, EventArgs e)
        {
            if (this.dataGView2.CurrentRow != null)
            {
                DataGridViewRow dgr2 = this.dataGView2.CurrentRow;
                if (dgr2.Cells["JL_StatusColumn"].Value.ToString() == "0")
                {
                    WJs.alert("该计量信息已经无效，无需删除！");
                    return;
                }
                //如果为编辑  新增fujian都加上cardid  会没有rowid就直接删除行  否则更改状态  JL_StatusColumn
                if (dgr2.Cells["JL_RowNOColumn"].Value != null && dgr2.Cells["JL_RowNOColumn"].Value.ToString() != "")
                {
                    if (WJs.confirm("你确定要删除选中的卡片计量信息？"))
                    {
                        LData.Exe("UpdateJLStatusInCard", null, new object[] { dgr2.Cells["JL_CardIdColumn"].Value.ToString(), dgr2.Cells["JL_RowNOColumn"].Value.ToString(), His.his.Choscode.ToString() });
                        JLFormLoad();
                    }
                    if (isFlag == 2)
                    {
                        dataGView2.Rows.Remove(dgr2);
                    }
                }
                else
                {
                    dataGView2.Rows.Remove(dgr2);
                }
            }
            else
            {
                WJs.alert("请选择要删除的卡片计量信息！");
            }
        }
        private void FJ_addtoolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["状态"] = "1";
            de["操作员ID"] = His.his.UserId.ToString();
            de["操作员名称"] = His.his.UserName.ToString();
            de["医疗机构编码"] = His.his.Choscode.ToString();
            de["修改时间"] = DateTime.Now;
            de["单位"] = "1";//默认添加一个单位
            if (isFlag == 1)//点击编辑时，无论怎么都是一样的出库ID
            {
                de["卡片ID"] = dr["卡片ID"].ToString();
            }
            de["金额"] = 0.ToString("f" + XiaoShuWei);
            this.dataGView1.AddRow(de, 0);

        }
        private void JL_AddtoolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["状态"] = "1";
            de["操作员ID"] = His.his.UserId.ToString();
            de["操作员名称"] = His.his.UserName.ToString();
            de["医疗机构编码"] = His.his.Choscode.ToString();
            de["修改时间"] = DateTime.Now;
            if (isFlag == 1)//点击编辑时，无论怎么都是一样的出库ID
            {
                de["卡片ID"] = dr["卡片ID"].ToString();
            }
            this.dataGView2.AddRow(de, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGView1.IsAutoAddRow = false;
            this.dataGView2.IsAutoAddRow = false;
            if (WJs.confirm("你确定要保存设备卡片信息及其所有的附属信息吗？"))
            {
                IfOneToOne = GetSysDanWei(2202);//获取是否允许一对多
                if (YanZhengZhu() == 1)
                {
                    return;
                }
                if (YanZhengFJInfo() == 1)
                {
                    return;
                }
                if (YanZhengJLInfo() == 1)
                {
                    return;
                }

                int EQNUMInCard = Convert.ToInt32(eqnum_txtbox.Text);
                if (EQNUMInCard > 1)
                {
                    if (IfOneToOne == 1)//一对一 
                    {
                        for (int i = 1; i <= EQNUMInCard; i++)
                        {
                            eqnum_txtbox.Text = "1";
                            //若为新增，当数目大于1，全部新增即可。//第一次进来为编辑 而且数量大于1 ，那么第一个为更新 其余的都为插入  关键是需要改变数量
                            if (isFlag == 1)
                            {
                                SaveOrAdd();
                                isFlag = 2;
                                continue;
                            }
                            SaveOrAdd();
                        }
                    }//[一对多]
                    else
                    {
                        SaveOrAdd();
                    }
                }
                else
                {
                    SaveOrAdd();
                }
            }
            if (isOK)
            {
                WJs.alert("卡片信息修改成功，即将关闭本窗口……");
                this.Close();
            }
        }

        private void SaveOrAdd()
        {
            if (isFlag == 1)//编辑
            {
                //分三次  ? 两个表格 分别处理  其余三个界面可以一起处理  主界面 财产 说明一起  ************** 在一起会出错                   
                ActionLoad acZhuInfo = ActionLoad.Conn();
                acZhuInfo.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acZhuInfo.Sql = "ModifyZhuInfo";
                AddZhuInfo(acZhuInfo);
                acZhuInfo.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhuInfo_ServiceLoad);
                acZhuInfo.Post();
                if (!isOK)
                {
                    return;
                }
                ActionLoad acCC = ActionLoad.Conn();
                acCC.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acCC.Sql = "ModifyCCInfo";
                //对财产进行判断
                acCC.Add("IsUpdateCC", null);
                if (CCcardid_textBox.Text != "")
                {
                    AddCCInfo(acCC);
                    acCC.Add("IsUpdateCC", "1");
                }
                else
                {//需要判断是否存在其他内容  若时间与默认时间不一样，则说明修改。
                    // if (CCbuydate_textBox.Value.ToString("yyyy-MM-dd HH:mm") != CCbuydate_textBox11.ToString("yyyy-MM-dd HH:mm"))
                    if (CCbuydate_textBox11 - CCbuydate_textBox.Value > TimeSpan.FromSeconds(15) || CCbuydate_textBox.Value - CCbuydate_textBox11 > TimeSpan.FromSeconds(15))
                    {
                        AddCCInfo(acCC);
                        acCC.Add("IsUpdateCC", "0");
                    }
                    else
                    {
                        foreach (Control ctr in CCGet_groupBox.Controls)
                        {
                            if (ctr is Label || ctr is MaskedTextBox)
                            {
                                continue;
                            }
                            if (ctr is TextBox && ctr.Text != "")
                            {
                                AddCCInfo(acCC);
                                acCC.Add("IsUpdateCC", "0");
                                break;
                            }
                        }
                    }
                }
                if (acCC.Param["IsUpdateCC"] != null)
                {
                    acCC.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhuInfo_ServiceLoad);
                    acCC.Post();

                }
                if (!isOK)
                {
                    return;
                }
                //单独对说明进行判断
                ActionLoad acSM = ActionLoad.Conn();
                acSM.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acSM.Sql = "ModifySMInfo";
                acSM.Add("IsUpdateSM", null);
                if (SMcardid_textBox.Text != "")
                {
                    AddSMInfo(acSM);
                    acSM.Add("IsUpdateSM", "1");
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        AddSMInfo(acSM);
                        acSM.Add("IsUpdateSM", "0");
                    }
                    else //图片不存在
                    {
                        foreach (Control ctr in CardExplain_tab.Controls)
                        {
                            if (ctr is Label || ctr is Button)
                            {
                                continue;
                            }
                            if (ctr is TextBox && ctr.Text != "")
                            {
                                AddSMInfo(acSM);
                                acSM.Add("IsUpdateSM", "0");
                                break;
                            }
                        }
                    }
                }
                if (acSM.Param["IsUpdateSM"] != null)
                {
                    acSM.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhuInfo_ServiceLoad);
                    acSM.Post();
                }


                if (!isOK)
                {
                    return;
                }
                //继续判断附件是否存在内容
                if (dataGView1.GetData() != null)
                {
                    foreach (Dictionary<string, ObjItem> itFJ in dataGView1.GetData())
                    {
                        ActionLoad acFJ = ActionLoad.Conn();
                        acFJ.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                        acFJ.Sql = "ModifyFJInfo";
                        //因为经过第一次的验证后不知道为什么会增加新的空行所以进行第二次的验证
                        if (itFJ["附件名称"].ToString() == "" || itFJ["附件名称"].ToString() == null)
                        {
                            continue;
                        }
                        AddFJInfo(acFJ, itFJ);
                        //存在内容的情况下，判断该行是新增还是需更新[我已经更新]
                        if (itFJ["行号"].ToString() == "" || itFJ["行号"].ToString() == null)//说明为自己点的新增
                        {
                            acFJ.Add("IsUpdateFJ", "0");
                        }
                        else
                        {
                            acFJ.Add("IsUpdateFJ", "1");
                        }
                        acFJ.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhuInfo_ServiceLoad);
                        acFJ.Post();
                    }
                    if (!isOK)
                    {
                        return;
                    }
                }

                //继续 计量设备是否存在内容
                if (dataGView2.GetData() != null)
                {
                    foreach (Dictionary<string, ObjItem> itJL in dataGView2.GetData())
                    {
                        ActionLoad acJL = ActionLoad.Conn();
                        acJL.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                        acJL.Sql = "ModifyJLInfo";

                        if (itJL["维修内容摘要"].ToString() == "" || itJL["维修内容摘要"].ToString() == null)
                        {
                            continue;
                        }
                        AddJLInfo(acJL, itJL);
                        //存在内容的情况下，判断该行是新增还是需更新[我已经更新]
                        if (itJL["行号"].ToString() == "" || itJL["行号"].ToString() == null)
                        {
                            acJL.Add("IsUpdateJL", "0");//插入操作
                        }
                        else
                        {
                            acJL.Add("IsUpdateJL", "1");
                        }
                        acJL.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhuInfo_ServiceLoad);
                        acJL.Post();
                    }
                    if (!isOK)
                    {
                        return;
                    }
                }
            }
            else  //isFlag=2 或者 10  当处于10的时候，我们应该是新建一张设备卡片
            {
                //===============================新增
                ActionLoad acZhu = ActionLoad.Conn();//主
                acZhu.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acZhu.Sql = "AddZhuInfo";
                AddZhuInfo(acZhu);
                if (isFlag == 10)
                {
                    acZhu.Add("IsFlag", 10);
                }
                acZhu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                acZhu.Post();
                if (!isOK)
                {
                    return;
                }
                //计量
                if (dataGView2.GetData() != null)
                {
                    foreach (Dictionary<string, ObjItem> itemJL in dataGView2.GetData())
                    {
                        ActionLoad acJL = ActionLoad.Conn();
                        acJL.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                        acJL.Sql = "AddJLInfo";
                        if (itemJL["维修内容摘要"].ToString() == "" || itemJL["维修内容摘要"].ToString() == null)
                        {
                            continue;
                        }
                        AddJLInfo(acJL, itemJL);
                        acJL.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                        acJL.Post();
                    }
                }

                if (!isOK)
                {
                    return;
                }
                //财产
                ActionLoad acCC = ActionLoad.Conn();
                acCC.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acCC.Sql = "AddCCInfo";
                //单独拿出MaskedTextBox
                //if (CCbuydate_textBox.Value.ToString("yyyy-MM-dd HH:mm") != CCbuydate_textBox11.ToString("yyyy-MM-dd HH:mm"))

                if (CCbuydate_textBox11 - CCbuydate_textBox.Value > TimeSpan.FromSeconds(15) || CCbuydate_textBox.Value - CCbuydate_textBox11 > TimeSpan.FromSeconds(15))
                {
                    AddCCInfo(acCC);
                }
                else
                {
                    foreach (Control ctr in CCGet_groupBox.Controls)
                    {
                        if (ctr is Label || ctr is MaskedTextBox)
                        {
                            continue;
                        }
                        if (ctr is TextBox && ctr.Text != "")
                        {
                            AddCCInfo(acCC);
                            break;
                        }
                    }
                }
                //只要包含参数，那么就会发送出去[填了内容]
                if (acCC.Param.Count > 0)
                {
                    acCC.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                    acCC.Post();
                }
                if (!isOK)
                {
                    return;
                }

                //附件
                if (dataGView1.GetData() != null)
                {
                    foreach (Dictionary<string, ObjItem> itemFJ in dataGView1.GetData())
                    {
                        ActionLoad acFJ = ActionLoad.Conn();
                        acFJ.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                        acFJ.Sql = "AddFJInfo";
                        //因为经过第一次的验证后不知道为什么会增加新的空行所以进行第二次的验证
                        if (itemFJ["附件名称"].ToString() == "" || itemFJ["附件名称"].ToString() == null)
                        {
                            continue;
                        }
                        AddFJInfo(acFJ, itemFJ);
                        acFJ.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                        acFJ.Post();
                    }
                }
                if (!isOK)
                {
                    return;
                }


                //说明
                ActionLoad acSM = ActionLoad.Conn();
                acSM.Action = "LKWZSVR.lkeq.WareManag.EQBuildCardSvr";
                acSM.Sql = "AddSMInfo";

                if (pictureBox1.Image != null)
                {
                    AddSMInfo(acSM);
                }
                else //图片不存在
                {
                    foreach (Control ctr in CardExplain_tab.Controls)
                    {
                        if (ctr is Label || ctr is Button)
                        {
                            continue;
                        }
                        if (ctr is TextBox && ctr.Text != "")
                        {
                            AddSMInfo(acSM);
                            break;
                        }
                    }
                }
                if (acSM.Param.Count > 0)
                {
                    acSM.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                    acSM.Post();
                }
            }
        }

        void acZhu_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK = true;
            }
            else
            {
                isOK = false;
                WJs.alert(e.Msg.Msg);
            }
        }

        void acZhuInfo_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK = true;
            }
            else
            {
                isOK = false;
                WJs.alert(e.Msg.Msg);//照理是某一个执行错误之后的都不会再继续进行                
            }
        }


        private void AddZhuInfo(ActionLoad ac)
        {
            //48
            ac.Add("CARDID", cardId_txtbox.Text);
            ac.Add("CARDCODE", cardcode_txtbox.Text);
            ac.Add("STOCKFLOWNO", stockflowno_ytComboBox.Value);
            ac.Add("STOCKID", stockid_txtbox.Text);
            ac.Add("EQID", eqId_txtbox.Text);

            ac.Add("EQNAME", eqname_ytComboBox.Text);

            ac.Add("COUNTRY", guobie_textBox.Text);
            ac.Add("CONTRACTCODE", hetonghao_textBox.Text);
            ac.Add("EQNUM", eqnum_txtbox.Text);
            ac.Add("PRICE", jiage_textBox.Text);
            ac.Add("YPRICE", yuanzhi_textBox.Text);

            double shoufei;
            if (double.TryParse(shoufeibiaozhun_textBox.Text.Trim(), out shoufei))
            {
                ac.Add("FARE", shoufei.ToString("f" + XiaoShuWei));
            }
            else
            {
                ac.Add("FARE", "");
            }

            //要么为空，要么就为正确的日期格式[带11的是刚进入的时候获取的时间，如果时间没改变，那么为没有输入时间，如果时间改变了，则设置为当前时间框内的时间]
            //if (dinggou_dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") == dinggou_dateTimePicker11.ToString("yyyy-MM-dd HH:mm"))
            //{
            //    ac.Add("DGDATE", "");
            //}
            //else
            //{
            //    ac.Add("DGDATE", dinggou_dateTimePicker1.Value);
            //}

            //如果小于10秒的时间，是原值加载时的差异，否则是修改了的
            if (dinggou_dateTimePicker11 - dinggou_dateTimePicker1.Value > TimeSpan.FromSeconds(15) || dinggou_dateTimePicker1.Value - dinggou_dateTimePicker11 > TimeSpan.FromSeconds(15))
            {

                ac.Add("DGDATE", dinggou_dateTimePicker1.Value);
            }
            else
            {
                ac.Add("DGDATE", "");
            }

            if (CCSJdateTimePicker11 - CCSJdateTimePicker2.Value > TimeSpan.FromSeconds(15) || CCSJdateTimePicker2.Value - CCSJdateTimePicker11 > TimeSpan.FromSeconds(15))
            {
                ac.Add("CCDATE", CCSJdateTimePicker2.Value);
            }
            else
            {
                ac.Add("CCDATE", "");
            }

            if (LYSJ_dateTimePicker11 - LYSJ_dateTimePicker.Value > TimeSpan.FromSeconds(15) || LYSJ_dateTimePicker.Value - LYSJ_dateTimePicker11 > TimeSpan.FromSeconds(15))
            {
                ac.Add("LYDATE", LYSJ_dateTimePicker.Value);
            }
            else
            {
                ac.Add("LYDATE", "");
            }

            if (AZSJ_dateTimePicker11 - AZSJ_dateTimePicker.Value > TimeSpan.FromSeconds(15) || AZSJ_dateTimePicker.Value - AZSJ_dateTimePicker11 > TimeSpan.FromSeconds(15))
            {
                ac.Add("SETUPDATE", AZSJ_dateTimePicker.Value);
            }
            else
            {
                ac.Add("SETUPDATE", "");
            }



            //if (BFRQ_dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm") == BFRQ_dateTimePicker11.ToString("yyyy-MM-dd HH:mm"))
            if (BFRQ_dateTimePicker11 - BFRQ_dateTimePicker.Value > TimeSpan.FromSeconds(15) || BFRQ_dateTimePicker.Value - BFRQ_dateTimePicker11 > TimeSpan.FromSeconds(15))
            {
                ac.Add("BFDATE", BFRQ_dateTimePicker.Value);
            }
            else
            {
                ac.Add("BFDATE", "");
            }

            ac.Add("LYPEOPLE", LYR_textBox.Text);
            ac.Add("SETUPPEOPLE", AZR_textBox.Text);
            ac.Add("CCCODE", CCH_textBox.Text);
            ac.Add("STATUSCODE", statuscode_selText.Value);
            if (synx_textBox.Text.Trim() != "" && synx_textBox.Text != null)
            {
                ac.Add("USEYEAR", Convert.ToDouble(synx_textBox.Text.Trim()) * 12);
            }
            else
            {
                ac.Add("USEYEAR", "");
            }

            if (YSYNX_textBox.Text.Trim() != "" && YSYNX_textBox.Text != null)
            {
                ac.Add("USEDYEAR", Convert.ToDouble(YSYNX_textBox.Text.Trim()) * 12);
            }
            else
            {
                ac.Add("USEDYEAR", "");
            }

            ac.Add("TOTALWORK", totalwork_txtbox.Text);
            ac.Add("TOTALEDWORK", totaledwork_txtbox.Text);
            ac.Add("TOTALZJ", leijizhejiu_textBox.Text);


            ac.Add("CZRATE", canzhilv_textBox.Text);
            ac.Add("MAINUSE", zhuyaoyongtu_textBox.Text);
            ac.Add("YSREC", yanshoujilu_textBox.Text);
            ac.Add("YSPEOPLE", yanshourenyuan_txtBox.Text);
            ac.Add("QUATHING", zhiliangqingkuang_textBox.Text);
            ac.Add("FDVALUE", fdvalue_txtbox.Text);

            ac.Add("JDLEVEL", jdlevel_txtbox.Text);
            ac.Add("CHECKLEVEL", checklevel_txtbox.Text);
            ac.Add("CHECKRANGE", checkrange_txtbox.Text);

            if (checkZQ_txtbox.Text.Trim() != "" && checkZQ_txtbox.Text != null)
            {
                ac.Add("CHECKZQ", Convert.ToDouble(checkZQ_txtbox.Text.Trim()) * 12);
            }
            else
            {
                ac.Add("CHECKZQ", "");
            }
            ac.Add("DEPTID", Deptid_selText.Value);
            ac.Add("BGPEOPLE", baoguanren_textBox.Text);
            ac.Add("BFPEOPLE", BFR_textBox.Text);
            ac.Add("MEMO", memo_textBox.Text);
            ac.Add("TXM", txm_textBox.Text);
            ac.Add("STATUS", Status_ytComboBox.Value);

            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);
            //是否自动启用
            if (IfAutoStar == 0)
            {
                ac.Add("STARTDATE", "");
                ac.Add("STARTPEOPLE", "");
            }
            else
            {
                ac.Add("STARTDATE", DateTime.Now);
                ac.Add("STARTPEOPLE", His.his.UserName);
            }
        }

        private void AddFJInfo(ActionLoad ac, Dictionary<string, ObjItem> drFJ)
        {
            //15
            ac.Add("CARDID", cardId_txtbox.Text.ToString());
            ac.Add("ROWNO", drFJ["行号"].ToString());
            ac.Add("FJNAME", drFJ["附件名称"].ToString());
            ac.Add("COUNTRY", drFJ["国别"].ToString());
            ac.Add("GGXH", drFJ["规格型号"].ToString());
            ac.Add("CCCODE", drFJ["出厂号"].ToString());
            ac.Add("UNIT", drFJ["单位"].ToString());
            ac.Add("NUM", drFJ["数量"].ToInt());
            ac.Add("PRICE", drFJ["单价"].ToString());
            ac.Add("TOTALMONEY", drFJ["金额"].ToString());
            ac.Add("STATUS", drFJ["状态"].ToString());
            ac.Add("USERID", drFJ["操作员ID"].ToString());
            ac.Add("USERNAME", drFJ["操作员名称"].ToString());
            ac.Add("CHOSCODE", drFJ["医疗机构编码"].ToString());
            ac.Add("RECDATE", drFJ["修改时间"].ToDateTime());
        }

        private void AddJLInfo(ActionLoad ac, Dictionary<string, ObjItem> drJL)
        {
            //12
            ac.Add("CARDID", cardId_txtbox.Text.ToString());
            ac.Add("ROWNO", drJL["行号"].ToString());
            ac.Add("REPAIRTHING", drJL["维修内容摘要"].ToString());

            ac.Add("JL", drJL["结论"].ToString());
            ac.Add("REPAIRPEOPLE", drJL["维修人"].ToString());
            ac.Add("CHECKDATE", drJL["检定日期"].ToDateTime());

            ac.Add("STATUS", drJL["状态"].ToString());
            ac.Add("USERID", drJL["操作员ID"].ToString());
            ac.Add("USERNAME", drJL["操作员名称"].ToString());

            ac.Add("RECDATE", drJL["修改时间"].ToDateTime());
            ac.Add("CHOSCODE", drJL["医疗机构编码"].ToString());
            ac.Add("REPAIRDATE", drJL["维修日期"].ToDateTime());
        }

        private void AddCCInfo(ActionLoad ac)//5
        {
            ac.Add("CARDID", cardId_txtbox.Text);
            ac.Add("SUPPLY", CCsupply_textBox.Text);
            ac.Add("CSNAME", CCcsname_textBox.Text);

            //if (CCbuydate_textBox.Value.ToString("yyyy-MM-dd HH:mm") == CCbuydate_textBox11.ToString("yyyy-MM-dd HH:mm"))不知道为什么 这里的相差时间非常大
            if (CCbuydate_textBox11 - CCbuydate_textBox.Value > TimeSpan.FromSeconds(15) || CCbuydate_textBox.Value - CCbuydate_textBox11 > TimeSpan.FromSeconds(15))
            {

                ac.Add("BUYDATE", CCbuydate_textBox.Value);
            }
            else
            {
                ac.Add("BUYDATE", "");
            }

            ac.Add("OTHER", CCother_textBox.Text);
            ac.Add("NEWOLD", CCnewold_textBox.Text);

            //出厂时间
            //if (CCSJdateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm") != CCSJdateTimePicker11.ToString("yyyy-MM-dd HH:mm"))
            if (CCSJdateTimePicker11 - CCSJdateTimePicker2.Value > TimeSpan.FromSeconds(15) || CCSJdateTimePicker2.Value - CCSJdateTimePicker11 > TimeSpan.FromSeconds(15))
            {
                ac.Add("CCDATE", CCSJdateTimePicker2.Value);
            }
            else
            {
                ac.Add("CCDATE", "");
            }

            ac.Add("PRICE", jiage_textBox.Text);
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("CHOSCODE", His.his.Choscode);
        }

        private void AddSMInfo(ActionLoad ac)
        {
            ac.Add("CARDID", cardId_txtbox.Text);

            ac.Add("EXPLAINNUM", SMexplainNum_textBox.Text);
            ac.Add("TECHNUM", SMteachnum_textBox.Text);
            ac.Add("BOXTHING", SMboxthing_textBox.Text);
            ac.Add("CERTIFICATE", SMcertificate_textBox.Text);
            ac.Add("OTHER", SMother_textBox.Text);
            if (bytPic != null)
            {
                ac.Add("PICFILE", bytPic);//这个需要特殊对待
            }
            else
            {
                ac.Add("PICFILE", bytPicLoad);
            }
            ac.Add("EXT", SMext_textBox.Text);

            ac.Add("USERID", His.his.UserId);
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("CHOSCODE", His.his.Choscode);
        }



        #region 保存时数据的验证

        private int YanZhengJLInfo()
        {
            Num = 0;
            List<Dictionary<string, ObjItem>> list = this.dataGView2.GetData();
            if (list != null)
            {
                if (list.Count <= 0)
                {
                    return 1;
                }
                foreach (Dictionary<string, ObjItem> item in list)
                {
                    Num++;
                    if (item["维修内容摘要"].ToString() == "" || item["维修内容摘要"].ToString() == null)
                    {
                        WJs.alert("计量模块必录项，第" + Num + "行：维修内容摘要！");
                        return 1;
                    }
                    if (item["维修日期"].ToString() == "" || item["维修日期"].ToString() == null)
                    {
                        WJs.alert("计量模块必录项，第" + Num + "行：维修日期！");
                        return 1;
                    }
                    if (item["状态"].ToString() == "" || item["状态"].ToString() == null)
                    {
                        WJs.alert("计量模块必录项，第" + Num + "行：状态！");
                        return 1;
                    }
                }
            }
            return 0;
        }
        private int YanZhengFJInfo()
        {
            Num = 0;
            List<Dictionary<string, ObjItem>> list = this.dataGView1.GetData();
            if (list != null)
            {
                if (list.Count <= 0)
                {
                    return 1;
                }
                foreach (Dictionary<string, ObjItem> item in list)
                {
                    Num++;
                    if (item["附件名称"].ToString() == "" || item["附件名称"].ToString() == null)
                    {
                        WJs.alert("附件模块必录项，第" + Num + "行：附件名称！");
                        return 1;
                    }
                    if (item["单位"].ToString() == "" || item["单位"].ToString() == null)
                    {
                        WJs.alert("附件模块必录项，第" + Num + "行：单位！");
                        return 1;
                    }
                    if (item["数量"].ToString() == "" || item["数量"].ToString() == null)
                    {
                        WJs.alert("附件模块必录项，第" + Num + "行：数量！");
                        return 1;
                    }
                    if (item["状态"].ToString() == "" || item["状态"].ToString() == null)
                    {
                        WJs.alert("附件模块必录项，第" + Num + "行：状态！");
                        return 1;
                    }
                }
            }
            return 0;
        }
        private int YanZhengZhu()
        {
            if (Deptid_selText.Value == "" || Deptid_selText.Value == null)
            {
                WJs.alert("请选择科室信息！");
                Deptid_selText.Focus();
                return 1;
            }
            if (stockflowno_ytComboBox.Value == "" || stockflowno_ytComboBox.Value == null)
            {
                WJs.alert("请选择库存流水号！");
                stockflowno_ytComboBox.Focus();
                return 1;
            }
            if (statuscode_selText.Value == "" || statuscode_selText.Value == null)
            {
                WJs.alert("请选择使用状态！");
                statuscode_selText.Focus();
                return 1;
            }
            if (eqname_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择对应流水号的设备！");
                eqname_ytComboBox.Focus();
                return 1;
            }
            if (eqnum_txtbox.Text == "")
            {
                WJs.alert("请填写设备数量！");
                eqnum_txtbox.Focus();
                return 1;
            }
            if (Convert.ToDouble(eqnum_txtbox.Text) % 1 != 0 || Convert.ToDouble(eqnum_txtbox.Text) < 0)
            {
                WJs.alert("数量必须为正整数，已设置为默认值（库存流水记录里的未建卡数）！");
                eqnum_txtbox.Text = EqNumDefault.ToString();
                return 1;
            }

            if (canzhilv_textBox.Text.Trim() != "" && canzhilv_textBox.Text.Trim() != null)
            {
                if (WJs.IsNum(canzhilv_textBox.Text) == false || Convert.ToDouble(canzhilv_textBox.Text.Trim()) > 1 || Convert.ToDouble(canzhilv_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("残值率只能为0到1的数字！");
                    return 1;
                }
            }

            if (checkZQ_txtbox.Text.Trim() != "" && checkZQ_txtbox.Text.Trim() != null)
            {
                if (WJs.IsNum(checkZQ_txtbox.Text) == false || Convert.ToDouble(checkZQ_txtbox.Text) < 0)
                {
                    WJs.alert("检定周期只能为大于0的数字！");
                    return 1;
                }
            }

            if (shoufeibiaozhun_textBox.Text.Trim() != "" && shoufeibiaozhun_textBox.Text.Trim() != null)
            {
                if (WJs.IsNum(shoufeibiaozhun_textBox.Text.Trim()) == false || Convert.ToDouble(shoufeibiaozhun_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("收费标准只能为大于0的数字！");
                    return 1;
                }
            }

            if (synx_textBox.Text.Trim() != "" && synx_textBox.Text.Trim() != null)
            {
                if (WJs.IsNum(synx_textBox.Text.Trim()) == false || Convert.ToDouble(synx_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("使用年限只能为大于0的数字！");
                    return 1;
                }
            }

            if (YSYNX_textBox.Text.Trim() != "" && YSYNX_textBox.Text.Trim() != null)
            {
                if (WJs.IsNum(YSYNX_textBox.Text.Trim()) == false || Convert.ToDouble(YSYNX_textBox.Text.Trim()) < 0)
                {
                    WJs.alert("已使用年限只能为大于0的数字！");
                    return 1;
                }
            }


            DataTable dtIfJLOrZJ = LData.LoadDataTable("FindIfJLOrZJInfo", null, new object[] { eqId_txtbox.Text, His.his.Choscode });
            if (dtIfJLOrZJ != null)
            {
                DataRow drIfJLOrZJ = dtIfJLOrZJ.Rows[0];
                if (drIfJLOrZJ["IFJL"].ToString() == "1")
                {
                    groupBox4.Enabled = true;
                    foreach (Control itCtr in groupBox4.Controls)
                    {
                        if (itCtr is Label)
                        {
                            continue;
                        }
                        if (itCtr.Text == "" || itCtr.Text == null)
                        {
                            WJs.alert("该设备为计量设备，请填写其详细的计量信息！");
                            fdvalue_txtbox.Focus();
                            return 1;
                        }
                    }
                }
                if (drIfJLOrZJ["ZJTYPE"].ToString() == "2")
                {
                    groupBox3.Enabled = true;
                    if (totaledwork_txtbox.Text == "" || totaledwork_txtbox.Text == null || totalwork_txtbox.Text == "" || totalwork_txtbox.Text == null)
                    {
                        //是否计提折旧SELECT IFJL,ZJTYPE FROM LKEQ.DICTEQ WHERE EQID=1
                        WJs.alert("该设备为按工作量法计提折旧，请填写工作量信息！");
                        totalwork_txtbox.Focus();
                        return 1;
                    }
                }
            }
            return 0;
        }

        #endregion

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            this.TiaoSu.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("金额").ToString() + "元";
        }

        private void dataGView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            label119.Text = "共:" + this.dataGView2.Rows.GetRowCount(DataGridViewElementStates.Visible) + "笔";
        }

    }
}
