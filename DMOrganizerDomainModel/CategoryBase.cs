using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class CategoryBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Category> Subcategories { get; } = new List<Category>();
        public virtual List<Document> Documents { get; } = new List<Document>();

        // required one-to-many: category/organizer is parent of books, may have no books
        public virtual List<Book> Books { get; } = new List<Book>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}