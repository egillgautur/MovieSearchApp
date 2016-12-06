using System;
using System.Collections.Generic;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieDownload;
using System.Threading;
using System.IO;
using CoreGraphics;

namespace MovieSearchApp.iOS.Controllers
{
	public class TopRatedListController : UITableViewController
	{
		private List<Models.Movie> _movieList;
		private bool _reload;
		private ApiService _apiService;

		public TopRatedListController(List<Models.Movie> movieList)
		{
			this._movieList = movieList;
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
			this._reload = true;
			_apiService = new ApiService(new iosImageImp());
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Top rated movies";
			this.View.BackgroundColor = UIColor.DarkGray;
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			//If private member reload is true, call API, start spinner and call reloadData()
			if (this._reload)
			{
				var activity = new UIActivityIndicatorView(new CGRect(this.View.Bounds.Width / 2, 10, 0, 20))
				{
					ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White

				};

				activity.HidesWhenStopped = true;
				this.View.AddSubview(activity);
				activity.StartAnimating();

				this._movieList = await _apiService.getMovie(false, "");
			
				this.TableView.ReloadData();
				activity.StopAnimating();
				this.TableView.Source = new MovieListSource(this._movieList, onSelectedMovie);
			}

			this._reload = true;
		}

		private void onSelectedMovie(int row)
		{
			this.NavigationController.PushViewController(new MovieDetailController(this._movieList[row]), true);

			//Setting private member _reload to false since we don't want to trigger reloadData() when you enter details on movie
			this._reload = false;
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			//If member variable _reload is true, clear the list and reload data
			if (this._reload) {
				this._movieList.Clear();
				this.TableView.ReloadData();
			}
		}
	}
}
