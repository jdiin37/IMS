using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
    public class TraceController : BaseController
    {
        // GET: Traceability/Trace
        public ActionResult Index(string traceNo)
        {
            TraceVM vm = new TraceVM();

            if (traceNo == null)
            {
                vm.traceMaster = new TraceMaster() { TraceNo = "123" };
                
            }
            

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}