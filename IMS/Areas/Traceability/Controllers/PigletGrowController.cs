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
      var list = getPigletGrows().OrderByDescending(m=>m.BornDate_s);

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
          PigType = "L",
          SeqNo = 1,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,02,01),
          BornDate_e = new DateTime(2018,02,28),
          PigletCnt = 114,
          PigType = "L",
          SeqNo = 4,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,03,01),
          BornDate_e = new DateTime(2018,03,31),
          PigletCnt = 134,
          PigType = "L",
          SeqNo = 5,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,04,01),
          BornDate_e = new DateTime(2018,04,30),
          PigletCnt = 128,
          PigType = "L",
          SeqNo = 6,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,05,01),
          BornDate_e = new DateTime(2018,05,31),
          PigletCnt = 94,
          PigType = "L",
          SeqNo = 7,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,06,01),
          BornDate_e = new DateTime(2018,06,30),
          PigletCnt = 94,
          PigType = "L",
          SeqNo = 8,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,07,01),
          BornDate_e = new DateTime(2018,07,31),
          PigletCnt = 94,
          PigType = "L",
          SeqNo = 9,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,08,01),
          BornDate_e = new DateTime(2018,08,31),
          PigletCnt = 94,
          PigType = "D",
          SeqNo = 10,
        },
        new PigletGrow
        {
          BornDate_s = new DateTime(2018,09,01),
          BornDate_e = new DateTime(2018,09,30),
          PigletCnt = 94,
          PigType = "L",
          SeqNo = 11,
        },
         new PigletGrow
        {
          BornDate_s = new DateTime(2019,02,01),
          BornDate_e = new DateTime(2019,02,28),
          PigletCnt = 113,
          PigType = "D",
          SeqNo = 2,

        },
          new PigletGrow
        {
          BornDate_s = new DateTime(2019,03,01),
          BornDate_e = new DateTime(2019,03,07),
          PigletCnt = 81,
          PigType = "L",
          SeqNo = 3,
        },
      };
      
      return list.ToList();

    }
    
    public ActionResult CreateEditTrace(string traceNo,int? pigCnt)
    {
      ViewBag.TraceNo = traceNo;

      int pigCnt1 = pigCnt ?? 0;

      TraceMaster traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();

      if (traceMaster == null)
      {
        traceMaster = new TraceMaster()
        {
          TraceNo = traceNo,
          PigCnt = pigCnt1,
          PigFarmId = Request.Cookies["pigFarmId"].Value,
          CreDate = DateTime.Now,
          CreUser = User.ID,
          Status = "T",
        };

       
        TraceDetail traceDetail = new TraceDetail()
        {
          TraceNo = traceNo,
          WorkStage = (int)IMSEnum.WorkStage.Stage1,
          WorkType = "出生",
          WorkContent = "出生",
          CreDate = DateTime.Now,
          CreUser = User.ID,
          WorkUser = User.ID,
          Status = "Y",
          WorkDate = new DateTime(int.Parse(traceNo.Substring(1, 4)), int.Parse(traceNo.Substring(5, 2)), int.Parse(traceNo.Substring(7, 2))),
          WorkDate_end = new DateTime(int.Parse(traceNo.Substring(9, 4)), int.Parse(traceNo.Substring(13, 2)), int.Parse(traceNo.Substring(15, 2))),
        };

        AddTempTraceMaster(traceMaster);
        AddTraceDetail(traceDetail);

      }

      return View(traceMaster);


    }

    #region Stage1
    [ChildActionOnly]//無法在瀏覽器上用URL存取此action
    public PartialViewResult _TraceDetails(string traceNo)
    {

      var traceDetails = from c in IMSdb.TraceDetail
                         where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage1
                         orderby c.WorkDate 
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView(traceDetails.ToList());
    }

    [HttpPost]
    public ActionResult _TraceDetails(TraceDetail traceDetail, string traceNo,int pigChangeType)
    {
      if (ModelState.IsValid)
      {
        traceDetail.WorkStage = (int)IMSEnum.WorkStage.Stage1;
        traceDetail.PigChangeCnt = pigChangeType * traceDetail.PigChangeCnt;

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

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage1
                     orderby c.WorkDate 
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView("_TraceDetails", traceDetails.ToList());
    }

    public PartialViewResult _CreateTraceDetail(string traceNo)
    {

      ViewBag.TraceNo = traceNo;
      return PartialView("_CreateTraceDetail");
    }

    #endregion

    #region Stage2
    [ChildActionOnly]//無法在瀏覽器上用URL存取此action
    public PartialViewResult _TraceDetailsStage2(string traceNo)
    {

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage2
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView(traceDetails.ToList());
    }

    [HttpPost]
    public ActionResult _TraceDetailsStage2(TraceDetail traceDetail, string traceNo)
    {
      if (ModelState.IsValid)
      {
        traceDetail.WorkStage = (int)IMSEnum.WorkStage.Stage2;
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

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage2
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView("_TraceDetailsStage2", traceDetails.ToList());
    }

    public PartialViewResult _CreateTraceDetailStage2(string traceNo)
    {

      ViewBag.TraceNo = traceNo;
      return PartialView("_CreateTraceDetailStage2");
    }
    #endregion

    #region Stage3
    [ChildActionOnly]//無法在瀏覽器上用URL存取此action
    public PartialViewResult _TraceDetailsStage3(string traceNo)
    {

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage3
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView(traceDetails.ToList());
    }

    [HttpPost]
    public ActionResult _TraceDetailsStage3(TraceDetail traceDetail, string traceNo)
    {
      if (ModelState.IsValid)
      {
        traceDetail.WorkStage = (int)IMSEnum.WorkStage.Stage3;
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

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.Stage3
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView("_TraceDetailsStage3", traceDetails.ToList());
    }

    public PartialViewResult _CreateTraceDetailStage3(string traceNo)
    {

      ViewBag.TraceNo = traceNo;
      return PartialView("_CreateTraceDetailStage3");
    }

    #endregion

    #region StageLast
    [ChildActionOnly]//無法在瀏覽器上用URL存取此action
    public PartialViewResult _TraceDetailsStageLast(string traceNo)
    {

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.StageLast
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView(traceDetails.ToList());
    }

    [HttpPost]
    public ActionResult _TraceDetailsStageLast(TraceDetail traceDetail, string traceNo)
    {
      if (ModelState.IsValid)
      {
        traceDetail.WorkStage = (int)IMSEnum.WorkStage.StageLast;
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

      var traceDetails = from c in IMSdb.TraceDetail
                     where c.TraceNo == traceNo && c.WorkStage == (int)IMSEnum.WorkStage.StageLast
                     orderby c.WorkDate
                     select c;

      ViewBag.TraceNo = traceNo;

      return PartialView("_TraceDetailsStageLast", traceDetails.ToList());
    }


    public PartialViewResult _CreateTraceDetailStageLast(string traceNo)
    {

      ViewBag.TraceNo = traceNo;
      return PartialView("_CreateTraceDetailStageLast");
    }


    #endregion


    public ActionResult DeleteTraceDetail(int seqNo)
    {

      TraceDetail traceDetail = IMSdb.TraceDetail.Find(seqNo);

      IMSdb.TraceDetail.Remove(traceDetail);
      IMSdb.SaveChanges();

      return RedirectToAction("CreateEditTrace", new { traceNo = traceDetail.TraceNo });
    }
    

    #region Method
    public TraceMaster AddTempTraceMaster(TraceMaster traceMaster)
    {
      IMSdb.TraceMaster.Add(traceMaster);
      IMSdb.SaveChanges();
      return traceMaster;
    }


    public TraceDetail AddTraceDetail(TraceDetail traceDetail)
    {
      IMSdb.TraceDetail.Add(traceDetail);
      IMSdb.SaveChanges();
      return traceDetail;
    }
    #endregion
  }
}