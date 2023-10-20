using Core.Entity;
using Core.Interface;
using Core.Interface.Repository;
using Core.Interface.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
	public class UserService : IUserService
	{
		protected readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public IUser? GetUserById(Guid id)
		{
			return _userRepository.GetUserById(id);
		}

		public bool CreateUser(IUser user)
		{
			user.Id = Guid.NewGuid();
			user.CreateDate = DateTime.Now;
			return _userRepository.CreateUser(user);
		}

		public bool IsEmailUsed(string email)
		{
			return _userRepository.GetUserByEmail(email) != null;
		}

		public IEnumerable<IUser?> GetAllUsers()
		{
			return _userRepository.GetAllUsers(); 
		}
	}
}
