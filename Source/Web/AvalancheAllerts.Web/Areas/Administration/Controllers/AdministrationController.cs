namespace AvalancheAllerts.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using AvalancheAllerts.Common;
    using AvalancheAllerts.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
