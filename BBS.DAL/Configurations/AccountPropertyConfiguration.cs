using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Configurations
{
    public class AccountPropertyConfiguration : IEntityTypeConfiguration<AccountProperty>
    {
        public void Configure(EntityTypeBuilder<AccountProperty> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }
}
