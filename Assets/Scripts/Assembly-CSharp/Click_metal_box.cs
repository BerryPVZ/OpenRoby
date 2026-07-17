using UnityEngine;

public class Click_metal_box : MonoBehaviour
{
	public AudioClip metal_sound;

	public string anim;

	private bool switcher = true;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		if (switcher)
		{
			switcher = false;
			base.GetComponent<Animation>().Play(anim);
			if (PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(metal_sound);
			}
		}
	}

	private void OnMouseUp()
	{
		switcher = true;
	}
}
