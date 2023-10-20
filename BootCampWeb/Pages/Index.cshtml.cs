using Core.Entity;
using Core.Interface;
using Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;

namespace BootCampWeb.Pages
{
	public class IndexModel : PageModel
	{
		protected readonly IUserService _userService;

		public IndexModel(IUserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		public UserModel? Author { get; set; }

		public Result? Responce { get; set; }

		public enum ResultType
		{
			Success,
			Error
		}
		public class Result
		{
			public ResultType ResultType { get; set; }
			public string? Message { get; set; }
		}


		public class UserModel
		{
			[HiddenInput]
			public Guid Id { get; set; }

			[Required]
			[StringLength(50, MinimumLength = 3)]
			[Display(Name = "Name", Prompt = "Enter your name")]
			public string? Name { get; set; }

			[Required]
			[DataType(DataType.EmailAddress)]
			[Display(Name = "Email", Prompt = "Enter your email")]
			public string? Email { get; set; }

			[Required]
			[Range(18, 99)]
			[Display(Name = "Age", Prompt = "Enter your age")]
			public int Age { get; set; }

			[Required]
			[MinLength(8)]
			[MaxLength(16)]
			[DataType(DataType.Password)]
			[Display(Name = "Password", Prompt = "Enter your password")]
			[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
			public string? Password { get; set; }

			[Required]
			[DataType(DataType.Password)]
			[Compare("Password", ErrorMessage = "Passwords must be equals")]
			[Display(Name = "Confirm Password", Prompt = "Confirm your password")]
			public string? ConfirmPassword { get; set; }
		}


		public IActionResult OnGet()
		{
			return Page();
		}

		public void OnPost()
		{
			ModelState.Remove("Author.Id");
			if (!string.IsNullOrEmpty(Author?.Email) && _userService.IsEmailUsed(Author.Email))
			{
				ModelState.AddModelError("Author.E5mail", "This email can't be used");
			}

			if (!ModelState.IsValid)
			{
				return;
			}

			if (Author != null)
			{
				var user = new User()
				{
					Name = Author.Name,
					Age = Author.Age,
					Email = Author.Email,
					Password = Author.Password
				};

				if (_userService.CreateUser(user))
				{
					Responce = new Result() { ResultType = ResultType.Success, Message = $"User {Author.Name} has been created" };
				}
				else
				{
					Responce = new Result() { ResultType = ResultType.Error, Message = $"User {Author.Name} has not been created" };
				}
			}
			else
			{
				Responce = new Result() { ResultType = ResultType.Error, Message = $"User has not been created" };
			}
		}

		public IActionResult OnPostCheckEmail(string email)
		{
			return new JsonResult(_userService.IsEmailUsed(email));
		}
	}
}