namespace AvalancheAllerts.Services.Data
{
    using System.Linq;

    using AvalancheAllerts.Data.Common;
    using AvalancheAllerts.Data.Models;

    public class FeedbackService:IFeedbackService
    {
        private readonly IDbRepository<Feedback> feedbackDb;

        public FeedbackService(IDbRepository<Feedback> feedbackDb)
        {
            this.feedbackDb = feedbackDb;
        }

        public IQueryable<Feedback> GetAll()
        {
            return this.feedbackDb.All();
        }

        public void Create(Feedback feedback)
        {
            this.feedbackDb.Add(feedback);
        }

        public void SaveChanges()
        {
            this.feedbackDb.Save();
        }
    }
}
