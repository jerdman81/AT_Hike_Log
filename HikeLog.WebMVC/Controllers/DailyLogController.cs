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
        public ActionResult Index(string option, string search)
        {
            var service = CreateDailyLogService();
            if(option == "Miles Hiked")
            {
                var model = service.GetDailyLogByMilesHiked(search);
                return View(model);
            }
            else if (option == "Date")
            {
                var model = service.GetDailyLogByDate(search);
                return View(model);
            }
            else if (option == "Mile Marker")
            {
                var model = service.GetDailyLogByMileMarker(search);
                return View(model);
            }
            else if (option == "Has Notes")
            {
                var model = service.GetDailyLogByHasNotes(search);
                return View(model);
            }
            else if (option == "Note Content")
            {
                var model = service.GetDailyLogByNoteContent(search);
                return View(model);
            }
            else if (option == "Starred")
            {
                var model = service.GetDailyLogByStarStatus(search);
                return View(model);
            }
            else
            {
                var model = service.GetDailyLogs();
                            return View(model);
            }
            
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

        public ActionResult Details(int id)
        {
            var svc = CreateDailyLogService();
            var model = svc.GetDailyLogById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateDailyLogService();
            var detail = service.GetDailyLogById(id);
            var model =
                new DailyLogEdit
                {
                    DailyLogId = detail.DailyLogId,
                    ProfileId = detail.ProfileId,
                    SectionId = detail.SectionId,
                    Date = detail.Date,
                    StartMile = detail.StartMile,
                    EndMile = detail.EndMile,
                    Notes = detail.Notes,
                    IsStarred = detail.IsStarred

                };
            return View(model);
        }

       [HttpPost]
       [ValidateAntiForgeryToken]

       public ActionResult Edit(int id, DailyLogEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if(model.DailyLogId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateDailyLogService();

            if (service.UpdateDailyLog(model))
            {
                TempData["SaveResult"] = "Your DailyLog was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your DailyLog could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateDailyLogService();
            var model = svc.GetDailyLogById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateDailyLogService();
            service.DeleteDailyLog(id);
            TempData["SaveResult"] = "Your DailyLog was deleted";
            return RedirectToAction("Index");
        }


        private DailyLogService CreateDailyLogService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DailyLogService(userId);
            return service;
        }
    }
}