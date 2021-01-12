using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CoffeeShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {   
        [RegularExpression(@"^[\w\s]{4, 30}$", ErrorMessage = "The user's first name must contain between 4 and 30 characters!")]
        public string FirstName { get; set; }

        [RegularExpression(@"^\w{4, 10}$", ErrorMessage = "The user's last name must contain between 4 and 30 characters!")]
        public string LastName { get; set; }
        // one-to-many relationship with Order
        [ForeignKey("ClientId")]
        public virtual ICollection<Order> Orders { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CoffeeShop", throwIfV1Schema: false)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderInfo> OrderInfoes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}