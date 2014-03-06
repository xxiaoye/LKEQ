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
using YtWinContrl.com.datagrid;
using ChSys;
using YtUtil.tool;
using YiTian.db;
using EQWareManag.form;

namespace EQWareManag
{
    public partial class EQLingUse : Form, IPlug
    {
        bool isOk;
        bool isOK2;//作为冲销
        DataTable GetStockIdAndNum;//库存总表ID号和数目
        bool ifHaveExsit;//设备ID是否在库存总表当中存在
        int BeforeNum;//根据库存ID，获取入库之前的库存数量；如果库存总表是新增时，该值为0
        string IOIDInDetail;
        int JKNum = 0;
        int CXJKNum = 0;
        int IfLYBuildCard;
        bool isHaveCard;
        public EQLingUse()
        {
            InitializeComponent();
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").add("全部", "10").Bind(this.Status_ytComboBox);
            this.Status_ytComboBox.SelectedIndex = 1;

            this.selTextInpt1.Sql = "WareBindInDiaoBo";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            IOIDInDetail = LData.Es("FindIOIdInEQInMainLingYong", null, new object[] { His.his.Choscode });
            IfLYBuildCard = GetSysDanWei(2201);
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

        private void EQLingUse_Load(object sender, EventArgs e)
        {
            
            TvList.newBind().add("作废", "0").add("等待审核", "1").add("审核被拒", "2").add("已审核", "6").add("已冲销", "7").Bind(this.status_Column);
            TvList.newBind().add("普通", "0").add("调拨", "1").add("申领", "2").add("盘点", "3").Bind(this.opflag_Cloumn);
            WarecodeToWareName();
            this.dataGView1.Url = "LoadEQOutMainInfoLingYong";
            this.dataGView2.Url = "LoadEQOutDetailListLingYong";
            //dataGView1.IsPage = true;
            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
        void WarecodeToWareName()
        {
            DataTable EQWareInfo = LData.LoadDataTable("FindWareNameInEQDiaoBo", null, new object[] { His.his.Choscode });
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.ware1Cloumn).Items.Clear();
            ((DataGridViewComboBoxColumn)this.TargetWarecodeColumn).Items.Clear();

            if (EQWareInfo != null)
            {
                foreach (DataRow r in EQWareInfo.Rows)
                {
                    tv.add(r[1].ToString(), r[0].ToString());
                }
            }
            tv.Bind(TargetWarecodeColumn);
            tv.Bind(this.ware1Cloumn);

        }
        private int GetSysDanWei(int Id)
        {
            return Convert.ToInt32(LData.Es("GetSysDanWeiInEQDiaoBoEdit", null, new object[] { Id }));
        }
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
                sqls.Add(" AND a.STATUS=0 ");

            }
            if (this.Status_ytComboBox.SelectedIndex == 1)
            {
                sqls.Add(" AND a.STATUS=1 ");

            }
            if (this.Status_ytComboBox.SelectedIndex == 2)
            {
                sqls.Add(" AND a.STATUS=2 ");

            }
            if (this.Status_ytComboBox.SelectedIndex == 3)
            {
                sqls.Add(" AND a.STATUS=6 ");

            }
            if (this.Status_ytComboBox.SelectedIndex == 4)
            {
                sqls.Add(" AND a.STATUS=7 ");

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
        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            this.Status_ytComboBox.SelectedIndex = 1;
            button1_Click(null, null);
            this.dataGView1.setFocus(0, 1);
        }
        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            //只能删除状态为1或2的领用数据 更改状态为0
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1" || dr["状态"].ToString() == "2")
                {

                    if (WJs.confirm("你确定要将该条设备领用信息作废？"))
                    {
                        LData.Exe("UpdateLingYongStatus", null, new object[] { "0", DateTime.Now, His.his.Choscode.ToString(), dr["出库ID"].ToString() });
                        refresh_toolStrip_Click(null, null);
                    }
                }
                else
                {
                    WJs.alert("只能将状态为等待审核以及审核被拒的设备领用记录作废！");
                    return;
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备领用记录！");
            }
        }
        private void SubmitCheck_toolStrip_Click(object sender, EventArgs e)
        {
            //对状态为审核被拒（即“状态=2”）的出库数据进行提交审核操作，更改状态为1即可。
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "2")
                {
                    if (WJs.confirm("您确定要将该条设备领用记录重新提交审核吗？"))
                    {
                        LData.Exe("UpdateLingYongStatus", null, new object[] { "1", DateTime.Now, His.his.Choscode.ToString(), dr["出库ID"].ToString() });
                        refresh_toolStrip_Click(null, null);
                    }
                }
                else
                {
                    WJs.alert("只能对处于审核被拒状态的设备领用记录进行提交审核操作");
                }
            }
            else
            {
                WJs.alert("请选择要提交审核的设备领用记录！");
            }
        }
        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                EQLingYongEdit form = new EQLingYongEdit(dr, 0);
                form.ShowDialog();
                if (selTextInpt1.Text != "" || selTextInpt1.Value != null)
                {
                    refresh_toolStrip_Click(null, null);
                }
            }
            else
            {
                WJs.alert("请选择要浏览的设备领用记录！");
            }
        }
        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1" || dr["状态"].ToString() == "2")
                {
                    EQLingYongEdit form = new EQLingYongEdit(dr, 1);
                    form.ShowDialog();
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("只能对等待审核与审核被拒的领用信息进行编辑！");
                }
            }
            else
            {

                WJs.alert("请选择要编辑的设备领用信息！");
            }
        }
        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }
        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            EQLingYongEdit form = new EQLingYongEdit(null, 2);
            form.ShowDialog();
            if (selTextInpt1.Text != "" || selTextInpt1.Value != null)
            {
                refresh_toolStrip_Click(null, null);
            }
        }

        private void Submited_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            //对处于等待审核状态（即“出库库房=当前所选库房 and 状态=1 and 操作标志=1”）的调拨数据进行审核操作，
            //如果审核不通过，则更改状态为2（审核被拒）；如审核通过，则更改状态为6（已审核）
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1")
                {
                    ActionLoad ac = ActionLoad.Conn();
                    ac.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                    ac.Sql = "SHOutMainInfo";
                    ac.Add("OUTID", dr["出库ID"].ToString());
                    ac.Add("SHDATE", DateTime.Now);
                    ac.Add("SHUSERID", His.his.UserId.ToString());//审核人员即当前的操作人员
                    ac.Add("SHUSERNAME", His.his.UserName);
                    ac.Add("CHOSCODE", His.his.Choscode);
                    List<Dictionary<string, ObjItem>> XiList = dataGView2.GetData();
                    if (XiList == null || XiList.Count <= 0)
                    {
                        WJs.alert("该出库主表内无细表信息，无法审核！");
                        return;
                    }
                    DialogResult diaResl = MessageBox.Show("经过您的审核，是否通过？", "消息提示框", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (diaResl == DialogResult.Yes)
                    {
                        //审核过程
                        ShenHeGuoCheng(dr, XiList);
                        if (!isOk)
                        {
                            return;
                        }
                        ac.Add("STATUS", "6");
                        ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                        ac.Post();
                        if (IfLYBuildCard == 1)
                        {//领用时建卡
                            foreach (Dictionary<string, ObjItem> drXX in XiList)
                            {
                                JKNum++;
                                if (drXX["流水号"].IsNull == true || drXX["流水号"].ToString() == "")
                                {
                                    continue;
                                }
                                WJs.alert("系统设置为审核后建卡，领用单上第" + JKNum + "条数据开始建卡，即将弹出建卡窗口……");
                                AddEQBuildCard form = new AddEQBuildCard(dr, drXX, 10);
                                form.ShowDialog();
                            }
                        }
                    }
                    else if (diaResl == DialogResult.No)
                    {
                        ac.Add("STATUS", "2");
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
                    WJs.alert("只能对当前状态为等待审核的设备领用信息进行审核！");
                }
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
        private void ShenHeGuoCheng(Dictionary<string, ObjItem> dr, List<Dictionary<string, ObjItem>> XiList)
        {
            //1.1 入库主表的操作
            ActionLoad acZRK = ActionLoad.Conn();
            acZRK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
            acZRK.Sql = "InsertInMain";
            AddRuKuZhuBiaoInfo(acZRK, dr);
            acZRK.Add("OUTID", dr["出库ID"].ToString());//专为更新对应的INID
            acZRK.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            acZRK.Post();
            if (!isOk)
            { return; }

            //1.2 更新出库库房的相同设备各自数目  [库存主表]
            //select EQID, sum(Num) AS 数量 from lkeq.eqoutdetail  where outid=? and CHOSCODE=?  group by eqid    UPDATE LKEQ.EQSTOCK SET (NUM=NUM- ?) WHERE CHOSCODE=? AND WARECODE=? AND EQID=?
            //ActionLoad acDTNumForEQStock = ActionLoad.Conn();
            //DataTable DTNumForEQStock = acDTNumForEQStock.Find("GetStockNumInOutDetail", null, new object[] { dr["出库ID"].ToString(), His.his.Choscode });

            DataTable DTNumForEQStock = LData.LoadDataTable("GetStockNumInOutDetail", null, new object[] { dr["出库ID"].ToString(), His.his.Choscode });
            if (DTNumForEQStock != null)
            {
                foreach (DataRow itemRow in DTNumForEQStock.Rows)
                {
                    LData.Exe("UpdateStockNumInLingYong", null, new object[] { Convert.ToInt32(itemRow["数量"].ToString()), His.his.Choscode, dr["出库库房"].ToString(), itemRow["EQID"].ToString() });
                }
            }


            //就是主表某条信息  [出库数据] 对应细表的全部数据
            foreach (Dictionary<string, ObjItem> drXi in XiList)
            {
                if (LData.LoadDataTable("StockDetailSearchByLiuShuiHao", new object[] { drXi["库存流水号"].ToString(), His.his.Choscode.ToString() }) == null)
                {
                    continue;
                }
                //1更新出库库房数据[针对每条流水必须都更新]  UPDATE LKEQ.EQSTOCKDETAIL SET OUTNUM=(OUTNUM+?) WHERE FLOWNO=? AND CHOSCODE=?
                LData.Exe("UpdateEQStockDetailNum", null, new object[] { Convert.ToInt32(drXi["数量"].ToString()), drXi["库存流水号"].ToString(), His.his.Choscode });

                //2 先入库细表  1条[对应会生成一条库存流水]
                ActionLoad acXRK = ActionLoad.Conn();
                acXRK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                acXRK.Sql = "InsertEQInDetail";
                AddRuKuXiBiaoInfo(acXRK, drXi);
                acXRK.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                acXRK.Post();
                if (!isOk)
                { return; }

                //3 库存主表  [更新出库目的库房]   由于不存在目的库房，所以无需更新目的库房
                ActionLoad acKCZ = ActionLoad.Conn();
                acKCZ.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                acKCZ.Sql = "InsertStockMain";
                AddEQStockMainInfo(acKCZ, drXi, dr);
                if (!ifHaveExsit)  //不存在才需插入，存在则已经更新完毕
                {
                    acKCZ.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                    acKCZ.Post();
                }
                if (!isOk)
                { return; }

                //4   库存流水表
                ActionLoad acKCX = ActionLoad.Conn();
                acKCX.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                acKCX.Sql = "InsertStockDetail";
                AddEQStockDetailInfo(acKCX, drXi, dr);
                acKCX.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                acKCX.Post();
                if (!isOk)
                { return; }
            }
            if (isOk)
            {
                WJs.alert("设备调拨信息审核成功！");
                refresh_toolStrip_Click(null, null);
            }
        }

        //审核库存流水
        private void AddEQStockDetailInfo(ActionLoad ac, Dictionary<string, ObjItem> drX, Dictionary<string, ObjItem> drZ)
        {
            //之前便已经更新完INID  从数据库获取

            ac.Add("FLOWNO", "");//服务端生成
            ac.Add("INID", "");//对应入库主表主键
            ac.Add("WARECODE", null);//入库主表的入库库房编码=出库主表的目的库房编码==null
            ac.Add("EQID", drX["设备ID"].ToString());
            ac.Add("DEPTID", drZ["出库目的科室ID"].ToString());

            //是否针对新增了库存总表  
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
        //审核库存主表
        private void AddEQStockMainInfo(ActionLoad ac, Dictionary<string, ObjItem> drX, Dictionary<string, ObjItem> drZ)
        {
            //SELECT STOCKID,NUM FROM LKEQ.EQSTOCK  WHERE EQID=? AND WARECODE IS NULL AND DEPTID=? AND CHOSCODE=?
            GetStockIdAndNum = LData.LoadDataTable("IfHaveExistInEQStock_LingYong", null, new object[] { drX["设备ID"].ToString(), drZ["出库目的科室ID"].ToString(), His.his.Choscode });
            if (GetStockIdAndNum == null || GetStockIdAndNum.Rows.Count <= 0)
            {
                //不存在 需插入
                ifHaveExsit = false;
                ac.Add("STOCKID", "");//服务端生成
                ac.Add("WARECODE", null);//入库主表的入库库房编码=出库主表的目的库房编码==null
                ac.Add("EQID", drX["设备ID"].ToString());
                ac.Add("DEPTID", drZ["出库目的科室ID"].ToString());
                ac.Add("CHOSCODE", His.his.Choscode);
                ac.Add("UNITCODE", drX["单位编码"].ToString());
                ac.Add("MEMO", "");
                ac.Add("NUM", drX["数量"].ToString());
                BeforeNum = 0;
            }
            else
            {
                //存在  需更新库存数目   UPDATE LKEQ.EQSTOCK SET NUM=(NUM+?) WHERE EQID=? AND WARECODE IS NULL AND DEPTID=? AND CHOSCODE=?  针对目的科室
                ifHaveExsit = true;
                BeforeNum = Convert.ToInt32(GetStockIdAndNum.Rows[0][1].ToString());
                LData.Exe("UpdateEQStockNumForTarget_LingYong", null, new object[] { Convert.ToInt32(drX["数量"].ToString()), drX["设备ID"].ToString(), drZ["出库目的科室ID"].ToString(), His.his.Choscode.ToString() });
            }
        }

        //审核入库细表
        private void AddRuKuXiBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> drX)
        {
            ac.Add("DETAILNO", "");//服务端生成
            ac.Add("INID", "");//入库主表的ID  服务端获取
            ac.Add("EQID", drX["设备ID"].ToString());
            ac.Add("UNITCODE", drX["单位编码"].ToString());
            ac.Add("GG", drX["规格"].ToString());
            ac.Add("XH", drX["型号"].ToString());
            ac.Add("CD", drX["产地"].ToString());
            ac.Add("NUM", drX["数量"].ToInt());
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
            ac.Add("STOCKFLOWNO", "");//等生成对应的流水表再更新
        }
        //审核入库主表
        private void AddRuKuZhuBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> drZhuBiao)
        {

           
            ac.Add("INID", "");//ID后面自动生成
            ac.Add("IOID", IOIDInDetail);
            ac.Add("OPFLAG", 2);//申领入库主表OPFLAG肯定是2  申领

            //ac.Add("WARECODE", drZhuBiao["出库目的库房"].ToString());
            ac.Add("WARECODE", null);
            ac.Add("DEPTID", drZhuBiao["出库目的科室ID"].ToString());

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
                if (drZhu["对应入库ID"].IsNull == true || drZhu["对应入库ID"].ToString() == "")
                {
                    WJs.alert("没有包含该条设备领用信息的入库信息，无法进行冲销操作！");
                    return;
                }
                if (drZhu["状态"].ToString() == "6")
                {
                    if (WJs.confirm("确认对已审核的数据进行冲销操作？"))
                    {
                        //更改状态   是不是可以同时适应出库和入库呢？  
                        ActionLoad acCX = ActionLoad.Conn();
                        acCX.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                        acCX.Sql = "ChongXiaoRuChu";
                        acCX.Add("STATUS", "0");
                        acCX.Add("CXUSERNAME", His.his.UserName);
                        acCX.Add("CXUSERID", His.his.UserId.ToString());
                        acCX.Add("CXDATE", DateTime.Now);
                        acCX.Add("INID", drZhu["对应入库ID"].ToString());
                        acCX.Add("OUTID", drZhu["出库ID"].ToString());
                        acCX.Add("CHOSCODE", drZhu["医疗机构编码"].ToString());
                        acCX.ServiceLoad += new YtClient.data.events.LoadEventHandle(acCX_ServiceLoad);
                        acCX.Post();

                        //执行冲销操作
                        List<Dictionary<string, ObjItem>> XBList = this.dataGView2.GetData();
                        ChongXiaoCaoZuo(drZhu, XBList);
                        if (!isOK2)
                        {
                            WJs.alert("冲销失败！");
                            return;
                        }

                        //判断是否建卡？【循环细表内容】
                        foreach (Dictionary<string, ObjItem> item in XBList)
                        {
                            CXJKNum++;
                            // SELECT FLOWNO,DEPTID,STOCKID,EQID FROM LKEQ.EQSTOCKDETAIL WHERE CARDNUM &gt;= ? AND CHOSCODE=? AND FLOWNO=?
                            DataTable cxCard = LData.LoadDataTable("IfHaveBuildCardInLingYong", null, new object[] { item["数量"].ToInt(), His.his.Choscode, item["库存流水号"].ToString() });
                            if (cxCard == null || cxCard.Rows.Count <= 0)
                            {
                                continue;
                            }
                            else
                            {
                                DataRow dr = cxCard.Rows[0];//只包含一行
                                if (WJs.confirm("该领用单上的第" + CXJKNum + "条数据，建立了卡片，是否要将卡片进行冲销？"))
                                {
                                    //冲销掉对应的卡片   第一，减少对应的库存流水内的卡片数量；第二，在卡片表格内更改状态为7                
                                    //UPDATE LKEQ.EQSTOCKDETAIL SET CARDNUM=CARDNUM-? WHERE FLOWNO=? AND CHOSCODE=?

                                    //UPDATE  LKEQ.EQCARDREC SET  STATUS=7 
                                    //WHERE CARDID IN ( SELECT CARDID  FROM (SELECT *  FROM LKEQ.EQCARDREC 
                                    //WHERE  STOCKID=? AND STOCKFLOWNO=?  AND DEPTID=?  ORDER BY CARDID DESC) WHERE ROWNUM<=?)
                                    isHaveCard = true;
                                    LData.Exe("ChongXiaoKaPianNumLingYong", null, new object[] { item["数量"].ToInt(), dr["FLOWNO"].ToString(), His.his.Choscode });
                                    //LData.Exe("ChongXiaoKaPianInLingYong", null, new object[] { dr["STOCKID"].ToString(), dr["FLOWNO"].ToString(), dr["DEPTID"].ToString(), His.his.Choscode, dr["EQID"].ToString(), item["数量"].ToInt() });
                                    LData.Exe("ChongXiaoKaPianInLingYong", null, new object[] { dr["STOCKID"].ToString(), dr["FLOWNO"].ToString(), drZhu["出库目的科室ID"].ToString(), His.his.Choscode, dr["EQID"].ToString(), item["数量"].ToInt() });
                                }
                            }
                        }
                        if (isHaveCard)
                        {
                            WJs.alert("卡片冲销成功！");
                        }
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

        private void ChongXiaoCaoZuo(Dictionary<string, ObjItem> drZ, List<Dictionary<string, ObjItem>> drXList)
        {
            //6个表的数据需要进行更新 
            //我的意思是： 主表首先直接冲销  然后循环细表进行冲销
            //出库主表里面的INID 找到 入库主表信息  读取完了之后 修改重新插入冲销数据
            //由对应的INID 找到 对应的入库细表数据  读取完了之后 修改重新插入冲销数据

            //1.1,生成对应的出库主表  状态为7 
            ActionLoad acCXZCK = ActionLoad.Conn();
            acCXZCK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
            acCXZCK.Sql = "ChongXiaoZhuChuKu";
            ChongXiaoCKZhuBiao(acCXZCK, drZ);
            acCXZCK.ServiceLoad += new YtClient.data.events.LoadEventHandle(acCX_ServiceLoad);
            acCXZCK.Post();
            if (!isOK2)
            { return; }

            //1.2  //将库存主表内的数目还原更新   出库库房的数目加回来
            //select EQID, sum(Num) AS 数量 from lkeq.eqoutdetail  where outid=? and CHOSCODE=?  group by eqid   
            //UPDATE LKEQ.EQSTOCK SET NUM=NUM- ? WHERE CHOSCODE=? AND WARECODE=? AND EQID=?

            DataTable DTNumForEQStock = LData.LoadDataTable("GetStockNumInOutDetail", null, new object[] { drZ["出库ID"].ToString(), His.his.Choscode });
            if (DTNumForEQStock != null && DTNumForEQStock.Rows.Count != 0)
            {
                foreach (DataRow itemRow in DTNumForEQStock.Rows)
                {
                    LData.Exe("UpdateStockNumInLingYong", null, new object[] { Convert.ToInt32(itemRow["数量"].ToString()) * (-1), His.his.Choscode, drZ["出库库房"].ToString(), itemRow["EQID"].ToString() });
                }
            }

            foreach (Dictionary<string, ObjItem> drXiBiao in drXList)
            {
                //  出库细表  ac冲销细出库
                if (drXiBiao["出库ID"].IsNull == true || drXiBiao["出库ID"].ToString() == "")
                {
                    continue;
                }
                if (LData.LoadDataTable("StockDetailSearchByLiuShuiHao", new object[] { drXiBiao["库存流水号"].ToString(), His.his.Choscode.ToString() }) == null)
                {
                    continue;
                }
                //2.1更新出库库房数据[针对每条流水必须都更新]    UPDATE LKEQ.EQSTOCKDETAIL SET OUTNUM=(OUTNUM+?) WHERE FLOWNO=? AND CHOSCODE=?  流水表内的出库数目减回来
                LData.Exe("UpdateEQStockDetailNum", null, new object[] { Convert.ToInt32(drXiBiao["数量"].ToString()) * (-1), drXiBiao["库存流水号"].ToString(), His.his.Choscode });

                //2.2
                ActionLoad acCXXCK = ActionLoad.Conn();
                acCXXCK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                acCXXCK.Sql = "ChongXiaoXiChuKu";
                ChongXiaoCKXiBiao(acCXXCK, drXiBiao);
                acCXXCK.ServiceLoad += new YtClient.data.events.LoadEventHandle(acCX_ServiceLoad);
                acCXXCK.Post();
            }
            if (!isOK2)
            { return; }

            //2.1  入库主表  状态为7   
            ActionLoad acCXZRK = ActionLoad.Conn();
            acCXZRK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
            acCXZRK.Sql = "ChongXiaoZhuRuKu";

            //由原始INID 获取对应的 入库主表和细表的内容   根据这些内容生成冲销操作所需要的数据【前面已经验证了INID的存在】
            DataTable dt = LData.LoadDataTable("FindINMainInfo", null, new object[] { His.his.Choscode.ToString(), drZ["对应入库ID"].ToString() });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];//要冲销的调拨数据对应的入库主表信息
                ChongXiaoRKZhuBiao(acCXZRK, dr);
                acCXZRK.ServiceLoad += new YtClient.data.events.LoadEventHandle(acCX_ServiceLoad);
                acCXZRK.Post();
            }
            else
            {
                WJs.alert("没有对应的入库主表信息出错，冲销操作被终止！");
                isOK2 = false;
                return;
            }
            if (!isOK2)
            { return; }

            //2.2  //将库存主表内的数目还原更新  细表内   出库目的科室的数目减回来
            //select EQID, sum(Num) AS 数量 from lkeq.eqindetail  where inid=? and CHOSCODE=?  group by eqid    
            //UPDATE LKEQ.EQSTOCK SET NUM=(NUM+?) WHERE EQID=? AND WARECODE IS NULL AND DEPTID=? AND CHOSCODE=?
            DataTable DTNumForEQStockCX = LData.LoadDataTable("GetStockNumInInDetail", null, new object[] { drZ["对应入库ID"].ToString(), His.his.Choscode });
            if (DTNumForEQStockCX != null)
            {
                foreach (DataRow itemRow in DTNumForEQStockCX.Rows)
                {
                    LData.Exe("UpdateEQStockNumForTarget_LingYong", null, new object[] { Convert.ToInt32(itemRow["数量"].ToString()) * (-1), itemRow["EQID"].ToString(), drZ["出库目的科室ID"].ToString(), His.his.Choscode });
                }
            }

            //2.3   入库细表  
            DataTable dtDetail = LData.LoadDataTable("FindInDetailInfo", null, new object[] { His.his.Choscode.ToString(), drZ["对应入库ID"].ToString() });
            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetail.Rows)
                {
                    ActionLoad acCXXRK = ActionLoad.Conn();
                    acCXXRK.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                    acCXXRK.Sql = "ChongXiaoXiRuKu";
                    ChongXiaoRKXiBiao(acCXXRK, dr);
                    acCXXRK.ServiceLoad += new YtClient.data.events.LoadEventHandle(acCX_ServiceLoad);
                    acCXXRK.Post();
                    //2.1  更新对应的目的库房[入库流水每条流水必须都更新]   UPDATE LKEQ.EQSTOCKDETAIL SET NUM=(NUM+?) WHERE FLOWNO=? AND CHOSCODE=?  流水表内的入库数目没有那么多，减回来
                    LData.Exe("UpdateStockNumInLingYongForCX", null, new object[] { Convert.ToInt32(dr["NUM"]) * (-1), dr["STOCKFLOWNO"].ToString(), His.his.Choscode });
                }
            }
            if (!isOK2)
            { return; }
        }
        //冲销入库细表
        private void ChongXiaoRKXiBiao(ActionLoad ac, DataRow drX)
        {
            ac.Add("DETAILNO", "");//服务端生成
            ac.Add("INID", "");//服务端获取
            ac.Add("EQID", drX["EQID"].ToString());
            ac.Add("UNITCODE", drX["UNITCODE"].ToString());
            ac.Add("GG", drX["GG"].ToString());
            ac.Add("XH", drX["XH"].ToString());
            ac.Add("CD", drX["CD"].ToString());
            ac.Add("NUM", Convert.ToInt32(drX["NUM"]) * (-1));
            ac.Add("PRICE", drX["PRICE"].ToString());
            ac.Add("MONEY", (Convert.ToDouble(drX["MONEY"]) * (-1)).ToString());
            ac.Add("OTHERMONEY", (Convert.ToDouble(drX["OTHERMONEY"]) * (-1)).ToString());
            ac.Add("TOTALPRICE", (Convert.ToDouble(drX["TOTALPRICE"])).ToString());//这里依旧是单价和成本单价为负数
            ac.Add("TOTALMONEY", (Convert.ToDouble(drX["TOTALMONEY"]) * (-1)).ToString());

            DateTime UserFulDate;
            if (DateTime.TryParse(drX["PRODUCTDATE"].ToString(), out UserFulDate))
            {
                ac.Add("PRODUCTDATE", UserFulDate);
            }
            else
            {
                ac.Add("PRODUCTDATE", "");
            }

            if (DateTime.TryParse(drX["VALIDDATE"].ToString(), out UserFulDate))
            {
                ac.Add("VALIDDATE", UserFulDate);
            }
            else
            {
                ac.Add("VALIDDATE", "");
            }

            ac.Add("CHOSCODE", drX["CHOSCODE"].ToString());
            ac.Add("MEMO", drX["MEMO"].ToString());
            ac.Add("SUPPLYID", drX["SUPPLYID"].ToString());
            ac.Add("SUPPLYNAME", drX["SUPPLYNAME"].ToString());
            ac.Add("TXM", drX["TXM"].ToString());
            ac.Add("STOCKFLOWNO", drX["STOCKFLOWNO"].ToString());
        }
        //冲销入库主表
        private void ChongXiaoRKZhuBiao(ActionLoad acChongXiaoZhuRuKu, DataRow dr)
        {
            acChongXiaoZhuRuKu.Add("INID", "");//服务端生成  针对原本的冲销ID

            acChongXiaoZhuRuKu.Add("IOID", dr["IOID"].ToString());
            //acChongXiaoZhuRuKu.Add("WARECODE", dr["WARECODE"].ToString());
            acChongXiaoZhuRuKu.Add("WARECODE", null);
            acChongXiaoZhuRuKu.Add("DEPTID", dr["DEPTID"].ToString());
            acChongXiaoZhuRuKu.Add("RECIPECODE", dr["RECIPECODE"].ToString());
            acChongXiaoZhuRuKu.Add("SHDH", dr["SHDH"].ToString());
            acChongXiaoZhuRuKu.Add("SUPPLYID", dr["SUPPLYID"].ToString());
            acChongXiaoZhuRuKu.Add("SUPPLYNAME", dr["SUPPLYNAME"].ToString());
            acChongXiaoZhuRuKu.Add("TOTALMONEY", (Convert.ToDouble(dr["TOTALMONEY"]) * (-1)).ToString());
            acChongXiaoZhuRuKu.Add("INVOICECODE", dr["INVOICECODE"].ToString());

            DateTime UseFulDateTime;
            if (DateTime.TryParse(dr["INVOICEDATE"].ToString(), out UseFulDateTime))
            {
                acChongXiaoZhuRuKu.Add("INVOICEDATE", UseFulDateTime);
            }
            else
            {
                acChongXiaoZhuRuKu.Add("INVOICEDATE", "");
            }

            if (DateTime.TryParse(dr["INDATE"].ToString(), out UseFulDateTime))
            {
                acChongXiaoZhuRuKu.Add("INDATE", UseFulDateTime);
            }
            else
            {
                acChongXiaoZhuRuKu.Add("INDATE", "");
            }

            if (DateTime.TryParse(dr["RECDATE"].ToString(), out UseFulDateTime))
            {
                acChongXiaoZhuRuKu.Add("RECDATE", UseFulDateTime);
            }
            else
            {
                acChongXiaoZhuRuKu.Add("RECDATE", "");
            }

            if (DateTime.TryParse(dr["SHDATE"].ToString(), out UseFulDateTime))
            {
                acChongXiaoZhuRuKu.Add("SHDATE", UseFulDateTime);
            }
            else
            {
                acChongXiaoZhuRuKu.Add("SHDATE", "");
            }

            if (DateTime.TryParse(dr["CXDATE"].ToString(), out UseFulDateTime))
            {
                acChongXiaoZhuRuKu.Add("CXDATE", UseFulDateTime);
            }
            else
            {
                acChongXiaoZhuRuKu.Add("CXDATE", "");
            }

            acChongXiaoZhuRuKu.Add("SHUSERID", dr["SHUSERID"].ToString());
            acChongXiaoZhuRuKu.Add("SHUSERNAME", dr["SHUSERNAME"].ToString());
            acChongXiaoZhuRuKu.Add("INVOICEMONEY", (Convert.ToDouble(dr["INVOICEMONEY"]) * (-1)).ToString());
            acChongXiaoZhuRuKu.Add("OTHERMONEY", (Convert.ToDouble(dr["OTHERMONEY"]) * (-1)).ToString());

            acChongXiaoZhuRuKu.Add("BUYUSER", dr["BUYUSER"].ToString());
            acChongXiaoZhuRuKu.Add("STATUS", "7");
            acChongXiaoZhuRuKu.Add("MEMO", dr["MEMO"].ToString());
            acChongXiaoZhuRuKu.Add("OPFLAG", dr["OPFLAG"].ToString());
            acChongXiaoZhuRuKu.Add("USERID", dr["USERID"].ToString());
            acChongXiaoZhuRuKu.Add("USERNAME", dr["USERNAME"].ToString());

            acChongXiaoZhuRuKu.Add("CXUSERID", dr["CXUSERID"].ToString());
            acChongXiaoZhuRuKu.Add("CXUSERNAME", dr["CXUSERNAME"].ToString());
            acChongXiaoZhuRuKu.Add("CHOSCODE", dr["CHOSCODE"].ToString());
        }
        //冲销出库细表
        private void ChongXiaoCKXiBiao(ActionLoad ac, Dictionary<string, ObjItem> drXiBiao)
        {
            //细表 21   直接读取获得
            ac.Add("DETAILNO", null);//服务端生成
            ac.Add("OUTID", null);//获取最新生成
            ac.Add("EQID", drXiBiao["设备ID"].ToString());

            ac.Add("NUM", drXiBiao["数量"].ToInt() * (-1));//负数  需要更新数量

            ac.Add("UNITCODE", drXiBiao["单位编码"].ToString());
            ac.Add("PRICE", drXiBiao["单价"].ToDouble());//单价和成本单价为正？
            ac.Add("MONEY", drXiBiao["金额"].ToDouble() * (-1));//负数
            ac.Add("GG", drXiBiao["规格"].ToString());
            ac.Add("XH", drXiBiao["型号"].ToString());
            ac.Add("CD", drXiBiao["产地"].ToString());
            ac.Add("OTHERMONEY", drXiBiao["运杂费"].ToDouble() * (-1));
            ac.Add("TOTALPRICE", drXiBiao["成本单价"].ToDouble());
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
        //冲销出库主表
        private void ChongXiaoCKZhuBiao(ActionLoad ac, Dictionary<string, ObjItem> drZhu)
        {
            //主表 22 主键为自动生成
            ac.Add("OUTID", "");//服务端生成
            ac.Add("IOID", drZhu["出库方式ID"].ToString());
            ac.Add("RECIPECODE", drZhu["单据号"].ToString());
            ac.Add("WARECODE", drZhu["出库库房"].ToString());
            //ac.Add("TARGETWARECODE", drZhu["出库目的库房"].ToString());
            ac.Add("TARGETWARECODE", null);
            ac.Add("TARGETDEPTID", drZhu["出库目的科室ID"].ToString());
            ac.Add("TOTALMONEY", (drZhu["总金额"].ToDouble() * (-1)).ToString());
            ac.Add("OUTDATE", drZhu["制单日期"].ToDateTime());
            ac.Add("STATUS", "7");//状态全部为已冲销
            ac.Add("MEMO", drZhu["备注"].ToString());
            ac.Add("OPFLAG", drZhu["操作标志"].ToString());
            ac.Add("RECDATE", drZhu["修改时间"].ToDateTime());
            ac.Add("SHDATE", drZhu["审核日期"].ToDateTime());
            ac.Add("SHUSERID", drZhu["审核操作员ID"].ToString());
            ac.Add("SHUSERNAME", drZhu["审核操作员姓名"].ToString());

            ac.Add("CXDATE", DateTime.Now);
            ac.Add("CXUSERID", His.his.UserId.ToString());
            ac.Add("CXUSERNAME", His.his.UserName);
            ac.Add("USERNAME", drZhu["操作员姓名"].ToString());
            ac.Add("USERID", drZhu["操作员ID"].ToString());
            ac.Add("INID", "");//由最新生成的更新
            ac.Add("CHOSCODE", His.his.Choscode);
        }

        void acCX_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK2 = true;
            }
            else
            {
                WJs.alert(e.Msg.Msg);
                isOK2 = false;
            }
        }
    }
}
