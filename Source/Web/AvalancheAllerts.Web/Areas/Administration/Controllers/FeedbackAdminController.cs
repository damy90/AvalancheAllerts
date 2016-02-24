namespace AvalancheAllerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AvalancheAllerts.Common;
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Controllers;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Feedback;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class FeedbackAdminController : BaseController
    {
        private const int ItemsPerPage = 4;

        private readonly IFeedbackService feedback;

        public FeedbackAdminController(IFeedbackService feedback)
        {
            this.feedback = feedback;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            FeedBackListViewModel viewModel;
            if (this.HttpContext.Cache["Feedback page_" + id] != null)
            {
                viewModel = (FeedBackListViewModel)this.HttpContext.Cache["Feedback page_" + id];
            }
            else
            {
                var page = id;
                var allItemsCount = this.feedback.GetAll().Count();
                var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
                var itemsToSkip = (page - 1) * ItemsPerPage;
                var feedbacks = this.feedback.GetAll()
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.Id)
                    .Skip(itemsToSkip)
                    .Take(ItemsPerPage)
                    .To<FeedbackViewModel>().ToList();

                viewModel = new FeedBackListViewModel()
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    FeedBacks = feedbacks
                };

                this.HttpContext.Cache["Feedback page_" + id] = viewModel;
            }

            return this.View(viewModel);
        }
    }
}
