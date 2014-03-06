using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nhservice.orm
{
    
    public class ZyRegisterInfo
    {
        /// <summary>
        /// 地区编号
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 个人代码
        /// </summary>
        public string Grbm { get; set; }
        /// <summary>
        /// 医院编号
        /// </summary>
        public string Hoscode { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string Zyh { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public string Rytime { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public string Cytime { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string Ryks { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string Cyks { get; set; }
        /// <summary>
        /// 医
        /// </summary>
        public string Doctor { get; set; }
        /// <summary>
        /// 入院状态
        /// </summary>
        public string Ryzt { get; set; }
        /// <summary>
        /// 出院状态
        /// </summary>
        public string Cyzt { get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string Jbbm { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string Bq { get; set; }
        /// <summary>
        /// 病房号
        /// </summary>
        public string Bfh { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        public string Cwh { get; set; }
        /// <summary>
        /// 是否证件齐全
        /// </summary>
        public string Sfzjqt { get; set; }
        /// <summary>
        /// 手术名称代码
        /// </summary>
        public string Sscode { get; set; }
        /// <summary>
        /// 住院类型
        /// </summary>
        public string ZyLx{ get; set; }
    }
}
