
namespace DMOrganizerDomainModel
{
    public class ContainerDMO
    {
        public ContainerDMO(int id, int type, uint width, uint height, int coordX, int coordY, List<PageDMO> pageDMOs) 
        {
            ID = id;
            Type = type;
            Width = width;
            Height = height;
            CoordX = coordX;
            CoordY = coordY;
            PageDMOs = pageDMOs;
        }
        // all not null
        public int ID { get; set; }
        public int Type { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        // need join table of objects, many to many
        public List<PageDMO> PageDMOs { get; set; }
        public List<ObjectDMO> ObjectDMOs { get; set; }
    }
}
