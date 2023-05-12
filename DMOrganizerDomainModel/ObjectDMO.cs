using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ObjectDMO
    {
        public int Id { get; set; }
        public string LinkToObject { get; set; }

        // required many-to-many: pages/containers
        public virtual List<PageDMO> PageDMOs { get; set; } = new List<PageDMO>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
