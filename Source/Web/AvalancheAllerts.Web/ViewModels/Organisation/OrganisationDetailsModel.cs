namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.ViewModels.Test;

    public class OrganisationDetailsModel : OrganisationViewModel
    {

        public OrganisationDetailsModel()
        {
            this.Tests = new List<TestViewModel>();
        }

        /*public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Owner { get; set; }*/

        public ICollection<TestViewModel> Tests { get; set; }

        public override void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Organisation, OrganisationDetailsModel>()
                .ForMember(x => x.Owner, opt => opt.MapFrom(x => x.Owner.Email))
                .ForMember(
                    x => x.Tests,
                    opt => opt.MapFrom(x => x.Tests));
        }
    }
}