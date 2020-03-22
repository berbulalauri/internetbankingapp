using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(lr => lr.LoanInterestRate)
                .HasColumnType("decimal(9, 2)");

            builder.Property(lr => lr.LoanPayment)
                .HasColumnType("decimal(18, 3)");

            builder.Property(lr => lr.MonthlyPayment)
                .HasColumnType("decimal(18, 3)");

            builder.Property(lr => lr.OriginalLoanSum)
                .HasColumnType("decimal(18, 3)");

            builder.Property(lr => lr.PercentPayment)
                .HasColumnType("decimal(18, 3)");

            builder.Property(lr => lr.RemainingLoanSum)
                .HasColumnType("decimal(18, 3)");
        }
    }
}
