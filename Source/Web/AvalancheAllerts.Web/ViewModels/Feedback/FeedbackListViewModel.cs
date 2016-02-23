namespace AvalancheAllerts.Web.ViewModels.Feedback
{
    using System.Collections.Generic;

    public class FeedBackListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<FeedbackViewModel> FeedBacks { get; set; }
    }
}