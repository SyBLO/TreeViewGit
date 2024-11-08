using MauiAppDTClassLibrary.Models.FileEntityModels;
using Microsoft.EntityFrameworkCore;


namespace MauiAppDTClassLibrary.Data
{
    public class FileDbContext : DbContext
    {

        public FileDbContext()
        {
        }

        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "C:\\App\\maui\\BaseSQLite\\filedirectories.db");
            options.UseSqlite($"Data Source={dbPath}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Index pour accélérer la recherche par FileName
            modelBuilder.Entity<FileEntity>().HasIndex(f => f.FileName);
        }

        public DbSet<FileEntity> Files { get; set; }

    }
}
