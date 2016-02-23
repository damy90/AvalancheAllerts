namespace AvalancheAllerts.Web.ViewModels.Test
{
    using AutoMapper;

    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Web.Infrastructure.Mapping;

    public class TestDetailsModel : TestViewModel, IHaveCustomMappings
    {
        public string TestResultsDescription { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Test, TestDetailsModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}