using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class MapsConfig : IEntityTypeConfiguration<Maps>
{
    public void Configure(EntityTypeBuilder<Maps> builder)
    {
        builder.HasKey(a => a.Id);
    }
}