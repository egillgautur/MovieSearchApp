using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MovieDownload;

namespace MovieSearchApp.iOS
{
	public class iosImageImp : IImageImp
	{
		private ImageDownloader _downloader;
		public iosImageImp()
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
