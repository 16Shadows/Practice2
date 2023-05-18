using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using System.Web;

namespace WebAPP.Areas.Organizers.Controllers
{
    [Authorize]
    [Area("Organizers")]
    [Route("{area}")]
    public class OrganizersController : Controller
    {
        public class OrganizerVM
        {
			public OrganizerVM(Organizer org)
			{
                if (org == null)
                    throw new ArgumentNullException(nameof(org));

				Id = org.Id;
				Name = org.Name;
			}

			public int Id { get; }
            public string Name { get; }
        }

        public class OrganizersPayload
        {
            public OrganizersPayload(IEnumerable<OrganizerVM> organizers)
            {
                Organizers = organizers;
            }

            public IEnumerable<OrganizerVM> Organizers { get; }
        }

        private readonly UserManager<UserAccount> userManager;
        private readonly WebAPPContext dbContext;

        public OrganizersController(WebAPPContext context, UserManager<UserAccount> manager)
        {
            userManager = manager;
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Organizers");
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Organizer>>> List()
        {
            var user = await userManager.GetUserAsync(User);

            return Json(
                new OrganizersPayload( 
                    dbContext
                    .Organizers
                    .Where(x => x.OwnerId == user.Id)
                    .OrderBy(x => x.Id)
                    .Select(x => new OrganizerVM(x))
                    .ToArray() 
                )
            );
        }

        [HttpPost("create/{name:required:minlength(1)}")]
        public async Task<ActionResult<OrganizerVM>> Create(string name)
        {
            var user = await userManager.GetUserAsync(User);

            if (dbContext.HasOrganizer(user, name))
                return Conflict();

            Organizer org = new Organizer()
            {
                Name = name,
                Owner = user
            };

            await dbContext.Organizers.AddAsync(org);

            await dbContext.SaveChangesAsync();

            return Accepted ( new OrganizerVM(org) );
        }

        [HttpDelete("{id:int:required}/delete")]
        public async Task<ActionResult<OrganizerVM>> Delete(int id)
        {
            var user = await userManager.GetUserAsync(User);

            Organizer? target = dbContext.GetOrganizer(user, id);

            if (target == null)
                return NotFound();

            OrganizerVM vm = new OrganizerVM(target);

            dbContext.Organizers.Remove(target);
            
            await dbContext.SaveChangesAsync();

            return Ok(vm);
        }

        [HttpPost("{id:int:required}/rename/{name:required:minlength(1)}")]
        public async Task<ActionResult<OrganizerVM>> Rename(int id, string name)
        {
            var user = await userManager.GetUserAsync(User);

            Organizer? target = dbContext.GetOrganizer(user, id);

            if (target == null)
                return NotFound();

            bool any = dbContext
                       .Organizers
                       .Any(x => x.OwnerId == user.Id && x.Name == name && x.Id != target.Id);

            if (any)
                return Conflict();

            target.Name = HttpUtility.HtmlEncode(name);

            await dbContext.SaveChangesAsync();

            return Ok(new OrganizerVM(target));
        }
    }
}
