using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMS.Comm;

namespace IMS.Areas.Traceability.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Traceability/Home
        public ActionResult Index(int? page, string traceNo,DateTime? sdate,DateTime? edate,string status)
        {
            if (!(sdate != null && edate != null) && !(sdate == null && edate == null))
            {
                TempData["Err"] = "請輸入完整的日期起迄日";
            }

            ViewBag.traceNo = traceNo;

            ViewBag.sdate = sdate?.ToString("yyyy-MM-dd") ;
            ViewBag.edate = edate?.ToString("yyyy-MM-dd");

            var traceMasters = getTraceMasters(traceNo, sdate, edate, status);


            int pageNumber = (page ?? 1);

            return View(traceMasters.ToPagedList(pageNumber, Config.PageSize));
        }

        private IEnumerable<TraceMaster> getTraceMasters(string traceNo,DateTime? sdate, DateTime? edate, string status)
        {
            IEnumerable<TraceMaster> lists ;


            if (sdate != null && edate != null)
            {
                lists = IMSdb.TraceMaster.Where(x=>x.CreDate >= sdate && x.CreDate <= edate).Select(m => m).OrderByDescending(m => m.TraceNo);
            }
            else
            {
                lists = IMSdb.TraceMaster.Select(z=>z).OrderByDescending(m => m.TraceNo);
            }


            if (status != null && status != "")
            {
                lists = lists.Where(x=>x.Status == status);
            }

            if(traceNo != null && traceNo != "")
            {
                lists = lists.Where(x => x.TraceNo.Contains(traceNo));
            }

            return lists.ToList();
        }
        
        public ActionResult GetWorkList()
        {
            var workList = IMSdb.WorkBasic.Where(m => m.Status != "N").OrderBy(m=>m.WorkCode).ToList();
            if (Request.IsAjaxRequest())
            {
                if (workList == null)
                {
                    return Json(workList, JsonRequestBehavior.AllowGet);
                }

                return Json(workList, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
            }

            return Json(workList, JsonRequestBehavior.AllowGet);
        }
    }
}