using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{
    public class MainController : BaseController
    {
    
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Logout()
        {
            //清空所有 Session 資料
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var c = new HttpCookie(FormsAuthentication.FormsCookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index","Login");
        }


        public ActionResult ShowPaper()
        {
            return View();
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