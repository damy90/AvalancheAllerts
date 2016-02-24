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

        private readonly IDbRepository<Organisation> organisations;

        public TestsService(IDbRepository<Test> tests, IDbRepository<Organisation> organisations)
        {
            this.tests = tests;
            this.organisations = organisations;
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
            //TODO: fix performance (remove ToList)
            return this.tests.All()
                .Where(t => t.Latitude != null && t.Longitude != null)
                .ToList()
                .Where(t => this.Distance(position.Latitude, position.Longitude, t.Latitude.Value, t.Longitude.Value) <= radius)
                .AsQueryable();
        }

        private double Distance(double positionLat, double positionLon, double pointLat, double pointLon)
        {
            var position = new GeoCoordinate(positionLat, positionLon);
            var point = new GeoCoordinate(pointLat, pointLon);

            return position.GetDistanceTo(point);
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
            //TODO: add tests to organisation. User is always null. Only user id is set.
            /*this.tests.Save();
            var entity = this.GetAll().FirstOrDefault(t => t.Id == test.Id);
            entity.Organisations = test.User.Organisations;*/
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
