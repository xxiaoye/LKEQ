using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using YtService.action;
using YtService.config;

namespace LKWZSVR.his.sys
{
    public class GetHisParam:IEx
    {
        #region IEx 成员
         
        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string ccode = data.Param["cHosCode"].ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("名称"));
            dt.Columns.Add(new DataColumn("值"));

               /*string supcode = "" ;
               if (ccode.Trim().Length > 7)
               {
                   supcode = ccode.Substring(0, ccode.Length - 2);
               }
               else {
                   supcode = ccode;
               } 
                //获取农合前缀
               DataRow  r =  dt.NewRow();
                object qz = dao.ExecuteScalar("select nhperfix from SYSDICTHOSPITAL where cHosCode=?", new object[] { supcode });
               if (qz == null)
               {
                   r["名称"] = "NHPREFIX";
                   r["值"] = "";
               }
               else {
                   r["名称"] = "NHPREFIX";
                   r["值"] = qz.ToString();
               }
               dt.Rows.Add(r); 
           
               object NHHOSCODE = dao.ExecuteScalar("select nhhospcode from SYSDICTHOSPITAL where cHosCode=?", new object[] { ccode });
               DataRow rd = dt.NewRow();
               if (NHHOSCODE == null)
               {
                   rd["名称"] = "NHHOSCODE";
                   rd["值"] = "";
               }
               else
               {
                   rd["名称"] = "NHHOSCODE";
                   rd["值"] = NHHOSCODE.ToString();
               }
               dt.Rows.Add(rd);

               object areacode = dao.ExecuteScalar("select areacode from SYSDICTHOSPITAL where cHosCode=?", new object[] { ccode });
               DataRow rd1 = dt.NewRow();
               if (areacode == null)
               {
                   rd1["名称"] = "AREACODE";
                   rd1["值"] = "";
               }
               else
               {
                   rd1["名称"] = "AREACODE";
                   rd1["值"] = areacode.ToString();
               }
               dt.Rows.Add(rd1);   */

            msg = "OK";            

            DataTable hisYBParam = dao.find("select t.* from 医保设置表 t where t.choscode=?", new object[] { ccode });

            string sql = OptContent.get("Get_SuperCode").Sql;
            DataTable sup = dao.find(sql, new object[] { ccode });
            string wsjcode = sup.Rows[0][0].ToString();

            DataTable hisPa = dao.find("select ID,sysvalue from 系统参数 t where (choscode is null or choscode=? or choscode=?) and not exists" +
                                       "(select * from 系统参数 where (choscode=? or choscode=?) and t.id=id and length(nvl(t.choscode,' '))<length(choscode) )",
                                        new object[] { ccode, wsjcode, ccode, wsjcode });  //优先取医院设置的系统参数，再到卫生局设置的系统参数，再取全省系统参数

            DataRow rdwsj  = dt.NewRow();
            DataRow usnode = dt.NewRow();
            DataRow rdnhperfix = dt.NewRow();
            rdwsj["名称"] = "WSJCODE";
            usnode["名称"] = "USEZBY";
            rdnhperfix["名称"] = "NHPERFIX";
            if (sup != null)
            {
                rdwsj["值"] = sup.Rows[0][0].ToString();
                rdnhperfix["值"] = sup.Rows[0][2].ToString();
                if (sup.Rows[0][1] != null && sup.Rows[0][1].ToString().Length > 0)
                    usnode["值"] = sup.Rows[0][1].ToString();
                else
                    usnode["值"] = "0";
            }
            else
            {               
                rdwsj["值"] = "";
                usnode["值"] = "0";
                rdnhperfix["值"] = "";
            }
            dt.Rows.Add(rdwsj);
            dt.Rows.Add(usnode);
            dt.Rows.Add(rdnhperfix);
            return new object[] { dt, hisPa, sup, hisYBParam };
        }

        #endregion
    }
}
