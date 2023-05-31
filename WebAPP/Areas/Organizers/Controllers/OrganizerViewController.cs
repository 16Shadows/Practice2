using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Authorize]
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}")]
	public class OrganizerViewController : Controller
	{
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

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			ViewData["OrganizerName"] = ViewData["Title"] = target.Name;
			ViewData["OrganizerID"] = organizerId;

			return View("Organizer");
		}

		[HttpGet("root")]
		public async Task<ActionResult<CategoryContentVM>> Root(int organizerId)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext
								.Organizers
								.Where(x => x.OwnerId == user.Id && x.Id == organizerId)
								.Include(x => x.Subcategories)
								.Include(x => x.Documents)
								.Include(x => x.Books)
								.AsSplitQuery()
								.ToArray()
								.FirstOrDefault();

			if (target == null)
				return NotFound();

			return Ok ( new CategoryContentVM(target) );
		}

		[HttpPost("createCategory")]
		[Consumes("text/plain")]
		public async Task<ActionResult<CategoryContentVM>> CreateCategory(int organizerId, [FromBody] string name)
		{
			if (name.Length == 0)
				return BadRequest();

			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Categories
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == organizerId && x.Name == name)
					   .Include(x => x.Parent)
					   .AsSplitQuery()
					   .ToArray()
					   .Any(x => x.Parent.GetType().IsAssignableTo(typeof(Organizer)));

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

		[HttpPost("createDocument")]
		[Consumes("text/plain")]
		public async Task<ActionResult<CategoryContentVM>> CreateDocument(int organizerId, [FromBody] string name)
		{
			if (name.Length == 0)
				return BadRequest();

			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Documents
					   .Where(x => x.OrganizerId == organizerId && x.CategoryId == organizerId && x.Title == name)
					   .Include(x => x.Category)
					   .AsSplitQuery()
					   .ToArray()
					   .Any(x => x.Category.GetType().IsAssignableTo(typeof(Organizer)));

			if (any)
				return Conflict();

			Document document = new Document()
			{
				Title = name,
				Category = target,
				Organizer = target
			};
			dbContext.Documents.Add(document);

			await dbContext.SaveChangesAsync();

			return Ok(new DocumentVM(document));
		}

		// Book
		[HttpPost("createBook")]
		[Consumes("text/plain")]
		public async Task<ActionResult<Book>> CreateBook(int organizerId, [FromBody] string name)
		{
			if (name.Length == 0)
				return BadRequest();

			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Books
					   .Where(b => b.OrganizerId == organizerId && b.ParentCategoryId == organizerId && b.Name == name)
					   .Include(b => b.ParentCategory)
					   .AsSplitQuery()
					   .ToArray()
					   .Any(b => b.ParentCategory.GetType().IsAssignableTo(typeof(Organizer)));

			if (any)
				return Conflict();

			Book book = new Book()
			{
				Name = name,
				ParentCategory = target,
				Organizer = target,
				OrganizerId = organizerId
			};
			dbContext.Books.Add(book);

			await dbContext.SaveChangesAsync();

			return Ok(book);
		}
	}
}
