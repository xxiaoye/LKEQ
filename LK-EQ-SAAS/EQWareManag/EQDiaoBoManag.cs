using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using YtWinContrl.com.datagrid;
using ChSys;
using YtClient;
using YtUtil.tool;
using YiTian.db;
using EQWareManag.form;

namespace EQWareManag
{
    public partial class EQDiaoBoManag : Form, IPlug
    {
        DataTable EQWareInfo;
        DataTable EQOutInfo;
        DataTable EQUnitInfo;
        DataTable EQIDInfo;

        bool ifHaveExsit;//针对库存主表是否存在某一种设备
        object GetStockId;
        int BeforeNum = 0;//保存库存流水的入库前数量[beforenum]
        bool isOk;//针对审核
        bool isOK2;//针对冲销

        string IOID;
        DataTable GetStockIdAndNum;//库存总表ID号和数目

        string TargetWareDeptid;

        //int TransOUTID = 0;

        public EQDiaoBoManag()
        {
            InitializeComponent();
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").add("全部", "10").Bind(this.Status_ytComboBox);
            this.Status_ytComboBox.SelectedIndex = 1;

            this.selTextInpt1.Sql = "WareBindInDiaoBo";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
        }

        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {

        }

        public void init()
        {

        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        private void EQDiaoBoManag_Load(object sender, EventArgs e)
        {

            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").Bind(this.status_Column);
            TvList.newBind().add("普通", "0").add("调拨", "1").add("申领", "2").add("盘点", "3").Bind(this.opflag_Cloumn);
            IOID = LData.Es("FindIOIdInEQInMain", null, new object[] { His.his.Choscode });
            //[这里其实都可以用联合查询做出来，为了不做代码块的大范围改动，这里暂时不修改]

            //库房名称绑定
            ActionLoad acware = ActionLoad.Conn();
            acware.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acware.Sql = "BindWareName";
            acware.Add("CHOSCODE", His.his.Choscode);
            acware.ServiceLoad += new YtClient.data.events.LoadEventHandle(acware_ServiceLoad);
            acware.Post();


            //出库名称绑定
            ActionLoad acoutname = ActionLoad.Conn();
            acoutname.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acoutname.Sql = "BindOutName";
            acoutname.Add("CHOSCODE", His.his.Choscode);
            acoutname.ServiceLoad += new YtClient.data.events.LoadEventHandle(acoutname_ServiceLoad);
            acoutname.Post();

            //细表中 单位编码转换为汉字
            UnitCodeBind();

            //细表  设备ID
            EQIDBind();

            this.dataGView1.Url = "LoadEQOutMainInfo";
            // this.dataGView1.IsPage = true;
            this.dataGView2.Url = "LoadEQOutDetailList";



            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        #region 回调函数

        //出库名称  [先前不知道可以LData.LoadDataTable]
        void acoutname_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            EQOutInfo = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.outstyle_Column).Items.Clear();
            if (EQOutInfo != null)
            {
                foreach (DataRow r in EQOutInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
                tv.Bind(this.outstyle_Column);
            }
        }

        //库房编码转名称
        void acware_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            EQWareInfo = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.ware1Cloumn).Items.Clear();
            ((DataGridViewComboBoxColumn)this.ware2Cloumn).Items.Clear();
            if (EQWareInfo != null)
            {
                foreach (DataRow r in EQWareInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
            }
            tv.Bind(this.ware2Cloumn);
            tv.Bind(this.ware1Cloumn);
        }


        //单位编码
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
            EQUnitInfo = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Unitcode_Column).Items.Clear();
            if (EQUnitInfo != null)
            {
                foreach (DataRow r in EQUnitInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
                tv.Bind(this.Unitcode_Column);
            }
        }

        void EQIDBind()
        {
            ActionLoad acEQID = ActionLoad.Conn();
            acEQID.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acEQID.Sql = "BindEQIDInfo";
            acEQID.Add("CHOSCODE", His.his.Choscode);
            acEQID.ServiceLoad += new YtClient.data.events.LoadEventHandle(acEQID_ServiceLoad);
            acEQID.Post();
        }

        void acEQID_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            EQIDInfo = e.Msg.GetDataTable();
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.EqIdName_Column).Items.Clear();
            if (EQIDInfo != null)
            {
                foreach (DataRow r in EQIDInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
                tv.Bind(this.EqIdName_Column);
            }
        }

        #endregion


        #region  查询   [过滤]
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.selTextInpt1.Value == null || this.selTextInpt1.Value.Trim() == "")
            {
                WJs.alert("请选择一个库房信息！");
                selTextInpt1.Focus();
                return;
            }
            if (this.Status_ytComboBox.SelectedIndex < 0)
            {
                WJs.alert("请选择状态信息！");
                return;
            }

            //  TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").add("所有","10").Bind(this.Status_ytComboBox);
            SqlStr sqls = SqlStr.newSql();
            if (this.Status_ytComboBox.SelectedIndex == 0)
            {
                sqls.Add(" AND STATUS=0 ");
            }
            if (this.Status_ytComboBox.SelectedIndex == 1)
            {
                sqls.Add(" AND STATUS=1 ");
            }
            if (this.Status_ytComboBox.SelectedIndex == 2)
            {
                sqls.Add(" AND STATUS=2 ");
            }
            if (this.Status_ytComboBox.SelectedIndex == 3)
            {
                sqls.Add(" AND STATUS=6 ");
            }
            if (this.Status_ytComboBox.SelectedIndex == 4)
            {
                sqls.Add(" AND STATUS=7 ");
            }
            else
            {
                sqls.Add(" AND  1=1 ");
            }
            if (this.dateTimePicker1.Value.CompareTo(this.dateTimePicker2.Value) > 0)
            {
                WJs.alert("起始日期必须小于末尾日期！");
                return;
            }
            this.dataGView1.reLoad(new object[] { His.his.Choscode, this.selTextInpt1.Value, this.dateTimePicker1.Value, this.dateTimePicker2.Value }, sqls);
            this.TiaoSu.Text = this.dataGView1.RowCount.ToString() + "笔";
            this.JinEHeJi.Text = this.dataGView1.Sum("总金额").ToString() + "元";

        }

        #endregion

        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            if (this.dataGView1.CurrentRow == null)
            {
                WJs.alert("请在主表中选择要浏览的行");
                return;
            }
            else
            {
                Dictionary<string, ObjItem> drmain = this.dataGView1.getRowData();
                EQDiaoBoEdit form = new EQDiaoBoEdit(drmain, 0);//这里的0是代表“浏览”
                form.ShowDialog();
            }
        }
        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }



        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drmain = this.dataGView1.getRowData();
            if (drmain != null)
            {
                if (drmain["状态"].ToString() == "1" || drmain["状态"].ToString() == "2")
                {
                    EQDiaoBoEdit form = new EQDiaoBoEdit(drmain, 1);//这里的1是代表“编辑”
                    form.ShowDialog();
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("只能对等待审核与审核被拒的调拨信息进行编辑！");
                }
            }
            else
            {
                WJs.alert("请选择要编辑的调拨信息");
                return;
            }
        }


        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            EQDiaoBoEdit form = new EQDiaoBoEdit(null, 2);
            form.ShowDialog();
            if (selTextInpt1.Text != "" || selTextInpt1.Value != null)
            {
                refresh_toolStrip_Click(null, null);
            }
        }

        //刷新
        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            this.Status_ytComboBox.SelectedIndex = 1;
            button1_Click(null, null);
            this.dataGView1.setFocus(0, 1);
        }




        //审核======================================还未写完==================================已完
        private void Submited_toolStrip_Click(object sender, EventArgs e)
        {
            this.dataGView2.IsAutoAddRow = false;
            //对处于等待审核状态（即“出库库房=当前所选库房 and 状态=1 and 操作标志=1”）的调拨数据进行审核操作，
            //如果审核不通过，则更改状态为2（审核被拒）；如审核通过，则更改状态为6（已审核）
            Dictionary<string, ObjItem> drZhuBiao = this.dataGView1.getRowData();
            TargetWareDeptid = drZhuBiao["出库目的科室ID"].ToString();//由于很多地方用到了目的科室，我这里就格外提出          

            ActionLoad ACSendOutId = ActionLoad.Conn();
            ACSendOutId.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            ACSendOutId.Sql = "SendOutId";
            ACSendOutId.Add("OUTID", drZhuBiao["出库ID"].ToString());
            ACSendOutId.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ACSendOutId.Post();

            if (drZhuBiao != null)
            {
                if (drZhuBiao["状态"].ToString() == "1")
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                    ac.Sql = "ShenHeOutEQInfo";
                    ac.Add("OUTID", drZhuBiao["出库ID"].ToString());
                    ac.Add("CHOSCODE", drZhuBiao["医疗机构编码"].ToString());
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("SHUSERID", His.his.UserId.ToString());//审核人员即当前的操作人员
                    ac.Add("SHUSERNAME", His.his.UserName.ToString());

                    DialogResult diaResl = MessageBox.Show("经过您的审核，是否通过？", "消息提示框", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (diaResl == DialogResult.Yes)
                    {
                        //下面是针对其他数据表的操作及其更新                   

                        List<Dictionary<string, ObjItem>> drXiBiao = this.dataGView2.GetData();
                        ShenHeCaoZuo(drZhuBiao, drXiBiao);

                        ac.Add("STATUS", "6");  //已审核通过
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else if (diaResl == DialogResult.No)
                    {
                        ac.Add("STATUS", "2");  //审核被拒
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                    }
                    else
                    {
                        return;
                    }
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("只能对处于等待审核的设备调拨记录进行审核操作！");
                }
            }
            else
            {
                WJs.alert("请选择要审核的设备调拨记录！");
            }
        }

        void ac_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                WJs.alert(e.Msg.Msg);
            }
        }

        private void ShenHeCaoZuo(Dictionary<string, ObjItem> drZhuBiao, List<Dictionary<string, ObjItem>> drXiBiao)
        {
            //这里的出库数据 相对应与  入库数据  再修改库存总表和流水表的数据
            //联系 ： EQOUTMAIN :WARECODE ---- EQSTOCK:WARECODE   EQOUTDETAIL:STOCKFLOWNO----EQSTOCKDETAIL:FLOWNO
            //        EQOUTMAIN:INID OPFLAG IOID(IOFLAG=1) ---- EQINMAIN: INID OPFLAG
            if (drXiBiao != null && drXiBiao.Count > 0)
            {
                //1 入库主表的插入    
                ActionLoad ac1 = ActionLoad.Conn();
                ac1.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                ac1.Sql = "InsertInMain";
                AddRuKuZhuBiaoInfo(ac1, drZhuBiao);
                ac1.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac1.Post();
                if (!isOk)
                { return; }

                //这里是根据EQID 和WARECODE 来更新总表  比在内部循环的次数要少
                //select EQID, sum(Num) AS 数量 from lkeq.eqoutdetail  where outid=? and CHOSCODE=?  group by eqid;
                DataTable XiGroupbyEQIdTB = LData.LoadDataTable("GetEQIdNumGroupBy_EQDiaoBoManag", null, new object[] { drZhuBiao["出库ID"].ToString(), His.his.Choscode });
                if (XiGroupbyEQIdTB != null && XiGroupbyEQIdTB.Rows.Count != 0)
                {
                    foreach (DataRow item in XiGroupbyEQIdTB.Rows)
                    {
                        //UPDATE LKEQ.EQSTOCK SET NUM=(NUM-?) WHERE WARECODE=? AND EQID=? AND CHOSCODE=? 
                        LData.Exe("UpdataEQStockNumInfo_EQDiaoBoManag", null, new object[] { Convert.ToInt32(item["数量"]), drZhuBiao["出库库房"].ToString(), item["EQID"].ToString(), His.his.Choscode });
                    }
                }

                //就是主表某条信息   对应细表的全部数据
                foreach (Dictionary<string, ObjItem> drXi in drXiBiao)
                {
                    //防止自动增加行
                    if (drXi["流水号"].IsNull == true || drXi["库存流水号"].IsNull == true || drXi["流水号"].ToString().Equals("") || drXi["库存流水号"].ToString().Equals(""))
                    {
                        continue;
                    }

                    //2 更新出库库房数据   UPDATE LKEQ.EQSTOCKDETAIL SET OUTNUM=(OUTNUM+?) WHERE FLOWNO=? AND CHOSCODE=?   
                    LData.Exe("UpdateEQStockDetailNum", null, new object[] { Convert.ToInt32(drXi["数量"].ToString()), drXi["库存流水号"].ToString(), His.his.Choscode });

                    //3 更新或插入信息至库存主表  [更新出库目的库房]
                    ActionLoad acStockMain = ActionLoad.Conn();
                    acStockMain.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                    acStockMain.Sql = "InsertStockMain";
                    AddEQStockMainInfo(acStockMain, drXi, drZhuBiao);
                    if (!ifHaveExsit)  //不存在才需插入，存在则已经更新完毕
                    {
                        acStockMain.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        acStockMain.Post();
                    }
                    if (!isOk)
                    { return; }

                    //4   库存流水表  [直接新建的库存流水入库单，所以不存在更新数据]
                    ActionLoad acStockDetail = ActionLoad.Conn();
                    acStockDetail.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                    acStockDetail.Sql = "InsertStockDetail";
                    AddEQStockDetailInfo(acStockDetail, drXi, drZhuBiao);
                    acStockDetail.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    acStockDetail.Post();
                    if (!isOk)
                    { return; }

                    //5  入库细表
                    ActionLoad acInDetail = ActionLoad.Conn();
                    acInDetail.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                    acInDetail.Sql = "InsertEQInDetail";
                    AddRuKuXiBiaoInfo(acInDetail, drXi, drZhuBiao);
                    acInDetail.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    acInDetail.Post();
                    if (!isOk)
                    { return; }
                }
                if (isOk)
                {
                    WJs.alert("设备调拨信息审核成功！");
                    refresh_toolStrip_Click(null, null);
                }
            }
            else
            {
                WJs.alert("出库细表无任何内容，不会生成任何库存记录！");
                return;
            }
        }


        //inid, ioid, warecode, deptid, recipecode, shdh, supplyid, supplyname, totalmoney, invoicecode, 
        //invoicedate, invoicemoney, othermoney, indate, buyuser, status, memo, opflag, userid, username,
        //recdate, shdate, shuserid, shusername, cxdate, cxuserid, cxusername, choscode  28

        //入库主表
        private void AddRuKuZhuBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> drZhuBiao)
        {
            ac.Add("INID", "");//ID后面自动生成
            ac.Add("IOID", IOID);
            ac.Add("OPFLAG", 1);//调拨入库主表OPFLAG肯定是1
            ac.Add("WARECODE", drZhuBiao["出库目的库房"].ToString());
            ac.Add("DEPTID", TargetWareDeptid);
            ac.Add("TOTALMONEY", this.dataGView2.Sum("成本金额").ToString());
            ac.Add("INVOICEMONEY", this.dataGView2.Sum("金额").ToString());
            ac.Add("OTHERMONEY", this.dataGView2.Sum("运杂费").ToString());
            ac.Add("RECIPECODE", "");//服务端生成
            ac.Add("SHDH", "");
            ac.Add("INVOICECODE", "");
            ac.Add("INVOICEDATE", "");
            ac.Add("BUYUSER", "");
            ac.Add("MEMO", drZhuBiao["备注"].ToString());
            ac.Add("SUPPLYID", "");
            ac.Add("SUPPLYNAME", "");
            ac.Add("INDATE", DateTime.Now);//?
            ac.Add("STATUS", "6");
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName);
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("SHDATE", DateTime.Now);
            ac.Add("SHUSERID", His.his.UserId.ToString());
            ac.Add("SHUSERNAME", His.his.UserName);
            ac.Add("CXDATE", "");
            ac.Add("CXUSERID", "");
            ac.Add("CXUSERNAME", "");
        }

        //detailno, inid, eqid, unitcode, gg, xh, cd, num, price, money, othermoney, totalprice, totalmoney,
        //supplyid, supplyname, productdate, validdate, memo, txm, choscode, stockflowno 21

        //入库细表
        private void AddRuKuXiBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> drX, Dictionary<string, ObjItem> drZ)
        {
            // string TransINID = LData.Es("FindINIDForStockDetail", null, new object[] { drZ["出库ID"].ToString(), drZ["医疗机构编码"].ToString() });

            ac.Add("DETAILNO", "");//服务端生成
            ac.Add("INID", "");//服务端获取
            ac.Add("EQID", drX["设备ID"].ToString());
            ac.Add("UNITCODE", drX["单位编码"].ToString());
            ac.Add("GG", drX["规格"].ToString());
            ac.Add("XH", drX["型号"].ToString());
            ac.Add("CD", drX["产地"].ToString());
            ac.Add("NUM", drX["数量"].ToString());
            ac.Add("PRICE", drX["单价"].ToString());
            ac.Add("MONEY", drX["金额"].ToString());
            ac.Add("OTHERMONEY", drX["运杂费"].ToString());
            ac.Add("TOTALPRICE", drX["成本单价"].ToString());
            ac.Add("TOTALMONEY", drX["成本金额"].ToString());
            ac.Add("PRODUCTDATE", drX["生产日期"].ToDateTime());
            ac.Add("VALIDATE", drX["有效期"].ToDateTime());
            ac.Add("CHOSCODE", drX["医疗机构编码"].ToString());
            ac.Add("MEMO", drX["备注"].ToString());
            ac.Add("SUPPLYID", drX["生产厂家ID"].ToString());
            ac.Add("SUPPLYNAME", drX["生产厂家名称"].ToString());
            ac.Add("TXM", drX["条形码"].ToString());
            ac.Add("STOCKFLOWNO", "");
        }

        //stockid, warecode, deptid, eqid, num, unitcode, memo, choscode
        //库存主表  针对每一条入库细表信息，分别做一次判断 EQID 8    这里的drZ仍是指出库主表
        private void AddEQStockMainInfo(ActionLoad ac, Dictionary<string, ObjItem> drX, Dictionary<string, ObjItem> drZ)
        {
            //SELECT STOCKID,NUM FROM LKEQ.EQSTOCK  WHERE EQID=? AND WARECODE=? AND CHOSCODE=?
            GetStockIdAndNum = LData.LoadDataTable("IfHaveExistInEQStock", null, new object[] { drX["设备ID"].ToString(), drZ["出库目的库房"].ToString(), His.his.Choscode });
            if (GetStockIdAndNum == null || GetStockIdAndNum.Rows.Count <= 0)
            {
                //不存在 需插入
                ifHaveExsit = false;
                ac.Add("STOCKID", "");//服务端生成
                ac.Add("WARECODE", drZ["出库目的库房"].ToString());//入库主表的入库库房编码=出库主表的目的库房编码
                ac.Add("EQID", drX["设备ID"].ToString());
                ac.Add("DEPTID", TargetWareDeptid);
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("UNITCODE", drX["单位编码"].ToString());
                ac.Add("MEMO", "");
                ac.Add("NUM", drX["数量"].ToString());
                BeforeNum = 0;
            }
            else
            {
                //存在  需更新库存数目 []
                ifHaveExsit = true;
                BeforeNum = Convert.ToInt32(GetStockIdAndNum.Rows[0][1].ToString());//出库目的库房的库存总表更新
                LData.Exe("UpdateEQStockNumForTarget", null, new object[] { Convert.ToInt32(drX["数量"].ToString()), drX["设备ID"].ToString(), drZ["出库目的库房"].ToString(), His.his.Choscode.ToString() });
            }
        }


        //flowno, inid, warecode, deptid, eqid, stockid, num, beforenum, unitcode, outnum, cardnum, gg, 
        //xh, cd,price, money, othermoney, totalprice, totalmoney, supplyid, supplyname, productdate,
        //validdate, memo, txm, recipecode, shdh, ghsupplyid, ghsupplyname, indate, choscode  31
        //库存流水   出库库房对应库存流水号   
        private void AddEQStockDetailInfo(ActionLoad ac, Dictionary<string, ObjItem> drX, Dictionary<string, ObjItem> drZ)
        {
            //之前便已经更新完INID  从数据库获取

            ac.Add("FLOWNO", "");//服务端生成

            string InIdForStockDetail = LData.Es("FindINIDForStockDetail", null, new object[] { drZ["出库ID"].ToString(), drZ["医疗机构编码"].ToString() });
            ac.Add("INID", InIdForStockDetail);//对应入库主表主键

            ac.Add("WARECODE", drZ["出库目的库房"].ToString());//入库主表的入库库房编码=出库主表的目的库房编码
            ac.Add("EQID", drX["设备ID"].ToString());
            ac.Add("DEPTID", TargetWareDeptid);

            if (ifHaveExsit)
            {
                ac.Add("STOCKID", GetStockIdAndNum.Rows[0][0].ToString());//对应库存主键  
            }
            else
            {
                ac.Add("STOCKID", "");
            }
            ac.Add("NUM", drX["数量"].ToInt());
            ac.Add("BEFORENUM", BeforeNum);
            ac.Add("UNITCODE", drX["单位编码"].ToString());
            ac.Add("OUTNUM", 0);
            ac.Add("CARDNUM", 0);

            ac.Add("GG", drX["规格"].ToString());
            ac.Add("XH", drX["型号"].ToString());
            ac.Add("CD", drX["产地"].ToString());
            ac.Add("PRICE", drX["单价"].ToString());
            ac.Add("MONEY", drX["金额"].ToString());
            ac.Add("OTHERMONEY", drX["运杂费"].ToString());
            ac.Add("TOTALPRICE", drX["成本单价"].ToString());
            ac.Add("TOTALMONEY", drX["成本金额"].ToString());
            ac.Add("PRODUCTDATE", drX["生产日期"].ToDateTime());
            ac.Add("VALIDDATE", drX["有效期"].ToDateTime());
            ac.Add("MEMO", drX["备注"].ToString());
            ac.Add("SUPPLYID", drX["生产厂家ID"].ToString());
            ac.Add("SUPPLYNAME", drX["生产厂家名称"].ToString());
            ac.Add("TXM", drX["条形码"].ToString());

            ac.Add("RECIPECODE", "");

            ac.Add("SHDH", "");
            ac.Add("GHSUPPLYID", "");
            ac.Add("GHSUPPLYNAME", "");
            ac.Add("INDATE", DateTime.Now);
            ac.Add("CHOSCODE", drX["医疗机构编码"].ToString());

        }

        private void SubmitCheck_toolStrip_Click(object sender, EventArgs e)
        {
            //对状态为审核被拒（即“状态=2”）的出库数据进行提交审核操作，更改状态为1即可。
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "2")
                {
                    if (WJs.confirm("您确定要将该条审核被拒的数据提交审核吗？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                        ac.Sql = "SubmitShenHeOrDel";
                        ac.Add("OUTID", dr["出库ID"].ToString());
                        ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                        ac.Add("STATUS", "1");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        if (isOk)
                        {
                            WJs.alert("更改设备调拨状态信息成功！");
                        }
                        refresh_toolStrip_Click(null, null);
                    }
                }
                else
                {
                    WJs.alert("只能对处于审核被拒状态的设备调拨记录进行提交审核操作");
                }
            }
            else
            {
                WJs.alert("请选择要提交审核的设备调拨记录！");
            }
        }

        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            //	删除：不能物理删除记录；只能删除状态为1或2的调拨数据；只需要将要删除的出库主表的状态设置为0即可。
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1" || dr["状态"].ToString() == "2")
                {
                    if (WJs.confirm("您确定要将该条设备调拨记录作废？"))
                    {
                        ActionLoad ac = ActionLoad.Conn();
                        ac.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                        ac.Sql = "SubmitShenHeOrDel";
                        ac.Add("OUTID", dr["出库ID"].ToString());
                        ac.Add("CHOSCODE", dr["医疗机构编码"].ToString());
                        ac.Add("STATUS", "0");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        if (isOk)
                        {
                            WJs.alert("更改设备调拨状态信息成功！");
                        }
                        refresh_toolStrip_Click(null, null);
                    }
                }
                else
                {
                    WJs.alert("只能对处于等待审核以及审核被拒状态的设备调拨记录进行删除操作！");
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备调拨记录！");
            }
        }

        //每切换一次主表信息，细表自动显示内容  [由于前面在做新增和删除引用了对应的细表数据，所有需求的变动只能隐藏掉细表而不能删除]
        private void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                this.dataGView2.reLoad(new object[] { dr["出库ID"].ToString(), His.his.Choscode.ToString() });
            }
            else
            {
                this.dataGView2.reLoad(null);
            }
        }

        private void Chongxiao_toolStrip_Click(object sender, EventArgs e)
        {
            dataGView2.IsAutoAddRow = false;
            Dictionary<string, ObjItem> drZhu = this.dataGView1.getRowData();
            if (drZhu != null)
            {
                //CXDATE CXUSERID CXUSERNAME
                if (drZhu["状态"].ToString() == "6")
                {
                    if (WJs.confirm("确认对已审核的数据进行冲销操作？"))
                    {
                        //更改状态   是不是可以同时适应出库和入库呢？  
                        ActionLoad acChongXiao = ActionLoad.Conn();
                        acChongXiao.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                        acChongXiao.Sql = "ChongXiaoRuChu";
                        acChongXiao.Add("STATUS", "0");
                        acChongXiao.Add("CXUSERNAME", His.his.UserName.ToString());
                        acChongXiao.Add("CXUSERID", His.his.UserId.ToString());
                        acChongXiao.Add("CXDATE", DateTime.Now);
                        acChongXiao.Add("OUTID", drZhu["出库ID"].ToString());
                        acChongXiao.Add("CHOSCODE", drZhu["医疗机构编码"].ToString());
                        acChongXiao.ServiceLoad += new YtClient.data.events.LoadEventHandle(acChongXiao_ServiceLoad);
                        acChongXiao.Post();
                        //执行冲销操作
                        List<Dictionary<string, ObjItem>> drXiBiaoList = this.dataGView2.GetData();
                        ChongXiaoCaoZuo(drZhu, drXiBiaoList);
                    }
                    if (isOK2)
                    {
                        WJs.alert("冲销该条调拨记录成功！");
                        refresh_toolStrip_Click(null, null);
                    }
                }
                else
                {
                    WJs.alert("只能对状态为已审核的数据进行冲销操作！");
                }
            }
            else
            {
                WJs.alert("请选择需要进行冲销操作的调拨信息！");
            }
        }

        void acChongXiao_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK2 = true;
            }
            else
            {
                isOK2 = false;
                WJs.alert(e.Msg.Msg);
            }
        }

        private void ChongXiaoCaoZuo(Dictionary<string, ObjItem> drZhu, List<Dictionary<string, ObjItem>> drXibiaoList)
        {
            //6个表的数据需要进行更新 
            //我的意思是： 主表首先直接冲销  然后循环细表进行冲销
            //出库主表里面的INID 找到 入库主表信息  读取完了之后 修改重新插入冲销数据
            //由对应的INID 找到 对应的入库细表数据  读取完了之后 修改重新插入冲销数据

            //1,生成对应的出库主表  状态为7 
            ActionLoad acChongXiaoZhuChuKu = ActionLoad.Conn();
            acChongXiaoZhuChuKu.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acChongXiaoZhuChuKu.Sql = "ChongXiaoZhuChuKu";
            ChongXiaoCKZhuBiao(acChongXiaoZhuChuKu, drZhu);
            acChongXiaoZhuChuKu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acChongXiao_ServiceLoad);
            acChongXiaoZhuChuKu.Post();
            if (!isOK2)
            { return; }


            //这里是根据EQID 和WARECODE 来更新总表  比在内部循环的次数要少
            //select EQID, sum(Num) AS 数量 from lkeq.eqoutdetail  where outid=? and CHOSCODE=?  group by eqid;
            DataTable XiGroupbyEQIdTB = LData.LoadDataTable("GetEQIdNumGroupBy_EQDiaoBoManag", null, new object[] { drZhu["出库ID"].ToString(), His.his.Choscode });
            if (XiGroupbyEQIdTB != null && XiGroupbyEQIdTB.Rows.Count != 0)
            {
                foreach (DataRow item in XiGroupbyEQIdTB.Rows)
                {
                    //UPDATE LKEQ.EQSTOCK SET NUM=(NUM-?) WHERE WARECODE=? AND EQID=? AND CHOSCODE=? 
                    LData.Exe("UpdataEQStockNumInfo_EQDiaoBoManag", null, new object[] { Convert.ToInt32(item["数量"]) * (-1), drZhu["出库库房"].ToString(), item["EQID"].ToString(), His.his.Choscode });
                }
            }

            // 1.2 出库细表 
            foreach (Dictionary<string, ObjItem> drXiBiao in drXibiaoList)
            {
                if (drXiBiao["出库ID"] == null || drXiBiao["出库ID"].ToString() == "")
                {
                    // WJs.alert("该条出库细表内无数据！");  直接跳过，不要提示，体验性不好
                    continue;
                }
                ActionLoad acChongXiaoXiChuKu = ActionLoad.Conn();
                acChongXiaoXiChuKu.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                acChongXiaoXiChuKu.Sql = "ChongXiaoXiChuKu";
                ChongXiaoCKXiBiao(acChongXiaoXiChuKu, drXiBiao);
                acChongXiaoXiChuKu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acChongXiao_ServiceLoad);
                acChongXiaoXiChuKu.Post();
                //对 出库库房 和对应的设备数量进行更新   [单条数据，细表对应的库存流水号， 对应一个STOCKID]    UPDATE LKEQ.EQSTOCKDETAIL SET OUTNUM=(OUTNUM+?) WHERE FLOWNO=? AND CHOSCODE=?
                LData.Exe("UpdateEQStockDetailNum", null, new object[] { drXiBiao["数量"].ToInt() * (-1), drXiBiao["库存流水号"].ToString(), His.his.Choscode });
            }
            if (!isOK2)
            { return; }

            //2,生成对应的入库主表  状态为7  
            ActionLoad acChongXiaoZhuRuKu = ActionLoad.Conn();
            acChongXiaoZhuRuKu.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acChongXiaoZhuRuKu.Sql = "ChongXiaoZhuRuKu";
            //由原始INID 获取对应的 入库主表和细表的内容   根据这些内容生成冲销操作所需要的数据  冲销的时候INID已经存在
            DataTable dt = LData.LoadDataTable("FindINMainInfo", null, new object[] { His.his.Choscode, drZhu["对应入库ID"].ToString() });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];//要冲销的调拨数据对应的入库主表信息
                ChongXiaoRKZhuBiao(acChongXiaoZhuRuKu, dr);
                acChongXiaoZhuRuKu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acChongXiao_ServiceLoad);
                acChongXiaoZhuRuKu.Post();
            }
            else
            {
                WJs.alert("入库主表信息出错，冲销操作被终止！");
                isOK2 = false;
                return;
            }
            if (!isOK2)
            { return; }

            //select EQID, sum(Num) AS 数量 from lkeq.eqindetail  where inid=? and CHOSCODE=?  group by eqid
            DataTable DTNumForEQStockCX = LData.LoadDataTable("GetStockNumInInDetail", null, new object[] { drZhu["对应入库ID"].ToString(), His.his.Choscode });
            if (DTNumForEQStockCX != null)
            {
                foreach (DataRow itemRow in DTNumForEQStockCX.Rows)
                {
                    //UPDATE LKEQ.EQSTOCK SET NUM=(NUM+?) WHERE EQID=? AND WARECODE=? AND CHOSCODE=?
                    LData.Exe("UpdateEQStockNumForTarget", null, new object[] { Convert.ToInt32(itemRow["数量"].ToString()) * (-1), itemRow["EQID"].ToString(), drZhu["出库目的库房"].ToString(), His.his.Choscode });
                }
            }

            //2.2   入库细表  
            DataTable dtDetail = LData.LoadDataTable("FindInDetailInfo", null, new object[] { His.his.Choscode.ToString(), drZhu["对应入库ID"].ToString() });
            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetail.Rows)
                {
                    ActionLoad acChongXiaoXiRuKu = ActionLoad.Conn();
                    acChongXiaoXiRuKu.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
                    acChongXiaoXiRuKu.Sql = "ChongXiaoXiRuKu";
                    ChongXiaoRKXiBiao(acChongXiaoXiRuKu, dr);
                    acChongXiaoXiRuKu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acChongXiao_ServiceLoad);
                    acChongXiaoXiRuKu.Post();
                    LData.Exe("UpdateEQStockDetailNum_InBiao", null, new object[] { Convert.ToInt32(dr["NUM"]) * (-1), dr["STOCKFLOWNO"].ToString(), His.his.Choscode });
                }
            }
            if (!isOK2)
            { return; }
        }
        //入库主表
        private void ChongXiaoRKZhuBiao(ActionLoad acChongXiaoZhuRuKu, DataRow dr)
        {
            acChongXiaoZhuRuKu.Add("INID", null);//服务端生成  针对原本的冲销ID
            acChongXiaoZhuRuKu.Add("IOID", dr["IOID"].ToString());
            acChongXiaoZhuRuKu.Add("WARECODE", dr["WARECODE"].ToString());
            acChongXiaoZhuRuKu.Add("DEPTID", dr["DEPTID"].ToString());
            acChongXiaoZhuRuKu.Add("RECIPECODE", dr["RECIPECODE"].ToString());
            acChongXiaoZhuRuKu.Add("SHDH", dr["SHDH"].ToString());
            acChongXiaoZhuRuKu.Add("SUPPLYID", dr["SUPPLYID"].ToString());
            acChongXiaoZhuRuKu.Add("SUPPLYNAME", dr["SUPPLYNAME"].ToString());
            acChongXiaoZhuRuKu.Add("TOTALMONEY", (Convert.ToDouble(dr["TOTALMONEY"]) * (-1)).ToString());
            acChongXiaoZhuRuKu.Add("INVOICECODE", dr["INVOICECODE"].ToString());

            DateTime Invoicedate;
            DateTime.TryParse(dr["INVOICEDATE"].ToString(), out Invoicedate);
            acChongXiaoZhuRuKu.Add("INVOICEDATE", Invoicedate);

            acChongXiaoZhuRuKu.Add("INVOICEMONEY", (Convert.ToDouble(dr["INVOICEMONEY"]) * (-1)).ToString());
            acChongXiaoZhuRuKu.Add("OTHERMONEY", (Convert.ToDouble(dr["OTHERMONEY"]) * (-1)).ToString());

            DateTime Indate;
            DateTime.TryParse(dr["INDATE"].ToString(), out Indate);
            acChongXiaoZhuRuKu.Add("INDATE", Indate);

            acChongXiaoZhuRuKu.Add("BUYUSER", dr["BUYUSER"].ToString());
            acChongXiaoZhuRuKu.Add("STATUS", "7");
            acChongXiaoZhuRuKu.Add("MEMO", dr["MEMO"].ToString());
            acChongXiaoZhuRuKu.Add("OPFLAG", dr["OPFLAG"].ToString());
            acChongXiaoZhuRuKu.Add("USERID", dr["USERID"].ToString());
            acChongXiaoZhuRuKu.Add("USERNAME", dr["USERNAME"].ToString());

            DateTime Recdate;
            DateTime.TryParse(dr["RECDATE"].ToString(), out Recdate);
            acChongXiaoZhuRuKu.Add("RECDATE", Recdate);

            DateTime Shdate;
            DateTime.TryParse(dr["SHDATE"].ToString(), out Shdate);
            acChongXiaoZhuRuKu.Add("SHDATE", Shdate);

            acChongXiaoZhuRuKu.Add("SHUSERID", dr["SHUSERID"].ToString());
            acChongXiaoZhuRuKu.Add("SHUSERNAME", dr["SHUSERNAME"].ToString());

            DateTime Cxdate;
            DateTime.TryParse(dr["CXDATE"].ToString(), out Cxdate);
            acChongXiaoZhuRuKu.Add("CXDATE", Cxdate);

            acChongXiaoZhuRuKu.Add("CXUSERID", dr["CXUSERID"].ToString());
            acChongXiaoZhuRuKu.Add("CXUSERNAME", dr["CXUSERNAME"].ToString());
            acChongXiaoZhuRuKu.Add("CHOSCODE", dr["CHOSCODE"].ToString());

        }
        //入库细表
        private void ChongXiaoRKXiBiao(ActionLoad ac, DataRow drX)
        {
            ac.Add("DETAILNO", null);//服务端生成
            ac.Add("INID", null);
            ac.Add("EQID", drX["EQID"].ToString());
            ac.Add("UNITCODE", drX["UNITCODE"].ToString());
            ac.Add("GG", drX["GG"].ToString());
            ac.Add("XH", drX["XH"].ToString());
            ac.Add("CD", drX["CD"].ToString());
            ac.Add("NUM", Convert.ToInt32(drX["NUM"]) * (-1));
            ac.Add("PRICE", drX["PRICE"].ToString());
            ac.Add("MONEY", (Convert.ToDouble(drX["MONEY"]) * (-1)).ToString());
            ac.Add("OTHERMONEY", drX["OTHERMONEY"].ToString());
            ac.Add("TOTALPRICE", drX["TOTALPRICE"].ToString());
            ac.Add("TOTALMONEY", (Convert.ToDouble(drX["TOTALMONEY"]) * (-1)).ToString());

            DateTime Productdate;
            DateTime.TryParse(drX["PRODUCTDATE"].ToString(), out Productdate);
            ac.Add("PRODUCTDATE", Productdate);

            DateTime Validdate;
            DateTime.TryParse(drX["VALIDDATE"].ToString(), out Validdate);
            ac.Add("VALIDATE", Validdate);

            ac.Add("CHOSCODE", drX["CHOSCODE"].ToString());
            ac.Add("MEMO", drX["MEMO"].ToString());
            ac.Add("SUPPLYID", drX["SUPPLYID"].ToString());
            ac.Add("SUPPLYNAME", drX["SUPPLYNAME"].ToString());
            ac.Add("TXM", drX["TXM"].ToString());
            ac.Add("STOCKFLOWNO", drX["STOCKFLOWNO"].ToString());
        }
        //出库主表
        private void ChongXiaoCKZhuBiao(ActionLoad ac, Dictionary<string, ObjItem> drZhu)
        {
            //主表 22 主键为自动生成
            ac.Add("OUTID", null);//服务端生成
            ac.Add("IOID", drZhu["出库方式"].ToString());
            ac.Add("RECIPECODE", drZhu["单据号"].ToString());
            ac.Add("WARECODE", drZhu["出库库房"].ToString());
            ac.Add("TARGETWARECODE", drZhu["出库目的库房"].ToString());

            //ac.Add("DEPTID", LData.Es("FindDPTInEQInMain", null, new object[] { drZ["出库目的库房"].ToString(), His.his.Choscode.ToString() }));
            //ac.Add("TARGETDEPTID", LData.Es("FindDPTInEQInMain", null, new object[] { drZhu["出库目的库房"].ToString(), His.his.Choscode.ToString() }));  //在服务端查询后再插入

            ac.Add("TARGETDEPTID", TargetWareDeptid);
            ac.Add("TOTALMONEY", (drZhu["总金额"].ToDouble() * (-1)).ToString());
            ac.Add("OUTDATE", drZhu["制单日期"].ToDateTime());
            ac.Add("STATUS", "7");//状态全部为已冲销
            ac.Add("MEMO", drZhu["备注"].ToString());
            ac.Add("OPFLAG", drZhu["操作标志"].ToString());
            ac.Add("RECDATE", drZhu["修改时间"].ToDateTime());
            ac.Add("SHDATE", drZhu["审核日期"].ToDateTime());
            ac.Add("SHUSERID", drZhu["审核操作员ID"].ToString());
            ac.Add("SHUSERNAME", drZhu["审核操作员姓名"].ToString());
            DataTable dt = LData.LoadDataTable("FindChongXiaoYuanChuKuInfo", null, new object[] { His.his.Choscode.ToString(), drZhu["出库ID"].ToString() });
            DataRow dr = dt.Rows[0];
            ac.Add("CXDATE", Convert.ToDateTime(dr["CXDATE"]));
            ac.Add("CXUSERID", dr["CXUSERID"].ToString());
            ac.Add("CXUSERNAME", dr["CXUSERNAME"].ToString());
            ac.Add("USERNAME", drZhu["操作员姓名"].ToString());
            ac.Add("USERID", drZhu["操作员ID"].ToString());
            ac.Add("INID", "");//由最新生成的更新
            ac.Add("CHOSCODE", drZhu["医疗机构编码"].ToString());
        }
        //出库细表
        private void ChongXiaoCKXiBiao(ActionLoad ac, Dictionary<string, ObjItem> drXiBiao)
        {
            //细表 21   直接读取获得
            ac.Add("DETAILNO", null);//服务端生成
            ac.Add("OUTID", null);//获取最新生成
            ac.Add("EQID", drXiBiao["设备ID"].ToString());

            ac.Add("NUM", drXiBiao["数量"].ToInt() * (-1));//负数  需要更新数量

            ac.Add("UNITCODE", drXiBiao["单位编码"].ToString());
            ac.Add("PRICE", drXiBiao["单价"].ToDouble() * (-1));
            ac.Add("MONEY", drXiBiao["金额"].ToDouble() * (-1));//负数
            ac.Add("GG", drXiBiao["规格"].ToString());
            ac.Add("XH", drXiBiao["型号"].ToString());
            ac.Add("CD", drXiBiao["产地"].ToString());
            ac.Add("OTHERMONEY", drXiBiao["运杂费"].ToDouble() * (-1));
            ac.Add("TOTALPRICE", drXiBiao["成本单价"].ToDouble() * (-1));
            ac.Add("TOTALMONEY", drXiBiao["成本金额"].ToDecimal() * (-1));//负数
            ac.Add("TXM", drXiBiao["条形码"].ToString());
            ac.Add("STOCKFLOWNO", drXiBiao["库存流水号"].ToString());
            ac.Add("SUPPLYID", drXiBiao["生产厂家ID"].ToString());
            ac.Add("SUPPLYNAME", drXiBiao["生产厂家名称"].ToString());
            ac.Add("PRODUCTDATE", drXiBiao["生产日期"].ToDateTime());
            ac.Add("VALIDDATE", drXiBiao["有效期"].ToDateTime());
            ac.Add("MEMO", drXiBiao["备注"].ToString());
            ac.Add("CHOSCODE", drXiBiao["医疗机构编码"].ToString());
        }
    }
}
