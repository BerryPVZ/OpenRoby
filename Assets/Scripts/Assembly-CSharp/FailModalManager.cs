using UnityEngine;

public class FailModalManager : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Stop_xvatalki()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("xvatalka");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (!gameObject.Equals(null))
			{
				Object.Destroy(gameObject);
			}
		}
	}

	public void SendFailFllurry()
	{
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		FlurryAndroidiOSManager.EventSimple("Fail pack " + num + " level " + num2);
	}
}
