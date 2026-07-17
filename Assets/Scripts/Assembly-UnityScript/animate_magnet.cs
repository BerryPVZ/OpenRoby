using System;
using UnityEngine;

[Serializable]
public class animate_magnet : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (!GetComponent<Animation>().IsPlaying("magnet"))
		{
			GetComponent<Animation>().Play("magnet");
		}
	}

	public virtual void Main()
	{
	}
}
