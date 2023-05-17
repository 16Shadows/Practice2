using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("{area}/Page")]
	[ApiController]
	public class PageController : Controller
    {
		class PagesPayload
		{
			// Class to wrap fetch data and additional info for views,
			// will be converted into json object
			public PagesPayload(List<PageDMO> pages)
			{
				Pages = pages;
			}
			public List<PageDMO> Pages { get; }
		}
		private readonly WebAPPContext _context;

        public PageController(WebAPPContext context)
        {
            _context = context;
        }
		[Authorize]
		[HttpGet("")]
		public IActionResult Index()
		{
			return View("Page");
		}

		// GET: api/Pages
		[Authorize]
		[HttpGet("table")]
		public async Task<ActionResult<IEnumerable<PageDMO>>> GetPages()
        {
            if (_context.Pages == null)
            {
                return NotFound();
            }
            return await _context.Pages.ToListAsync();
        }

		// GET: api/Pages/5
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
