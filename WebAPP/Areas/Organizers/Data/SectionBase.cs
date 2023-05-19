using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public abstract class SectionBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<Section> Sections { get; } = new HashSet<Section>();
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
        public int CategoryId { get; set; }
        public CategoryBase Category { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
