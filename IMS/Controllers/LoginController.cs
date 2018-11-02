using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string lang)
        {
            //清空所有 Session 資料
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            if (lang == null)
            {
                return View();
            }
            else
            {
                ViewBag.lang = lang;
                return View(lang);
            }
        }

        public ActionResult Login()
        {

            if(true)
            {
                return RedirectToAction("Index","Main");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        //public ActionResult ChangeLang(string lang)
        //{
        //    ViewBag.langSys = lang;
        //    return RedirectToAction("Index");
        //}
    }
}