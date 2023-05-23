using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class SectionVM : ItemBaseVM
	{
		public SectionVM(Section section) : base(nameof(Section), section.Title, section.Id) {}
	}
}
