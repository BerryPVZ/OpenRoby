using UnityEngine;

public class Slider_focus : MonoBehaviour
{
	public Slider slider;

	private Vector3 touch_0;

	private void Start()
	{
		slider = GameObject.Find("pack_tube").GetComponent<Slider>();
	}

	private void OnMouseDown()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		touch_0.x = Input.mousePosition.x;
		touch_0.y = Input.mousePosition.y;
		Ray ray = Camera.main.ScreenPointToRay(touch_0);
		if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject && hitInfo.collider.tag.Equals("pack") && Mathf.Abs(hitInfo.collider.transform.position.x) > 0.8f * slider.h_w && slider.play_focus)
		{
			slider.start = false;
			if (hitInfo.collider.transform.position.x < 0f)
			{
				StartCoroutine(slider.slide_page(true));
			}
			else
			{
				StartCoroutine(slider.slide_page(false));
			}
			slider.play_dovodchick = false;
			PackHelp.TurnOff();
		}
	}
}
