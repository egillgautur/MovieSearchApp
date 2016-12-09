
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;

namespace MovieSearchApp.Droid
{
	[Activity(Label = "MovieDetailActivity")]
	public class MovieDetailActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.SetContentView(Resource.Layout.MovieDetailLayout);

			// Create your application here

			var json = this.Intent.GetStringExtra("Movie Details");
			var movieList = JsonConvert.DeserializeObject<Models.Movie>(json);


			var title = this.FindViewById<TextView>(Resource.Id.title).Text = movieList.Name;
			var info = this.FindViewById<TextView>(Resource.Id.info).Text = movieList.ReleaseYear + " | " + movieList.Runtime + " | " + movieList.Genre;
			var overview = this.FindViewById<TextView>(Resource.Id.overview).Text = movieList.Overview;

			var file = new File(movieList.ImageName);
			var bmimg = BitmapFactory.DecodeFile(file.AbsolutePath);
			this.FindViewById<ImageView>(Resource.Id.image).SetImageBitmap(bmimg);
		}
	}
}
