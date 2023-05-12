using Microsoft.EntityFrameworkCore;
using System;

namespace DMOrganizerDomainModel
{
    [Index(nameof(Name), nameof(Parent), IsUnique = true)]
    public class Category : CategoryBase
    {
        public Category(string name, CategoryBase parent) : base(name)
        {
            Parent = parent;
            ParentId = parent.Id;
        }

        public virtual int ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }
    }
}
