using Core.Interface;
using Core.Interface.Repository;
using DataBase.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
	public class ImageRepository : IImageRepository
	{
		protected readonly MyAppDbContext _dbContext;

		public ImageRepository(MyAppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IImage? GetImageByName(string name)
		{
			return _dbContext.Images.FirstOrDefault(x => x.Name == name);
		}

		public bool CreateImage(IImage image)
		{
			try
			{
				_dbContext.Images.Add(new Image()
				{
					Data = image.Data,
					Name = image.Name,
					UpdateDate = DateTime.Now
				});
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		public bool UpdateImage(IImage image)
		{
			try
			{
				var dbImage = _dbContext.Images.FirstOrDefault(x => x.Name == image.Name);
				if (dbImage != null)
				{
					dbImage.UpdateDate = image.UpdateDate;
					dbImage.Data = image.Data;
					//_dbContext.Images.Update(dbImage);
					_dbContext.SaveChanges();
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}
	}
}
