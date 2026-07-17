using System.Collections;
using UnityEngine;

public class PreloaderManager : MonoBehaviour
{
	public static IEnumerator PutOnScreen(bool loadComics)
	{
		yield return new WaitForSeconds(0.01f);
		GameObject preloader = FindTaggedObject("preloader");
		if (preloader != null)
		{
			preloader.transform.position = new Vector3(0f, 0f, 800f);
			yield return new WaitForSeconds(1.5f);
		}
		else
		{
			Debug.LogWarning("Preloader object was not found; loading the selected level directly.");
			yield return new WaitForSeconds(0.05f);
		}

		int pack = PlayerPrefs.GetInt("cur_pack");
		if (pack < 1)
		{
			pack = 1;
			PlayerPrefs.SetInt("cur_pack", pack);
		}

		if (loadComics)
		{
			GameObject comicPrefab = Resources.Load<GameObject>("comics" + pack);
			if (comicPrefab != null)
			{
				Object.Instantiate(comicPrefab);
				PutOffScreen();
				yield break;
			}
			Debug.LogWarning("Comic prefab 'Resources/comics" + pack + "' was not found; opening the level directly.");
		}

		DestroyMenuObject();
		LevelLoader.LoadLevel();
	}

	public static void PutOffScreen()
	{
		GameObject preloader = FindTaggedObject("preloader");
		if (preloader != null)
		{
			preloader.transform.position = new Vector3(3000f, 0f, 800f);
		}
	}

	public static void PutOffScreen(string objectName)
	{
		GameObject target = GameObject.Find(objectName);
		if (target != null)
		{
			target.transform.position = new Vector3(3000f, 0f, 800f);
		}
	}

	public static void PutOnScreen()
	{
		GameObject preloader = FindTaggedObject("preloader");
		if (preloader != null)
		{
			preloader.transform.position = new Vector3(0f, 0f, 800f);
		}
	}

	public static void PutOnScreen(string objectName)
	{
		PutOnScreen(objectName, 800f);
	}

	public static void PutOnScreen(string objectName, float z)
	{
		GameObject target = GameObject.Find(objectName);
		if (target != null)
		{
			target.transform.position = new Vector3(0f, 0f, z);
		}
	}

	public static IEnumerator LoadMainMenu()
	{
		if (!InstantiateMenu())
		{
			yield break;
		}
		MoveMenuMainObjectsOffscreen();
		yield return new WaitForSeconds(0.5f);
		PutOffScreen();
		PlayAnimation("btn_MM_play", "btn_MM_Play_01");
		PlayAnimation("game_Name", "game_name_01");
		PlayAnimation("stoika", "stoika_01");
		PlayAnimation("Social", "social_01");
		PlayAnimation("Sound", "option_01");
		PrepareSlider(false);
	}

	public static IEnumerator LoadSelectLevel()
	{
		if (!InstantiateMenu())
		{
			yield break;
		}
		MoveMenuMainObjectsOffscreen();
		yield return new WaitForSeconds(0.5f);

		int pack = Mathf.Max(1, PlayerPrefs.GetInt("cur_pack"));
		LevelLoader.LoadEpisodeBackground(pack, true);
		PlayerScoresManager scoresManager = FindCameraComponent<PlayerScoresManager>();
		if (scoresManager != null)
		{
			scoresManager.UpdateSelectLevel(pack);
		}
		PutOffScreen();
		PlayAnimation("score_tablo", "score_tablo_01");
		PlayAnimation("Select_Level_01", "level_select_01");
		PlayAnimation("btn_MM_back", "btn_back_01");
		PrepareSlider(false);
		PlayAnimation("roof_01", "roof_01_01");
		PlayAnimation("lamp_left", "lamp_left");
		PlayAnimation("lamp_right", "lamp_right");
	}

	public static IEnumerator LoadSelectPack()
	{
		if (!InstantiateMenu())
		{
			yield break;
		}
		MoveMenuMainObjectsOffscreen();
		yield return new WaitForSeconds(0.5f);

		int pack = Mathf.Max(1, PlayerPrefs.GetInt("cur_pack"));
		LevelLoader.LoadEpisodeBackground(pack, true);
		PlayerScoresManager scoresManager = FindCameraComponent<PlayerScoresManager>();
		if (scoresManager != null)
		{
			scoresManager.UpdateSelectPack();
		}
		PutOffScreen();
		PrepareSlider(true);
		PlayAnimation("btn_MM_back", "btn_back_01");
		PlayAnimation("roof_01", "roof_01_01");
		PlayAnimation("lamp_left", "lamp_left");
		PlayAnimation("lamp_right", "lamp_right");
	}

	public static void LoadNextPack()
	{
		PlayerPreperencesManager preferences = FindCameraComponent<PlayerPreperencesManager>();
		if (preferences == null)
		{
			return;
		}
		preferences.SetPack(Mathf.Clamp(PlayerPreperencesManager.GetPack() + 1, 1, 4));
		PlayerPreperencesManager.SetLevel(1);
		GameObject comicPrefab = Resources.Load<GameObject>("comics" + PlayerPreperencesManager.GetPack());
		if (comicPrefab != null)
		{
			Object.Instantiate(comicPrefab);
			PutOffScreen();
		}
		else
		{
			LevelLoader.LoadLevel();
		}
	}

	private static bool InstantiateMenu()
	{
		if (FindTaggedObject("menu") != null)
		{
			return true;
		}
		GameObject menuPrefab = Resources.Load<GameObject>("Menu");
		if (menuPrefab == null)
		{
			Debug.LogError("Menu prefab was not found at Resources/Menu.prefab.");
			return false;
		}
		Object.Instantiate(menuPrefab);
		return true;
	}

	private static void MoveMenuMainObjectsOffscreen()
	{
		SetPosition("btn_MM_play", new Vector3(0f, -1000f, 0f));
		SetPosition("game_Name", new Vector3(0f, -1000f, 0f));
		SetPosition("stoika", new Vector3(0f, -1000f, 0f));
		SetPosition("Social", new Vector3(0f, -1000f, 0f));
		SetPosition("Sound", new Vector3(0f, -1000f, 0f));
	}

	private static void PrepareSlider(bool selectPack)
	{
		GameObject tube = GameObject.Find("pack_tube");
		Slider slider = tube != null ? tube.GetComponent<Slider>() : null;
		if (slider != null)
		{
			slider.SetPositionToProcrutka(selectPack);
		}
	}

	private static void DestroyMenuObject()
	{
		GameObject menu = FindTaggedObject("menu");
		if (menu != null)
		{
			Object.Destroy(menu);
		}
	}

	private static GameObject FindTaggedObject(string tagName)
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

	private static T FindCameraComponent<T>() where T : Component
	{
		GameObject cameraObject = GameObject.Find("Camera");
		return cameraObject != null ? cameraObject.GetComponent<T>() : null;
	}

	private static void SetPosition(string objectName, Vector3 position)
	{
		GameObject target = GameObject.Find(objectName);
		if (target != null)
		{
			target.transform.position = position;
		}
	}

	private static void PlayAnimation(string objectName, string animationName)
	{
		GameObject target = GameObject.Find(objectName);
		Animation animation = target != null ? target.GetComponent<Animation>() : null;
		if (animation != null && animation.GetClip(animationName) != null)
		{
			animation.Play(animationName);
		}
	}
}
