using System;
using System.Collections.Generic;

namespace Example.App
{
    public class Document
    {
        public int Id { get; set; }

        public string Content { get; set;}

        public DateTimeOffset CreatedAt { get; set; }
        
        public virtual ICollection<SubItem> Items { get; set; }
        public int AuthorId { get; set; }
        public  virtual Author Author { get; set; }
    }

    public class SubItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DocumentId { get; set; }
        
        public virtual Document Document { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public  virtual  ICollection<Document>  Documents { get; set; }
    }
}
