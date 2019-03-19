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
    public class PhotoController : BaseController
    {
        // GET: Management/Photo
        public ActionResult Index(Guid? pigFarmId, string searchString ="",int currentPage = 1)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ViewBag.PigFarmId = pigFarmId;
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentPage = currentPage;

            return View();
        }

        public ActionResult Display(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = IMSdb.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View("Display", photo);
        }

        public ActionResult Delete(Guid? id)
        {
            Photo item = IMSdb.Photo.Find(id);
            IMSdb.Photo.Remove(item);
            IMSdb.SaveChanges();
            return RedirectToAction("Index", new { pigFarmId = item.PigFarmId });
        }

        public ActionResult Create(Guid pigFarmId)
        {
            if (pigFarmId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo newPhoto = new Photo();
            newPhoto.CreDate = DateTime.Now;  //新增表單的欄位          
            newPhoto.PigFarmId = pigFarmId;

            return View("Create", newPhoto);
        }
 

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreDate = DateTime.Now;
            photo.CreUser = User.ID;
            if (ModelState.IsValid)
            {
                //有post照片才做照片上傳的處理
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;  //抓照片型態
                    photo.PhotoFile = new byte[image.ContentLength];  //取得上傳照片的大小再轉byte陣列
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }
                else
                {
                    TempData["err"] = "請選擇圖片";
                    return View("Create", photo);
                }
                IMSdb.Photo.Add(photo);
                IMSdb.SaveChanges();
                return RedirectToAction("Index",new { pigFarmId  = photo.PigFarmId});

            }
            else
            {
                return View("Create", photo);
            }
        }



        public FileContentResult GetImage(Guid? id)
        {
            Photo photo = IMSdb.Photo.Find(id);

            if (photo != null)
                return File(photo.PhotoFile, photo.ImageMimeType);
            else
                return null;
        }

        public ActionResult _PhotoGallery(Guid pigFarmId,string searchString ="", int number = 0, int currentPage = 1)
        {
            List<Photo> photos;

            int pageItem = 9;

            int skipNum = (currentPage - 1) * pageItem;


            ViewBag.ItemSize = IMSdb.Photo.Where(m => m.PigFarmId == pigFarmId).Count();
            ViewBag.currentPage = currentPage;
            ViewBag.number = number;
            ViewBag.searchString = searchString;


            if (number == 0) //無指定每頁筆數
            {
                //Lambda
                if (searchString == "")
                {
                    photos = IMSdb.Photo.Where(m => m.PigFarmId == pigFarmId).OrderByDescending(p => p.PostDate).ThenBy(p => p.Title).Skip(skipNum).Take(pageItem).ToList();

                }
                else
                {
                    photos = IMSdb.Photo.Where(m => m.PigFarmId == pigFarmId && (m.Title.Contains(searchString) 
                    || m.Description.Contains(searchString) 
                    || m.PostName.Contains(searchString))).OrderByDescending(p => p.PostDate).ThenBy(p => p.Title).ToList();
                    ViewBag.ItemSize = photos.Count();
                    photos = photos.Skip(skipNum).Take(pageItem).ToList();
                }
                
                //LINQ
                //photos = (from p in context.Photos
                //          orderby p.CreatedDate descending, p.PhotoID ascending
                //          select p).ToList();

                //SQL
                //select * from photo 
                //order by CreatedDate desc, PhotoID
            }
            /////////////////////////////////////////////////////////////////////////////////
            else
            {
                //Lambda
                //photos = context.Photos.OrderByDescending(p => p.CreatedDate).ThenBy(p => p.PhotoID).Take(number).ToList();
                //LINQ
                photos = (from p in IMSdb.Photo
                            where p.PigFarmId == pigFarmId
                            orderby p.PostDate descending, p.Title ascending
                            select p).Take(number).ToList();
                
                //SQL
                //select top 2 * from photo 
                //order by CreatedDate desc, PhotoID

            }
            return PartialView("_PhotoGallery", photos);
        }
    }
}