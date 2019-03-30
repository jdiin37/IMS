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
          BornDate_e = new DateTime(2018,01,31),
          PigletCnt = 120,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,02,01),
          BornDate_e = new DateTime(2018,02,28),
          PigletCnt = 114,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,03,01),
          BornDate_e = new DateTime(2018,03,31),
          PigletCnt = 134,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,04,01),
          BornDate_e = new DateTime(2018,04,30),
          PigletCnt = 128,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,05,01),
          BornDate_e = new DateTime(2018,05,31),
          PigletCnt = 94,
        },
      };
      
      return list.ToList();

    }

    //重做一個 成長履歷 不要用之前的
    //public ActionResult CreatePigGrow()
    //{

    //}
  }
}