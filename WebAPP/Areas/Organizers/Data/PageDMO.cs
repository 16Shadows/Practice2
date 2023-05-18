using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    //?
    [Index(nameof(Position), nameof(ParentBookId), IsUnique = true)]
    public class PageDMO
    {
        public int Id { get; set; }
        public int Position { get; set; }
        // required one-to-many: child of books
        public int ParentBookId { get; set; } // Required foreign key property
        public Book ParentBook { get; set; } // Required reference navigation to principal

        // required many-to-many: pages/containers
        public List<ContainerDMO> ContainerDMOs { get; set;} = new List<ContainerDMO>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
