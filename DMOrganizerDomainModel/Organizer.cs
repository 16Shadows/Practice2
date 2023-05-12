using Microsoft.EntityFrameworkCore;

namespace DMOrganizerDomainModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
    public class Organizer : CategoryBase
    {
        public int OwnerId { get; set; }
        public virtual Account Owner { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
