using UnityEngine;

public class PlayerScoresManager : MonoBehaviour
{
	public Mesh[] select_pack_stars_meshes;
	public Mesh[] select_pack_numbers;
	public Mesh[] level_icon_state;
	public Mesh[] level_icon_stars;
	public Mesh[] unlocked_packs;
	public Mesh[] locked_packs;
	public Material[] packs_materials;

	private GraphicsSource gs;

	private void Start()
	{
		if (packs_materials == null || packs_materials.Length < 2)
		{
			packs_materials = new Material[2];
		}

		GameObject graphicsObject = GameObject.Find("GraphicsSource");
		if (graphicsObject != null)
		{
			gs = graphicsObject.GetComponent<GraphicsSource>();
			if (gs != null)
			{
				if (gs.GraphicsHD())
				{
					TrySetPackMaterial(0, gs.materials, 1, 2);
					TrySetPackMaterial(1, gs.materials, 1, 3);
				}
				else
				{
					TrySetPackMaterial(0, gs.materials, 0, 2);
					TrySetPackMaterial(1, gs.materials, 0, 3);
				}
			}
		}
	}

	public void UpdateSelectPack()
	{
		LevelLoader.SyncLevelCountsToPrefs();
		SetStarsOnPack(1);
		SetOpenedLevelsAmount(1);
		for (int i = 2; i <= 4; i++)
		{
			if (IsPackUnLocked(i))
			{
				UnLockPack(i);
				SetStarsOnPack(i);
				SetOpenedLevelsAmount(i);
			}
			else
			{
				LockPack(i);
			}
		}
	}

	public void UpdateSelectLevel(int pack)
	{
		LevelLoader.SyncLevelCountsToPrefs();
		int totalLevels = PlayerPrefs.GetInt("total_levels_pack_" + pack);
		int wonLevels = Mathf.Clamp(PlayerPrefs.GetInt("won_levels_pack_" + pack), 1, Mathf.Max(1, totalLevels));
		PlayerPrefs.SetInt("won_levels_pack_" + pack, wonLevels);

		int slotCount = 20;
		for (int level = 1; level <= slotCount; level++)
		{
			GameObject icon = GameObject.Find(level.ToString());
			GameObject stars = GameObject.Find("level_" + level + "_g");
			GameObject releasedNumber = GameObject.Find("LvlN_" + level + "Relise");
			bool available = level <= totalLevels;

			if (icon == null)
			{
				continue;
			}

			SetRenderer(icon, available);
			if (!available)
			{
				SetRenderer(stars, false);
				SetRenderer(releasedNumber, false);
				continue;
			}

			bool unlocked = level <= wonLevels;
			SetMesh(icon, GetMesh(level_icon_state, unlocked ? 1 : 0));
			SetRenderer(stars, unlocked);
			SetRenderer(releasedNumber, unlocked);
			if (unlocked)
			{
				int starCount = Mathf.Clamp(PlayerPrefs.GetInt("stars_pack_" + pack + "_level_" + level), 0, 3);
				SetMesh(stars, GetMesh(level_icon_stars, starCount));
			}
		}

		UpdateTabloScore(pack);
	}

	private void UpdateTabloScore(int pack)
	{
		int totalScore = 0;
		int totalLevels = PlayerPrefs.GetInt("total_levels_pack_" + pack);
		for (int level = 1; level <= totalLevels; level++)
		{
			totalScore += PlayerPrefs.GetInt("scores_pack_" + pack + "_level_" + level);
		}

		for (int i = 1; i < 6; i++)
		{
			SetRenderer(GameObject.Find("t_" + i), false);
		}

		int[] digits = VictoryScoresToArr(totalScore);
		SetMesh(GameObject.Find("t_6"), GetMesh(select_pack_numbers, digits[5]));
		bool started = false;
		for (int i = 1; i < 6; i++)
		{
			GameObject digitObject = GameObject.Find("t_" + i);
			if (digits[i - 1] == 0 && !started)
			{
				SetRenderer(digitObject, false);
				continue;
			}
			started = true;
			SetRenderer(digitObject, true);
			SetMesh(digitObject, GetMesh(select_pack_numbers, digits[i - 1]));
		}
	}

	private int[] VictoryScoresToArr(int totalScores)
	{
		int[] result = new int[6];
		for (int i = 0; i < 6; i++)
		{
			int divisor = (int)Mathf.Pow(10f, 5 - i);
			result[i] = totalScores / divisor;
			totalScores -= result[i] * divisor;
		}
		return result;
	}

	private void SetStarsOnPack(int pack)
	{
		int totalLevels = PlayerPrefs.GetInt("total_levels_pack_" + pack);
		if (totalLevels <= 0)
		{
			return;
		}

		int totalStars = 0;
		for (int level = 1; level <= totalLevels; level++)
		{
			totalStars += PlayerPrefs.GetInt("stars_pack_" + pack + "_level_" + level);
		}

		float average = (float)totalStars / totalLevels;
		int meshIndex = average == 0f ? 0 : (average < 1.5f ? 1 : (average < 3f ? 2 : 3));
		SetMesh(GameObject.Find("p_" + pack + "_s"), GetMesh(select_pack_stars_meshes, meshIndex));
	}

	private void SetOpenedLevelsAmount(int pack)
	{
		int totalStars = 0;
		for (int level = 1; level <= PlayerPrefs.GetInt("total_levels_pack_" + pack); level++)
		{
			totalStars += PlayerPrefs.GetInt("stars_pack_" + pack + "_level_" + level);
		}

		int tens = totalStars / 10;
		int ones = totalStars - 10 * tens;
		GameObject tensObject = GameObject.Find("p_" + pack + "_d_1");
		if (tens > 0)
		{
			SetRenderer(tensObject, true);
			SetMesh(tensObject, GetMesh(select_pack_numbers, Mathf.Clamp(tens, 0, 9)));
		}
		else
		{
			SetRenderer(tensObject, false);
		}
		SetMesh(GameObject.Find("p_" + pack + "_d_2"), GetMesh(select_pack_numbers, Mathf.Clamp(ones, 0, 9)));
	}

	public Mesh GetTexture(int n)
	{
		return GetMesh(select_pack_numbers, n);
	}

	public void LockPack(int pack)
	{
		GameObject packObject = GameObject.Find("btn_pack_obj_0" + pack);
		SetMaterial(packObject, GetMaterial(packs_materials, 0));
		SetMesh(packObject, GetMesh(locked_packs, pack - 2));
		Transform blink = packObject != null ? packObject.transform.Find("btn_pack_blink") : null;
		if (blink != null)
		{
			SetRenderer(blink.gameObject, false);
		}
		SwitchRenderer("btn_pack_shadow_0" + pack, false);
		SwitchRenderer("n_" + pack + "_1", false);
		SwitchRenderer("n_" + pack + "_2", false);
		SwitchRenderer("p_" + pack + "_d_1", false);
		SwitchRenderer("p_" + pack + "_d_2", false);
		SwitchRenderer("p_" + pack + "_s", false);
		SwitchRenderer("slash_" + pack, false);
		SwitchRenderer("n_" + pack + "_h_1", true);
		SwitchRenderer("n_" + pack + "_h_2", true);
		SwitchRenderer("n_" + pack + "_h_3", true);
		SwitchRenderer("n_" + pack + "_h_4", true);
		SwitchRenderer("p_" + pack + "_h", true);
	}

	public void UnLockPack(int pack)
	{
		GameObject packObject = GameObject.Find("btn_pack_obj_0" + pack);
		SetMaterial(packObject, GetMaterial(packs_materials, 1));
		SetMesh(packObject, GetMesh(unlocked_packs, pack - 2));
		SwitchRenderer("btn_pack_shadow_0" + pack, true);
		SwitchRenderer("n_" + pack + "_1", true);
		SwitchRenderer("n_" + pack + "_2", true);
		SwitchRenderer("p_" + pack + "_d_1", true);
		SwitchRenderer("p_" + pack + "_d_2", true);
		SwitchRenderer("p_" + pack + "_s", true);
		SwitchRenderer("slash_" + pack, true);
		SwitchRenderer("n_" + pack + "_h_1", false);
		SwitchRenderer("n_" + pack + "_h_2", false);
		SwitchRenderer("n_" + pack + "_h_3", false);
		SwitchRenderer("n_" + pack + "_h_4", false);
		SwitchRenderer("p_" + pack + "_h", false);
	}

	private void SwitchRenderer(string objectName, bool enabled)
	{
		SetRenderer(GameObject.Find(objectName), enabled);
	}

	public bool IsPackUnLocked(int pack)
	{
		// All restored episodes are intentionally available from the start.
		// This only unlocks the episode buttons; levels inside each episode still
		// use won_levels_pack_X for normal progression.
		return pack >= 1 && pack <= 4;
	}

	public void SetPHNumber(int n)
	{
		GameObject hundreds = GameObject.Find("ph_1");
		if (n < 100)
		{
			SetRenderer(hundreds, false);
			SetMesh(GameObject.Find("ph_2"), GetMesh(select_pack_numbers, n / 10));
			SetMesh(GameObject.Find("ph_3"), GetMesh(select_pack_numbers, n % 10));
		}
		else
		{
			SetRenderer(hundreds, true);
			SetMesh(hundreds, GetMesh(select_pack_numbers, 1));
			n -= 100;
			SetMesh(GameObject.Find("ph_2"), GetMesh(select_pack_numbers, n / 10));
			SetMesh(GameObject.Find("ph_3"), GetMesh(select_pack_numbers, n % 10));
		}
	}

	private void TrySetPackMaterial(int destination, Material[][] source, int group, int index)
	{
		if (source != null && group >= 0 && group < source.Length && source[group] != null && index >= 0 && index < source[group].Length)
		{
			packs_materials[destination] = source[group][index];
		}
	}

	private static Mesh GetMesh(Mesh[] source, int index)
	{
		return source != null && index >= 0 && index < source.Length ? source[index] : null;
	}

	private static Material GetMaterial(Material[] source, int index)
	{
		return source != null && index >= 0 && index < source.Length ? source[index] : null;
	}

	private static void SetRenderer(GameObject target, bool enabled)
	{
		if (target == null)
		{
			return;
		}
		Renderer renderer = target.GetComponent<Renderer>();
		if (renderer != null)
		{
			renderer.enabled = enabled;
		}
	}

	private static void SetMesh(GameObject target, Mesh mesh)
	{
		if (target == null || mesh == null)
		{
			return;
		}
		MeshFilter filter = target.GetComponent<MeshFilter>();
		if (filter != null)
		{
			filter.mesh = mesh;
		}
	}

	private static void SetMaterial(GameObject target, Material material)
	{
		if (target == null || material == null)
		{
			return;
		}
		Renderer renderer = target.GetComponent<Renderer>();
		if (renderer != null)
		{
			renderer.material = material;
		}
	}
}
