using System;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Threading.Tasks;
using System.Collections.Generic;
using MovieSearchApp.Models;

namespace MovieSearchApp
{
	public class ApiService
	{
		private IImageImp _imp;
		private Movies _movies;
		public ApiService(IImageImp imp)
		{
			IMovieDbSettingImp sett = new IMovieDbSettingImp();
			MovieDbFactory.RegisterSettings(sett);
			this._imp = imp;
			this._movies = new Movies();

		}

		public async Task<List<Models.Movie>> getMovie(bool searchValue, string searchString) {
			
			//Clear our movie list so it is empty before we search for movies
			this._movies.movieList.Clear();


			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
			ApiSearchResponse<MovieInfo> response;
			if (searchValue)
			{
				response = await movieApi.SearchByTitleAsync(searchString);
			}
			else { 
				response = await movieApi.GetTopRatedAsync(1);
			}

				//Iterate through all results that matched the search string
				foreach (var item in response.Results)
				{
					string path = "";
					string castMembersString = "";
					string genreString = "";
					string runtimeString = "";
					string overviewString = "";
					int counter = 0;

					//API call to get cast members
					var castResponse = await movieApi.GetCreditsAsync(item.Id);

					//Handling null exception
					if (castResponse.Item != null)
					{
						var castMembers = castResponse.Item.CastMembers;

						//String mix to get rid of ',' in the end of string and only show 3 cast members
						foreach (var it in castMembers)
						{
							castMembersString += it.Name + ", ";
							counter++;

							if (counter == 3) { break; }

						}

						//Getting rid of ',' in the end of string
						if (castMembersString != "")
						{
							castMembersString = castMembersString.Remove(castMembersString.Length - 2);
						}
					}

					//API call to get runtime, genres and overview
					var detailsResponse = await movieApi.FindByIdAsync(item.Id);

					//Handling null exception
					if (detailsResponse.Item != null)
					{
						var genres = detailsResponse.Item.Genres;

						//Adding all genres in one string
						foreach (var iter in genres)
						{
							genreString += iter.Name + ", ";
						}

						//Getting rid of ',' in the end of string
						if (genreString != "")
						{
							genreString = genreString.Remove(genreString.Length - 2);
						}

						runtimeString = detailsResponse.Item.Runtime + " min";
						overviewString = detailsResponse.Item.Overview;
					}

					if (_imp != null)
					{
						path = await _imp.getImage(item.PosterPath);
					}

					var movie = new Models.Movie()
					{
						Name = item.Title,
						ReleaseYear = item.ReleaseDate.Year.ToString(),
						ImageName = path,
						CastMembers = castMembersString,
						Genre = genreString,
						Runtime = runtimeString,
						Overview = overviewString
					};
					this._movies.movieList.Add(movie);
				}

			return _movies.movieList;
		}
	}
}
