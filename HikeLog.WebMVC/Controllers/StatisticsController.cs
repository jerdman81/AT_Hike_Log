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
        public ActionResult Index()
        {
            return View();
        }
    }

    
}