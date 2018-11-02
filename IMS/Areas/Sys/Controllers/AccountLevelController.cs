using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Sys.Controllers
{
    public class AccountLevelController : BaseController
    {
        // GET: Sys/AccountLevel
        public ActionResult Index()
        {

            return View(IMSdb.AccountLevel.Where(m=>m.Status == "Y").ToList());
        }



        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountLevel accountLevel)
        {
            accountLevel.CreDate = DateTime.Now;
            accountLevel.Status = "Y";
            if (ModelState.IsValid)
            {
                IMSdb.AccountLevel.Add(accountLevel);
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountLevel);
        }

        public ActionResult Delete(int id)
        {
            AccountLevel item = IMSdb.AccountLevel.Where(m => m.Id == id).FirstOrDefault();
            item.Status = "N";
            IMSdb.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountLevel item = IMSdb.AccountLevel.Where(m => m.Id == id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(AccountLevel accountLevel)
        {
            if(accountLevel == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.AccountLevel.Where(m => m.Id == accountLevel.Id).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.Level = accountLevel.Level;
                item.LevelName = accountLevel.LevelName;
                item.ModDate = DateTime.Now;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountLevel);

        }


    }
}