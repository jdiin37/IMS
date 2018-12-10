using IMS.Models;
using IMS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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




        #region File Service
        protected ActionResult ImageNotFound()
        {
            return File(Server.MapPath("~/Image/no_image_found.jpg"), "image/jpg");
        }
        #endregion


    }
}