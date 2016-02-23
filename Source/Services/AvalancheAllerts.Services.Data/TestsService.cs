using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Services.Data
{
    using AvalancheAllerts.Data.Common;
    using AvalancheAllerts.Data.Models;
    using System.Device.Location;

    public class TestsService : ITestsService
    {
        private readonly IDbRepository<Test> tests;

        public TestsService(IDbRepository<Test> tests)
        {
            this.tests = tests;
        }

        public IQueryable<Test> GetAll()
        {
            return this.tests.All();
        }

        public IQueryable<Test> GetAllTimeSpan(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Test> GetByUser(string userName)
        {
            return this.tests.All().Where(t => t.User.Email == userName);
        }

        public IQueryable<Test> GetByOrganisation(string organisationName)
        {
            return this.tests.All().Where(t => t.Organisations.FirstOrDefault(o => o.Name == organisationName) != null);
        }

        public IQueryable<Test> FilterRadius(GeoCoordinate position, int radius)
        {
            //TODO: fix performance
            return this.tests.All()
                .ToList()
                .Where(t => t.Position.GetDistanceTo(position) <= radius)
                .AsQueryable();
        }

        public IQueryable<Test> Filter(DateTime startDate, DateTime endDate, List<string> organisationNames, List<string> userNames)
        {
            throw new NotImplementedException();
        }

        public Test GetById(int id)
        {
            return this.tests.GetById(id);
        }

        public void Create(Test test)
        {
            this.tests.Add(test);
        }

        public void Update(Test test)
        {
            this.tests.Update(test);
        }

        public void Delete(Test test)
        {
            this.tests.Delete(test);
        }

        public void SaveChanges()
        {
            this.tests.Save();
        }
    }
}
