using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Data;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
[Index(nameof(Name), nameof(ParentId), IsUnique = true)]
public class Category : CategoryBase
{
    public int ParentId { get; set; }
    public virtual CategoryBase Parent { get; set; }
    public int OrganizerId { get; set; }
    public virtual Organizer Organizer { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.