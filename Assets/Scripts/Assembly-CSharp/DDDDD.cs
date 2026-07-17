using UnityEngine;

public class DDDDD : MonoBehaviour
{
	private Slider slider;

	private void Start()
	{
	}

	private void Update()
	{
		slider = GameObject.Find("pack_tube").GetComponent<Slider>();
		GetComponent<TextMesh>().text = slider.GetSlideFinished().ToString();
	}
}
