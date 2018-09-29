using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class PigResumeController : Controller
    {
        // GET: PigResume
        public ActionResult Index()
        {
            List<PigResume> list = new List<PigResume>();

            int[] id = { 1, 2, 3 };
            string[] PigFarmID = { "AA", "BB", "CC" };
            string[] Type = { "黑豬", "白豬", "毛豬" };
            string[] Slaughterhouse = { "屏東屠宰場", "長治屠宰場", "左營屠宰場" };
            DateTime[] BirthDate = { DateTime.Now, DateTime.Now, DateTime.Now };

            for(var i = 0;i< id.Length;i++)
            {
                PigResume pigResume = new PigResume
                {
                    ID = id[i],
                    PigFarmID = PigFarmID[i],
                    Type = Type[i],
                    Slaughterhouse = Slaughterhouse[i],
                    BirthDate = BirthDate[i]
                };
                list.Add(pigResume);
            }

            return View(list);
        }
    }
}