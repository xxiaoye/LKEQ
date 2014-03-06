using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtPlugin;
using ChSys;
using YiTian.db;
using YtWinContrl.com.datagrid;
using YtUtil.tool;
using UseingEQ.form;
using YtClient;

namespace UseingEQ
{
    public partial class EQMonthAccountsManag : Form, IPlug
    {
        public EQMonthAccountsManag()
        {
            InitializeComponent();
            this.dataGView1.Url = "EQMonthAccountManag_Load";
            this.dataGView2.Url = "EQMonthAccountManag_SelectChange_Load";
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

        private void EQMonthAccountsManag_Load(object sender, EventArgs e)
        {
            this.dataGView2.IsAutoAddRow = false;
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(StatusColumn);
            TvList.newBind().add("手工折旧", "0").add("自动折旧", "1").Bind(DepreTypeColumn);


            this.dataGView1.reLoad(new object[] { His.his.Choscode });
            this.dataGView1.IsPage = true;
            this.dataGView1.SelectionChanged += new EventHandler(dataGView1_SelectionChanged);
            YJFlagJudge();

        }
        private void YJFlagJudge()
        {
            this.dataGView1.IsAutoAddRow = false;
            int RowI = this.dataGView1.RowCount;
            if (RowI <= 0)
            {
                return;
            }
            for (int i = 0; i < RowI; i++)
            {
                DataTable StatusTable = LData.LoadDataTable("GetYJStatus_EQMonthAccountManag", null, new object[] { His.his.Choscode, this.dataGView1["QiJianIDColumn", i].Value.ToString() });
                if (StatusTable == null)
                {
                    this.dataGView1["YJFlagColumn", i].Value = "未月结";
                    continue;
                }
                foreach (DataRow dr in StatusTable.Rows)
                {
                    if (dr[0].ToString() == "1")
                    {
                        this.dataGView1["YJFlagColumn", i].Value = "已月结";
                        break;
                    }
                    this.dataGView1["YJFlagColumn", i].Value = "未月结";
                }
            }
        }

        void dataGView1_SelectionChanged(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();
            if (dr != null)
            {
                this.dataGView2.reLoad(new object[] { dr["期间ID"].ToString(), His.his.Choscode.ToString() });
            }
            else
            {
                this.dataGView2.reLoad(null);
            }
        }


        //月结
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = this.dataGView1.getRowData();//获取主表数据
            List<Dictionary<string, ObjItem>> drList = this.dataGView2.GetData();
            //如果为空，则这个里面肯定不包含对应的自动折旧记录和状态为1的折旧记录
            if (drList == null || drList.Count == 0)
            {
                EQMonthAccountEdit form = new EQMonthAccountEdit(dr, null, 1);
                form.ShowDialog();

                this.dataGView1.reLoad(new object[] { His.his.Choscode });
                dataGView1_SelectionChanged(null, null);
                YJFlagJudge();
            }
            else
            {
                foreach (Dictionary<string, ObjItem> item in drList)
                {
                    if (item["状态"].ToString() == "1")
                    {
                        WJs.alert("该期间划分里包含状态为1（有效）的设备折旧记录，无法进行月结！");
                        return;
                    }
                }
                //经过循环，没有状态为1的设备折旧记录，可以月结
                EQMonthAccountEdit form = new EQMonthAccountEdit(dr, null, 1);
                form.ShowDialog();


                this.dataGView1.reLoad(new object[] { His.his.Choscode });
                dataGView1_SelectionChanged(null, null);
                YJFlagJudge();


            }
        }
        //恢复
        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drX = this.dataGView2.getRowData();
            Dictionary<string, ObjItem> drZ = this.dataGView1.getRowData();
            if (drX != null)
            {
                if (drX["状态"].ToString() == "1")
                {
                    EQMonthAccountEdit form = new EQMonthAccountEdit(drZ, drX, 2);
                    form.ShowDialog();

                    this.dataGView1.reLoad(new object[] { His.his.Choscode });
                    dataGView1_SelectionChanged(null, null);
                    YJFlagJudge();
                }
                else
                {
                    WJs.alert("折旧记录为无效的数据，无法进行恢复操作！");
                    return;
                }
            }
            else
            {
                WJs.alert("请选择需要恢复的设备折旧记录！");
                return;
            }
        }

        private void dataGView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dictionary<string, ObjItem> drX = this.dataGView2.getRowData();
            Dictionary<string, ObjItem> drZ = this.dataGView1.getRowData();
            if (drX != null)
            {
                EQMonthAccountEdit form = new EQMonthAccountEdit(drZ, drX, 0);
                form.ShowDialog();
            }
            else
            {
                WJs.alert("请双击要查看的设备折旧记录！");
                return;
            }
        }
    }
}
