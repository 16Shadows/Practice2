using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class DocumentContentVM : SectionContentVM
	{
		public DocumentContentVM(Document document) : base(document)
		{
			Tags = document.Tags.Select(x => x.Name).ToList();
		}

		public List<string> Tags { get; }
	}
}
