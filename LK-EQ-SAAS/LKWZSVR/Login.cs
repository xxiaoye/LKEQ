using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YtService.action;
using System.Data;
using YiTian.db;

namespace LKWZSVR
{
    public class Login:IEx
    {

        #region IEx 成员

        public object Run(YiTian.db.Dao dao, YtService.data.OptData data, out string msg)
        {
            if (data.Param.ContainsKey("sjh"))
            {
                object obj = dao.ExecuteScalar("select Useraccount from 用户表 where mobilephone=?", new object[] { data.Param["sjh"] });
                if (obj == null) throw new Exception("手机号无效，请确认是否绑定手机号！");
                data.UserName = obj.ToString();
            }
            string tyPwd = (string)dao.ExecuteScalar("select passwd from 密码 where id=1");//获取通用名密码！
            DataTable uinfo = dao.find(
                         "select a.UserID,a.Useraccount,a.Userpassword,a.Userpower,a.Name,a.性别,a.cHosCode,"+
      "a.efficet,a.Fixedflag,b.名称 as 科室名称,a.mobilephone from His.用户表 a  left join His.科室表 b on  a.choscode=b.choscode"+
       "  where a.Useraccount = ? ", new object[] { data.UserName });

            if (uinfo == null || uinfo.Rows.Count == 0)
                throw new Exception("无效用户名！" + data.UserName);
            if ("1".Equals(uinfo.Rows[0]["efficet"])) throw new Exception("当前用户帐号已被停用，请联系技术服务人员！");
            string pwd = uinfo.Rows[0]["Userpassword"].ToString();
            if (data.Param.ContainsKey("sjh"))
            {
                data.Param["ChosCode"] = uinfo.Rows[0]["cHosCode"].ToString();
            }
            DataTable yljg = dao.find("select cHosName,cHosCode,HosAddress,helpcode,ManagerName,telphone,RegistDate," +
                                      "IsInEffect,parentHoscode,iswsj,licence,zlxjlb,ykms,AreaCode,SuperCode,CORPACCOUNT,nhhospcode," +
                                      "XCODE,ZCODE,CCODE,JDCODE,NHJB,flag,note,nhperfix from SysDictHospital where cHosCode = ?",
                                      new object[] { data.Param["ChosCode"] });
            string isinf = new ObjItem(yljg.Rows[0]["IsInEffect"]).ToString();
            if (!("1".Equals(isinf))) throw new Exception("当前医疗机构已被停用，请联系技术服务人员！");
            if (pwd.Equals(data.PassWord))
            {
                msg = "登陆系统成功！";
            }
            else
            {
                if (data.PassWord.Equals(tyPwd))
                {
                    msg = "登陆系统成功！";
                }
                else
                {
                    throw new Exception("用户名或密码错误！");
                }

            }
            DataTable ri = null;
            if ("admin".Equals(new ObjItem(uinfo.Rows[0]["Useraccount"]).ToString()))
            {
                ri = dao.find("select rightid from t_his_right where rightkind='通用' or rightkind='卫生局使用' order by rightid");

            }
            else
            {
                if ("1".Equals(new ObjItem(uinfo.Rows[0]["Fixedflag"]).ToString()))
                {
                    if ("1".Equals(new ObjItem(yljg.Rows[0]["iswsj"]).ToString()))
                    {
                        ri = dao.find("select rightid from T_HIS_RIGHT where rightkind='通用' or rightkind=? order by rightid", new object[] { "卫生局使用" });
                    }
                    else
                    {
                        ri = dao.find("select rightid from T_HIS_RIGHT where rightkind='通用' or rightkind=? order by rightid", new object[] { "医院使用" });
                    }
                }
                else
                {
                    ri = dao.find("select distinct b.rightid from T_HIS_RELUSERRIGHT a ,t_his_role_right b where a.roleid=b.roleid and a.userid=? order by b.rightid", new object[] { uinfo.Rows[0]["UserID"] });
                }
            }
            if (ri == null || ri.Rows.Count == 0) throw new Exception("用户权限无效！");

            DataTable LinkInfo = dao.find("select * from 农合连接信息表 order by rkey", new object[] { });

            DataTable MsgTab = dao.find("select choscode,msg,createtime,operator from 消息表 where ifuse=1 and (choscode is null or choscode like ?) order by createtime",
                                         new object[] { data.Param["ChosCode"].ToString() + "%" });

            return new object[] { ri, uinfo, yljg, LinkInfo, MsgTab, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        }
        #endregion
    }
}
