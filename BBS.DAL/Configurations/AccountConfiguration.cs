using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);


            builder.HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Currency)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Services)
               .WithOne(t => t.Account)
               .HasForeignKey(t => t.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.DepositAccountForInterests)
                .WithOne(dai => dai.AccountForInterest)
                .HasForeignKey(dai => dai.AccountForInterestId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.DepositAccountToTransfers)
                .WithOne(dat => dat.AccountToTransfer)
                .HasForeignKey(dat => dat.AccountToTransferId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.SendTransactions)
                .WithOne(t => t.SenderAccount)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasMany(a => a.RecievedTransactions)
                .WithOne(t => t.ReceiverAccount)
                .HasForeignKey(t => t.ReceiverAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.AccruedLoanRequests)
                .WithOne(lr => lr.AccrueAccount)
                .HasForeignKey(lr => lr.AccrueAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Loans)
                .WithOne(l => l.Account)
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.TransferedLoanRequests)
                .WithOne(lr => lr.TransferAccount)
                .HasForeignKey(lr => lr.TransferAccountId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(a => a.Balance)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
