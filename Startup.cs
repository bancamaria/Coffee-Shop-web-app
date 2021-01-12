using CoffeeShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(CoffeeShop.Startup))]
namespace CoffeeShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminAndUserRoles();
        }

        private void CreateAdminAndUserRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>
                (
                    new RoleStore<IdentityRole>(ctx)
                );
            var userManager = new UserManager<ApplicationUser>
                (
                    new UserStore<ApplicationUser>(ctx)
                );

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };

                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };

                var adminCreated = userManager.Create(user, "Admin2020!");

                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }

            if (!roleManager.RoleExists("Supplier"))
            {
                var role = new IdentityRole
                {
                    Name = "Supplier"
                };

                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "supplier@supply.com",
                    Email = "supplier@supply.com"
                };

                var supplierCreated = userManager.Create(user, "Supplier2020!");

                if (supplierCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Supplier");
                }
            }

            if (!roleManager.RoleExists("Client"))
            {
                var role = new IdentityRole
                {
                    Name = "Client"
                };

                roleManager.Create(role);
            }
        }
    }
}
