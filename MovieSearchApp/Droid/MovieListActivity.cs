using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MovieSearchApp.Droid
{
	using Newtonsoft.Json;

	[Activity(Label = "Movie list")]
	public class MovieListActivity : ListActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var jsonStr = this.Intent.GetStringExtra("movieList");
			var movieList = JsonConvert.DeserializeObject<List<Models.Movie>>(jsonStr);
			this.ListAdapter = new MovieListAdapter(this, movieList);
			// Create your application here
		}
	}
}
