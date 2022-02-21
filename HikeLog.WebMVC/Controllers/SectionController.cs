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
        public ActionResult Index(string option, string search)
        {
            var service = CreateSectionService();
            if (option == "Name")
            {
                var model = service.GetSectionByName(search);
                return View(model);
            }
            else if (option == "Direction")
            {
                var model = service.GetSectionByDirection(search);
                return View(model);
            }
            else if (option == "Date")
            {
                var model = service.GetSectionByDate(search);
                return View(model);
            }
            else if (option == "Mile Marker")
            {
                var model = service.GetSectionByMM(search);
                return View(model);
            }
            else
            {
                var model = service.GetSections();
                return View(model);
            }
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

        public ActionResult Details(int id)
        {
            var svc = CreateSectionService();
            var model = svc.GetSectionById(id);

            return View(model);
        }
                
        public ActionResult Edit(int id)
        {
            var service = CreateSectionService();
            var detail = service.GetSectionById(id);
            var model =
                new SectionEdit
                {
                    SectionId = detail.SectionId,
                    SectionName = detail.SectionName,
                    StartDate = detail.StartDate,
                    EndDate = detail.EndDate,
                    StartMile = detail.StartMile,
                    EndMile = detail.EndMile,
                    Direction = detail.Direction
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SectionEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if(model.SectionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSectionService();

            if (service.UpdateSection(model))
            {
                TempData["SaveResult"] = "Your Section was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Section could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSectionService();
            var model = svc.GetSectionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSectionService();
            service.DeleteSection(id);
            TempData["SaveResult"] = "Your Section was deleted";
            return RedirectToAction("Index");
        }

        private SectionService CreateSectionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SectionService(userId);
            return service;
        }
    }

    
}