using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;

namespace MovieSearchApp.Droid
{
	using Newtonsoft.Json;

	[Activity(Label = "Movie list")]
	public class MovieListActivity : ListActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here

			var jsonStr = this.Intent.GetStringExtra("movieList");
			var movieList = JsonConvert.DeserializeObject<List<Models.Movie>>(jsonStr);
			this.ListAdapter = new MovieListAdapter(this, movieList);
			this.ListView.ItemClick += (sender, args) =>
			{
				var intent = new Intent(this, typeof(MovieDetailActivity));
				intent.PutExtra("Movie Details", JsonConvert.SerializeObject(movieList[args.Position]));
				this.StartActivity(intent);
			};

		}

	}
}
