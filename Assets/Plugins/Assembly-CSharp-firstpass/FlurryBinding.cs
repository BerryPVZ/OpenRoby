using System.Collections.Generic;
using System.Runtime.InteropServices;
using Prime31;
using UnityEngine;

public class FlurryBinding
{
	[DllImport("__Internal")]
	private static extern void _flurryStartSession(string apiKey);

	public static void startSession(string apiKey)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryStartSession(apiKey);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryLogEvent(string eventName, bool isTimed);

	public static void logEvent(string eventName, bool isTimed)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryLogEvent(eventName, isTimed);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryLogEventWithParameters(string eventName, string parameters, bool isTimed);

	public static void logEventWithParameters(string eventName, Dictionary<string, string> parameters, bool isTimed)
	{
		if (parameters == null)
		{
			parameters = new Dictionary<string, string>();
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryLogEventWithParameters(eventName, parameters.toJson(), isTimed);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryEndTimedEvent(string eventName, string parameters);

	public static void endTimedEvent(string eventName)
	{
		endTimedEvent(eventName, new Dictionary<string, string>());
	}

	public static void endTimedEvent(string eventName, Dictionary<string, string> parameters)
	{
		if (parameters == null)
		{
			parameters = new Dictionary<string, string>();
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryEndTimedEvent(eventName, parameters.toJson());
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurrySetAge(int age);

	public static void setAge(int age)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurrySetAge(age);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurrySetGender(string gender);

	public static void setGender(string gender)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurrySetGender(gender);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurrySetUserId(string userId);

	public static void setUserId(string userId)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurrySetUserId(userId);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurrySetSessionReportsOnCloseEnabled(bool sendSessionReportsOnClose);

	public static void setSessionReportsOnCloseEnabled(bool sendSessionReportsOnClose)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurrySetSessionReportsOnCloseEnabled(sendSessionReportsOnClose);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurrySetSessionReportsOnPauseEnabled(bool setSessionReportsOnPauseEnabled);

	public static void setSessionReportsOnPauseEnabled(bool setSessionReportsOnPauseEnabled)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurrySetSessionReportsOnPauseEnabled(setSessionReportsOnPauseEnabled);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryAdsInitialize(bool enableTestAds);

	public static void enableAds(bool enableTestAds)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryAdsInitialize(enableTestAds);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryAdsSetUserCookies(string cookies);

	public static void adsSetUserCookies(Dictionary<string, string> cookies)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryAdsSetUserCookies(cookies.toJson());
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryAdsClearUserCookies();

	public static void adsClearUserCookies()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryAdsClearUserCookies();
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryAdsSetKeywords(string keywords);

	public static void adsSetKeywords(string keywords)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryAdsSetKeywords(keywords);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryAdsClearKeywords();

	public static void adsClearKeywords()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryAdsClearKeywords();
		}
	}

	[DllImport("__Internal")]
	private static extern bool _flurryFetchAdForSpace(string space, int adSize);

	public static void fetchAdForSpace(string space, FlurryAdSize adSize)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryFetchAdForSpace(space, (int)adSize);
		}
	}

	[DllImport("__Internal")]
	private static extern bool _flurryIsAdAvailableForSpace(string space, int adSize);

	public static bool isAdAvailableForSpace(string space, FlurryAdSize adSize)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _flurryIsAdAvailableForSpace(space, (int)adSize);
		}
		return false;
	}

	[DllImport("__Internal")]
	private static extern bool _flurryFetchAndDisplayAdForSpace(string space, int adSize);

	public static void fetchAndDisplayAdForSpace(string space, FlurryAdSize adSize)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryFetchAndDisplayAdForSpace(space, (int)adSize);
		}
	}

	[DllImport("__Internal")]
	private static extern bool _flurryDisplayAdForSpace(string space, int adSize);

	public static void displayAdForSpace(string space, FlurryAdSize adSize)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryDisplayAdForSpace(space, (int)adSize);
		}
	}

	[DllImport("__Internal")]
	private static extern void _flurryRemoveAdFromSpace(string space);

	public static void removeAdFromSpace(string space)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_flurryRemoveAdFromSpace(space);
		}
	}
}
