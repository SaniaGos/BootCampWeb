using Core.Entity;
using Core.Interface;
using Core.Interface.Repository;
using Microsoft.Extensions.Options;

namespace Core.Service
{
	public class ImageService : IImageService
	{
		protected readonly IImageRepository _imageRepository;
		protected readonly AppConf _config;

		public ImageService(IImageRepository imageRepository, IOptions<AppConf> options)
		{
			_imageRepository = imageRepository;
			_config = options.Value;
		}

		public async Task<byte[]> GetImage(int? id, string? name)
		{
			if (!string.IsNullOrEmpty(name) && id.HasValue && id.Value != 0)
			{
				var imageName = GetImageName(id.Value, name);
				if (ImageCache.IsImageDownload(imageName, out byte[]? image))
				{
					return image ?? new byte[0];
				}
				else
				{
					if (IsImageInBase(imageName, out IImage dbImage))
					{
						if (dbImage.UpdateDate.Add(_config.ElapsedDbTime) > DateTime.Now)
						{
							ImageCache.AddImage(imageName, dbImage.Data, DateTime.Now.Add(_config.ElapsedCacheTime));
							return dbImage.Data;
						}
						else
						{
							return await UpdateImage(_imageRepository.UpdateImage, id.Value, name, dbImage.Data);
						}
					}
					else
					{
						return await UpdateImage(_imageRepository.CreateImage, id.Value, name, new byte[0]);
					}
				}
			}

			return new byte[0];
		}

		private async Task<byte[]> UpdateImage(Func<IImage, bool> func, int id, string name, byte[] defaultData)
		{
			var image = await GetImageFromSite(name, id);
			if (image.Length == 0) { return defaultData; }

			var imageName = GetImageName(id, name);
			var iImage = new Image()
			{
				Data = image,
				Name = imageName,
				UpdateDate = DateTime.Now
			};

			ImageCache.AddImage(imageName, image, DateTime.Now.Add(_config.ElapsedCacheTime));
			func?.Invoke(iImage);
			return image;
		}

		private string GetImageName(int id, string name) => "image_" + name;
		//private string GetImageName(int id, string name) => id + "_image_" + name;

		private bool IsImageInBase(string imageName, out IImage image)
		{
			image = _imageRepository.GetImageByName(imageName)!;
			return image != null;
		}

		private async Task<byte[]> GetImageFromSite(string? name, int? id)
		{
			using HttpClient client = new HttpClient();
			byte[] byteArr = new byte[0];

			try
			{
				using var responce = await client.GetAsync($"https://robohash.org/{name}");

				if (responce != null && responce.IsSuccessStatusCode)
				{
					byteArr = await responce.Content.ReadAsByteArrayAsync();
				}
			}
			catch (Exception ex)
			{ }

			return byteArr;
		}
	}
}
