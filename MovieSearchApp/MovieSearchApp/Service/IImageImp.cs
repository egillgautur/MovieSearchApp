using System;
using System.Threading.Tasks;

namespace MovieSearchApp
{
	public interface IImageImp
	{
		Task<string> getImage(string path);
	}
}
