using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class ReportsConfig : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasOne<Account>(r => r.Sender);
        builder.HasOne<Lineup>(r => r.Lineup);
    }
}