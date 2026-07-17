using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	private void Start()
	{
		int pack = PlayerPrefs.GetInt("cur_pack");
		if (pack < 1)
		{
			pack = 1;
			PlayerPrefs.SetInt("cur_pack", pack);
		}

		float halfWidth = Proportion_manager.GetWidth() / 2f;
		SetPositionX("pack_" + pack + "_left", 0f - halfWidth);
		SetPositionX("pack_" + pack + "_right", halfWidth);
		if (pack == 1)
		{
			SetPositionX("lamps", 0f);
		}
		if (2f * halfWidth > 1024f)
		{
			SetScaleX("pack_" + pack + "_back", 2f * halfWidth / 1024f);
		}
	}

	private void SetPositionX(string objectName, float x)
	{
		GameObject target = GameObject.Find(objectName);
		if (target == null)
		{
			return;
		}
		Vector3 position = target.transform.position;
		position.x = x;
		target.transform.position = position;
	}

	private void SetScaleX(string objectName, float x)
	{
		GameObject target = GameObject.Find(objectName);
		if (target == null)
		{
			return;
		}
		Vector3 localScale = target.transform.localScale;
		localScale.x = x;
		target.transform.localScale = localScale;
	}
}
