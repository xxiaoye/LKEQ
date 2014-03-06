using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using YtService.util;
using YtService.config;
using System.Windows.Forms;

namespace LKWZSVR.lkeq.WareManag
{
    public class EQBuildCardSvr : IEx
    {
        #region IEx 成员
        int CardId = 0;

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Sql == null)
            {
                throw new Exception("SQL参数为空！");
            }


            if (data.Sql.Equals("ModifyZhuInfo"))
            {
                if (DaoTool.Update(dao, OptContent.get("SaveEQCardRecInfo"), data) < 0)
                {
                    msg = "执行失败！";
                    throw new Exception("设备卡片记录表更新失败！" + dao.ErrMsg);
                }
                //更新对应的设备数目
                DaoTool.ExecuteNonQuery(dao, OptContent.get("UpdateCardNumInCardInfo"), data);
                msg = "执行成功！";
                return "ok";
            }
            if (data.Sql.Equals("ModifyCCInfo"))
            {
                if (data.Param["IsUpdateCC"].ToString() == "1")
                {
                    if (DaoTool.Update(dao, OptContent.get("SaveEQCardCCInfo"), data) < 0)
                    {
                        throw new Exception("设备财产记录表更新失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    if (DaoTool.Save(dao, OptContent.get("SaveEQCardCCInfo"), data) < 0)
                    {
                        throw new Exception("设备财产记录表新增失败！" + dao.ErrMsg);
                    }
                }
                msg = "执行成功！";
                return "ok";
            }
            if (data.Sql.Equals("ModifySMInfo"))
            {
                if (data.Param["IsUpdateSM"].ToString() == "1")
                {
                    if (DaoTool.Update(dao, OptContent.get("SaveEQCardSMInfo"), data) < 0)
                    {
                        throw new Exception("设备说明记录表更新失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    if (DaoTool.Save(dao, OptContent.get("SaveEQCardSMInfo"), data) < 0)
                    {
                        throw new Exception("设备说明记录表新增失败！" + dao.ErrMsg);
                    }
                }
                msg = "执行成功！";
                return "ok";
            }

            if (data.Sql.Equals("ModifyFJInfo"))
            {
                if (data.Param["IsUpdateFJ"].ToString() == "1")
                {
                    if (DaoTool.Update(dao, OptContent.get("SaveEQCardFJInfo"), data) < 0)
                    {
                        throw new Exception("设备附件记录表更新失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    //若为新增，则 行号需要改变 
                    // data.Param.Remove("IsUpdateFJ");
                    data.Param["ROWNO"] = DaoTool.ExecuteScalar(dao, OptContent.get("FindRowNoInFJ"), data).ToInt() + 1;
                    if (DaoTool.Save(dao, OptContent.get("SaveEQCardFJInfo"), data) < 0)
                    {
                        throw new Exception("设备附件记录表新增失败！" + dao.ErrMsg);
                    }
                }
                msg = "执行成功！";
                return "ok";
            }


            if (data.Sql.Equals("ModifyJLInfo"))
            {
                if (data.Param["IsUpdateJL"].ToString() == "1")
                {
                    if (DaoTool.Update(dao, OptContent.get("SaveEQCardJLInfo"), data) < 0)
                    {
                        throw new Exception("设备计量记录表更新失败！" + dao.ErrMsg);
                    }
                }
                else
                {
                    data.Param["ROWNO"] = DaoTool.ExecuteScalar(dao, OptContent.get("FindRowNoInFJ"), data).ToInt() + 1;
                    if (DaoTool.Save(dao, OptContent.get("SaveEQCardJLInfo"), data) < 0)
                    {
                        throw new Exception("设备计量记录表新增失败！" + dao.ErrMsg);
                    }
                }
                msg = "执行成功！";
                return "ok";
            }

            //前面全部为修改部分  下面开始处理新增部分




            if (data.Sql.Equals("AddZhuInfo"))
            {
                // if(data.Param["IsFlag"]!=null||data.Param["IsFlag"].ToString()="")
                data.Param["CARDID"] = DaoTool.Seq(dao, "LKEQ.SEQEQCardUse");
                CardId = Convert.ToInt32(data.Param["CARDID"]);

                string Prefix = dao.Es("SELECT PREFIX FROM LKEQ.DICTEQKIND WHERE CHOSCODE=" + data.Param["CHOSCODE"].ToString() + "  AND KINDCODE=(SELECT KINDCODE FROM LKEQ.DICTEQ WHERE EQID=" + data.Param["EQID"].ToString() + " AND CHOSCODE=" + data.Param["CHOSCODE"].ToString() + ")").ToString().ToUpper();
                int LengthG = Convert.ToInt32(dao.Es("SELECT DISTINCT SYSVALUE FROM HIS.系统参数 WHERE ID=2206"));//长度

                //可以直接这样搜索是前缀一样，比较的是后面的数字

                object MaxFirst = dao.Es("SELECT MAX(CARDCODE) FROM LKEQ.EQCARDREC WHERE EQID=" + data.Param["EQID"].ToString() + " AND CHOSCODE=" + data.Param["CHOSCODE"].ToString() + " AND CARDCODE LIKE " + "'%" + Prefix + "%'");
                string Max;
                if (MaxFirst == null)
                {
                     Max = "1";
                }
                else
                {
                     Max = MaxFirst.ToString();
                    if (Max.Contains(Prefix))
                    {
                        Max = ((Convert.ToInt64(Max.Substring(Max.LastIndexOf(Prefix) + Prefix.Length))) + 1).ToString();
                    }
                    else
                    {
                        Max = "1";
                    }
                }
                data.Param["CARDCODE"] = Prefix + 0.ToString("D" + (LengthG - Max.Length)) + Max;
                if (DaoTool.Save(dao, OptContent.get("SaveEQCardRecInfo"), data) < 0)
                {
                    throw new Exception("设备卡片主表新增失败！" + dao.ErrMsg);
                }
                //同样的更新数目到库存流水表
                DaoTool.ExecuteNonQuery(dao, OptContent.get("UpdateCardNumInCardInfo"), data);
                msg = "执行成功！";
                return "ok";
                //

            }
            if (data.Sql.Equals("AddJLInfo"))
            {
                data.Param["CARDID"] = CardId;
                data.Param["ROWNO"] = DaoTool.ExecuteScalar(dao, OptContent.get("FindRowNoInFJ"), data).ToInt() + 1;
                if (DaoTool.Save(dao, OptContent.get("SaveEQCardJLInfo"), data) < 0)
                {
                    throw new Exception("设备计量信息新增失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";

            }
            if (data.Sql.Equals("AddCCInfo"))
            {
                data.Param["CARDID"] = CardId;
                if (DaoTool.Save(dao, OptContent.get("SaveEQCardCCInfo"), data) < 0)
                {
                    throw new Exception("设备财产信息新增失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";

            }
            if (data.Sql.Equals("AddFJInfo"))
            {
                data.Param["CARDID"] = CardId;
                data.Param["ROWNO"] = DaoTool.ExecuteScalar(dao, OptContent.get("FindRowNoInFJ"), data).ToInt() + 1;
                if (DaoTool.Save(dao, OptContent.get("SaveEQCardFJInfo"), data) < 0)
                {
                    throw new Exception("设备附件信息新增失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";

            }
            if (data.Sql.Equals("AddSMInfo"))
            {
                data.Param["CARDID"] = CardId;
                if (DaoTool.Save(dao, OptContent.get("SaveEQCardSMInfo"), data) < 0)
                {
                    throw new Exception("设备说明信息新增失败！" + dao.ErrMsg);
                }
                msg = "执行成功！";
                return "ok";
            }


            else
            {
                throw new Exception("系统出错，请与管理员联系！");

            }



        }

        #endregion
    }
}
