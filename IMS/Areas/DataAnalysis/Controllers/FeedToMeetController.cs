using IMS.Comm;
using IMS.Controllers;
using IMS.Models;
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
      
      return View();
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