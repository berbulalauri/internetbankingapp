using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Transactions)
                .WithOne(t => t.Service)
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(s => s.FixedFee)
                .HasColumnType("decimal(18, 4)");

            builder.Property(s => s.PercentFee)
                .HasColumnType("decimal(18, 4)");
        }
    }
}
