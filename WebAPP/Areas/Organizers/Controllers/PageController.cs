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

        // get page object and its containers
		[Authorize]
		[HttpGet("{pageId:int}/content")]
        public async Task<ActionResult<PageDMO>> GetPageContent(int pageId)
        {
			if (!_context.Pages.Any(e => e.Id == pageId))
			{
				return NotFound();
			}
			var page = _context.Pages.Where(p => p.Id == pageId)
				.Include(p => p.ContainerDMOs)
				.FirstOrDefault();
			
			return Json(page);
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
				ParentBook = _context.Books.First(b => b.Id == bookId)
			};

			await _context.Pages.AddAsync(pg);

			await _context.SaveChangesAsync();

			// get instance of new page with id
			var newPage = await _context.Pages.Where(p => p.ParentBookId == bookId)
				.Where(p => p.Position == position)
				.Include(p => p.ContainerDMOs)
				.FirstAsync();

			var j = Json(newPage);
			return Accepted(j);
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

			// update all pages
			await ChangePagePositionsAfterDelete(bookId, pos);

			var list = await _context.Pages.Where(p => p.ParentBookId == bookId)
				.OrderBy(p => p.Position)
				.ToListAsync();

			return Accepted(new PagesPayload(list));
		}
		// Changes all pages' positions in book after deleted one
		private async Task ChangePagePositionsAfterDelete(int bookId, int borderPosition)
		{
			var list = await _context.Pages.Where(p => p.ParentBookId == bookId)
				.Where(p => p.Position > borderPosition).ToListAsync();

			foreach (var page in list)
			{
				page.Position -= 1;
			}
			// send changes to server
			await _context.SaveChangesAsync();
		}

        private bool PageDMOExists(int id)
        {
            return (_context.Pages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
