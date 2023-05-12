using Microsoft.EntityFrameworkCore;

namespace DMOrganizerDomainModel
{
    [Index(nameof(Title), nameof(Parent), IsUnique = true)]
    public class Section : SectionBase
    {
        public Section(string title, string text, SectionBase parent) : base(title, text)
        {
            Parent = parent;
            ParentId = parent.Id;
        }

        public virtual int ParentId { get; set; }
        public virtual SectionBase Parent { get; set; }
    }
}
