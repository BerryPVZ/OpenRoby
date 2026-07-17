using System.Collections;
using UnityEngine;

public class BallDatchick : MonoBehaviour
{
	public GameObject target;

	public GameObject enemy;

	public bool main_datchick;

	private int pack;

	public bool victory;

	public bool fail;

	private AudioClip target_win;

	private AudioClip enemy_fail;

	private float t;

	public bool destroyed;

	private void Start()
	{
		destroyed = false;
		AudioRuntime.Ensure2DSource(base.gameObject);
		if (base.transform.parent.name.Equals("main_box_bottom"))
		{
			main_datchick = true;
		}
		if (main_datchick)
		{
			t = 0.05f;
		}
		else
		{
			t = 0.04f;
		}
		victory = false;
		fail = false;
		pack = PlayerPrefs.GetInt("cur_pack");
		Vector3 position = new Vector3(0f, 0f, 0f);
		switch (pack)
		{
		case 1:
			target = GameObject.Find("ball");
			position.x = GameObject.Find("main_box_1").transform.position.x;
			position.y = GameObject.Find("main_box_1").transform.position.y;
			target_win = AudioRuntime.LoadClip("sounds/MainRobo/MainRobo_win");
			break;
		case 2:
			if (main_datchick)
			{
				target = GameObject.Find("ball");
				enemy = GameObject.Find("robot3");
				position.x = GameObject.Find("main_box_bottom").transform.Find("main_box").transform.position.x;
				position.y = GameObject.Find("main_box_bottom").transform.Find("main_box").transform.position.y;
				target_win = AudioRuntime.LoadClip("sounds/MainRobo/MainRobo_win");
				enemy_fail = AudioRuntime.LoadClip("sounds/SoldierRobo/SRobo_fail");
			}
			else
			{
				target = GameObject.Find("robot3");
				enemy = GameObject.Find("ball");
				position.x = GameObject.Find("main_box_bottom_h").transform.Find("main_box").transform.position.x;
				position.y = GameObject.Find("main_box_bottom_h").transform.Find("main_box").transform.position.y;
				target_win = AudioRuntime.LoadClip("sounds/SoldierRobo/SRobo_win");
				enemy_fail = AudioRuntime.LoadClip("sounds/MainRobo/MainRobo_fail");
			}
			break;
		case 4:
			target = GameObject.Find("robot4");
			position.x = GameObject.Find("main_box_4").transform.Find("mBox_4").transform.position.x;
			position.y = GameObject.Find("main_box_4").transform.Find("mBox_4").transform.position.y;
			target_win = AudioRuntime.LoadClip("sounds/TechRobo/TechRobo_win");
			break;
		}
		position.z = target.transform.position.z;
		base.transform.position = position;
		StartCoroutine(CheckRobo());
	}

	private void Go()
	{
		VictoryManager component = GameObject.Find("Camera").GetComponent<VictoryManager>();
		GameObject box = ((!base.transform.parent.name.Equals("main_box_bottom")) ? GameObject.Find("main_box_bottom") : GameObject.Find("main_box_bottom_h"));
		StartCoroutine(component.RunHvatalkaPack2(box, base.gameObject.transform.parent.gameObject, pack));
	}

	private IEnumerator CheckRobo()
	{
		while (true)
		{
			yield return new WaitForSeconds(t);
			if (target != null && (target.transform.position - base.transform.position).magnitude < 25f)
			{
				Object.Destroy(target);
				destroyed = true;
				victory = true;
				GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
				GameObject tmp_s = new GameObject();
				tmp_s.transform.parent = GameObject.FindGameObjectWithTag("level").transform;
				tmp_s.AddComponent<AudioSource>();
				tmp_s.GetComponent<AudioSource>().clip = target_win;
				if (target.name.Equals("robot3"))
				{
					tmp_s.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (PlayerPreperencesManager.GetSound())
				{
					tmp_s.GetComponent<AudioSource>().Play();
				}
				if (enemy == null)
				{
					Scores s = GameObject.FindGameObjectWithTag("level").GetComponent<Scores>();
					s.StopTimer();
					VictoryManager vm = GameObject.Find("Camera").GetComponent<VictoryManager>();
					if (pack == 2)
					{
						Go();
					}
					else
					{
						vm.RunHvatalka(base.gameObject.transform.parent.gameObject, pack);
					}
				}
				else
				{
					string g = ((!base.transform.parent.name.Equals("main_box_bottom")) ? "main_box_bottom" : "main_box_bottom_h");
					BallDatchick bd = GameObject.Find(g).transform.Find("RoboDatchick").gameObject.GetComponent<BallDatchick>();
					if (bd.destroyed)
					{
						Go();
					}
				}
			}
			if (pack.Equals(2) && enemy != null && (enemy.transform.position - base.transform.position).magnitude < 25f)
			{
				Object.Destroy(enemy);
				fail = true;
				if ((bool)GameObject.Find("level_failed_window") && (!(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.y) < 100f) || !(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.x) < 100f)))
				{
					GameObject.Find("level_failed_window").transform.position = new Vector3(0f, 0f, 200f);
					GameObject.Find("level_failed_window").GetComponent<Animation>().Play("level_failed_01");
					if (PlayerPreperencesManager.GetSound())
					{
						AudioRuntime.Play(base.GetComponent<AudioSource>(), enemy_fail);
					}
				}
			}
		}
	}
}
