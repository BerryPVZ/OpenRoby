using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public double x;

	public double y;

	public double x1;

	public double x2;

	public double y1;

	public double y2;

	public double x1_h;

	public double x2_h;

	public double y1_h;

	public double y2_h;

	public double x_h;

	public double y_h;

	public string w;

	public bool victory_m;

	public bool victory_h;

	public AudioClip sound_fail;

	public AudioClip sound_fail_robot3;

	public AudioClip sound_win;

	public AudioClip sound_win_robot3;

	public AudioClip sound_win_robot4;

	public bool already_time_to_check;

	public int pack;

	private void Start()
	{
		x = 0.0;
		y = 0.0;
		x1 = 0.0;
		y1 = 0.0;
		x2 = 0.0;
		y2 = 0.0;
		x1_h = 0.0;
		x2_h = 0.0;
		y1_h = 0.0;
		y2_h = 0.0;
		x_h = 0.0;
		y_h = 0.0;
		pack = PlayerPrefs.GetInt("cur_pack");
	}

	private void Update()
	{
		Check_head();
	}

	private void Check_head()
	{
		if (!already_time_to_check)
		{
			return;
		}
		if (!pack.Equals(4))
		{
			if (!GameObject.Find("main_box_bottom"))
			{
				return;
			}
			x1 = GameObject.Find("main_box_bottom/main_box_fix1").transform.position.x;
			x2 = GameObject.Find("main_box_bottom/main_box_fix2").transform.position.x;
			y1 = GameObject.Find("main_box_bottom/main_box_fix1").transform.position.y;
			y2 = GameObject.Find("main_box_bottom/main_box_fix2").transform.position.y;
		}
		else
		{
			if (!GameObject.Find("main_box_4"))
			{
				return;
			}
			x1 = GameObject.Find("main_box_fix1").transform.position.x;
			x2 = GameObject.Find("main_box_fix2").transform.position.x;
			y1 = GameObject.Find("main_box_fix1").transform.position.y;
			y2 = GameObject.Find("main_box_fix2").transform.position.y;
		}
		switch (pack)
		{
		case 1:
			if (!victory_m && (bool)GameObject.Find("ball"))
			{
				x = GameObject.Find("ball").transform.position.x;
				y = GameObject.Find("ball").transform.position.y;
				if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
				{
					victory_m = true;
					Object.Destroy(GameObject.Find("ball"));
					already_time_to_check = false;
				}
			}
			break;
		case 2:
			x1_h = GameObject.Find("main_box_bottom_h/main_box_fix1").transform.position.x;
			x2_h = GameObject.Find("main_box_bottom_h/main_box_fix2").transform.position.x;
			y1_h = GameObject.Find("main_box_bottom_h/main_box_fix1").transform.position.y;
			y2_h = GameObject.Find("main_box_bottom_h/main_box_fix2").transform.position.y;
			if ((bool)GameObject.Find("robot3"))
			{
				x_h = GameObject.Find("robot3").transform.position.x;
				y_h = GameObject.Find("robot3").transform.position.y;
			}
			if ((bool)GameObject.Find("ball"))
			{
				x = GameObject.Find("ball").transform.position.x;
				y = GameObject.Find("ball").transform.position.y;
			}
			if (!victory_m && x >= x1 && x <= x2 && y >= y1 && y <= y2)
			{
				Object.Destroy(GameObject.Find("ball"));
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(sound_win);
				}
				victory_m = true;
				if (victory_h)
				{
					already_time_to_check = false;
					break;
				}
			}
			if (x_h >= x1 && x_h <= x2 && y_h >= y1 && y_h <= y2)
			{
				Object.Destroy(GameObject.Find("robot3"));
				x_h = 0.0;
				y_h = 0.0;
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(sound_fail_robot3);
				}
				GameObject.Find("level_failed_window").GetComponent<Animation>().Play("level_failed_01");
				victory_m = true;
				victory_h = true;
				already_time_to_check = false;
				break;
			}
			if (!victory_h && x_h >= x1_h && x_h <= x2_h && y_h >= y1_h && y_h <= y2_h)
			{
				Object.Destroy(GameObject.Find("robot3"));
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(sound_win_robot3);
				}
				victory_h = true;
				if (victory_m)
				{
					already_time_to_check = false;
					break;
				}
			}
			if (x >= x1_h && x <= x2_h && y >= y1_h && y <= y2_h)
			{
				Object.Destroy(GameObject.Find("ball"));
				x = 0.0;
				y = 0.0;
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(sound_fail);
				}
				GameObject.Find("level_failed_window").GetComponent<Animation>().Play("level_failed_01");
				victory_h = true;
				victory_m = true;
				already_time_to_check = false;
			}
			break;
		case 4:
			if (victory_m || !GameObject.Find("robot4"))
			{
				break;
			}
			x = GameObject.Find("robot4").transform.position.x;
			y = GameObject.Find("robot4").transform.position.y;
			if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
			{
				victory_m = true;
				Object.Destroy(GameObject.Find("robot4"));
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(sound_win_robot4);
				}
				already_time_to_check = false;
			}
			break;
		case 3:
			break;
		}
	}
}
