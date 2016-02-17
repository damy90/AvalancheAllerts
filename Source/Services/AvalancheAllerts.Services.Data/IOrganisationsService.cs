using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Services.Data
{
    using AvalancheAllerts.Data.Models;

    public interface IOrganisationsService
    {
        IQueryable<Organisation> GetAll();

        IQueryable<Organisation> GetByUser(string userName);

        Organisation GetById(int id);
    }
}
