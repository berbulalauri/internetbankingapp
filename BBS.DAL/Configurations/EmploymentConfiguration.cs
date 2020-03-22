using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class EmploymentConfiguration : IEntityTypeConfiguration<Employment>
    {
        public void Configure(EntityTypeBuilder<Employment> builder)
        {
            builder.HasKey(e => e.Id);


            builder.HasMany(e => e.LoanRequests)
                .WithOne(lr => lr.Employment)
                .HasForeignKey(lr => lr.EmployementId)
                .OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
