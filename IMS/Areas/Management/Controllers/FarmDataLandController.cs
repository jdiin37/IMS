using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Management.Controllers
{
    public class FarmDataLandController : BaseController
    {
        // GET: Management/FarmDataLand
        public ActionResult Edit(Guid? pigFarmId)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmDataLand item = IMSdb.FarmDataLand.Where(m => m.PigFarmId == pigFarmId).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Create", new { pigFarmId = pigFarmId });
            }

            return View(item);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(FarmDataLand farmDataLand)
        {
            if (farmDataLand == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.FarmDataLand.Where(m => m.Id == farmDataLand.Id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.LandNo = farmDataLand.LandNo;
                item.LandNo2 = farmDataLand.LandNo2;
                item.LandNo3 = farmDataLand.LandNo3;
                item.AreaSize = farmDataLand.AreaSize;
                item.LandProperty = farmDataLand.LandProperty;
                item.UseIsCity = farmDataLand.UseIsCity;
                item.UseNonCity = farmDataLand.UseNonCity;
                item.NonCityType = farmDataLand.NonCityType;

                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(farmDataLand);

        }


        public ActionResult Create(Guid pigFarmId)
        {

            FarmDataLand newFarmDataLand = new FarmDataLand();
            newFarmDataLand.PigFarmId = pigFarmId;

            return View("Create", newFarmDataLand);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FarmDataLand farmDataLand)
        {

            farmDataLand.CreDate = DateTime.Now;
            farmDataLand.CreUser = User.ID;
            if (ModelState.IsValid)
            {

                IMSdb.FarmDataLand.Add(farmDataLand);
                IMSdb.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View("Create", farmDataLand);
            }
        }
    }
}