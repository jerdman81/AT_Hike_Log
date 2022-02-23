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
                
        public ActionResult Details(string option, string sectionId)
        {
            int section = Convert.ToInt32(sectionId);
            var svc = CreateStatisticsService();
            if(option == "Section")
            {
                var model = svc.GetStatsForSection(section);
                return View(model);
            }
            return View();
        }

        private StatisticsService CreateStatisticsService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StatisticsService(userId);
            return service;
        }
    }

    
}