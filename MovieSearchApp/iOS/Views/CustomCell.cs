using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace MovieSearchApp.iOS.Views
{
	public class CustomCell : UITableViewCell
	{
		private UILabel _nameLabel, _yearLabel, _castMembersLabel;
		private UIImageView _imageView;

		public CustomCell(NSString cellId)
			: base(UITableViewCellStyle.Default, cellId)
		{
			this.BackgroundColor = UIColor.DarkGray;
			this._imageView = new UIImageView();
			this._nameLabel = new UILabel()
			{
				Font = UIFont.FromName("Helvetica", 16f),
				TextColor = UIColor.White
			};

			this._yearLabel = new UILabel()
			{
				Font = UIFont.FromName("Helvetica", 10f),
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Left
			};

			this._castMembersLabel = new UILabel()
			{
				Font = UIFont.FromName("Helvetica", 8f),
				TextColor = UIColor.LightGray,
				TextAlignment = UITextAlignment.Left
			};

			this.ContentView.AddSubviews(new UIView[] { this._imageView, this._nameLabel, this._yearLabel, this._castMembersLabel });
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			this._imageView.Frame = new CGRect(2, 2, this.ContentView.Bounds.Width - 300, 40);
			this._nameLabel.Frame = new CGRect(60, 1, this.ContentView.Bounds.Width - 65, 20);
			this._yearLabel.Frame = new CGRect(60, 15, 100, 20);
			this._castMembersLabel.Frame = new CGRect(60, 15, 300, 40);
			//var s = new CGRect(this.ContentView.Bounds.Width - 60, 5, 50, 20);
			//5 ------- -60

		}

		public void UpdateCell(string name, string year, string imageName, string lis)
		{
			this._imageView.Image = UIImage.FromFile(imageName);
			this._nameLabel.Text = name;
			this._yearLabel.Text = "("+ year + ")" ;
			this._castMembersLabel.Text = lis;

			this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
		}
	}
}
