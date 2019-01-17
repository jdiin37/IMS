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
    public class FarmDataLicenseController : BaseController
    {
        // GET: Management/FarmDataLicense
        public ActionResult Edit(Guid? pigFarmId)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmDataLicense item = IMSdb.FarmDataLicense.Where(m => m.PigFarmId == pigFarmId).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Create", new { pigFarmId = pigFarmId });
            }

            return View(item);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(FarmDataLicense farmDataLicense)
        {
            if (farmDataLicense == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.FarmDataLicense.Where(m => m.Id == farmDataLicense.Id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.RunType = farmDataLicense.RunType;
                item.LandAllowFlag = farmDataLicense.LandAllowFlag;
                item.LandAllowNo = farmDataLicense.LandAllowNo;
                item.BuildingAllowFlag = farmDataLicense.BuildingAllowFlag;
                item.BuildingAllowNo = farmDataLicense.BuildingAllowNo;
                item.FarmLicenseFlag = farmDataLicense.FarmLicenseFlag;
                item.FarmLicenseNo = farmDataLicense.FarmLicenseNo;
                item.DrainAllowFlag = farmDataLicense.DrainAllowFlag;
                item.DrainAllowNo = farmDataLicense.DrainAllowNo;
                item.WasteWaterDeviceFlag = farmDataLicense.WasteWaterDeviceFlag;
                item.WasteWaterDeviceNo = farmDataLicense.WasteWaterDeviceNo;
                item.WasteWaterDeviceSize = farmDataLicense.WasteWaterDeviceSize;

                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(farmDataLicense);

        }


        public ActionResult Create(Guid pigFarmId)
        {

            FarmDataLicense newFarmDataLicense = new FarmDataLicense();
            newFarmDataLicense.CreDate = DateTime.Now;
            newFarmDataLicense.CreUser = User.ID;
            newFarmDataLicense.PigFarmId = pigFarmId;

            return View("Create", newFarmDataLicense);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FarmDataLicense farmDataLicense)
        {

            farmDataLicense.CreDate = DateTime.Now;
            farmDataLicense.CreUser = User.ID;
            if (ModelState.IsValid)
            {

                IMSdb.FarmDataLicense.Add(farmDataLicense);
                IMSdb.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View("Create", farmDataLicense);
            }
        }
    }
}