using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Data
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(Name), nameof(OrganizerId), IsUnique = true)]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
        public ICollection<Document> Documents { get; } = new HashSet<Document>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
