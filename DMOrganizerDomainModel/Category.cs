using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    [Index(nameof(Name), nameof(Parent), IsUnique = true)]
    public class Category : CategoryBase
    {
        public Category(string name, CategoryBase parent) : base(name)
        {
            Parent = parent;
            ParentId = parent.Id;
        }

        public virtual int ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }
        // optional one-to-many: parent of books, may have no books
        public List<Book> Books { get; } = new List<Book>(); // Collection navigation containing dependents

    }
}
