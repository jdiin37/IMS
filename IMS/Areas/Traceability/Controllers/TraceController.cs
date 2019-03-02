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
        public ActionResult Index(string traceNo,Guid? pigFarmId)
        {
            TraceVM vm = new TraceVM();


            if (traceNo == null)
            {
                vm.traceMaster = new TraceMaster() { TraceNo = "123" };
                
            }
            
            if(pigFarmId != null)
            {
                vm.pigFarm = IMSdb.PigFarm.Where(m => m.Id == pigFarmId).FirstOrDefault();
                vm.traceMaster.PigFarmId = pigFarmId.ToString();
            }
            else
            {
                vm.pigFarm = IMSdb.PigFarm.Where(m=> m.Status == "Y").FirstOrDefault();
                vm.traceMaster.PigFarmId = vm.pigFarm.Id.ToString();
            }

            ViewBag.TraceNo = vm.traceMaster.TraceNo;

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }


        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProduceForTrace(string traceNo)
        {
            //Lambda寫法
            //var comments = context.Comments.Where(c=>c.PhotoID== PhotoID);

            //Linq寫法
            var produces = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo
                           select c;

            //為了在View裡取得ID的值所以先存在ViewBag裡
            ViewBag.TraceNo = traceNo;

            return PartialView(produces.ToList());
        }

        [HttpPost]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProduceForTrace(TraceDetail traceDetail,string traceNo)
        {
            if (ModelState.IsValid)
            {
                IMSdb.TraceDetail.Add(traceDetail);
                IMSdb.SaveChanges();
            }
            else
            {
                
                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));


                TempData["ProduceErr"] = message;
            }

            var produces = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo
                           select c;

            ViewBag.TraceNo = traceNo;

            return PartialView("_ProduceForTrace",produces.ToList());
        }

        public PartialViewResult _CreateATraceDetail(string traceNo)
        {
            TraceDetail newTraceDetail = new TraceDetail();
            newTraceDetail.TraceNo = traceNo;

            ViewBag.TraceNo = traceNo;
            return PartialView("_CreateATraceDetail");
        }

        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProcessForTrace(string traceNo)
        {
            //Lambda寫法
            //var comments = context.Comments.Where(c=>c.PhotoID== PhotoID);

            //Linq寫法
            var processes = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo
                           select c;

            //為了在View裡取得ID的值所以先存在ViewBag裡
            ViewBag.TraceNo = traceNo;

            return PartialView(processes.ToList());
        }

    }
}