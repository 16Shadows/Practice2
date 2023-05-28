using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class OrganizerVM : ItemBaseVM
	{
		public OrganizerVM(Organizer organizer) : base(nameof(Organizer), organizer.Name, organizer.Id) {}
	}
}
