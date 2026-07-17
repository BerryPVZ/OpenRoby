using UnityEngine;

public abstract class Press_abstract_box : MonoBehaviour
{
	private Vector3 touch_0;

	public bool switcher;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.touchCount <= 0)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		touch_0.x = Input.GetTouch(0).position.x;
		touch_0.y = Input.GetTouch(0).position.y;
		Ray ray = Camera.main.ScreenPointToRay(touch_0);
		if (Input.GetTouch(0).phase != TouchPhase.Began)
		{
			return;
		}
		if (Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.transform.gameObject == base.gameObject)
			{
				action(hitInfo.transform.gameObject);
			}
			else
			{
				switcher = false;
			}
		}
		else
		{
			switcher = false;
		}
	}

	private void OnMouseUp()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android)
		{
			RaycastHit hitInfo = default(RaycastHit);
			touch_0.x = Input.mousePosition.x;
			touch_0.y = Input.mousePosition.y;
			Ray ray = Camera.main.ScreenPointToRay(touch_0);
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
			{
				action(hitInfo.transform.gameObject);
			}
		}
	}

	public abstract void action(GameObject g);
}
