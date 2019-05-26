using IMS.Comm;
using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.DataAnalysis.Controllers
{
  public class FeedToMeetController : BaseController
  {
    // GET: DataAnalysis/FeedToMeet
    

    public ActionResult Index(int? page, string traceNo, DateTime? sdate, DateTime? edate, string status)
    {
      if (!(sdate != null && edate != null) && !(sdate == null && edate == null))
      {
        TempData["Err"] = "請輸入完整的日期起迄日";
      }

      ViewBag.traceNo = traceNo;

      ViewBag.sdate = sdate?.ToString("yyyy-MM-dd");
      ViewBag.edate = edate?.ToString("yyyy-MM-dd");
      ViewBag.status = status;

      var traceMasters = getTraceMasters(traceNo, sdate, edate, status);


      int pageNumber = (page ?? 1);

      return View(traceMasters.ToPagedList(pageNumber, Config.PageSize));
    }

    private IEnumerable<TraceMaster> getTraceMasters(string traceNo, DateTime? sdate, DateTime? edate, string status)
    {
      IEnumerable<TraceMaster> lists;


      if (sdate != null && edate != null)
      {
        lists = IMSdb.TraceMaster.Where(x => x.CreDate >= sdate && x.CreDate <= edate).Select(m => m).OrderByDescending(m => m.TraceNo);
      }
      else
      {
        lists = IMSdb.TraceMaster.Select(z => z).OrderByDescending(m => m.TraceNo);
      }


      if (status != null && status != "")
      {
        lists = lists.Where(x => x.Status == status);
      }

      if (traceNo != null && traceNo != "")
      {
        lists = lists.Where(x => x.TraceNo.Contains(traceNo));
      }
      

      return lists;
    }

    
    public ActionResult Analysis(string tradeList)
    {
      string[] tradeNoArr = tradeList.Split(',');
      
      foreach(string tradeNo in tradeNoArr)
      {





      }

      ViewBag.TraceNos = tradeList.Substring(1);

      FeedToMeetVM vm = new FeedToMeetVM();

      vm.stageArea = new List<StageArea>
      {
        new StageArea
        {
          stageName = "哺乳期",
          startDate = new DateTime(2018,01,01),
          endDate = new DateTime(2018,02,01),
          startPigCnt =20,
          endPigCnt = 18,
          startWeight = 0,
          endWeight = 110,
          FeedWeight = 150,

        },
        new StageArea
        {
          stageName = "保育期",
          startDate = new DateTime(2018,02,01),
          endDate = new DateTime(2018,03,01),
          startPigCnt =18,
          endPigCnt = 18,
          startWeight = 110,
          endWeight = 354,
          FeedWeight = 420,
        },
        new StageArea
        {
          stageName = "生長期",
          startDate = new DateTime(2018,03,01),
          endDate = new DateTime(2018,05,15),
          startPigCnt =18,
          endPigCnt = 18,
          startWeight = 354,
          endWeight = 1083,
          FeedWeight = 1832,
        },
        new StageArea
        {
          stageName = "肥育期",
          startDate = new DateTime(2018,05,15),
          endDate = new DateTime(2018,09,01),
          startPigCnt =18,
          endPigCnt = 18,
          startWeight = 1083,
          endWeight = 1963,
          FeedWeight = 3000,
        },
      };

      return View(vm);
    }


    [HttpPost]
    public ActionResult Analysis(List<TraceMaster> TraceNos)
    {

      if (Request.IsAjaxRequest())
      {
        return Json(TraceNos);
      }
      return null;
    }

    public ActionResult CreateRandonData()
    {

      for(int a=0; a < 100; a++)
      {
        int pigCnt = getRandomInt(15,30);

        FeetToMeetData feetToMeet = new FeetToMeetData();


        feetToMeet.CreDate = DateTime.Now;
        feetToMeet.Stage1Days = getRandomInt(26,32);
        feetToMeet.Stage2Days = getRandomInt(54,60) - feetToMeet.Stage1Days;
        feetToMeet.Stage3Days = getRandomInt(125,140) - (feetToMeet.Stage1Days + feetToMeet.Stage2Days);
        feetToMeet.Stage4Days = getRandomInt(200, 240) - (feetToMeet.Stage1Days + feetToMeet.Stage2Days + feetToMeet.Stage3Days);


        feetToMeet.Stage1sPigCnt = pigCnt + getRandomInt(0, 3);
        feetToMeet.Stage1ePigCnt = pigCnt;
        feetToMeet.Stage2sPigCnt = pigCnt;
        feetToMeet.Stage2ePigCnt = pigCnt;
        feetToMeet.Stage3sPigCnt = pigCnt;
        feetToMeet.Stage3ePigCnt = pigCnt;
        feetToMeet.Stage4sPigCnt = pigCnt;
        feetToMeet.Stage4ePigCnt = pigCnt;

        feetToMeet.Stage1sWeight = 0;
        feetToMeet.Stage1eWeight = pigCnt * getRandomDouble(5,7);
        feetToMeet.Stage2sWeight = feetToMeet.Stage1eWeight;
        feetToMeet.Stage2eWeight = pigCnt * getRandomDouble(12, 20);
        feetToMeet.Stage3sWeight = feetToMeet.Stage2eWeight;
        feetToMeet.Stage3eWeight = pigCnt * getRandomDouble(40, 60);
        feetToMeet.Stage4sWeight = feetToMeet.Stage3eWeight;
        feetToMeet.Stage4eWeight = pigCnt * getRandomDouble(60, 110);

      }

      return null;
    }

    
    public ActionResult TestRandan()
    {
      double a = getRandomDouble(5, 7);
      double b = getRandomDouble(5, 7);

      double c = getRandomDouble(5, 7);
      double d = getRandomDouble(5, 7);
      return null;
    }

    private int getRandomInt(int x,int y)
    {
      Random r = new Random(Guid.NewGuid().GetHashCode());//亂數種子
      int i = r.Next(x, y);//
      return i;
    }
    

    private double getRandomDouble(double min,double max)
    {
      Random r = new Random(Guid.NewGuid().GetHashCode());
      double i = r.NextDouble() * (max - min) + min;
      return i;
    }

  }
}