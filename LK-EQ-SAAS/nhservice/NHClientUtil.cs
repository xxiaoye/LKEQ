using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YtUtil.tool;
using nhservice.orm;
using System.Xml.Linq;

namespace nhservice
{
   public class NHClientUtil
    {
       public static NHHomeInfo BindNHPeoList(string nhyear,ComboBox com, TextBox nhzh)
       {
           NHHomeInfo peo = null;
           if (nhzh.Text.Trim().Length == 0) {
               WJs.alert("农合证号不能为空！");
               nhzh.Focus();
               return null;
           }
           try
           {
               com.Items.Clear();
               peo = NHClient.GetHomeInfo(nhyear,nhzh.Text.Trim());

               List<NHPeoInfo> lis = NHClient.GetPeoList(nhyear, nhzh.Text.Trim());

               if (lis != null && lis.Count > 0) {
                   com.Items.Add("请选择农合病人");
                   com.DisplayMember = "Xm";
                   foreach (NHPeoInfo p in lis) {
                       com.Items.Add(p);
                   }
                  //com.Text = peo.Hzxm;
                  //Application.DoEvents();
                  //com.SelectedIndex = 1;
                  //com.Focus();
               }
               
           }
           catch (Exception e){
               WJs.alert(e.Message);
               nhzh.Focus();
           }
           return peo;
       }
       public static string GetBrAge(object Csrq) {
           try
           {
               DateTime dt = DateTime.Parse(Csrq.ToString());
               DateTime n  = DateTime.Now;
               if (dt.Year == n.Year && dt.Month == n.Month)
               {
                   return (n.Day - dt.Day) + "天";
               }
               else if (dt.Year == n.Year)
               {
                   return (n.Month - dt.Month) + "月";
               }
               else
               {
                   return (n.Year - dt.Year) + "";
               }
           }
           catch
           {
               return "0";
           } 
       }

       public static List<Dictionary<string, object>> GetParamsByStr(string xml)
       {
           XElement el = XElement.Parse(xml);
           List<Dictionary<string, object>> lis = new List<Dictionary<string, object>>();
           var list = from ls in el.Elements("row") select ls;
           foreach (var it in list)
           {
               Dictionary<string, object> obj = new Dictionary<string, object>();
               foreach (var n in it.Elements())
               {
                   obj[n.Name.ToString()] = n.Value;
               }
               lis.Add(obj);
           }
           return lis;
       }

       public static string[] SubmitMzInfo(string nhyear,RegisterInfo nhInfo, List<RegisterMx> mxLi,decimal zje, out string nhmzh)
       {
           nhmzh = NHClient.SubmitMZInfo(nhyear,nhInfo, mxLi);

           string[] ret = NHClient.MzJs(nhyear,nhInfo.Ylzh, nhmzh, false);

           decimal tol = decimal.Parse(ret[13]);//得到预结算后的总费用;
           if (Math.Abs(tol - zje) > decimal.Parse("0.1"))
           { //出现差额则删除原门诊信息重新上传;
               try
               {
                   NHClient.DelMzXX(nhInfo.Ylzh, nhmzh);
               }
               catch (Exception Ed)
               {
                   WJs.alert("删除前一次门诊暂存数据出错，"+Ed.Message); //throw Ed;
                   return null;
               }
               nhmzh = NHClient.SubmitMZInfo(nhyear,nhInfo, mxLi);
               ret = NHClient.MzJs(nhyear,nhInfo.Ylzh, nhmzh, false);               
           }
           return ret;
       }
       public static string PDError( string RetInfo)
       {   // 判断返回的信息是否错误   
           int ErrID;
           string ErrStr = "SYSTEM_";
           string tt = null;

           if (RetInfo.Length >= ErrStr.Length && RetInfo.Substring(0, 7).Equals(ErrStr) && !"0000".Equals(RetInfo.Replace(ErrStr,"")))
           {
               string error = RetInfo.Replace(ErrStr, "");
               if (WJs.IsNum(error))
               {
                   ErrID = int.Parse(error);
                   switch (ErrID)
                   {
                       case 0: break;
                       case 1: RetInfo = "病人医疗证号错误-nh"; break;
                       case 2: RetInfo = "病人姓名错误-nh"; break;
                       case 3: RetInfo = "病人姓名参合状态错误-nh"; break;
                       case 4: RetInfo = "成员编号在列表中不存在-nh"; break;
                       case 5: RetInfo = "判断暂存错误-nh"; break;
                       case 6: RetInfo = "获取暂存错误-nh"; break;
                       case 7: RetInfo = "获取成员信息错误-nh"; break;
                       case 8: RetInfo = "获取成员信息不存在-nh"; break;
                       case 9: RetInfo = "成员当前状态为空-nh"; break;
                       case 10: RetInfo = "成员当前状态(0. 正常 1. 注销)不正常-nh"; break;
                       case 11: RetInfo = "获取家庭信息错误-nh"; break;
                       case 12: RetInfo = "获取年龄出错-nh"; break;
                       case 13: RetInfo = "住院信息为空-nh"; break;
                       case 14: RetInfo = "上传时住院信息中个人编码错误-nh"; break;
                       case 15: RetInfo = "上传时住院信息中家庭编码错误-nh"; break;
                       case 16: RetInfo = "就诊类型错误-nh"; break;
                       case 17: RetInfo = "入院时间为空-nh"; break;
                       case 18: RetInfo = "出院时间不能在入院时间前-nh"; break;
                       case 19: RetInfo = "就医机构代码错误-nh"; break;
                       case 20: RetInfo = "就医机构类别:不能为空-nh"; break;
                       case 21: RetInfo = "入院科室代码错误-nh"; break;
                       case 22: RetInfo = "出院科室代码错误-nh"; break;
                       case 23: RetInfo = "入院状态代码错误-nh"; break;
                       case 24: RetInfo = "出院状态代码错误-nh"; break;
                       case 25: RetInfo = "疾病代码错误-nh"; break;
                       case 26: RetInfo = "补助账户类别错误-nh"; break;
                       case 27: RetInfo = "补助类别错误-nh"; break;
                       case 28: RetInfo = "核算机构错误-nh"; break;
                       case 29: RetInfo = "要删除的明细的主记录不存在-nh"; break;
                       case 30: RetInfo = "要删除的明细的主记录不是暂存状态-nh"; break;
                       case 31: RetInfo = "计算失败-nh"; break;
                       case 32: RetInfo = "结算失败-nh"; break;
                       case 33: RetInfo = "冲红失败-nh"; break;
                       case 34: RetInfo = "要上传的门诊明细对应的主记录不存在-nh"; break;
                       case 35: RetInfo = "要上传的住院明细对应的主记录不存在-nh"; break;
                       case 36: RetInfo = "病人在其它医院有没有结算的记录-nh"; break;
                       case 37: RetInfo = "计算的住院记录不存在，请检查是否已被删除-nh"; break;
                       case 38: RetInfo = "结算的住院记录不存在，请检查是否已被删除-nh"; break;
                       case 39: RetInfo = "要冲红的记录不存在-nh"; break;
                       case 40: RetInfo = "已结算数据无法进行预结算-nh"; break;
                       case 41: RetInfo = "计算的门诊记录不存在-nh"; break;
                       case 42: RetInfo = "记录已结算或冲毁,不能再上传明细-nh"; break;

                       case 43: RetInfo= "住院数据不存在或者未结算-nh"; break;
                       case 44: RetInfo= "优抚计算失败-nh"; break;
                       case 45: RetInfo= "未进行优抚住院申请 -nh"; break;
                       case 46: RetInfo= "不是优抚补偿定点医疗机构或者定点医疗机构优抚补偿未启用 -nh"; break;
                       case 98: RetInfo= "住院号重复，核实是否已结算-nh"; break;
                       case 99: RetInfo= "机构已禁用-nh"; break; 

                       default:  RetInfo = "未知错误"; break;
                   }
               }
               tt = RetInfo;
           }
          
           return tt;
       }  
    }
}
