using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ObjectDMO
    {
        public int Id { get; set; }
        public string LinkToObject { get; set; }

        // required many-to-many: containers/objects
        public virtual List<ContainerDMO> ContainerDMOs { get; set; } = new List<ContainerDMO>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
