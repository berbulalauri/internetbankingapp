using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class EmploymentTypeConfiguration : IEntityTypeConfiguration<EmploymentType>
    {
        public void Configure(EntityTypeBuilder<EmploymentType> builder)
        {
            builder.HasKey(et => et.Id);


            builder.HasMany(et => et.Employments)
                .WithOne(e => e.EmploymentType)
                .HasForeignKey(e => e.EmploymentTypeId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
