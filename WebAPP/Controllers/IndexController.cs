using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Controllers
{
	[Route("")]
	public class IndexController : Controller
	{
		[HttpGet("")]
		public IActionResult Index()
		{
			return LocalRedirect("/Organizers");
		}
	}
}
