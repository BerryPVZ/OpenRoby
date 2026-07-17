using UnityEngine;

public class Shut_down_complete_stars : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Turn_off()
	{
		for (int i = 1; i < 4; i++)
		{
			GameObject.Find("star_GUI_" + i).GetComponent<Renderer>().enabled = false;
		}
	}
}
