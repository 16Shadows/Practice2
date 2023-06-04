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
	[Route("{area}/{organizerId:int:required}/document/{documentId:int:required}")]
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
		public async Task<ActionResult<DocumentContentVM>> Root(int organizerId, int documentId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId))
				return NotFound();

			Document? document = dbContext
								 .Documents
								 .Where(x => x.OrganizerId == organizerId && x.Id == documentId)
								 .Include(x => x.Sections)
								 .Include(x => x.Tags)
								 .AsSplitQuery()
								 .ToArray()
								 .FirstOrDefault();

			if (document == null)
				return NotFound();

			return Ok(new DocumentContentVM(document));
		}

		[HttpPost("rename")]
		[Consumes("text/plain")]
		public async Task<ActionResult> RenameDocument(int organizerId, int documentId, [FromBody] string name)
		{
			if (name.Length == 0)
				return BadRequest();

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
					   .AsSplitQuery()
					   .ToArray()
					   .Any(x => x.Category.Equals(document.Category));
			
			if (any)
				return Conflict();

			document.Title = name;

			await dbContext.SaveChangesAsync();
			
			return Ok();
		}

		[HttpPost("setContent")]
		[Consumes("text/plain")]
		public async Task<ActionResult> SetContent(int organizerId, int documentId, [FromBody] string newContent)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId))
				return NotFound();

			Document? document = dbContext.GetDocument(organizerId, documentId);

			if (document == null)
				return NotFound();

			document.Text = newContent;

			await dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpPost("createSection")]
		[Consumes("text/plain")]
		public async Task<ActionResult<DocumentVM>> CreateSection(int organizerId, int documentId, [FromBody] string name)
		{
			if (name.Length == 0)
				return BadRequest();

			var user = await userManager.GetUserAsync(User);

			Organizer? organizer = dbContext.GetOrganizer(user, organizerId);

			if (organizer == null) 
				return NotFound();

			Document? document = dbContext.GetDocument(organizerId, documentId);

			if (document == null)
				return NotFound();

			bool any = dbContext
					   .Sections
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == document.Id && x.Title == name)
					   .Include(x => x.Parent)
					   .AsSplitQuery()
					   .ToArray()
					   .Any(x => x.Parent.Equals(document));
			
			if (any)
				return Conflict();

			Section section = new Section()
			{
				Title = name,
				Organizer = organizer,
				Parent = document
			};

			dbContext.Sections.Add(section);

			await dbContext.SaveChangesAsync();
			
			return Ok( new SectionVM(section) );
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteDocument(int organizerId, int documentId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Document? document = dbContext.GetDocument(organizerId, documentId);

			if (document == null)
				return NotFound();

			dbContext.Documents.Remove(document);

			await dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpPost("addTag")]
		[Consumes("text/plain")]
		public async Task<ActionResult> AddTag(int organizerId, int documentId, [FromBody] string tagText)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? organizer = dbContext.GetOrganizer(user, organizerId);

			if (organizer == null)
				return NotFound();

			Document? document = dbContext
								 .Documents
								 .Where(x => x.OrganizerId == organizerId && x.Id == documentId)
								 .Include(x => x.Tags.Where(x => x.Name == tagText && x.OrganizerId == organizerId))
								 .AsSplitQuery()
								 .ToArray()
								 .FirstOrDefault();

			if (document == null)
				return NotFound();
			else if (document.Tags.Count > 0)
				return Conflict();

			Tag? tag = dbContext
					   .Tags
					   .Where(x => x.Name == tagText && x.OrganizerId == organizerId)
					   .ToArray()
					   .FirstOrDefault();

			if (tag == null)
			{
				tag = new Tag()
				{
					Name = tagText,
					Organizer = organizer
				};
			}

			tag.Documents.Add(document);
			document.Tags.Add(tag);
			dbContext.Tags.Add(tag);

			await dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete("removeTag")]
		[Consumes("text/plain")]
		public async Task<ActionResult> RemoveTag(int organizerId, int documentId, [FromBody] string tagText)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId))
				return NotFound();

			Document? document = dbContext
								 .Documents
								 .Where(x => x.OrganizerId == organizerId && x.Id == documentId)
								 .Include(x => x.Tags.Where(x => x.Name == tagText && x.OrganizerId == organizerId))
								 .ThenInclude(x => x.Documents)
								 .AsSplitQuery()
								 .ToArray()
								 .FirstOrDefault();

			if (document == null || document.Tags.Count == 0)
				return NotFound();

			Tag tag = document.Tags.First();

			tag.Documents.Remove(document);
			document.Tags.Remove(tag);

			if (tag.Documents.Count == 0)
				dbContext.Tags.Remove(tag);

			await dbContext.SaveChangesAsync();

			return Ok();
		}
	}
}
