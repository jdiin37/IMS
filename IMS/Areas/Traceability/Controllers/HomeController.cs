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
        public ActionResult Index(int? page)
        {
            var traceMasters = getTraceMasters("");


            int pageNumber = (page ?? 1);

            return View(traceMasters.ToPagedList(pageNumber, Config.PageSize));
        }

        private List<TraceMaster> getTraceMasters(string status)
        {
            if(status == "")
            {
                return IMSdb.TraceMaster.Select(m=>m).OrderByDescending(m=>m.TraceNo).ToList();
            }
            else
            {
                return IMSdb.TraceMaster.Where(m => m.Status == status).OrderByDescending(m => m.TraceNo).ToList();
            }
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