using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.DataAnalysis.Controllers
{
    public class HomeController : Controller
    {
        // GET: DataAnalysis/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}