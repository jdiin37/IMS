using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Traceability/Home
        public ActionResult Index()
        {
            return View();
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