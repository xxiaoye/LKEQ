using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using YiTian.util;

namespace LKWZSVR.his.sys
{
    public class yljgWin : IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string A = data.Sql;
            msg = "";
            if ("Del".Equals(A)) {
               object p = dao.ExecuteScalar("select count(*) num from 用户表 where fixedflag=0 and cHosCode = '" + data.Param["choscode"] + "'");
               if (p!= null && int.Parse(p.ToString())>0)
                   throw new Exception("当前医疗机构已被使用，不能删除!");

               object p2 = dao.ExecuteScalar("select count(*) num from sysdicthospital where parenthoscode = '" + data.Param["choscode"] + "'");
               if (p2 != null && int.Parse(p2.ToString()) > 0)
                   throw new Exception("当前医疗机构还有子机构，不能删除!");

               if (dao.ExecuteNonQuery("DELETE FROM sysdicthospital WHERE cHosCode='" + data.Param["choscode"] + "'") < 0) 
                   throw new Exception("删除医疗机构失败！");

               //msg = "删除成功完成！";
            }
            else if ("SaveNHFare".Equals(A))  //保存农合诊疗及药品字典
            {
                List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(StringZip.Decompress(data.Param["nhdatamx"].ToString()));
                Opt NhZLData = OptContent.get("nh_FareZLDict");
                Opt NhYPData = OptContent.get("nh_FareYPDict");
                if (mxli == null || mxli.Count == 0)
                {
                    throw new Exception("无农合费用字典明细！");
                }

                string vchoscode = data.Param["cHosCode"].ToString() ;
                int rank = int.Parse(data.Param["rank"].ToString()) ;

                if (dao.ExecuteNonQuery("DELETE FROM 农合药品表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合药品数据出错！");
                if (dao.ExecuteNonQuery("DELETE FROM 农合项目表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合项目数据出错！");

                foreach (Dictionary<string, object> pa in mxli)
                {
                    pa["rank"] = rank;
                    pa["cHosCode"] = vchoscode;
                    if (pa["农合类别"].ToString().Equals("02") || pa["农合类别"].ToString().Equals("09"))
                    {
                        if (DaoTool.Save(dao, NhYPData, pa) < 0)
                            throw new Exception("保存农合费用字典药品明细失败！");
                    }
                    else 
                    {
                        if (pa["农合类别"].ToString().Equals("05") || pa["农合类别"].ToString().Equals("10"))
                        {
                            if (DaoTool.Save(dao, NhYPData, pa) < 0)
                                throw new Exception("保存农合费用字典药品明细失败！");
                        }

                        if (DaoTool.Save(dao, NhZLData, pa) < 0)
                            throw new Exception("保存农合费用字典诊疗明细失败！");
                    }
                    
                }
            }
            else if ("SaveICD10".Equals(A))  //保存农合疾病字典
            {
                List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(StringZip.Decompress(data.Param["icd10"].ToString()));
                Opt NhicdData = OptContent.get("nh_saveICDDict");
                if (mxli == null || mxli.Count == 0)
                {
                    throw new Exception("无农合疾病字典明细！");
                }

                dao.ExecuteNonQuery("truncate table 农合疾病表");  //永远返回-1

                foreach (Dictionary<string, object> pa in mxli)
                {
                    if (DaoTool.Save(dao, NhicdData, pa) < 0)
                        throw new Exception("保存农合疾病字典失败！");
                }
                dao.ExecuteNonQuery("update 农合疾病表 t set t.orderid=(select ordid from jb where 编码=t.农合编码) where exists(select * from jb where 编码=t.农合编码)");
            }

            else if ("SaveSS".Equals(A))  //保存农合手术字典
            {
                List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(StringZip.Decompress(data.Param["SS"].ToString()));
                Opt NhicdData = OptContent.get("nh_saveSSDict");
                if (mxli == null || mxli.Count == 0)
                {
                    throw new Exception("无农合手术字典明细！");
                }
                dao.ExecuteNonQuery("truncate table 农合手术表");

                foreach (Dictionary<string, object> pa in mxli)
                {
                    if (DaoTool.Save(dao, NhicdData, pa) < 0)
                        throw new Exception("保存农合手术字典失败！");
                }
                
            }

            else if ("CheckNHFare".Equals(A))  //保存并进行核对农合费用项目
            {
                List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(StringZip.Decompress(data.Param["nhdatamx"].ToString()));
                Opt NhZLData = OptContent.get("nh_FareZLDict");
                Opt NhYPData = OptContent.get("nh_FareYPDict");
                if (mxli == null || mxli.Count == 0)
                {
                    throw new Exception("无农合费用字典明细！");
                }

                string vchoscode = data.Param["cHosCode"].ToString();
                int rank = int.Parse(data.Param["rank"].ToString());

                if (dao.ExecuteNonQuery("DELETE FROM 农合药品表BK WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合药品BK数据出错！");

                if (dao.ExecuteNonQuery("DELETE FROM 农合项目表BK WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合项目BK数据出错！");

                if (dao.ExecuteNonQuery("Insert into 农合药品表BK(农合编码,农合名称,农合类别,保内外,报补比例,单位,rank,updatetime,基药标志,cHosCode) "+
                    "Select 农合编码,农合名称,农合类别,保内外,报补比例,单位,rank,updatetime,基药标志,cHosCode from 农合药品表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("插入农合药品BK数据出错！");

                if (dao.ExecuteNonQuery("Insert into 农合项目表BK(农合编码,农合名称,农合类别,保内外,报补比例,单位,rank,updatetime,基药标志,cHosCode) " +
                    "Select 农合编码,农合名称,农合类别,保内外,报补比例,单位,rank,updatetime,基药标志,cHosCode from 农合项目表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("插入农合项目BK数据出错！");

                if (dao.ExecuteNonQuery("DELETE FROM 农合药品表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合药品数据出错！");
                if (dao.ExecuteNonQuery("DELETE FROM 农合项目表 WHERE cHosCode='" + vchoscode + "' and RANK=" + rank) < 0)
                    throw new Exception("删除农合项目数据出错！");

                foreach (Dictionary<string, object> pa in mxli)
                {
                    pa["rank"] = rank;
                    pa["cHosCode"] = vchoscode;
                    if (pa["农合类别"].ToString().Equals("02") || pa["农合类别"].ToString().Equals("09"))
                    {
                        if (DaoTool.Save(dao, NhYPData, pa) < 0)
                            throw new Exception("保存农合费用字典药品明细失败！");
                    }
                    else
                    {
                        if (pa["农合类别"].ToString().Equals("05") || pa["农合类别"].ToString().Equals("10"))
                        {
                            if (DaoTool.Save(dao, NhYPData, pa) < 0)
                                throw new Exception("保存农合费用字典药品明细失败！");
                        }

                        if (DaoTool.Save(dao, NhZLData, pa) < 0)
                            throw new Exception("保存农合费用字典诊疗明细失败！");
                    }

                }
            }

            else if ("getLog".Equals(A))  //生成日志数据
            {
                dao.ExecuteNonQuery(" begin PCK_HisCommUse.sp_SaveNHChangeFareLog('" + data.Param["cHosCode"].ToString() + "' ," + data.Param["rank"].ToString() + "); end;");

                dao.ExecuteNonQuery(" begin PCK_HisCommUse.sp_CheckNHFareDict('" + data.Param["cHosCode"].ToString() + "' ," + data.Param["rank"].ToString() + "); end;");
                //   throw new Exception("生成农合更新日志失败！");
            }

            else if ("SavejmFare".Equals(A))  //保存安顺地区城镇医保费用目录
            {
                List<Dictionary<string, object>> mxli = ObjConvert.GetParamsByStr(StringZip.Decompress(data.Param["datamx"].ToString()));
                Opt fareData = OptContent.get("jm_savefareDict");
                if (mxli == null || mxli.Count == 0)
                {
                    throw new Exception("无居保目录明细！");
                }
                if (dao.ExecuteNonQuery("delete 地区居保目录 where choscode='" + data.Param["cHosCode"].ToString() + "'") < 0)
                    throw new Exception("保存费用目录失败del！");

                string bm ;
                foreach (Dictionary<string, object> pa in mxli)
                {
                    bm = pa["大类编码"].ToString() ;
                    if (bm.Equals("81"))
                        pa["ypflag"] = 1;
                    else if (bm.Equals("11") || bm.Equals("12") || bm.Equals("13"))
                        pa["ypflag"] = 2;
                    else
                        pa["ypflag"] = 0;

                    pa["拼音码"] = pa["拼音码"].ToString().ToLower();
                    pa["choscode"] = data.Param["cHosCode"].ToString() ;
                    if (DaoTool.Save(dao, fareData, pa) < 0)
                        throw new Exception("保存居保目录明细失败！");
                }
            }
            else if ("dictcopy".Equals(A))  //机构字典相互复制
            {
                if (dao.ExecuteNonQuery(" begin PCK_HisCommUse.sp_jgDictCopy('" +
                    data.Param["fromHosCode"].ToString() + "' ,'" + data.Param["toHosCode"].ToString() + "' ,'" +
                    data.Param["ypflag"].ToString() + "' ,'" + data.Param["nhcode"].ToString() + "'); end;") < 0)
                 {
                    if (dao.ErrMsg!=null)
                      throw new Exception(dao.ErrMsg);
                 }
            }
            else if ("delyw".Equals(A))  //业务数据删除
            {
                if (dao.ExecuteNonQuery(" begin PCK_HisCommUse.sp_clearData('" + data.Param["choscode"].ToString() + "'," +
                                        data.Param["flag"].ToString() + "); end;") < 0)
                {
                    if (dao.ErrMsg != null)
                        throw new Exception(dao.ErrMsg);
                }
            }
            else if ("delypdict".Equals(A))  //药品字典删除
            {
                object p = dao.ExecuteScalar("select count(*) num from 库存药品 where cHosCode = " + data.Param["choscode"].ToString());
                if (p != null && int.Parse(p.ToString()) > 0)
                    throw new Exception("当前医疗还有库存药品业务数据，不能直接删除字典!");

                if (dao.ExecuteNonQuery("delete 药品字典表 where choscode='" + data.Param["choscode"].ToString() + "'") < 0)
                {
                    throw new Exception("药品字典删除失败！");
                }
            }
            else if ("setFlag".Equals(A))  //设置机构提示信息
            {
                if (dao.ExecuteNonQuery(" update sysdicthospital set flag=" + data.Param["flag"].ToString() + ",LICENCE='" +
                                        data.Param["info"].ToString() + "' where CHOSCODE='" + data.Param["cHosCode"].ToString() + "'") < 0)
                {
                    if (dao.ErrMsg != null)
                        throw new Exception(dao.ErrMsg);
                }
            }

            
            return "ok"; 
        }

        #endregion
    }
}
