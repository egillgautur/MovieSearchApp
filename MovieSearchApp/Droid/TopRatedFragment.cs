using System;

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MovieSearchApp.droid;
using System.Collections.Generic;

using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearchApp.Droid
{
	using MovieSearchApp.Models;
	using Android.Views.InputMethods;
	using Newtonsoft.Json;
	using System.Threading.Tasks;

	public class TopRatedFragment : Fragment
	{
		private List<Models.Movie> _movieList;
		private ApiService _apiService;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			this._movieList = new List<Models.Movie>();
			this._apiService = new ApiService(new droidImageImp());


			// Create your fragment here

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			// Use this to return your custom view for this Fragment

			var rootView = inflater.Inflate(Resource.Layout.TopRatedLayout, container, false);
			// Get our UI controls from the loaded layout:

			getTopRated(rootView);

			return rootView;
		}

		public async void getTopRated(View rootView) { 
			var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
			spinner.Visibility = ViewStates.Invisible;
			spinner.Visibility = Android.Views.ViewStates.Visible;

			this._movieList = await _apiService.getMovie(false, "");
			var intent = new Intent(this.Context, typeof(MovieListActivity));
			intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movieList));
			spinner.Visibility = ViewStates.Gone;
			this.StartActivity(intent);
		}
	}
}