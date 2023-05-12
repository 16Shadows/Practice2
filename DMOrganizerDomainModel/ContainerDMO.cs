
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class ContainerDMO
    {
        public ContainerDMO(int type, uint width, uint height, int coordX, int coordY) 
        {
            Type = type;
            Width = width;
            Height = height;
            CoordX = coordX;
            CoordY = coordY;
        }
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
}
