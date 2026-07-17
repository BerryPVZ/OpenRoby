using System;
using UnityEngine;

[Serializable]
public class Comics : MonoBehaviour
{
	public float pause_before_start_anim;

	public string anim;

	public virtual void Start()
	{
		if ((bool)GameObject.FindGameObjectWithTag("backgr_obj"))
		{
			int num = 2000;
			Vector3 position = GameObject.FindGameObjectWithTag("backgr_obj").transform.position;
			float num2 = (position.x = num);
			Vector3 vector = (GameObject.FindGameObjectWithTag("backgr_obj").transform.position = position);
		}
		play_anim();
	}

	public virtual void play_anim()
	{
		if (!anim.Equals(string.Empty))
		{
			GetComponent<Animation>().Play();
		}
		else
		{
			MonoBehaviour.print("type in the name of comics anim");
		}
	}

	public virtual void Create_obj(string path)
	{
		UnityEngine.Object.Instantiate(Resources.Load(path));
	}

	public virtual void Update()
	{
	}
}
