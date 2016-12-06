using System;
using UIKit;

namespace MovieSearchApp.iOS.Controllers
{
	public class TabBarDelegate : UITabBarControllerDelegate
	{
		
		public TabBarDelegate()
		{
		}

		public override void ViewControllerSelected(UITabBarController tabBarController, UIViewController viewController)
		{
			base.ViewControllerSelected(tabBarController, viewController);
		}
	}
}
