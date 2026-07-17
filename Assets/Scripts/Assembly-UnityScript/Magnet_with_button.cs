using System;
using UnityEngine;

[Serializable]
public class Magnet_with_button : MonoBehaviour
{
	public GameObject[] metal_objects;

	public int magnetic_strength;

	public double magnetic_distance;

	public Vector3 direction;

	public float angle;

	public bool power;

	public Magnet_with_button()
	{
		magnetic_strength = 10;
		magnetic_distance = 100.0;
	}

	public virtual void Magnet()
	{
	}

	public virtual void Start()
	{
		metal_objects = null;
		metal_objects = GameObject.FindGameObjectsWithTag("metal");
		int i = 0;
		GameObject[] array = metal_objects;
		for (int length = array.Length; i < length; i++)
		{
		}
	}

	public virtual void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			RaycastHit hitInfo = default(RaycastHit);
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			if (Physics.Raycast(ray, out hitInfo, 2000f) && hitInfo.transform.gameObject.Equals(gameObject))
			{
				power = !power;
			}
		}
		if (power)
		{
			if ((bool)GameObject.Find("robot4") && GameObject.Find("robot4").GetComponent<Rigidbody>().isKinematic)
			{
				GameObject.Find("robot4").GetComponent<Rigidbody>().isKinematic = false;
			}
			run_magnet();
			if (!GetComponent<Animation>().IsPlaying("electromagnet"))
			{
				GetComponent<Animation>().Play("electromagnet");
			}
		}
	}

	public virtual void OnMouseDown()
	{
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo, 2000f) && hitInfo.transform.gameObject == gameObject)
		{
			power = !power;
		}
	}

	public virtual void run_magnet()
	{
		Vector3 vector = default(Vector3);
		int i = 0;
		GameObject[] array = metal_objects;
		for (int length = array.Length; i < length; i++)
		{
			if (array[i] != null)
			{
				vector = GameObject.Find("magnet_dot_2").transform.position - GameObject.Find("magnet_dot_1").transform.position;
				direction = transform.position - array[i].transform.position;
				angle = Vector3.Angle(vector, direction);
				if (!(angle >= 22.5f))
				{
					array[i].GetComponent<Rigidbody>().AddForce(direction.normalized * magnetic_strength * (float)(magnetic_distance / (double)direction.magnitude));
				}
			}
		}
	}
}
