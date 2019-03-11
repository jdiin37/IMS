using IMS.Controllers;
using IMS.DAL;
using IMS.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace IMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //Database.SetInitializer(new IMSDBInitializer());

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

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


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
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
                            if(!baseController.UpdateSessionID(serializeModel.ID, new Guid(serializeModel.SessionGid)))
                            {
                                //SessionID失效
                                FormsAuthentication.SignOut();
                                Response.Redirect("/Login");
                            }                          
                        }
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect("/Login");
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
                    Response.Redirect("/Login");
                }
            }
        }
    }
}
