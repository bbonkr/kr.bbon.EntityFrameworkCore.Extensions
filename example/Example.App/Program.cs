using System;
using System.Linq;

using kr.bbon.EntityFrameworkCore.Extensions;

namespace Example.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var helper = new DatabaseHelper())
            {
                helper.InitializeDatabase();
                helper.Seed();

                Action<Document> consoleWriteAction = (el) =>
                {
                    Console.WriteLine($"{el.Id} | {el.Content} | {el.CreatedAt:HH:mm:ss}");
                };

                using (var ctx = new ExampleDbContext())
                {
                    Console.WriteLine("-".PadRight(80, '-'));
                    Console.WriteLine("All elements");
                    Console.WriteLine("-".PadRight(80, '-'));
                    ctx.Documents.ToList().ForEach(consoleWriteAction);

                    Console.WriteLine();

                    Console.WriteLine("-".PadRight(80, '-'));
                    Console.WriteLine("Sort Id descendant");
                    Console.WriteLine("-".PadRight(80, '-'));
                    ctx.Documents.Sort(nameof(Document.Id), false).ToList().ForEach(consoleWriteAction);

                    Console.WriteLine();

                    Console.WriteLine("-".PadRight(80, '-'));
                    Console.WriteLine("Sort CreatedAt ascendant");
                    Console.WriteLine("-".PadRight(80, '-'));
                    ctx.Documents.Sort(nameof(Document.CreatedAt), true).ToList().ForEach(consoleWriteAction);

                    Console.WriteLine();

                    Console.WriteLine("-".PadRight(80, '-'));
                    Console.WriteLine("Sort Content ascendant then Id descendant");
                    Console.WriteLine("-".PadRight(80, '-'));
                    ctx.Documents.Sort(nameof(Document.Content)).Sort(nameof(Document.Id), false).ToList().ForEach(consoleWriteAction);


                }

                Console.ReadLine();
            }
        }
    }
}