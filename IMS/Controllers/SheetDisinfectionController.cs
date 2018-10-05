using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SheetDisinfectionController : Controller
    {

        IMSContext Db = new IMSContext();
        // GET: SheetDisinfection
        public ActionResult Index()
        {
            List<SheetDisinfection> list = new List<SheetDisinfection>();
            //list = Db.SheetDisinfection.Select(m=>m).ToList();


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
            m.ModDate = DateTime.Now;

            Db.SheetDisinfection.Add(m);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}