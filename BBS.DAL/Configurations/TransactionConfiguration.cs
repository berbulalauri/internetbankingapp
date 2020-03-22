using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BBS.DAL.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.HasKey(t => t.Id);


            builder.HasOne(t => t.SenderCard)
                .WithMany(sc => sc.Transactions)
                .HasForeignKey(t => t.SenderCardId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
