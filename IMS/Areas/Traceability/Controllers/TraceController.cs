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

        //public ActionResult Edit(string traceNo, Guid? pigFarmId)
        //{
            
        //}
        

        public TraceMaster SaveTraceMaster(TraceMaster traceMaster,string status)
        {
            TraceMaster modTraceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceMaster.TraceNo).FirstOrDefault();

            modTraceMaster.PigFarmId = traceMaster.PigFarmId;
            modTraceMaster.ModDate = DateTime.Now;
            modTraceMaster.ModUser = User.ID;
            modTraceMaster.Status = status;

            IMSdb.SaveChanges();
            return modTraceMaster;
        }

        public TraceMaster AddTempTraceMaster(TraceMaster traceMaster)
        {
            IMSdb.TraceMaster.Add(traceMaster);
            IMSdb.SaveChanges();
            return traceMaster;
        }



        // GET: Traceability/Trace
        public ActionResult CreateEdit(string traceNo,Guid? pigFarmId)
        {
            TraceVM vm = new TraceVM();


            if (traceNo == null)
            {
                TraceMaster traceMaster = new TraceMaster()
                {
                    TraceNo = GetTraceNo(),
                    CreDate = DateTime.Now,
                    CreUser = User.ID,              
                    Status = "T",
                 };
                vm.traceMaster = traceMaster;

                //豬場ID
                vm.pigFarm = IMSdb.PigFarm.Where(m => m.Status == "Y").FirstOrDefault();
                vm.traceMaster.PigFarmId = vm.pigFarm.Id.ToString();

                AddTempTraceMaster(traceMaster);

            }
            else
            {
                vm.traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();
            }
            
            if(pigFarmId != null)
            {
                vm.pigFarm = IMSdb.PigFarm.Where(m => m.Id == pigFarmId).FirstOrDefault();
                vm.traceMaster.PigFarmId = pigFarmId.ToString();
                SaveTraceMaster(vm.traceMaster, "T");
            }
            

            ViewBag.TraceNo = vm.traceMaster.TraceNo;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(TraceMaster traceMaster)
        {
            return RedirectToAction("Index");
        }



        public ActionResult Index()
        {
            return RedirectToAction("Index","Home",new { area = "Traceability"});
        }


        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProduceForTrace(string traceNo)
        {
            
            var produces = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo
                           orderby c.WorkDate descending
                           select c;
            
            ViewBag.TraceNo = traceNo;

            return PartialView(produces.ToList());
        }

        [HttpPost]
        public PartialViewResult _ProduceForTrace(TraceDetail traceDetail,string traceNo)
        {
            if (ModelState.IsValid)
            {
                traceDetail.CreDate = DateTime.Now;
                traceDetail.CreUser = User.ID;
                traceDetail.WorkClass = "produce";
                traceDetail.Status = "Y";
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
                           orderby c.WorkDate descending
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


        public ActionResult DeleteTraceDetail(int seqNo)
        {
            
            TraceDetail traceDetail = IMSdb.TraceDetail.Find(seqNo);
            
            IMSdb.TraceDetail.Remove(traceDetail);
            IMSdb.SaveChanges();
            
            return RedirectToAction("CreateEdit", new { traceNo = traceDetail.TraceNo });
        }


        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProcessForTrace(string traceNo)
        {
            
            var processes = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo
                           select c;
            
            ViewBag.TraceNo = traceNo;

            return PartialView(processes.ToList());
        }

    }
}