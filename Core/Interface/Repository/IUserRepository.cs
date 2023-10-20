using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	public interface IUserRepository
	{
		IUser? GetUserById(Guid id);
		IUser? GetUserByEmail(string email);
		IEnumerable<IUser?> GetAllUsers();
		bool CreateUser(IUser user);
	}
}
