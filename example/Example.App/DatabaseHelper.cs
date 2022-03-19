using System;
using System.Collections.Generic;

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
                var subItemId = 1;
                var authorId = 1;
                
                for (var id = 1; id <= 10; id++)
                {
                    ctx.Documents.Add(new Document
                    {
                        Id = id,
                        Content = $"{(char)(65 + (id % 2))}",
                        CreatedAt = DateTimeOffset.UtcNow.AddSeconds(id),
                        Items = new List<SubItem>
                        {
                          new SubItem{ Id =subItemId++, Name = $"Item #{subItemId}"},
                          new SubItem{ Id =subItemId++, Name = $"Item #{subItemId}"},
                          new SubItem{ Id =subItemId++, Name = $"Item #{subItemId}"},
                        },
                        Author = new Author
                        {
                            Id = authorId++,
                            Name = $"Tester #{authorId}"
                        },
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