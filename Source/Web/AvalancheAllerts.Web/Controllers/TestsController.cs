﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheAllerts.Web.Controllers
{
    using System.Device.Location;

    using AutoMapper;

    using AvalancheAllerts.Common;
    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels;
    using AvalancheAllerts.Web.ViewModels.Test;

    using Microsoft.AspNet.Identity;

    public class TestsController : BaseController
    {
        private readonly ITestsService Tests;

        public TestsController(ITestsService tests)
        {
            this.Tests = tests;
        }

        // GET: Tests
        public ActionResult GetAll()
        {
            var result = this.Tests.GetAll().Where(t => t.IsDeleted == false).To<TestViewModel>().ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Nearby(double lat, double lon, int radius)
        {
            var currentPosition = new GeoCoordinate(lat, lon);
            var result = this.Tests.FilterRadius(currentPosition, radius * 1000).Where(t => t.IsDeleted == false).To<TestViewModel>().ToList(); //to meters

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestCreateModel test)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.Mapper.Map<Test>(test);
                entity.UserId = this.User.Identity.GetUserId();
                this.Tests.Create(entity);
                try
                {
                    this.Tests.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            var result = this.Tests.GetAll().Where(x => x.IsDeleted == false).To<TestDetailsModel>().FirstOrDefault(t => t.Id == id);

            if (result == null)
            {
                return this.HttpNotFound();
            }

            return this.View(result);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var test = this.Tests.GetAll().To<TestCreateModel>().FirstOrDefault(o => o.Id == id);

            if (test == null)
            {
                return HttpNotFound();
            }

            if (!(this.User.Identity.GetUserName() == test.Author || this.User.IsInRole(GlobalConstants.AdministratorRoleName)))
            {
                //TODO: unauthorized error
                return this.RedirectToAction("Index", "Home");
            }

            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", organisation.OwnerId);
            return this.View("Edit", test);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OwnerId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] TestCreateModel organisation)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Tests.GetAll().To<Test>().FirstOrDefault(x => x.Id == organisation.Id);
                if (!(this.User.Identity.GetUserName() == entity.User.UserName || this.User.IsInRole(GlobalConstants.AdministratorRoleName)))
                {
                    //TODO: unauthorized error
                    return this.RedirectToAction("Index", "Home");
                }

                this.Tests.Update(entity);
                this.Tests.SaveChanges();;
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}