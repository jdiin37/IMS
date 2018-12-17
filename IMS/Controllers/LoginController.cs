using IMS.Models.Auth;
using IMS.Resources;
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
            
            ViewBag.Msg = msg;

            return View();           
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account,string password,string remember)
        {

            Logout();
            var user = IMSdb.Account.Where(m => m.AccountNo == account && m.Password == password).FirstOrDefault();
            if (user != null)
            {
                 
                Guid sessionGid = new Guid(); //生成sessionGID

                if (remember == "1")
                    sessionGid = CreateSessionID(user.AccountNo, DateTime.Now.AddDays(15));
                else
                    sessionGid = CreateSessionID(user.AccountNo, DateTime.Now.AddHours(3));

                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();

                serializeModel.ID = user.AccountNo;
                serializeModel.Name = user.AccountName;
                serializeModel.Email = user.Email;
                serializeModel.Level = user.Level;
                serializeModel.SessionGid = sessionGid.ToString();

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = null;

                if (remember == "1")
                    authTicket = new FormsAuthenticationTicket(1, user.AccountName, DateTime.Now, DateTime.Now.AddDays(15), false, userData);
                else
                    authTicket = new FormsAuthenticationTicket(1, user.AccountName, DateTime.Now, DateTime.Now.AddHours(3), false, userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = authTicket.Expiration, Path = "/" };
                Response.Cookies.Add(faCookie);
                
                
                return RedirectToAction("Index","Main");
            }
            else
            {
                return RedirectToAction("Index",new { msg = Resource.Msg_LoginError});
            }
            
        }
            
    }
}