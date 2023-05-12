using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class CategoryBase
    {
        public CategoryBase(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Category> Subcategories { get; } = new List<Category>();
        public virtual List<Document> Documents { get; } = new List<Document>();

        // required one-to-many: category/organizer is parent of books, may have no books
        public List<Book> Books { get; } = new List<Book>();
    }
}