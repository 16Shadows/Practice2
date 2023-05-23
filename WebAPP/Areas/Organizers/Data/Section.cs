using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    [Index(nameof(Title), nameof(ParentId), IsUnique = true)]
	public class Section : SectionBase
	{
        public int ParentId { get; set; }
        public SectionBase Parent { get; set; }

        public override bool Equals(object? obj)
		{
			return obj is Section other && other.Id == this.Id;
		}
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
}