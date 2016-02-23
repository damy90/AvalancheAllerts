namespace AvalancheAllerts.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    using AvalancheAllerts.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Test> Tests { get; set; }

        public IDbSet<Organisation> Organisations { get; set; }

        public IDbSet<Feedback> Feedbacks { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Organisation>()
                .HasMany(x => x.Users)
                .WithMany(u=>u.Organisations)
                .Map(
                    m =>
                        {
                            m.MapLeftKey("AspNetUsers.Id");
                            m.MapRightKey("Organisation.Id");
                            m.ToTable("UsersOrganisations");
                        });

            base.OnModelCreating(modelBuilder);
        }
        //public System.Data.Entity.DbSet<AvalancheAllerts.Data.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class ModelBuilder
    {
    }
}
