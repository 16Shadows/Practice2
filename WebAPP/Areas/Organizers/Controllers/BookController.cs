using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Organizers.Data;
using WebAPP.Areas.Organizers.Models;
using WebAPP.Areas.Identity.Data;
using static WebAPP.Areas.Organizers.Controllers.PageController;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static WebAPP.Areas.Organizers.Controllers.ContainerController;

namespace WebAPP.Areas.Organizers.Controllers
{
    [Area("Organizers")]
    [Route("{area}/{organizerId:int:required}/Book")]
    [ApiController]
    public class BookController : Controller
    {

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
                .Where(b => b.OrganizerId == organizerId && b.Id == bookId)
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

			return Ok(Json(book));
        }

		[Authorize]
        [HttpGet("{bookId:int}/content")]
        public async Task<ActionResult<IEnumerable<PageDMO>>> GetBookContent(int organizerId, int bookId)
        {
            if (!_context.Books.Any(e => e.Id == bookId))
            {
                return NotFound();
            }
            var book = await _context.Books
                .Where(b => b.OrganizerId == organizerId && b.Id == bookId)
                .Include(b => b.PageDMOs)
                .AsSplitQuery()
                .FirstAsync();

			return Json(book);
		}

		// UPDATE container by containerID
		[Authorize]
		[HttpPut("rename/{bookId:int}")]
		public async Task<ActionResult<Book>> RenameBook(int organizerId, int bookId, [FromBody] string newName)
		{
			var book = _context.Books
                .Where(b => b.OrganizerId == organizerId && b.Id == bookId)
                .FirstOrDefault();
			if (book == null)
			{
				return NotFound();
			}

			book.Name = newName;
			
			await _context.SaveChangesAsync();

			return Ok();
		}


		[Authorize]
        [HttpDelete("delete/{bookId:int}")]
        public async Task<IActionResult> DeleteBook(int organizerId, int bookId)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Where(x => x.OrganizerId == organizerId && x.Id == bookId)
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

			return Accepted();
		}
        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
	}
}
