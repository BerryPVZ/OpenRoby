using UnityEngine;

public class AnimationStop : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StopAnim()
	{
		base.GetComponent<Animation>().Stop();
	}
}
