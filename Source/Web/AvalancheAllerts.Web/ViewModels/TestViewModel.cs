namespace AvalancheAllerts.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Device.Location;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class TestViewModel : IMapFrom<Test>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Place { get; set; }

        public GeoCoordinate Position { get; set; }

        [Required]
        public string TestResultsDescription { get; set; }

        [Range(1, 5)]
        public int DangerLevel { get; set; }

        //TODO:username
    }
}