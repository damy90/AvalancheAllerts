using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AvalancheAllerts.Services.Data
{
    using AvalancheAllerts.Data.Common;
    using AvalancheAllerts.Data.Models;

    public class OrganisationsService : IOrganisationsService
    {
        private readonly IDbRepository<Organisation> organisations;

        public OrganisationsService(IDbRepository<Organisation> organisations)
        {
            this.organisations = organisations;
        }

        public IQueryable<Organisation> GetAll()
        {
            return this.organisations.All();
        }

        public IQueryable<Organisation> GetByUser(string userName)
        {
            return this.organisations.All().Where(o => o.Users.FirstOrDefault(u => u.Email == userName) != null);
        }

        public Organisation GetById(int id)
        {
            return this.organisations.GetById(id);
        }
    }
}
