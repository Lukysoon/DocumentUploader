using DocumentUploader.DocumentService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploader.DocumentService.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Czech_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(ApplicationDbContext).Assembly
            );
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Tag> Tags { get; set; }
}