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

            builder.Entity<Client>()
                .HasMany(c => c.WorkOrders)
                .WithOne(w => w.Client)
                .HasForeignKey(w => w.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkOrder>()
                .HasMany(w => w.Comments)
                .WithOne(c => c.WorkOrder)
                .HasForeignKey(c => c.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
