using UnityEngine;

public class FlurryEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		FlurryManager.spaceDidDismissEvent += spaceDidDismissEvent;
		FlurryManager.spaceWillLeaveApplicationEvent += spaceWillLeaveApplicationEvent;
		FlurryManager.spaceDidFailToRenderEvent += spaceDidFailToRenderEvent;
		FlurryManager.spaceDidReceiveAdEvent += spaceDidReceiveAdEvent;
		FlurryManager.spaceDidFailToReceiveAdEvent += spaceDidFailToReceiveAdEvent;
	}

	private void OnDisable()
	{
		FlurryManager.spaceDidDismissEvent -= spaceDidDismissEvent;
		FlurryManager.spaceWillLeaveApplicationEvent -= spaceWillLeaveApplicationEvent;
		FlurryManager.spaceDidFailToRenderEvent -= spaceDidFailToRenderEvent;
		FlurryManager.spaceDidReceiveAdEvent -= spaceDidReceiveAdEvent;
		FlurryManager.spaceDidFailToReceiveAdEvent -= spaceDidFailToReceiveAdEvent;
	}

	private void spaceDidDismissEvent(string space)
	{
		Debug.Log("spaceDidDismissEvent: " + space);
	}

	private void spaceWillLeaveApplicationEvent(string space)
	{
		Debug.Log("spaceWillLeaveApplicationEvent: " + space);
	}

	private void spaceDidFailToRenderEvent(string space)
	{
		Debug.Log("spaceDidFailToRenderEvent: " + space);
	}

	private void spaceDidReceiveAdEvent(string space)
	{
		Debug.Log("spaceDidReceiveAdEvent: " + space);
	}

	private void spaceDidFailToReceiveAdEvent(string space)
	{
		Debug.Log("spaceDidFailToReceiveAdEvent: " + space);
	}
}
