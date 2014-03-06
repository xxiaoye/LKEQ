using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Text;
using YiTian.db;
using System.Collections.Generic;
using YtService.util;
using YtService.config;
using YtService.data;
using System.Text.RegularExpressions;
namespace LKHisSer.his
{
    public class Util
    {

        /// <summary>
        /// 获得ID字符的下一个值，支持非数字
        /// </summary>
        /// <param name="ID">字符值</param>
        /// <param name="Len">需要的长度，如果是非数字，则需要数字部分的长度</param>
        public static string getNextID(string ID, int Len)
        {
            if (ID == null || "".Equals(ID))
            {
                ID = "1";
            }
            Regex objRegex = new Regex(@"[^0-9]");

            if (!objRegex.IsMatch(ID))
            {
                ID = (long.Parse(ID) + 1).ToString();
                return ID.PadLeft(Len, '0');
            }
            else
            {
                string num = objRegex.Replace(ID, "");
                return ID.Replace(num, "") + ("" + (long.Parse(num) + 1)).PadLeft(Len, '0');
            }
        }

        /*
        public static string getNextDJHId(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            string t = id.Substring(0, 2);
            id = id.Substring(2);

            long num = long.Parse(id);
            string next = (num + 1) + "";
            StringBuilder sbf = new StringBuilder();
            for (int i = 0; i < (10 - next.Length); i++)
            {
                sbf.Append("0");
            }
            sbf.Append(next);

            return  t+ sbf.ToString();
        }  

        public static string getNextId2(string id,int l)
        {
            if (id.Length <2)
            {
                return null;
            }
            string t = id.Substring(0, 2);
            id = id.Substring(2);

            long num = long.Parse(id);
            string next = (num + 1) + "";
            StringBuilder sbf = new StringBuilder();
            for (int i = 0; i < (l-2 - next.Length); i++)
            {
                sbf.Append("0");
            }
            sbf.Append(next);

            return t + sbf.ToString();
        }
        */
        
        /// <summary>
        /// 获取住院号
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string getZyNextNum(Dao dao, YtService.data.OptData data) {
            Dictionary<string, object> gz = DaoTool.Get(dao, OptContent.get("ZY_GetDJHGZ"), data);
            if (gz == null) {
                gz = new Dictionary<string, object>();
                gz["前缀规则"] = "YYYYMM";
                gz["主体是否补零"] = "0";
                gz["主体长度"] = "5";
                gz["起始号"] = null;
            }
            string d = "";
            try
            {
                string qz = getPrestring(gz["前缀规则"].ToString());
                string qsh = "";
                if (gz["起始号"] != null)
                    qsh = gz["起始号"].ToString();
                qsh = qsh.Trim();
                int ztlength = new ObjItem(gz["主体长度"]).ToInt();
                if (ztlength == 0) ztlength = 5;
                data.Param["住院号"] = qz + "%";
                data.Param["length"] = qz.Length + ztlength;
            
                 d = DaoTool.ExecuteScalar(dao, OptContent.get("ZY_GetMaxDJH"), data).ToString();
                if (d == null)
                {
                    d = qz + getBegin(qsh, gz["前缀规则"].ToString(), ztlength, gz["主体是否补零"].ToString());
                }
                else
                {
                    if (isLowBegin(d, qsh, gz["前缀规则"].ToString()))
                    {
                        d = qz + getBegin(qsh, gz["前缀规则"].ToString(), ztlength, gz["主体是否补零"].ToString());
                    }
                    else
                    {
                        d = qz + getMaxpre(d, gz["前缀规则"].ToString(), ztlength, gz["主体是否补零"].ToString());
                    }
                }
            }
            catch
            {
                d = "";
            }
            return d;

        }
        private static string getMaxpre(string djh, string gz, int ztlength, string ztbl)
        {
            int length = 0;
            if ("无".Equals(gz))
            {
                length = 0;
            }
            else if ("YY".Equals(gz))
            {
                length = 2;
            }
            else if ("YYYY".Equals(gz))
            {
                length = 4;
            }
            else if ("YYMM".Equals(gz))
            {
                length = 4;
            }
            else if ("YYYYMM".Equals(gz))
            {
                length = 6;
            }
            else
            {
                length = 0;
            }
            String id = djh.Substring(length);
            if (id.Length > ztlength)
            {
                throw new Exception("起始住院号错误id:"+id +" djh:"+djh);
            }
            int lsh = int.Parse(id);
            lsh++;

            id = "" + lsh;

            if ("1".Equals(ztbl))
            {
                return id;
            }
            else
            {
                StringBuilder sbf = new StringBuilder();
                int slength = id.Length;
                for (int i = 0; i < (ztlength - slength); i++)
                {
                    sbf.Append("0");
                }
                sbf.Append(id);
                return sbf.ToString();
            }
        }
        private static string getPrestring(string gz)
        {

            string year = "" + DateTime.Now.Year+"";
            string month = "" + DateTime.Now.Month + "";
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if ("无".Equals(gz))
            {
                return "";
            }
            else if ("YY".Equals(gz))
            {
                return year.Substring(2, 2);
            }
            else if ("YYYY".Equals(gz))
            {
                return year;
            }
            else if ("YYMM".Equals(gz))
            {
                return year.Substring(2, 2) + month;
            }
            else if ("YYYYMM".Equals(gz))
            {
                return year + month;
            }
            else
            {
                return "";
            }
        }
        private static string getBegin(string beginStr, string gz, int ztlength, string ztbl)
        {
            if (beginStr == null || "".Equals(beginStr) || int.Parse(beginStr) == 0)
            {
                if ("1".Equals(ztbl))
                {
                    return "1";
                }
                else
                {
                    string str = "1";
                    StringBuilder sbf = new StringBuilder();
                    for (int i = 0; i < (ztlength - 1); i++)
                    {
                        sbf.Append("0");
                    }
                    return sbf.ToString() + str;
                }
            }
            else
            {
                int length = 0;
                if ("无".Equals(gz))
                {
                    length = 0;
                }
                else if ("YY".Equals(gz))
                {
                    length = 2;
                }
                else if ("YYYY".Equals(gz))
                {
                    length = 4;
                }
                else if ("YYMM".Equals(gz))
                {
                    length = 4;
                }
                else if ("YYYYMM".Equals(gz))
                {
                    length = 6;
                }
                else
                {
                    length = 0;
                }

                string id = beginStr.Substring(length);
                if (id.Length > ztlength)
                {
                    throw new Exception("起始住院号错误id:" + id + " djh:" + ztlength);
                }

                return id;
            }
        }

        private static bool isLowBegin(string djh, string beginStr, string gz)
        {
            bool result = false;
            if (beginStr == null || "".Equals(beginStr) || int.Parse(beginStr) == 0)
            {
                result = false;
            }
            else
            {
                if ("无".Equals(gz))
                {
                    if (int.Parse(djh) < int.Parse(beginStr))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if ("YY".Equals(gz))
                {
                    string id = beginStr.Substring(2);
                    string year = beginStr.Substring(0, 2);
                    string nid = djh.Substring(2);
                    string nyear = djh.Substring(0, 2);
                    if (int.Parse(year) >= int.Parse(nyear))
                    {
                        if (int.Parse(id) > int.Parse(nid))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if ("YYYY".Equals(gz))
                {
                    string id = beginStr.Substring(4);
                    string year = beginStr.Substring(0, 4);
                    string nid = djh.Substring(4);
                    string nyear = djh.Substring(0, 4);
                    if (int.Parse(year) >= int.Parse(nyear))
                    {
                        if (int.Parse(id) > int.Parse(nid))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if ("YYMM".Equals(gz))
                {
                    string id = beginStr.Substring(4);
                    string year = beginStr.Substring(0, 2);
                    string month = beginStr.Substring(2, 2);
                    string nid = djh.Substring(4);
                    string nyear = djh.Substring(0, 2);
                    string nmonth = djh.Substring(2, 2);
                    if (int.Parse(year) >= int.Parse(nyear))
                    {
                        if (int.Parse(month) >= int.Parse(nmonth))
                        {
                            if (int.Parse(id) > int.Parse(nid))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if ("YYYYMM".Equals(gz))
                {
                    string id = beginStr.Substring(6);
                    string year = beginStr.Substring(0, 4);
                    string month = beginStr.Substring(4, 2);
                    string nid = djh.Substring(6);
                    string nyear = djh.Substring(0, 4);
                    string nmonth = djh.Substring(4, 2);
                    if (int.Parse(year) >= int.Parse(nyear))
                    {
                        if (int.Parse(month) >= int.Parse(nmonth))
                        {
                            if (int.Parse(id) > int.Parse(nid))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    if (int.Parse(djh) < int.Parse(beginStr))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 保存员工持款信息
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="data"></param>
        /// <param name="cklx">持款类型 1 挂号 2 门诊 3 住院 4 其他</param>
        /// <param name="je">收款金额</param>
        /// <param name="lsh">流水号 或就诊好</param>
        /// <param name="lx">类型</param>
        /// <param name="allowNull">是否允许持款为空</param>
        public static void SaveYgCk(Dao dao, OptData data,int cklx,decimal je,string lsh,string lx,bool allowNull) {
            Dictionary<string, object> param = DaoTool.Get(dao, OptContent.get("MZ_HaveCKList"), data);
            
            if (param == null)
            {
                if (!allowNull)
                    throw new Exception("员工持款信息无效！");
                data.Param["收款日期"] = DateTime.Now;
                data.Param["累计持款额"] = je;
                data.Param["累计交款额"] = 0;
                if (cklx ==1)
                    data.Param["挂号人数"] = 1;
                if (cklx == 2)
                    data.Param["门诊人数"] = 1;
                if (DaoTool.Save(dao, OptContent.get("MZ_SaveYGCK"), data) < 0)
                {
                    throw new Exception("保存员工持款信息失败！");
                }
            }
            else
            {
                param["收款日期"] = DateTime.Now;
                param["累计持款额"] = (new ObjItem(param["累计持款额"]).ToDecimal() +je);
                if (je > 0)
                {
                    if (cklx == 1)
                        param["挂号人数"] = (new ObjItem(param["挂号人数"]).ToInt() + 1);
                    if (cklx == 2)
                        param["门诊人数"] = (new ObjItem(param["门诊人数"]).ToInt() + 1);
                }
                else {
                    if (cklx == 1)
                        param["挂号人数"] = (new ObjItem(param["挂号人数"]).ToInt() - 1);
                    if (cklx == 2)
                        param["门诊人数"] = (new ObjItem(param["门诊人数"]).ToInt() - 1);
                    decimal ck = new ObjItem(param["累计持款额"]).ToDecimal() + je;
                   // if (ck < 0)
                        //throw new Exception("员工持款额不足于支付退款");
                }
                param["cHosCode"] = data.Param["cHosCode"];
                if (DaoTool.ExecuteNonQuery(dao, OptContent.get("MZ_UpdateYGCKINFO"), param) < 0)
                {
                    throw new Exception("更新员工持款信息失败！");
                }
            }
            data.Param["发生金额"] = je;
            data.Param["发生时间"] = DateTime.Now;
            data.Param["类型"] = lx;
            data.Param["就诊号"] = lsh;
            if (DaoTool.Save(dao, OptContent.get("MZ_SaveYGZJL"), data) < 0)
            {
                throw new Exception("保存员工资金流信息失败！");
            }
        }
        /// <summary>
        /// 保存员工持款信息
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="data"></param>
        /// <param name="cklx">持款类型 1 挂号 2 门诊 3 住院 4 其他</param>
        /// <param name="je">收款金额</param>
        /// <param name="lsh">流水号 或就诊好</param>
        /// <param name="lx">类型</param>
        public static void SaveYgCk(Dao dao, OptData data, int cklx, decimal je, string lsh, string lx) {
             SaveYgCk(dao, data, cklx, je, lsh, lx, true);
        }
        /// <summary>
        /// 更新住院费用 (已经停用,没有使用此功能)
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="cho"></param>
        /// <param name="zyh"></param>
        /// <param name="m">要增加或减少的金额</param>
        public static void UpdateZyJe(Dao dao, string cho, string zyh, decimal m) 
        {
            //object Allm = dao.ExecuteScalar("select 金额 from 住院登记表 where cHosCode=? and 住院号=?", new object[] { cho, zyh });
            //decimal f = 0;
            //if (Allm != null) f = decimal.Parse(Allm.ToString());
            //f = f + m;
            int i = dao.ExecuteNonQuery("update 住院登记表 set 金额=金额+? where cHosCode=? and 住院号=?", new object[] { m, cho, zyh });
            if (i < 0) throw new Exception("更新住院费用失败！");
        }
    }
}
