namespace AvalancheAllerts.Web.ViewModels.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Device.Location;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class TestViewModel : TestCreateModel, IMapFrom<Test>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        //TODO:username
    }
}