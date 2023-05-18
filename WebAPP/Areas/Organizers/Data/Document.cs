using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(Title), nameof(CategoryId), IsUnique = true)]
    public class Document : SectionBase
    {
        public List<Tag> Tags { get; } = new List<Tag>();
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
