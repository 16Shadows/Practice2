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
	[Route("Organizers/{organizerId:int:required}/category/{categoryId:int:required}")]
	public class CategoryController : Controller
	{
		private readonly WebAPPContext dbContext;
		private readonly UserManager<UserAccount> userManager;

		public CategoryController(WebAPPContext dbContext, UserManager<UserAccount> userManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
		}

		[HttpGet("")]
		public async Task<ActionResult<CategoryContentVM>> Root(int organizerId, int categoryId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId))
				return NotFound();

			Category? target = dbContext
							   .Categories
							   .Where(x => x.Id == categoryId && x.OrganizerId == organizerId)
							   .Include(x => x.Subcategories)
							   .Include(x => x.Documents)
							   .ToArray()
							   .FirstOrDefault();

			if (target == null)
				return NotFound();

			return Ok(new CategoryContentVM(target));
		}

		[HttpPost("createCategory/{name:required:minlength(1)}")]
		public async Task<ActionResult<CategoryVM>> CreateCategory(int organizerId, int categoryId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? organizer = dbContext.GetOrganizer(user, organizerId);

			if (organizer == null) 
				return NotFound();

			Category? parent = dbContext.GetCategory(organizerId, categoryId);

			if (parent == null)
				return NotFound();

			bool any = dbContext
					   .Categories
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == categoryId && x.Name == name)
					   .Include(x => x.Parent)
					   .ToArray()
					   .Any(x => x.Parent.Equals(parent));

			if (any)
				return Conflict();

			Category category = new Category()
			{
				Name = name,
				Parent = parent,
				Organizer = organizer
			};
			dbContext.Categories.Add(category);

			await dbContext.SaveChangesAsync();

			return Ok(new CategoryVM(category));
		}

		[HttpPost("createDocument/{name:required:minlength(1)}")]
		public async Task<ActionResult<DocumentVM>> CreateDocument(int organizerId, int categoryId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? organizer = dbContext.GetOrganizer(user, organizerId);

			if (organizer == null) 
				return NotFound();

			Category? parent = dbContext.GetCategory(organizerId, categoryId);

			if (parent == null)
				return NotFound();

			bool any = dbContext
					   .Documents
					   .Where(x => x.OrganizerId == organizerId && x.CategoryId == categoryId && x.Title == name)
					   .Include(x => x.Category)
					   .ToArray()
					   .Any(x => x.Category.Equals(parent));

			if (any)
				return Conflict();

			Document document = new Document()
			{
				Title = name,
				Category = parent,
				Organizer = organizer
			};
			dbContext.Documents.Add(document);

			await dbContext.SaveChangesAsync();

			return Ok(new DocumentVM(document));
		}

		[HttpPost("rename/{name:required:minlength(1)}")]
		public async Task<ActionResult<CategoryVM>> RenameCategory(int organizerId, int categoryId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Category? category = dbContext
								 .Categories
								 .Where(x => x.Id == categoryId && x.OrganizerId == organizerId)
								 .Include(x => x.Parent)
								 .ToArray()
								 .FirstOrDefault();

			if (category == null)
				return NotFound();

			bool any = dbContext
					   .Categories
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == category.ParentId && x.Id != categoryId && x.Name == name)
					   .Include(x => x.Parent)
					   .ToArray()
					   .Any(x => x.Parent.Equals(category.Parent));
			
			if (any)
				return Conflict();

			category.Name = name;
			
			await dbContext.SaveChangesAsync();

			return Ok( new CategoryVM(category) );
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<CategoryVM>> DeleteCategory(int organizerId, int categoryId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Category? category = dbContext.GetCategory(organizerId, categoryId);

			if (category == null)
				return NotFound();

			CategoryVM vm = new CategoryVM(category);

			dbContext.Categories.Remove(category);

			await dbContext.SaveChangesAsync();

			return Ok(vm);
		}
	}
}
