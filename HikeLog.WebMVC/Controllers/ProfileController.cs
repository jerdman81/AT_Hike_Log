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
            if (ModelState.IsValid) return View(model);
            
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

        private ProfileService CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileService(userId);
            return service;
        }

    }


}