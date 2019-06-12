using IMS.Comm;
using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

      for (int a = 0; a < 200; a++)
      {
        int pigCnt = getRandomInt(15, 30);

        FeetToMeetData feetToMeet = new FeetToMeetData();


        feetToMeet.CreDate = DateTime.Now;
        feetToMeet.SowType = GetRandomString4(1);
        feetToMeet.BoarType = GetRandomString4(1);
        feetToMeet.TraceNo = feetToMeet.SowType + feetToMeet.BoarType + feetToMeet.CreDate.ToString("yyyyMMdd") + feetToMeet.CreDate.ToString("yyyyMMdd") + "-" + GetTraceNoSeqNo().ToString();

        feetToMeet.Stage1Days = getRandomInt(26, 32);
        feetToMeet.Stage2Days = getRandomInt(54, 60) - feetToMeet.Stage1Days;
        feetToMeet.Stage3Days = getRandomInt(125, 140) - (feetToMeet.Stage1Days + feetToMeet.Stage2Days);
        feetToMeet.Stage4Days = getRandomInt(200, 240) - (feetToMeet.Stage1Days + feetToMeet.Stage2Days + feetToMeet.Stage3Days);

        feetToMeet.Stage1sDate = feetToMeet.CreDate;
        feetToMeet.Stage1eDate = feetToMeet.Stage1sDate.Value.AddDays(feetToMeet.Stage1Days);
        feetToMeet.Stage2sDate = feetToMeet.Stage1eDate;
        feetToMeet.Stage2eDate = feetToMeet.Stage2sDate.Value.AddDays(feetToMeet.Stage2Days);
        feetToMeet.Stage3sDate = feetToMeet.Stage2eDate;
        feetToMeet.Stage3eDate = feetToMeet.Stage3sDate.Value.AddDays(feetToMeet.Stage3Days);
        feetToMeet.Stage4sDate = feetToMeet.Stage3eDate;
        feetToMeet.Stage4eDate = feetToMeet.Stage4sDate.Value.AddDays(feetToMeet.Stage4Days);


        feetToMeet.Stage1sPigCnt = pigCnt + getRandomInt(0, 3);
        feetToMeet.Stage1ePigCnt = pigCnt;
        feetToMeet.Stage2sPigCnt = pigCnt;
        feetToMeet.Stage2ePigCnt = pigCnt;
        feetToMeet.Stage3sPigCnt = pigCnt;
        feetToMeet.Stage3ePigCnt = pigCnt;
        feetToMeet.Stage4sPigCnt = pigCnt;
        feetToMeet.Stage4ePigCnt = pigCnt;

        feetToMeet.Stage1sWeight = 0;
        feetToMeet.Stage1eWeight = Math.Round(pigCnt * getRandomDouble(5, 7), 2);
        feetToMeet.Stage2sWeight = feetToMeet.Stage1eWeight;
        if (feetToMeet.SowType == "L")
        {

          feetToMeet.Stage2eWeight = Math.Round(pigCnt * getRandomDouble(12, 20), 2);
        }
        else
        {
          feetToMeet.Stage2eWeight = Math.Round(pigCnt * getRandomDouble(12, 24), 2);
        }
        feetToMeet.Stage3sWeight = feetToMeet.Stage2eWeight;
        if (feetToMeet.SowType == "L")
        {

          feetToMeet.Stage3eWeight = Math.Round(pigCnt * getRandomDouble(40, 60), 2);
        }
        else
        {
          feetToMeet.Stage3eWeight = Math.Round(pigCnt * getRandomDouble(44, 68), 2);
        }
        
        feetToMeet.Stage4sWeight = feetToMeet.Stage3eWeight;
        if (feetToMeet.SowType == "L")
        {
          feetToMeet.Stage4eWeight = Math.Round(pigCnt * getRandomDouble(60, 110), 2);
        }
        else
        {
          feetToMeet.Stage4eWeight = Math.Round(pigCnt * getRandomDouble(74, 120), 2);
        }
        
        feetToMeet.Stage1AddWeight = feetToMeet.Stage1eWeight - feetToMeet.Stage1sWeight;
        feetToMeet.Stage2AddWeight = feetToMeet.Stage2eWeight - feetToMeet.Stage2sWeight;
        feetToMeet.Stage3AddWeight = feetToMeet.Stage3eWeight - feetToMeet.Stage3sWeight;
        feetToMeet.Stage4AddWeight = feetToMeet.Stage4eWeight - feetToMeet.Stage4sWeight;

        feetToMeet.Stage1FeedWeight = Math.Round(feetToMeet.Stage1ePigCnt * 0.28 * feetToMeet.Stage1Days, 2);
        feetToMeet.Stage2FeedWeight = Math.Round(feetToMeet.Stage2ePigCnt * 0.67 * feetToMeet.Stage2Days, 2);
        feetToMeet.Stage3FeedWeight = Math.Round(feetToMeet.Stage3ePigCnt * 1.63 * feetToMeet.Stage3Days, 2);
        feetToMeet.Stage4FeedWeight = Math.Round(feetToMeet.Stage4ePigCnt * 3.4 * feetToMeet.Stage4Days, 2);

        feetToMeet.Stage1FeedToMeet = Math.Round(feetToMeet.Stage1FeedWeight/ feetToMeet.Stage1AddWeight, 2);
        feetToMeet.Stage2FeedToMeet = Math.Round(feetToMeet.Stage2FeedWeight / feetToMeet.Stage2AddWeight, 2);
        feetToMeet.Stage3FeedToMeet = Math.Round(feetToMeet.Stage3FeedWeight / feetToMeet.Stage3AddWeight, 2);
        feetToMeet.Stage4FeedToMeet = Math.Round(feetToMeet.Stage4FeedWeight / feetToMeet.Stage4AddWeight, 2);

        IMSdb.FeetToMeetData.Add(feetToMeet);
      }

      IMSdb.SaveChanges();

      return null;
    }


    public string GetRandomString4(int length)
    {
      var str = "LD";
      var next = new Random(Guid.NewGuid().GetHashCode());
      var builder = new StringBuilder();
      for (var i = 0; i < length; i++)
      {
        builder.Append(str[next.Next(0, str.Length)]);
      }
      return builder.ToString();
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