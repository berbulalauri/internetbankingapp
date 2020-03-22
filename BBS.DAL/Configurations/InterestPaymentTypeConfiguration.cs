using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class InterestPaymentTypeConfiguration : IEntityTypeConfiguration<InterestPaymentType>
    {
        public void Configure(EntityTypeBuilder<InterestPaymentType> builder)
        {
            builder.HasKey(ipt => ipt.Id);

            builder.HasMany(ipt => ipt.DepositTypes)
                .WithOne(dt => dt.InterestPaymentType)
                .HasForeignKey(dt => dt.InterestPaymentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
