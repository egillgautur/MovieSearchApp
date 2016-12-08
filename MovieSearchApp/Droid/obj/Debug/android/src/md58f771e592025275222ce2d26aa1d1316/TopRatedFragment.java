package md58f771e592025275222ce2d26aa1d1316;


public class TopRatedFragment
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MovieSearchApp.Droid.TopRatedFragment, MovieSearchApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TopRatedFragment.class, __md_methods);
	}


	public TopRatedFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TopRatedFragment.class)
			mono.android.TypeManager.Activate ("MovieSearchApp.Droid.TopRatedFragment, MovieSearchApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
