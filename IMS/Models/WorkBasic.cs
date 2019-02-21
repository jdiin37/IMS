using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class WorkBasic
    {
        [Key]
        public int SeqNo { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(20, ErrorMessage = "長度不得超過12"),MinLength(4, ErrorMessage = "長度不得小於4")]
        [Display(Name = "WorkCode", ResourceType = typeof(Resources.Resource))]
        public string WorkCode { get; set; }

        [Display(Name = "WorkClass", ResourceType = typeof(Resources.Resource))]
        [StringLength(20)]
        public string WorkClass { get; set; }


        [Display(Name = "WorkType", ResourceType = typeof(Resources.Resource))]
        [StringLength(20)]
        public string WorkType { get; set; }

        [Display(Name = "WorkContent", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.MultilineText), StringLength(400)]
        [Required]
        public string WorkContent { get; set; }

        [Display(Name = "Memo", ResourceType = typeof(Resources.Resource))]       
        public string Memo { get; set; }


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