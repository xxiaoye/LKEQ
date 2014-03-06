using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using YtClient;
using YiTian.db;
using YtClient.data;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ChSys
{
    public class HisUtils
    {
        /// <summary>
        /// 获取最新票据号
        /// </summary> 
        /// <param name="fptype">票据类型: 1-门诊  2-住院   3-预交款</param>
        /// <returns>票据号码</returns>
        public static string GetFPcode(short fptype)
        {
            if (His.his.IsFpPj)
            {
                ActionLoad dl = ActionLoad.Conn();
                dl.Action = "Find";
                dl.Sql = "sys_getMaxFP";
                dl.SetParams(new object[] { His.his.Choscode, fptype, His.his.UserId });
                string code = dl.Post().GetValue();
                if (code != null)
                    return code;
                else
                    return "";
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 检查发票是否正确
        /// </summary> 
        /// <param name="fptype">票据类型: 1-门诊  2-住院</param>
        /// <param name="fpcode">发票号</param>
        /// <returns>true or false</returns>
        public static Boolean checkMZFP(short fptype, ref string fpcode)
        {
            ActionLoad pdac = ActionLoad.Conn();
            pdac.Action = "LKWZSVR.his.sys.FPop";
            pdac.Sql = "PDinvoice";
            pdac.Add("choscode", His.his.Choscode);
            pdac.Add("kind", fptype);
            pdac.Add("fpcode", fpcode);
            pdac.Add("userid", His.his.UserId);
            ServiceMsg rMsg = pdac.Post();
            fpcode = rMsg.GetValue();
            return rMsg.Msg.Equals("ok"); 
        }

        /// <summary>
        /// 补打发票保存新票据号
        /// </summary> 
        /// <param name="fptype">票据类型: 1-门诊  2-住院   3-预交款</param>
        /// <param name="oldfp">原票号</param>
        /// <param name="newfp">新票号</param>
        /// <param name="sno">发票序号</param>
        /// <param name="code">门诊为处方号，住院为 住院号</param>
        /// <returns>true or false</returns>
        public static Boolean SaveNewFPcode(short fptype, string oldfp,string newfp,string sno,string code)
        {
            ActionLoad pdac = ActionLoad.Conn();
            pdac.Action = "LKWZSVR.his.sys.FPop";
            pdac.Sql = "SaveNewFP";
            pdac.Add("choscode", His.his.Choscode);
            pdac.Add("kind", fptype);
            pdac.Add("oldfp", oldfp);
            pdac.Add("newfp", newfp);
            pdac.Add("sno", sno);
            pdac.Add("code", code);
            pdac.Add("userid", His.his.UserId);
            return pdac.Post().Msg.Equals("ok");
        }

        /// <summary>        
        /// 保存操作日志到库中
        /// <param name="funID">功能号ID</param>
        /// <param name="rem">辅助说明</param>
        /// </summary> 
        public static Boolean SaveOpLog(int funID, string rem)
        {
            //0:业务数据删除  1:药品字典数据删除   2:清除库存数据     3:库存初始化
            //4:单独强制删除住院数据   5:住院退号  6:冲红农合端住院结算 7:领用就诊卡

            ActionLoad pdac = ActionLoad.Conn();
            pdac.Action = "LKWZSVR.his.sys.SysUserSet";
            pdac.Sql = "SaveOpLog";
            pdac.Add("choscode", His.his.Choscode);
            pdac.Add("chosname", His.his.ChosName);
            pdac.Add("username", His.his.UserName);
            pdac.Add("hostinfo", GetUseOpInfo());
            pdac.Add("funID", funID);
            pdac.Add("说明", rem);
            return pdac.Post().Msg.Equals("ok");
        }
 
        /// <summary>        
        /// GetUseOpInfo 获取用户操作日志信息，如IP，MAC 
        /// </summary> 
        public static string GetUseOpInfo()
        {
            string logs = "";
            IPAddress[] IPList = GetLocalIp();
            if (IPList != null)
            {
                for (int i = 0; i < IPList.Length; i++)
                {
                    logs = logs + " IP" + (i + 1) + "=" + IPList[i].ToString();
                }
            }
            logs = logs+"  MAC="+GetMACAddress();
            logs = logs+"  外网IP="+GetIP();
            return logs;
        }

        /// <summary>        
        /// GetLocalIp 获取本机的IP地址        
        /// </summary>        
        public static IPAddress[] GetLocalIp()  
        {   
             string hostName = Dns.GetHostName() ;  
             IPHostEntry hEntry = Dns.Resolve(hostName) ;   
             return hEntry.AddressList ;  
        }
     

     
         /// <summary>      
         /// GetMacAddress 获取本机所有网卡的Mac地址        
        /// </summary>         
        public static string GetMACAddress()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty)     //only return MAC Address from first card   
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        MACAddress = mo["MacAddress"].ToString();
                    }
                }
                else
                {
                    break;
                }
                mo.Dispose();
            }
            MACAddress = MACAddress.Replace(":", "");
            return MACAddress;
        }


        /// <summary>      
        /// GetIP 获取外网IP   
        /// </summary>   
        static string GetIP()
        {
            string ip="";
            try
            {
                string strUrl = "http://iframe.ip138.com/ic.asp"; //获得IP的网址了   http://www.ip138.com/ip2city.asp
                Uri uri = new Uri(strUrl);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据   
                int i = all.IndexOf("[") + 1;
                string tempip = all.Substring(i, 15);
                ip = tempip.Replace("]", "").Replace(" ", "");
                i = all.IndexOf("来自：") + 2;
                tempip = all.Substring(i, all.IndexOf("</center>")-i);
                ip = ip + " " + tempip;
            }
            catch
            {

            }
            return ip;
        }


        /// <summary>
        /// 打开金证健康档案页面
        /// </summary> 
        /// <param name="DoctorID">医生编码</param>
        /// <param name="DoctorName">医生姓名</param>
        /// <param name="Name">医生姓名</param>
        /// <returns>true or false</returns>
        public static int OpenJKDAUrl(string DoctorID, string DoctorName, string Name,
            string SickCode, string ID_NO, string ylzh)
        {
            string url = "ORG_CODE=" + His.his.Choscode + "&DOCTOR_NO=" + DoctorID;
            url += "&DOCTOR_NAME=" + DoctorName + "&NAME=" + Name + "&INCLINICAL_NO=" + SickCode; 
            url += "&ID_NO=" + ID_NO + "&INSURANCE_NO=" + ylzh; 
            url = His.his.jkdaLinkUrl + To16Str(url, "GBK");
            //His.his.jkdaLinkUrl = "http://10.0.11.3/rhin-autologin/sign-in/login.do?DATA=" 
            Process pl = Process.Start("IEXPLORE.EXE", url);//调用IE打开指定网页
            return pl.Handle.ToInt32(); 
        }

        public static string To16Str(string s, string en)
        {
            byte[] gbk = Encoding.GetEncoding(en).GetBytes(s);
            string s1 = ""; string s1d = "";
            foreach (byte b in gbk)
            {
                //s1 += Convert.ToString(b, 16)+" ";
                s1 += string.Format("{0:X2}", b) + "";
                s1d += b + " ";
            }
            return s1;
        }

    }
   
}
