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
    }
}