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
    public class FarmDataBaseController : BaseController
    {
        // GET: Management/FarmDataBase
        public ActionResult Edit(Guid? pigFarmId)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmDataBase item = IMSdb.FarmDataBase.Where(m => m.PigFarmId == pigFarmId).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Create",new { pigFarmId = pigFarmId});
            }

            return View(item);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(FarmDataBase farmDataBase)
        {
            if (farmDataBase == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.FarmDataBase.Where(m => m.Id == farmDataBase.Id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.OwnerName = farmDataBase.OwnerName;
                item.OwnerBirth = farmDataBase.OwnerBirth;
                item.OwnerAddress = farmDataBase.OwnerAddress;
                item.OwnerPhone = farmDataBase.OwnerPhone;
                item.OwnerFax = farmDataBase.OwnerFax;
                item.OwnerEducation = farmDataBase.OwnerEducation;
                item.FarmName = farmDataBase.FarmName;
                item.FarmAddress = farmDataBase.FarmAddress;
                item.FarmPhone = farmDataBase.FarmPhone;
                item.FarmFax = farmDataBase.FarmFax;

                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(farmDataBase);

        }


        public ActionResult Create(Guid pigFarmId)
        {

            FarmDataBase newFarmDataBase = new FarmDataBase();
            newFarmDataBase.CreDate = DateTime.Now; 
            newFarmDataBase.CreUser = User.ID;
            newFarmDataBase.PigFarmId = pigFarmId;

            return View("Create", newFarmDataBase);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FarmDataBase farmDataBase)
        {

            farmDataBase.CreDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                IMSdb.FarmDataBase.Add(farmDataBase);
                IMSdb.SaveChanges();
                return RedirectToAction("Index","Home");

            }
            else
            {
                return View("Create", farmDataBase);
            }
        }
      
    }
}