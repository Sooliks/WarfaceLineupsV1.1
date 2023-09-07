using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class LineupsConfig : IEntityTypeConfiguration<Lineup>
{
    public void Configure(EntityTypeBuilder<Lineup> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasOne<Account>(l => l.Owner);
        builder.HasMany<Comment>(l=>l.Comments);
    }
}