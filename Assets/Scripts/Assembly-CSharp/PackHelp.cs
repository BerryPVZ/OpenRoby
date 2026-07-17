using UnityEngine;

public class PackHelp : MonoBehaviour
{
	private static bool is_on;

	private static Vector3 location;

	private static string amount;

	private void Start()
	{
		is_on = false;
		location = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		if (is_on)
		{
			GUI.Box(new Rect(location.x - 100f, location.y - 100f, 200f, 200f), "Chapter Locked.\n\n Earn " + amount + " stars to unlock it.");
			if (!GUI.Button(new Rect(location.x - 50f, location.y + 25f, 100f, 50f), "Ok"))
			{
			}
		}
	}

	public static void TurnOn(Vector3 loc, string m)
	{
		is_on = true;
		amount = m;
	}

	public static void TurnOff()
	{
		is_on = false;
	}

	public static bool GetValue()
	{
		return is_on;
	}

	public static void Switch(Vector3 loc, string m)
	{
		if (!is_on)
		{
			is_on = true;
			amount = m;
		}
		else
		{
			is_on = false;
		}
	}
}
