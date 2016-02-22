using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheAllerts.Web.Controllers
{
    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Organisation;

    using Microsoft.AspNet.Identity;

    public class OrganisationsController : BaseController
    {
        private readonly IOrganisationsService Organisations;

        public OrganisationsController(IOrganisationsService organisations)
        {
            this.Organisations = organisations;
        }
        
        public ActionResult Index()
        {
            var organisations = this.Organisations.GetAll()
                .Where(x => x.IsDeleted == false)
                .To<OrganisationViewModel>()
                .ToList();

            return View(organisations);
        }
        
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

        // GET: OrganisationsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }
        
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
            
            return View(organisation);
        }
        
        [Authorize]
        public ActionResult Edit(int id)
        {
            var organisation = this.Organisations.GetAll().To<OrganisationViewModel>().FirstOrDefault(o => o.Id == id);

            if (organisation == null)
            {
                return HttpNotFound();
            }

            if (User.Identity.GetUserName() != organisation.Owner)
            {
                //TODO: unauthorized error
                return this.RedirectToAction("Index", "Home");
            }

            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", organisation.OwnerId);
            return View(organisation);
        }

        // POST: Administration/OrganisationsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OwnerId,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] OrganisationEditModel organisation)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Organisations.GetById(organisation.Id);
                if (User.Identity.GetUserName() != entity.Owner.UserName)
                {
                    //TODO: unauthorized error
                    return this.RedirectToAction("Index", "Home");
                }

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
    }
}