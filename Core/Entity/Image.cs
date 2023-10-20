using Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
	public class Image : IImage
	{
		public string? Name { get; set; }
		public byte[]? Data { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
