using UnityEngine;

public class Scaler : MonoBehaviour
{
	public Vector3 scale_iPod;

	public Vector3 scale_iPad;

	private void Start()
	{
		float num = Screen.width / Screen.height;
		float num2 = Screen.height / Screen.width;
		if (num2 > num)
		{
			num = num2;
		}
		if (num < 1.4f)
		{
			base.gameObject.transform.localScale = scale_iPad;
		}
		else
		{
			base.gameObject.transform.localScale = scale_iPod;
		}
	}

	private void Update()
	{
	}
}
