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
        private readonly IOrganisationsService Organisations;

        public OrganisationsAdminController(IOrganisationsService organisations)
        {
            this.Organisations = organisations;
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
            return this.RedirectToAction("Index", "Organisations");
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
