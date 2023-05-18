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
		public class PagesPayload
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

        // create blanc view of page with page ID
		[Authorize]
		[HttpGet("{pageId:int}")]
		public IActionResult Index(int pageId)
		{
			if (!PageDMOExists(pageId))
			{
				return NotFound();
			}
            ViewData["PageID"] = pageId;

			return View("Page");
		}

        // get page object and its containers
		[Authorize]
		[HttpGet("{pageId:int}/content")]
        public async Task<ActionResult<PageDMO>> GetPageContent(int pageId)
        {
			if (!_context.Pages.Any(e => e.Id == pageId))
			{
				return NotFound();
			}
			var page = await _context.Pages.Where(p => p.Id == pageId)
				.Include(p => p.ContainerDMOs).FirstAsync();
			
            var j = Json(page);
			return j;
		}

		// POST new page to the book by bookID
		[Authorize]
		[HttpPost("create/{bookId:int}/{position:int}")]
		public async Task<ActionResult<PageDMO>> PostPageDMO(int bookId, int position)
		{
			var ok = _context.Books.Find(bookId);
			if (ok == null) //check that book exists
			{
				return NotFound();
			}

			// create new page with parent
			PageDMO pg = new PageDMO()
			{
				Position = position,
				ParentBookId = bookId,
				ParentBook = _context.Books.First(b => b.Id == bookId),

			};

			await _context.Pages.AddAsync(pg);

			await _context.SaveChangesAsync();

			// get instance of new page with id
			var newPage = _context.Pages.Where(p => p.ParentBookId == bookId)
				.Where(p => p.Position == position).First();

			return Accepted(new PagesPayload(new List<PageDMO>() { newPage }));
		}

		// DELETE
		[Authorize]
		[HttpDelete("delete/{bookId:int}/{pageId:int}")]
		public async Task<IActionResult> DeletePageDMO(int bookId, int pageId)
		{
			if (_context.Pages == null)
			{
				return NotFound();
			}
			var pageDMO = await _context.Pages.FindAsync(pageId);
			if (pageDMO == null)
			{
				return NotFound();
			}
			var pos = pageDMO.Position;

			_context.Pages.Remove(pageDMO);
			await _context.SaveChangesAsync();
			// wait for all pages to update
			await ChangePagePositionsAfterDelete(bookId, pos);

			return Accepted();
		}
		// Changes all pages' positions in book after deleted one
		private async Task ChangePagePositionsAfterDelete(int bookId, int borderPosition)
		{
			var list = _context.Pages.Where(p => p.ParentBookId == bookId)
				.Where(p => p.Position > borderPosition).ToList();

			foreach (var page in list)
			{
				page.Position -= 1;
			}
		}

		[Authorize]
		[HttpPut("{pageId:int}")]
        public async Task<IActionResult> PutPageDMO(int pageId, PageDMO pageDMO)
        {
            if (pageId != pageDMO.Id)
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
                if (!PageDMOExists(pageId))
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
        private bool PageDMOExists(int id)
        {
            return (_context.Pages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
