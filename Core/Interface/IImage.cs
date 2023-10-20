using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
	public interface IImage
	{
		string? Name { get; set; }
		byte[]? Data { get; set; }
		DateTime UpdateDate { get; set; }
	}
}
