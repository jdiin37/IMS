using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
  public class FeedToMeetVM
  {
    public List<StageArea> stageArea;
  }


  public class StageArea
  {
    public string stageName { get; set; }

    public int PigCnt { get; set; }

    public int StageDays { get; set; }

    public double startWeight { get; set; }

    public DateTime startDate { get; set; }

    public double endWeight { get; set; }

    public DateTime endDate { get; set; }

    public double FeedWeight { get; set; }



  }


}