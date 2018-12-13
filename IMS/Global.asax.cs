using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);


            BundleConfig.RegisterBundles(BundleTable.Bundles);
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

        }
    }
}
