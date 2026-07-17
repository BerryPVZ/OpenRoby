using System.Collections;
using UnityEngine;

public class Manage_elmag_new : MonoBehaviour
{
	private Btn_activation_manager btn_manager;

	public bool power;

	private Vector3 touch_0;

	private GameObject[] metal_objects;

	public int magnetic_strength = 10;

	public float magnetic_distance = 100f;

	private Vector3 direction;

	private float angle;

	private void Start()
	{
		metal_objects = GameObject.FindGameObjectsWithTag("metal");
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		StartCoroutine(Run_magnet());
	}

	private void Update()
	{
		if (btn_manager.Button_active() && Input.touchCount > 0)
		{
			RaycastHit hitInfo = default(RaycastHit);
			touch_0.x = Input.GetTouch(0).position.x;
			touch_0.y = Input.GetTouch(0).position.y;
			Ray ray = Camera.main.ScreenPointToRay(touch_0);
			if (Input.GetTouch(0).phase == TouchPhase.Began && btn_manager.Button_active() && Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
			{
				power = !power;
			}
		}
	}

	private void OnMouseDown()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android && btn_manager.Button_active())
		{
			RaycastHit hitInfo = default(RaycastHit);
			touch_0.x = Input.mousePosition.x;
			touch_0.y = Input.mousePosition.y;
			Ray ray = Camera.main.ScreenPointToRay(touch_0);
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
			{
				power = !power;
			}
		}
	}

	private IEnumerator Run_magnet()
	{
		Vector3 magnet_vector = new Vector3(0f, 0f, 0f);
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (power)
			{
				if ((bool)GameObject.Find("robot4"))
				{
					GameObject.Find("robot4").GetComponent<Rigidbody>().isKinematic = false;
				}
				GameObject[] array = metal_objects;
				foreach (GameObject metal_object in array)
				{
					if (metal_object != null)
					{
						magnet_vector = base.transform.Find("magnet_dot_2").transform.position - base.transform.Find("magnet_dot_1").transform.position;
						direction = base.transform.position - metal_object.transform.position;
						angle = Vector3.Angle(magnet_vector, direction);
						if ((double)angle < 22.5 && (bool)metal_object.GetComponent<Rigidbody>())
						{
							metal_object.GetComponent<Rigidbody>().AddForce(direction.normalized * magnetic_strength * (magnetic_distance / direction.magnitude));
						}
					}
				}
				if (!base.GetComponent<Animation>().IsPlaying("electromagnet"))
				{
					base.GetComponent<Animation>().Play("electromagnet");
				}
			}
			else
			{
				base.GetComponent<Animation>().Stop();
			}
		}
	}
}
