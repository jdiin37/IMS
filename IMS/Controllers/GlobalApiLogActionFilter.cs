using IMS.Comm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace IMS.Controllers
{
  public class GlobalApiLogActionFilter : System.Web.Http.Filters.ActionFilterAttribute
  {
    public override void OnActionExecuting(HttpActionContext context)
    {
      try
      {
        //Entities db = new Entities();
        //var log = new ApiLog()
        //{
        //  Method = context.Request.Method.ToString(),
        //  Uri = context.Request.RequestUri.AbsolutePath,
        //  Input = JsonConvert.SerializeObject(context.ActionArguments),
        //};
        //if (context.RequestContext.Principal.Identity.IsAuthenticated)
        //{
        //  log.AccountGid = ((CustomPrincipal)context.RequestContext.Principal).GID;
        //}
        //db.ApiLog.Add(log);
        //db.SaveChanges();
      }
      catch
      {
        ; // i see nothing (艸)
      }

    }
    public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext context)
    {
      //string errorMsg = string.Empty;

      //string controllerName = context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
      //string actionName = context.ActionContext.ActionDescriptor.ActionName;

      //var objectContent = context.Response.Content as ObjectContent;
      //if (objectContent != null)
      //{
      //  var type = objectContent.ObjectType; //type of the returned object
      //  var value = objectContent.Value; //holding the returned value
      //  var messageProp = value.GetType().GetProperty("Message");
      //  var successProp = value.GetType().GetProperty("Message");
      //  if (messageProp != null && successProp != null)
      //  {
      //    object m = messageProp.GetValue(value, null);
      //    object s = successProp.GetValue(value, null);

      //    if (m is string && s is bool)
      //    {
      //      string msg = m as string;
      //      bool success = Convert.ToBoolean(s);
      //      if (!success && msg != string.Empty)
      //      {
      //        msg = JsonConvert.SerializeObject(new
      //        {
      //          controller = controllerName,
      //          action = actionName,
      //          errorMessage = msg
      //        });

      //        SlackClient sc = new SlackClient();
      //        sc.PostMessage(msg);
      //      }
      //    }
      //  }
      //}
    }
  }
}