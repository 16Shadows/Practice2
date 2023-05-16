using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{id:int:required}")]
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
		public async Task<IActionResult> Index(int id)
		{
			var user = await userManager.GetUserAsync(User);

			bool any = dbContext
					   .Organizers
					   .Any(x => x.OwnerId == user.Id && x.Id == id);

			if (!any)
				return NotFound();

			return View("Organizer");
		}
	}
}
