namespace AvalancheAllerts.Web.ViewModels.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Device.Location;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class TestViewModel : IMapFrom<Test>
    {
        public int Id { get; set; }
        
        public string Place { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Altitude { get; set; }
        
        public int DangerLevel { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}