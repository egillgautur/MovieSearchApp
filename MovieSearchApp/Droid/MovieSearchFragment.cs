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

    public class MovieSearchFragment : Fragment
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

            var rootView = inflater.Inflate(Resource.Layout.MovieSearchLayout, container, false);
            // Get our UI controls from the loaded layout:
			var searchText = rootView.FindViewById<EditText>(Resource.Id.searchText);
			var button = rootView.FindViewById<Button>(Resource.Id.searchButton);
			var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
			spinner.Visibility = ViewStates.Invisible;

			button.Click += async (sender, args) =>
			{
				spinner.Visibility = Android.Views.ViewStates.Visible;
				InputMethodManager manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
				manager.HideSoftInputFromWindow(searchText.WindowToken, 0);
				this._movieList = await _apiService.getMovie(true, searchText.Text);
				var intent = new Intent(this.Context, typeof(MovieListActivity));
				//intent.PutStringArrayListExtra("MovieList", this._movieL.movieList.Select(p => p.Name).ToArray());
				intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movieList));
				spinner.Visibility = ViewStates.Gone;
				this.StartActivity(intent);
			};

            return rootView;
        }
    }
}