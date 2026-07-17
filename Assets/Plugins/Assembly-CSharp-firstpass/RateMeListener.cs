using System;
using UnityEngine;

public class RateMeListener : MonoBehaviour
{
	public Rating rating;

	public static event Action OnRemindMeClickedEvent;

	public static event Action OnRateClickedEvent;

	public static event Action OnCancelClickedEvent;

	private void OnRemindMeClicked(string dummy)
	{
		int value = RateMe.getUnixTime() + RateMe.getRemindMeDelay();
		PlayerPrefs.SetInt("rateme-plugin-futureaskdate", value);
		Rating.On_remind();
		PlayerPrefs.Save();
		if (RateMeListener.OnRemindMeClickedEvent != null)
		{
			RateMeListener.OnRemindMeClickedEvent();
		}
	}

	private void OnRateClicked(string dummy)
	{
		PlayerPrefs.SetInt("rateme-plugin-futureaskdate", -1);
		if (RateMeListener.OnRateClickedEvent != null)
		{
			RateMeListener.OnRateClickedEvent();
		}
	}

	private void OnCancelClicked(string dummy)
	{
		PlayerPrefs.SetInt("rateme-plugin-futureaskdate", -1);
		if (RateMeListener.OnCancelClickedEvent != null)
		{
			RateMeListener.OnCancelClickedEvent();
		}
	}
}
