using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSCCProject.Domain.Entities;
using static SSCCProject.Domain.Constants;

namespace SSCCProject.Infrastructure.EntityTypeConfigurations;


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
