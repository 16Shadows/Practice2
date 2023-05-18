using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}")]
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
		public async Task<IActionResult> Index(int organizerId)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == organizerId);

			if (target == null)
				return NotFound();

			ViewData["Title"] = target.Name;
			ViewData["OrganizerID"] = organizerId;

			return View("Organizer");
		}

		[HttpGet("root")]
		public async Task<ActionResult<CategoryBaseVM>> Root(int organizerId)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == organizerId);

			if (target == null)
				return NotFound();

			return Ok ( new CategoryBaseVM(target) );
		}

		[HttpGet("category/{categoryId:int:required}")]
		public async Task<ActionResult<CategoryBaseVM>> Category(int organizerId, int categoryId)
		{
			var user = await userManager.GetUserAsync(User);

			bool any = dbContext
					   .Organizers
					   .Any(x => x.OwnerId == user.Id && x.Id == organizerId);

			if (!any)
				return NotFound();

			Category? category = dbContext
								.Categories
								.First(x => x.Id == categoryId && x.OrganizerId == organizerId);

			return Ok ( new CategoryBaseVM(category) );
		}

		[HttpPost("category/create/{name:minlength(1):required}")]
		public async Task<ActionResult<CategoryBaseVM>> CreateCategory(int organizerId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Categories
					   .Where(x => x.OrganizerId == organizerId && x.Name == name)
					   .ToArray()
					   .FirstOrDefault((Category?)null)
					   ?.Parent == target;

			if (any)
				return Conflict();

			Category category = new Category()
			{
				Name = name,
				Parent = target,
				Organizer = target
			};
			dbContext.Categories.Add(category);

			await dbContext.SaveChangesAsync();

			return Ok(new CategoryVM(category));
		}

		[HttpPost("document/create/{name:minlength(1):required}")]
		public async Task<ActionResult<CategoryBaseVM>> CreateDocument(int organizerId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.First(x => x.OwnerId == user.Id && x.Id == organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Documents
					   .Where(x => x.OrganizerId == organizerId && x.Title == name)
					   .ToArray()
					   .FirstOrDefault((Document?)null)
					   ?.Category == target;

			if (any)
				return Conflict();

			Document category = new Document()
			{
				Title = name,
				Category = target,
				Organizer = target
			};
			dbContext.Documents.Add(category);

			await dbContext.SaveChangesAsync();

			return Ok(new DocumentVM(category));
		}
	}
}
