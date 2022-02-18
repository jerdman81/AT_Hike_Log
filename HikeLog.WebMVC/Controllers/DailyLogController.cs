using HikeLog.Models.DailyLog;
using HikeLog.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HikeLog.WebMVC.Controllers
{
    public class DailyLogController : Controller
    {
        [Authorize]

        // GET: DailyLog
        public ActionResult Index()
        {
            var service = CreateDailyLogService();
            var model = service.GetDailyLogs();
            return View(model);
        }

        // GET : DailyLog
        public ActionResult Create()
        {
            ViewBag.Title = "New DailyLog";
            return View();
        }

        // POST : DailyLog
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DailyLogCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDailyLogService();

            if (service.CreateDailyLog(model))
            {
                TempData["SaveResult"] = "Your DailyLog was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "DailyLog could not be created.");
            return View(model);
        }


        private DailyLogService CreateDailyLogService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DailyLogService(userId);
            return service;
        }
    }
}