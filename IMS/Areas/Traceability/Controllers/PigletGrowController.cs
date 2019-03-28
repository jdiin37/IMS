using IMS.Comm;
using IMS.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
  public class PigletGrowController : Controller
  {
    // GET: Traceability/PigletGrow
    public ActionResult Index(int? page)
    {
      var list = getPigletGrows();

      int pageNumber = (page ?? 1);
      return View(list.ToPagedList(pageNumber, Config.PageSize));
    }

    private IEnumerable<PigletGrow> getPigletGrows()
    {
      List<PigletGrow> list = new List<PigletGrow> (){
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,01,01),
          BornDate_e = new DateTime(2018,01,08),
          PigletCnt = 120,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,01,09),
          BornDate_e = new DateTime(2018,01,16),
          PigletCnt = 114,
        },
      };
      
      return list.ToList();

    }
  }
}