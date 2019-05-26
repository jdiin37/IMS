﻿using IMS.Comm;
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

      ViewBag.PigFarmId = pigFarmId;
      var list = getPigletGrows();

      int pageNumber = (page ?? 1);
      return View(list.ToPagedList(pageNumber, Config.PageSize));
    }

    [ChildActionOnly]
    public ActionResult ModalCreateTraceMaster()
    {
      return PartialView();
    }

    public ActionResult RefreshPigCnt(string traceNo)
    {
      int pigChangeCnt = 0;
      if (IMSdb.TraceDetail.Where(m => m.TraceNo == traceNo && m.WorkType == "DoPigChange").Any())
      {
        pigChangeCnt = IMSdb.TraceDetail.Where(m => m.TraceNo == traceNo && m.WorkType == "DoPigChange").Sum(m => m.PigChangeCnt);
      }

      int pigCnt = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).Select(m => m.PigCnt).FirstOrDefault();
      int totalPigCnt = pigCnt + pigChangeCnt;

      if (Request.IsAjaxRequest())
      {
        if (totalPigCnt == 0)
        {
          return Json(0, JsonRequestBehavior.AllowGet);
        }

        return Json(totalPigCnt, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
      }

      return Json(totalPigCnt, JsonRequestBehavior.AllowGet);
    }

    public ActionResult GetPigCnt(DateTime sdate, DateTime edate, string dadType, string momType)
    {
      if (sdate == null)
      {
        sdate = edate;
      }

      if (edate == null)
      {
        edate = sdate;
      }

      sdate = sdate.AddMilliseconds(-1);
      edate = edate.AddDays(1);

      int? pigCnt = (from a in IMSdb.FarrowingRecord
                     join b in IMSdb.PigBasic
                     on a.PigGid equals b.GID
                     where a.FarrowingDate >= sdate && a.FarrowingDate < edate
                     && a.Status == "Y"
                     && a.BoarNo.StartsWith(dadType)
                     && b.PigType.StartsWith(momType)
                     select a.BornCnt).Sum();

      if (Request.IsAjaxRequest())
      {
        if (pigCnt == null)
        {
          return Json(0, JsonRequestBehavior.AllowGet);
        }

        return Json(pigCnt, JsonRequestBehavior.AllowGet);  //將物件序列化JSON並回傳
      }

      return Json(pigCnt, JsonRequestBehavior.AllowGet);
    }



    private IEnumerable<TraceMaster> getPigletGrows()
    {
      //List<PigletGrow> list = new List<PigletGrow> (){
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,01,01),
      //    BornDate_e = new DateTime(2018,01,31),
      //    PigletCnt = 120,
      //    PigType = "L",
      //    SeqNo = 1,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,02,01),
      //    BornDate_e = new DateTime(2018,02,28),
      //    PigletCnt = 114,
      //    PigType = "L",
      //    SeqNo = 4,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,03,01),
      //    BornDate_e = new DateTime(2018,03,31),
      //    PigletCnt = 134,
      //    PigType = "L",
      //    SeqNo = 5,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,04,01),
      //    BornDate_e = new DateTime(2018,04,30),
      //    PigletCnt = 128,
      //    PigType = "L",
      //    SeqNo = 6,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,05,01),
      //    BornDate_e = new DateTime(2018,05,31),
      //    PigletCnt = 94,
      //    PigType = "L",
      //    SeqNo = 7,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,06,01),
      //    BornDate_e = new DateTime(2018,06,30),
      //    PigletCnt = 94,
      //    PigType = "L",
      //    SeqNo = 8,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,07,01),
      //    BornDate_e = new DateTime(2018,07,31),
      //    PigletCnt = 94,
      //    PigType = "L",
      //    SeqNo = 9,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,08,01),
      //    BornDate_e = new DateTime(2018,08,31),
      //    PigletCnt = 94,
      //    PigType = "D",
      //    SeqNo = 10,
      //  },
      //  new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2018,09,01),
      //    BornDate_e = new DateTime(2018,09,30),
      //    PigletCnt = 94,
      //    PigType = "L",
      //    SeqNo = 11,
      //  },
      //   new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2019,02,01),
      //    BornDate_e = new DateTime(2019,02,28),
      //    PigletCnt = 113,
      //    PigType = "D",
      //    SeqNo = 2,

      //  },
      //    new PigletGrow
      //  {
      //    BornDate_s = new DateTime(2019,03,01),
      //    BornDate_e = new DateTime(2019,03,07),
      //    PigletCnt = 81,
      //    PigType = "L",
      //    SeqNo = 3,
      //  },
      //};
      var traceMasters = IMSdb.TraceMaster.Select(z => z).OrderByDescending(m => m.CreDate);
      return traceMasters.ToList();

    }
    
    public ActionResult CreateEditTrace(string traceNo)
    {
      ViewBag.TraceNo = traceNo;
      

      TraceMaster traceMaster = IMSdb.TraceMaster.Where(m => m.TraceNo == traceNo).FirstOrDefault();
      
      return View(traceMaster);


    }

    public FileContentResult GetImage(int seqNo)
    {
      TraceDetail traceDetail = IMSdb.TraceDetail.Find(seqNo);

      if (traceDetail != null)
        return File(traceDetail.PhotoFile, "image/jpeg");
      else
        return null;
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
    public ActionResult _TraceDetails(TraceDetail traceDetail, string traceNo,int pigChangeType, HttpPostedFileBase image)
    {
      if (ModelState.IsValid)
      {
        //有post照片才做照片上傳的處理
        if (image != null)
        {
          traceDetail.PhotoFile = new byte[image.ContentLength];  //取得上傳照片的大小再轉byte陣列
          image.InputStream.Read(traceDetail.PhotoFile, 0, image.ContentLength);
        }
        
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

      if (traceDetail.WorkType.StartsWith("Done")) return JavaScript("location.reload();");
      
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
    public ActionResult _TraceDetailsStage2(TraceDetail traceDetail, string traceNo, HttpPostedFileBase image2)
    {
      if (ModelState.IsValid)
      {
        //有post照片才做照片上傳的處理
        if (image2 != null)
        {
          traceDetail.PhotoFile = new byte[image2.ContentLength];  //取得上傳照片的大小再轉byte陣列
          image2.InputStream.Read(traceDetail.PhotoFile, 0, image2.ContentLength);
        }

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

      if (traceDetail.WorkType.StartsWith("Done")) return JavaScript("location.reload();");

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
    public ActionResult _TraceDetailsStage3(TraceDetail traceDetail, string traceNo, HttpPostedFileBase image3)
    {
      if (ModelState.IsValid)
      {
        //有post照片才做照片上傳的處理
        if (image3 != null)
        {
          traceDetail.PhotoFile = new byte[image3.ContentLength];  //取得上傳照片的大小再轉byte陣列
          image3.InputStream.Read(traceDetail.PhotoFile, 0, image3.ContentLength);
        }

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

      if (traceDetail.WorkType.StartsWith("Done")) return JavaScript("location.reload();");

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
    public ActionResult _TraceDetailsStageLast(TraceDetail traceDetail, string traceNo, HttpPostedFileBase image4)
    {
      if (ModelState.IsValid)
      {
        //有post照片才做照片上傳的處理
        if (image4 != null)
        {
          traceDetail.PhotoFile = new byte[image4.ContentLength];  //取得上傳照片的大小再轉byte陣列
          image4.InputStream.Read(traceDetail.PhotoFile, 0, image4.ContentLength);
        }

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

      if (traceDetail.WorkType.StartsWith("Done")) return JavaScript("location.reload();");

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