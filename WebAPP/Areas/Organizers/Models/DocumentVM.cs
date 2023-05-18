using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class DocumentVM : ItemBaseVM
	{
		public DocumentVM(Document document) : base(nameof(Document), document.Title, document.Id) { }
	}
}
