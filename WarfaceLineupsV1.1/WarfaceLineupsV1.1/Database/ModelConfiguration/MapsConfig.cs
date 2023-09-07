using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.ModelConfiguration;

public class MapsConfig : IEntityTypeConfiguration<Map>
{
    public void Configure(EntityTypeBuilder<Map> builder)
    {
        builder.HasKey(m => m.Id);
    }
}