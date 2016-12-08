using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Content;
using MovieSearchApp.Droid;
using Newtonsoft.Json;
using System.Linq;
using MovieSearchApp.droid;
namespace MovieSearchApp.Droid
{

	[Activity(Label = "Movie Search App", Icon = "@drawable/icon", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private Models.Movies _movieL;
		private ApiService _apiService;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			this._movieL = new Models.Movies();
			this._apiService = new ApiService(new droidImageImp());

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.Main);
			var searchText = this.FindViewById<EditText>(Resource.Id.searchText);
			var button = this.FindViewById<Button>(Resource.Id.searchButton);

			button.Click += async (sender, args) =>
			{
				var str = await _apiService.getMovie(true, searchText.Text);
				var intent = new Intent(this, typeof(MovieListActivity));
				intent.PutStringArrayListExtra("MovieList", this._movieL.movieList.Select(p => p.Name).ToArray());
				intent.PutExtra("movieList", JsonConvert.SerializeObject(str));
				this.StartActivity(intent);
			};

		}
	}
}