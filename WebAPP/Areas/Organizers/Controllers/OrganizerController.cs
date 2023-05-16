using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.Areas.Organizers.Controllers
{
    [Area("Organizers")]
    [Route("{area}/Organizer")]
    [Route("")]
    public class OrganizerController : Controller
    {
        class OrganizersPayload
        {
            public OrganizersPayload(List<Organizer> organizers)
            {
                Organizers = organizers;
            }

            public List<Organizer> Organizers { get; }
        }

        private readonly UserManager<UserAccount> userManager;

        public OrganizerController(UserManager<UserAccount> manager)
        {
            userManager = manager;
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Organizers");
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Organizer>>> List()
        {
            var user = await userManager.GetUserAsync(User);
            /*
            return Json(user.Organizers);
            */

            Organizer dummy = new Organizer()
            {
                Name = "Test"
            };
            Organizer dummy2 = new Organizer()
            {
                Name = "Test2"
            };

            return Json( new OrganizersPayload( new List<Organizer>() { dummy, dummy2 }) );
        }
    }
}
