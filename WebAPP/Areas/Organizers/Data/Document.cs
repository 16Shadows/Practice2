using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    [Index(nameof(Title), nameof(CategoryId), IsUnique = true)]
    public class Document : SectionBase
    {
        public ICollection<Tag> Tags { get; } = new HashSet<Tag>();
    }
}
