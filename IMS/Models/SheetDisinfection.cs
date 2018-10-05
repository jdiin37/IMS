using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class SheetDisinfection
    {
        [Key]
        [DisplayName("流水號")]
        public int ID { get; set; }

        [DisplayName("日期")]
        public DateTime ExcuteDate { get; set; }

        [DisplayName("棟(欄)別")]
        public string Building { get; set; }

        [DisplayName("消毒劑種類 ")]
        public string UseType { get; set; }

        [DisplayName("用法(用量)")]
        public string UseDose { get; set; }

        [DisplayName("備註")]
        public string Note { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime ModDate { get; set; }

        public string ModUser { get; set; }
    }
}