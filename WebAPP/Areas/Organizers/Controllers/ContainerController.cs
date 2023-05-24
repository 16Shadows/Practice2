using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using static WebAPP.Areas.Organizers.Controllers.PageController;

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
		[HttpGet("{id:int}")]
        public async Task<ActionResult<ContainerDMO>> GetContainerDMO(int id)
        {
            if (_context.Containers == null)
            {
                return NotFound();
            }
            var containerDMO = await _context.Containers.FindAsync(id);

            if (containerDMO == null)
            {
                return NotFound();
            }

            return Json(new ContainersPayload(new List<ContainerDMO>() { containerDMO }));
		}

		// POST new default container in the page by pageID
		[Authorize]
		[HttpPost("create/{pageId:int}")]
		public async Task<ActionResult<PageDMO>> PostPageDMO(int pageId)
		{
			var ok = _context.Pages.Find(pageId);
			if (ok == null) //check that book exists
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

        // DELETE: api/Containers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContainerDMO(int id)
        {
            if (_context.Containers == null)
            {
                return NotFound();
            }
            var containerDMO = await _context.Containers.FindAsync(id);
            if (containerDMO == null)
            {
                return NotFound();
            }

            _context.Containers.Remove(containerDMO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContainerDMOExists(int id)
        {
            return (_context.Containers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
