using System.Collections;
using UnityEngine;

public class Bubble_ok : MonoBehaviour
{
	private bool works = true;

	public string n;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("bubble" + n + "(Clone)").GetComponent<Animation>().Play("bubble" + n + "_1");
		while (works)
		{
			yield return new WaitForSeconds(0.1f);
		}
		Object.Destroy(GameObject.Find("bubble" + n + "(Clone)"));
	}

	private void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			RaycastHit hitInfo = default(RaycastHit);
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject && works)
			{
				GameObject.Find("bubble" + n + "(Clone)").GetComponent<Animation>().Play("bubble" + n + "_2");
				works = false;
			}
		}
	}

	private void OnMouseDown()
	{
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
		{
			GameObject.Find("bubble" + n + "(Clone)").GetComponent<Animation>().Play("bubble" + n + "_2");
			works = false;
		}
	}
}
