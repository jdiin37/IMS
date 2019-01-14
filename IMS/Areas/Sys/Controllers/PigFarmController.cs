using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMS.Comm;

namespace IMS.Areas.Sys.Controllers
{
    public class PigFarmController : BaseController
    {
        // GET: Sys/PigFarm
        public ActionResult Index(string sortOrder, string searchString, string currentFilter,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var pigFarms = IMSdb.PigFarm.Where(m => m.Status == "Y");


            if (!String.IsNullOrEmpty(searchString))
            {

                pigFarms = pigFarms.Where(s => s.Name.Contains(searchString)
                                       || s.CreUser.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pigFarms = pigFarms.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    pigFarms = pigFarms.OrderBy(s => s.CreDate);
                    break;
                case "date_desc":
                    pigFarms = pigFarms.OrderByDescending(s => s.CreDate);
                    break;
                default:
                    pigFarms = pigFarms.OrderBy(s => s.Name);
                    break;
            }
            
            int pageNumber = (page ?? 1);
            return View(pigFarms.ToPagedList(pageNumber, Config.PageSize));

            
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PigFarm pigFarm, HttpPostedFileBase image)
        {
            pigFarm.CreDate = DateTime.Now;
            pigFarm.CreUser = User.ID;
            if (ModelState.IsValid)
            {
                //有post照片才做照片上傳的處理
                if (image != null)
                {
                    pigFarm.ImageMimeType = image.ContentType;  //抓照片型態
                    pigFarm.PhotoFile = new byte[image.ContentLength];  //取得上傳照片的大小再轉byte陣列
                    image.InputStream.Read(pigFarm.PhotoFile, 0, image.ContentLength);
                }


                IMSdb.PigFarm.Add(pigFarm);
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pigFarm);
        }

        public ActionResult Delete(Guid id)
        {
            PigFarm item = IMSdb.PigFarm.Where(m => m.Id == id).FirstOrDefault();
            item.Status = "N";
            IMSdb.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PigFarm item = IMSdb.PigFarm.Where(m => m.Id == id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(PigFarm pigFarm, HttpPostedFileBase image)
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


                item.Name = pigFarm.Name;
                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pigFarm);

        }

        
        public FileContentResult GetImage(Guid? id)
        {
            PigFarm pigFarm = IMSdb.PigFarm.Find(id);

            if (pigFarm != null)
                return File(pigFarm.PhotoFile, pigFarm.ImageMimeType);
            else
                return null;
        }
    }
}