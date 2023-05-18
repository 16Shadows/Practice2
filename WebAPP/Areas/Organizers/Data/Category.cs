using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Data;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
[Index(nameof(Name), nameof(ParentId), IsUnique = true)]
public class Category : CategoryBase
{
    public int ParentId { get; set; }
    public CategoryBase Parent { get; set; }
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }

	public override bool Equals(object? obj)
	{
		return obj is Category other && other.Id == Id;
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()