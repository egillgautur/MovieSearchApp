using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MovieDownload;

namespace MovieSearchApp.droid
{
	public class droidImageImp : IImageImp
	{
		private ImageDownloader _downloader;
		public droidImageImp()
		{
			
			_downloader = new ImageDownloader(new StorageClient());
		}

		public async Task<string> getImage(string path)
		{
			//Path for image
			var localPath = "";
			if (path != null)
			{
				localPath = _downloader.LocalPathForFilename(path);
				var cancelToken = new CancellationToken();
				if (!File.Exists(path))
				{
					await _downloader.DownloadImage(path, localPath, cancelToken);
				}
			}

			return localPath;
		}
	}
}
