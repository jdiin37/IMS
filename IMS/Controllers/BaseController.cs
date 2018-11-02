using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class BaseController : Controller
    {
        protected IMSContext IMSdb;

        public BaseController()
            : base()
        {
            IMSdb = new IMSContext();
        }

       
        public Account CurrentUser
        {
            get
            {
                return IMSdb.Account.First(m => m.AccountNo == "");
            }
        }
        

        #region File Service
        protected ActionResult ImageNotFound()
        {
            return File(Server.MapPath("~/Image/no_image_found.jpg"), "image/jpg");
        }
        #endregion


    }
}