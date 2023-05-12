
using System.Reflection.Metadata;

namespace DMOrganizerDomainModel
{
    public class PageDMO
    {
        public PageDMO(int id, int position, Book parentBook, List<ContainerDMO> containerDMOs)
        {
            ID = id;
            Position = position;
            ParentBook = parentBook; // not null
            ParentBookID = parentBook.ID; // ???
            ContainerDMOs = containerDMOs;
        }
        // all not null
        public int ID { get; set; }
        public int Position { get; set; }
        public int ParentBookID { get; set; } // Required foreign key property
        public Book ParentBook { get; set; } = null!;// Required reference navigation to principal

        // need join table of containers, many to many
        public List<ContainerDMO> ContainerDMOs { get; set;}
    }
}
