using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class Category
    {
        
        [Key]
        [MaxLength(10, ErrorMessage = "長度不得超過10")]
        [DisplayName("類別編號")]
        public string CategoryID { get; set; }


        [DisplayName("類別名稱")]
        public string CategoryName { get; set; }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        public string CreUser { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModDate { get; set; }

        [DisplayName("修改人員")]
        public string ModUser { get; set; }

        public ICollection<CategorySub> CategorySubs { get; set; }
    }
}