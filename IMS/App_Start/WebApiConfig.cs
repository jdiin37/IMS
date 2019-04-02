using IMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IMS
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {

      GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //不使用XML格式

      config.Filters.Add(new GlobalApiLogActionFilter());


      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}
