namespace AvalancheAllerts.Web.ViewModels.Organisation
{
    using System.ComponentModel.DataAnnotations;

    public class OrganisationCreateModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string Name { get; set; }

        [MinLength(5)]
        [MaxLength(170)]
        public string Description { get; set; }
    }
}