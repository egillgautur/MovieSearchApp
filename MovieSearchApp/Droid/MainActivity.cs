﻿using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Content;
using MovieSearchApp.Droid;
using Newtonsoft.Json;
using System.Linq;
using MovieSearchApp.droid;
using Android.Support.V4.App;

namespace MovieSearchApp.Droid
{

	[Activity(Label = "Movie Search App", Icon = "@drawable/icon", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.Main);

			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			ToolbarTabs.Construct(this, toolbar);
		}
	}
}