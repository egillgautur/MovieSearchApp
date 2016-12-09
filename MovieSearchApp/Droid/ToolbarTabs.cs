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
using MovieSearchApp.Droid;

namespace MovieSearchApp.Droid
{
    using Android.Support.Design.Widget;
    using Android.Support.V4.App;
    using Android.Support.V4.View;

    public class ToolbarTabs
    {
		private TopRatedFragment _topRatedFragment = new TopRatedFragment();
        public void Construct(FragmentActivity activity, Toolbar toolbar)
        {
            var fragments = new Fragment[]
            {
				new MovieSearchFragment(),
				_topRatedFragment
            };
            var titles = CharSequence.ArrayFromStringArray(new[]
            {
                "Search",
                "Top Rated"
            });

			var viewPager = activity.FindViewById<ViewPager>(Resource.Id.viewpager); 
            viewPager.Adapter = new TabsFragmentPagerAdapter(activity.SupportFragmentManager, fragments, titles);
			//viewPager.OffscreenPageLimit = 0; 

			// Give the TabLayout the ViewPager
			/*var tabLayout = activity.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);*/

			var tabLayout = activity.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
			tabLayout.SetupWithViewPager(viewPager);
			viewPager.PageSelected += (sender, args) =>
				{
				//viewPager.SetCurrentItem(args.Position, true);
				var tab = args.Position;
					if (tab == 1)
					{
						_topRatedFragment.getTopRated(activity);
					}
				};

            SetToolbar(activity, toolbar);
        }

        public static void SetToolbar(Activity activity, Toolbar toolbar)
        {
            activity.SetActionBar(toolbar);
            activity.ActionBar.Title = activity.GetString(Resource.String.ToolbarTitle);
        }
    }
}