using System;
using UIKit;
namespace MovieSearchApp.iOS.Controllers
{
	public class TabBarController : UITabBarController
	{
		public TabBarController()
		{
			base.ViewDidLoad();

			this.TabBar.BackgroundColor = UIColor.White;
			this.TabBar.TintColor = UIColor.Black;

			this.SelectedIndex = 0;
		}

	}
}
