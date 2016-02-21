using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheAllerts.Web.Controllers
{
    using System.Device.Location;

    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels;

    public class TestsController : Controller
    {
        private readonly ITestsService Tests;

        public TestsController(ITestsService tests)
        {
            this.Tests = tests;
        }

        // GET: Tests
        public ActionResult GetAll()
        {
            var result = this.Tests.GetAll().Where(t => t.IsDeleted == false).To<TestViewModel>().ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Nearby(double lat, double lon, int radius)
        {
            var currentPosition = new GeoCoordinate(lat, lon);
            var result = this.Tests.FilterRadius(currentPosition, radius * 1000).Where(t => t.IsDeleted == false).To<TestViewModel>().ToList(); //to meters

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}