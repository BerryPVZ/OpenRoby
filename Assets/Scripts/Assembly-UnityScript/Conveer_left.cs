using System;
using UnityEngine;

[Serializable]
public class Conveer_left : MonoBehaviour
{
	public double force_ball;

	public double force_box;

	public double force_ball_reducer;

	public Conveer_left()
	{
		force_ball = -100.0;
		force_box = -50.0;
		force_ball_reducer = 2.0;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		ContactPoint contactPoint = collision.contacts[0];
		push_o(contactPoint.otherCollider, contactPoint.point);
	}

	public virtual void OnCollisionStay(Collision collision)
	{
		int i = 0;
		ContactPoint[] contacts = collision.contacts;
		for (int length = contacts.Length; i < length; i++)
		{
			push_o(contacts[i].otherCollider, contacts[i].point);
		}
	}

	public virtual void push_o(Collider body, Vector3 position)
	{
		Vector3 vector = default(Vector3);
		if (body.tag.Equals("ball"))
		{
			vector = new Vector3((float)(0.0 - force_ball), 0f, 0f);
			body.GetComponent<Rigidbody>().AddForceAtPosition(vector, position);
			body.GetComponent<Rigidbody>().AddForce(vector / (float)force_ball_reducer);
			return;
		}
		vector = new Vector3((float)(0.0 - force_box), 0f, 0f);
		if ((bool)body.GetComponent<Rigidbody>())
		{
			body.GetComponent<Rigidbody>().AddForce(vector);
			body.GetComponent<Rigidbody>().AddForceAtPosition(vector, position);
		}
	}
}
