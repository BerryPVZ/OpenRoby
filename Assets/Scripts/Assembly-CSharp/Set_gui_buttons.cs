using UnityEngine;

public class Set_gui_buttons : MonoBehaviour
{
	public float x;

	private void Start()
	{
		x = (float)Screen.width / 2f * 600f / (float)Screen.height;
		set_x_pos("btnPauseInGame", 0f - x);
		set_x_pos("btnRestartInGame", x);
		set_x_pos("btnLeftLevelSelect", x);
		set_x_pos("btnRightLevelSelect", 0f - x);
		set_x_pos("help_parent", 0f - x);
	}

	private void Update()
	{
	}

	private void set_x_pos(string name, float pos)
	{
		Vector3 position = GameObject.Find(name).transform.position;
		position.x = pos;
		GameObject.Find(name).transform.position = position;
	}
}
