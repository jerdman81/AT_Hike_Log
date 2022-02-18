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

        private DailyLogService CreateDailyLogService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DailyLogService(userId);
            return service;
        }
    }
}