using HikeLog.Models;
using HikeLog.Models.Profile;
using HikeLog.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HikeLog.WebMVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var service = CreateProfileService();
            var model = service.GetProfiles();
            return View(model);
        }

        // GET : Profile
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var service = CreateProfileService();

            if (service.CreateProflie(model))
            {
                TempData["SaveResult"] = "Your Profile was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Profile could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateProfileService();
            var model = svc.GetProfileById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateProfileService();
            var detail = service.GetProfileById(id);
            var model =
                new ProfileEdit
                {
                    ProfileId = detail.ProfileId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    TrailName = detail.TrailName,
                    Hometown = detail.Hometown
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProfileEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ProfileId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateProfileService();

            if (service.UpdateProfile(model))
            {
                TempData["SaveResult"] = "Your Profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Profile could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateProfileService();
            var model = svc.GetProfileById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateProfileService();
            service.DeleteProfile(id);
            TempData["SaveResult"] = "Your Profile was deleted";
            return RedirectToAction("Index");
        }
        

        private ProfileService CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileService(userId);
            return service;
        }

    }


}