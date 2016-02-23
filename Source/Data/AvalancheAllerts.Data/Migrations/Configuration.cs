namespace AvalancheAllerts.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Device.Location;
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
                    //avoiding NaN values
                    Latitude = 42.6042826,
                    Longitude = 23.3882316,
                    TestResultsDescription = "Q2, CT7",
                    Place = "Витоша Платото",
                    UserId = context.Users.FirstOrDefault().Id
                };
                
                context.Tests.Add(test1);
                var test2 = new Test()
                {
                    CreatedOn = DateTime.UtcNow,
                    DangerLevel = 5,
                    Latitude = 42.5940509,
                    Longitude = 23.3201253,
                    Altitude = 1900,
                    TestResultsDescription = "Q0, CT3",
                    Place = "Витоша Черни Връх",
                    UserId = context.Users.FirstOrDefault().Id
                };
                context.Tests.Add(test2);

                var test3 = new Test()
                {
                    CreatedOn = DateTime.UtcNow,
                    DangerLevel = 1,
                    Latitude = 42.113862,
                    Longitude = 23.4967623,
                    Altitude = 2900,
                    TestResultsDescription = "Q3, CT13",
                    Place = "Рила: Маркуджик 3",
                    UserId = context.Users.FirstOrDefault().Id
                };
                context.Tests.Add(test3);
            }

            if (!context.Organisations.Any())
            {
                var org = new Organisation()
                {
                    Name = "ПСС",
                    Description = "Планинска спасителна служба",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);

                org = new Organisation()
                {
                    Name = "BASES",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);

                org = new Organisation()
                {
                    Name = "me6tosi",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);

                org = new Organisation()
                {
                    Name = "bla bla",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);

                org = new Organisation()
                {
                    Name = "organisation",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);

                org = new Organisation()
                {
                    Name = "Greanpeace",
                    OwnerId = context.Users.FirstOrDefault().Id
                };

                context.Organisations.Add(org);
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
