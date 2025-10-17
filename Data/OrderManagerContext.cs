using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.Data
{
    public class OrderManagerContext : DbContext
    {
        public OrderManagerContext(DbContextOptions<OrderManagerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>().HasKey(c => c.Id);
            base.OnModelCreating(builder);
        }

        public DbSet<Client> Clients { get; set; }
    }
}
