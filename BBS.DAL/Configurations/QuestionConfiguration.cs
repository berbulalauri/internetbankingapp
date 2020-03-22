using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);


            builder.HasMany(q => q.Users)
                .WithOne(u => u.Question)
                .HasForeignKey(u => u.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
