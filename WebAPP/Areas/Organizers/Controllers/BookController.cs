using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using static WebAPP.Areas.Organizers.Controllers.PageController;

namespace WebAPP.Areas.Organizers.Controllers
{
    [Area("Organizers")]
    [Route("{area}/Book")]
    [ApiController]
    public class BookController : Controller
    {
        class BooksPayload
        {
            // Class to wrap fetch data and additional info for views,
            // will be converted into json object
            public BooksPayload(List<Book> books)
            {
                Books = books;
            }
            public List<Book> Books { get; }
        }

        private readonly WebAPPContext _context;

        public BookController(WebAPPContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult Index(int id)
        {
            //// test
			//var b = new Book()
			//{
			//	Name = "test",
			//	ParentCategory = _context.Organizers.First(),
			//	ParentCategoryId = _context.Organizers.First().Id
			//};
			//_context.Books.Add(b);
			//_context.SaveChangesAsync();


			if (!BookExists(id)) // need better check?
            {
                return NotFound();
            }
			ViewData["BookID"] = id;

            // will need to make it partial and return object itself
			return View("Book");
        }

		[Authorize]
		[HttpGet("tablelist")]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooklist()
		{
			if (_context.Books == null)
			{
				return NotFound();
			}
			var data = await _context.Books.ToListAsync();
			return Json(new BooksPayload(data));
		}

		[Authorize]
        [HttpGet("{bookId:int}/content")]
        public async Task<ActionResult<IEnumerable<PageDMO>>> GetBookContent(int bookId)
        {
            if (!_context.Books.Any(e => e.Id == bookId))
            {
                return NotFound();
            }
            var book = await _context.Books.Where(b => b.Id == bookId)
                .Include(b => b.PageDMOs).FirstAsync();

			var j = Json(book);

			return Json(new BooksPayload(new List<Book>() { book }));
		}

        // not implemented
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'WebAPPContext.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

		[Authorize]
		[HttpDelete("{bookId:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
		private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // test
		[Authorize]
		[HttpGet("table")]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			if (_context.Books == null)
			{
				return NotFound();
			}
			var data = await _context.Books.ToListAsync();
			//return Json(new BooksPayload(data));
			return View("_BookTable");
		}
	}
}
