using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using Newtonsoft.Json;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}/Container")]
	[ApiController]
    public class ContainerController : Controller
    {
		class ContainersPayload
		{
			// Class to wrap fetch data and additional info for views,
			// will be converted into json object
			public ContainersPayload(List<ContainerDMO> containers)
			{
				Containers = containers;
			}
			public List<ContainerDMO> Containers { get; }
		}
		private readonly WebAPPContext _context;

        public ContainerController(WebAPPContext context)
        {
            _context = context;
        }

		[Authorize]
		[HttpGet("{contId:int}/content")]
        public async Task<ActionResult<ContainerDMO>> GetContainerDMO(int organizerId, int contId)
        {
            if (_context.Containers == null)
            {
                return NotFound();
            }
            var containerDMO = _context.Containers
				.Where(c => c.OrganizerId == organizerId && c.Id == contId)
				.Include(c => c.ObjectDMOs)
				.AsSplitQuery()
				.FirstOrDefault();

            if (containerDMO == null)
            {
                return NotFound();
            }

            return Json(containerDMO);
		}

		// POST new default container in the page by pageID
		[Authorize]
		[HttpPost("create/{pageId:int}")]
		public async Task<ActionResult<PageDMO>> PostContainerDMO(int organizerId, int pageId)
		{
			var page = _context.Pages
				.Where(p => p.OrganizerId == organizerId && p.Id == pageId)
				.FirstOrDefault();
			if (page == null) //check that parent page exists
			{
				return NotFound();
			}

            // create new container with parent
            ContainerDMO cont = new ContainerDMO()
            {

                Type = 1,
                Width = 200,
                Height = 200,
                CoordX = 20,
                CoordY = 20,
                ParentPageId = pageId,
                ParentPage = _context.Pages.First(p => p.Id == pageId),
				Organizer = page.Organizer,
				OrganizerId = page.OrganizerId,
				ObjectDMOs = new List<ObjectDMO>()
            };

			await _context.Containers.AddAsync(cont);

			await _context.SaveChangesAsync();

			// get instance of new container with id
			var newCont = _context.Containers.Where(c => c.Id == cont.Id)
				.First();
			var j = Json(newCont);
			return Accepted(j);
		}

		// DELETE
		[Authorize]
		[HttpDelete("delete/{containerId:int}")]
		public async Task<IActionResult> DeleteContainerDMO(int organizerId, int containerId)
		{
			if (_context.Containers == null)
			{
				return NotFound();
			}

			var containerDMO = _context.Containers
				.Where(c => c.OrganizerId == organizerId && c.Id == containerId)
				.FirstOrDefault();

			if (containerDMO == null)
			{
				return NotFound();
			}

			_context.Containers.Remove(containerDMO);
			await _context.SaveChangesAsync();

			return Accepted();
		}
		public class ContentData
		{
			public int? Width { get; set; }
			public int? Height { get; set; }
			public int? CoordX { get; set; }
			public int? CoordY { get; set; }
			public int? Type { get; set; }
		}

		// UPDATE container by containerID
		[Authorize]
		[HttpPut("update/{containerId:int}")]
		public async Task<ActionResult<PageDMO>> UpdateContainerDMO(int organizerId, int containerId, [FromBody] ContentData newData)
		{
			var cont = _context.Containers
				.Where(c => c.OrganizerId == organizerId && c.Id == containerId)
				.FirstOrDefault();

			if (cont == null) //check that container exists
			{
				return NotFound();
			}

			if (newData.Width != null) { cont.Width = (int)newData.Width; }
			if (newData.Height != null) { cont.Height = (int)newData.Height; }
			if (newData.CoordX != null) { cont.CoordX = (int)newData.CoordX; }
			if (newData.CoordY != null) { cont.CoordY = (int)newData.CoordY; }
			if (newData.Type != null) { cont.Type = (int)newData.Type; }

			await _context.SaveChangesAsync();

			return Ok();
		}
		private bool ContainerDMOExists(int id)
        {
            return (_context.Containers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
