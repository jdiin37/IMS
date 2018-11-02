﻿using IMS.Controllers;
using System.Web;
using System.Web.Mvc;

namespace IMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new ValueReport()); //Log 
        }
        
    }
}