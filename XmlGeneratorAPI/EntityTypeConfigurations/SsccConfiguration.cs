using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSCCProject.Domain.Entities;
using static SSCCProject.Domain.Constants;

namespace SSCCProject.Infrastructure.EntityTypeConfigurations;

public class SsccConfiguration : IEntityTypeConfiguration<SSCC>
{
    public void Configure(EntityTypeBuilder<SSCC> builder)
    {
        builder.Property(x => x.Code)
            .HasColumnType(DatabaseColumnTypes.VARCHAR)
            .IsRequired()
            .HasMaxLength(18); //length : 18

        builder.HasIndex(x => x.Code)
        .IsUnique();

        builder.Property(x => x.ExtensionDigit)
            .HasColumnType(DatabaseColumnTypes.VARCHAR)
            .IsRequired()
            .HasMaxLength(1); //length : 1

        builder.Property(x => x.Gs1CompanyPrefix)
            .HasColumnType(DatabaseColumnTypes.VARCHAR)
            .IsRequired()
            .HasMaxLength(12); //length : 4 to 12

        builder.Property(x => x.SerialNumberPaddedZeros)
           .HasColumnType(DatabaseColumnTypes.VARCHAR)
           .IsRequired()
           .HasMaxLength(12); //remaining digits after subtracting extension digit (length : 1), GS1 company prefix and (length : 4 to 12 ) , checkdigit (length : 1)

        builder.Property(x => x.CheckDigit)
            .HasColumnType(DatabaseColumnTypes.VARCHAR)
            .IsRequired()
            .HasMaxLength(1);
    }
}
