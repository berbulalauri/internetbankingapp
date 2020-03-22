using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasMany(c => c.Deposits)
                .WithOne(d => d.Currency)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.LoanTypes)
                .WithOne(lt => lt.Currency)
                .HasForeignKey(lt => lt.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.DepositTypes)
                .WithOne(dt => dt.Currency)
                .HasForeignKey(dt => dt.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(c => c.Rate)
                .HasColumnType("decimal(10, 2)");
        }
    }
}
