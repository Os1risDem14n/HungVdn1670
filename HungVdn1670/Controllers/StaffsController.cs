﻿using HungVdn1670.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungVdn1670.ViewModels;
using System.Threading.Tasks;

namespace HungVdn1670.Controllers
{
	[Authorize(Roles = "admin")]
	public class StaffsController : Controller
	{

		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _userManager;
		public StaffsController()
		{
			_context = new ApplicationDbContext();
			_userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}

		// GET: Staffs
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult ShowStaffs()
		{
			AdminChangePasswordViewModel mymodel = new AdminChangePasswordViewModel();
			var users = _context.Users.ToList();

			var staffs = new List<ApplicationUser>();

			foreach (var user in users)
			{
				if (_userManager.GetRoles(user.Id)[0].Equals("staff"))
				{
					staffs.Add(user);
				}
			}
			mymodel.User = staffs;
			return View(mymodel);
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{


			var userInfo = _context.UserDetails
				.SingleOrDefault(u => u.UserId.Equals(id));

			if (userInfo == null) return HttpNotFound();

			return View(userInfo);
		}
		[HttpPost]
		public ActionResult Edit(UserDetail userInfo)
		{
			var userInfoInDb = _context.UserDetails.SingleOrDefault(u => u.UserId.Equals(userInfo.UserId));

			if (userInfo == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			userInfoInDb.FullName = userInfo.FullName;
			userInfoInDb.Age = userInfo.Age;
			userInfoInDb.TOEICScore = userInfo.TOEICScore;
			userInfoInDb.ProgrammingLanguage = userInfo.ProgrammingLanguage;
			_context.SaveChanges();

			return RedirectToAction("ShowStaffs");
		}

		[HttpGet]
		public ActionResult Details(string id)
		{
			var user = _context.UserDetails
				.SingleOrDefault(t => t.UserId == id);

			if (user == null) return HttpNotFound();

			return View(user);
		}

		public ActionResult Delete(string id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			//var userId = User.Identity.GetUserId();
			var userInfo = _context.UserDetails
				.SingleOrDefault(t => t.UserId == id);
			var user = _context.Users
				.SingleOrDefault(t => t.Id == id);

			if (user == null) return HttpNotFound();
			_context.UserDetails.Remove(userInfo);
			_context.Users.Remove(user);
			_context.SaveChanges();

			return RedirectToAction("ShowStaffs");
		}
	}
}