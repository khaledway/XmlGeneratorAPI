using Microsoft.EntityFrameworkCore;
using SSCCProject.Domain.Entities;
using XmlGeneratorAPI.Models;

namespace XmlGeneratorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<FilesUpload> FilesUploads { get; set; }
        public DbSet<SSCC> SSCCs { get; set; }
        public DbSet<LogisticUnit> LogisticUnits { get; set; }
        public DbSet<SGTIN> SGTINs { get; set; }
        public DbSet<LogisticUnitAssignment> LogisticUnitsAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FilesUpload>(entity =>
            {
                entity.ToTable("FilesUpload");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Path).IsRequired().HasMaxLength(1024);
                entity.Property(x => x.Folder).IsRequired().HasMaxLength(1024);
                entity.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(512);
                entity.Property(x => x.StoredFileName).IsRequired().HasMaxLength(256);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
