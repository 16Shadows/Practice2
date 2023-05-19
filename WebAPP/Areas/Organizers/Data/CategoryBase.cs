using System;
using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public abstract class CategoryBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Subcategories { get; } = new HashSet<Category>();
        public ICollection<Document> Documents { get; } = new HashSet<Document>();

        // required one-to-many: category/organizer is parent of books, may have no books
        public ICollection<Book> Books { get; } = new HashSet<Book>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}