namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class OrganisationSelectListModel : IMapFrom<Organisation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TestsCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Organisation, OrganisationSelectListModel>()
                .ForMember(x => x.TestsCount, opt => opt.MapFrom(x => x.Tests.Count));
        }
    }
}