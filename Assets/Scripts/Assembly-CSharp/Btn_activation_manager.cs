using System.Collections;
using UnityEngine;

public class Btn_activation_manager : MonoBehaviour
{
	private bool buttons_active = true;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Disactivate(float t)
	{
		if (buttons_active)
		{
			StartCoroutine(Disactivate_btns(t));
		}
	}

	private IEnumerator Disactivate_btns(float t)
	{
		buttons_active = false;
		yield return new WaitForSeconds(t);
		buttons_active = true;
	}

	public bool Button_active()
	{
		if (buttons_active)
		{
			return true;
		}
		return false;
	}
}
