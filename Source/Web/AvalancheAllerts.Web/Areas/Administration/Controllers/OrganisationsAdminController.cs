using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AvalancheAllerts.Data;
using AvalancheAllerts.Data.Models;

namespace AvalancheAllerts.Web.Areas.Administration.Controllers
{
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Controllers;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Organisation;

    using Microsoft.AspNet.Identity;

    public class OrganisationsAdminController : BaseController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private readonly IOrganisationsService Organisations;

        public OrganisationsAdminController(IOrganisationsService organisations)
        {
            this.Organisations = organisations;
        }

        // GET: Administration/OrganisationsAdmin
        public ActionResult Index()
        {
            var organisations = this.Organisations.GetAll()
                .Where(x => x.IsDeleted == false)
                .To<OrganisationViewModel>()
                .ToList();

            return View(organisations);
        }

        // GET: Administration/OrganisationsAdmin/Details/5
        public ActionResult Details(int id)
        {
            var organisation = this.Organisations.GetAll()//.ToList().AsQueryable()
                .To<OrganisationDetailsModel>()
                .FirstOrDefault(x => x.Id == id);
            if (organisation == null)
            {
                return this.HttpNotFound();
            }
            return this.View(organisation);
        }

        // GET: Administration/OrganisationsAdmin/Create
        public ActionResult Create()
        {
            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Administration/OrganisationsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OwnerId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] OrganisationCreateModel organisation)
        {
            if (ModelState.IsValid)
            {
                var entity = new Organisation()
                {
                    Name = organisation.Name,
                    Description = organisation.Description,
                    OwnerId = this.User.Identity.GetUserId()
                };

                this.Organisations.Add(entity);
                this.Organisations.SaveChanges();
                return this.RedirectToAction("Index");
            }

            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", organisation.OwnerId);
            return View(organisation);
        }

        // GET: Administration/OrganisationsAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            var organisation = this.Organisations.GetAll().To<OrganisationViewModel>().FirstOrDefault(o => o.Id == id);

            if (organisation == null)
            {
                return HttpNotFound();
            }

            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", organisation.OwnerId);
            return View(organisation);
        }

        // POST: Administration/OrganisationsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OwnerId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] OrganisationEditModel organisation)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Organisations.GetById(organisation.Id);
                entity.Description = organisation.Description;
                entity.Name = organisation.Name;

                this.Organisations.Update(entity);
                this.Organisations.SaveChanges();
                return this.RedirectToAction("Index");
            }

            var result =
                this.Organisations.GetAll().To<OrganisationViewModel>().FirstOrDefault(o => o.Id == organisation.Id);

            return this.View(result);
        }

        // GET: Administration/OrganisationsAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var organisation = this.Organisations.GetAll().To<OrganisationViewModel>().FirstOrDefault(o => o.Id == id);
            if (organisation == null)
            {
                return HttpNotFound();
            }

            return View(organisation);
        }

        // POST: Administration/OrganisationsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organisation organisation = this.Organisations.GetById(id);
            this.Organisations.Delete(organisation);
            this.Organisations.SaveChanges();
            return this.RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
