using Core;
using Core.Enum;
using Core.Helper;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BootCampWeb.Pages
{
	public class ImageModel : PageModel
	{
		protected readonly IImageService _imageService;
		public Day Day { get; set; }

		public ImageModel(IImageService imageService)
		{
			_imageService = imageService;
		}

		public async Task<IActionResult> OnGet(int? id, string? name)
		{
			var byteArr = await _imageService.GetImage(id, name);
						
			return File(byteArr, $"image/{name?.Split(".")?.Last()}");
		}
	}
}
