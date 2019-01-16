using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IMS.Areas.Management.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Management/Home
        public ActionResult Index()
        {
            //ViewBag.pigFarmId = Request.Cookies["pigFarmId"].Value;
            return View();
        }
        
        public ActionResult GetFarmDataBase(Guid pigFarmId)
        {
            FarmDataBase farmDataBase = IMSdb.FarmDataBase.Where(m => m.PigFarmId ==pigFarmId).FirstOrDefault();
            if (Request.IsAjaxRequest())
            {
                if (farmDataBase == null)
                {
                    return Json(farmDataBase, JsonRequestBehavior.AllowGet);
                }
                
                return Json(farmDataBase, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
            }

            return Json(farmDataBase, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFarmDataLand(Guid pigFarmId)
        {
            FarmDataLand farmDataLand = IMSdb.FarmDataLand.Where(m => m.PigFarmId == pigFarmId).FirstOrDefault();
            if (Request.IsAjaxRequest())
            {
                if (farmDataLand == null)
                {
                    return Json(farmDataLand, JsonRequestBehavior.AllowGet);
                }

                return Json(farmDataLand, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
            }

            return Json(farmDataLand, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFarmDataLicense(Guid pigFarmId)
        {
            FarmDataLicense farmDataLicense = IMSdb.FarmDataLicense.Where(m => m.PigFarmId == pigFarmId).FirstOrDefault();
            if (Request.IsAjaxRequest())
            {
                if (farmDataLicense == null)
                {
                    return Json(farmDataLicense, JsonRequestBehavior.AllowGet);
                }

                return Json(farmDataLicense, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
            }

            return Json(farmDataLicense, JsonRequestBehavior.AllowGet);
        }


    }
}