using IMS.Comm;
using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Traceability.Controllers
{
  public class PigletGrowController : BaseController
  {
    // GET: Traceability/PigletGrow
    public ActionResult Index(string pigFarmId,int? page)
    {

      if (pigFarmId == null)
      {
        if (Request.Cookies["pigFarmId"] == null)
        {
          if (IMSdb.PigFarm.Where(x => x.Status == "Y").Any())
          {
            pigFarmId = IMSdb.PigFarm.Where(x => x.Status == "Y").FirstOrDefault().Id.ToString();
            ViewBag.PigFarmId = pigFarmId;
          }
          else
          {
            TempData["Msg"] = "請先建立養豬場";
            return RedirectToAction("Index", "PigFarm", new { area = "Sys" });
          }
        }
        else
        {
          pigFarmId = Request.Cookies["pigFarmId"].Value;
        }

      }
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
    public ActionResult CreateEditTrace(string traceNo)
    {
      ViewBag.TraceNo = traceNo;

      TraceMaster traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();

      if (traceMaster == null)
      {
        traceMaster = new TraceMaster()
        {
          TraceNo = traceNo,
          PigFarmId = Request.Cookies["pigFarmId"].Value,
          CreDate = DateTime.Now,
          CreUser = User.ID,
          Status = "T",
        };

        AddTempTraceMaster(traceMaster);
      }

      return View(traceMaster);


    }

    [ChildActionOnly]//無法在瀏覽器上用URL存取此action
    public PartialViewResult _TraceDetails(string traceNo)
    {

      var produces = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo
                     orderby c.WorkDate descending
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView(produces.ToList());
    }

    [HttpPost]
    public ActionResult _TraceDetails(TraceDetail traceDetail, string traceNo)
    {
      if (ModelState.IsValid)
      {
        traceDetail.CreDate = DateTime.Now;
        traceDetail.CreUser = User.ID;
        traceDetail.Status = "Y";
        IMSdb.TraceDetail.Add(traceDetail);
        IMSdb.SaveChanges();
      }
      else
      {

        var message = string.Join("", ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage));


        //TempData["Err"] = message;

        return JavaScript("showIntro('" + message + "')");
      }

      var produces = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo 
                     orderby c.WorkDate descending
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView("_TraceDetails", produces.ToList());
    }


    public PartialViewResult _CreateTraceDetail(string traceNo)
    {

      ViewBag.TraceNo = traceNo;
      return PartialView("_CreateTraceDetail");
    }

    #region Method
    public TraceMaster AddTempTraceMaster(TraceMaster traceMaster)
    {
      IMSdb.TraceMaster.Add(traceMaster);
      IMSdb.SaveChanges();
      return traceMaster;
    }
    #endregion
  }
}