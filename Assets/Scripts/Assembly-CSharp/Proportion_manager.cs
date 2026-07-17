using System.Collections;
using UnityEngine;

public class Proportion_manager : MonoBehaviour
{
	public static float width;

	private void Start()
	{
		width = (float)Screen.width * 600f / (float)Screen.height;
		SetMenu();
	}

	public void SetMenu()
	{
		if ((bool)GameObject.FindGameObjectWithTag("menu"))
		{
			set_x_pos("Btn_MM_Back", width / 2f + 5f);
			set_x_pos("BtnSocial", 0f - width / 2f - 5f);
			set_x_pos("BtnSound", width / 2f + 5f);
			set_x_pos("Btn_Pack_02", (0f - width) / 2f);
			set_x_pos("Btn_Pack_03", (0f - width) * 2f / 2f);
			set_x_pos("Btn_Pack_04", (0f - width) * 3f / 2f);
			set_x_pos("Score_Tablo", width / 2f - 100f);
			set_x_pos("Stoika", 0f - width / 2f + 123f);
			if (!base.gameObject.name.Equals("Menu(Clone)"))
			{
				PlayerPreperencesManager.WritePacksY();
			}
			StartCoroutine(Change_packs_scale());
		}
	}

	public static float GetWidth()
	{
		return width;
	}

	private void set_x_pos(string name, float pos)
	{
		Vector3 position = GameObject.Find(name).transform.position;
		position.x = pos;
		GameObject.Find(name).transform.position = position;
	}

	private void Update()
	{
	}

	private IEnumerator Change_packs_scale()
	{
		while (true)
		{
			Vector3 tmp = new Vector3(0f, 0f, 0f);
			if ((bool)GameObject.FindGameObjectWithTag("menu"))
			{
				for (int i = 1; i < 5; i++)
				{
					GameObject.Find("btn_pack_0" + i).transform.localScale = Set_scale(i);
				}
			}
			yield return new WaitForSeconds(0.03f);
		}
	}

	private Vector3 Set_scale(int i)
	{
		if (Mathf.Abs(GameObject.Find("btn_pack_0" + i).transform.position.x) < GetH_w())
		{
			return new Vector3(1f, 1f, 1f) * (8f + 2f * (1f - Mathf.Abs(GameObject.Find("btn_pack_0" + i).transform.position.x / GetH_w())));
		}
		return new Vector3(8f, 8f, 8f);
	}

	public float GetH_w()
	{
		return width / 2f;
	}
}
