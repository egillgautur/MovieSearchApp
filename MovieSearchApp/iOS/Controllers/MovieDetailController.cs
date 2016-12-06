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
	public partial class MovieDetailController : UIViewController
	{
		private Models.Movie _movie;

		public MovieDetailController(Models.Movie mov)
		{
			this._movie = mov;
		}

		protected MovieDetailController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.DarkGray;

			this.Title = _movie.Name;

			var movieLabel = this.MovieLabel();
			var yearLabel = this.YearLabel();
			var genreLabel = this.GenreLabel();
			var runtimeLabel = this.RuntimeLabel();
			var descriptionLabel = this.DescriptionLabel();
			var imageLabel = this.ImageLabel();
			var text = this.textLabel(yearLabel.Text + " | " + runtimeLabel.Text + " | " + genreLabel.Text);

			this.View.AddSubview(movieLabel);
			this.View.AddSubview(text);
			this.View.AddSubview(descriptionLabel);
			this.View.AddSubview(imageLabel);

		}

		private UILabel textLabel(string t)
		{
			var textl = new UILabel
			{
				Text = t,
				Frame = new CGRect(15, 437,300 , 50),
				Font = UIFont.FromName("Helvetica", 12f)
			};

			return textl;
		}

		private UIImageView ImageLabel() {
			var image = new UIImageView()
			{
				Image = UIImage.FromFile(_movie.ImageName),
				Frame = new CGRect(15, 75, this.View.Bounds.Width - 28, this.View.Bounds.Width - 20)
			};

			return image;
		}

		private UILabel MovieLabel()
		{
			var movieLabel = new UILabel()
			{
				Frame = new CGRect(15, 420, this.View.Bounds.Width -50, 50),
				Font = UIFont.FromName("Helvetica", 18f),
				Text = this._movie.Name
			};

			return movieLabel;
		}

		private UILabel YearLabel() { 
			var yearLabel = new UILabel()
			{
				Text = this._movie.ReleaseYear,
			};

			return yearLabel;
		}

		private UILabel GenreLabel()
		{
			var genreLabel = new UILabel()
			{
				Text = this._movie.Genre
			};
			return genreLabel;
		}

		private UILabel RuntimeLabel()
		{
			var runtimeLabel = new UILabel()
			{
				Text = this._movie.Runtime
			};

			return runtimeLabel;
		}

		private UILabel DescriptionLabel()
		{
			var descriptionLabel = new UILabel()
			{
				Frame = new CGRect(15, 480, this.View.Bounds.Width - 80, 0),
				Text = this._movie.Overview,
				Font = UIFont.FromName("Helvetica", 10f),

			};

			//Show the entire overview text
			descriptionLabel.LineBreakMode = UILineBreakMode.WordWrap;
			descriptionLabel.Lines = 0;

			//Starting the text at beginning of frame (don't want the text centered)
			descriptionLabel.SizeToFit();

			return descriptionLabel;
		}


		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

	}
}