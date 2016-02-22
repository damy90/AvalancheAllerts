namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class OrganisationViewModel : OrganisationEditModel, IMapFrom<Organisation>, IHaveCustomMappings
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Owner { get; set; }

        public virtual void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Organisation, OrganisationViewModel>()
                .ForMember(x => x.Owner, opt => opt.MapFrom(x => x.Owner.Email));
        }
    }
}