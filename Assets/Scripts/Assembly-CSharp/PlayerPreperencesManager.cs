using UnityEngine;

public class PlayerPreperencesManager : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void InitializeAudioDefaults()
	{
		EnsureAudioPreferences();
	}

	private static void EnsureAudioPreferences()
	{
		string soundValue = PlayerPrefs.GetString("sound");
		if (soundValue != "true" && soundValue != "false")
		{
			PlayerPrefs.SetString("sound", "true");
		}

		string musicValue = PlayerPrefs.GetString("music");
		if (musicValue != "true" && musicValue != "false")
		{
			PlayerPrefs.SetString("music", "true");
		}
	}

	private void Start()
	{
		EnsureAudioPreferences();
		if (PlayerPrefs.GetInt("Bundle version 1.4") < 1)
		{
			PlayerPrefs.SetInt("Bundle version 1.4", 1);
			PlayerProgressParser.ReadProgress();
		}
		int currentPack = Mathf.Clamp(PlayerPrefs.GetInt("cur_pack"), 1, 4);
		PlayerPrefs.SetInt("cur_pack", currentPack);

		LevelLoader.SyncLevelCountsToPrefs();
		int currentPackTotal = PlayerPrefs.GetInt("total_levels_pack_" + currentPack);
		int currentLevel = currentPackTotal > 0 ? Mathf.Clamp(PlayerPrefs.GetInt("cur_level"), 1, currentPackTotal) : 1;
		PlayerPrefs.SetInt("cur_level", currentLevel);
		PlayerPrefs.SetInt("min_stars_pack_2", 0);
		PlayerPrefs.SetInt("min_stars_pack_3", 0);
		PlayerPrefs.SetInt("min_stars_pack_4", 0);
		for (int i = 1; i <= 4; i++)
		{
			int total = PlayerPrefs.GetInt("total_levels_pack_" + i);
			if (total > 0)
			{
				int won = PlayerPrefs.GetInt("won_levels_pack_" + i);
				PlayerPrefs.SetInt("won_levels_pack_" + i, Mathf.Clamp(won < 1 ? 1 : won, 1, total));
			}
		}
		PlayerPrefs.Save();
	}

	private void OpenAllLevels()
	{
		LevelLoader.SyncLevelCountsToPrefs();
		for (int pack = 1; pack <= 4; pack++)
		{
			int total = PlayerPrefs.GetInt("total_levels_pack_" + pack);
			for (int level = 1; level <= total; level++)
			{
				PlayerPrefs.SetInt("stars_pack_" + pack + "_level_" + level, 3);
			}
			PlayerPrefs.SetInt("won_levels_pack_" + pack, total);
		}
		PlayerPrefs.Save();
	}

	public static bool GetSound()
	{
		EnsureAudioPreferences();
		return PlayerPrefs.GetString("sound") == "true";
	}

	public static void TurnSoundOff()
	{
		PlayerPrefs.SetString("sound", "false");
		PlayerPrefs.Save();
	}

	public static void TurnSoundOn()
	{
		PlayerPrefs.SetString("sound", "true");
		PlayerPrefs.Save();
	}

	public static bool GetMusic()
	{
		EnsureAudioPreferences();
		return PlayerPrefs.GetString("music") == "true";
	}

	public static void TurnMusicOff()
	{
		PlayerPrefs.SetString("music", "false");
		PlayerPrefs.Save();
	}

	public static void TurnMusicOn()
	{
		PlayerPrefs.SetString("music", "true");
		PlayerPrefs.Save();
	}

	public void SetPack(int i)
	{
		PlayerPrefs.SetInt("cur_pack", i);
	}

	public static int GetPack()
	{
		return PlayerPrefs.GetInt("cur_pack");
	}

	public static void SetLevel(int i)
	{
		PlayerPrefs.SetInt("cur_level", i);
	}

	public static void RefreshStarsCounter()
	{
		PlayerPrefs.SetInt("stars_level_" + PlayerPrefs.GetInt("cur_level") + "_", 0);
	}

	public static int GetCollectedStars()
	{
		int num = PlayerPrefs.GetInt("stars_level_" + PlayerPrefs.GetInt("cur_level") + "_");
		if (num < 3)
		{
			return num;
		}
		return 2;
	}

	public static void AddStar()
	{
		PlayerPrefs.SetInt("stars_level_" + PlayerPrefs.GetInt("cur_level") + "_", GetCollectedStars() + 1);
	}

	public static void SetScoresProgress(int score)
	{
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		int num3 = PlayerPrefs.GetInt("scores_pack_" + num + "_level_" + num2);
		if (score > num3)
		{
			PlayerPrefs.SetInt("scores_pack_" + num + "_level_" + num2, score);
		}
	}

	public static void SetStarsProgress()
	{
		int num = PlayerPrefs.GetInt("cur_level");
		int num2 = PlayerPrefs.GetInt("cur_pack");
		int num3 = PlayerPrefs.GetInt("stars_level_" + num + "_");
		int num4 = PlayerPrefs.GetInt("stars_pack_" + num2 + "_level_" + num);
		if (num3 > num4)
		{
			PlayerPrefs.SetInt("stars_pack_" + num2 + "_level_" + num, num3);
		}
	}

	public static int GetScoresProgress()
	{
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		return PlayerPrefs.GetInt("scores_pack_" + num + "_level_" + num2);
	}

	public static bool GoNextLevel()
	{
		int num = PlayerPrefs.GetInt("cur_level");
		int num2 = PlayerPrefs.GetInt("total_levels_pack_" + PlayerPrefs.GetInt("cur_pack"));
		if (num < num2)
		{
			return true;
		}
		return false;
	}

	public static void SetWonLevels()
	{
		int level = PlayerPrefs.GetInt("cur_level");
		int pack = PlayerPrefs.GetInt("cur_pack");
		int total = PlayerPrefs.GetInt("total_levels_pack_" + pack);
		int unlockedThrough = Mathf.Clamp(level + 1, 1, Mathf.Max(1, total));
		if (PlayerPrefs.GetInt("won_levels_pack_" + pack) < unlockedThrough)
		{
			PlayerPrefs.SetInt("won_levels_pack_" + pack, unlockedThrough);
		}
	}

	public static void WritePacksY()
	{
		PlayerPrefs.SetFloat("btn_pack_01", GameObject.Find("btn_pack_01").transform.position.y);
		PlayerPrefs.SetFloat("btn_pack_02", GameObject.Find("btn_pack_02").transform.position.y);
	}

	public static int GetWonLevels(int pack)
	{
		return PlayerPrefs.GetInt("won_levels_pack_" + pack);
	}

	public static int GetScoresProgress(int pack, int level)
	{
		return PlayerPrefs.GetInt("scores_pack_" + pack + "_level_" + level);
	}

	public static int GetStarsProgress(int pack, int level)
	{
		return PlayerPrefs.GetInt("stars_pack_" + pack + "_level_" + level);
	}

	public static void SetWonLevels(int pack, int x)
	{
		if (GetWonLevels(pack) < x)
		{
			PlayerPrefs.SetInt("won_levels_pack_" + pack, x);
		}
	}

	public static void SetScoresProgress(int pack, int level, int x)
	{
		if (GetScoresProgress(pack, level) < x)
		{
			PlayerPrefs.SetInt("scores_pack_" + pack + "_level_" + level, x);
		}
	}

	public static void SetStarsProgress(int pack, int level, int x)
	{
		if (GetStarsProgress(pack, level) < x)
		{
			PlayerPrefs.SetInt("stars_pack_" + pack + "_level_" + level, x);
		}
	}
}
