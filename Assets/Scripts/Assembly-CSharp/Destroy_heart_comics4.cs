using System.Collections;
using UnityEngine;

public class Destroy_heart_comics4 : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(destr());
	}

	private void Update()
	{
	}

	private IEnumerator destr()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (!GameObject.FindGameObjectWithTag("comics"))
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
