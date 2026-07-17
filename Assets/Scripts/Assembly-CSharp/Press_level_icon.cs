using UnityEngine;

public class Press_level_icon : Press_abstract
{
	private Btn_activation_manager btn_manager;
	private Menu menu;
	private bool level_icon;

	private void Start()
	{
		GameObject cameraObject = GameObject.Find("Camera");
		if (cameraObject != null)
		{
			btn_manager = cameraObject.GetComponent<Btn_activation_manager>();
			menu = cameraObject.GetComponent<Menu>();
		}
		level_icon = CompareTag("level_icon");
	}

	public override void action()
	{
		int levelNumber = GetLevelNumber();
		int pack = Mathf.Max(1, PlayerPrefs.GetInt("cur_pack"));
		int wonLevels = PlayerPrefs.GetInt("won_levels_pack_" + pack);
		if (levelNumber < 1 || btn_manager == null || !btn_manager.Button_active())
		{
			return;
		}
		if (level_icon && levelNumber > wonLevels)
		{
			return;
		}
		if (!LevelLoader.LevelExists(pack, levelNumber))
		{
			Debug.LogError("Level-select button " + levelNumber + " points to a missing prefab in episode " + pack + ".");
			return;
		}

		PlayerPreperencesManager.SetLevel(levelNumber);
		if (menu != null)
		{
			menu.OpenRoof();
		}
		StartCoroutine(PreloaderManager.PutOnScreen(levelNumber == 1));
		btn_manager.Disactivate(1f);
	}

	private int GetLevelNumber()
	{
		int value;
		if (int.TryParse(gameObject.name, out value))
		{
			return value;
		}
		Manage_mesh meshManager = GetComponent<Manage_mesh>();
		return meshManager != null ? meshManager.level_number : -1;
	}
}
