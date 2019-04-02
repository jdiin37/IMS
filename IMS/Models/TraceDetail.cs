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
    [StringLength(20)]
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



    [Display(Name = "WorkMemo", ResourceType = typeof(Resources.Resource))]
    [DataType(DataType.MultilineText), StringLength(400)]
    public string WorkMemo { get; set; }


    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    [Display(Name = "WorkDate", ResourceType = typeof(Resources.Resource))]
    public DateTime WorkDate { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    [Display(Name = "WorkDate_end", ResourceType = typeof(Resources.Resource))]
    public DateTime? WorkDate_end { get; set; }

    [Display(Name = "FeedValue", ResourceType = typeof(Resources.Resource))]
    public double? FeedValue { get; set; }

    [Display(Name = "FeedUnit", ResourceType = typeof(Resources.Resource))]
    public string FeedUnit { get; set; }


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