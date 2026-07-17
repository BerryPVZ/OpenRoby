using System;
using Prime31;

public class FlurryManager : AbstractManager
{
	public static event Action<string> spaceDidDismissEvent;

	public static event Action<string> spaceWillLeaveApplicationEvent;

	public static event Action<string> spaceDidFailToRenderEvent;

	public static event Action<string> spaceDidFailToReceiveAdEvent;

	public static event Action<string> spaceDidReceiveAdEvent;

	static FlurryManager()
	{
		AbstractManager.initialize(typeof(FlurryManager));
	}

	public void spaceDidDismiss(string space)
	{
		if (FlurryManager.spaceDidDismissEvent != null)
		{
			FlurryManager.spaceDidDismissEvent(space);
		}
	}

	public void spaceWillLeaveApplication(string space)
	{
		if (FlurryManager.spaceWillLeaveApplicationEvent != null)
		{
			FlurryManager.spaceWillLeaveApplicationEvent(space);
		}
	}

	public void spaceDidFailToRender(string space)
	{
		if (FlurryManager.spaceDidFailToRenderEvent != null)
		{
			FlurryManager.spaceDidFailToRenderEvent(space);
		}
	}

	public void spaceDidFailToReceiveAd(string space)
	{
		if (FlurryManager.spaceDidFailToReceiveAdEvent != null)
		{
			FlurryManager.spaceDidFailToReceiveAdEvent(space);
		}
	}

	public void spaceDidReceiveAd(string space)
	{
		if (FlurryManager.spaceDidReceiveAdEvent != null)
		{
			FlurryManager.spaceDidReceiveAdEvent(space);
		}
	}
}
