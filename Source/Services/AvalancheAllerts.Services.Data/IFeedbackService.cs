namespace AvalancheAllerts.Services.Data
{
    using System.Linq;

    using AvalancheAllerts.Data.Models;

    public interface IFeedbackService
    {
        IQueryable<Feedback> GetAll();

        void Create(Feedback feedback);

        void SaveChanges();
    }
}
