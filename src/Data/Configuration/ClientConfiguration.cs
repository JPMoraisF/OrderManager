using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Name).HasMaxLength(20);
            builder.HasMany(c => c.WorkOrders)
                .WithOne(w => w.Client)
                .HasForeignKey(w => w.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
