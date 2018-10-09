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

        public string SubValue { get; set; }

        public string SubName { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }

        [ForeignKey("CategoryList")]
        public string CategoryID { get; set; }

        public CategoryList CategoryList { get; set; }
    }
}