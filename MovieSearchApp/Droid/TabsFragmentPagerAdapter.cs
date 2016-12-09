namespace MovieSearchApp.Droid
{
    using Android.Support.V4.App;

    using Java.Lang;

    public class TabsFragmentPagerAdapter : FragmentPagerAdapter
    {
        private readonly Fragment[] _fragments;

        private readonly ICharSequence[] _titles;

        public TabsFragmentPagerAdapter(FragmentManager fm, Fragment[] fragments, ICharSequence[] titles)
            : base(fm)
        {
            this._fragments = fragments;
            this._titles = titles;
        }

        public override int Count
        {
            get
            {
                return this._fragments.Length;
            }
        }

        public override Fragment GetItem(int position)
        {
            return this._fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return this._titles[position];
        }
    }
}