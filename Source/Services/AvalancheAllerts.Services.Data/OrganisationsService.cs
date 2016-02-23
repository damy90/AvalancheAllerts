using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AvalancheAllerts.Services.Data
{
    using AvalancheAllerts.Data;
    using AvalancheAllerts.Data.Common;
    using AvalancheAllerts.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class OrganisationsService : IOrganisationsService
    {
        private readonly IDbRepository<Organisation> organisations;

        //private readonly ApplicationDbContext users;

        private readonly IDbGenericRepository<ApplicationUser, string> users; 

        //private readonly UserStore userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());

        public OrganisationsService(IDbRepository<Organisation> organisations, IDbGenericRepository<ApplicationUser, string> users)
        {
            this.organisations = organisations;
            this.users = users;
        }

        public IQueryable<Organisation> GetAll()
        {
            return this.organisations.All();
        }

        public IQueryable<Organisation> GetByUser(string userName)
        {
            return this.organisations.All(); //.Where(o => o.Users.FirstOrDefault(u => u.Email == userName) != null);
        }

        public Organisation GetById(int id)
        {
            return this.organisations.GetById(id);
        }

        public void Add(Organisation organisation)
        {
            this.organisations.Add(organisation);
        }

        public void SaveChanges()
        {
            this.organisations.Save();
        }

        public void Update(Organisation organisation)
        {
            this.organisations.Update(organisation);
        }

        public void Delete(Organisation organisation)
        {
            this.organisations.Delete(organisation);
        }

        public void Join(int organisationId, string userId)
        {
            var user = this.users.GetById(userId);
            var entity = this.organisations.GetById(organisationId);
            entity.Users.Add(user);
        }
    }
}
