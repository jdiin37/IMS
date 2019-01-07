using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Sys.Controllers
{
    public class CategorySubController : BaseController
    {
        // GET: Sys/CategorySub
        public ActionResult Index(string categoryId)
        {

            if (categoryId == null)
            {
                categoryId = IMSdb.Category.FirstOrDefault().CategoryID;
            }

            CategoryVM vm = new CategoryVM()
            {
                categoryLists = IMSdb.Category.ToList(),
                categorySubs = IMSdb.CategorySub.Where(m=>m.CategoryID == categoryId).ToList()
            };

            ViewBag.CategoryId = categoryId;

            ViewBag.CategoryListName = IMSdb.Category.Where(m => m.CategoryID == categoryId).FirstOrDefault().CategoryName + " 類別";

            return View(vm);
        }


        public ActionResult Create(string categoryId)
        {
            if(categoryId == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryListName = IMSdb.Category.Where(m => m.CategoryID == categoryId).FirstOrDefault().CategoryName ;

            CategorySub newCategorySub = new CategorySub();
            newCategorySub.CategoryID = categoryId;

            return View("Create", newCategorySub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategorySub categorySub)
        {
            categorySub.CreDate = DateTime.Now;
            categorySub.CreUser = User.ID;
            
            if (ModelState.IsValid)
            {
                IMSdb.CategorySub.Add(categorySub);
                IMSdb.SaveChanges();
                return RedirectToAction("Index", new { categoryId = categorySub.CategoryID });
            }

            return View(categorySub);
        }



        public ActionResult Delete(int? SubId)
        {
            var categorySub = IMSdb.CategorySub.Find(SubId);
            if (categorySub == null)
            {
                return RedirectToAction("Index");
            }
            IMSdb.CategorySub.Remove(categorySub);
            IMSdb.SaveChanges();
            return RedirectToAction("Index", new { categoryId = categorySub.CategoryID });
        }

    }
}