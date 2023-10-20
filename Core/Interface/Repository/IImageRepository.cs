using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IImageRepository
    {
		IImage? GetImageByName(string name);
		bool CreateImage(IImage image);
		bool UpdateImage(IImage image);
	}
}
