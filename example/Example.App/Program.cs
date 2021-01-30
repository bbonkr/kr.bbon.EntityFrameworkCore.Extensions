using System;

namespace Example.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Document
    {
        public int Id { get; set; }

        public string Content { get; set;}

        public DateTimeOffset CreatedAt { get; set; }
    }
}
