using System;
using UnityEngine;

[Serializable]
public class move_modals : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void put_on_screen()
	{
		transform.position = new Vector3(0f, 0f, -350f);
	}

	public virtual void put_off_screen()
	{
		GameObject.Find("level_complited_window").transform.position = new Vector3(0f, 2000f, -350f);
	}

	public virtual void Main()
	{
	}
}
