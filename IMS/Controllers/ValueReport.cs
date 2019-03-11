using IMS.DAL;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMS.Controllers
{
    public class ValueReport : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }

        void LogValues(RouteData routeData)
        {
            using (IMSDBContext Db = new IMSDBContext())
            {
                var areaName = routeData.DataTokens["area"] == null ? "N/A" : routeData.DataTokens["area"];
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                var parame = routeData.Values["id"] == null ? "N/A" : routeData.Values["id"];

                ActionLog actionlog = new ActionLog()
                {
                    AreaName = areaName.ToString(),
                    ActionName = actionName.ToString(),
                    ControlName = controllerName.ToString(),
                    Parame = parame.ToString(),
                    CreateBy = "N/A",
                    LogTime = DateTime.Now,
                };

                Db.ActionLog.Add(actionlog);
                Db.SaveChanges();
            }

        }
        //2.8-4 覆寫OnActionExecuting方法,執行LogValues方法,透過ActionExecutingContext.RouteData屬性傳入RouteData物件
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if(IsCheck)
                LogValues(filterContext.RouteData);

        }

        void RequestLog()
        {
            using (IMSDBContext Db = new IMSDBContext())
            {
                var ip = HttpContext.Current.Request.ServerVariables["Local_Addr"];
                var host = HttpContext.Current.Request.ServerVariables["Server_Name"];

                var browser = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
                var requestType = HttpContext.Current.Request.RequestType;
                var userHostAddress = HttpContext.Current.Request.UserHostAddress;
                var userHostName = HttpContext.Current.Request.UserHostName;
                var httpMethod = HttpContext.Current.Request.HttpMethod;

                RequestLog request = new RequestLog()
                {
                    IP = ip,
                    Host = host,
                    browser = browser,
                    requestType = requestType,
                    userHostAddress = userHostAddress,
                    userHostName = userHostName,
                    httpMethod = httpMethod,
                    LogTime = DateTime.Now
                };

                Db.RequestLog.Add(request);
                Db.SaveChanges();
            }

        }
        //2.8-6 覆寫OnActionExecuted方法,執行RequestLog方法
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (IsCheck)
                RequestLog();
        }

    }
}