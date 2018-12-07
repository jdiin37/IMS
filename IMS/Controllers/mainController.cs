﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class MainController : BaseController
    {
    
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Logout()
        {
            return RedirectToAction("Index","Login");
        }


        public ActionResult ShowPaper()
        {
            return View();
        }





        [ChildActionOnly]
        public ActionResult NavMenu()
        {
            return View();
        }

        [ChildActionOnly]
        [ValueReport(IsCheck = false)]
        public ActionResult BoxMenu()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [ValueReport(IsCheck =false)]
        public ActionResult UserInfo()
        {
            return PartialView();
        }


    }
}