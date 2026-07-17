using System;
using UnityEngine;

[Serializable]
public class ConveerManager : MonoBehaviour
{
	public double force_ball = 1.0;
	public double force_box = 1.0;

	public virtual void Start()
	{
		foreach (Transform child in transform)
		{
			Conveer_left conveyor = child.GetComponent<Conveer_left>();
			if (conveyor != null)
			{
				conveyor.force_ball = force_ball;
				conveyor.force_box = force_box;
			}
		}
	}

	public virtual void Update() { }
	public virtual void Main() { }
}
