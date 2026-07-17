using System.Runtime.InteropServices;
using UnityEngine;

public class iOSRateMe
{
	[DllImport("__Internal")]
	private static extern void n_askToRate(string appID, string title, string message, string rateButton, string remindMeButton, string cancelButton);

	public static void askToRate(string appID, string title, string message, string rateButton, string remindMeButton, string cancelButton)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			n_askToRate(appID, title, message, rateButton, remindMeButton, cancelButton);
		}
	}
}
