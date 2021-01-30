
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
    }
}
