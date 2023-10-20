using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
	public class User : IUser
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public int Age { get; set; }
		public DateTime CreateDate { get; set; }
		public string? Password { get; set; }
	}
}
