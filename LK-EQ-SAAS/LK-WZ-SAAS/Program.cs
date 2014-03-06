using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YTMain;
using System.Drawing;
using YtClient.data.events;
using YtSys;
using System.Data;
using YtClient;
using YtClient.data;
using YiTian.db;
using YtUtil.tool;
using ChSys;
using JiChuDictionary;
namespace LK_WZ_SAAS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        
        [STAThread]
        
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Dict());

            SysSet.IsRunUnInstall = false;   //是否强制卸载
            SysSet.UpdateAddress = "http://hiscs.gzwsxxh.com:82/install";  //更新路径

            SysSet.LoginImg = new Bitmap("Ico/login.jpg");
            SysSet.TopImg = new Bitmap("Ico/top.jpg");
            SysSet.MainImg = new Bitmap("Ico/main.jpg");
            SysSet.LinkTel = "服务热线: 110 ";//4006613506转1 
            SysSet.LinkQQ = "企业QQ：QQNUM";//800013506
            //SysSet.LinkTel = "";
            //SysSet.LinkQQ = "";
            SysSet.MainIco = new Icon("Ico/main.ICO");
            //SysSet.NotifyIco = new Icon("Ico/notify.ico");
            SysSet.PtLogin = false;//登录模式
            SysSet.SjLogin = true;//手机登录 
            SysSet.CheckSuo = false;//加密锁检查 

            SysSet.MenuName = "allmenu";//不知道干啥的
            //SysSet.MenuName = "lkwzmenu";//不知道干啥的

            SysSet.SysName = "联科医院信息系统( WZ )";
            SysSet.SysPath = "联科软件";
            SysSet.MainTitle = "联科医院信息系统( WZ )";
            SysSet.UserInputLoc = new Point(25, 50);
            SysSet.NotifyShow = false;
            SysSet.LoginInc = "LKWZSVR.Login";//提交到服务端的那个函数里面
            //SysSet.InitPlug = "LkEmr.EmrInit";
            SysSet.IsUpdate = false;
            SysSet.UpdateDbConn = "DbEmr";
            SysSet.UpdateOpenWin = false;
            SysSet.RunNewMain = true;
            SysSet.MySelfRunLogin = true;//全部执行自己的登录信息


            SysSet.MenuWidth = 188;
            //状态栏新增点击按钮
            SysSet.BottomUrls.Add(new UrlLink("农合后台系统", "http://xnh.gzxnh.gov.cn/guizhou/"));//主窗体底部添加信息


            SysSet.LoginAdSuc = new LoginSucHandle(delegate(LoadEvent e, string sjh, string userPwd, Sys login)
            {
                His.his.Useraccount = login.Useraccount;
                His.his.UserId = 0;
                His.his.ChosName = login.DepName;
                His.his.Choscode = login.DepCode;
                His.his.UserName = login.UserName;
            });
            SysSet.LoginSuc = new LoginSucHandle(delegate(LoadEvent e, string sjh, string userPwd, Sys login)
            {

                DataTable dt = e.Msg.GetDataTable("dataTwo");
                DataTable yljg = e.Msg.GetDataTable("data2");
                DataTable LinkInfo = e.Msg.GetDataTable("data3");
                DataTable MsgTab = e.Msg.GetDataTable("data4");

                // 初始化医疗机构信息
                His.his.Choscode = dt.Rows[0]["Choscode"].ToString();
                His.his.ChosName = yljg.Rows[0]["cHosName"].ToString();
                His.his.Useraccount = dt.Rows[0]["Useraccount"].ToString().Replace(His.his.Choscode, "");
                His.his.UserName = dt.Rows[0]["Name"].ToString();
                His.his.UserId = int.Parse(dt.Rows[0]["UserID"].ToString());
                //His.his.postName = dt.Rows[0]["职务"].ToString();
                His.his.DeptName = dt.Rows[0]["科室名称"].ToString();
                //His.his.DeptID = dt.Rows[0]["科室ID"].ToString();
                //His.his.DoctorID = dt.Rows[0]["医生ID"].ToString();

                His.his.Iswsj = ("1".Equals(yljg.Rows[0]["Iswsj"].ToString()));
                His.his.Nhjb = yljg.Rows[0]["NHJB"].ToString();
                His.his.Nhhospcode = yljg.Rows[0]["nhhospcode"].ToString();
                His.his.Xcode = yljg.Rows[0]["XCODE"].ToString();
                His.his.Zcode = yljg.Rows[0]["Zcode"].ToString();
                His.his.Ccode = yljg.Rows[0]["Ccode"].ToString();
                His.his.areacode = yljg.Rows[0]["AreaCode"].ToString();
                His.his.Supercode = yljg.Rows[0]["Supercode"].ToString();
                His.his.Rank = yljg.Rows[0]["zlxjlb"].ToString().Trim();
                if (His.his.Rank.Equals(""))
                {
                    if (His.his.Iswsj)
                        His.his.Rank = "1";
                    else
                        His.his.Rank = "2";
                }

                if (LinkInfo != null)
                {
                    His.his.NHLinkUrl = LinkInfo.Rows[0]["LINK"].ToString();  // 农合连接地址
                    His.his.jkdaLinkUrl = LinkInfo.Rows[1]["LINK"].ToString();  // 健康档案查阅连接地址
                }

                if (dt.Rows[0]["Fixedflag"] != null)
                {
                    His.Fixedflag = int.Parse(dt.Rows[0]["Fixedflag"].ToString()) == 1;
                }
                else His.Fixedflag = false;

                ActionLoad load = ActionLoad.Conn();
                load.Action = "LKWZSVR.his.sys.GetHisParam";
                load.Add("cHosCode", His.his.Choscode);
                ServiceMsg e1 = load.Post();
                His.PRMDT = e1.GetDataTable();
                His.HisParam = e1.GetDataTable("dataTwo");
                His.wsjYBParam = e1.GetDataTable("data2");
                His.YBParam = e1.GetDataTable("data3");

               // NHClient.NHHOSCODE = His.his.Nhhospcode;         // His.GetSysParam("NHHOSCODE");
                His.his.WsjCode = His.GetSysParam("WSJCODE");
                His.his.Nhperfix = His.GetSysParam("NHPERFIX");
                His.his.IsNotKF = new ObjItem(His.HisSysParam(His.HP_SFKZZKF)).ToInt() != 1;
                His.his.IsUseYZ = new ObjItem(His.HisSysParam(27)).ToInt() == 1;
                His.his.IsFpPj = new ObjItem(His.HisSysParam(His.sys_PJmagr)).ToInt() == 1;
                His.his.UseBedbg = new ObjItem(His.HisSysParam(His.sys_BedBg)).ToInt() == 1;
                His.his.IsYZAutoMB = new ObjItem(His.HisSysParam(28)).ToInt() == 1;
                His.his.YZPnum = His.HisSysParam(30).ToString();

                login.DepCode = His.his.Choscode;
                login.DepName = His.his.ChosName;
                login.UserID = His.his.UserId + "";
                login.Useraccount = His.his.Useraccount;
                login.UserName = His.his.UserName;
                login.XName = His.his.UserName;
                login.DeptName = His.his.DeptName;
                login.Tel = sjh;
                YtSys.Ui.User = login;
                ClientState.UseName = His.his.Choscode + "" + His.his.Useraccount;
                ClientState.Pwd = userPwd;
                string fwqDate = e.Msg.GetValue("data5").Trim();
                DateTime d = DateTime.Parse(fwqDate);//同步服务器时间
                His.his.WebDate = d;

                TimeSpan t = d - DateTime.Now;
                if (Math.Abs(t.TotalMinutes) > 2)
                {
                    try
                    {
                        Cmd.SetSysTime(short.Parse(d.Year + ""), short.Parse(d.Month + ""), short.Parse(d.Day + ""), short.Parse(d.Hour + ""), short.Parse(d.Minute + ""), short.Parse(d.Second + ""));
                    }
                    catch
                    {
                        WJs.alert("同步本机时间与服务器时间失败，请手动更改本机时间！\n服务器当前时间为【" + d.ToString("yyyy-MM-dd HH:ss") + "】");
                        return;
                    }
                }
                His.Right = e.Msg.GetDataTable();//获取用户权限

                // loadDataDict();


                if ("123456".Equals(userPwd))
                {
                    WJs.alert("您是用初始密码登陆，请您尽快修改密码！");
                }


                //------------ 系统消息显示 -------------------
                if (!His.his.Iswsj && MsgTab != null && MsgTab.Rows.Count > 0)  //消息显示
                {
                    WJs.alert(MsgTab.Rows[0]["msg"].ToString());
                }

                //------------- 机构设置消息显示 --------------
                //if (new ObjItem(yljg.Rows[0]["flag"].ToString()).ToInt() > 0)
                //{                 
                //    InfoFrm fm = new InfoFrm(yljg.Rows[0]["LICENCE"].ToString());
                //    fm.ShowDialog();
                //    if (new ObjItem(yljg.Rows[0]["flag"].ToString()).ToInt() == 3)//禁用系统
                //        Application.Exit();
                //}

                if (DataCache.serviceData != null)
                    DataCache.serviceData.Clear();

                //设置提示信息；
                string jbstr;
                if (His.his.Rank.Equals("1"))
                    jbstr = "县级";
                else if (His.his.Rank.Equals("3"))
                    jbstr = "村级";
                else
                {
                    if (!His.his.Iswsj)
                        jbstr = "乡镇级";
                    else
                        jbstr = "县级以上";
                }

                if (!His.his.Rank.Equals(His.his.Nhjb) && !His.his.Iswsj)
                {
                    if (His.his.Nhjb.Equals("1"))
                        jbstr = jbstr + "(农合级别:县级)";
                    else if (His.his.Rank.Equals("3"))
                        jbstr = jbstr + "、(农合级别:村级)";
                    else if (His.his.Nhjb.Equals("2"))
                        jbstr = jbstr + "、(农合级别:乡镇级)";
                    else
                        jbstr = jbstr + "、(农合级别:未设置)";
                }

                jbstr = " 【收费标准:" + jbstr + "】 ";

                SysSet.BottomAlert = "          当前机构：" + His.his.ChosName + jbstr + " ,          用户：" + His.his.UserName +
                                     " 、" + His.his.DeptName + " 、" + His.his.postName + "          ";

            });

            YTMain.RunMain.Main();
        }
    /*
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WZWareManag());
        }
     * */
     
    }


}
