namespace XmlGeneratorAPI.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XmlGeneratorAPI.Models;
using static XmlGeneratorAPI.Models.Constants;

public class LogisticUnitConfiguration : IEntityTypeConfiguration<LogisticUnit>
{
    public void Configure(EntityTypeBuilder<LogisticUnit> builder)
    {
        builder.Property(x => x.Name)
               .HasColumnType(DatabaseColumnTypes.VARCHAR)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.WeightInKg)
               .HasPrecision(10, 2);      // e.g., 99999999.99 max

        builder.Property(x => x.LengthInCm)
               .HasPrecision(10, 2);

        builder.Property(x => x.WidthInCm)
               .HasPrecision(10, 2);

        builder.Property(x => x.HeightInCm)
               .HasPrecision(10, 2);

        builder.Property(x => x.Status)
               .IsRequired();
    }
}