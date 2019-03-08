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
    public class HomeController : BaseController
    {
        // GET: Management/Home
        public ActionResult Index()
        {
            //ViewBag.pigFarmId = Request.Cookies["pigFarmId"].Value;
            if(Request.Cookies["pigFarmId"] == null)
            {
                ViewBag.pigFarmId = IMSdb.PigFarm.Select(z=>z.Id).FirstOrDefault().ToString();
            }
            return View();
        }

        public ActionResult EditPigFarmPic(Guid? pigFarmId)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PigFarm item = IMSdb.PigFarm.Where(m => m.Id == pigFarmId).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPigFarmPic(PigFarm pigFarm, HttpPostedFileBase image,string DelFlag)
        {
            if (pigFarm == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.PigFarm.Where(m => m.Id == pigFarm.Id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                //有post照片才做照片上傳的處理
                if (image != null)
                {
                    item.ImageMimeType = image.ContentType;  //抓照片型態
                    item.PhotoFile = new byte[image.ContentLength];  //取得上傳照片的大小再轉byte陣列
                    image.InputStream.Read(item.PhotoFile, 0, image.ContentLength);
                }
                else
                {
                    if(DelFlag != null)
                    {
                        item.ImageMimeType = null;
                        item.PhotoFile = null;
                    }
                }


                item.Name = pigFarm.Name;
                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pigFarm);

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