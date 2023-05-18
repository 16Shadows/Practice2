using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Authorize]
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}/document/{documentId:int:required}")]
	public class DocumentController : Controller
	{
		private readonly WebAPPContext dbContext;
		private readonly UserManager<UserAccount> userManager;

		public DocumentController(WebAPPContext dbContext, UserManager<UserAccount> userManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
		}

		[HttpGet("")]
		public async Task<ActionResult> Root()
		{
			var user = await userManager.GetUserAsync(User);

			return Ok("Hello, world!");
		}

		[HttpPost("rename/{name:required:minlength(1)}")]
		public async Task<ActionResult<CategoryVM>> RenameDocument(int organizerId, int documentId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Document? document = dbContext.GetDocument(organizerId, documentId);

			if (document == null)
				return NotFound();

			bool any = dbContext
					   .Documents
					   .Where(x => x.OrganizerId == organizerId && x.CategoryId == document.CategoryId && x.Id != documentId && x.Title == name)
					   .Include(x => x.Category)
					   .ToArray()
					   .Any(x => x.Category.Equals(document.Category));
			
			if (any)
				return Conflict();

			document.Title = name;

			await dbContext.SaveChangesAsync();
			
			return Ok( new DocumentVM(document) );
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<CategoryVM>> DeleteDocument(int organizerId, int categoryId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Document? document = dbContext.GetDocument(organizerId, categoryId);

			if (document == null)
				return NotFound();

			DocumentVM vm = new DocumentVM(document);

			dbContext.Documents.Remove(document);

			await dbContext.SaveChangesAsync();

			return Ok(vm);
		}
	}
}
