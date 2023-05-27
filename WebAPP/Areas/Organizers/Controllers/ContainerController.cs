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
	[Route("{area}/Container")]
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
        public async Task<ActionResult<ContainerDMO>> GetContainerDMO(int contId)
        {
            if (_context.Containers == null)
            {
                return NotFound();
            }
            var containerDMO = _context.Containers.Where(c => c.ID == contId).Include(c => c.ObjectDMOs).First();

            if (containerDMO == null)
            {
                return NotFound();
            }

            return Json(containerDMO);
		}

		// POST new default container in the page by pageID
		[Authorize]
		[HttpPost("create/{pageId:int}")]
		public async Task<ActionResult<PageDMO>> PostContainerDMO(int pageId)
		{
			var ok = _context.Pages.Find(pageId);
			if (ok == null) //check that page exists
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
                ParentPage = _context.Pages.First(p => p.Id == pageId)
            };

			await _context.Containers.AddAsync(cont);

			await _context.SaveChangesAsync();

			// get instance of new page with id
			var newCont = _context.Containers.Where(p => p.ParentPageId == pageId)
                .Include(c => c.ObjectDMOs)
				.First();
			var j = Json(new ContainersPayload(new List<ContainerDMO>() { newCont }));
			return Accepted(j);
		}

		// DELETE
		[Authorize]
		[HttpDelete("delete/{containerId:int}")]
		public async Task<IActionResult> DeleteContainerDMO(int containerId)
		{
			if (_context.Containers == null)
			{
				return NotFound();
			}
			var containerDMO = await _context.Containers.FindAsync(containerId);
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
		[HttpPut("update/{containerID:int}")]
		public async Task<ActionResult<PageDMO>> UpdateContainerDMO(int containerID, [FromBody] ContentData newData)
		{
			var cont = _context.Containers.Find(containerID);
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
            return (_context.Containers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
