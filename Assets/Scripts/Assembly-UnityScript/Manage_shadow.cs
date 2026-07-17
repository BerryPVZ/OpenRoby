using System;
using UnityEngine;

[Serializable]
public class Manage_shadow : MonoBehaviour
{
	public double shadow_position;

	public double g;

	public Texture2D p_0;

	public Texture2D p_1;

	public Texture2D p_2;

	public Texture2D p_3;

	public Texture2D p_4;

	public Texture2D p_5;

	public Texture2D p_6;

	public Texture2D p_7;

	public Texture2D p_8;

	public Texture2D p_9;

	public Texture2D p_10;

	public double cos_1;

	public double cos_2;

	public double angle;

	public double object_width;

	public Manage_shadow()
	{
		shadow_position = -274.0;
		angle = 90.0;
		object_width = 152.0;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		double num = shadow_position;
		Vector3 position = transform.Find("shadow").position;
		float num2 = (position.y = (float)num);
		Vector3 vector = (transform.Find("shadow").position = position);
		float x = transform.position.x;
		Vector3 position2 = transform.Find("shadow").position;
		float num3 = (position2.x = x);
		Vector3 vector3 = (transform.Find("shadow").position = position2);
		int num4 = 0;
		Quaternion rotation = transform.Find("shadow").rotation;
		float num5 = (rotation.z = num4);
		Quaternion quaternion = (transform.Find("shadow").rotation = rotation);
		cos_1 = Mathf.Abs(Mathf.Cos((float)(((double)transform.rotation.eulerAngles.z - angle / 2.0) * 2.0 * 3.140000104904175 / 360.0)));
		cos_2 = Mathf.Abs(Mathf.Cos((float)(((double)transform.rotation.eulerAngles.z + angle / 2.0) * 2.0 * 3.140000104904175 / 360.0)));
		if (!(cos_2 <= cos_1))
		{
			cos_1 = cos_2;
		}
		double num6 = object_width * (double)Mathf.Sqrt(2f) * cos_1;
		Vector3 localScale = transform.Find("shadow").localScale;
		float num7 = (localScale.x = (float)num6);
		Vector3 vector5 = (transform.Find("shadow").localScale = localScale);
		double num8 = 95.0;
		g = (double)(-137f - transform.position.y) / num8;
		set_texture(g);
	}

	public virtual void set_texture(double g)
	{
		if (!(g > 0.0))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_0;
		}
		else if (!(g > 0.10000000149011612))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_1;
		}
		else if (!(g > 0.20000000298023224))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_2;
		}
		else if (!(g > 0.30000001192092896))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_3;
		}
		else if (!(g > 0.4000000059604645))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_4;
		}
		else if (!(g > 0.5))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_5;
		}
		else if (!(g > 0.6000000238418579))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_6;
		}
		else if (!(g > 0.699999988079071))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_7;
		}
		else if (!(g > 0.800000011920929))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_8;
		}
		else if (!(g > 0.8999999761581421))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_9;
		}
		else if (!(g <= 0.8999999761581421))
		{
			transform.Find("shadow").GetComponent<Renderer>().material.mainTexture = p_10;
		}
	}
}
