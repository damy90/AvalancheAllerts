using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AvalancheAllerts.Data.Common.Models;

    public class Organisation : BaseModel<int>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string Name { get; set; }

        public Organisation()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Tests = new HashSet<Test>();
        }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
