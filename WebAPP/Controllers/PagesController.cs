using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using WebAPP;

namespace WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly WebAPPContext _context;

        public PagesController(WebAPPContext context)
        {
            _context = context;
        }

        // GET: api/Pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageDMO>>> GetPages()
        {
          if (_context.Pages == null)
          {
              return NotFound();
          }
            return await _context.Pages.ToListAsync();
        }

        // GET: api/Pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageDMO>> GetPageDMO(int id)
        {
          if (_context.Pages == null)
          {
              return NotFound();
          }
            var pageDMO = await _context.Pages.FindAsync(id);

            if (pageDMO == null)
            {
                return NotFound();
            }

            return pageDMO;
        }

        // PUT: api/Pages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageDMO(int id, PageDMO pageDMO)
        {
            if (id != pageDMO.Id)
            {
                return BadRequest();
            }

            _context.Entry(pageDMO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageDMOExists(id))
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

        // POST: api/Pages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PageDMO>> PostPageDMO(PageDMO pageDMO)
        {
          if (_context.Pages == null)
          {
              return Problem("Entity set 'DMOrganizerDBContext.Pages'  is null.");
          }
            _context.Pages.Add(pageDMO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPageDMO", new { id = pageDMO.Id }, pageDMO);
        }

        // DELETE: api/Pages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageDMO(int id)
        {
            if (_context.Pages == null)
            {
                return NotFound();
            }
            var pageDMO = await _context.Pages.FindAsync(id);
            if (pageDMO == null)
            {
                return NotFound();
            }

            _context.Pages.Remove(pageDMO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PageDMOExists(int id)
        {
            return (_context.Pages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
