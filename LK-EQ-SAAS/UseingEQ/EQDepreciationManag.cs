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
using YtUtil.tool;
using ChSys;
using YiTian.db;
using YtClient;
using UseingEQ.form;

namespace UseingEQ
{
    public partial class EQDepreciationManag : Form, IPlug
    {
        public EQDepreciationManag()
        {
            InitializeComponent();
            //这个是从共享建卡过来的
            this.selTextInpt1.Sql = "FindDeptidInCard";
            this.selTextInpt1.SelParam = His.his.Choscode + "|{key}|{key}|{key}";
            dataGView1.Url = "LoadZhuBiaoInfo_EQDepreManag";
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

        private void EQDepreciationManag_Load(object sender, EventArgs e)
        {
            TvList.newBind().add("无效", "0").add("有效", "1").Bind(StatusColumn);
            TvList.newBind().add("手工折旧", "0").add("自动折旧", "1").Bind(DepreTypeColumn);

            this.dateTimeDuan1.InitCorl();
            this.dateTimeDuan1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selTextInpt1.Text == "" || selTextInpt1.Value == "")
            {
                WJs.alert("请选择科室！");
                selTextInpt1.Focus();
                return;
            }
            if (this.dateTimePicker1.Value.CompareTo(this.dateTimePicker2.Value) > 0)
            {
                WJs.alert("起始日期必须小于末尾日期！");
                return;
            }
            //SELECT distinct a.*,b.名称,c.Datename  FROM LKEQ.EQCARDDEPREREC a,HIS.科室表 b,LKEQ.DICTEQDATE c
            //WHERE a.DEPTID=b.ID AND a.Dateid=c.Dateid
            //AND a.DEPTID=? AND a.CHOSCODE=? AND a.RECDATE &gt;? AND a.RECDATE &lt;? AND a.DEPRETYPE=0
            dataGView1.reLoad(new object[] { selTextInpt1.Value, His.his.Choscode, this.dateTimePicker1.Value, this.dateTimePicker2.Value });
        }

        private void Del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1")
                {
                    //更新对应的卡片表内信息
                    //SELECT CARDID,TOTALZJ,MONTHZJ,TOTALEDWORK,MONTHWORK FROM LKEQ.EQCARDDEPREDETAILREC WHERE CHOSCODE=? AND DEPREID=?
                    //UPDATE LKEQ.EQCARDREC SET TOTALZJ=TOTALZJ - ?,TOTALEDWORK=TOTALEDWORK - ? WHERE CARDID=? AND CHOSCODE=?
                    DataTable DetailTable = LData.LoadDataTable("FindAllDetailInfoForUpdateData_EQDepreManag", null, new object[] { His.his.Choscode, dr["折旧ID"].ToString() });
                    if (DetailTable != null && DetailTable.Rows.Count > 0)
                    {
                        foreach (DataRow item in DetailTable.Rows)
                        {
                            //就每一条卡片数据进行判断更新
                            DataTable ZJWorkdt = GetZJWork_EQDepre(item["CARDID"].ToString());
                            if (ZJWorkdt != null)
                            {
                                DataRow ZJWorkdr = ZJWorkdt.Rows[0];
                                double Result;
                                //如果原本为Null，转换失败，则设置为0，否则肯定直接转换成功
                                if (!double.TryParse(ZJWorkdr["TOTALZJ"].ToString(), out Result))
                                {
                                    ZJWorkdr["TOTALZJ"] = "0.00";
                                }
                                if (!double.TryParse(ZJWorkdr["TOTALEDWORK"].ToString(), out Result))
                                {
                                    ZJWorkdr["TOTALEDWORK"] = "0.00";
                                }
                                if (Convert.ToDouble(ZJWorkdr["TOTALZJ"]) < Convert.ToDouble(item["MONTHZJ"]))
                                {
                                    WJs.alert("卡片" + ZJWorkdr["CARDID"].ToString() + ":累计折旧小于本月折旧，无法删除，否则出现折旧为负数！");
                                    return;
                                }
                                if (Convert.ToDouble(ZJWorkdr["TOTALEDWORK"]) < Convert.ToDouble(item["MONTHWORK"]))
                                {
                                    WJs.alert("卡片" + ZJWorkdr["CARDID"].ToString() + ":累计工作量小于本月工作量，无法删除，否则出现工作量为负数！");
                                    return;
                                }
                                LData.Exe("UpdateNumForDeleNotNull_EQDepreManag", null, new object[] { Convert.ToDouble(item["MONTHZJ"]), Convert.ToDouble(item["MONTHWORK"]), item["CARDID"].ToString(), His.his.Choscode });
                            }
                        }
                    }
                    else
                    {
                        WJs.alert("该条折旧信息无细表数据，无法删除！");
                        return;
                    }
                    //设为无效
                    LData.Exe("UpdateStatusInfo_EQDepreciationManag", null, new object[] { "0", dr["折旧ID"].ToString(), His.his.Choscode });
                    WJs.alert("删除成功！");
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("已经无效的数据无需删除！");
                }
            }
            else
            {
                WJs.alert("请选择需要删除的设备折旧信息！");
                return;
            }
        }

        private void refresh_toolStrip_Click(object sender, EventArgs e)
        {
            if (selTextInpt1.Text != "" || selTextInpt1.Value != "")
            {
                button1_Click(null, null);
            }
            else
            {
                WJs.alert("请先选择一个科室！");
            }
        }

        private void Edit_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                if (dr["状态"].ToString() == "1")
                {
                    EQDepreciationEdit form = new EQDepreciationEdit(dr, 1);
                    form.ShowDialog();
                    refresh_toolStrip_Click(null, null);
                }
                else
                {
                    WJs.alert("不能编辑无效状态设备折旧信息！");
                    return;
                }
            }
            else
            {
                WJs.alert("请选择需要编辑的设备折旧信息！");
                return;
            }
        }

        private void View_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> dr = dataGView1.getRowData();
            if (dr != null)
            {
                EQDepreciationEdit form = new EQDepreciationEdit(dr, 0);
                form.ShowDialog();
            }
            else
            {
                WJs.alert("请选择需要浏览的设备折旧信息！");
                return;
            }
        }

        private void Add_toolStrip_Click(object sender, EventArgs e)
        {
            EQDepreciationEdit form = new EQDepreciationEdit(null, 2);
            form.ShowDialog();
            if (selTextInpt1.Text == "" || selTextInpt1.Value == "")
            {
                return;
            }
            else
            {
                refresh_toolStrip_Click(null, null);
            }

            //新增后更新数据[这里我放到了服务端去执行]
        }

        private void dataGView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View_toolStrip_Click(null, null);
        }

        private DataTable GetZJWork_EQDepre(string CardId_EQDepre)
        {
            DataTable dt = LData.LoadDataTable("GetZJWork_EQDepreInEdit", null, new object[] { His.his.Choscode, CardId_EQDepre });
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
    }
}
