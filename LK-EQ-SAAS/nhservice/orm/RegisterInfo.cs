using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nhservice.orm
{
    /// <summary>
    /// 门诊登记对象
    /// </summary>
    public class RegisterInfo
    {
        public string Areacode { get; set; } // 地区编号
        public string Hospcode { get; set; } // 医院编号
        public string Doctors { get; set; }  // 医生
        public string Grbm { get; set; }     // 个人代码
        public string Ryrq { get; set; }     // 就诊时间 格式是2008-02-06
        public string Jbbm { get; set; }     // 疾病代码
        public string Ksbm { get; set; }     // 入院科室编码
        public string Status { get; set; }   // 就诊状态
        public string Type { get; set; }    // 门诊类型
        public string Ylzh { get; set; }      //门诊号
        public bool Isjz { get; set; }
        public string Nhmzh { get; set; }
        public string Istg { get; set; }     //是否提高
        public string IsSS { get; set; }     //是否输液
        public string Isjtzh { get; set; }   //是否下家庭账户
    }

    public class RegisterMx {
        public string Area { get; set; }
        /// <summary>
        /// 明细主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 明细项目编
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 医院明细项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目类别
        /// </summary>
        public string Lb{ get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Gg { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string Jx { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Dw { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Dj { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Sl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Je { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime Sysj { get; set; }

    }

    public class JsXx
    {
        /// <summary>
        /// 是否结算成功
        /// </summary>
        public bool IsSuc { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Zhye { get; set; }
        /// <summary>
        /// 门诊总费用
        /// </summary>
        public decimal Mzzfy { get; set; }
        /// <summary>
        /// 保内费用
        /// </summary>
        public decimal Blfy { get; set; }
        /// <summary>
        /// 自费费用
        /// </summary>
        public decimal Zffy { get; set; }
        /// <summary>
        /// 家庭账户补助
        /// </summary>
        public decimal Zhbz { get; set; }
        /// <summary>
        /// 门诊统筹补助
        /// </summary>
        public decimal Tcbz { get; set; }
        /// <summary>
        /// 大额统筹补助
        /// </summary>
        public decimal Detcbz { get; set; }
        

    }
    public class RegJsInfo
    {
        public RegisterInfo Reg { get; set; }
        public JsXx Js { get; set; }

    }
}
