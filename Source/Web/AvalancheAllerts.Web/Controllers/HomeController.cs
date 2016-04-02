namespace AvalancheAllerts.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.ViewModels.Organisation;

    using Infrastructure.Mapping;

    public class HomeController : BaseController
    {
        private readonly IOrganisationsService Organisations;

        public HomeController(IOrganisationsService organisations)
        {
            this.Organisations = organisations;
        }

        public ActionResult Index()
        {
            var organisations = this.Organisations.GetAll()
                .To<OrganisationSelectListModel>()
                .OrderByDescending(o => o.TestsCount)
                .ToList();

            return this.View(organisations);
        }
    }
}
