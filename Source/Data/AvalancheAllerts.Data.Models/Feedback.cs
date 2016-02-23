namespace AvalancheAllerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AvalancheAllerts.Data.Common.Models;

    public class Feedback : BaseModel<int>
    {
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
