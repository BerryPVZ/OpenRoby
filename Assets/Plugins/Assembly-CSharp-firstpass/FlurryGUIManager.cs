using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class FlurryGUIManager : MonoBehaviourGUI
{
	private void OnGUI()
	{
		beginColumn();
		if (GUILayout.Button("Start Flurry Session"))
		{
			FlurryBinding.setAge(12);
			FlurryBinding.setGender("M");
			FlurryBinding.startSession("INSERT_YOUR_FLURRY_KEY");
		}
		if (GUILayout.Button("Log Event"))
		{
			FlurryBinding.logEvent("Stuff Happened", false);
		}
		if (GUILayout.Button("Log Event with Params"))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("akey1", "value1");
			dictionary.Add("bkey2", "value2");
			dictionary.Add("ckey3", "value3");
			dictionary.Add("dkey4", "value4");
			FlurryBinding.logEventWithParameters("EventWithParams", dictionary, false);
		}
		if (GUILayout.Button("Log Timed Event"))
		{
			FlurryBinding.logEvent("Timed Event", true);
		}
		if (GUILayout.Button("End Timed Event"))
		{
			FlurryBinding.endTimedEvent("Timed Event");
		}
		if (GUILayout.Button("Set Reports on Close"))
		{
			FlurryBinding.setSessionReportsOnCloseEnabled(true);
		}
		if (GUILayout.Button("Set Reports on Pause"))
		{
			FlurryBinding.setSessionReportsOnPauseEnabled(true);
		}
		endColumn(true);
		if (GUILayout.Button("Enable Ads"))
		{
			FlurryBinding.enableAds(true);
		}
		if (GUILayout.Button("Fetch Ads"))
		{
			FlurryBinding.fetchAdForSpace("adSpace", FlurryAdSize.Bottom);
			FlurryBinding.fetchAdForSpace("splash", FlurryAdSize.Fullscreen);
		}
		if (GUILayout.Button("Check if Ad Available"))
		{
			bool flag = FlurryBinding.isAdAvailableForSpace("adSpace", FlurryAdSize.Bottom);
			Debug.Log("is ad available: " + flag);
		}
		if (GUILayout.Button("Show Ad on Bottom"))
		{
			FlurryBinding.displayAdForSpace("adSpace", FlurryAdSize.Bottom);
		}
		if (GUILayout.Button("Fetch and Show Ad"))
		{
			FlurryBinding.fetchAndDisplayAdForSpace("adSpace", FlurryAdSize.Top);
		}
		if (GUILayout.Button("Show Full Screen Ad"))
		{
			FlurryBinding.fetchAndDisplayAdForSpace("splash", FlurryAdSize.Fullscreen);
		}
		if (GUILayout.Button("Remove Ad"))
		{
			FlurryBinding.removeAdFromSpace("adSpace");
		}
		endColumn();
	}
}
