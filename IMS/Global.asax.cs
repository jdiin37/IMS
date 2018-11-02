using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace IMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpCookie langCookie = Request.Cookies["sysLang"];

            CultureInfo currentCulture = null;
            if (langCookie == null || String.IsNullOrWhiteSpace(langCookie.Value) || langCookie.Value == "null")
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
                HttpCookie newCookie = new HttpCookie("sysLang", currentCulture.Name);
                newCookie.Path = "/";
                newCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(newCookie);
            }
            else
                currentCulture = new CultureInfo(langCookie.Value);

            Thread.CurrentThread.CurrentUICulture = currentCulture;

            // server Culture run 在 Invariant culture下
            Thread.CurrentThread.CurrentCulture = new CultureInfo("");

            // 切換 datatimeFormate culture 但是 datetime 記算還是以西元Calander計算
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat = new CultureInfo(currentCulture.Name).DateTimeFormat;
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.Calendar = CultureInfo.InvariantCulture.Calendar;

            // HTTP-GET 的 modelbinding，依據改過calander的CurrentCulture為主
            //CustomModelBindersConfig.RegisterCustomModelBinders();

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                // 先取得該使用者的 FormsIdentity
                FormsIdentity id = (FormsIdentity)User.Identity;
                // 再取出使用者的 FormsAuthenticationTicket
                FormsAuthenticationTicket ticket = id.Ticket;
                // 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
                string[] roles = ticket.UserData.Split(new char[] { ',' });
                // 指派角色到目前這個 HttpContext 的 User 物件去
                //剛剛在創立表單的時候，你的UserData 放使用者名稱就是取名稱，我放的是群組代號，所以取出來就是群組代號
                //然後會把這個資料放到Context.User內
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }

        }


        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        try
        //        {
        //            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //            //CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
        //            //CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
        //            //newUser.ID = serializeModel.ID;
        //            //newUser.Name = serializeModel.Name;
        //            //newUser.Email = serializeModel.Email;
        //            //newUser.roles = serializeModel.roles;
        //            //HttpContext.Current.User = newUser;
        //        }
        //        catch
        //        {
        //            FormsAuthentication.SignOut();
        //            Response.Redirect("/LogIn");

        //        }
        //    }
        //}
    }
}
