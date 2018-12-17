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
            
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;
            
            base.OnAuthorization(filterContext);

        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CheckAuth(httpContext);
        }
        
        protected bool CheckAuth(HttpContextBase httpContext)
        {
            bool result = false;
            //HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var currentUser = HttpContext.Current.User as CustomPrincipal;
            if (currentUser != null)
            {
                result = true;                
            }else{
                FormsAuthentication.SignOut();
                result = false;
            }

            return result;
        }
        

    }
}