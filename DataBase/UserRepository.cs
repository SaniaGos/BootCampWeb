using Core.Interface;
using Core.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class UserRepository : IUserRepository
    {
        protected readonly MyAppDbContext _dbContext;
        private const string productsKey = "productsKey";
        private readonly IMemoryCache _memoryCache;

        public UserRepository(MyAppDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public IEnumerable<IUser?>? GetAllUsers()
        {
            if (!_memoryCache.TryGetValue(productsKey, out IEnumerable<IUser?>? users))
            {
                var usersFromDb = _dbContext.Users.Include(x => x.Address);
                var result = new List<IUser?>();
                foreach (var user in usersFromDb)
                {
                    result.Add(CastToIUser(user));
                }
                users = result;
                
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _memoryCache.Set(productsKey, users, cacheEntryOptions);

            }
            return users;
        }

        public IUser? GetUserById(Guid id)
        {
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return CastToIUser(user);
                }
            }
            return null;
        }

        public IUser? GetUserByEmail(string email)
        {
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user?.Email) && user.Email.ToLower().Equals(email.ToLower()))
                {
                    return CastToIUser(user);
                }
            }
            return null;
        }

        public bool CreateUser(IUser user)
        {
            try
            {
                _dbContext.Users.Add(new Models.User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Age = user.Age,
                    CreateDate = user.CreateDate,
                    Password = user.Password
                });

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private IUser? CastToIUser(Models.User user)
        {
            if (user == null) return null;

            return new Core.Entity.User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                CreateDate = user.CreateDate,
                Password = user.Password
            };
        }
    }
}
