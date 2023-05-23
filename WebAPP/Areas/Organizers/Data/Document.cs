using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Data
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    [Index(nameof(Title), nameof(CategoryId), IsUnique = true)]
	public class Document : SectionBase
	{
        public int CategoryId { get; set; }
		public CategoryBase Category { get; set; }
		public ICollection<Tag> Tags { get; } = new HashSet<Tag>();

		public override bool Equals(object? obj)
		{
			return obj is Document other && other.Id == this.Id;
		}
	}
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
}
