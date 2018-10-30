using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Dashboard.Controllers
{
    public class MainController : Controller
    {
        // GET: Dashboard/Main
        public ActionResult Index()
        {
            return View();
        }
    }
}