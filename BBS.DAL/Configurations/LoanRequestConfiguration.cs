using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class LoanRequestConfiguration : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> builder)
        {
            builder.HasKey(lr => lr.Id);


            builder.HasOne(lr => lr.Loan)
                .WithOne(l => l.LoanRequest)
                .HasForeignKey<Loan>(l => l.LoanRequestId);

            builder.HasOne(lr => lr.User)
                .WithMany(u => u.LoanRequests)
                .HasForeignKey(lr => lr.UserId);


            builder.Property(lr => lr.LoanSum)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
