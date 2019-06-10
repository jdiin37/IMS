using IMS.Controllers;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
  public class PigBornController : BaseController
  {
    // GET: Traceability/PigBorn
    public ActionResult Index()
    {

      PigBornListVM vm = new PigBornListVM()
      {
        pigBornList = IMSdb.FarrowingRecord
        .Where(m => m.Status == "Y").OrderByDescending(m => m.FarrowingDate).Select(m => m != null ? new PigBorn {
          BornDate = m.FarrowingDate,
          BornCnt = m.BornCnt,
          MomId = m.PigGid,
          BoarNo = m.BoarNo,
        } : null).ToList()
      };


      return View(vm);
    }
  }
}