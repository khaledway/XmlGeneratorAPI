using Microsoft.EntityFrameworkCore;
using XmlGeneratorAPI.Models;

namespace XmlGeneratorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FilesUpload> FilesUploads => Set<FilesUpload>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
    }
}
