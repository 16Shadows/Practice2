using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly WebAPPContext _context;

        public ContainerController(WebAPPContext context)
        {
            _context = context;
        }

        // GET: api/Containers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContainerDMO>>> GetContainers()
        {
            if (_context.Containers == null)
            {
                return NotFound();
            }
            return await _context.Containers.ToListAsync();
        }

        // GET: api/Containers/5
        [HttpGet("{id}")]
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

            return containerDMO;
        }

        // PUT: api/Containers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContainerDMO(int id, ContainerDMO containerDMO)
        {
            if (id != containerDMO.ID)
            {
                return BadRequest();
            }

            _context.Entry(containerDMO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerDMOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Containers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContainerDMO>> PostContainerDMO(ContainerDMO containerDMO)
        {
            if (_context.Containers == null)
            {
                return Problem("Entity set 'DMOrganizerDBContext.Containers'  is null.");
            }
            _context.Containers.Add(containerDMO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContainerDMO", new { id = containerDMO.ID }, containerDMO);
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
