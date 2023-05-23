using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class SectionContentVM
	{
		public SectionContentVM(SectionBase section)
		{
			if (section == null) 
				throw new ArgumentNullException(nameof(section));

			Content = section.Text;
			Sections = section.Sections.Select(x => new SectionVM(x)).ToList();
		}

		public string Content { get; }
		public List<SectionVM> Sections { get; }
	}
}
