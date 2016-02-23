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
            /*modelBuilder.Entity<Test>().Property(m => m.Position.Latitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Latitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Longitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Altitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Course).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.HorizontalAccuracy).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Speed).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.VerticalAccuracy).IsOptional();*/

            modelBuilder.Configurations.Add(new Test.Testfiguration());
            //modelBuilder.Entity<Test>().Property(m=>m.)
            base.OnModelCreating(modelBuilder);
        }

        private double? EnsureIsNotNaN(double? param)
        {
            if (double.IsNaN((double)param))
            {
                param = null;
            }

            return param;
        }
        //public System.Data.Entity.DbSet<AvalancheAllerts.Data.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class ModelBuilder
    {
    }
}
