﻿using System;
using HungVdn1670.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var users = _context.Users.ToList();

            var trainer = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("trainer"))
                {
                    trainer.Add(user);
                }
            }

            return View(trainer);
        }
    }
}