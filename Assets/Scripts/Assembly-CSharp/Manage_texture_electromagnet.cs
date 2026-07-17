using UnityEngine;

public class Manage_texture_electromagnet : MonoBehaviour
{
	public Texture2D p_on;

	public Texture2D p_rel;

	public int level_number;

	public bool power;

	private Vector3 touch_0;

	private void Start()
	{
		if (!power)
		{
			GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
		}
		else
		{
			GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
		}
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
		if (!Physics.Raycast(ray, out hitInfo))
		{
			return;
		}
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (hitInfo.transform.gameObject == base.gameObject && (bool)GameObject.Find("electromagnet_btn"))
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
			}
			else if (!power && (bool)GameObject.Find("electromagnet_btn"))
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
			}
		}
		if (Input.GetTouch(0).phase == TouchPhase.Ended && hitInfo.transform.gameObject == base.gameObject)
		{
			power = !power;
			if (power && (bool)GameObject.Find("electromagnet_btn"))
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
			}
			else if ((bool)GameObject.Find("electromagnet_btn"))
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
			}
		}
	}

	private void OnMouseDown()
	{
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			power = !power;
			if (power)
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
			}
			else
			{
				GameObject.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
			}
		}
	}
}
