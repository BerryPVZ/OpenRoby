using System;
using UnityEngine;

public class RateMe
{
	private static int remindMeDelay = 432000;

	public static int getUnixTime()
	{
		return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
	}

	public static void askToRate(string appID, string title, string message, string rateButton, string remindMeButton, string cancelButton)
	{
		int num = PlayerPrefs.GetInt("rateme-plugin-futureaskdate", 0);
		if (num != -1 && getUnixTime() > num)
		{
			iOSRateMe.askToRate(appID, title, message, rateButton, remindMeButton, cancelButton);
		}
	}

	public static void setRemindMeDelay(TimeSpan newDelay)
	{
		remindMeDelay = (int)newDelay.TotalSeconds;
	}

	public static int getRemindMeDelay()
	{
		return remindMeDelay;
	}

	public static void resetState()
	{
		PlayerPrefs.SetInt("rateme-plugin-futureaskdate", 0);
		PlayerPrefs.Save();
	}
}
