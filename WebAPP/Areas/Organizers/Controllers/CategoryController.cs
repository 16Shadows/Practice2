using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Areas.Organizers.Controllers
{
	[Area("Organizers")]
	[Route("Organizers/{organizerId:int:required}/category/{categoryId:int:required}")]
	public class CategoryController : Controller
	{
		[HttpGet("")]
		public IActionResult Root()
		{
			return View();
		}
	}
}
