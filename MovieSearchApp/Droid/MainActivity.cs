using Android.App;
using Android.Widget;
using Android.OS;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.ApiResponse;

namespace MovieSearchApp.Droid
{
	[Activity(Label = "MovieSearchApp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			IMovieDbSettingImp sett = new IMovieDbSettingImp();
			MovieDbFactory.RegisterSettings(sett);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; };
		}
	}
}

