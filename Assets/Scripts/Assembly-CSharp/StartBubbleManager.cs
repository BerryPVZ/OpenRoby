using System.Collections;
using UnityEngine;

public class StartBubbleManager : MonoBehaviour
{
	public string parent;

	private void Start()
	{
		if (parent.Equals(null))
		{
			MonoBehaviour.print("Which is my roby?");
		}
		else
		{
			base.transform.parent = GameObject.FindGameObjectWithTag("level").transform;
			Vector3 position = GameObject.Find(parent).transform.position;
			position.y += 12f;
			base.transform.position = position;
		}
		StartCoroutine(Destr());
	}

	private void Update()
	{
	}

	private IEnumerator Destr()
	{
		yield return new WaitForSeconds(3f);
		Object.Destroy(base.gameObject);
	}
}
