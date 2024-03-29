﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using HungVdn1670.Models;


namespace HungVdn1670.Controllers
{
    [Authorize]
	public class CategoriesController : Controller
    {
		private ApplicationDbContext _context;
		public CategoriesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: Categories
		[Authorize(Roles = "staff")]
		public ActionResult Index()
		{
			var categories = _context.Categories.ToList();
			return View(categories);
		}

		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "staff")]
		public ActionResult Create(Category category)
		{
			var isExists = _context.Categories.Any(x => x.Name == category.Name);
			if (isExists)
			{
				ModelState.AddModelError("Name", "Name already exists");
				return View(category);

			}
			if (!ModelState.IsValid)
			{
				return View(category);
			}
			var newCategory = new Category
			{
				Name = category.Name,
				Description = category.Description
			};

			_context.Categories.Add(newCategory);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Details(int? id)
		{
			if (id == null) return HttpNotFound();

			var category = _context.Categories
				.SingleOrDefault(t => t.Id == id);

			if (category == null) return HttpNotFound();

			return View(category);
		}


		[HttpGet]
		[Authorize(Roles = "staff")]
		public ActionResult Edit(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


			var category = _context.Categories
				.SingleOrDefault(t => t.Id == id);

			if (category == null) return HttpNotFound();

			return View(category);
		}

		[HttpPost]
		[Authorize(Roles = "staff")]
		public ActionResult Edit(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View(category);
			}

			var categoryInDb = _context.Categories
				.SingleOrDefault(t => t.Id == category.Id);

			if (categoryInDb == null) return HttpNotFound();
			categoryInDb.Name = category.Name;
			categoryInDb.Description = category.Description;

			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[Authorize(Roles = "staff")]
		public ActionResult Delete(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


			var category = _context.Categories
				.SingleOrDefault(t => t.Id == id);

			if (category == null) return HttpNotFound();

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}