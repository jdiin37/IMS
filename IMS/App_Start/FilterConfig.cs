using IMS.Controllers;
using System.Web;
using System.Web.Mvc;

namespace IMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(){ View = "exceptionError"});

            filters.Add(new AuthFilter()); //Auth

            //filters.Add(new ValueReport() { IsCheck = true }); //Log 
        }
        
    }
}