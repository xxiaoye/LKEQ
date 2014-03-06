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
using YtClient;
using YtUtil.tool;
using YtWinContrl.com.datagrid;

namespace EQWareManag.form
{
    public partial class EQDiaoBoEdit : Form
    {

        Dictionary<string, ObjItem> drmain;
        bool isOK; //代表修改是否成功
        int isFlag;        //0 代表浏览  1 代表编辑  2 代表新增       

        int Num = 0;//设置行数，提示用户第几行数据的操作失败或成功
        string isCanFKC;//是否允许负库存
        string TargetDEptid;
        int XiaoShuWei;//转换成小数位数
        public EQDiaoBoEdit()
        {
            InitializeComponent();
        }
        public EQDiaoBoEdit(Dictionary<string, ObjItem> drmain, int isFlag)
        {
            InitializeComponent();
            this.isFlag = isFlag;
            this.drmain = drmain;

            this.ware_selTextInpt.Sql = "WareBindInDiaoBo";
            this.ware_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";
            this.targetware_selTextInpt.Sql = "WareBindInDiaoBo";
            this.targetware_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            this.outfalg_selTextInpt.Sql = "outflagBindInDiaoBoEidt";
            this.outfalg_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}";

            this.TotalMoney_textBox1.Text = 0.ToString("f4");
            XiaoShuWei = GetSysDanWei(2203);
        }

        #region 主表细表及其中单位的初始化
        private void LoadZhuBiaoData()
        {
            //string warename = LData.Es("WareNameFindInEQDiaoBo", null, new object[] { drmain["出库ID"].ToString(), drmain["医疗机构编码"].ToString() });
            this.ware_selTextInpt.Text = drmain["出库库房_Text"].ToString();
            this.ware_selTextInpt.Value = drmain["出库库房"].ToString();

            //string targetwarename = LData.Es("TargetWareNameFindInEQDiaoBo", null, new object[] { drmain["出库ID"].ToString(), drmain["医疗机构编码"].ToString() });
            this.targetware_selTextInpt.Text = drmain["出库目的库房_Text"].ToString();
            this.targetware_selTextInpt.Value = drmain["出库目的库房"].ToString();


            this.outfalg_selTextInpt.Text = drmain["出库方式_Text"].ToString();
            this.outfalg_selTextInpt.Value = drmain["出库方式"].ToString();

            dateTimePicker1.Text = drmain["制单日期"].ToString();
            TotalMoney_textBox1.Text = drmain["总金额"].ToString();
            memo_textBox.Text = drmain["备注"].ToString();
            RecipeCode_textBox.Text = drmain["单据号"].ToString();
        }

        public void LoadXiBiaoData()
        {
            UnitCodeBind();
            this.dataGView1.Url = "XBInEQDiaoBoEdit";
            this.dataGView1.reLoad(new object[] { drmain["出库ID"].ToString(), His.his.Choscode });
        }


        #region 单位编码的绑定
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
        #endregion

        #endregion

        private void EQDiaoBoEdit_Load(object sender, EventArgs e)
        {
            //浏览或编辑
            if (isFlag == 0 || isFlag == 1)
            {
                LoadZhuBiaoData();
                LoadXiBiaoData();
                if (isFlag == 0)
                {
                    //浏览
                    this.toolStrip1.Enabled = false;
                    this.targetware_selTextInpt.Enabled = false;
                    this.outfalg_selTextInpt.Enabled = false;
                    this.memo_textBox.Enabled = false;
                    this.dateTimePicker1.Enabled = false;
                    this.dataGView1.ReadOnly = true;
                }
                this.ware_selTextInpt.Enabled = false;//浏览或者编辑都不允许更改出库库房了
            }
            else
            {
                //新增则直接初始化
                UnitCodeBind();
            }
            isCanFKC = LData.Es("FindIfCanFKC", null, new object[] { 2207 });
            this.dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
        }

        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["设备名称"].IsNull || e.Data["设备名称"].ToString().Trim().Equals(""))
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【设备名称】！");
                this.dataGView1.setFocus(e.Row.Index, "设备名称");
                return;
            }
            if (e.Data["单位编码"].IsNull || e.Data["单位编码"].ToString().Trim().Equals(""))
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【单位编码】！");
                this.dataGView1.setFocus(e.Row.Index, "单位编码");
                return;
            }
            if (!WJs.IsZs(e.Data["数量"].ToString()) || e.Data["数量"].ToDouble() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【数量】只能输入整数，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "数量");
                return;
            }
            if (e.Data["库存流水号"].IsNull || e.Data["库存流水号"].ToString().Trim().Equals(""))
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【库存流水号】！");
                this.dataGView1.setFocus(e.Row.Index, "库存流水号");
                return;
            }

            // 每一种设备ID 对应的设备数目之和与总的 
            if (isCanFKC == "0")//不允许负库存出库
            {
                if (e.Data["数量"].ToInt() > e.Data["库存余量"].ToInt())
                {
                    e.IsValid = false;
                    WJs.alert("由系统参数设置，第" + (e.Row.Index + 1) + "行的数量不能大于库存余量");
                    this.dataGView1.setFocus(e.Row.Index, "数量");
                }
            }

        }
        #region 废弃代码
        //void ware_selTextInpt_TextChanged(object sender, EventArgs e)
        //{
        //    if (this.ware_selTextInpt.Value != null && this.ware_selTextInpt.Value != "")
        //    {
        //        if (this.ware_selTextInpt.Value.ToString() != preValue)
        //        {
        //            if (preValue != null)
        //            {
        //                if (WJs.confirm("更改出库库房将会删除所有细表信息，确认更改出库库房？"))
        //                {
        //                    this.targetware_selTextInpt.Value = null;
        //                    this.targetware_selTextInpt.Text = null;
        //                    int K = this.dataGView1.Rows.Count;
        //                    for (int i = 0; i < K; i++)
        //                    {
        //                        this.dataGView1.Rows.Remove(this.dataGView1.Rows[0]);
        //                    }
        //                }
        //                else
        //                {
        //                    this.ware_selTextInpt.Value = preValue;
        //                    this.ware_selTextInpt.Text = preText;
        //                    return;
        //                }
        //            }
        //            preText = this.ware_selTextInpt.Text.ToString();
        //            preValue = this.ware_selTextInpt.Value.ToString();
        //        }
        //    }
        //}

        //private void selTextInpt1_Leave(object sender, EventArgs e)
        //{

        //} 
        #endregion


        private void Cancel_toolStrip_Click(object sender, EventArgs e)
        {
            if (WJs.confirm("您确定要退出吗？"))
            {
                this.Close();
            }
        }

        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            if (ware_selTextInpt.Value == null || this.ware_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库库房！");
                ware_selTextInpt.Focus();
                return;
            }
            if (targetware_selTextInpt.Value == null || this.targetware_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库目的库房！");
                targetware_selTextInpt.Focus();
                return;
            }

            if (outfalg_selTextInpt.Value == null || this.outfalg_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库方式！");
                outfalg_selTextInpt.Focus();
                return;
            }

            //this.dataGView1.GetSql("物资").Ps = His.his.Choscode + "|" + this.selTextInpt1.Value + "|" + isAllowFKC.ToString() + "|{key}|{key}|{key}|{key}|{key}|{key}";

            if (isCanFKC == "0") //如果不允许负库存   那么在加载的时候是不同的
            {
                this.dataGView1.addSql("XBInEQDiaoBoEdit11", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
            }
            else
            {
                this.dataGView1.addSql("XBInEQDiaoBoEdit12", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
            }

            //添加新行默认值
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["数量"] = 0;
            de["医疗机构编码"] = His.his.Choscode.ToString();
            de["运杂费"] = 0.ToString("f" + XiaoShuWei);
            de["金额"] = 0.ToString("f" + XiaoShuWei);
            de["单价"] = 0.ToString("f" + XiaoShuWei);
            de["成本单价"] = 0.ToString("f" + XiaoShuWei);
            de["成本金额"] = 0.ToString("f" + XiaoShuWei);
            if (isFlag == 1)//点击编辑时，无论怎么都是一样的出库ID
            {
                de["出库ID"] = drmain["出库ID"].ToString();
            }

            this.dataGView1.CellValueChanged += new DataGridViewCellEventHandler(dataGView1_CellValueChanged_1);
            this.dataGView1.AddRow(de, 0);
            this.dataGView1.AllowUserToAddRows = false;
        }


        private void del_toolStrip_Click(object sender, EventArgs e)
        {

            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                if (isFlag == 1)
                {
                    if (dr["流水号"].IsNull == false && dr["流水号"].ToString() != "")
                    {
                        if (WJs.confirm("你确定要删除选中的设备调拨信息，针对原始数据的删除时不可恢复的！"))
                        {
                            if (LData.Exe("DeleteOutDetailInfoInEdit", null, new object[] { dr["流水号"].ToString(), His.his.Choscode }) == "")
                            {
                                WJs.alert("删除信息失败！");
                                return;
                            }
                            this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                        }
                    }
                    else
                    {
                        this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);
                    }
                }
                else
                {
                    this.dataGView1.Rows.Remove(this.dataGView1.CurrentRow);

                }
                this.TotalMoney_textBox1.Text = this.dataGView1.Sum("金额").ToString();
            }
            else
            {
                WJs.alert("请选择要删除的设备调拨信息！");
            }
        }


        private void dataGView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == shuliang_Column.Index)
            {
                UpdateMoney(e.RowIndex);
            }
        }
        private void UpdateMoney(int row)
        {
            //数量不为0 或者空
            //成本单价>=<单价>+<运杂费>/<数量>
            //运杂费>= 所选库存流水记录里的运杂费/库存流水记录里的数量*本次出库数量；
            //<成本金额>=<成本单价>×<数量>
            DataGridViewRow r = this.dataGView1.Rows[row];
            if (r.Cells["EqIdName_Column"].Value == null)
            {
                return;
            }
            double result;

            if (!WJs.IsNum(r.Cells["shuliang_Column"].Value.ToString()) || !WJs.IsD0Zs(r.Cells["shuliang_Column"].Value.ToString()))
            {
                WJs.alert("数量必须为正整数，已设置为默认值1 ！");
                r.Cells["shuliang_Column"].Value = 1;
                return;
            }

            DataTable YunZaShuLiang = LData.LoadDataTable("FindYunZaFeiInEQDiaoBO", null, new object[] { dataGView1.Rows[row].Cells["KuCunLiuShui_Column"].Value, His.his.Choscode });
            if (YunZaShuLiang == null)
            {
                WJs.alert("该库存流水记录内不包含运杂费,设备数量等数据,无法继续操作！");
                return;
            }

            DataRow drr = YunZaShuLiang.Rows[0];
            double ChangeYunZaFei;
            //分别为对应的库存流水里面的数量和对应的运杂费       运杂费>= 所选库存流水记录里的运杂费/库存流水记录里的数量*本次出库数量；
            if (double.TryParse(drr["NUM"].ToString(), out result) && double.TryParse(drr["OTHERMONEY"].ToString(), out ChangeYunZaFei))
            {
                if (r.Cells["shuliang_Column"].Value != null && WJs.IsZs(r.Cells["shuliang_Column"].Value.ToString().Trim()) && r.Cells["danjia_Column"].Value != null)
                {
                    this.dataGView1.jsBds("金额=数量*单价");
                    r.Cells["yunzafei_Column"].Value = (ChangeYunZaFei * (Convert.ToDouble(r.Cells["shuliang_Column"].Value.ToString().Trim())) / result).ToString("f" + XiaoShuWei);
                    this.dataGView1.jsBds("成本单价=单价+运杂费/数量");
                    this.dataGView1.jsBds("成本金额=成本单价*数量");
                }
                else
                {
                    r.Cells["jine_Column"].Value = 0.ToString("f" + XiaoShuWei);
                    r.Cells["danjia_Column"].Value = 0.ToString("f" + XiaoShuWei);
                    r.Cells["yunzafei_Column"].Value = 0.ToString("f" + XiaoShuWei);
                }
            }
            this.TotalMoney_textBox1.Text = this.dataGView1.Sum("金额").ToString();
        }


        private bool XinXiYanZheng()
        {
            if (ware_selTextInpt.Value == targetware_selTextInpt.Value)
            {
                WJs.alert("出库库房和目的库房不能为同一库房！");
                return false;
            }
            if (ware_selTextInpt.Value == null || this.ware_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库库房！");
                ware_selTextInpt.Focus();
                return false;
            }
            if (targetware_selTextInpt.Value == null || this.targetware_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库目的库房！");
                targetware_selTextInpt.Focus();
                return false;
            }

            if (outfalg_selTextInpt.Value == null || this.outfalg_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库方式！");
                outfalg_selTextInpt.Focus();
                return false;
            }

            List<Dictionary<string, ObjItem>> drXiBiao = this.dataGView1.GetData();
            if (drXiBiao == null)
            {
                WJs.alert("细表不能为空，请修改后再进行保存！");
                return false;
            }
            return true;
        }

        private void Save_toolStrip_Click(object sender, EventArgs e)
        {

            dataGView1.IsAutoAddRow = false;
            this.TotalMoney_textBox1.Text = this.dataGView1.Sum("金额").ToString();
            if (!XinXiYanZheng())
            {
                return;
            }
            TargetDEptid = LData.Es("GetTargetDeptIdByTargetWare", null, new object[] { targetware_selTextInpt.Value.ToString(), His.his.Choscode });

            ActionLoad acZhu = ActionLoad.Conn();
            acZhu.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
            acZhu.Sql = "ModifyZhuOrAdd";
            if (isFlag == 1)
            {
                acZhu.Add("OUTID", drmain["出库ID"].ToString());
            }
            if (isFlag == 2)
            {
                acZhu.Add("OUTID", "");
            }
            AddZhuBiaoInfo(acZhu);

            string XiInfoXml = this.dataGView1.GetDataToXml();
            if (XiInfoXml != "" && XiInfoXml != null)
            {
                acZhu.Add("XiBiaoInfo", XiInfoXml);
                acZhu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acXi_ServiceLoad);
                acZhu.Post();
            }
            else
            {
                WJs.alert("细表内无数据，请填写数据后保存！");
            }

            if (isOK)
            {
                WJs.alert("保存设备调拨信息成功，即将关闭本窗口……");
                this.Close();
            }
        }

        void acXi_ServiceLoad(object sender, YtClient.data.events.LoadEvent e)
        {
            if (e.Msg.Msg.Equals("执行成功！"))
            {
                isOK = true;
            }
            else
            {
                isOK = false;
                WJs.alert(e.Msg.Msg.ToString());
            }
        }

        #region 已废弃，原本的循环更新
        //if (!isOK)
        //{
        //    return;
        //}

        ////细表主键流水号自动生成
        //foreach (Dictionary<string, ObjItem> rowxibiao in drXiBiao)
        //{
        //    ActionLoad acXi = ActionLoad.Conn();
        //    acXi.Action = "LKWZSVR.lkeq.WareManag.EQDiaoBoSvr";
        //    acXi.Sql = "ModifyOrAddXi";
        //    //无奈之举   针对可能在点完保存之后还自动增加新行的情况
        //    if (rowxibiao["设备名称"].IsNull == true || rowxibiao["设备名称"].ToString().Trim() == "" || rowxibiao["库存流水号"].IsNull == true || rowxibiao["库存流水号"].ToString() == "")
        //    {
        //        continue;
        //    }
        //    AddXiBiaoInfo(acXi, rowxibiao);
        //    acXi.ServiceLoad += new YtClient.data.events.LoadEventHandle(acXi_ServiceLoad);
        //    acXi.Post();
        //    if (!isOK)
        //    {
        //        return;
        //    }
        //} 
        #endregion

        private void AddZhuBiaoInfo(ActionLoad ac)
        {
            //主表 21 主键为自动获取，或自动生成
            ac.Add("WARECODE", this.ware_selTextInpt.Value);
            ac.Add("TARGETWARECODE", this.targetware_selTextInpt.Value);
            ac.Add("TARGETDEPTID", TargetDEptid);  //在服务端查询后再插入

            ac.Add("IOID", this.outfalg_selTextInpt.Value);
            ac.Add("RECIPECODE", this.RecipeCode_textBox.Text);// 单据号需要在服务端单独设置

            ac.Add("TOTALMONEY", this.TotalMoney_textBox1.Text.Trim());
            ac.Add("OUTDATE", DateTime.Now);
            if (isFlag == 1)
            {
                ac.Add("STATUS", drmain["状态"].ToString());
            }
            else
            {
                ac.Add("STATUS", "1");
            }//新生成的调拨单直接都是设置为等待审核【修改之后自动提交审核】
            ac.Add("MEMO", this.memo_textBox.Text.ToString()); //主表的备注
            ac.Add("OPFLAG", "1");//既然是出库操作，直接设置为1
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("SHDATE", null);
            ac.Add("SHUSERID", null);
            ac.Add("SHUSERNAME", null);
            ac.Add("CXDATE", null);
            ac.Add("CXUSERID", null);
            ac.Add("CXUSERNAME", null);
            ac.Add("USERNAME", His.his.UserName.ToString());
            ac.Add("USERID", His.his.UserId);
            ac.Add("INID", null);//入库ID在服务端单独设置
            ac.Add("CHOSCODE", His.his.Choscode.ToString());
        }
        private void AddXiBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> rowxibiao)
        {
            //细表 21 主键为自动获取或生成 另外 OUTID 从主表获取
            if (isFlag == 1)
            {
                ac.Add("OUTID", rowxibiao["出库ID"].ToString());
            }
            else
            {
                ac.Add("OUTID", "");
            }
            if (rowxibiao["流水号"].IsNull == true || rowxibiao["流水号"].ToString() == "")
            {
                ac.Add("DETAILNO", "");
            }
            else
            {
                ac.Add("DETAILNO", rowxibiao["流水号"].ToString());
            }
            ac.Add("EQID", rowxibiao["设备ID"].ToString());
            ac.Add("NUM", rowxibiao["数量"].ToInt());
            ac.Add("UNITCODE", rowxibiao["单位编码"].ToString());
            ac.Add("PRICE", rowxibiao["单价"].ToString());
            ac.Add("MONEY", rowxibiao["金额"].ToString());
            ac.Add("GG", rowxibiao["规格"].ToString());
            ac.Add("XH", rowxibiao["型号"].ToString());
            ac.Add("CD", rowxibiao["产地"].ToString());
            ac.Add("OTHERMONEY", rowxibiao["运杂费"].ToString());
            ac.Add("TOTALPRICE", rowxibiao["成本单价"].ToString());
            ac.Add("TOTALMONEY", rowxibiao["成本金额"].ToString());
            ac.Add("MEMO", rowxibiao["备注"].ToString());
            ac.Add("TXM", rowxibiao["条形码"].ToString());
            ac.Add("CHOSCODE", His.his.Choscode.ToString());
            ac.Add("STOCKFLOWNO", rowxibiao["库存流水号"].ToString());
            ac.Add("SUPPLYID", rowxibiao["生产厂家ID"].ToString());
            ac.Add("SUPPLYNAME", rowxibiao["生产厂家名称"].ToString());
            ac.Add("PRODUCTDATE", rowxibiao["生产日期"].ToDateTime());
            ac.Add("VALIDDATE", rowxibiao["有效期"].ToDateTime());

        }

        //根据系统Id2203来求精确度 获取2   这里调用时 ，为了简便我直接写值 f2
        private int GetSysDanWei(int Id)
        {
            return Convert.ToInt32(LData.Es("GetSysDanWeiInEQDiaoBoEdit", null, new object[] { Id }));
        }




        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label14.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;

        }


        private void dataGView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == SheBeiMingChen.Index)
            {
                if (isCanFKC == "0") //如果不允许负库存   那么在加载的时候是不同的
                {
                    this.dataGView1.addSql("XBInEQDiaoBoEdit11", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
                }
                else
                {
                    this.dataGView1.addSql("XBInEQDiaoBoEdit12", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
                }
            }

            if (e.ColumnIndex == KuCunLiuShui_Column.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["SheBeiMingChen"].Value != null)
                {
                    this.dataGView1.addSql("XBKCLSSelInput", "库存流水号", "", this.ware_selTextInpt.Value + "|" + dataGView1.Rows[e.RowIndex].Cells["EqIdName_Column"].Value + "|" + His.his.Choscode + "|{key}|{key}");
                }
                else
                {
                    WJs.alert("请先选择出库设备名称，请在设备名称内键入空格后选择！");
                }
            }
            if (e.ColumnIndex == shuliang_Column.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["SheBeiMingChen"].Value == null)
                {
                    WJs.alert("请先选择出库设备名称，请在设备名称内键入空格后选择！");
                }
            }
        }

       
    }
}
