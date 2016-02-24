namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.ViewModels.Test;
    using AvalancheAllerts.Web.ViewModels.User;

    public class OrganisationDetailsModel : OrganisationViewModel
    {

        public OrganisationDetailsModel()
        {
            this.Tests = new List<TestViewModel>();
        }

        public IEnumerable<TestViewModel> Tests { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        public override void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Organisation, OrganisationDetailsModel>()
                .ForMember(x => x.Owner, opt => opt.MapFrom(x => x.Owner.Email))
                .ForMember(
                    x => x.Tests,
                    opt => opt.MapFrom(x => x.Tests))
                .ForMember(x => x.Users, opt => opt.MapFrom(x => x.Users));
        }
    }
}