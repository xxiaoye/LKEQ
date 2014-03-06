using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nhservice.orm
{
    public class NHPeoInfo
    {
        /// <summary>
        /// 个人编码
        /// </summary>
        public string Grbm
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Xb
        {
            get;
            set;
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Csrq
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string Ide
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
        /// 是否慢病 0 是 1 否
        /// </summary>
        public string Sfmb
        {
            get;
            set;
        }

        /// <summary>
        /// 年度累计门诊补偿
        /// </summary>
        public string mztotalbc
        {
            get;
            set;
         }

        /// <summary>
        /// 年度累计住院补偿
        /// </summary>
        public string zytotalbc
        {
            get;
            set;
        }

        /// <summary>
        /// 是否封顶（1已封顶，0未封顶）
        /// </summary>
        public string topflag
        {
            get;
            set;
        }
    }
}
