using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Areas.Identity.Data;
using WebAPP.Areas.Organizers.Data;
using System.Web;

namespace WebAPP.Areas.Organizers.Controllers
{
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

        [Authorize]
        [HttpPost("create/{name:required:minlength(1)}")]
        public async Task<ActionResult<OrganizerVM>> Create(string name)
        {
            var user = await userManager.GetUserAsync(User);

            bool any = dbContext
                       .Organizers
                       .Any(x => x.OwnerId == user.Id && x.Name == name);

            if (any)
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

        [Authorize]
        [HttpDelete("{id:int:required}/delete")]
        public async Task<ActionResult<OrganizerVM>> Delete(int id)
        {
            var user = await userManager.GetUserAsync(User);

            bool any = dbContext.Organizers.Any(x => x.OwnerId == user.Id && x.Id == id);

            if (!any)
                return NotFound();

            Organizer org = new Organizer()
            {
                Id = id,
                OwnerId = user.Id
            };
            OrganizerVM vm = new OrganizerVM(org);

            dbContext.Organizers.Remove(org);
            
            await dbContext.SaveChangesAsync();

            return Ok(vm);
        }

        [Authorize]
        [HttpPost("{id:int:required}/rename/{name:required:minlength(1)}")]
        public async Task<ActionResult<OrganizerVM>> Rename(int id, string name)
        {
            var user = await userManager.GetUserAsync(User);

            Organizer? target = dbContext
                                .Organizers
                                .FirstOrDefault(x => x.OwnerId == user.Id && x.Id == id);

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
