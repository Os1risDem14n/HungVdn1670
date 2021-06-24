using System;
using HungVdn1670.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HungVdn1670.ViewModels;
using System.Threading.Tasks;

namespace HungVdn1670.Controllers
{
    [Authorize(Roles = "admin,staff")]
    public class TrainersController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TrainersController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        // GET: Trainers
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowTrainers()
        {
            AdminChangePasswordViewModel mymodel = new AdminChangePasswordViewModel();
            var users = _context.Users.ToList();

            var trainer = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("trainer"))
                {
                    trainer.Add(user);
                }
            }
            mymodel.User = trainer;
            return View(mymodel);
        }
    }
}