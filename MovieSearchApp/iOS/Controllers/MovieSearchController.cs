using System;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using UIKit;
using CoreGraphics;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace MovieSearchApp.iOS.Controllers
{
	public partial class MovieSearchController : UIViewController
	{
		private const int HorizontalMargin = 20;
		private const int StartY = 280;
		private const int StepY = 50;
		private int _yCoord;
		private List<Models.Movie> _movieList;
		private ApiService _apiService;

		public MovieSearchController(List<Models.Movie> movieList)
		{
			IMovieDbSettingImp sett = new IMovieDbSettingImp();
			MovieDbFactory.RegisterSettings(sett);
			//this._movies = new Movies();
			this._movieList = movieList;
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
			_apiService = new ApiService(new iosImageImp());
		}

		protected MovieSearchController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.DarkGray;
			this.Title = "Movie Search";

			//Creating spinner
			var activity = new UIActivityIndicatorView(new CGRect(160, 50, 25, 25))
			{
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White

			};
			activity.HidesWhenStopped = true;

			// Perform any additional setup after loading the view, typically from a nib
			this._yCoord = StartY;

			var searchLabel = this.MovieSearchLabel();

			var searchField = this.MovieSearchField();

			var searchButton = this.MovieSearchButton();

			var searchResultLabel = this.MovieSearchResultLabel();

			var image = this.img();

			searchButton.TouchUpInside += async (sender, args) =>
			{
				//Start spinner, disable the search button and change the color to gray
				activity.StartAnimating();
				searchButton.Enabled = false;
				searchButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
				searchField.ResignFirstResponder();

				if (searchField.Text == "")
				{
					searchResultLabel.Text = "Please enter text";
					activity.StopAnimating();
					searchButton.Enabled = true;
					searchButton.SetTitleColor(UIColor.White, UIControlState.Normal);
				}
				else
				{
					searchResultLabel.Text = "";
					this._movieList = await _apiService.getMovie(true, searchField.Text);

					//Stop spinner, enable the search button and change the color to white
					activity.StopAnimating();
					searchButton.Enabled = true;
					searchButton.SetTitleColor(UIColor.White, UIControlState.Normal);
					searchField.Text = "";

					this.NavigationController.PushViewController(new MovieListController(this._movieList), true);
				}
			};

			searchButton.AddSubview(activity);
			searchButton.BringSubviewToFront(activity);
			this.View.AddSubview(searchLabel);
			this.View.AddSubview(searchField);
			this.View.AddSubview(searchButton);
			this.View.AddSubview(searchResultLabel);
			this.View.AddSubview(image);
		}

		private UILabel MovieSearchLabel() {
			var searchLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = "Enter words in movie title: ",
				TextColor = UIColor.White
			};

			this._yCoord += StepY;

			return searchLabel;
		}

		private UITextField MovieSearchField() {
			var searchField = new UITextField()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Word in movie",
				BackgroundColor = UIColor.Gray
			};

			this._yCoord += StepY;

			return searchField;
		}

		private UIButton MovieSearchButton() { 
			var submitButton = UIButton.FromType(UIButtonType.RoundedRect);
			submitButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
			submitButton.SetTitle("Get movie", UIControlState.Normal);
			submitButton.SetTitleColor(UIColor.White,UIControlState.Normal);

			this._yCoord += StepY;

			return submitButton;
		}

		private UILabel MovieSearchResultLabel() { 
			var resultLabel = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
			};

			this._yCoord += StepY;

			return resultLabel;
		}

		private UIImageView img() {
			var imageView = new UIImageView(UIImage.FromBundle("film-strip.png"));
			imageView.Frame = new CGRect(20, 80, this.View.Bounds.Width - 40, 200);
			return imageView;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
