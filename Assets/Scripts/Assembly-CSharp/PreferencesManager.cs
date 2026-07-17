using UnityEngine;

public class PreferencesManager : MonoBehaviour
{
	private void Start()
	{
		Screen.sleepTimeout = -1;
		Input.multiTouchEnabled = false;
	}

	private void Update()
	{
	}
}
