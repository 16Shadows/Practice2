using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Models
{
	public class CategoryVM : ItemBaseVM
	{
		public CategoryVM(Category category) : base(nameof(Category), category.Name, category.Id) { }
	}
}
