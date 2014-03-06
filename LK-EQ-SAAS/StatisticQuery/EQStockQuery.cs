using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChSys;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using YtPlugin;

namespace StatisticQuery
{
    public partial class EQStockQuery : Form,IPlug
    {
        public EQStockQuery()
        {
            InitializeComponent();
        }
        #region IPlug 成员

        public Form getMainForm()
        {
            return this;
        }

        public void initPlug(IAppContent app, object[] param)
        {
          
        }

        public bool unLoad()
        {
            return true;
        }

        #endregion

        private void WZStockQuery_Load(object sender, EventArgs e)
        {

            TvList.newBind().Load("FindWare_EQStockQuery", new object[] { His.his.Choscode }).Bind(this.WARECODE);
            TvList.newBind().Load("StatisticQuery_EQStockQueryKindName", new object[] { His.his.Choscode }).Bind(this.Column29);

            TvList.newBind().Load("StatisticQuery_EQStockQueryKSName", new object[] { His.his.Choscode }).Bind(this.Column14);
            TvList.newBind().Load("StatisticQuery_EQStockQueryKSName", new object[] { His.his.Choscode }).Bind(this.Column2);
            TvList.newBind().Load("EQStatisticQuery_DanWeiBianMa", new object[] { His.his.Choscode }).Bind(this.UNITCODE);
            TvList.newBind().Load("EQStatisticQuery_DanWeiBianMa", new object[] { His.his.Choscode }).Bind(this.UNITCODEXI);
          
            this.selTextInpt1.SelParam = "{key}|{key}|" + His.his.Choscode;//为什么要这样写？
          //  selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt1.Sql = "FindEQKind_EQStockQuery";//与效期的数据库查询语句相同
           // this.selTextInpt1.textBox1.ReadOnly = true;

            this.selTextInpt2.SelParam = "{key}|{key}|{key}|{key}|" + His.his.Choscode;
          //  selTextInpt1.BxSr = false;//必须输入查询关键字
            this.selTextInpt2.Sql = "FindEQDict_EQStockQuery";//与效期的数据库查询语句相同
          //  this.selTextInpt2.textBox1.ReadOnly = true;
            this.selTextInpt_Ware.Sql = "EQStockQuery_EQOutWare";

            this.selTextInpt_Ware.SelParam = His.his.Choscode + "|{key}|{key}|{key}|{key}";

            this.selTextInpt_KS.Sql = "StatQuery_EQOutQueryKS";
            this.selTextInpt_KS.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
        }

        private void button1_Click(object sender, EventArgs e)
        {



            this.dataGView_xi.ClearData();
            this.dataGView_Main.Url = "EQStockQuery_EQSearchMainStock";
            SqlStr sql = SqlStr.newSql();

            if (this.selTextInpt_Ware.Value == null && this.selTextInpt_KS.Value == null)
            {
                WJs.alert("请选择出库库房或者科室");

                return;
            }
            if (!this.selTextInpt_Ware.Text.Equals(""))
            {
                sql.Add("and c.WARECODE=?", this.selTextInpt_Ware.Value);
            }
            if (!this.selTextInpt_KS.Text.Equals(""))
            {
                sql.Add("and c.DEPTID=?", this.selTextInpt_KS.Value);
            }

       
            if (this.selTextInpt1.Value != null)
            {
                sql.Add(" and e.KINDCODE =? ", this.selTextInpt1.Value);
            }
            if (this.selTextInpt2.Value != null)
            {
                sql.Add(" and c.EQID=? ", this.selTextInpt2.Value);
            }



            this.dataGView_Main.reLoad(new object[] {  His.his.Choscode }, sql);

            this.TiaoSu.Text = this.dataGView_Main.RowCount.ToString() + "条";
        }

        private void dataGView_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             
            DataRow r1 = this.dataGView_Main.GetRowData();
            this.dataGView_xi.Url = "EQStockQuery_EQStockDetailInfo";
            this.dataGView_xi.reLoad(new object[] { r1["STOCKID"], His.his.Choscode });

            this.label18.Text = this.dataGView_xi.RowCount.ToString() + "条";
            this.label14.Text = this.dataGView_xi.Sum("金额").ToString() + "元";
            this.label9.Text = this.dataGView_xi.Sum("运杂费").ToString() + "元";
            this.label11.Text = this.dataGView_xi.Sum("成本金额").ToString() + "元";
      
        }

     

    }
}
