using HikeLog.Models;
using HikeLog.Models.Section;
using HikeLog.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HikeLog.WebMVC.Controllers
{
    [Authorize]
    public class SectionController : Controller
    {
        // GET: Section
        public ActionResult Index()
        {
            var service = CreateSectionService();
            var model = service.GetSections();
            return View(model);
        }

        // GET : Section
        public ActionResult Create()
        {
            ViewBag.Title = "New Section";
            return View();
        }

        // POST : Section
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var service = CreateSectionService();

            if (service.CreateSection(model))
            {
                TempData["SaveResult"] = "Your Section was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Section could not be created.");
            return View(model);
        }

        private SectionService CreateSectionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SectionService(userId);
            return service;
        }
    }

    
}