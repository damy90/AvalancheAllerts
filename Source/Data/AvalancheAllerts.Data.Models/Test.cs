using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AvalancheAllerts.Data.Common.Models;

    public class Test : BaseModel<int>
    {
        public Test()
        {
            this.Organisations = new HashSet<Organisation>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Place { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public float? Elevation { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string TestResultsDescription { get; set; }

        [Range(1, 5)]
        public int DangerLevel { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
