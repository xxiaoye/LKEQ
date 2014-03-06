using System;
using System.Data;
using System.Configuration;
using System.Linq;


using System.Xml.Linq;
using YtService.action;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.sys
{
    public class FPop:IEx  //票据操作
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string A = data.Sql; 
            if ("Del".Equals(A)) 
             {
                 object p = dao.ExecuteScalar("select count(*) num from 票据领用表 where 领用序号 = " +
                                              data.Param["serialno"].ToString() + " and 领用张数>剩余张数");
                 if (p != null && int.Parse(p.ToString()) > 0)
                     throw new Exception("当前领用的票据已经被使用，不能再删除!");

                 if (dao.ExecuteNonQuery("delete 票据领用表 where 领用序号=" + data.Param["serialno"].ToString()) < 0)
                 {
                     throw new Exception("领用票据删除失败！");
                 }
             }
            else if ("PDinvoice".Equals(A))
            {
                object p = dao.ExecuteScalar("select 领用序号 from 票据领用表 where choscode ='" +
                                              data.Param["choscode"].ToString() + "' and 剩余张数>=1 and 票据类型="+
                                              data.Param["kind"].ToString() +" and length(开始票号)="+data.Param["fpcode"].ToString().Length+
                                              " and '"+data.Param["fpcode"].ToString()+"' between 开始票号 and 结束票号 and "+
                                              "((使用模式=1) or (使用模式=0 and 领用人员="+data.Param["userid"].ToString()+"))");
                if (p == null || p.ToString().Equals(""))
                    throw new Exception(data.Param["fpcode"].ToString()+"票据号未领用过，不能被使用!");

                object x = dao.ExecuteScalar("select count(*) cn from 票据使用表 where choscode ='" +
                                              data.Param["choscode"].ToString() + "' and 票据类型="+
                                              data.Param["kind"].ToString() +" and 票号='"+data.Param["fpcode"].ToString()+"'");
                if (x != null && int.Parse(x.ToString()) > 0)
                    throw new Exception(data.Param["fpcode"].ToString() + "票据号已经被使用过，不能重复使用!");

                msg = "ok";
                return p.ToString();
            }
            else if ("SaveNewFP".Equals(A))
            {
                if (data.Param["kind"].ToString().Equals("1")) //门诊
                {
                    if (dao.ExecuteNonQuery("update 门诊处方表 set 发票号=? where choscode=? and 处方号=?"
                           , new object[] { data.Param["newfp"], data.Param["choscode"], data.Param["code"] }) < 0)
                        throw new Exception("改写门诊处方发票号错误！");
                }
                else if (data.Param["kind"].ToString().Equals("2"))//住院
                {
                    if (dao.ExecuteNonQuery("update 住院登记表 set 发票号=? where choscode=? and 住院号=?"
                           , new object[] { data.Param["newfp"], data.Param["choscode"], data.Param["code"] }) < 0)
                        throw new Exception("改写住院发票号错误！");
                }
                else //预交款票据
                {
                    if (dao.ExecuteNonQuery("update 押金表 set 发票号=? where choscode=? and 住院号=? and 发票号=?"
                           , new object[] { data.Param["newfp"], data.Param["choscode"], data.Param["code"],
                                            data.Param["oldfp"].ToString()}) < 0)
                        throw new Exception("改写预交款票号错误！");
                }

                if (dao.ExecuteNonQuery("insert into 票据使用表(领用序号,票据类型,票号,操作员,choscode,code) values(" +
                                        data.Param["sno"].ToString() + "," + data.Param["kind"].ToString() + ",'" +
                                        data.Param["newfp"].ToString() + "'," + data.Param["userid"].ToString() + ",'" +
                                        data.Param["choscode"].ToString() + "','" + data.Param["code"].ToString() + "')") < 0)
                    throw new Exception("补打发票时票据使用表写入错误！");

                if (dao.ExecuteNonQuery("update 票据使用表 set 票据状态=1 where 领用序号=" +
                                        data.Param["sno"].ToString() + " and choscode='" + data.Param["choscode"].ToString() +
                                        "' and 票号='" + data.Param["oldfp"].ToString() + "'") < 0)
                    throw new Exception("补打发票时更改票据使用表状态错误！");

                if (dao.ExecuteNonQuery("insert into 作废票据表(票号,票据类型,更换新票号,操作员,choscode) values('" +
                                        data.Param["oldfp"].ToString() + "'," + data.Param["kind"].ToString() + ",'" +
                                        data.Param["newfp"].ToString() + "'," + data.Param["userid"].ToString() + ",'" +
                                        data.Param["choscode"].ToString() + "')") < 0)
                    throw new Exception("作废票据写入错误！");
            }
 
            msg = "ok";
            return "ok";
        }

        #endregion
    }
}
