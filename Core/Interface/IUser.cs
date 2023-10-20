using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
	public interface IUser
	{
		Guid Id { get; set; }
		string? Name { get; set; }
		string? Email { get; set; }
		int Age { get; set; }
		DateTime CreateDate { get; set; }
		string? Password { get; set; }
	}
}
