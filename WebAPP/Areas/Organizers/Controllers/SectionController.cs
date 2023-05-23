using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Authorize]
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}/section/{sectionId:int:required}")]
	public class SectionController : Controller
	{
		private readonly WebAPPContext dbContext;
		private readonly UserManager<UserAccount> userManager;

		public SectionController(WebAPPContext dbContext, UserManager<UserAccount> userManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
		}

		[Route("")]
		public async Task<ActionResult<SectionContentVM>> Index(int organizerId, int sectionId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId))
				return NotFound();

			Section? section = dbContext
							   .Sections
							   .Where(x => x.OrganizerId == organizerId && x.Id == sectionId)
							   .Include(x => x.Sections)
							   .ToArray()
							   .FirstOrDefault();

			if (section == null)
				return NotFound();

			return Ok(new SectionContentVM(section));
		}

		[HttpPost("rename/{name:required:minlength(1)}")]
		public async Task<ActionResult<SectionVM>> RenameSection(int organizerId, int sectionId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Section? section = dbContext.GetSection(organizerId, sectionId);

			if (section == null)
				return NotFound();

			bool any = dbContext
					   .Sections
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == section.ParentId && x.Id != sectionId && x.Title == name)
					   .Include(x => x.Parent)
					   .ToArray()
					   .Any(x => x.Parent.Equals(section.Parent));
			
			if (any)
				return Conflict();

			section.Title = name;

			await dbContext.SaveChangesAsync();
			
			return Ok( new SectionVM(section) );
		}

		[HttpPost("createSection/{name:required:minlength(1)}")]
		public async Task<ActionResult<SectionVM>> CreateSection(int organizerId, int sectionId, string name)
		{
			var user = await userManager.GetUserAsync(User);

			Organizer? organizer = dbContext.GetOrganizer(user, organizerId);

			if (organizer == null) 
				return NotFound();

			Section? parent = dbContext.GetSection(organizerId, sectionId);

			if (parent == null)
				return NotFound();

			bool any = dbContext
					   .Sections
					   .Where(x => x.OrganizerId == organizerId && x.ParentId == parent.ParentId && x.Title == name)
					   .Include(x => x.Parent)
					   .ToArray()
					   .Any(x => x.Parent.Equals(parent));
			
			if (any)
				return Conflict();

			Section section = new Section()
			{
				Title = name,
				Organizer = organizer,
				Parent = parent
			};

			dbContext.Sections.Add(section);

			await dbContext.SaveChangesAsync();
			
			return Ok( new SectionVM(section) );
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<SectionVM>> DeleteSection(int organizerId, int sectionId)
		{
			var user = await userManager.GetUserAsync(User);

			if (!dbContext.HasOrganizer(user, organizerId)) 
				return NotFound();

			Section? section = dbContext.GetSection(organizerId, sectionId);

			if (section == null)
				return NotFound();

			SectionVM vm = new SectionVM(section);

			dbContext.Sections.Remove(section);

			await dbContext.SaveChangesAsync();

			return Ok(vm);
		}
	}
}
