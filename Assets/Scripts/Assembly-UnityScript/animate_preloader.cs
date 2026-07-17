using System;
using UnityEngine;

[Serializable]
public class animate_preloader : MonoBehaviour
{
	public virtual void Start()
	{
		Resources.UnloadUnusedAssets();
	}

	public virtual void Update()
	{
		if (!GetComponent<Animation>().IsPlaying("preloader"))
		{
			GetComponent<Animation>().Play("preloader");
		}
	}

	public virtual void Main()
	{
	}
}
