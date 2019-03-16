using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
    public class PigBasicController : BaseController
    {
        // GET: Traceability/PigBasic
        public ActionResult Index(string pigFarmId, string searchString)
        {
            ViewBag.CurrentFilter = searchString;

            if (pigFarmId == null)
            {
                if (IMSdb.PigFarm.Where(x => x.Status == "Y").Any())
                {
                    pigFarmId = IMSdb.PigFarm.Where(x => x.Status == "Y").FirstOrDefault().Id.ToString();
                }
                else
                {
                    TempData["Msg"] = "請先建立養豬場";
                    return RedirectToAction("Index", "PigFarm", new { area = "Sys" });
                }
            }
            
            var Pigs = IMSdb.PigBasic.Where(m => m.PigFarmId == pigFarmId && m.Status == "Y").ToList();

            ViewBag.PigFarmId = pigFarmId;

            if (!String.IsNullOrEmpty(searchString))
            {
                Pigs = Pigs.Where(s => s.PigNo.Contains(searchString)).ToList();
            }


            return View(Pigs);
        }

        public ActionResult Create(Guid pigFarmId)
        {
            if (pigFarmId == null)
            {
                return HttpNotFound();
            }

            ViewBag.PigFarmId = pigFarmId;
            ViewBag.PigFarmName = IMSdb.PigFarm.Where(m => m.Id == pigFarmId).FirstOrDefault().Name;

            PigBasic newPigBasic = new PigBasic();
            newPigBasic.PigFarmId = pigFarmId.ToString();

            return View("Create", newPigBasic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PigBasic pigBasic)
        {
            pigBasic.CreDate = DateTime.Now;
            pigBasic.CreUser = User.ID;
            pigBasic.Status = "Y";

            if (IMSdb.PigBasic.Where(m => m.PigNo == pigBasic.PigNo).Any())
            {
                ViewBag.ErrMsg = "編號重複";
                return View(pigBasic);
            }

            if (ModelState.IsValid)
            {

                IMSdb.PigBasic.Add(pigBasic);
                IMSdb.SaveChanges();
                return RedirectToAction("Index", new { pigFarmId = pigBasic.PigFarmId });
            }

            return View(pigBasic);
        }

        public ActionResult Edit(Guid pigGid)
        {
            if (pigGid == null)
            {
                return HttpNotFound();
            }

            PigBasic pigBasic = IMSdb.PigBasic.Find(pigGid);
            ViewBag.PigFarmId = pigBasic.PigFarmId;
            ViewBag.PigFarmName = IMSdb.PigFarm.Where(m => m.Id.ToString() == pigBasic.PigFarmId).FirstOrDefault().Name;
            
            return View("Edit", pigBasic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PigBasic pigBasic)
        {
            if (pigBasic == null)
            {
                return HttpNotFound();
            }

            PigBasic item = IMSdb.PigBasic.Find(pigBasic.GID);

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.ModDate = DateTime.Now;
                item.CreUser = User.ID;

                item.PigBirth = pigBasic.PigBirth;
                item.Parity = pigBasic.Parity;
                item.PigDad = pigBasic.PigDad;
                item.PigMom = pigBasic.PigMom;
                item.PigGrandDad = pigBasic.PigGrandDad;
                item.PigGrandMom = pigBasic.PigGrandMom;
                item.PigGGrandDad = pigBasic.PigGGrandDad;
                item.PigGGrandMom = pigBasic.PigGGrandMom;

                item.PigType = pigBasic.PigType;
                item.SameParity = pigBasic.SameParity;
                item.CommentBody = pigBasic.CommentBody;
                item.CommentFront = pigBasic.CommentFront;
                item.CommentEnd = pigBasic.CommentEnd;
                item.CommentSum = pigBasic.CommentSum;

                item.FirstBreeding = pigBasic.FirstBreeding;
                item.FirstBreedingAge = pigBasic.FirstBreedingAge;
                IMSdb.SaveChanges();
                return RedirectToAction("Index", new { pigFarmId = item.PigFarmId });
            }
            
            return View(pigBasic);
        }


        public ActionResult Delete(Guid pigGid)
        {
            if (pigGid == null)
            {
                return HttpNotFound();
            }


            PigBasic item = IMSdb.PigBasic.Find(pigGid);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            item.Status = "D";
            //IMSdb.PigBasic.Remove(item);
            IMSdb.SaveChanges();
            return RedirectToAction("Index", new { pigFarmId = item.PigFarmId });

        }



        public ActionResult FarrowingRecord(Guid pigGid)
        {
            if (pigGid == null)
            {
                return HttpNotFound();
            }


            PigBasic item = IMSdb.PigBasic.Find(pigGid);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public PartialViewResult _FarrowingRecord(Guid pigGid)
        {

            var farrowingRecords = from c in IMSdb.FarrowingRecord
                                   where c.PigGid == pigGid 
                                   orderby c.FarrowingDate descending
                                   select c;

            ViewBag.PigGid = pigGid;

            return PartialView(farrowingRecords.ToList());
        }


    }
}