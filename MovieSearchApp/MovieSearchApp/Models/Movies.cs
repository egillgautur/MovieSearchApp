using System;
using System.Collections.Generic;

namespace MovieSearchApp.Models
{
	public class Movies
	{
		private List<Models.Movie> _movies;

		public Movies()
		{
			this._movies = new List<Models.Movie>();
		}

		public List<Models.Movie> movieList => this._movies;
	}
}
