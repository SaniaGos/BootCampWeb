using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
	internal class Address
	{
		[Key]
		public int Id { get; set; }

		[StringLength(127)]
		public string? City { get; set; }

		[StringLength(127)]
		public string? Country { get; set; }

		[StringLength(127)]
		public string? Street { get; set; }
		
		[Required]
		public string? Index { get; set; }
	}
}
