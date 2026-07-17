using System;
using UnityEngine;

[Serializable]
public class set_scale_to_backgr : MonoBehaviour
{
	public virtual void Start()
	{
		double num = Screen.width * 300 / Screen.height;
		MonoBehaviour.print(num.ToString());
		if (!(2.0 * num <= 1024.0))
		{
			double num2 = 2.0 * num / 1024.0;
			Vector3 localScale = transform.localScale;
			float num3 = (localScale.x = (float)num2);
			Vector3 vector = (transform.localScale = localScale);
		}
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
