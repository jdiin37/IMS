using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMS.Comm;
using IMS.Controllers;
using IMS.Models;
using System.Net;

namespace IMS.Areas.Traceability.Controllers
{
    public class WorkBasicController : BaseController
    {
        // GET: Traceability/WorkBasic
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodeSortParm = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var works = IMSdb.WorkBasic.Where(m => m.Status == "Y");


            if (!String.IsNullOrEmpty(searchString))
            {

                works = works.Where(s => s.WorkCode.Contains(searchString)
                                       || s.WorkType.Contains(searchString) || s.WorkContent.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    works = works.OrderByDescending(s => s.WorkCode);
                    break;
                case "date":
                    works = works.OrderBy(s => s.CreDate);
                    break;
                case "date_desc":
                    works = works.OrderByDescending(s => s.CreDate);
                    break;
                default:
                    works = works.OrderBy(s => s.WorkType).ThenBy(s => s.WorkCode);
                    break;
            }

            int pageNumber = (page ?? 1);
            return View(works.ToPagedList(pageNumber, Config.PageSize));
        }

        public ActionResult CreateProduce()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduce(WorkBasic work)
        {
            work.CreDate = DateTime.Now;
            work.CreUser = User.ID;
            work.WorkClass = "produce";
            work.Status = "Y";

            if (IMSdb.WorkBasic.Where(m => m.WorkCode == work.WorkCode).Count() > 0)
            {
                ViewBag.ErrMsg = "編號重複";
                return View(work);
            }

            if (ModelState.IsValid)
            {
                
                IMSdb.WorkBasic.Add(work);
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(work);
        }


        public ActionResult Edit(int? seqNo)
        {
            if (seqNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkBasic item = IMSdb.WorkBasic.Where(m => m.SeqNo == seqNo).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            if(item.WorkClass == "produce")
            {
                return RedirectToAction("EditProduce",new { seqNo = seqNo });
            }
            else if(item.WorkClass == "process")
            {
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }



        public ActionResult EditProduce(int? seqNo)
        {
            if (seqNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkBasic item = IMSdb.WorkBasic.Where(m => m.SeqNo == seqNo).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduce(WorkBasic work)
        {
            if (work == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.WorkBasic.Where(m => m.SeqNo == work.SeqNo).FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                
                //item.WorkCode = work.WorkCode;
                item.WorkContent = work.WorkContent;
                item.WorkType = work.WorkType;
                item.ModDate = DateTime.Now;
                item.ModUser = User.ID;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(work);

        }

        public ActionResult Delete(int? seqNo)
        {
            WorkBasic item = IMSdb.WorkBasic.Find(seqNo);
            if(item == null)
            {
                return RedirectToAction("Index");
            }
            IMSdb.WorkBasic.Remove(item);
            IMSdb.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public ActionResult CreateProcess()
        {

            return View();
        }
    }
}