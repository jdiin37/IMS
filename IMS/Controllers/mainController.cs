using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class MainController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Logout()
        {
            return RedirectToAction("Index","Login");
        }





        [ChildActionOnly]
        public ActionResult NavMenu()
        {
            return View();
        }

        [ChildActionOnly]
        [ValueReport(IsCheck = false)]
        public ActionResult BoxMenu()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [ValueReport(IsCheck =false)]
        public ActionResult UserInfo()
        {
            return PartialView();
        }


    }
}