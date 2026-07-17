using System;
using UnityEngine;

[Serializable]
public class put_on_pause : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void PutOnPause()
	{
		if (Time.timeScale == 1f)
		{
			Time.timeScale = 0f;
		}
	}

	public virtual void put_on_shader()
	{
		GameObject.Find("Shader").transform.position = new Vector3(0f, 0f, -50f);
	}

	public virtual void put_off_shader()
	{
		GameObject.Find("Shader").transform.position = new Vector3(4000f, 0f, -50f);
	}

	public virtual void Main()
	{
	}
}
