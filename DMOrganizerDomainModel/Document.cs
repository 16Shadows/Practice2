using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DMOrganizerDomainModel
{
    [Index(nameof(Title), nameof(Parent), IsUnique = true)]
    public class Document : SectionBase
    {
        public Document(string title, string text, CategoryBase parent) : base(title, text)
        {
            Parent = parent;
            ParentId = Parent.Id;
        }

        public virtual int ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }
        public virtual List<Tag> Tags { get; } = new List<Tag>();
    }
}
