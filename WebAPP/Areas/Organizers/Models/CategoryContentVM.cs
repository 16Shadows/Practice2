using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class CategoryContentVM
	{
		public CategoryContentVM(CategoryBase category)
		{
			Subcategories = category.Subcategories.Select(x => new CategoryVM(x)).ToList();
			Documents = category.Documents.Select(x => new DocumentVM(x)).ToList();
		}

		public List<CategoryVM> Subcategories { get; }
		public List<DocumentVM> Documents { get; }
	}
}
