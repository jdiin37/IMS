using IMS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

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
            bool result = false;
          
            if (httpContext.Request.Cookies != null)
            {
                var accountID = httpContext.Request.Cookies["accountID"];
                var sessionID = httpContext.Request.Cookies["sessionID"];
                
                if (accountID != null && sessionID != null)
                {
                    
                    using (BaseController baseController = new BaseController())
                    {
                        result = baseController.UpdateSessionID(accountID.Value.ToString(), new Guid(sessionID.Value.ToString()));
                    }

                }
            }

            return result;
        }

    }
}