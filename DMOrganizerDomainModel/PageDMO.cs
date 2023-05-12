using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DMOrganizerDomainModel
{
    //?
    [Index(nameof(Position), nameof(ParentBookID), IsUnique = true)]
    public class PageDMO
    {
        public PageDMO(int position, Book parentBook)
        {
            Position = position;
            ParentBook = parentBook; // not null
            ParentBookID = parentBook.ID; // ???
        }
        public int ID { get; set; }
        public int Position { get; set; }
        // required one-to-many: child of books
        public int ParentBookID { get; set; } // Required foreign key property
        public Book ParentBook { get; set; } // Required reference navigation to principal

        // required many-to-many: pages/containers
        public List<ContainerDMO> ContainerDMOs { get; set;} = new List<ContainerDMO>();
    }
}
