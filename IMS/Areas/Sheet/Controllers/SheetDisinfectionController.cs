using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Sheet.Controllers
{
    public class SheetDisinfectionController : Controller
    {

        IMSContext Db = new IMSContext();
        // GET: SheetDisinfection
        public ActionResult Index()
        {
            List<SheetDisinfection> list = new List<SheetDisinfection>();
            list = Db.SheetDisinfection.Select(m=>m).ToList();


            return View(list);
        }
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SheetDisinfection m)
        {           
            m.CreDate = DateTime.Now;

            Db.SheetDisinfection.Add(m);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            SheetDisinfection item = Db.SheetDisinfection.Where(m => m.ID == Id).FirstOrDefault();
            Db.SheetDisinfection.Remove(item);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            SheetDisinfection item = Db.SheetDisinfection.Where(m => m.ID == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(SheetDisinfection sheetDisinfection)
        {
            var item = Db.SheetDisinfection.Where(m => m.ID == sheetDisinfection.ID).FirstOrDefault();
            item.ExcuteDate = sheetDisinfection.ExcuteDate;
            item.Building = sheetDisinfection.Building;
            item.UseType = sheetDisinfection.UseType;
            item.UseDose = sheetDisinfection.UseDose;
            item.Note = sheetDisinfection.Note;
            item.ModDate = DateTime.Now;
            
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}