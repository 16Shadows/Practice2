using Microsoft.EntityFrameworkCore;
using System;

namespace DMOrganizerDomainModel
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Index(nameof(Name), nameof(ParentId), IsUnique = true)]
    public class Category : CategoryBase
    {
        public virtual int ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
