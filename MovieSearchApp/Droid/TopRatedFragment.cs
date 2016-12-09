using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MovieSearchApp.droid;
using System.Collections.Generic;

namespace MovieSearchApp.Droid
{
	using Newtonsoft.Json;
	using Android.App;

	public class TopRatedFragment : Android.Support.V4.App.Fragment
	{
		private List<Models.Movie> _movieList;
		private ApiService _apiService;
		private View _rootView;

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


			// Get our UI controls from the loaded layout:
			this._rootView = inflater.Inflate(Resource.Layout.TopRatedLayout, container, false);

			return _rootView;
		}

		public async void getTopRated(Activity context) { 
			var spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
			spinner.Visibility = ViewStates.Invisible;
			spinner.Visibility = Android.Views.ViewStates.Visible;
			var listAdapter = this._rootView.FindViewById<ListView>(Resource.Id.movielistview);
			this._movieList = await _apiService.getMovie(false, "yolo");
			spinner.Visibility = ViewStates.Gone;
			listAdapter.Adapter = new MovieListAdapter(context, this._movieList);
			listAdapter.ItemClick += (sender, args) =>
			{
				var intent = new Intent(this.Context, typeof(MovieDetailActivity));
				intent.PutExtra("Movie Details", JsonConvert.SerializeObject(this._movieList[args.Position]));
				this.StartActivity(intent);
			};
		}

	}
}