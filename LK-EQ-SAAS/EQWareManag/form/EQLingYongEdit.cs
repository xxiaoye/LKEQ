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
using YtWinContrl.com.datagrid;
using YtWinContrl.com.contrl;
using YtUtil.tool;

namespace EQWareManag.form
{
    public partial class EQLingYongEdit : Form
    {
        public EQLingYongEdit()
        {
            InitializeComponent();
        }

        string isCanFKC = "0";//默认设置为不准负出库
        int XiaoShuWei;//小数位数
        int Num;//行数提示
        int isFlag;   // 0 为浏览  1 为编辑  2 为新增  isFlag
        Dictionary<string, ObjItem> dr;
        bool isOK;

        private void Cancel_toolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UnitCodeBind()
        {
            DataTable dtUCB = LData.LoadDataTable("FindUnitNameInEQDiaoBo", null, null);
            TvList tv = TvList.newBind();
            ((DataGridViewComboBoxColumn)this.Unitcode_Column).Items.Clear();
            if (dtUCB != null)
            {
                foreach (DataRow item in dtUCB.Rows)
                {
                    tv.add(item[1].ToString(), item[0].ToString());
                }
                tv.Bind(Unitcode_Column);
            }
        }
        //根据系统Id2203来求精确度 
        private int GetSysDanWei(int Id)
        {
            return Convert.ToInt32(LData.Es("GetSysDanWeiInEQDiaoBoEdit", null, new object[] { Id }));
        }

        public EQLingYongEdit(Dictionary<string, ObjItem> drMain, int isFlag)
        {
            InitializeComponent();            
            this.isFlag = isFlag;
            this.dr = drMain;

            this.ware_selTextInpt.Sql = "WareBindInLingYong_LingYongEdit";
            this.ware_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";


            this.targetDeptid_selTextInpt.Sql = "DeptidBindInLingYong";
            this.targetDeptid_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            this.outfalg_selTextInpt.Sql = "outflagBindInLingYongEdit";
            this.outfalg_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}";


            this.TotalMoney_textBox1.Text = 0.ToString("f4");
            XiaoShuWei = GetSysDanWei(2203);
            UnitCodeBind();

        }


        private void GetDeptid(string Warecode)
        {
            DataTable dt = LData.LoadDataTable("FindDeptIDName", null, new object[] { Warecode, His.his.Choscode });
            if (dt != null)
            {
                DataRow dr = dt.Rows[0];
                ware_selTextInpt.Text = dr[1].ToString();
                ware_selTextInpt.Value = dr[0].ToString();
            }
        }
        private void LoadZhuBiaoData()
        {
            GetDeptid(dr["出库库房"].ToString());

            ware_selTextInpt.Value = dr["出库库房"].ToString();
            ware_selTextInpt.Text = dr["出库库房_Text"].ToString();

            targetDeptid_selTextInpt.Value = dr["出库目的科室ID"].ToString();
            targetDeptid_selTextInpt.Text = dr["出库目的科室"].ToString();

            outfalg_selTextInpt.Text = dr["出库方式"].ToString();
            outfalg_selTextInpt.Value = dr["出库方式ID"].ToString();

            memo_textBox.Text = dr["备注"].ToString();
            RecipeCode_textBox.Text = dr["单据号"].ToString();
            TotalMoney_textBox1.Text = dr["总金额"].ToString();

            dateTimePicker1.Value = dr["制单日期"].ToDateTime();
        }
        private void LoadXiBiaoData()
        {
            this.dataGView1.Url = "XBInEQDiaoBoEdit";
            this.dataGView1.reLoad(new object[] { dr["出库ID"].ToString(), His.his.Choscode });
        }


        private void DisEnable()
        {
            this.toolStrip1.Enabled = false;
            //this.ware_selTextInpt.Enabled = false;
            this.targetDeptid_selTextInpt.Enabled = false;
            this.outfalg_selTextInpt.Enabled = false;
            this.memo_textBox.Enabled = false;
            this.dateTimePicker1.Enabled = false;
            this.dataGView1.ReadOnly = true;

        }

        private void EQLingYongEdit_Load(object sender, EventArgs e)
        {

            //浏览或编辑
            if (isFlag == 0 || isFlag == 1)
            {
                LoadZhuBiaoData();
                LoadXiBiaoData();
                if (isFlag == 0)
                {
                    //浏览
                    DisEnable();
                }
                this.ware_selTextInpt.Enabled = false;//新增和编辑都不允许对库房进行修改了                
            }
            isCanFKC = LData.Es("FindIfCanFKC", null, new object[] { 2207 });
        }


        private void del_toolStrip_Click(object sender, EventArgs e)
        {

            if (dataGView1.CurrentRow == null)
            {
                WJs.alert("请选择要删除的设备领用信息！");
            }
            if (isFlag == 2)
            {
                dataGView1.Rows.Remove(dataGView1.CurrentRow);
            }
            if (isFlag == 1)
            {
                if (dataGView1.CurrentRow.Cells["Liushuihao_Column"].Value.ToString() != "" && dataGView1.CurrentRow.Cells["Liushuihao_Column"].Value != null)
                {
                    if (WJs.confirm("您确定要删除选中的设备领用信息？针对原始数据的删除时不可恢复的！"))
                    {
                        if (LData.Exe("DeleteOutDetailInfoInEdit", null, new object[] { this.dataGView1.CurrentRow.Cells["Liushuihao_Column"].Value, His.his.Choscode }) == "")
                        {
                            WJs.alert("删除信息失败！");
                            return;
                        }
                        dataGView1.Rows.Remove(dataGView1.CurrentRow);
                    }
                }
                else
                {
                    dataGView1.Rows.Remove(dataGView1.CurrentRow);
                }
            }
            this.TotalMoney_textBox1.Text = this.dataGView1.Sum("金额").ToString();
        }
        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            if (ware_selTextInpt.Value == null || ware_selTextInpt.Value == "")
            {
                WJs.alert("请选择申领库房！");
                ware_selTextInpt.Focus();
                return;
            }

            if (targetDeptid_selTextInpt.Value == null || targetDeptid_selTextInpt.Value == "")
            {
                WJs.alert("请选择需要申领设备的科室！");
                targetDeptid_selTextInpt.Focus();
                return;
            }

            if (outfalg_selTextInpt.Value == null || this.outfalg_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库方式！");
                outfalg_selTextInpt.Focus();
                return;
            }

            if (isCanFKC == "0")
            {
                this.dataGView1.addSql("SelptInLingYong1", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
            }
            else
            {
                this.dataGView1.addSql("SelptInLingYong2", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
            }
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["数量"] = 0;
            de["医疗机构编码"] = His.his.Choscode.ToString();
            de["运杂费"] = 0.00;
            de["金额"] = 0.00;
            de["单价"] = 0.00;
            de["成本单价"] = 0.00;
            de["成本金额"] = 0.00;
            if (isFlag == 1)//点击编辑时，无论怎么都是一样的出库ID
            {
                de["出库ID"] = dr["出库ID"].ToString();
            }
            this.dataGView1.AddRow(de, 0);
        }

        private void dataGView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == SheBeiMingChen.Index)
            {
                if (isCanFKC == "0")
                {
                    this.dataGView1.addSql("SelptInLingYong1", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
                }
                else
                {
                    this.dataGView1.addSql("SelptInLingYong2", "设备名称", "", this.ware_selTextInpt.Value + "|" + His.his.Choscode + "|{key}|{key}|{key}|{key}");
                }
            }

            if (e.ColumnIndex == KuCunLiuShui_Column.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["SheBeiMingChen"].Value != null)
                {
                    this.dataGView1.addSql("DGVInfoBindInLingYong", "库存流水号", "", this.ware_selTextInpt.Value + "|" + dataGView1.Rows[e.RowIndex].Cells["EqIdName_Column"].Value + "|" + His.his.Choscode + "|{key}|{key}");
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
                    //this.dataGView1.setFocus(e.RowIndex, "设备名称");
                }
            }
        }
        private void dataGView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == KuCunLiuShui_Column.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["SheBeiMingChen"].Value == null)
                {
                    WJs.alert("请先选择出库设备名称，请在设备名称内键入空格后选择！");
                }
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

        private int YanZhengZhuXinxi()
        {
            if (ware_selTextInpt.Value == null || ware_selTextInpt.Value == "")
            {
                WJs.alert("请选择申领库房！");
                ware_selTextInpt.Focus();
                return 1;
            }

            if (targetDeptid_selTextInpt.Value == null || targetDeptid_selTextInpt.Value == "")
            {
                WJs.alert("请选择需要申领设备的科室！");
                targetDeptid_selTextInpt.Focus();
                return 1;
            }

            if (outfalg_selTextInpt.Value == null || this.outfalg_selTextInpt.Value == "")
            {
                WJs.alert("请选择出库方式！");
                outfalg_selTextInpt.Focus();
                return 1;
            }

            return 0;

        }
        //验证细表信息
        private int YanZhengXiXinXi()
        {
            int n = dataGView1.RowCount;
            if (n <= 0)
            {
                WJs.alert("细表信息不能为空！");
                return 1;
            }

            for (int i = 0; i < n; i++)
            {
                Num++;
                DataGridViewCellCollection dgvc = dataGView1.Rows[i].Cells;
                if (dgvc != null)
                {

                    if (dgvc["SheBeiMingChen"].Value == null || dgvc["SheBeiMingChen"].Value.ToString().Trim() == "")
                    {
                        WJs.alert("第" + Num + "行，必填项：设备名称，请选择好之后进行保存！");
                        return 1;
                    }

                    if (dgvc["KCYLColumn"].Value == null || dgvc["KCYLColumn"].Value.ToString().Trim() == "")
                    {
                        WJs.alert("第" + Num + "行，必填项：流水号，请选择好之后进行保存！");
                        return 1;
                    }

                    if (dgvc["shuliang_Column"].Value == null || dgvc["shuliang_Column"].Value.ToString().Trim() == "")
                    {
                        WJs.alert("第" + Num + "行，必填项：数量，请选择好之后进行保存！");
                        return 1;
                    }

                    //if (!WJs.IsNum(dgvc["shuliang_Column"].Value.ToString()) || Convert.ToInt32(dgvc["shuliang_Column"].Value.ToString()) < 0 || WJs.IsZs(dgvc["shuliang_Column"].Value.ToString()))
                    if (!WJs.IsNum(dgvc["shuliang_Column"].Value.ToString()) || !WJs.IsD0Zs(dgvc["shuliang_Column"].Value.ToString()))
                    {
                        WJs.alert("第" + Num + "行，数量必须为正整数 ！");
                        return 1;
                    }

                    if (dgvc["Unitcode_Column"].Value == null || dgvc["Unitcode_Column"].Value.ToString().Trim() == "")
                    {
                        WJs.alert("第" + Num + "行，必填项：单位编码，请选择好之后进行保存！");
                        return 1;
                    }

                    if (isCanFKC == "0")
                    {
                        if (Convert.ToInt32(dgvc["shuliang_Column"].Value) > Convert.ToInt32(dgvc["KCYLColumn"].Value))
                        {
                            WJs.alert("由系统参数决定，不能进行负库存出库操作！");
                            return 1;
                        }
                    }
                }
                else
                {
                    WJs.alert("存在空行！");//此种情况基本不会出现，因为默认的添加行就是存在内容的
                    return 1;
                }
            }
            return 0;
        }


        private void Save_toolStrip_Click(object sender, EventArgs e)
        {
            dataGView1.IsAutoAddRow = false;
            this.TotalMoney_textBox1.Text = this.dataGView1.Sum("金额").ToString();
            if (YanZhengZhuXinxi() == 1)
            {
                return;
            }
            if (YanZhengXiXinXi() == 1)
            {
                return;
            }

            List<Dictionary<string, ObjItem>> ListXiBiao = dataGView1.GetData();
            if (ListXiBiao == null)
            {
                WJs.alert("细表不能为空，请修改后再进行保存！");
                return;
            }
            if (ListXiBiao.Count < 0 && ListXiBiao != null)
            {
                WJs.alert("请先填入细表数据后再进行保存！");
                return;
            }

            ActionLoad acZhu = ActionLoad.Conn();
            acZhu.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
            acZhu.Sql = "ModifyOrAddZhu";
            AddZhuBiaoInfo(acZhu);
            acZhu.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
            acZhu.Post();
            if (!isOK)
            { return; }


            foreach (Dictionary<string, ObjItem> itXi in ListXiBiao)
            {
                ActionLoad acXi = ActionLoad.Conn();
                acXi.Action = "LKWZSVR.lkeq.WareManag.EQLingYongSvr";
                acXi.Sql = "ModifyOrAddXi";

                if (itXi["设备名称"].IsNull == true || itXi["设备名称"].ToString() == "" || itXi["库存流水号"].IsNull == true || itXi["库存流水号"].ToString() == "")
                {
                    continue;
                }
                AddXiBiaoInfo(acXi, itXi);
                acXi.ServiceLoad += new YtClient.data.events.LoadEventHandle(acZhu_ServiceLoad);
                acXi.Post();
                if (!isOK)
                { return; }
            }
            if (isOK)
            {
                WJs.alert("保存设备领用信息成功，即将关闭本窗口……");
                this.Close();
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
        private void AddZhuBiaoInfo(ActionLoad ac)
        {
            //主表 21 主键为自动获取，或自动生成
            if (isFlag == 1)
            {
                ac.Add("OUTID", dr["出库ID"].ToString());
            }
            if (isFlag == 2)
            {
                ac.Add("OUTID", "");
            }
            ac.Add("WARECODE", this.ware_selTextInpt.Value);

            //ac.Add("TARGETWARECODE", TargetWarecode_selText.Value);
            ac.Add("TARGETWARECODE", null);
            ac.Add("TARGETDEPTID", this.targetDeptid_selTextInpt.Value);
            ac.Add("IOID", outfalg_selTextInpt.Value);
            ac.Add("RECIPECODE", RecipeCode_textBox.Text); // 单据号需要在服务端单独设置

            ac.Add("TOTALMONEY", this.TotalMoney_textBox1.Text.Trim());
            ac.Add("OUTDATE", DateTime.Now);
            if (isFlag == 1)
            {
                ac.Add("STATUS", dr["状态"].ToString());
            }
            else
            {
                ac.Add("STATUS", "1");
            }//新生成的调拨单直接都是设置为等待审核【修改之后自动提交审核】
            ac.Add("MEMO", this.memo_textBox.Text.ToString()); //主表的备注
            ac.Add("OPFLAG", "2");//既然是申领出库操作，直接设置为2
            ac.Add("RECDATE", DateTime.Now);
            ac.Add("SHDATE", null);
            ac.Add("SHUSERID", null);
            ac.Add("SHUSERNAME", null);
            ac.Add("CXDATE", null);
            ac.Add("CXUSERID", null);
            ac.Add("CXUSERNAME", null);
            ac.Add("USERNAME", His.his.UserName.ToString());
            ac.Add("USERID", His.his.UserId);
            ac.Add("INID", null);//入库ID在服务端单独更新
            ac.Add("CHOSCODE", His.his.Choscode.ToString());
        }

        private void AddXiBiaoInfo(ActionLoad ac, Dictionary<string, ObjItem> rowxibiao)
        {
            //细表 19 主键为自动获取或生成 另外 OUTID 从主表获取

            if (isFlag == 1)
            {
                ac.Add("OUTID", dr["出库ID"].ToString());
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
            ac.Add("UNITCODE", rowxibiao["单位"].ToString());
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

        private void AddXiBiaoInfo(ActionLoad ac, DataGridViewCellCollection dgvc)
        {
            //19 + OUTID +DETAILNO
            if (isFlag == 1)
            {
                ac.Add("OUTID", dr["出库ID"].ToString());
            }
            else
            {
                ac.Add("OUTID", "");
            }

            if (dgvc["Liushuihao_Column"].Value == null)
            {
                ac.Add("DETAILNO", "");
            }
            else
            {
                ac.Add("DETAILNO", dgvc["Liushuihao_Column"].Value.ToString());
            }//去服务端判断 然后是否为null  或者为修改则无
            ac.Add("EQID", dgvc["EqIdName_Column"].Value.ToString().Trim());
            ac.Add("NUM", dgvc["shuliang_Column"].Value.ToString().Trim());
            // ac.Add("UNITCODE", TvList.getValue(Unitcode_Column));
            ac.Add("UNITCODE", dgvc["Unitcode_Column"].Value.ToString().Trim());
            ac.Add("PRICE", Convert.ToDouble(dgvc["danjia_Column"].Value).ToString("f" + XiaoShuWei));
            ac.Add("MONEY", Convert.ToDouble(dgvc["jine_Column"].Value).ToString("f" + XiaoShuWei));
            ac.Add("GG", dgvc["guige_Column"].Value);
            ac.Add("XH", dgvc["xinghao_Column"].Value);
            ac.Add("CD", dgvc["changdi_Column"].Value);
            ac.Add("OTHERMONEY", Convert.ToDouble(dgvc["yunzafei_Column"].Value).ToString("f" + XiaoShuWei));
            ac.Add("TOTALPRICE", Convert.ToDouble(dgvc["chengbendanjia_Column"].Value).ToString("f" + XiaoShuWei));
            ac.Add("TOTALMONEY", Convert.ToDouble(dgvc["chengbenjine_Column"].Value).ToString("f" + XiaoShuWei));
            ac.Add("MEMO", dgvc["memo_Column"].Value);
            ac.Add("TXM", dgvc["tiaoxingma_Column"].Value);
            ac.Add("CHOSCODE", His.his.Choscode);
            ac.Add("STOCKFLOWNO", dgvc["KuCunLiuShui_Column"].Value);
            ac.Add("SUPPLYID", dgvc["changjiaid_Column"].Value);
            ac.Add("SUPPLYNAME", dgvc["changjiamingcheng_Column"].Value);
            DateTime result;
            if (DateTime.TryParse(dgvc["shengchanriqi_Column"].Value.ToString().Trim(), out result))
            {
                ac.Add("PRODUCTDATE", result);
            }
            else
            {
                ac.Add("PRODUCTDATE", "");
            }
            if (DateTime.TryParse(dgvc["youxiaoqi_Column"].Value.ToString().Trim(), out result))
            {
                ac.Add("VALIDDATE", result);
            }
            else
            {
                ac.Add("VALIDDATE", "");
            }
        }

        private void dataGView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.label14.Text = "共:" + this.dataGView1.Rows.GetRowCount(DataGridViewElementStates.Visible) + "条"; ;
        }
    }
}
