using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;

namespace WebAPP.Areas.Organizers.Controllers
{
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
								.Where(x => x.OwnerId == user.Id && x.Id == organizerId)
								.Include(x => x.Subcategories)
								.Include(x => x.Documents)
								.First();

			if (target == null)
				return NotFound();

			return Ok ( new CategoryBaseVM(target) );
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
