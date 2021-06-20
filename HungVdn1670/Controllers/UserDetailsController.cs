using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungVdn1670.Models;
using Microsoft.AspNet.Identity;

namespace HungVdn1670.Controllers
{
    public class UserDetailsController : Controller
    {
        private ApplicationDbContext _context;
        public UserDetailsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: UserDetails
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var UserDetail = _context.UserDetails.SingleOrDefault(u => u.UserId.Equals(userId));

            if (UserDetail == null) return HttpNotFound();

            return View(UserDetail);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var UserDetail = _context.UserDetails.SingleOrDefault(u => u.UserId.Equals(userId));

            if (UserDetail == null) return HttpNotFound();

            return View(UserDetail);
        }

        [HttpPost]
        public ActionResult Edit(UserDetail UserDetail)
        {
            var UserDetailInDb = _context.UserDetails.SingleOrDefault(u => u.UserId.Equals(UserDetail.UserId));

            if (UserDetail == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserDetailInDb.FullName = UserDetail.FullName;
            UserDetailInDb.Age = UserDetail.Age;
            UserDetailInDb.TOEICScore = UserDetail.TOEICScore;
            UserDetailInDb.ProgrammingLanguage = UserDetail.ProgrammingLanguage;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
