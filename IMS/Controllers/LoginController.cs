using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string lang)
        {
            if(lang == null)
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

        public ActionResult ChangeLang(string lang)
        {
            ViewBag.langSys = lang;
            return RedirectToAction("Index");
        }
    }
}