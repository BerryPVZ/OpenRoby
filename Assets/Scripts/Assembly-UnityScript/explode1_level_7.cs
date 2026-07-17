using System;
using UnityEngine;

[Serializable]
public class explode1_level_7 : MonoBehaviour
{
	public float radius;

	public float power_1;

	public float power_2;

	public AudioClip explode;

	public Transform puff_explode;

	public bool level_4;

	public explode1_level_7()
	{
		radius = 5000f;
		power_1 = 15000f;
		power_2 = 10000f;
	}

	public virtual void OnMouseDown()
	{
		if (!level_4)
		{
			if ((bool)GameObject.FindGameObjectWithTag("ball"))
			{
				GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>().isKinematic = false;
				float x = GameObject.FindGameObjectWithTag("ball").transform.position.x + 0.1f;
				Vector3 position = GameObject.FindGameObjectWithTag("ball").transform.position;
				float num = (position.x = x);
				Vector3 vector = (GameObject.FindGameObjectWithTag("ball").transform.position = position);
			}
		}
		else if ((bool)GameObject.Find("robot4"))
		{
			GameObject.Find("robot4").GetComponent<Rigidbody>().isKinematic = false;
			float x2 = GameObject.Find("robot4").transform.position.x + 0.1f;
			Vector3 position2 = GameObject.Find("robot4").transform.position;
			float num2 = (position2.x = x2);
			Vector3 vector3 = (GameObject.Find("robot4").transform.position = position2);
		}
		Vector3 position3 = this.transform.position;
		Collider[] array = Physics.OverlapSphere(position3, radius);
		if (!GameObject.Find("box_14"))
		{
			power_1 = power_2;
		}
		int i = 0;
		Collider[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if ((bool)array2[i] && (bool)array2[i].GetComponent<Rigidbody>())
			{
				array2[i].GetComponent<Rigidbody>().AddExplosionForce(power_1, position3, radius, 1f, ForceMode.Force);
				if (PlayerPreperencesManager.GetSound())
				{
					AudioRuntime.PlayOneShot(explode);
				}
			}
		}
		Transform transform = (Transform)UnityEngine.Object.Instantiate(puff_explode, this.transform.position, Quaternion.identity);
		transform.GetComponent<Animation>().Play("puff_bomb_box");
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Main()
	{
	}
}
