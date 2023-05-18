using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Identity.Data;

namespace WebAPP.Areas.Organizers.Data
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    [Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
	public class Organizer : CategoryBase
	{
        public string OwnerId { get; set; }
        public UserAccount Owner { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is Organizer other && other.Id == Id;
		}
	}
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
