using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using MovieSearchApp.iOS.Views;

namespace MovieSearchApp.iOS.Controllers
{
	public class MovieListSource : UITableViewSource
	{
		private List<Models.Movie> _movieList;
		private Action<int> _onSelectedMovie;
		public readonly NSString MovieListCellId = new NSString("MovieListCell");

		public MovieListSource(List<Models.Movie> movieList, Action<int> onSelectedMovie)
		{
			this._movieList = movieList;
			this._onSelectedMovie = onSelectedMovie;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (CustomCell)tableView.DequeueReusableCell(this.MovieListCellId);

			if (cell == null)
			{
				cell = new CustomCell((NSString)this.MovieListCellId);
			}

			int row = indexPath.Row;
			cell.UpdateCell(this._movieList[row].Name, this._movieList[row].ReleaseYear, this._movieList[row].ImageName, this._movieList[row].CastMembers);

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this._movieList.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
			this._onSelectedMovie(indexPath.Row);
		}
	}
}
