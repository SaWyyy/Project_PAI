using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Projekt_PAI.Model
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        public DbSet<BooksModel> Books { get; set; }

        public DbSet<OrdersModel> Orders { get; set; }

        public DbSet<AddressModel> Address { get; set; }
    }
}
