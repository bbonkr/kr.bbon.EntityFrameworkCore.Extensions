
using Microsoft.EntityFrameworkCore;

namespace Example.App
{
    public class ExampleDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Example");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubItem>()
                .HasOne(x => x.Document)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.DocumentId);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.AuthorId);

        }
    }
}
