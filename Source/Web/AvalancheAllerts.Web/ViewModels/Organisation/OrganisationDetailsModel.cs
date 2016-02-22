namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Test;

    public class OrganisationDetailsModel : OrganisationViewModel
    {
        public OrganisationDetailsModel()
        {
            this.Tests = new List<TestViewModel>();
        }

        public ICollection<TestViewModel> Tests { get; set; }

        public override void CreateMappings(IMapperConfiguration configuration)
        {
            base.CreateMappings(configuration);

            configuration.CreateMap<Organisation, OrganisationDetailsModel>()
                .ForMember(
                    x => x.Tests,
                    opt => opt.MapFrom(x => x.Tests.AsQueryable().To<TestViewModel>()));

            //configuration.CreateMap<Organisation, OrganisationViewModel>()
            //.ForMember(x => x.Owner, opt => opt.MapFrom(x => x.Owner.Email));
        }
    }
}