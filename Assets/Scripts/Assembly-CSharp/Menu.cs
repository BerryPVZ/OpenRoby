using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public float pause_between_screens;

	public string screen;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void play_animation_pack(string move)
	{
		string empty = string.Empty;
		empty = "btn_pack_01";
		for (int i = 1; i < 5; i++)
		{
			if (Mathf.Abs(GameObject.Find("btn_pack_0" + i).transform.position.x) < 10f)
			{
				GameObject.Find("btn_pack_0" + i).GetComponent<Animation>().Play("pack_centr_" + move);
				if (i - 1 > 0)
				{
					GameObject.Find("btn_pack_0" + (i - 1)).GetComponent<Animation>().Play("pack_left_" + move);
				}
				if (i + 1 < 5)
				{
					GameObject.Find("btn_pack_0" + (i + 1)).GetComponent<Animation>().Play("pack_right_" + move);
				}
			}
		}
	}

	public IEnumerator change_screen(string first, string second)
	{
		switch (first)
		{
		case "main":
			GameObject.Find("btn_MM_play").GetComponent<Animation>().Play("btn_MM_Play_02");
			GameObject.Find("Social").GetComponent<Animation>().Play("social_02");
			GameObject.Find("Sound").GetComponent<Animation>().Play("option_02");
			GameObject.Find("lamp_left").GetComponent<Animation>().Play("lamp_left");
			GameObject.Find("game_Name").GetComponent<Animation>().Play("game_name_02");
			GameObject.Find("stoika").GetComponent<Animation>().Play("stoika_02");
			GameObject.Find("roof_01").GetComponent<Animation>().Play("roof_01_01");
			break;
		case "select_pack":
			play_animation_pack("02");
			if (second.Equals("main"))
			{
				GameObject.Find("btn_MM_back").GetComponent<Animation>().Play("btn_back_02");
			}
			break;
		case "pack1":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		case "pack2":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		case "pack3":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		case "pack4":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		case "pack":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		}
		yield return new WaitForSeconds(pause_between_screens);
		switch (second)
		{
		case "main":
			GameObject.Find("btn_MM_play").GetComponent<Animation>().Play("btn_MM_Play_01");
			GameObject.Find("Social").GetComponent<Animation>().Play("social_01");
			GameObject.Find("Sound").GetComponent<Animation>().Play("option_01");
			GameObject.Find("lamp_left").GetComponent<Animation>().Play("lamp_left_02");
			GameObject.Find("lamp_right").GetComponent<Animation>().Play("lamp_right_02");
			GameObject.Find("game_Name").GetComponent<Animation>().Play("game_name_01");
			GameObject.Find("stoika").GetComponent<Animation>().Play("stoika_01");
			GameObject.Find("roof_01").GetComponent<Animation>().Play("roof_01_02");
			break;
		case "select_pack":
		{
			play_animation_pack("01");
			if (first.Equals("main"))
			{
				GameObject.Find("btn_MM_back").GetComponent<Animation>().Play("btn_back_01");
			}
			int currentPack = Mathf.Max(1, PlayerPrefs.GetInt("cur_pack"));
			LevelLoader.LoadEpisodeBackground(currentPack, false);
			break;
		}
		case "pack1":
			LevelLoader.LoadEpisodeBackground(1, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack2":
			LevelLoader.LoadEpisodeBackground(2, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack3":
			LevelLoader.LoadEpisodeBackground(3, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack4":
			LevelLoader.LoadEpisodeBackground(4, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		}
		screen = second;
	}

	private IEnumerable change_screen_fast(string first, string second, float t)
	{
		switch (first)
		{
		case "main":
			GameObject.Find("btn_MM_play").GetComponent<Animation>().Play("btn_MM_Play_02");
			GameObject.Find("Social").GetComponent<Animation>().Play("social_02");
			GameObject.Find("Sound").GetComponent<Animation>().Play("option_02");
			GameObject.Find("lamp_left").GetComponent<Animation>().Play("lamp_left");
			GameObject.Find("lamp_right").GetComponent<Animation>().Play("lamp_right");
			GameObject.Find("game_Name").GetComponent<Animation>().Play("game_name_02");
			GameObject.Find("stoika").GetComponent<Animation>().Play("stoika_02");
			GameObject.Find("roof_01").GetComponent<Animation>().Play("roof_01_01");
			break;
		case "select_pack":
			play_animation_pack("02");
			if (second.Equals("main"))
			{
				GameObject.Find("btn_MM_back").GetComponent<Animation>().Play("btn_back_02");
			}
			break;
		case "pack1":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		case "pack2":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
			break;
		}
		if (t > 0f)
		{
			yield return new WaitForSeconds(t);
		}
		switch (second)
		{
		case "main":
			GameObject.Find("btn_MM_play").GetComponent<Animation>().Play("btn_MM_Play_01");
			GameObject.Find("Social").GetComponent<Animation>().Play("social_01");
			GameObject.Find("Sound").GetComponent<Animation>().Play("option_01");
			GameObject.Find("lamp_left").GetComponent<Animation>().Play("lamp_left_02");
			GameObject.Find("lamp_right").GetComponent<Animation>().Play("lamp_right_02");
			GameObject.Find("game_Name").GetComponent<Animation>().Play("game_name_01");
			GameObject.Find("stoika").GetComponent<Animation>().Play("stoika_01");
			GameObject.Find("roof_01").GetComponent<Animation>().Play("roof_01_02");
			break;
		case "select_pack":
		{
			play_animation_pack("01");
			if (first.Equals("main"))
			{
				GameObject.Find("btn_MM_back").GetComponent<Animation>().Play("btn_back_01");
			}
			int currentPack = Mathf.Max(1, PlayerPrefs.GetInt("cur_pack"));
			LevelLoader.LoadEpisodeBackground(currentPack, false);
			break;
		}
		case "pack1":
			LevelLoader.LoadEpisodeBackground(1, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack2":
			LevelLoader.LoadEpisodeBackground(2, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack3":
			LevelLoader.LoadEpisodeBackground(3, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack4":
			LevelLoader.LoadEpisodeBackground(4, false);
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		case "pack":
			GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_01");
			GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_01");
			break;
		}
		screen = second;
	}

	public void OpenRoof()
	{
		GameObject.Find("score_tablo").GetComponent<Animation>().Play("score_tablo_02");
		GameObject.Find("Select_Level_01").GetComponent<Animation>().Play("level_select_02");
		GameObject.Find("btn_MM_back").GetComponent<Animation>().Play("btn_back_02");
		GameObject.Find("roof_01").GetComponent<Animation>().Play("roof_01_open_01");
	}
}
