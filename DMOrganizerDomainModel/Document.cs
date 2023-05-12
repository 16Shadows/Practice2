using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(Title), nameof(ParentId), IsUnique = true)]
    public class Document : SectionBase
    {
        public int ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }
        public virtual List<Tag> Tags { get; } = new List<Tag>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
