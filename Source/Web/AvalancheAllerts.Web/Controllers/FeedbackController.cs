using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheAllerts.Web.Controllers
{
    using AvalancheAllerts.Data.Models;
    using AvalancheAllerts.Services.Data;
    using AvalancheAllerts.Web.Infrastructure.Mapping;
    using AvalancheAllerts.Web.ViewModels.Feedback;

    using Microsoft.AspNet.Identity;

    public class FeedbackController : BaseController
    {
        private const int ItemsPerPage = 4;

        private readonly IFeedbackService feedback;

        public FeedbackController(IFeedbackService feedback)
        {
            this.feedback = feedback;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedbackInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var feedback = new Feedback()
            {
                Content = model.Content,
                Title = model.Title
            };

            if (this.User.Identity.IsAuthenticated)
            {
                feedback.AuthorId = this.User.Identity.GetUserId();
            }

            this.feedback.Create(feedback);
            this.feedback.SaveChanges();

            this.TempData["Notification"] = "Thank you for your feedback!";
            return this.Redirect("/");
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