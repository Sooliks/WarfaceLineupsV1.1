using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;


namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class AccountsConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasMany(a => a.Lineups);
        builder.HasMany(a => a.Comments);
    }
}