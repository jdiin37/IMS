using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IMS.Comm;
using IMS.Controllers;

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
                    works = works.OrderBy(s => s.WorkCode);
                    break;
            }

            int pageNumber = (page ?? 1);
            return View(works.ToPagedList(pageNumber, Config.PageSize));
        }
    }
}