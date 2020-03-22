using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBS.DAL.Configurations
{
    class CardRequestConfiguration : IEntityTypeConfiguration<CardRequest>
    {
        public void Configure(EntityTypeBuilder<CardRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithOne(x => x.CardRequest)
                .HasForeignKey<CardRequest>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.Card)
                .WithOne(x => x.CardRequest)
                .HasForeignKey<CardRequest>(x => x.CardId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(x => x.CardRequestHistories)
                .WithOne(x => x.CardRequest)
                .HasForeignKey(x => x.CardRequestId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
