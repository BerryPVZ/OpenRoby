using UnityEngine;

public static class GUIHelpers
{
	public static float defaultLineHeight
	{
		get
		{
			return 22f;
		}
	}

	public static Vector3 MouseToGUIPosition(Vector3 v)
	{
		return new Vector3(v.x, (float)Screen.height - v.y, v.z);
	}

	public static float OnePixelInWorld(Camera camera, float depth)
	{
		return (camera.ScreenToWorldPoint(Vector3.up + Vector3.forward * depth) - camera.ScreenToWorldPoint(Vector3.zero + Vector3.forward * depth)).y;
	}

	public static GUISkin CopySkin(GUISkin source)
	{
		GUISkin gUISkin = ScriptableObject.CreateInstance(typeof(GUISkin)) as GUISkin;
		gUISkin.box = new GUIStyle(source.box);
		gUISkin.button = new GUIStyle(source.button);
		gUISkin.customStyles = new GUIStyle[source.customStyles.Length];
		for (int i = 0; i < gUISkin.customStyles.Length; i++)
		{
			gUISkin.customStyles[i] = new GUIStyle(source.customStyles[i]);
		}
		gUISkin.font = source.font;
		gUISkin.horizontalScrollbar = new GUIStyle(source.horizontalScrollbar);
		gUISkin.horizontalScrollbarLeftButton = new GUIStyle(source.horizontalScrollbarLeftButton);
		gUISkin.horizontalScrollbarRightButton = new GUIStyle(source.horizontalScrollbarRightButton);
		gUISkin.horizontalScrollbarThumb = new GUIStyle(source.horizontalScrollbarThumb);
		gUISkin.horizontalSlider = new GUIStyle(source.horizontalSlider);
		gUISkin.horizontalSliderThumb = new GUIStyle(source.horizontalSliderThumb);
		gUISkin.label = new GUIStyle(source.label);
		gUISkin.scrollView = new GUIStyle(source.scrollView);
		gUISkin.textArea = new GUIStyle(source.textArea);
		gUISkin.textField = new GUIStyle(source.textField);
		gUISkin.toggle = new GUIStyle(source.toggle);
		gUISkin.verticalScrollbar = new GUIStyle(source.verticalScrollbar);
		gUISkin.verticalScrollbarDownButton = new GUIStyle(source.verticalScrollbarDownButton);
		gUISkin.verticalScrollbarThumb = new GUIStyle(source.verticalScrollbarThumb);
		gUISkin.verticalScrollbarUpButton = new GUIStyle(source.verticalScrollbarUpButton);
		gUISkin.verticalSlider = new GUIStyle(source.verticalSlider);
		gUISkin.verticalSliderThumb = new GUIStyle(source.verticalSliderThumb);
		gUISkin.window = new GUIStyle(source.window);
		gUISkin.name = string.Format("{0} (Duplicate)", source);
		return gUISkin;
	}
}
