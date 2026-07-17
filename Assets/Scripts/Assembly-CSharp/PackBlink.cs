using UnityEngine;

public class PackBlink : MonoBehaviour
{
	private void Start()
	{
	}

	public void TurnOn()
	{
		GameObject.Find("btn_pack_obj_0" + PlayerPrefs.GetInt("cur_pack")).transform.Find("btn_pack_blink").GetComponent<Renderer>().enabled = true;
	}

	public void TurnOff()
	{
		GameObject.Find("btn_pack_obj_0" + PlayerPrefs.GetInt("cur_pack")).transform.Find("btn_pack_blink").GetComponent<Renderer>().enabled = false;
	}
}
