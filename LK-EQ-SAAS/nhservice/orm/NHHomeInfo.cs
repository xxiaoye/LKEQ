using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nhservice.orm
{
    public class NHHomeInfo
    {
        /// <summary>
        /// 家庭编码
        /// </summary>
        public string Jtbh
        {
            get;
            set;
        }
        /// <summary>
        /// 医疗证号
        /// </summary>
        public string Ylzh
        {
            get;
            set;
        }
        /// <summary>
        /// 户主姓名
        /// </summary>
        public string Hzxm
        {
            get;
            set;
        }
        /// <summary>
        /// 住址
        /// </summary>
        public string Zz
        {
            get;
            set;
        }
        /// <summary>
        /// 账户余额
        /// </summary>
        public string Zhye
        {
            get;
            set;
        }
        /// <summary>
        /// 户属性
        /// </summary>
        public string Hsx
        {
            get;
            set;
        }
        /// <summary>
        /// 参合状态
        /// </summary>
        public string Chzt
        {
            get;
            set;
        }
        /// <summary>
        /// 账户状态
        /// </summary>
        public string Zhzt
        {
            get;
            set;
        }
    }
     


    public class RegCardInfo  //就诊卡信息
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardcode
        {
            get;
            set;
        }
        /// <summary>
        /// 医疗证号
        /// </summary>
        public string Ylzh
        {
            get;
            set;
        }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string grbm
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string sickname
        {
            get;
            set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex
        {
            get;
            set;
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday
        {
            get;
            set;
        }

         /// <summary>
        /// 身份证号
        /// </summary>
        public string idcard
        {
            get;
            set;
        }   

        /// <summary>
        /// 住址
        /// </summary>
        public string Zz
        {
            get;
            set;
        }
        
    }
}
