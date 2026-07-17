using System.Collections;
using UnityEngine;

public class Destroy_particle : MonoBehaviour
{
	public float timer = 1f;

	private void Start()
	{
		StartCoroutine(destr());
	}

	private void Update()
	{
	}

	private IEnumerator destr()
	{
		yield return new WaitForSeconds(timer);
		Object.Destroy(base.gameObject);
	}
}
