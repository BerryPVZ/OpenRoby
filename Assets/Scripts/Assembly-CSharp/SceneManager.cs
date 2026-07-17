using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);
		int pack = PlayerPrefs.GetInt("cur_pack");
		float x = Proportion_manager.GetWidth() / 2f;
		SetPositionX("restart", x);
		SetPositionX("pause", 0f - x);
		SetPositionX("wall_1", 0f - x);
		SetPositionX("wall_2", x);
	}

	private void SetPositionX(string name, float x)
	{
		Vector3 position = GameObject.Find(name).transform.position;
		position.x = x;
		GameObject.Find(name).transform.position = position;
	}

	private void SetScaleX(string name, float x)
	{
		Vector3 localScale = GameObject.Find(name).transform.localScale;
		localScale.x = x;
		GameObject.Find(name).transform.localScale = localScale;
	}
}
