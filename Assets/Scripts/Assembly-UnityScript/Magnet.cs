using System;
using UnityEngine;

[Serializable]
public class Magnet : MonoBehaviour
{
	public GameObject[] metal_objects;

	public int magnetic_strength;

	public double magnetic_distance;

	public Vector3 direction;

	public float angle;

	public Magnet()
	{
		magnetic_strength = 10;
		magnetic_distance = 100.0;
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
		run_magnet();
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
				vector = transform.Find("magnet_dot_2").transform.position - transform.Find("magnet_dot_1").transform.position;
				direction = transform.position - array[i].transform.position;
				angle = Vector3.Angle(vector, direction);
				if (!(angle >= 22.5f) && (bool)array[i].GetComponent<Rigidbody>())
				{
					array[i].GetComponent<Rigidbody>().AddForce(direction.normalized * magnetic_strength * (float)(magnetic_distance / (double)direction.magnitude));
				}
			}
		}
	}
}
