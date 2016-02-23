namespace AvalancheAllerts.Web.ViewModels.Test
{
    using System.ComponentModel.DataAnnotations;
    using System.Device.Location;

    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    using Microsoft.Ajax.Utilities;

    public class TestCreateModel : IMapTo<Test>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Place { get; set; }

        //public virtual GeoCoordinate Position { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Altitude { get; set; }

        [Required]
        public string TestResultsDescription { get; set; }

        [Range(1, 5)]
        public int DangerLevel { get; set; }
    }
}