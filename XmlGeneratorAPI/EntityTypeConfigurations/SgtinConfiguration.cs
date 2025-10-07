using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XmlGeneratorAPI.Models;
using static XmlGeneratorAPI.Models.Constants;

namespace XmlGeneratorAPI.EntityTypeConfigurations;


public class SgtinConfiguration : IEntityTypeConfiguration<SGTIN>
{
    public void Configure(EntityTypeBuilder<SGTIN> builder)
    {
        builder.Property(x => x.Code)
            .HasColumnType(DatabaseColumnTypes.VARCHAR)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Code)
               .IsUnique();
    }
}
