using System.Collections;
using UnityEngine;

public class Repeat_anim : MonoBehaviour
{
	public new string name;

	private void Start()
	{
		StartCoroutine(repeat());
	}

	private IEnumerator repeat()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (!base.GetComponent<Animation>().IsPlaying(name))
			{
				base.GetComponent<Animation>().Play(name);
			}
		}
	}

	private void Update()
	{
	}
}
