using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Sys.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Sys/Category
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;


            var categorys = IMSdb.Category.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {

                categorys = categorys.Where(s => s.CategoryID.Contains(searchString)
                                       || s.CategoryName.Contains(searchString));
            }


            return View(categorys.ToList());
        }


        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            category.CreDate = DateTime.Now;
            category.CreUser = User.ID;
            if (ModelState.IsValid)
            {
                if(IMSdb.Category.Find(category.CategoryID) != null)
                {
                    ViewBag.Error = "類別編號重複";
                    return View(category);
                }
                IMSdb.Category.Add(category);
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Delete(string id)
        {
            Category item = IMSdb.Category.Where(m => m.CategoryID == id).FirstOrDefault();
            IMSdb.Category.Remove(item);
            IMSdb.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category item = IMSdb.Category.Where(m => m.CategoryID == id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.Category.Where(m => m.CategoryID == category.CategoryID).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.CategoryName = category.CategoryName;
                item.ModUser = User.ID;
                item.ModDate = DateTime.Now;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);

        }


    }
}