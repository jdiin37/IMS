using IMS.Models;
using IMS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{
    public class BaseController : Controller
    {
        protected IMSContext IMSdb;

        public BaseController()
            : base()
        {
            IMSdb = new IMSContext();
        }
        
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
        
        public Account CurrentUser
        {
            get
            {
                return IMSdb.Account.First(m => m.AccountNo == User.ID);
            }
        }      

        public Guid CreateSessionID(string accountNo,DateTime deadLineTime)
        {
            Guid sessionGid = new Guid();
            sessionGid = Guid.NewGuid();

            AccountSession accountSession = new AccountSession
            {
                AccountNo = accountNo,
                SessionGID = sessionGid,
                CreDate = DateTime.Now,
                DeadLineTime = deadLineTime
            };

            IMSdb.AccountSession.Add(accountSession);
            IMSdb.SaveChanges();

            return sessionGid;
        }

        public bool UpdateSessionID(string accountNo,Guid sessionGid)
        {
            
            AccountSession accountSession = IMSdb.AccountSession.Where(m => m.AccountNo == accountNo && m.SessionGID == sessionGid &&m.DeadLineTime > DateTime.Now).FirstOrDefault();

            if(accountSession != null)
            {
                accountSession.LastActionTime = DateTime.Now;
                
                IMSdb.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool ClearSessionID(string accountNo, Guid sessionGid)
        {

            AccountSession accountSession = IMSdb.AccountSession.Where(m => m.AccountNo == accountNo && m.SessionGID == sessionGid && m.DeadLineTime > DateTime.Now).FirstOrDefault();


            if (accountSession != null)
            {
                accountSession.DeadLineTime = DateTime.Now;
                IMSdb.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Logout()
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
            if(User != null)
            {
                ClearSessionID(User.ID, new Guid(User.SessionGid));
            }
            
        }


        public ActionResult ToLogingPage()
        {
            Logout();
            return RedirectToAction("Index", "Login");
        }



        #region File Service
        protected ActionResult ImageNotFound()
        {
            return File(Server.MapPath("~/Image/no_image_found.jpg"), "image/jpg");
        }
        #endregion

        #region Get SeqNo
        protected String GetTraceNo()
        {
            var lastNo = IMSdb.SeqNo.Where(m => m.Name == "TraceNo").FirstOrDefault();
            DateTime lastDate = lastNo.ModDate;

            

            if(DateTime.Today > lastDate)
            {
                lastNo.CurrentValue = 0 + lastNo.IncrementValue;
            }
            else
            {
                lastNo.CurrentValue = lastNo.CurrentValue + lastNo.IncrementValue;
            }
            
            lastNo.ModDate = DateTime.Now;
            IMSdb.SaveChanges();
            
            return DateTime.Today.ToString("yyyyMMdd") + lastNo.CurrentValue.ToString("000");

        }
        #endregion
    }
}