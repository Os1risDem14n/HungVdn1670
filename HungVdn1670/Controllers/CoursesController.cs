﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HungVdn1670.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HungVdn1670.ViewModels;
using System.Diagnostics;
using System.Net;
using Microsoft.Ajax.Utilities;

namespace HungVdn1670.Controllers
{
    [Authorize]
	public class CoursesController : Controller
    {
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _userManager;

		public CoursesController()
		{
			_context = new ApplicationDbContext();
			_userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}

		// GET: Courses
		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Index(string searchString)
		{
			var courses = _context.Courses.ToList();
			if (!searchString.IsNullOrWhiteSpace())
			{
				courses = courses.Where(t => t.Name.Contains(searchString)).ToList();
			}
			return View(courses);
		}


		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Create()
		{
			var viewModel = new CourseCategoriesViewModel()
			{
				Categories = _context.Categories.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "staff")]
		public ActionResult Create(Course course)
		{
			var isExists = _context.Courses.Any(x => x.Name == course.Name);
			if (isExists)
			{
				var viewModel = new CourseCategoriesViewModel()
				{
					Categories = _context.Categories.ToList()
				};
				ModelState.AddModelError("Name", "Name already exists");
				return View(viewModel);

			}
			if (!ModelState.IsValid)
			{
				var viewModel = new CourseCategoriesViewModel()
				{
					Course = course,
					Categories = _context.Categories.ToList()
				};
				return View(viewModel);
			}	
			var newCourse = new Course
			{
				Name = course.Name,
				CategoryId = course.CategoryId,
				StartDate = course.StartDate,
				Description = course.Description
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpGet]
		public ActionResult Details(int? id)
		{
			if (id == null) return HttpNotFound();

			var course = _context.Courses
				.Include(t => t.Category)
				.SingleOrDefault(t => t.Id == id);

			if (course == null) return HttpNotFound();

			return View(course);
		}


		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			//var userId = User.Identity.GetUserId();

			var course = _context.Courses
				//.Where(t => t.UserId.Equals(userId))
				.SingleOrDefault(t => t.Id == id);

			if (course == null) return HttpNotFound();

			var viewModel = new CourseCategoriesViewModel()
			{
				Course = course,
				Categories = _context.Categories.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CourseCategoriesViewModel()
				{
					Course = course,
					Categories = _context.Categories.ToList()
				};
				return View(viewModel);
			}

			//var userId = User.Identity.GetUserId();

			var courseInDb = _context.Courses
				//.Where(t => t.UserId.Equals(userId))
				.SingleOrDefault(t => t.Id == course.Id);

			if (courseInDb == null) return HttpNotFound();
			courseInDb.Name = courseInDb.Name;
			courseInDb.Description = courseInDb.Description;
			courseInDb.StartDate = courseInDb.StartDate;
			courseInDb.CategoryId = courseInDb.CategoryId;

			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			//var userId = User.Identity.GetUserId();

			var course = _context.Courses
				//.Where(t => t.UserId.Equals(userId))
				.SingleOrDefault(t => t.Id == id);

			if (course == null) return HttpNotFound();

			_context.Courses.Remove(course);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult ShowTrainees(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
			var members = _context.UserInCourses
				.Include(t => t.User)
				.Where(t => t.CourseId == id)
				.Select(t => t.User);
			var trainee = new List<ApplicationUser>();       // Init List Users to Add Course

			foreach (var user in members)
			{
				if (_userManager.GetRoles(user.Id)[0].Equals("trainee"))
				{
					trainee.Add(user);
				}
			}
			ViewBag.CourseId = id;
			return View(trainee);
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult AddTrainees(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

			if (_context.Courses.SingleOrDefault(t => t.Id == id) == null)
				return HttpNotFound();

			var usersInDb = _context.Users.ToList();      // User trong Db

			var usersInTeam = _context.UserInCourses         // User trong Team
				.Include(t => t.User)
				.Where(t => t.CourseId == id)
				.Select(t => t.User)
				.ToList();

			var usersToAdd = new List<ApplicationUser>();       // Init List Users to Add Team

			foreach (var user in usersInDb)
			{
				if (!usersInTeam.Contains(user) &&
					_userManager.GetRoles(user.Id)[0].Equals("trainee"))
				{
					usersToAdd.Add(user);
				}
			}

			var viewModel = new UserInCoursesViewModel
			{
				CourseId = (int)id,
				Users = usersToAdd
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "staff")]
		public ActionResult AddTrainees(UserInCourse model)
		{
			var UserInCourse = new UserInCourse
			{
				CourseId = model.CourseId,
				UserId = model.UserId
			};

			_context.UserInCourses.Add(UserInCourse);
			_context.SaveChanges();

			return RedirectToAction("ShowTrainees", new { id = model.CourseId });
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult ShowTrainers(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
			var members = _context.UserInCourses
				.Include(t => t.User)
				.Where(t => t.CourseId == id)
				.Select(t => t.User);
			var trainer = new List<ApplicationUser>();       // Init List Users to Add Course

			foreach (var user in members)
			{
				if (_userManager.GetRoles(user.Id)[0].Equals("trainer"))
				{
					trainer.Add(user);
				}
			}
			ViewBag.CourseId = id;
			return View(trainer);
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult AddTrainers(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

			if (_context.Courses.SingleOrDefault(t => t.Id == id) == null)
				return HttpNotFound();

			var usersInDb = _context.Users.ToList();      // User trong Db

			var usersInTeam = _context.UserInCourses         // User trong Team
				.Include(t => t.User)
				.Where(t => t.CourseId == id)
				.Select(t => t.User)
				.ToList();

			var usersToAdd = new List<ApplicationUser>();       // Init List Users to Add Team

			foreach (var user in usersInDb)
			{
				if (!usersInTeam.Contains(user) &&
					_userManager.GetRoles(user.Id)[0].Equals("trainer"))
				{
					usersToAdd.Add(user);
				}
			}

			var viewModel = new UserInCoursesViewModel
			{
				CourseId = (int)id,
				Users = usersToAdd
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "staff")]
		public ActionResult AddTrainers(UserInCourse model)
		{
			var UserInCourse = new UserInCourse
			{
				CourseId = model.CourseId,
				UserId = model.UserId
			};

			_context.UserInCourses.Add(UserInCourse);
			_context.SaveChanges();

			return RedirectToAction("ShowTrainers", new { id = model.CourseId });
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult RemoveTrainees(int id, string userId)
		{
			var UserInCourseToRemove = _context.UserInCourses
				.SingleOrDefault(t => t.CourseId == id && t.UserId == userId);

			if (UserInCourseToRemove == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

			_context.UserInCourses.Remove(UserInCourseToRemove);
			_context.SaveChanges();

			return RedirectToAction("ShowTrainees", new { id = id });
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult RemoveTrainers(int id, string userId)
		{
			var UserInCourseToRemove = _context.UserInCourses
				.SingleOrDefault(t => t.CourseId == id && t.UserId == userId);

			if (UserInCourseToRemove == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

			_context.UserInCourses.Remove(UserInCourseToRemove);
			_context.SaveChanges();
			return RedirectToAction("ShowTrainers", new { id = id });
		}


		[HttpGet]
		[Authorize(Roles = "trainer, trainee")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var courses = _context.UserInCourses
				.Where(t => t.UserId.Equals(userId))
				.Include(t => t.Course)
				.Select(t => t.Course)
				.ToList();

			return View(courses);
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Report()
		{
			var viewModel = new List<SatisticalReportViewModel>();

			//var userId = User.Identity.GetUserId();


			var coursesInDb = _context.Courses
				.Include(t => t.Category)
				.ToList();

			var coursesGroupByName = coursesInDb.GroupBy(t => t.Category.Name).ToList();

			foreach (var categoryGroup in coursesGroupByName)
			{
				var categoryQuantity = categoryGroup.Select(c => c.Category).Count();
				viewModel.Add(new SatisticalReportViewModel()
				{
					CategoryName = categoryGroup.Key,
					CategoryQuantity = categoryQuantity
				});
			}

			return View(viewModel);
		}
	}
}