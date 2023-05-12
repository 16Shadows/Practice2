using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    public class ObjectDMO
    {
        public ObjectDMO(string link) 
        {
            LinkToObject = link;
        }
        public int ID { get; set; }
        public string LinkToObject { get; set; }

        // required many-to-many: pages/containers
        public List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>();
    }
}
