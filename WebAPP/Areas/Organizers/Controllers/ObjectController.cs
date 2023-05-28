using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPP.Areas.Organizers.Data;
using static WebAPP.Areas.Organizers.Controllers.OrganizersController;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}/Object")]
	[ApiController]
    public class ObjectController : Controller
    {
		class ObjectsPayload
		{
			// Class to wrap fetch data and additional info for views,
			// will be converted into json object
			public ObjectsPayload(List<ObjectDMO> objects)
			{
				Objects = objects;
			}
			public List<ObjectDMO> Objects { get; }
		}
		private readonly WebAPPContext _context;

        public ObjectController(WebAPPContext context)
        {
            _context = context;
        }

		[Authorize]
		[HttpGet("{objectId:int}/content")]
        public async Task<ActionResult<ObjectDMO>> GetObjectDMO(int organizerId, int objectId)
        {
            if (_context.Objects == null)
            {
                return NotFound();
            }
            var objectDMO = _context.Objects
                .Where(o => o.OrganizerId == organizerId && o.Id == objectId)
				.FirstOrDefault();

            if (objectDMO == null)
            {
                return NotFound();
            }

            return objectDMO;
        }


		[Authorize]
		[HttpPost("create/{containerId:int}")]
		public async Task<ActionResult<ObjectDMO>> CreateObjectDMO(int organizerId, int containerId, [FromBody] string link)
        {
			var cont = _context.Containers
				.Where(c => c.OrganizerId == organizerId && c.Id == containerId)
				.FirstOrDefault();
			if (cont == null) //check that parent container exists
			{
				return NotFound();
			}

            // create new object with parent
            ObjectDMO obj = new ObjectDMO()
            {
                ParentContainerId = containerId,
                ParentContainer = cont,
                Organizer = cont.Organizer,
                OrganizerId = cont.OrganizerId,
                LinkToObject = link
            };

			await _context.Objects.AddAsync(obj);

			await _context.SaveChangesAsync();

			// get instance of new object with id
			var newObj = _context.Objects.Where(p => p.ParentContainerId == containerId)
				.Include(c => c.LinkToObject)
				.First();

			var j = Json(newObj);
			return Accepted(j);
		}

		// DELETE
		[Authorize]
		[HttpDelete("delete/{objectId:int}")]
		public async Task<IActionResult> DeleteObjectDMO(int organizerId, int objectId)
        {
            if (_context.Objects == null)
            {
                return NotFound();
            }
            var objectDMO = _context.Objects
				.Where(o => o.OrganizerId == organizerId && o.Id == objectId)
				.FirstOrDefault();

			if (objectDMO == null)
            {
                return NotFound();
            }

            _context.Objects.Remove(objectDMO);
            await _context.SaveChangesAsync();

			return Accepted();
		}

        private bool ObjectDMOExists(int id)
        {
            return (_context.Objects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
