using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
	[Index(nameof(Name), IsUnique = true)]
	internal class Image : IImage
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string? Name { get; set; }
		[Required]
		public byte[]? Data { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
