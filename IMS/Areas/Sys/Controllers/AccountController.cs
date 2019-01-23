using IMS.Controllers;
using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Comm;
using System.Net;

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
                accountLevels = IMSdb.AccountLevel.Where(m=>m.Status == "Y").OrderBy(m=>m.LevelName).ToList(),
                accounts = IMSdb.Account.Where(m => m.Level == level).OrderBy(m => m.AccountNo).ToList()
            };

            ViewBag.Level = level;
            ViewBag.LevelName = IMSdb.AccountLevel.Where(m => m.Level == level).FirstOrDefault().LevelName;

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

        public ActionResult Edit(int? seqNo)
        {
            if (seqNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = IMSdb.Account.Find(seqNo);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (account == null)
            {
                return HttpNotFound();
            }


            var item = IMSdb.Account.Find(account.SeqNo);

            if (item == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                item.AccountName = account.AccountName;
                item.Email = account.Email;
                item.ModUser = User.ID;
                item.ModDate = DateTime.Now;
                IMSdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);

        }
    }
}