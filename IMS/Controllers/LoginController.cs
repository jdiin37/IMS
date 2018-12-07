using IMS.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{

    [ValueReport(IsCheck = false)]
    public class LoginController : BaseController
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string lang,string msg)
        {
            //清空所有 Session 資料
            //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();

            //Session.Abandon();
            //Session.Clear();            
            //FormsAuthentication.SignOut();

            if (Request.Cookies["accountID"] != null)
            {
                var c = new HttpCookie("accountID");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            if (Request.Cookies["sessionID"] != null)
            {
                var c = new HttpCookie("sessionID");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }


            ViewBag.Msg = msg;

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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account,string password,string remember)
        {
            
            var user = IMSdb.Account.Where(m => m.AccountNo == account && m.Password == password).FirstOrDefault();
            if (user != null)
            {
                Guid sessionGid = new Guid();
                if (remember == "1")
                {
                    sessionGid = CreateSessionID(user.AccountNo, DateTime.Now.AddDays(15));
                    HttpCookie accountCookie = new HttpCookie("accountID", user.AccountNo) { Expires = DateTime.Now.AddDays(15), Path = "/" };
                    HttpCookie sessionCookie = new HttpCookie("sessionID", sessionGid.ToString()) { Expires = DateTime.Now.AddDays(15), Path = "/" };
                    Response.Cookies.Add(accountCookie);
                    Response.Cookies.Add(sessionCookie);
                }
                else
                {
                    sessionGid = CreateSessionID(user.AccountNo, DateTime.Now.AddHours(3));
                    HttpCookie accountCookie = new HttpCookie("accountID", user.AccountNo) { Expires = DateTime.Now.AddHours(3), Path = "/" };
                    HttpCookie sessionCookie = new HttpCookie("sessionID", sessionGid.ToString()) { Expires = DateTime.Now.AddHours(3), Path = "/" };
                    Response.Cookies.Add(accountCookie);
                    Response.Cookies.Add(sessionCookie);
                }
                
                //Session["sessionID"] = sessionGid.ToString();
                //Session["accountID"] = user.AccountNo;

                return RedirectToAction("Index","Main");
            }
            else
            {
                return RedirectToAction("Index",new { msg = "帳號密碼有誤"});
            }
            
        }

     

        //public ActionResult ChangeLang(string lang)
        //{
        //    ViewBag.langSys = lang;
        //    return RedirectToAction("Index");
        //}
    }
}