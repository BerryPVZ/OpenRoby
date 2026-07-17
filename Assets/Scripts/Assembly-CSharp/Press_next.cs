using UnityEngine;

public class Press_next : Press_abstract
{
	private Btn_activation_manager btn_manager;

	private void Start()
	{
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
	}

	public override void action()
	{
		if (Time.timeScale.Equals(0f))
		{
			Time.timeScale = 1f;
			Manage_mesh component = base.gameObject.GetComponent<Manage_mesh>();
			component.PlaySound();
		}
		if (!btn_manager.Button_active())
		{
			return;
		}
		btn_manager.Disactivate(2f);
		if (!PlayerPreperencesManager.GoNextLevel())
		{
			if (PlayerPreperencesManager.GetPack() == 4)
			{
				btn_manager.Disactivate(1f);
				Object.Instantiate(Resources.Load("Congrats"));
				return;
			}
			PreloaderManager.PutOnScreen();
			StartCoroutine(ModalsManager.PutOffScreen("level_complited_window"));
			PlayerScoresManager playerScoresManager = new PlayerScoresManager();
			if (playerScoresManager.IsPackUnLocked(PlayerPreperencesManager.GetPack() + 1))
			{
				PreloaderManager.LoadNextPack();
			}
			else
			{
				StartCoroutine(PreloaderManager.LoadSelectPack());
			}
			Menu component2 = GameObject.Find("Camera").GetComponent<Menu>();
			LevelLoader.DestroyLevelAndBackgr();
		}
		else
		{
			StartCoroutine(ModalsManager.PutOffScreen(base.gameObject.transform.parent.name));
			StartCoroutine(LevelLoader.LoadNextLevel());
		}
	}
}
