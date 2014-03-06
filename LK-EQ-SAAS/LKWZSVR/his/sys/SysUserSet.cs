using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using System.Data;
using YtService.config;
using YtService.util;

namespace LKWZSVR.his.sys
{
    public  class SysUserSet:IEx
    {
        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            string ac = data.Sql;
            msg = "";
            if ("EditPwd".Equals(ac))
            {
                DataTable dt  = dao.find("select a.userpassword from 用户表 a where   a.userid=? and a.choscode=? "
                     , new object[] {  data.Param["UserId"], data.Param["cHosCode"] }
                    );
                if (dt == null || dt.Rows.Count == 0)
                    throw new Exception("用户无效！");
                string pd = "";
                if (dt.Rows[0][0] != null)
                    pd = dt.Rows[0][0].ToString();

                if (!data.Param["oldPwd"].ToString().Equals(pd))
                    throw new Exception("原密码错误！");
                if (data.Param.ContainsKey("sjh"))
                {
                    object obj1 = dao.ExecuteScalar("select count(*) from 用户表 a where a.mobilephone=?"
                        , new object[] { data.Param["sjh"] }
                       );
                    if (int.Parse(obj1.ToString())>0) throw new Exception("您输入的手机号已被其他用户绑定，请重新输入！");
                    int i = dao.ExecuteNonQuery("update  用户表 set mobilephone=? where  userid=? and choscode=?"
                        , new object[] { data.Param["sjh"], data.Param["UserId"], data.Param["cHosCode"] }
                        );
                    if (i < 0)
                        throw new Exception("帮定手机号失败！");
                }
                int b = dao.ExecuteNonQuery("update  用户表 set userpassword=? where  userid=? and choscode=?"
                        , new object[] { data.Param["Pwd"], data.Param["UserId"], data.Param["cHosCode"] }
                        );
                if (b < 0)
                    throw new Exception("修改用户密码失败！");
                msg = "修改密码成功！";
            }
            else if ("RegCardSet".Equals(ac))
            {
                if (!data.Param.ContainsKey("密码"))
                    throw new Exception("输入的密码无效请重新输入！");
                string pwd = data.Param["密码"].ToString();
                object p = dao.ExecuteScalar("select passwd from 密码 where id=4");
                if (p != null)
                {
                    if (!p.ToString().Equals(pwd))
                    {
                        throw new Exception("输入的密码无效请重新输入！");
                    }
                }
                else
                {
                    throw new Exception("输入的密码无效请重新输入！");
                }

                DataTable dt = dao.find("select 1 from 就诊卡范围表 where (开始号 between ? and ? or 结束号 between ? and ?) and length(开始号)=?"
                          , new object[] { data.Param["开始号"], data.Param["结束号"], data.Param["开始号"], data.Param["结束号"], data.Param["长度"] }
                    );
                if (dt != null && dt.Rows.Count > 0)
                    throw new Exception("输入的就诊卡号已被领用过，请重新录入号码");

                Opt op = OptContent.get("sys_SaveRegCard");
                if (DaoTool.Save(dao, op, data) > -1)
                {
                    msg = "保存就诊卡数据成功！";
                }
                else
                {
                    throw new Exception("就诊卡领用失败");
                }
            }
            else if ("SaveOpLog".Equals(ac))
            {
                if (dao.ExecuteNonQuery("insert into 特殊操作日志(choscode,chosname,操作功能,操作员,操作机器,辅助说明) values('" +
                                       data.Param["choscode"].ToString() + "','" + data.Param["chosname"].ToString() + "'," +
                                       data.Param["funID"].ToString() + ",'" + data.Param["username"].ToString() + "','" +
                                       data.Param["hostinfo"].ToString() + "','" + data.Param["说明"].ToString() + "')") < 0)
                    throw new Exception("保存操作日志错误." + dao.ErrMsg);
                else
                    msg = "ok";
            }

            
            return "ok";
        }

        #endregion
    }
}
