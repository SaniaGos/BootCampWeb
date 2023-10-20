using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
	public interface IImageService
	{
		Task<byte[]> GetImage(int? id, string? name);
	}
}
