using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YiTian.db;
using System.IO;
using YtWinContrl.com.datagrid;
using ChSys;
using YtUtil.tool;
using YtClient;

namespace UseingEQ.form
{
    public partial class EQMonthAccountEdit : Form
    {

        Dictionary<string, ObjItem> drZ;
        Dictionary<string, ObjItem> drX;
        int isFlag;//0表示浏览  1 表示月结  2 表示恢复
        bool isOk;
        bool isOk2;//给恢复用

        public EQMonthAccountEdit()
        {
            InitializeComponent();
        }
        public EQMonthAccountEdit(Dictionary<string, ObjItem> drZ, Dictionary<string, ObjItem> drX, int isFlag)
        {
            InitializeComponent();
            TvList.newBind().add("手工折旧", "0").add("自动折旧", "1").Bind(ZJType_ComboBox);
            ZJType_ComboBox.SelectedIndex = 1;
            ZJType_ComboBox.Enabled = false;

            this.DateId_selTextInp.Sql = "FindDateNameInfo_EQDepreciationEdit";
            this.DateId_selTextInp.SelParam = His.his.Choscode + "|{key}|{key}";

            this.drZ = drZ;
            this.drX = drX;
            this.isFlag = isFlag;
            dataGView1.Url = "FirstLoadXiBiaoInfo_EQMonthAccountEdit";

            for (int i = 0; i < dataGView1.Columns.Count; i++)
            {
                dataGView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void LoadInfo_EQMonthAccount()
        {
            DateId_selTextInp.Text = drX["期间划分名称"].ToString();
            DateId_selTextInp.Value = drX["期间划分ID"].ToString();

            Memo_textBox.Text = drX["备注"].ToString();
            ZJType_ComboBox.Value = drX["折旧类型"].ToString();
        }

        private void EQMonthAccountEdit_Load(object sender, EventArgs e)
        {
            //0 为浏览   2 为恢复 [都需要加载对应的折旧ID下的折旧细表数据]
            if (isFlag == 0 || isFlag == 2)
            {
                LoadInfo_EQMonthAccount();
                dataGView1.reLoad(new object[] { drX["折旧ID"].ToString(), His.his.Choscode });
                if (isFlag == 0)
                {
                    QueRen_toolStrip.Visible = false;
                    HuiFu_toolStrip.Visible = false;
                }
                if (isFlag == 2)
                {
                    QueRen_toolStrip.Visible = false;
                }
            }
            //月结处理
            if (isFlag == 1)
            {
                DateId_selTextInp.Text = drZ["期间"].ToString();
                DateId_selTextInp.Value = drZ["期间ID"].ToString();

                HuiFu_toolStrip.Visible = false;

                //SELECT a.CARDID ,a.EQNAME,a.TOTALWORK,a.TOTALEDWORK, a.TOTALZJ ,a.MEMO,
                //a.CHOSCODE ,a.USERID, a.USERNAME,a.RECDATE,a.YPRICE,a.CZRATE,a.USEYEAR,
                //b.ZJTYPE,b.ZJRATE
                //FROM LKEQ.EQCARDREC a, LKEQ.DICTEQ b
                //WHERE a.CHOSCODE=? AND a.EQID=b.EQID AND a.EQID IN( SELECT EQID FROM LKEQ.DICTEQ WHERE ZJTYPE!=4 AND CHOSCODE=? )
                this.dataGView1.Url = "ReLoadInformation_EditEQMonthAccount";
                this.dataGView1.reLoad(new object[] { His.his.Choscode, His.his.Choscode });
                UpdateZJWorkValue();
            }
        }

        private void UpdateZJWorkValue()
        {
            int a = this.dataGView1.RowCount;
            if (a <= 0)
            {
                return;
            }
            double result = 0.00;
            for (int i = 0; i < a; i++)
            {
                //原值  
                if (!double.TryParse(this.dataGView1["YZColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["YZColumn", i].Value = "0.00";
                }
                //预计残值
                if (!double.TryParse(this.dataGView1["YJCZColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["YJCZColumn", i].Value = "0.00";
                }
                //预计使用年限
                if (!double.TryParse(this.dataGView1["YJSYNXColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["YJSYNXColumn", i].Value = "0.00";
                }
                //年折旧率
                if (!double.TryParse(this.dataGView1["NZJLColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["NZJLColumn", i].Value = "0.00";
                }
                //工作量总额==总工作量
                if (!double.TryParse(this.dataGView1["ZGZLColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["ZGZLColumn", i].Value = "0.00";
                }
                //累计工作量 ==工作量   
                if (!double.TryParse(this.dataGView1["LJGZLColumn", i].Value.ToString(), out result))
                {
                    this.dataGView1["LJGZLColumn", i].Value = "0.00";
                }

                if (this.dataGView1["ZJTypeColumn", i].Value.ToString() == "1")
                {
                    this.dataGView1["NowMonthZJColumn", i].Value = (double.Parse(this.dataGView1["YZColumn", i].Value.ToString()) * (1 - double.Parse(this.dataGView1["YJCZColumn", i].Value.ToString())) / double.Parse(this.dataGView1["YJSYNXColumn", i].Value.ToString()) / 12).ToString("f4");
                    this.dataGView1["NowMonthWorkColumn", i].Value = (double.Parse(this.dataGView1["ZGZLColumn", i].Value.ToString()) * (1 - double.Parse(this.dataGView1["YJCZColumn", i].Value.ToString())) / double.Parse(this.dataGView1["YJSYNXColumn", i].Value.ToString()) / 12).ToString("f4");
                }
                if (this.dataGView1["ZJTypeColumn", i].Value.ToString() == "2")
                {
                    this.dataGView1["NowMonthZJColumn", i].Value = (double.Parse(this.dataGView1["YZColumn", i].Value.ToString()) * (1 - double.Parse(this.dataGView1["YJCZColumn", i].Value.ToString())) / double.Parse(this.dataGView1["ZGZLColumn", i].Value.ToString()) * double.Parse(this.dataGView1["LJGZLColumn", i].Value.ToString())).ToString("f4"); ;
                    this.dataGView1["NowMonthWorkColumn", i].Value = (double.Parse(this.dataGView1["ZGZLColumn", i].Value.ToString()) * (1 - double.Parse(this.dataGView1["YJCZColumn", i].Value.ToString())) / double.Parse(this.dataGView1["LJGZLColumn", i].Value.ToString())).ToString("f4");
                }
                if (this.dataGView1["ZJTypeColumn", i].Value.ToString() == "3")
                {
                    this.dataGView1["NowMonthZJColumn", i].Value = (double.Parse(this.dataGView1["YZColumn", i].Value.ToString()) * double.Parse(this.dataGView1["NZJLColumn", i].Value.ToString()) / 12).ToString("f4");
                    this.dataGView1["NowMonthWorkColumn", i].Value = (double.Parse(this.dataGView1["ZGZLColumn", i].Value.ToString()) * double.Parse(this.dataGView1["NZJLColumn", i].Value.ToString()) / 12).ToString("f4");
                }
            }
        }

        private void Close_toolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QueRen_toolStrip_Click(object sender, EventArgs e)
        {
            if (dataGView1.RowCount <= 0)
            {
                WJs.alert("当前折旧细表内无数据，不能进行月结信息！");
                return;
            }
            //确认的时候先保存折旧记录  然后更新对应的数据
            string XmlData = this.dataGView1.GetDataToXml();
            ActionLoad ac = ActionLoad.Conn();
            ac.Action = "LKWZSVR.lkeq.UseingEQ.EQMonthAccountSvr";
            ac.Sql = "QueRenYueJie";
            ac.Add("XmlDataList", XmlData);
            ac.Add("BEGINDATE", drZ["开始日期"].ToDateTime());
            ac.Add("ENDDATE", drZ["结束日期"].ToDateTime());
            AddZhuBiaoInfo(ac);
            ac.ServiceLoad += new YtClient.data.events.LoadEventHandle(ac_ServiceLoad);
            ac.Post();
            if (isOk)
            {
                WJs.alert("月结成功,即将关闭本窗口！");
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
            ac.Add("DEPREID", "");
            ac.Add("DEPTID", null);
            ac.Add("DEPRETYPE", ZJType_ComboBox.Value);
            ac.Add("DATEID", DateId_selTextInp.Value);
            ac.Add("MEMO", Memo_textBox.Text);
            ac.Add("STATUS", "1");//因为只能是有效
            ac.Add("USERID", His.his.UserId.ToString());
            ac.Add("USERNAME", His.his.UserName.ToString());
            ac.Add("CHOSCODE", His.his.Choscode);
        }

        private void HuiFu_toolStrip_Click(object sender, EventArgs e)
        {
            //主要是将数据返回回去即可
            this.dataGView1.IsAutoAddRow = false;
            List<Dictionary<string, ObjItem>> drList = this.dataGView1.GetData();
            if (drList == null || drList.Count <= 0)
            {
                WJs.alert("该月结单细表内无数据，无需恢复！");
                return;
            }
            foreach (Dictionary<string, ObjItem> item in drList)
            {
                if (item["启用日期"].IsNull == true)//尚未启用
                {
                    continue;
                }
                else
                {
                    //启用日期不为null  且为本月的
                    if (item["启用日期"].ToDateTime().CompareTo(drZ["开始日期"].ToDateTime()) > 0 && item["启用日期"].ToDateTime().CompareTo(drZ["结束日期"].ToDateTime()) < 0)
                    {
                        continue;
                    }
                    if (item["启用日期"].ToDateTime().CompareTo(drZ["结束日期"].ToDateTime()) > 0)
                    {
                        continue;
                    }
                }

                if (item["报废日期"].IsNull == false)//报废日期不为空
                {
                    //报废日期不为空  当报废日期小于本月开始日期就无需恢复 [不为本月报废]
                    if (item["报废日期"].ToDateTime().CompareTo(drZ["开始日期"].ToDateTime()) < 0)
                    {
                        continue;
                    }
                }
                //获取卡片ID内实时的数据
                DataTable dt = GetZJWork_EQDepre(item["卡片ID"].ToString());
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
                    if (Convert.ToDouble(drR["TOTALZJ"].ToString()) < (item["本月折旧"].ToDouble()))
                    {
                        WJs.alert("无法进行月结:卡片" + item["卡片ID"].ToString() + "累计折旧小于本月折旧，无法删除，否则更新时出现折旧为负数！");
                        return;
                    }
                    if (Convert.ToDouble(drR["TOTALEDWORK"].ToString()) < (item["本月工作量"]).ToDouble())
                    {
                        WJs.alert("无法进行月结:累计工作量小于本月工作量，无法删除，否则更新时出现工作量为负数！");
                        return;
                    }
                    LData.Exe("UpdateNumForDeleNotNull_EQDepreManag", null, new object[] { item["本月折旧"].ToDouble(), item["本月工作量"].ToDouble(), item["卡片ID"].ToString(), His.his.Choscode });
                    isOk2 = true;
                }
                else
                {
                    //这里只要有一个为空，就会进行不下去，难道出现负数？
                    WJs.alert("不存在卡片" + item["卡片ID"].ToString() + "的数据，请查看建卡界面！");
                    return;
                }
            }
            //设置为无效
            LData.Exe("UpdateStatusInfo_EQDepreciationManag", null, new object[] { "0", drX["折旧ID"].ToString(), His.his.Choscode });
            WJs.alert("恢复成功，即将关闭本窗口……");
            this.Close();

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
