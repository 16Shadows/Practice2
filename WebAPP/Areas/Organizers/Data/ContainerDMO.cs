
using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ContainerDMO
    {
        // all not null
        public int ID { get; set; }
        public int Type { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        // required many-to-many: pages/containers
        public List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>();
        // required many-to-many: containers/objects
        public List<ObjectDMO> ObjectDMOs { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
