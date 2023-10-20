using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
	public class MyAppDbContext : DbContext
	{
		internal DbSet<User> Users { get; set; }
		internal DbSet<Image> Images { get; set; }


		public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
			: base(options)
		{
		}
	}
}