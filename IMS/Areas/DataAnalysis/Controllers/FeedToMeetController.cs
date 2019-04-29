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
      FeedToMeetVM vm = new FeedToMeetVM();

      vm.stageArea = new List<StageArea>
      {
        new StageArea
        {
          stageName = "哺乳期",
          startDate = new DateTime(2018,01,01),
          endDate = new DateTime(2018,02,01),
        },
        new StageArea
        {
          stageName = "保育期",
          startDate = new DateTime(2018,02,01),
          endDate = new DateTime(2018,03,01),
        },
        new StageArea
        {
          stageName = "生長期",
          startDate = new DateTime(2018,03,01),
          endDate = new DateTime(2018,05,15),
        },
        new StageArea
        {
          stageName = "肥育期",
          startDate = new DateTime(2018,05,15),
          endDate = new DateTime(2018,09,01),
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


  }
}