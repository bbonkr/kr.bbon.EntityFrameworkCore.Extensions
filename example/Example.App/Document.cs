using System;

namespace Example.App
{
    public class Document
    {
        
        public int Id { get; set; }

        public string Content { get; set;}

        public DateTimeOffset CreatedAt { get; set; }
    }
}
