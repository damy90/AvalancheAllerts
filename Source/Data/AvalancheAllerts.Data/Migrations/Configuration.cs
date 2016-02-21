namespace AvalancheAllerts.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using AvalancheAllerts.Common;
    using AvalancheAllerts.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const string AdministratorUserName = "admin@admin.com";
            const string AdministratorPassword = AdministratorUserName;

            if (!context.Roles.Any())
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName };
                userManager.Create(user, AdministratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            }

            context.SaveChanges();

            if (!context.Tests.Any())
            {
                var test1 = new Test()
                {
                    CreatedOn = DateTime.UtcNow,
                    DangerLevel = 2,
                    Elevation = 1700,
                    Latitude = 42.6042826f,
                    Longitude = 23.3882316f,
                    TestResultsDescription = "Q2, CT7",
                    Place = "Витоша Платото",
                    UserId = context.Users.FirstOrDefault().Id
                };

                context.Tests.Add(test1);
                var test2 = new Test()
                {
                    CreatedOn = DateTime.UtcNow,
                    DangerLevel = 5,
                    Elevation = 1700,
                    Latitude = 42.5940509f,
                    Longitude = 23.3201253f,
                    TestResultsDescription = "Q0, CT3",
                    Place = "Витоша Черни Връх",
                    UserId = context.Users.FirstOrDefault().Id
                };
                context.Tests.Add(test2);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
