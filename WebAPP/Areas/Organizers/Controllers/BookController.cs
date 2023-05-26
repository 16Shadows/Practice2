using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;
using WebAPP.Areas.Identity.Data;

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
		private readonly UserManager<UserAccount> userManager;

		public BookController(WebAPPContext context, UserManager<UserAccount> _userManager)
        {
            _context = context;
			userManager = _userManager;
		}

        [Authorize]
        [HttpGet("{bookId:int}")]
        public async Task<ActionResult<DocumentContentVM>> GetBook(int organizerId, int bookId)
        {
            var user = await userManager.GetUserAsync(User);
            if (!_context.HasOrganizer(user, organizerId))
            {
				return NotFound();
			}
                
            var book = _context.Books
                .Where(b => b.ParentOrganizerId == organizerId && b.Id == bookId)
                .Include(b => b.PageDMOs).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

			return Ok(Json(book));
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

			return Json(book);
		}

        // not implemented
		//[Authorize]
		//[HttpPost]
  //      public async Task<ActionResult<Book>> PostBook(Book book)
  //      {
  //        if (_context.Books == null)
  //        {
  //            return Problem("Entity set 'WebAPPContext.Books'  is null.");
  //        }
  //          _context.Books.Add(book);
  //          await _context.SaveChangesAsync();

  //          return CreatedAtAction("GetBook", new { id = book.Id }, book);
  //      }

        // not implemented
		//[Authorize]
		//[HttpDelete("{bookId:int}")]
  //      public async Task<IActionResult> DeleteBook(int id)
  //      {
  //          if (_context.Books == null)
  //          {
  //              return NotFound();
  //          }
  //          var book = await _context.Books.FindAsync(id);
  //          if (book == null)
  //          {
  //              return NotFound();
  //          }

  //          _context.Books.Remove(book);
  //          await _context.SaveChangesAsync();

  //          return NoContent();
  //      }
		private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
	}
}
