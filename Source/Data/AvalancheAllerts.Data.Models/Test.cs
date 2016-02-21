using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Device.Location;

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

        public GeoCoordinate Position { get; set; }

        /*[Required]
        public DateTime Date { get; set; }*/

        [Required]
        public string TestResultsDescription { get; set; }

        [Range(1, 5)]
        public int DangerLevel { get; set; }

        
        public string UserId { get; set; }

        
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
