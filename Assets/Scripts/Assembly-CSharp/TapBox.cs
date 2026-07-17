using UnityEngine;

public class TapBox : Press_abstract_box
{
	private AudioClip sound_box_disappear;

	private AudioClip sound_block_disappear;

	private AudioClip sound_balk_disappear;

	private AudioClip sound_box_metal;

	private AudioClip sound_box_explode;

	private GameObject puff;

	private Object puff_box;

	private Object puff_block;

	private Object puff_explode;

	public string[] balls;

	public float radius = 5000f;

	public float explotion_power = 10000f;

	public VictoryPack3 vp3;

	private void Start()
	{
		sound_box_disappear = AudioRuntime.LoadClip("sounds/TapBox/box_wooden");
		sound_block_disappear = AudioRuntime.LoadClip("sounds/TapBox/block");
		sound_balk_disappear = AudioRuntime.LoadClip("sounds/TapBox/balk_wooden");
		sound_box_metal = AudioRuntime.LoadClip("sounds/TapBox/box_metal");
		sound_box_explode = AudioRuntime.LoadClip("sounds/TapBox/box_explode");
		puff_block = Resources.Load("puff_box_big");
		puff_box = Resources.Load("puff_box");
		puff_explode = Resources.Load("puff_bomb_box");
		balls = new string[4];
		balls[0] = "ball";
		balls[1] = "robot2";
		balls[2] = "robot3";
		balls[3] = "robot4";
	}

	public override void action(GameObject obj)
	{
		if ((bool)GameObject.Find("level_failed_window") && GameObject.Find("level_failed_window").GetComponent<Animation>().isPlaying)
		{
			return;
		}
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		if ((bool)GameObject.FindGameObjectWithTag("start_buble"))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("start_buble");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (!gameObject.Equals(null))
				{
					Object.Destroy(gameObject);
				}
			}
		}
		if ((bool)GameObject.FindGameObjectWithTag("backgr_obj"))
		{
			if (num < 4)
			{
				if (num < 3)
				{
					if (!GameObject.FindGameObjectWithTag("ball"))
					{
						return;
					}
				}
				else
				{
					VictoryPack3 component = GameObject.Find("ball").GetComponent<VictoryPack3>();
					if (component.GetVictory())
					{
						return;
					}
				}
			}
			else if (!GameObject.Find("robot4"))
			{
				return;
			}
		}
		if (obj.tag.Equals("balk"))
		{
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound_balk_disappear);
			}
			string path = "puff_balk/puff_" + obj.name;
			string text = "puff_" + obj.name;
			Object.Instantiate(Resources.Load(path));
			GameObject.Find(text + "(Clone)").transform.position = obj.transform.position;
			GameObject.Find(text + "(Clone)").transform.rotation = obj.transform.rotation;
		}
		if (obj.tag.Equals("box"))
		{
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound_box_disappear);
			}
			puff = (GameObject)Object.Instantiate(puff_box, obj.transform.position, Quaternion.identity);
			puff.transform.rotation = obj.transform.rotation;
			puff.GetComponent<Animation>().Play("puff_box_small");
		}
		if (obj.tag.Equals("block"))
		{
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound_block_disappear);
			}
			puff = (GameObject)Object.Instantiate(puff_block, obj.transform.position, Quaternion.identity);
			puff.transform.rotation = obj.transform.rotation;
			puff.GetComponent<Animation>().Play("puff_box_big");
		}
		if (obj.name.Equals("box_metal"))
		{
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound_box_metal);
			}
			obj.GetComponent<Animation>().Play("box_metal_shine");
		}
		if (!obj.tag.Equals("metal"))
		{
			KickBall();
		}
		if (obj.name.Equals("box_explode"))
		{
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound_box_explode);
			}
			puff = (GameObject)Object.Instantiate(puff_explode, obj.transform.position, Quaternion.identity);
			puff.transform.rotation = obj.transform.rotation;
			puff.GetComponent<Animation>().Play("puff_bomb_box");
			Vector3 position = obj.transform.position;
			Collider[] array3 = Physics.OverlapSphere(position, radius);
			Collider[] array4 = array3;
			foreach (Collider collider in array4)
			{
				if ((bool)collider.GetComponent<Rigidbody>())
				{
					collider.GetComponent<Rigidbody>().AddExplosionForce(explotion_power, position, radius, 1f, ForceMode.Force);
				}
			}
		}
		if (!obj.tag.Equals("metal") && !obj.tag.Equals("ball"))
		{
			Object.Destroy(obj);
		}
	}

	private void KickBall()
	{
		for (int i = 0; i < 4; i++)
		{
			if ((bool)GameObject.Find(balls[i]))
			{
				GameObject.Find(balls[i]).GetComponent<Rigidbody>().isKinematic = false;
				Vector3 position = GameObject.Find(balls[i]).transform.position;
				position.x += 0.01f;
				GameObject.Find(balls[i]).transform.position = position;
			}
		}
	}
}
