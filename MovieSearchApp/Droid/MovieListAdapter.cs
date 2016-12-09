using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MovieSearchApp.Droid
{
	using Android.Graphics;
	using Java.IO;

	public class MovieListAdapter : BaseAdapter<Models.Movie>
	{
		private Activity _context;

		private List<Models.Movie> _movieList;

		public MovieListAdapter(Activity context, List<Models.Movie> movieList)
		{
			this._context = context;
			this._movieList = movieList;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
			}

			var movie = this._movieList[position];
			view.FindViewById<TextView>(Resource.Id.name).Text = movie.Name;
			view.FindViewById<TextView>(Resource.Id.year).Text = movie.ReleaseYear;
			view.FindViewById<TextView>(Resource.Id.castMembers).Text = movie.CastMembers;

			var img = new File(movie.ImageName);
			var bmimg = BitmapFactory.DecodeFile(img.AbsolutePath);
			view.FindViewById<ImageView>(Resource.Id.image).SetImageBitmap(bmimg);

			return view;
		}

		public override int Count
		{
			get
			{
				return this._movieList.Count;
			}
		}

		public override Models.Movie this[int position]
		{
			get
			{
				return this._movieList[position];
			}
		}


	}
}