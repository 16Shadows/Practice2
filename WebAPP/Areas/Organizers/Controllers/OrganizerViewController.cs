using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System.Data.Entity.Core.Mapping;
using System.Runtime.CompilerServices;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{id:int:required}")]
	public class OrganizerViewController : Controller
	{
		public class ItemBaseVM
		{
			protected ItemBaseVM(string type, string name, int id)
			{
				Type = type ?? throw new ArgumentNullException(nameof(type));
				Name = name ?? throw new ArgumentNullException(nameof(name));
				Id = id;
			}

			public string Type { get; }
			public string Name { get; }
			public int Id { get; }
		}

		public class CategoryVM : ItemBaseVM
		{
			public CategoryVM(Category category) : base(nameof(Category), category.Name, category.Id) { }
		}

		public class DocumentVM : ItemBaseVM
		{
			public DocumentVM(Document document) : base(nameof(Document), document.Title, document.Id) { }
		}

		public class CategoryBaseVM
		{
			public CategoryBaseVM(CategoryBase category)
			{
				Subcategories = category.Subcategories.Select(x => new CategoryVM(x)).ToList();
				Documents = category.Documents.Select(x => new DocumentVM(x)).ToList();
			}

			public List<CategoryVM> Subcategories { get; }
			public List<DocumentVM> Documents { get; }
		}

		private readonly UserManager<UserAccount> userManager;
        private readonly WebAPPContext dbContext;

        public OrganizerViewController(WebAPPContext context, UserManager<UserAccount> manager)
        {
            userManager = manager;
            dbContext = context;
        }

		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == id);

			if (target == null)
				return NotFound();

			ViewData["Title"] = target.Name;
			ViewData["OrganizerID"] = id;

			return View("Organizer");
		}

		[HttpGet("root")]
		public async Task<ActionResult<CategoryBaseVM>> Root(int id)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == id);

			if (target == null)
				return NotFound();

			return Ok ( new CategoryBaseVM(target) );
		}

		[HttpGet("category/{categoryId:int:required}")]
		public async Task<ActionResult<CategoryBaseVM>> Category(int id, int categoryId)
		{
			var user = await userManager.GetUserAsync(User);

			bool any = dbContext
					   .Organizers
					   .Any(x => x.OwnerId == user.Id && x.Id == id);

			if (!any)
				return NotFound();

			Category? category = dbContext
								.Categories
								.First(x => x.Id == categoryId && x.OrganizerId == id);

			return Ok ( new CategoryBaseVM(category) );
		}
	}
}
