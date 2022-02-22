using HikeLog.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HikeLog.WebMVC.Controllers
{
    public class StatisticsController : Controller
    {
        [Authorize]
        // GET: Statistics
        public ActionResult Details(int sectionId)
        {
            var svc = CreateStatisticsService();
            var model = svc.GetStatsForSection(sectionId);
            return View(model);
        }

        private StatisticsService CreateStatisticsService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StatisticsService(userId);
            return service;
        }
    }

    
}