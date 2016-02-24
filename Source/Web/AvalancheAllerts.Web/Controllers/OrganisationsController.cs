using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheAllerts.Web.Controllers
{
    using AvalancheAllerts.Common;
    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Generic;
    using AvalancheAllerts.Web.ViewModels.Organisation;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    public class OrganisationsController : BaseController
    {
        private readonly IOrganisationsService Organisations;

        private ApplicationUserManager userManager;

        public OrganisationsController(IOrganisationsService organisations)
        {
            this.Organisations = organisations;
        }

        public OrganisationsController(IOrganisationsService organisations, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.Organisations = organisations;
            this.UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            ItemsPageModel<OrganisationViewModel> model;
            if (this.HttpContext.Cache["Organisations_" + page] != null)
            {
                model = (ItemsPageModel<OrganisationViewModel>)this.HttpContext.Cache["Organisations_" + page];

            }
            else
            {
                var organisationsCount = this.Organisations.GetAll().Count();
                var totalPages = (int)Math.Ceiling(organisationsCount / (decimal)pageSize);
                var itemsToSkip = (page - 1) * pageSize;
                var organisations = this.Organisations.GetAll()
                    .To<OrganisationViewModel>()
                    .OrderByDescending(x => x.TestsCount)
                    .ThenBy(x => x.Name)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToList();

                model = new ItemsPageModel<OrganisationViewModel>()
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    Items = organisations
                };

                //this.HttpContext.Cache["Feedback page_" + page] = model;
            }


            return View(model);
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

            if (!(this.User.Identity.GetUserName() == organisation.Owner || this.User.IsInRole(GlobalConstants.AdministratorRoleName)))
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

        [HttpPost]
        [Authorize]
        public ActionResult Join(int id)
        {
            var organisation = this.Organisations.GetById(id);
            var memberIds = organisation.Users.Select(u => u.Id);
            if (organisation != null && !memberIds.Contains(this.User.Identity.GetUserId()))
            {
                ApplicationUser me;
                var myId = this.User.Identity.GetUserId();

                /* try
                 {
                      me = this.UserManager.Users
                     .FirstOrDefault(u => u.Id == myId);
                 }
                 catch (Exception ex)
                 {

                     throw ex;
                 }*/


                this.Organisations.Join(organisation.Id, myId);

                try
                {
                    this.Organisations.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return this.RedirectToAction("Details", new { id = organisation.Id });
        }
    }
}