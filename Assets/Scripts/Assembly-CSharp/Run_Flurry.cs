using UnityEngine;

public class Run_Flurry : MonoBehaviour
{
	private string FLURRY_APP_ID;

	private void Start()
	{
		FLURRY_APP_ID = "2P3JWD7D8BSMNRRJCDZG";
		FlurryAndroidiOSManager.StartSession(FLURRY_APP_ID);
	}

	private void Update()
	{
	}

	public void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			S_C();
		}
		else
		{
			FlurryAndroidiOSManager.StartSession(FLURRY_APP_ID);
		}
	}

	public void OnApplicationQuit()
	{
		S_C();
	}

	private void S_C()
	{
		if (PlayerPrefs.GetInt("first_close") < 1)
		{
			PlayerPrefs.SetInt("first_close", 1);
			FlurryAndroidiOSManager.EventSimple("Completed levels in first run " + PlayerPrefs.GetInt("won_levels"));
		}
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		FlurryAndroidiOSManager.EventSimple("Last played pack " + num + " level " + num2);
	}
}
