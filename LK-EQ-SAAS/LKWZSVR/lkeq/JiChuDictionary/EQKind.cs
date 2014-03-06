using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using System.Data;
using YtService.config;
using YiTian.db;

namespace LKWZSVR.lkeq.JiChuDictionary
{
    class EQKind:IEx
    {



        #region IEx 成员

        public object Run(Dao dao, YtService.data.OptData data, out string msg)
        {

            msg = "设备信息";
            Dictionary<string, object> pa = new Dictionary<string, object>();
            string ac = data.Sql;
            ObjItem obj;
            object OBJ;
            if ("SaveEQKindInfo".Equals(ac))
            {


                pa["KINDNAME"] = data.Param["KINDNAME"].ToString();
                pa["USERNAME"] = data.Param["USERNAME"].ToString();
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["IFEND"] = Convert.ToDecimal(data.Param["IFEND"]);

                if (data.Param["PREFIX"] != null)
                {
                    pa["PREFIX"] = data.Param["PREFIX"].ToString();
                }
                else
                {
                    pa["PREFIX"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["PYCODE"] != null)
                {
                    pa["PYCODE"] = data.Param["PYCODE"].ToString();
                }
                else
                {
                    pa["PYCODE"] = null;
                }
                if (data.Param["WBCODE"] != null)
                {
                    pa["WBCODE"] = data.Param["WBCODE"].ToString();
                }
                else
                {
                    pa["WBCODE"] = null;
                }
                pa["SUPERCODE"] = data.Param["SUPERCODE"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();

                Opt cfmx = OptContent.get("EQKindSave");

                Opt cop_Name = OptContent.get("CompareEQKindName");
                Opt cop_Code = OptContent.get("CompareEQKindCode");

                Opt changeUpCodesIfEnd = OptContent.get("changeUpCodesIfEnd");

                obj = DaoTool.ExecuteScalar(dao, cop_Name, pa);
                if (!obj.IsNull)
                {
                    msg = "名称重复！";
                    return "ok";
                }

                if (DaoTool.Save(dao, cfmx, pa) < 0)
                    throw new Exception("添加设备信息失败！");
                if (DaoTool.ExecuteNonQuery(dao, changeUpCodesIfEnd, pa) < 0)
                    throw new Exception("修改上级是否末阶段失败！");
                msg = "添加成功！";
                return "ok";
            }
            if ("UpdataEQKindInfo".Equals(ac))
            {

                pa["KINDNAME"] = data.Param["KINDNAME"].ToString();
                pa["USERNAME"] = data.Param["USERNAME"].ToString();
                pa["IFUSE"] = Convert.ToDecimal(data.Param["IFUSE"]);
                pa["RECDATE"] = DateTime.Now;
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["USERID"] = Convert.ToDecimal(data.Param["USERID"]);
                pa["IFEND"] = Convert.ToDecimal(data.Param["IFEND"]);

                if (data.Param["PREFIX"] != null)
                {
                    pa["PREFIX"] = data.Param["PREFIX"].ToString();
                }
                else
                {
                    pa["PREFIX"] = null;
                }
                if (data.Param["MEMO"] != null)
                {
                    pa["MEMO"] = data.Param["MEMO"].ToString();
                }
                else
                {
                    pa["MEMO"] = null;
                }
                if (data.Param["PYCODE"] != null)
                {
                    pa["PYCODE"] = data.Param["PYCODE"].ToString();
                }
                else
                {
                    pa["PYCODE"] = null;
                }
                if (data.Param["WBCODE"] != null)
                {
                    pa["WBCODE"] = data.Param["WBCODE"].ToString();
                }
                else
                {
                    pa["WBCODE"] = null;
                }
                pa["SUPERCODE"] = data.Param["SUPERCODE"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                pa["SELECTCODE"] = data.Param["SELECTCODE"].ToString();
                Opt Updatacop_Name = OptContent.get("UpdataCompareEQKindName");
                Opt Updatacop_Code = OptContent.get("CompareEQKindCode");
                Opt Updatacop_UpCode = OptContent.get("CompareEQKindUpCode");
                Opt WZKindUpdata = OptContent.get("EQKindUpdata");
                Opt changeUpCodesIfEnd = OptContent.get("changeUpCodesIfEnd");

                obj = DaoTool.ExecuteScalar(dao, Updatacop_Name, pa);
                if (!obj.IsNull)
                {
                    msg = "名称重复！";
                    return "ok";
                }
                if (DaoTool.ExecuteNonQuery(dao, WZKindUpdata, pa) < 0)
                    throw new Exception("修改设备信息失败！");
                if (DaoTool.ExecuteNonQuery(dao, changeUpCodesIfEnd, pa) < 0)
                    throw new Exception("修改上级末节点失败！");
                msg = "修改成功！";
                return "ok";

            }

            if ("DelEQKindInfo".Equals(ac))
            {
                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                pa["SUPERCODE"] = data.Param["SUPERCODE"].ToString();

                Opt del = OptContent.get("EQKinddel");
                Opt del_Find = OptContent.get("EQKinddel_Find");

                Opt ifhavechildren = OptContent.get("ifhavechildren");
                Opt changeUpCodesIsEnd = OptContent.get("changeUpCodesIsEnd");
                //if (DaoTool.ExecuteNonQuery(dao, del_Find, pa) < 0)//是否使用过
                //   throw new Exception("使用过只能停用不能删除！");
                OBJ = DaoTool.ExecuteScalar(dao, del_Find, pa).Get();
                //if (Convert.ToInt32(OBJ) == 1)
                //    throw new Exception("使用过只能停用不能删除！");

                if (DaoTool.ExecuteNonQuery(dao, del, pa) < 0)
                    throw new Exception("删除设备成功！");

                if (DaoTool.ExecuteScalar(dao, ifhavechildren, pa).IsNull)
                {
                    if (DaoTool.ExecuteNonQuery(dao, changeUpCodesIsEnd, pa) < 0)
                        throw new Exception("修改上级末节点失败！");
                }

                msg = "删除成功！";
                return "ok";
            }
            if ("StartEQKindInfo".Equals(ac))
            {

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                Opt stop = OptContent.get("StartorStopEQKindInfo");
                pa["IFUSE"] = 1;
                if (DaoTool.ExecuteNonQuery(dao, stop, pa) < 0)
                    throw new Exception("启用设备失败！");
                msg = "启用设备成功！";
                return "ok";
            }
            if ("StopEQKindInfo".Equals(ac))
            {

                pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                pa["KINDCODE"] = data.Param["KINDCODE"].ToString();
                Opt start = OptContent.get("StartorStopEQKindInfo");
                Opt canstop = OptContent.get("CanStopEQKindInfo");
                pa["IFUSE"] = 0;
                //if (DaoTool.ExecuteNonQuery(dao, del_Find, pa) < 0)//是否使用过
                //    throw new Exception("使用过只能停用不能删除！");
                OBJ = DaoTool.ExecuteScalar(dao, canstop, pa).Get();
                if (Convert.ToInt32(OBJ) == 0)
                    throw new Exception("不是末节点不能停用！");
                if (DaoTool.ExecuteNonQuery(dao, start, pa) < 0)
                    throw new Exception("启用设备失败！");
                msg = "停用设备成功！";
                return "ok";
            }
            if ("CopyEQKindInfo".Equals(ac))
            {
                Opt Copy = OptContent.get("CopyEQKindInfo");
                Opt cfmx = OptContent.get("EQKindSave");
                DataTable dt = DaoTool.FindDT(dao, Copy, data);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["KINDNAME"] != null)
                    {
                        pa["KINDNAME"] = dr["KINDNAME"].ToString();
                    }
                    else
                    {
                        pa["KINDNAME"] = null;
                    }


                    if (dr["PYCODE"] != null)
                    {
                        pa["PYCODE"] = dr["PYCODE"].ToString();
                    }
                    else
                    {
                        pa["PYCODE"] = null;
                    }


                    if (dr["USERNAME"] != null)
                    {
                        pa["USERNAME"] = dr["USERNAME"].ToString();
                    }
                    else
                    {
                        pa["USERNAME"] = null;
                    }

                    if (dr["MEMO"] != null)
                    {
                        pa["MEMO"] = dr["MEMO"].ToString();
                    }
                    else
                    {
                        pa["MEMO"] = null;
                    }
                    pa["IFUSE"] = Convert.ToDecimal(dr["IFUSE"]);
                    pa["RECDATE"] = Convert.ToDateTime(dr["RECDATE"]);
                    pa["CHOSCODE"] = data.Param["CHOSCODE"].ToString();
                    pa["USERNAME"] = dr["USERNAME"].ToString();
                    pa["USERID"] = dr["USERID"].ToString();
                    pa["IFEND"] = Convert.ToDecimal(dr["IFEND"]);

                    pa["SUPERCODE"] = dr["SUPERCODE"].ToString();
                    pa["KINDCODE"] = dr["KINDCODE"].ToString();
                    DaoTool.Save(dao, cfmx, pa);
                }
                msg = "复制成功！";
                return "ok";
            }
            return "ok";
        }

        #endregion
    }
}
