using System;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearchApp
{
	public class IMovieDbSettingImp : IMovieDbSettings
	{
		public IMovieDbSettingImp()
		{
		}

		public string ApiKey
		{
			get
			{
				return "dd8579f19de65afd440a381f5b4e8d8e";
			}
		}

		public string ApiUrl
		{
			get
			{
				return "http://api.themoviedb.org/3/";
			}
		}
	}
}
