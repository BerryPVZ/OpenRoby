using UnityEngine;

public class Element2D
{
	public Keyframe2D[] keyframes;

	public Transform target;

	private int count;

	private float prevX;

	private float prevY;

	private float prevScaleX;

	private float prevScaleY;

	private float prevRotation;

	private float prevR = 1f;

	private float prevG = 1f;

	private float prevB = 1f;

	private float prevA = 1f;

	private float fadeX;

	private float fadeY;

	private float fadeScaleX;

	private float fadeScaleY;

	private float fadeRotation;

	private float fadeR = 1f;

	private float fadeG = 1f;

	private float fadeB = 1f;

	private float fadeA = 1f;

	private float initScaleX = 1f;

	private float initScaleY = 1f;

	private Color color;

	private Color[] colors = new Color[4];

	private Mesh mesh;

	public Element2D(Animation2D parent, string name, float x, float y, float scaleX, float scaleY, float rotation, float px1, float py1, float px2, float py2, float uvx1, float uvy1, float uvx2, float uvy2, float r, float g, float b, float a, int keyframes)
	{
		this.keyframes = new Keyframe2D[keyframes];
		GameObject gameObject = new GameObject("Element" + parent.transform.childCount + ((!(name != string.Empty)) ? string.Empty : (" : " + name)));
		target = gameObject.transform;
		target.parent = parent.transform;
		target.localPosition = new Vector3(x, y, -parent.transform.childCount);
		target.localScale = new Vector3(scaleX, scaleY, 1f);
		target.eulerAngles = new Vector3(0f, 0f, rotation);
		MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		MeshRenderer meshRenderer = (MeshRenderer)gameObject.AddComponent(typeof(MeshRenderer));
		mesh = new Mesh();
		meshRenderer.material = parent.material;
		Vector3[] array = new Vector3[4];
		int[] array2 = new int[6];
		Vector2[] array3 = new Vector2[4];
		array[0] = new Vector3(px1, py1, 0f);
		array[1] = new Vector3(px2, py1, 0f);
		array[2] = new Vector3(px1, py2, 0f);
		array[3] = new Vector3(px2, py2, 0f);
		array2[0] = 0;
		array2[1] = 1;
		array2[2] = 2;
		array2[3] = 1;
		array2[4] = 3;
		array2[5] = 2;
		array3[0] = new Vector2(uvx1, uvy1);
		array3[1] = new Vector2(uvx2, uvy1);
		array3[2] = new Vector2(uvx1, uvy2);
		array3[3] = new Vector2(uvx2, uvy2);
		color = new Color(r, g, b, a);
		colors[0] = color;
		colors[1] = color;
		colors[2] = color;
		colors[3] = color;
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.colors = colors;
		mesh.uv = array3;
		mesh.RecalculateNormals();
		meshFilter.sharedMesh = mesh;
		mesh.colors = colors;
	}

	public Element2D(Animation2D parent, string name, float x, float y, float scaleX, float scaleY, float rotation, int keyframes)
	{
		this.keyframes = new Keyframe2D[keyframes];
		target = parent.transform.Find(name);
		initScaleX = target.localScale.x;
		initScaleY = target.localScale.y;
		target.localPosition = new Vector3(x, y, target.localPosition.z);
		target.localScale = new Vector3(initScaleX * scaleX, initScaleY * scaleY, target.localScale.z);
		target.eulerAngles = new Vector3(0f, 0f, rotation);
	}

	public void AddKeyframe(Keyframe2D keyframe)
	{
		keyframes[count] = keyframe;
		count++;
	}

	public void StartFade(int frame)
	{
		prevX = target.localPosition.x;
		prevY = target.localPosition.y;
		prevScaleX = target.localScale.x / initScaleX;
		prevScaleY = target.localScale.y / initScaleX;
		prevRotation = target.eulerAngles.z;
		prevR = color.r;
		prevG = color.g;
		prevB = color.b;
		prevA = color.a;
		Keyframe2D keyframe2D = keyframes[0];
		Keyframe2D keyframe2D2 = null;
		for (int i = 0; i < keyframes.Length; i++)
		{
			Keyframe2D keyframe2D3 = keyframes[i];
			if (keyframe2D3.frame <= frame)
			{
				keyframe2D = keyframe2D3;
			}
			if (keyframe2D3.frame > frame && keyframe2D2 == null)
			{
				keyframe2D2 = keyframe2D3;
			}
		}
		float num;
		if (keyframe2D2 == null)
		{
			keyframe2D2 = keyframe2D;
			num = 1f;
		}
		else
		{
			num = ((float)frame - (float)keyframe2D.frame) / ((float)keyframe2D2.frame - (float)keyframe2D.frame);
		}
		fadeX = keyframe2D.x + num * (keyframe2D2.x - keyframe2D.x);
		fadeY = keyframe2D.y + num * (keyframe2D2.y - keyframe2D.y);
		fadeScaleX = keyframe2D.scaleX + num * (keyframe2D2.scaleX - keyframe2D.scaleX);
		fadeScaleY = keyframe2D.scaleY + num * (keyframe2D2.scaleY - keyframe2D.scaleY);
		float num2 = keyframe2D.rotation;
		float num3 = keyframe2D2.rotation;
		for (; num2 < 0f; num2 += 360f)
		{
		}
		for (; num3 < 0f; num3 += 360f)
		{
		}
		float num4 = num3 - 360f;
		if (Mathf.Abs(num3 - num2) > Mathf.Abs(num4 - num2))
		{
			num3 = num4;
		}
		num4 = num3 + 360f;
		if (Mathf.Abs(num3 - num2) > Mathf.Abs(num4 - num2))
		{
			num3 = num4;
		}
		fadeRotation = num2 + num * (num3 - num2);
		fadeR = keyframe2D.r + num * (keyframe2D2.r - keyframe2D.r);
		fadeG = keyframe2D.g + num * (keyframe2D2.g - keyframe2D.g);
		fadeB = keyframe2D.b + num * (keyframe2D2.b - keyframe2D.b);
		fadeA = keyframe2D.a + num * (keyframe2D2.a - keyframe2D.a);
	}

	public void Fade(float p)
	{
		float x = prevX + p * (fadeX - prevX);
		float y = prevY + p * (fadeY - prevY);
		target.localPosition = new Vector3(x, y, target.localPosition.z);
		x = prevScaleX + p * (fadeScaleX - prevScaleX);
		y = prevScaleY + p * (fadeScaleY - prevScaleY);
		target.localScale = new Vector3(x * initScaleX, y * initScaleY, target.localScale.z);
		float num = prevRotation;
		float num2 = fadeRotation;
		for (; num < 0f; num += 360f)
		{
		}
		for (; num2 < 0f; num2 += 360f)
		{
		}
		float num3 = num2 - 360f;
		if (Mathf.Abs(num2 - num) > Mathf.Abs(num3 - num))
		{
			num2 = num3;
		}
		num3 = num2 + 360f;
		if (Mathf.Abs(num2 - num) > Mathf.Abs(num3 - num))
		{
			num2 = num3;
		}
		target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, num + p * (num2 - num));
		if (mesh != null && (color.r != fadeR || color.g != fadeG || color.b != fadeB || color.a != fadeA))
		{
			color = new Color(prevR + p * (fadeR - prevR), prevG + p * (fadeG - prevG), prevB + p * (fadeB - prevB), prevA + p * (fadeA - prevA));
			colors[0] = (colors[1] = (colors[2] = (colors[3] = color)));
			mesh.colors = colors;
		}
	}

	public void Update(float frame)
	{
		Keyframe2D keyframe2D = null;
		Keyframe2D keyframe2D2 = null;
		for (int i = 0; i < keyframes.Length; i++)
		{
			Keyframe2D keyframe2D3 = keyframes[i];
			if ((float)keyframe2D3.frame <= frame)
			{
				keyframe2D = keyframe2D3;
			}
			if ((float)keyframe2D3.frame > frame && keyframe2D2 == null)
			{
				keyframe2D2 = keyframe2D3;
			}
		}
		float num;
		if (keyframe2D2 == null)
		{
			keyframe2D2 = keyframe2D;
			num = 1f;
		}
		else
		{
			num = (frame - (float)keyframe2D.frame) / (float)(keyframe2D2.frame - keyframe2D.frame);
		}
		float x = keyframe2D.x + num * (keyframe2D2.x - keyframe2D.x);
		float y = keyframe2D.y + num * (keyframe2D2.y - keyframe2D.y);
		target.localPosition = new Vector3(x, y, target.localPosition.z);
		x = keyframe2D.scaleX + num * (keyframe2D2.scaleX - keyframe2D.scaleX);
		y = keyframe2D.scaleY + num * (keyframe2D2.scaleY - keyframe2D.scaleY);
		target.localScale = new Vector3(x * initScaleX, y * initScaleY, target.localScale.z);
		float num2 = keyframe2D.rotation;
		float num3 = keyframe2D2.rotation;
		for (; num2 < 0f; num2 += 360f)
		{
		}
		for (; num3 < 0f; num3 += 360f)
		{
		}
		float num4 = num3 - 360f;
		if (Mathf.Abs(num3 - num2) > Mathf.Abs(num4 - num2))
		{
			num3 = num4;
		}
		num4 = num3 + 360f;
		if (Mathf.Abs(num3 - num2) > Mathf.Abs(num4 - num2))
		{
			num3 = num4;
		}
		target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, num2 + num * (num3 - num2));
		if (mesh != null && (color.r != keyframe2D2.r || color.g != keyframe2D2.g || color.b != keyframe2D2.b || color.a != keyframe2D2.a))
		{
			color = new Color(keyframe2D.r + num * (keyframe2D2.r - keyframe2D.r), keyframe2D.g + num * (keyframe2D2.g - keyframe2D.g), keyframe2D.b + num * (keyframe2D2.b - keyframe2D.b), keyframe2D.a + num * (keyframe2D2.a - keyframe2D.a));
			colors[0] = (colors[1] = (colors[2] = (colors[3] = color)));
			mesh.colors = colors;
		}
	}
}
