using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
  public class TraceMaster
  {
    [Key]
    public int SeqNo { get; set; }

    [Required]
    [Index(IsUnique = true)]
    [MaxLength(30, ErrorMessage = "長度不得超過30"), MinLength(4, ErrorMessage = "長度不得小於4")]
    [Display(Name = "TraceNo", ResourceType = typeof(Resources.Resource))]
    public string TraceNo { get; set; }

    [Display(Name = "PigFarm", ResourceType = typeof(Resources.Resource))]
    public string PigFarmId { get; set; }

    [DisplayName("其他檢驗標章")]
    public string Remark { get; set; }

    [Display(Name = "PkgDate", ResourceType = typeof(Resources.Resource))]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? PkgDate { get; set; }

    [Display(Name = "PigCnt", ResourceType = typeof(Resources.Resource))]
    public int PigCnt { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    [Display(Name = "BornDate", ResourceType = typeof(Resources.Resource))]
    public DateTime BornDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    [Display(Name = "BornDate_end", ResourceType = typeof(Resources.Resource))]
    public DateTime BornDate_end { get; set; }



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