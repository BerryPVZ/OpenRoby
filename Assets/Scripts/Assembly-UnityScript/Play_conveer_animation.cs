using System;
using UnityEngine;

[Serializable]
public class Play_conveer_animation : MonoBehaviour
{
	public string anim;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (!GetComponent<Animation>().IsPlaying(anim))
		{
			GetComponent<Animation>().Play(anim);
		}
	}
}
