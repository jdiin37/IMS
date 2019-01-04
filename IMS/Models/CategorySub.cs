using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class CategorySub
    {
        [Key]
        [DisplayName("流水號")]
        public int ID { get; set; }

        [DisplayName("項目編號")]
        public string SubValue { get; set; }

        [DisplayName("項目名稱")]
        public string SubName { get; set; }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        public string CreUser { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModDate { get; set; }

        [DisplayName("修改人員")]
        public string ModUser { get; set; }

        [ForeignKey("CategoryList")]
        public string CategoryID { get; set; }

        public Category CategoryList { get; set; }
    }
}