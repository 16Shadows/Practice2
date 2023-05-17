using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;

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
        [HttpGet("")]
		public IActionResult Index()
        {
            return View("Book");
        }

		// GET: api/Books
		[Authorize]
		[HttpGet("table")]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			if (_context.Books == null)
			{
				return NotFound();
			}
			var data = await _context.Books.ToListAsync();
			return Json(new BooksPayload(data));
		}

		// GET: api/Books/5
		[Authorize]
        public async Task<ActionResult<Book>> GetBook(int id)
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
            return Json(new BooksPayload(new List<Book>() { book }));
		}

		// PUT: api/Books/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

		// POST: api/Books
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

		// DELETE: api/Books/5
		[Authorize]
		[HttpDelete("{id}")]
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
    }
}
