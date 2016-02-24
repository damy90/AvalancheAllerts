namespace AvalancheAllerts.Web.ViewModels.User
{
    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public int Tests { get; set; }

        public int Organisations { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(x => x.Organisations, opt => opt.MapFrom(x => x.Organisations.Count))
                .ForMember(x => x.Tests, opt => opt.MapFrom(x => x.Tests.Count));
        }
    }
}