﻿using IMS.Controllers;
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
                TraceMaster newTraceMaster = new TraceMaster()
                {
                    TraceNo = GetTraceNo(),
                    CreDate = DateTime.Now,
                    CreUser = User.ID,              
                    Status = "T",
                 };
                vm.traceMaster = newTraceMaster;

                //豬場ID
                vm.pigFarm = IMSdb.PigFarm.Where(m => m.Status == "Y").FirstOrDefault();
                vm.traceMaster.PigFarmId = vm.pigFarm.Id.ToString();

                AddTempTraceMaster(vm.traceMaster);

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

        public ActionResult TempTrace(string traceNo)
        {
            if (traceNo != null)
            {
                var traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();
                SaveTraceMaster(traceMaster, "T");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DoneTrace(string traceNo)
        {
            if (traceNo != null)
            {
                var traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();
                SaveTraceMaster(traceMaster, "Y");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? seqNo)
        {
            TraceMaster item = IMSdb.TraceMaster.Find(seqNo);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            //delete Master
            IMSdb.TraceMaster.Remove(item);

            //delete Details
            var details = IMSdb.TraceDetail.Where(m => m.TraceNo == item.TraceNo);
            IMSdb.TraceDetail.RemoveRange(details);

            IMSdb.SaveChanges();
            return RedirectToAction("Index");

        }



        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _ProduceForTrace(string traceNo)
        {
            
            var produces = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo && c.WorkClass == "produce"
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
                           where c.TraceNo == traceNo && c.WorkClass == "produce"
                           orderby c.WorkDate descending
                           select c;
                           

            ViewBag.TraceNo = traceNo;

            return PartialView("_ProduceForTrace",produces.ToList());
        }
        

        public PartialViewResult _CreateATraceDetail(string traceNo)
        {
            
            ViewBag.TraceNo = traceNo;
            return PartialView("_CreateATraceDetail");
        }

        public PartialViewResult _CreateATraceDetail2(string traceNo)
        {
            
            ViewBag.TraceNo = traceNo;
            return PartialView("_CreateATraceDetail2");
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
                           where c.TraceNo == traceNo && c.WorkClass =="process"
                           orderby c.WorkDate descending
                           select c;
            
            ViewBag.TraceNo = traceNo;

            return PartialView(processes.ToList());
        }

        [HttpPost]
        public PartialViewResult _ProcessForTrace(TraceDetail traceDetail, string traceNo)
        {
            if (ModelState.IsValid)
            {
                traceDetail.CreDate = DateTime.Now;
                traceDetail.CreUser = User.ID;
                traceDetail.WorkClass = "process";
                traceDetail.Status = "Y";
                IMSdb.TraceDetail.Add(traceDetail);
                IMSdb.SaveChanges();
            }
            else
            {

                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));


                TempData["ProcessErr"] = message;
            }

            var process = from c in IMSdb.TraceDetail
                           where c.TraceNo == traceNo && c.WorkClass == "process"
                           orderby c.WorkDate descending
                           select c;


            ViewBag.TraceNo = traceNo;

            return PartialView("_ProcessForTrace", process.ToList());
        }

    }
}