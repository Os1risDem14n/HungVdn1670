using HungVdn1670.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HungVdn1670.Controllers
{
    [Authorize(Roles = "staff")]
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET: Trainees
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowTrainees()
        {
            var users = _context.Users.ToList();

            var trainee = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("trainee"))
                {
                    trainee.Add(user);
                }
            }

            return View(trainee);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var user = _context.UserDetails
                .SingleOrDefault(t => t.UserId == id);

            if (user == null) return HttpNotFound();

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {


            var UserDetail = _context.UserDetails
                .SingleOrDefault(u => u.UserId.Equals(id));

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


        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //var userId = User.Identity.GetUserId();
            var UserDetail = _context.UserDetails
                .SingleOrDefault(t => t.UserId == id);
            var user = _context.Users
                .SingleOrDefault(t => t.Id == id);

            if (user == null) return HttpNotFound();
            _context.UserDetails.Remove(UserDetail);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}