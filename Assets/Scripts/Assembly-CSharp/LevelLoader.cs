using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	private const int PackCount = 4;
	private const int LevelsPerPack = 20;
	private static bool initialized;
	private static List<string>[] levels;
	private static string[] backgrounds;
	private static GameObject activeBackground;

	// The game's level-select order is not the same as the prefab number order.
	// Keep the original 20-slot order. Some prefab numbers are intentionally skipped
	// because the menu order is different from the Resources filename order.
	private static readonly string[][] OriginalLevelOrder =
	{
		new string[20]
		{
			"Levels/pack_1_level_1", "Levels/pack_1_level_2", "Levels/pack_1_level_3", "Levels/pack_1_level_21",
			"Levels/pack_1_level_22", "Levels/pack_1_level_4", "Levels/pack_1_level_6", "Levels/pack_1_level_7",
			"Levels/pack_1_level_8", "Levels/pack_1_level_9", "Levels/pack_1_level_18", "Levels/pack_1_level_10",
			"Levels/pack_1_level_11", "Levels/pack_1_level_12", "Levels/pack_1_level_13", "Levels/pack_1_level_14",
			"Levels/pack_1_level_15", "Levels/pack_1_level_16", "Levels/pack_1_level_17", "Levels/pack_1_level_5"
		},
		new string[20]
		{
			"Levels/pack_2_level_16", "Levels/pack_2_level_1", "Levels/pack_2_level_2", "Levels/pack_2_level_3",
			"Levels/pack_2_level_10", "Levels/pack_2_level_4", "Levels/pack_2_level_6", "Levels/pack_2_level_8",
			"Levels/pack_2_level_5", "Levels/pack_2_level_7", "Levels/pack_2_level_11", "Levels/pack_2_level_12",
			"Levels/pack_2_level_13", "Levels/pack_2_level_14", "Levels/pack_2_level_15", "Levels/pack_2_level_17",
			"Levels/pack_2_level_18", "Levels/pack_2_level_19", "Levels/pack_2_level_21", "Levels/pack_2_level_20"
		},
		new string[20]
		{
			"Levels/pack_3_level_11", "Levels/pack_3_level_1", "Levels/pack_3_level_5", "Levels/pack_3_level_2",
			"Levels/pack_3_level_3", "Levels/pack_3_level_4", "Levels/pack_3_level_7", "Levels/pack_3_level_6",
			"Levels/pack_3_level_8", "Levels/pack_3_level_9", "Levels/pack_3_level_10", "Levels/pack_3_level_12",
			"Levels/pack_3_level_13", "Levels/pack_3_level_14", "Levels/pack_3_level_15", "Levels/pack_3_level_20",
			"Levels/pack_3_level_17", "Levels/pack_3_level_16", "Levels/pack_3_level_18", "Levels/pack_3_level_19"
		},
		new string[20]
		{
			"Levels/pack_4_level_11", "Levels/pack_4_level_1", "Levels/pack_4_level_2", "Levels/pack_4_level_5",
			"Levels/pack_4_level_6", "Levels/pack_4_level_3", "Levels/pack_4_level_7", "Levels/pack_4_level_8",
			"Levels/pack_4_level_9", "Levels/pack_4_level_10", "Levels/pack_4_level_4", "Levels/pack_4_level_12",
			"Levels/pack_4_level_15", "Levels/pack_4_level_14", "Levels/pack_4_level_13", "Levels/pack_4_level_18",
			"Levels/pack_4_level_16", "Levels/pack_4_level_17", "Levels/pack_4_level_20", "Levels/pack_4_level_19"
		}
	};

	private void Awake()
	{
		EnsureInitialized();
	}

	private void Start()
	{
		EnsureInitialized();
		SyncLevelCountsToPrefs();
	}

	private static void EnsureInitialized()
	{
		if (initialized && levels != null && backgrounds != null)
		{
			return;
		}

		levels = new List<string>[PackCount];
		backgrounds = new string[PackCount];
		for (int pack = 1; pack <= PackCount; pack++)
		{
			levels[pack - 1] = new List<string>(LevelsPerPack);
			for (int level = 0; level < LevelsPerPack; level++)
			{
				levels[pack - 1].Add(OriginalLevelOrder[pack - 1][level]);
			}
			backgrounds[pack - 1] = FindBackgroundPath(pack);
		}
		initialized = true;
	}

	private static string FindBackgroundPath(int pack)
	{
		string[] candidates =
		{
			"Levels/pack_" + pack + "_Back",
			"Levels/pack_" + pack + "_back",
			"Backgrounds/pack_" + pack + "_Back",
			"Backgrounds/pack_" + pack + "_back",
			"pack_" + pack + "_Back",
			"pack_" + pack + "_back"
		};

		for (int i = 0; i < candidates.Length; i++)
		{
			if (Resources.Load<GameObject>(candidates[i]) != null)
			{
				return candidates[i];
			}
		}
		return candidates[0];
	}

	public static int GetLevelCount(int pack)
	{
		EnsureInitialized();
		return IsValidPack(pack) ? LevelsPerPack : 0;
	}

	public static bool LevelExists(int pack, int level)
	{
		GameObject prefab;
		return TryGetLevelPrefab(pack, level, out prefab);
	}

	public static void SyncLevelCountsToPrefs()
	{
		EnsureInitialized();
		for (int pack = 1; pack <= PackCount; pack++)
		{
			int count = GetLevelCount(pack);
			PlayerPrefs.SetInt("total_levels_pack_" + pack, count);
			if (count > 0)
			{
				int won = PlayerPrefs.GetInt("won_levels_pack_" + pack);
				PlayerPrefs.SetInt("won_levels_pack_" + pack, Mathf.Clamp(won < 1 ? 1 : won, 1, count));
			}
			else
			{
				PlayerPrefs.SetInt("won_levels_pack_" + pack, 0);
			}
		}
		PlayerPrefs.Save();
	}

	public static void LoadEpisodeBackground(int pack)
	{
		LoadEpisodeBackground(pack, true);
	}

	public static void LoadEpisodeBackground(int pack, bool replaceExisting)
	{
		EnsureInitialized();
		if (!IsValidPack(pack))
		{
			Debug.LogError("Cannot load episode background: invalid pack " + pack + ".");
			return;
		}

		if (replaceExisting)
		{
			if (activeBackground != null)
			{
				UnityEngine.Object.Destroy(activeBackground);
				activeBackground = null;
			}
			DestroyObjectsWithTag("backgr_obj");
		}
		else if (activeBackground != null || FindObjectWithTag("backgr_obj") != null)
		{
			return;
		}

		GameObject prefab = Resources.Load<GameObject>(backgrounds[pack - 1]);
		if (prefab == null)
		{
			backgrounds[pack - 1] = FindBackgroundPath(pack);
			prefab = Resources.Load<GameObject>(backgrounds[pack - 1]);
		}

		if (prefab != null)
		{
			activeBackground = (GameObject)UnityEngine.Object.Instantiate(prefab);
		}
		else
		{
			Debug.LogError("Episode " + pack + " background was not found. Expected a prefab such as Resources/Levels/pack_" + pack + "_Back.prefab.");
		}
	}

	public static void LoadLevel()
	{
		EnsureInitialized();
		int pack = Mathf.Clamp(PlayerPrefs.GetInt("cur_pack"), 1, PackCount);
		int levelCount = GetLevelCount(pack);
		int level = levelCount > 0 ? Mathf.Clamp(PlayerPrefs.GetInt("cur_level"), 1, levelCount) : 1;
		PlayerPrefs.SetInt("cur_pack", pack);
		PlayerPrefs.SetInt("cur_level", level);

		GameObject levelPrefab;
		if (!TryGetLevelPrefab(pack, level, out levelPrefab))
		{
			Debug.LogError(GetMissingLevelMessage(pack, level));
			PreloaderManager.PutOffScreen();
			return;
		}

		GC.Collect();
		Resources.UnloadUnusedAssets();
		LoadEpisodeBackground(pack, false);
		if (FindObjectWithTag("level") == null)
		{
			UnityEngine.Object.Instantiate(levelPrefab);
		}
		PreloaderManager.PutOffScreen();
	}

	public static IEnumerator ReLoadLevel()
	{
		EnsureInitialized();
		int pack = PlayerPrefs.GetInt("cur_pack");
		int level = PlayerPrefs.GetInt("cur_level");
		DestroyObjectsWithTag("level");
		DestroyObjectsWithTag("help");
		yield return new WaitForSeconds(0.01f);

		GameObject prefab;
		if (TryGetLevelPrefab(pack, level, out prefab))
		{
			UnityEngine.Object.Instantiate(prefab);
			GC.Collect();
		}
		else
		{
			Debug.LogError(GetMissingLevelMessage(pack, level));
		}
	}

	public static IEnumerator LoadNextLevel()
	{
		EnsureInitialized();
		if (!PlayerPreperencesManager.GoNextLevel())
		{
			yield break;
		}

		PlayerPrefs.SetInt("cur_level", PlayerPrefs.GetInt("cur_level") + 1);
		int pack = PlayerPrefs.GetInt("cur_pack");
		int level = PlayerPrefs.GetInt("cur_level");
		DestroyObjectsWithTag("level");
		yield return new WaitForSeconds(0.01f);

		GameObject prefab;
		if (TryGetLevelPrefab(pack, level, out prefab))
		{
			UnityEngine.Object.Instantiate(prefab);
			GC.Collect();
			Resources.UnloadUnusedAssets();
		}
		else
		{
			Debug.LogError(GetMissingLevelMessage(pack, level));
		}
	}

	public static void DestroyLevelAndBackgr()
	{
		DestroyObjectsWithTag("level");
		if (activeBackground != null)
		{
			UnityEngine.Object.Destroy(activeBackground);
			activeBackground = null;
		}
		DestroyObjectsWithTag("backgr_obj");
		DestroyObjectsWithTag("help");
		GC.Collect();
		Resources.UnloadUnusedAssets();
	}

	private static bool TryGetLevelPrefab(int pack, int level, out GameObject prefab)
	{
		EnsureInitialized();
		prefab = null;
		if (!IsValidPack(pack) || level < 1 || level > levels[pack - 1].Count)
		{
			return false;
		}

		string path = levels[pack - 1][level - 1];
		prefab = Resources.Load<GameObject>(path);
		return prefab != null;
	}

	private static bool IsValidPack(int pack)
	{
		return pack >= 1 && pack <= PackCount;
	}

	private static string GetMissingLevelMessage(int pack, int level)
	{
		if (!IsValidPack(pack))
		{
			return "Cannot load level: invalid episode " + pack + ".";
		}
		if (level < 1 || level > levels[pack - 1].Count)
		{
			return "Cannot load episode " + pack + ", level " + level + ": the level is outside the discovered level range.";
		}
		return "Cannot load episode " + pack + ", level " + level + ". Missing Resources prefab: " + levels[pack - 1][level - 1] + ".";
	}

	private static GameObject FindObjectWithTag(string tagName)
	{
		try
		{
			return GameObject.FindGameObjectWithTag(tagName);
		}
		catch (UnityException)
		{
			return null;
		}
	}

	private static void DestroyObjectsWithTag(string tagName)
	{
		GameObject[] objects;
		try
		{
			objects = GameObject.FindGameObjectsWithTag(tagName);
		}
		catch (UnityException)
		{
			return;
		}
		for (int i = 0; i < objects.Length; i++)
		{
			if (objects[i] != null)
			{
				UnityEngine.Object.Destroy(objects[i]);
			}
		}
	}
}
