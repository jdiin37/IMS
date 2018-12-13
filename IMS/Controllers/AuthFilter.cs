using IMS.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{
    public class AuthFilter : AuthorizeAttribute
    {
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
           
            base.OnAuthorization(filterContext);

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;
            

        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CheckAuth(httpContext);
        }

        protected bool CheckAuth(HttpContextBase httpContext)
        {
            bool result = false;
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                    if (serializeModel.SessionGid != null)
                    {
                        using (BaseController baseController = new BaseController())
                        {
                            result = baseController.UpdateSessionID(serializeModel.ID, new Guid(serializeModel.SessionGid));
                        }
                    }

                    CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                    newUser.ID = serializeModel.ID;
                    newUser.Name = serializeModel.Name;
                    newUser.Email = serializeModel.Email;
                    newUser.Level = serializeModel.Level;
                    newUser.SessionGid = serializeModel.SessionGid;
                    HttpContext.Current.User = newUser;
                }
                catch
                {
                    FormsAuthentication.SignOut();
                    result = false;
                }
            }

            return result;
        }
        

    }
}