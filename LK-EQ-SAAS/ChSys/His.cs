using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ChSys
{
   public  class His
   {
       /// <summary>
       /// 当前登录的单位信息
       /// </summary> 
       /// 
       
       public string Choscode  { get; set; }
       public string ChosName { get; set; }
       public string WsjCode { get; set; }
       public static bool Fixedflag = false; 
    
       public int     UserId { get; set; }
       public string  Useraccount { get; set; } 
       public string UserName { get; set; }
       public string postName { get; set; }  //职务名称
       public string DeptName { get; set; }  //所属科室名称
       public string DeptID { get; set; }    //所属科室ID
       public string DoctorID { get; set; }  //所属医生ID
       public bool Iswsj { get; set; }
       public string Nhjb { get; set; }
       public string Nhhospcode { get; set; }
       public string Xcode { get; set; }
       public string Zcode { get; set; }
       public string Ccode { get; set; }
       public string areacode { get; set; } 
       public string Supercode { get; set; }
       public string Rank { get; set; }        // 医院级别
       public string Nhperfix { get; set; }    // 农合医疗证号前缀
       public string MZFPCode { get; set; }    // 门诊发票号
       public string ZYFPCode { get; set; }    // 住院发票号
       public string NHLinkUrl { get; set; }   // 农合连接地址
       public string jkdaLinkUrl { get; set; } // 健康档案查阅连接地址
       public bool IsNotKF { get; set; }       // 是否不控制库房(true:不控制  false:控制)
       public bool IsUseYZ { get; set; }       // 是否使用住院医嘱功能
       public bool IsYZAutoMB { get; set; }    // 是否使用住院医嘱自动加载模板功能
       public bool IsFpPj { get; set; }        // 是否使用发票票据
       public bool UseBedbg { get; set; }      // 是否使用床位包干

       public bool SetPrintCF { get; set; }     // 打印门诊处方笺标志

       public DateTime WebDate { get; set; }     // Web服务器时间

       public static DataTable PRMDT;
       public static DataTable HisParam;

       public static DataTable wsjYBParam; // 卫生局医保设置参数
       public static DataTable YBParam;    // 医疗机构医保设置参数

       public static DataTable TabtsYjj;   // 特殊预交金处理表

       public string YZPnum { get; set; }  // 医嘱单打印每页记录数

      // public static Dictionary<string, object> ybID = new Dictionary<string, object>(); //设置医保ID


       /// <summary>
       /// 获取系统参数信息
       /// </summary>
       /// <param name="pname"></param>
       /// <returns></returns>
       public static object HisSysParam(int i)
       {
           if (HisParam == null || HisParam.Rows.Count == 0) return null;
           foreach (DataRow r in HisParam.Rows) {
               if ((i + "").Equals(r[0])) return r[1];
           }
           return null;
       }

       /// <summary>
       /// 判断是否有此医保启用
       /// </summary>
       /// <param name="ybid">医保所属ID</param>
       /// <returns></returns>
       public static Boolean IfUseYB(int ybid,bool ifWSJ)
       { 
           if (!ifWSJ)
           {
               if (YBParam != null && YBParam.Rows.Count > 0)
               {
                   foreach (DataRow r in YBParam.Rows)
                   {
                       if ((ybid + "").Equals(r[1]) && r[7].Equals("1")) return true;
                   }
               }
           }
           else
           {
               if (wsjYBParam != null && wsjYBParam.Rows.Count > 0)
               {
                   foreach (DataRow r in wsjYBParam.Rows)
                   {
                       if ((ybid + "").Equals(r[1]) && r[7].Equals("1")) return true;
                   }
               }
           }
           return false;
       }

       /// <summary>
       /// 获取系统参数信息
       /// </summary>
       /// <param name="pname"></param>
       /// <returns></returns>
       public static string  GetSysParam(string pname){
           if (PRMDT == null)
           {
               return "";
           }
           foreach (DataRow r in PRMDT.Rows)
           {
               if (r["名称"].ToString().ToUpper().Equals(pname.ToUpper())) {
                   return r["值"].ToString();
               }
           }
           return "";
       }
     
       public static His his = new His();
       public static int GetSysRoleKind() {
           return 2;
       }

       public static DataTable Right;
       /// <summary>
       /// 判断是否有相应的权限
       /// </summary>
       /// <param name="rid"></param>
       /// <returns></returns>
       public static bool HaveRight(int rid) {
           if (Right == null) return false;
           foreach (DataRow r in Right.Rows) {
               int i = int.Parse(r[0].ToString());
               if (i == rid) return true;
           }
           return false;
       }
       /// <summary>
       /// 挂号有效天数
       /// </summary>
       public static int HP_GHYXTS = 1;

       /// <summary>
       /// 门诊发票使用套打
       /// </summary>
       public static int HP_MZFPDYGS = 3;
       /// <summary>
       /// 是否打印住院预交金
       /// </summary>
       public static int HP_DYZYYJJ = 4;
       /// <summary>
       /// 住院预交金打印格式
       /// </summary>
       public static int HP_ZYYJJDYGS = 5;
       /// <summary>
       /// 是否打印住院结算发票
       /// </summary>
       public static int HP_SFDYZYJSFP = 6;
       /// <summary>
       /// 住院结算发票打印格式
       /// </summary>
       public static int HP_ZYJSFPDYGS = 7;

       /// <summary>
       /// 门诊发票明细打印
       /// </summary>
       public static int HP_MZFPDYFS = 9;

       /// <summary>
       /// 是否可可操作库房
       /// </summary>
       public static int HP_SFKZZKF = 10;

       /// <summary>
       /// 划价未收费处方保存天数
       /// </summary>
       public static int HP_HJWSFCFBCTS = 11;

       /// <summary>
       /// 收费后是否自动发药
       /// </summary>
       public static int HP_AUTOFY = 12;

       /// <summary>
       /// 住院入院登记提前的天数
       /// </summary>
       public static int zy_InHospDay = 13;

       /// <summary>
       /// 启用门诊电子处方
       /// </summary>
       public static int sys_mzdzcf = 15;

       /// <summary>
       ///  启用床位包干补偿
       /// </summary>
       public static int sys_BedBg = 16;

       /// <summary>
       ///  收费后是否打印三联单
       /// </summary>
       public static int nh_Print3LD = 17;

       /// <summary>
       ///  门诊电子处方打印
       /// </summary>
       public static int sys_mzdzcfPrint = 18;

       /// <summary>
       ///  启用票据管理
       /// </summary>
       public static int sys_PJmagr = 19; 


   }
}
