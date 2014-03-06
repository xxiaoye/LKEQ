using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using YiTian.db;
using ChSys;
using YtWinContrl.com.datagrid;
using YtClient;

namespace UseingEQ.form
{
    public partial class EQDepreciationEdit : Form
    {

        Dictionary<string, ObjItem> dr;
        List<Dictionary<string, ObjItem>> XiList;//存储加载过来的原数据
        int isFlag;
        bool isOk;

        public EQDepreciationEdit()
        {
            InitializeComponent();
        }

        public EQDepreciationEdit(Dictionary<string, ObjItem> dr, int isFlag)
        {
            InitializeComponent();
            dataGView1.IsAutoAddRow = false;
            this.dr = dr;
            this.isFlag = isFlag;
            dataGView1.Url = "LoadXiBiaoInfo_EQDepreManag";
            //这个是从共享建卡过来的      

            this.Deptid_selTextInpt.Sql = "FindDeptidInCard";
            this.Deptid_selTextInpt.SelParam = His.his.Choscode + "|{key}|{key}|{key}";

            this.DateId_selTextInp.Sql = "FindDateNameInfo_EQDepreciationEdit";
            this.DateId_selTextInp.SelParam = His.his.Choscode + "|{key}|{key}";

            TvList.newBind().add("手工折旧", "0").add("自动折旧", "1").Bind(ZJType_ComboBox);
            ZJType_ComboBox.SelectedIndex = 0;
            ZJType_ComboBox.Enabled = false;
        }

        private void EQDepreciationEdit_Load(object sender, EventArgs e)
        {
            if (isFlag == 0 || isFlag == 1)
            {
                Deptid_selTextInpt.Text = dr["科室名称"].ToString();
                Deptid_selTextInpt.Value = dr["科室ID"].ToString();

                DateId_selTextInp.Text = dr["期间划分名称"].ToString();
                DateId_selTextInp.Value = dr["期间划分ID"].ToString();

                Memo_textBox.Text = dr["备注"].ToString();
                ZJType_ComboBox.Value = dr["折旧类型"].ToString();

                //SELECT DISTINCT a.*,b.Eqname  FROM LKEQ.EQCARDDEPREDETAILREC a ,LKEQ.EQCARDREC b WHERE a.CARDID=b.CARDID AND a.DEPREID=?  AND  a.CHOSCODE=?
                dataGView1.reLoad(new object[] { dr["折旧ID"].ToString(), His.his.Choscode });

                if (isFlag == 0)
                {
                    groupBox1.Enabled = false;
                    dataGView1.ReadOnly = true;
                    toolStrip1.Enabled = false;
                }
                if (isFlag == 1)
                {
                    XiList = dataGView1.GetData();//存储加载的原数据
                    Deptid_selTextInpt.Enabled = false;
                    SleDeptid_toolStrip.Enabled = false;//工具条上的从科室生成
                }
            }
            if (isFlag == 2)
            {
                //新增过来的时候
            }

            dataGView1.RowToXml += new RowToXmlHandle(dataGView1_RowToXml);
        }

        void dataGView1_RowToXml(RowToXmlEvent e)
        {
            if (e.Data["卡片ID"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【卡片ID】！");
                this.dataGView1.setFocus(e.Row.Index, "卡片ID");
                return;
            }
            if (e.Data["卡片设备"].IsNull)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行必须输入【卡片设备】");
                this.dataGView1.setFocus(e.Row.Index, "卡片设备");
                return;
            }

            if (!WJs.IsNum(e.Data["本月折旧"].ToString()) || e.Data["本月折旧"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【本月折旧】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "本月折旧");
                return;
            }
            if (!WJs.IsNum(e.Data["本月工作量"].ToString()) || e.Data["本月工作量"].ToFloat() <= 0)
            {
                e.IsValid = false;
                WJs.alert("第" + (e.Row.Index + 1) + "行【本月工作量】只能输入数字，并且必须大于0！");
                this.dataGView1.setFocus(e.Row.Index, "本月工作量");
                return;
            }

        }

        private void SleDeptid_toolStrip_Click(object sender, EventArgs e)
        {
            if (this.dataGView1.Rows.Count <= 0)
            {
                if (this.Deptid_selTextInpt.Value != "" && this.Deptid_selTextInpt.Value != null)
                {
                    dataGView1.Rows.Clear();
                    dataGView1.Url = "DeptidTextChange_EQdepreciationEdit";
                    dataGView1.reLoad(new object[] { Deptid_selTextInpt.Value, His.his.Choscode, His.his.Choscode });
                    WJs.alert("导入成功！");
                    SleDeptid_toolStrip.Enabled = false;
                    Deptid_selTextInpt.Enabled = false;
                }
                else
                {
                    WJs.alert("请先选择科室然后进行导入！");
                }
            }
            else
            {
                WJs.alert("该折旧细表内有数据，不能从科室自动生成！");
                return;
            }
        }

        private void Cancel_toolStrip_Click(object sender, EventArgs e)
        {
            if (WJs.confirmQd("确定退出？"))
            {
                this.Close();
            }
        }

        private void del_toolStrip_Click(object sender, EventArgs e)
        {
            Dictionary<string, ObjItem> drNow = dataGView1.getRowData();
            if (drNow != null)
            {
                if (drNow["折旧ID"].IsNull)
                {
                    //新增过来的全部没有折旧ID,这里设置为修改时的新增信息依旧没有
                    dataGView1.Rows.Remove(dataGView1.CurrentRow);
                }
                else
                {
                    if (WJs.confirmFb("针对原本的设备折旧数据的删除时不可恢复的，您确定要删除吗？"))
                    {
                        DataTable dt = GetZJWork_EQDepre(drNow["卡片ID"].ToString());
                        if (dt != null)
                        {
                            DataRow drR = dt.Rows[0];
                            double Result;
                            //如果原本为Null，转换失败，则设置为0，否则肯定直接转换成功
                            if (!double.TryParse(drR["TOTALZJ"].ToString(), out Result))
                            {
                                drR["TOTALZJ"] = "0.00";
                            }

                            if (!double.TryParse(drR["TOTALEDWORK"].ToString(), out Result))
                            {
                                drR["TOTALEDWORK"] = "0.00";
                            }
                            //这样就避免了，当为Null的时候，去更新出错，直接小于就return掉了
                            if (Convert.ToDouble(drR["TOTALZJ"].ToString()) < (drNow["本月折旧"].ToDouble()))
                            {
                                WJs.alert("无法删除该行:卡片累计折旧小于本月折旧，无法删除，否则更新时出现折旧为负数！");
                                return;
                            }
                            if (Convert.ToDouble(drR["TOTALEDWORK"].ToString()) < (drNow["本月工作量"]).ToDouble())
                            {
                                WJs.alert("无法删除该行:累计工作量小于本月工作量，无法删除，否则更新时出现工作量为负数！");
                                return;
                            }
                            LData.Exe("UpdateNumForDeleNotNull_EQDepreManag", null, new object[] { drNow["本月折旧"].ToDouble(), drNow["本月工作量"].ToDouble(), drNow["卡片ID"].ToString(), His.his.Choscode });
                        }
                        else
                        {
                            //这里只要有一个为空，就会进行不下去，难道出现负数？
                            WJs.alert("不存在卡片" + drNow["卡片ID"].ToString() + "的数据，请查看建卡界面！");
                            return;
                        }
                        LData.Exe("DeleteEQReciationInfo_EQDepreEdit", null, new object[] { dr["折旧ID"].ToString(), drNow["卡片ID"].ToString(), His.his.Choscode });
                        dataGView1.Rows.Remove(dataGView1.CurrentRow);
                        WJs.alert("删除成功！");
                    }
                }
            }
            else
            {
                WJs.alert("请选择要删除的设备调拨信息！");
            }
            if (isFlag == 2 && dataGView1.RowCount == 0)
            {
                SleDeptid_toolStrip.Enabled = true;
                Deptid_selTextInpt.Enabled = true;
            }
        }

        private void AddtoolStrip_Click(object sender, EventArgs e)
        {
            if (Deptid_selTextInpt.Value == "" || Deptid_selTextInpt.Text == "")
            {
                WJs.alert("请选择科室！");
                Deptid_selTextInpt.Focus();
                return;
            }

            if (DateId_selTextInp.Value == "" || DateId_selTextInp.Text == "")
            {
                WJs.alert("请选择期间划分！");
                DateId_selTextInp.Focus();
                return;
            }

            dataGView1.addSql("AddZJInfoForEdit_EQdepreciationEdit", "卡片设备", "", Deptid_selTextInpt.Value + "|" + His.his.Choscode + "|" + His.his.Choscode + "|{key}|{key}");

            //添加新行默认值
            Dictionary<string, object> de = new Dictionary<string, object>();
            de["医疗机构编码"] = His.his.Choscode;
            de["操作员ID"] = His.his.UserId.ToString();
            de["操作员姓名"] = His.his.UserName;
            de["修改时间"] = DateTime.Now;
            this.dataGView1.AddRow(de, 0);
            if (dataGView1.RowCount > 0)
            {
                SleDeptid_toolStrip.Enabled = false;
            }
        }

        private bool YanzhengIfCF()//验证是否重复
        {
            List<Dictionary<string, ObjItem>> dList = dataGView1.GetData();
            if (dList == null)
            {
                WJs.alert("细表内没有数据，请加入数据后继续操作！");
                return false;
            }
            List<int> YanZlist = new List<int>();
            foreach (Dictionary<string, ObjItem> item in dList)
            {
                YanZlist.Add(item["卡片ID"].ToInt());
            }
            for (int i = 0; i < YanZlist.Count - 1; i++)
            {
                for (int j = i + 1; j < YanZlist.Count; j++)
                {
                    if (YanZlist[i] == YanZlist[j])
                    {
                        WJs.alert("不能添加相同的卡片ID,请修改后保存！");
                        return false;
                    }
                }
            }
            return true;
        }

        private void Save_toolStrip_Click(object sender, EventArgs e)
        {
            dataGView1.IsAutoAddRow = false;
            if (Deptid_selTextInpt.Value == "" || Deptid_selTextInpt.Text == "")
            {
                WJs.alert("请选择科室！");
                return;
            }
            if (DateId_selTextInp.Value == "" || DateId_selTextInp.Text == "")
            {
                WJs.alert("请选择期间划分信息！");
                return;
            }
            if (dataGView1.RowCount == 0)
            {
                WJs.alert("请输入细表信息！");
                return;
            }

            if (!YanzhengIfCF())
            {
                return;
            }

            List<Dictionary<string, ObjItem>> NowData = dataGView1.GetData();
            // 保存的这里，主要是针对编辑状态下原本数据的更新
            if (isFlag == 1)
            {
                foreach (Dictionary<string, ObjItem> item in NowData)
                {
                    if (item["折旧ID"].IsNull || item["折旧ID"].ToString().Equals(""))
                    {
                        continue;
                    }
                    //循环原始的数据，判断是否存在原始的数据与现在的数据是相同的。
                    foreach (Dictionary<string, ObjItem> itYSJ in XiList)
                    {
                        if (item["卡片ID"].ToString() == itYSJ["卡片ID"].ToString())
                        {
                            //在保存的时候，原数据内的卡片id与现在的卡片id相同，更新数据   若数据无修改，无需更新
                            if (item["本月折旧"].ToDouble() == itYSJ["本月折旧"].ToDouble() && item["本月工作量"].ToDouble() == itYSJ["本月工作量"].ToDouble())
                            {
                                continue;
                            }
                            else
                            {
                                //SELECT TOTALZJ,TOTALEDWORK FROM LKEQ.EQCARDREC WHERE CHOSCODE=? AND CARDID=?
                                DataTable dt = GetZJWork_EQDepre(item["卡片ID"].ToString());
                                if (dt != null)
                                {
                                    DataRow dr = dt.Rows[0];
                                    double Result;
                                    //如果原本为Null，转换失败，则设置为0，否则肯定直接转换成功
                                    if (!double.TryParse(dr["TOTALZJ"].ToString(), out Result))
                                    {
                                        dr["TOTALZJ"] = "0.00";
                                    }

                                    if (!double.TryParse(dr["TOTALEDWORK"].ToString(), out Result))
                                    {
                                        dr["TOTALEDWORK"] = "0.00";
                                    }
                                    if (Convert.ToDouble(dr["TOTALZJ"].ToString()) + item["本月折旧"].ToDouble() - itYSJ["本月折旧"].ToDouble() < 0)
                                    {
                                        WJs.alert("设备卡片" + item["卡片ID"].ToString() + "，在当前折旧的计算下，累计折旧将为负数，请修改后再保存！");
                                        return;
                                    }
                                    if (Convert.ToDouble(dr["TOTALEDWORK"].ToString()) + item["本月工作量"].ToDouble() - itYSJ["本月工作量"].ToDouble() < 0)
                                    {
                                        WJs.alert("设备卡片" + item["卡片ID"].ToString() + "，在当前工作量的计算下，累计工作量为负数，请修改后再保存！");
                                        return;
                                    }
                                }
                                LData.Exe("UpdateNumForNumEdit_EQDepreEidt", null, new object[] { item["本月折旧"].ToDouble(), itYSJ["本月折旧"].ToDouble(), item["本月工作量"].ToDouble(), itYSJ["本月工作量"].ToDouble(), item["卡片ID"].ToString(), His.his.Choscode });
                                break;
                            }
                        }
                    }
                }
            }
            string str = this.dataGView1.GetDataToXml();
            if (str != null)
            {
                ActionLoad ac = ActionLoad.Conn();
                ac.Action = "LKWZSVR.lkeq.UseingEQ.EQDepreciationSvr";
                ac.Sql = "ModifyOrAddInfo";
                AddZhuBiaoInfo(ac);
                ac.Add("XiBiaoXML", str);
                ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
                ac.Post();
            }
            if (isOk)
            {

                WJs.alert("保存信息成功，即将关闭本窗口……");
                this.Close();
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

        private void AddZhuBiaoInfo(ActionLoad ac)
        {
            if (isFlag == 1)
            {
                ac.Add("DEPREID", dr["折旧ID"].ToString());
            }
            else
            {
                ac.Add("DEPREID", "");
            }
            ac.Add("DEPTID", Deptid_selTextInpt.Value);
            ac.Add("DEPRETYPE", ZJType_ComboBox.Value);
            ac.Add("DATEID", DateId_selTextInp.Value);
            ac.Add("MEMO", Memo_textBox.Text);
            ac.Add("STATUS", "1");//因为只能是有效
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName.ToString());
            ac.Add("CHOSCODE", His.his.Choscode.ToString());
        }

        private void dataGView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == CardEQColumn.Index)
            {
                dataGView1.addSql("AddZJInfoForEdit_EQdepreciationEdit", "卡片设备", "", Deptid_selTextInpt.Value + "|" + His.his.Choscode + "|" + His.his.Choscode + "|{key}|{key}");
            }

            if (e.ColumnIndex == NowMonthZJColumn.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["CardEQColumn"].Value == null)
                {
                    WJs.alert("请先选择卡片设备，请在卡片设备内键入空格后选择！");
                    return;
                }
            }

            if (e.ColumnIndex == NowMonthWorkColumn.Index)
            {
                if (this.dataGView1.Rows[e.RowIndex].Cells["CardEQColumn"].Value == null)
                {
                    WJs.alert("请先选择卡片设备，请在卡片设备内键入空格后选择！");
                    return;
                }
            }
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
