using System;
using System.Collections.Generic;
using UIKit;
using MovieSearchApp.Models;

namespace MovieSearchApp.iOS.Controllers
{
	public class MovieListController : UITableViewController
	{
		private List<Models.Movie> _movieList;

		public MovieListController(List<Models.Movie> movieList)
		{ 
			this._movieList = movieList;
		}

		public override void ViewDidLoad()
		{
			this.View.BackgroundColor = UIColor.DarkGray;
			this.Title = "Movie List";

			this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);

		}

		public void OnSelectedMovie(int row)
		{
			this.NavigationController.PushViewController(new MovieDetailController(this._movieList[row]), true);
		}

	}
}
