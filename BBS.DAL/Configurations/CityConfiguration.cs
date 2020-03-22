using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasMany(c => c.Users)
                .WithOne(u => u.City)
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
