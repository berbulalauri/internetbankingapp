using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class LoanTypeConfiguration : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> builder)
        {
            builder.HasKey(lt => lt.Id);


            builder.HasMany(lt => lt.LoanRequests)
                .WithOne(lr => lr.LoanType)
                .HasForeignKey(lr => lr.LoanTypeId)
                .OnDelete(DeleteBehavior.NoAction);


            //builder.Property(lt => lt.AdvancedRepayment)
            //    .HasColumnType("decimal(9, 2)");

            builder.Property(lt => lt.FeeForInsuranceLoan)
                .HasColumnType("decimal(9, 2)");

            builder.Property(lt => lt.FreeForServicesUponReciptCash)
                .HasColumnType("decimal(9, 2)");

            builder.Property(lt => lt.InterestRate)
                .HasColumnType("decimal(9, 2)");

            builder.Property(lt => lt.MinLoanSum)
                .HasColumnType("decimal(18, 4)");

            builder.Property(lt => lt.MaxLoanSum)
                .HasColumnType("decimal(18, 2)");

            builder.Property(lt => lt.MaxLoanSum)
                .HasColumnType("decimal(18, 2)");
            
            builder.Property(lt => lt.MonthlyFee)
                .HasColumnType("decimal(9, 2)");

            builder.Property(lt => lt.PaymentForAccidentInsurance)
                .HasColumnType("decimal(18, 2)");
        }
    }
}