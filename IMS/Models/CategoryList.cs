using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class CategoryList
    {
        
        [Key]
        [MaxLength(10, ErrorMessage = "長度不得超過10")]
        public string CategoryID { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }

        public ICollection<CategorySub> CategorySubs { get; set; }
    }
}