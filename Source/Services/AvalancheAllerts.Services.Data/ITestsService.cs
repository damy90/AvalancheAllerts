using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Services.Data
{
    using AvalancheAllerts.Data.Models;

    public interface ITestsService
    {
        IQueryable<Test> GetAll();

        IQueryable<Test> GetAllTimeSpan(DateTime startDate, DateTime endDate);

        IQueryable<Test> GetByUser(string userName);

        IQueryable<Test> GetByOrganisation(string organisationName);

        IQueryable<Test> Filter(
            DateTime startDate,
            DateTime endDate,
            List<string> organisationNames,
            List<string> userNames);

        Test GetById(int id);
    }
}
