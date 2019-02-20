using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class TraceDetail
    {
        [Key]
        public int SeqNo { get; set; }

        [Required]
        [Display(Name = "TraceNo", ResourceType = typeof(Resources.Resource))]
        public string TraceNo { get; set; }
        
        [Display(Name = "WorkCode", ResourceType = typeof(Resources.Resource))]
        public string WorkCode { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

        [DisplayName("新增人員")]
        public string WorkUser { get; set; }



        [StringLength(2)]
        public string Status { get; set; }

        [Required]
        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        public string CreUser { get; set; }

        [DisplayName("修改日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ModDate { get; set; }

        [DisplayName("修改人員")]
        public string ModUser { get; set; }

        
        
    }
}