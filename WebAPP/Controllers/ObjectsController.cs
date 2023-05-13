using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMOrganizerDomainModel;
using WebAPP;

namespace WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsController : ControllerBase
    {
        private readonly DMOrganizerDBContext _context;

        public ObjectsController(DMOrganizerDBContext context)
        {
            _context = context;
        }

        // GET: api/Objects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObjectDMO>>> GetObjects()
        {
          if (_context.Objects == null)
          {
              return NotFound();
          }
            return await _context.Objects.ToListAsync();
        }

        // GET: api/Objects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectDMO>> GetObjectDMO(int id)
        {
          if (_context.Objects == null)
          {
              return NotFound();
          }
            var objectDMO = await _context.Objects.FindAsync(id);

            if (objectDMO == null)
            {
                return NotFound();
            }

            return objectDMO;
        }

        // PUT: api/Objects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObjectDMO(int id, ObjectDMO objectDMO)
        {
            if (id != objectDMO.Id)
            {
                return BadRequest();
            }

            _context.Entry(objectDMO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectDMOExists(id))
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

        // POST: api/Objects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ObjectDMO>> PostObjectDMO(ObjectDMO objectDMO)
        {
          if (_context.Objects == null)
          {
              return Problem("Entity set 'DMOrganizerDBContext.Objects'  is null.");
          }
            _context.Objects.Add(objectDMO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObjectDMO", new { id = objectDMO.Id }, objectDMO);
        }

        // DELETE: api/Objects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjectDMO(int id)
        {
            if (_context.Objects == null)
            {
                return NotFound();
            }
            var objectDMO = await _context.Objects.FindAsync(id);
            if (objectDMO == null)
            {
                return NotFound();
            }

            _context.Objects.Remove(objectDMO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObjectDMOExists(int id)
        {
            return (_context.Objects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
