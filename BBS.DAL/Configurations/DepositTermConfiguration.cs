using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Configurations
{
    public class DepositTermConfiguration : IEntityTypeConfiguration<DepositTerm>
    {
        public void Configure(EntityTypeBuilder<DepositTerm> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(dt => dt.DepositTypes)
                .WithOne(d => d.DepositTerm)
                .HasForeignKey(dt => dt.DepositTermId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(dt => dt.Deposit)
               .WithOne(d => d.DepositTerm)
               .HasForeignKey(dt => dt.DepositTermId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
