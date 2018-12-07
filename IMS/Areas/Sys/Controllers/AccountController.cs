using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Comm;

namespace IMS.Areas.Sys.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Sys/Account
        public ActionResult Index(string level)
        {
            if(level == null)
            {
                level = IMSdb.AccountLevel.Where(m => m.Status == "Y").FirstOrDefault().Level;
            }

            AccountVM vm = new AccountVM()
            {
                accountLevels = IMSdb.AccountLevel.Where(m=>m.Status == "Y").ToList(),
                accounts = IMSdb.Account.Where(m => m.Level == level).ToList()
            };


            ViewBag.LevelName = IMSdb.AccountLevel.Where(m => m.Level == level).FirstOrDefault().LevelName + " 層級";

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            //account.Password = Config.AppSetting("defaultPWD");
            account.Status = "Y";
            account.CreDate = DateTime.Now;

            if(IMSdb.Account.Where(m => m.AccountNo == account.AccountNo).Count() >0)
            {
                ViewBag.ErrMsg = "帳號已經有人使用";
                return View(account);
            }

            if (ModelState.IsValid)
            {
                IMSdb.Account.Add(account);
                IMSdb.SaveChanges();
                return RedirectToAction("Index", new { level = account.Level });
            }

            return View(account);
        }

        

        public ActionResult Delete(int? seqNo)
        {
            var account = IMSdb.Account.Find(seqNo);
            if (account == null)
            {
                return RedirectToAction("Index");
            }
            IMSdb.Account.Remove(account);
            IMSdb.SaveChanges();
            return RedirectToAction("Index", new { level = account.Level });
        }
    }
}