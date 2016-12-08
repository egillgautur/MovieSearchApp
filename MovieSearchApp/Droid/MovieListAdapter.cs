﻿using System;
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
	using MovieSearchApp.Models;

	public class MovieListAdapter : BaseAdapter<Movie>
	{
		private Activity _context;

		private List<Movie> _movieList;

		public MovieListAdapter(Activity context, List<Movie> movieList)
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

			var resourceId = this._context.Resources.GetIdentifier(
				movie.ImageName,
				"drawable",
				this._context.PackageName);
			view.FindViewById<ImageView>(Resource.Id.picture).SetBackgroundResource(resourceId);

			return view;
		}

		public override int Count
		{
			get
			{
				return this._movieList.Count;
			}
		}

		public override Movie this[int position]
		{
			get
			{
				return this._movieList[position];
			}
		}
	}
}