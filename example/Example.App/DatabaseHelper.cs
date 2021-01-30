using System;

namespace Example.App
{
    public class DatabaseHelper :IDisposable
    {
      
        public void InitializeDatabase()
        {
            using (var ctx = new ExampleDbContext())
            {
                ctx.Database.EnsureCreated();
            }
        }


        public void Seed()
        {
            using (var ctx = new ExampleDbContext())
            {
                for (var id = 1; id <= 10; id++)
                {
                    ctx.Documents.Add(new Document
                    {
                        Id = id,
                        Content = $"{(char)(65 + (id % 2))}",
                        CreatedAt = DateTimeOffset.UtcNow.AddSeconds(id),
                    });
                }

                ctx.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var ctx = new ExampleDbContext())
            {
                ctx.Database.EnsureDeleted();
            }
        }

    }
}