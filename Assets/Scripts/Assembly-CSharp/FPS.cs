using UnityEngine;

public class FPS : MonoBehaviour
{
	private float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private float fps;

	private void Start()
	{
		timeleft = updateInterval;
	}

	public float Get_fps()
	{
		return fps;
	}

	private void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if ((double)timeleft <= 0.0)
		{
			GetComponent<TextMesh>().text = "FPS = " + (accum / (float)frames).ToString("f2");
			fps = accum / (float)frames;
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
