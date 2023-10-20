using Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System;
using System.Text.Json;
using System.Collections.Generic;
using Core.Entity;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace BootCampWeb.Pages
{
    public class UsersModel : PageModel
    {
        protected readonly IUserService _userService;
        private const string userKey_configName = "UserKey";
        private readonly IMemoryCache _memoryCache;

        public UsersModel(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        public IEnumerable<UserModel>? Users { get; set; }

        public class MyUsersModel
        {
            public List<UserModel> Users { get; set; }
            public int Total { get; set; }
            public int Skip { get; set; }
            public int Limit { get; set; }
        }

        public class Address
        {
            public string MyAddress { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string State { get; set; }
        }

        public class UserModel
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MaidenName { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string BirthDate { get; set; }
            public string Image { get; set; }
            public string BloodGroup { get; set; }
            public int Height { get; set; }
            public double Weight { get; set; }
            public string EyeColor { get; set; }
            public string Domain { get; set; }
            public string Ip { get; set; }
            public Address Address { get; set; }
            public string MacAddress { get; set; }
            public string University { get; set; }
            public string Ein { get; set; }
            public string Ssn { get; set; }
            public string UserAgent { get; set; }
        }

        public class UserComparer : IEqualityComparer<UserModel>
        {
            public bool Equals(UserModel? x, UserModel? y)
            {
                return x.Email.Equals(y.Email, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode([DisallowNull] UserModel obj)
            {
                return obj.Email.GetHashCode();
            }
        }
        class SimpleUser
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }


        public async Task<IActionResult> OnGet()
        {
            var users = await GetUsers();
            Users = users.Users.ToList();
            
			return Page();
        }

		private async Task<MyUsersModel?> GetUsers()
        {
            if (!_memoryCache.TryGetValue(userKey_configName, out MyUsersModel? users))
            {
                using var client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync("https://dummyjson.com/users?limit=100");

                var str = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<MyUsersModel>(str, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                users = result;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _memoryCache.Set(userKey_configName, users, cacheEntryOptions);

            }
            return users;
        }
    }
}
