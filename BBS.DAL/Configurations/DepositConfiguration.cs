using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.HasKey(d => d.Id);


            builder.HasOne(d => d.User)
                .WithMany(u => u.Deposits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(d => d.DepositAmount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
