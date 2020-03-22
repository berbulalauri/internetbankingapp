using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class DepositTypeConfiguration : IEntityTypeConfiguration<DepositType>
    {
        public void Configure(EntityTypeBuilder<DepositType> builder)
        {
            builder.HasKey(dt => dt.Id);


            builder.HasMany(dt => dt.Deposits)
                .WithOne(d => d.DepositType)
                .HasForeignKey(dt => dt.DepositTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(dt => dt.BonusRate)
                .HasColumnType("decimal(9, 2)");

            builder.Property(dt => dt.AnnualRate)
                .HasColumnType("decimal(9, 2)");

            builder.Property(dt => dt.MinimumDepositAmount)
                .HasColumnType("decimal(9, 2)");

            builder.Property(dt => dt.MaximumDepositAmount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(dt => dt.MinimumReplenishmentAmount)
                .HasColumnType("decimal(9, 2)");
        }
    }
}
