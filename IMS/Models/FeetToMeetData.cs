using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
  public class FeetToMeetData
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid GID { get; set; }

    public string TraceNo { get; set; }

    public int Stage1sPigCnt { get; set; }

    public int Stage1ePigCnt { get; set; }

    public int Stage1Days { get; set; }

    public double Stage1sWeight { get; set; }

    public double Stage1eWeight { get; set; }

    public DateTime? Stage1sDate { get; set; }

    public DateTime? Stage1eDate { get; set; }

    public double Stage1FeedWeight { get; set; }

    public double Stage1AddWeight { get; set; }

    public double Stage1FeedToMeet { get; set; }

    public int Stage2sPigCnt { get; set; }

    public int Stage2ePigCnt { get; set; }

    public int Stage2Days { get; set; }

    public double Stage2sWeight { get; set; }

    public double Stage2eWeight { get; set; }

    public DateTime? Stage2sDate { get; set; }

    public DateTime? Stage2eDate { get; set; }

    public double Stage2FeedWeight { get; set; }

    public double Stage2AddWeight { get; set; }

    public double Stage2FeedToMeet { get; set; }

    public int Stage3sPigCnt { get; set; }

    public int Stage3ePigCnt { get; set; }

    public int Stage3Days { get; set; }

    public double Stage3sWeight { get; set; }

    public double Stage3eWeight { get; set; }

    public DateTime? Stage3sDate { get; set; }

    public DateTime? Stage3eDate { get; set; }

    public double Stage3FeedWeight { get; set; }

    public double Stage3AddWeight { get; set; }

    public double Stage3FeedToMeet { get; set; }

    public int Stage4sPigCnt { get; set; }

    public int Stage4ePigCnt { get; set; }

    public int Stage4Days { get; set; }

    public double Stage4sWeight { get; set; }

    public double Stage4eWeight { get; set; }

    public DateTime? Stage4sDate { get; set; }

    public DateTime? Stage4eDate { get; set; }

    public double Stage4FeedWeight { get; set; }

    public double Stage4AddWeight { get; set; }

    public double Stage4FeedToMeet { get; set; }


    [StringLength(2)]
    [DefaultValue("Y")]
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
