using System.Collections;
using UnityEngine;

public class Rating : MonoBehaviour
{
	public bool free_version = true;

	public int min_starts;

	public int min_days = 3;

	public float pause_before_showing = 2f;

	public bool game_exit;

	public string URL = string.Empty;

	private static string APP_ID = "441508855";

	private void Start()
	{
	}

	private IEnumerator Show()
	{
		yield return new WaitForSeconds(pause_before_showing);
		RateMe.askToRate(APP_ID, "Like This App?", "Please rate it and don't forget to +1 it", "Rate It!", "Remind Me Later", "No Thanks");
	}

	public static void On_remind()
	{
		PlayerPrefs.SetInt("open_counter", 0);
		PlayerPrefs.SetInt("min_starts", 4);
	}
}
