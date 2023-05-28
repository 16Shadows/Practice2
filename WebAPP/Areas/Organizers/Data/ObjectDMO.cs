using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ObjectDMO
    {
        public int Id { get; set; }
        public string LinkToObject { get; set; }
		public int OrganizerId { get; set; }
		public Organizer Organizer { get; set; }

		// required one-to-many: containers/objects
		public int ParentContainerId { get; set; } // Required foreign key property
		public ContainerDMO ParentContainer { get; set; } // Required reference navigation to principal
	}
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
