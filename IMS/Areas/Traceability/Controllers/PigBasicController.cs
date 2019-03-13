using IMS.Controllers;
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

            //CategoryVM vm = new CategoryVM()
            //{
            //    categoryLists = IMSdb.Category.ToList(),
            //    categorySubs = IMSdb.CategorySub.Where(m => m.CategoryID == categoryId).ToList()
            //};

            var Pigs = IMSdb.PigBasic.Where(m => m.PigFarmId == pigFarmId && m.Status == "Y").ToList();

            ViewBag.PigFarmId = pigFarmId;
            //ViewBag.CategoryListName = IMSdb.Category.Where(m => m.CategoryID == categoryId).FirstOrDefault().CategoryName;


            if (!String.IsNullOrEmpty(searchString))
            {
                Pigs = Pigs.Where(s => s.PigNo.Contains(searchString)).ToList();
            }


            return View(Pigs);
        }
    }
}