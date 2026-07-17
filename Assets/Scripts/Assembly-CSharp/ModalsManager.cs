using System.Collections;
using UnityEngine;

public class ModalsManager : MonoBehaviour
{
	public static void PutPauseOnScreen()
	{
		if ((bool)GameObject.Find("level_paused_window"))
		{
			GameObject.Find("level_paused_window").transform.position = new Vector3(0f, 0f, 200f);
			GameObject.Find("level_paused_window").GetComponent<Animation>().Play("level_pause_01");
		}
	}

	public static IEnumerator PutOffScreen(string name)
	{
		switch (name)
		{
		case "level_paused_window":
			if ((bool)GameObject.Find(name))
			{
				GameObject.Find(name).GetComponent<Animation>().Play("level_pause_02");
				yield return new WaitForSeconds(0.5f);
				GameObject.Find(name).transform.position = new Vector3(4000f, 0f, 200f);
			}
			break;
		case "level_failed_window":
			if ((bool)GameObject.Find(name))
			{
				GameObject.Find(name).GetComponent<Animation>().Play("level_failed_02");
				yield return new WaitForSeconds(1f);
				GameObject.Find(name).transform.position = new Vector3(4000f, 0f, 200f);
			}
			break;
		case "level_complited_window":
		{
			if (!GameObject.Find(name))
			{
				break;
			}
			GameObject[] g = GameObject.FindGameObjectsWithTag("complete_star_particle");
			GameObject[] array = g;
			foreach (GameObject t in array)
			{
				if (!t.Equals(null))
				{
					Object.Destroy(t);
				}
			}
			GameObject.Find(name).GetComponent<Animation>().Play("level_completed_02");
			GameObject.Find(name).GetComponent<AudioSource>().Stop();
			break;
		}
		}
	}
}
