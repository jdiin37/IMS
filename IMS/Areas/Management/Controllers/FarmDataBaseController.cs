using IMS.Controllers;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Areas.Management.Controllers
{
    public class FarmDataBaseController : BaseController
    {
        // GET: Management/FarmDataBase
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult getData(Guid id)
        {
            var data = new
            {
                id = "",
                title = "",
                description = "",
                createdDate = ""
            };

            if (Request.IsAjaxRequest())
            {
                FarmDataBase farmDataBase = IMSdb.FarmDataBase.Find(id);
                if (farmDataBase == null)
                {
                    return Json(data);
                }

                data = new
                {
                    id = farmDataBase.OwnerName,
                    title = farmDataBase.OwnerName,
                    description = farmDataBase.OwnerName,
                    createdDate = farmDataBase.OwnerName
                };

                return Json(data);  //將物件序列化JSON並回傳
            }

            return Json(data);
        }
    }
}