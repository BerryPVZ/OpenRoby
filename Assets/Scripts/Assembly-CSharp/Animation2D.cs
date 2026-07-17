using System.Collections;
using UnityEngine;

public class Animation2D : MonoBehaviour
{
	public Element2D[] elements;

	public Material material;

	private bool _isLooping;

	private string[] labels;

	private Hashtable labelsLookup;

	private int count;

	private bool _isPlaying;

	private int framerate;

	private int frames;

	private float startTime;

	private int fadeAmount;

	private int fade;

	private int firstFrame;

	private int lastFrame;

	private int totalFrames;

	public bool isPlaying
	{
		get
		{
			return _isPlaying;
		}
	}

	public bool isLooping
	{
		get
		{
			return _isLooping;
		}
	}

	public void Initialize(int framerate, int frames, int elementcount)
	{
		this.framerate = framerate;
		this.frames = frames;
		elements = new Element2D[elementcount];
		labelsLookup = new Hashtable();
		labels = new string[frames];
	}

	public void AddElement(Element2D element)
	{
		elements[count] = element;
		count++;
	}

	public void AddLabel(int frame, string name)
	{
		labelsLookup[name] = frame;
		labels[frame] = name;
	}

	public void Play()
	{
		Play(0, frames - 1, true, 0);
	}

	public void Play(int fade)
	{
		Play(0, frames - 1, true, fade);
	}

	public void Play(bool loop)
	{
		Play(0, frames - 1, loop, 0);
	}

	public void Play(bool loop, int fade)
	{
		Play(0, frames - 1, loop, fade);
	}

	public void Play(string start)
	{
		Play(start, true, 0);
	}

	public void Play(string start, string end)
	{
		Play(start, end, true, 0);
	}

	public void Play(string start, bool loop)
	{
		Play(start, loop, 0);
	}

	public void Play(string start, string end, bool loop)
	{
		Play(start, end, loop, 0);
	}

	public void Play(string start, int fade)
	{
		Play(start, true, fade);
	}

	public void Play(string start, string end, int fade)
	{
		Play(start, end, true, fade);
	}

	public void Play(string start, bool loop, int fade)
	{
		Play((int)labelsLookup[start], loop, fade);
	}

	public void Play(string start, string end, bool loop, int fade)
	{
		Play((int)labelsLookup[start], (int)labelsLookup[end] - 1, loop, fade);
	}

	public void Play(int start, int end)
	{
		Play(start, end, true, 0);
	}

	public void Play(int start, bool loop)
	{
		Play(start, loop, 0);
	}

	public void Play(int start, int end, bool loop)
	{
		Play(start, end, loop, 0);
	}

	public void Play(int start, int end, int fade)
	{
		Play(start, end, true, fade);
	}

	public void Play(int start, bool loop, int fade)
	{
		int end = frames - 1;
		for (int i = start + 1; i < labels.Length; i++)
		{
			if (labels[i] != null && labels[i] != string.Empty)
			{
				end = i - 1;
			}
		}
		Play(start, end, loop, fade);
	}

	public void Play(int start, int end, bool loop, int fade)
	{
		if (end < start)
		{
			end = start;
		}
		firstFrame = start;
		lastFrame = end;
		totalFrames = end - start;
		startTime = Time.realtimeSinceStartup;
		_isPlaying = true;
		_isLooping = loop;
		this.fade = fade;
		for (int i = 0; i < elements.Length; i++)
		{
			if (this.fade > 0)
			{
				elements[i].StartFade(firstFrame);
			}
		}
		Update();
	}

	public void Stop()
	{
		_isPlaying = false;
	}

	public void Update()
	{
		if (!_isPlaying)
		{
			return;
		}
		float num = Time.realtimeSinceStartup - startTime;
		float num2 = (float)framerate * num;
		if (num2 < (float)fade)
		{
			float p = num2 / (float)fade;
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i].Fade(p);
			}
			return;
		}
		num2 -= (float)fade;
		num2 += (float)firstFrame;
		if (isLooping)
		{
			if (lastFrame == firstFrame)
			{
				num2 = firstFrame;
			}
			while (num2 > (float)lastFrame)
			{
				num2 -= (float)totalFrames;
			}
		}
		else if (num2 > (float)lastFrame)
		{
			Stop();
			return;
		}
		for (int j = 0; j < elements.Length; j++)
		{
			elements[j].Update(num2);
		}
	}

	public void Pose(string label)
	{
		Pose((int)labelsLookup[label]);
	}

	public void Pose(int frame)
	{
		for (int i = 0; i < elements.Length; i++)
		{
			elements[i].Update(frame);
		}
	}
}
