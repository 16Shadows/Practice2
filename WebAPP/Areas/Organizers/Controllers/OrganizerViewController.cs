using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
								.Where(x => x.OwnerId == user.Id && x.Id == organizerId)
								.Include(x => x.Subcategories)
								.Include(x => x.Documents)
								.ToArray()
								.FirstOrDefault();

			if (target == null)
				return NotFound();

			return Ok ( new CategoryBaseVM(target) );
		}

		[HttpGet("createCategory/{name:minlength(1):required}")]
		public async Task<ActionResult<CategoryBaseVM>> CreateCategory(int organizerId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Categories
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == organizerId && x.Name == name)
					   .Include(x => x.Parent)
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

		[HttpPost("createDocument/{name:minlength(1):required}")]
		public async Task<ActionResult<CategoryBaseVM>> CreateDocument(int organizerId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? target = dbContext.GetOrganizer(user, organizerId);

			if (target == null)
				return NotFound();

			bool any = dbContext
					   .Documents
					   .Where(x => x.OrganizerId == organizerId && x.CategoryId == organizerId && x.Title == name)
					   .Include(x => x.Category)
					   .ToArray()
					   .Any(x => x.Category.GetType().IsAssignableTo(typeof(Organizer)));

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
