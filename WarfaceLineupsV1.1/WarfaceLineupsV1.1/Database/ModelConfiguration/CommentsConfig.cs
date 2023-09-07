using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class CommentsConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne<Account>(c => c.Owner);
        builder.HasOne<Lineup>(c => c.Lineup);
    }
}