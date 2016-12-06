using System;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using UIKit;
using DM.MovieApi.MovieDb.Genres;
using DM.MovieApi.MovieDb.People;
using CoreGraphics;
using System.Drawing;
using System.Collections.Generic;
using MovieDownload;
using System.Threading;
using System.IO;

namespace MovieSearchApp.iOS.Controllers
{
	public class APIController : UIViewController
	{
		private Movies _movies;
		private ImageDownloader _downloader;
		private string _apiString;

		public APIController(string str)
		{
			this._movies = new Movies();
			_downloader = new ImageDownloader(new StorageClient());
			this._apiString = str;
		}

		public async void callApi(string apiString, string searchString) {
			
			//API call to search db by movie string
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
			ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(searchString);

			if (apiString == "Top Rated")
			{
				response = await movieApi.GetTopRatedAsync(1);
			}

			//Clear our movie list so it is empty before we search for movies
			this._movies.movieList.Clear();

			//Iterate through all results that matched the search string
			foreach (var item in response.Results)
			{
				string path = "";
				string castMembersString = "";
				String genreString = "";
				int counter = 0;

				//API call to get cast members
				var castResponse = await movieApi.GetCreditsAsync(item.Id);
				var castMembers = castResponse.Item.CastMembers;

				//API call to get runtime, genres and overview
				var detailsResponse = await movieApi.FindByIdAsync(item.Id);

				var runtime = "";
				var overview = "";
				if (detailsResponse.Item != null)
				{
					var genres = detailsResponse.Item.Genres;
					//Adding all genres in one string
					foreach (var iter in genres)
					{
						genreString += iter.Name + ", ";
					}
					runtime = detailsResponse.Item.Runtime + " min";
					overview = detailsResponse.Item.Overview;
				}


				//Path for image
				if (item.PosterPath != null)
				{
					path = _downloader.LocalPathForFilename(item.PosterPath);
					var cancelToken = new CancellationToken();
					if (!File.Exists(path))
					{
						await _downloader.DownloadImage(item.PosterPath, path, cancelToken);
					}
				}



				//Skítamix til að fá castmembers með kommu á milli en ekki í endann
				foreach (var it in castMembers)
				{
					if (counter == 2)
					{
						castMembersString += it.Name;
					}
					else {
						castMembersString += it.Name + ", ";
					}
					counter++;
					if (counter == 3) { break; }

				}



				//Cutta kommuna af endanum á genres
				if (genreString != "")
				{
					genreString = genreString.Remove(genreString.Length - 2);
				}


				var movie = new Movie()
				{
					Name = item.Title,
					ReleaseYear = item.ReleaseDate.Year.ToString(),
					ImageName = path,
					CastMembers = castMembersString,
					Genre = genreString,
					Runtime = runtime,
					Overview = overview
				};
				this._movies.movieList.Add(movie);
			}

			if (apiString == "Top Rated")
			{
				this.NavigationController.PushViewController(new TopRatedListController(this._movies.movieList), true);
			}
			else { 
				this.NavigationController.PushViewController(new MovieListController(this._movies.movieList), true);
			}


		}
	}
}
