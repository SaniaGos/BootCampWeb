using Microsoft.AspNetCore.Mvc;

namespace BootCampWeb.Controllers
{
	public class UserController : Controller
	{
		[HttpGet()]
		public IActionResult Index(int id)
		{
			return new OkResult();
		}

		[HttpGet()]
		public IActionResult Edit(int id)
		{
			return new OkResult();
		}
	}
}
