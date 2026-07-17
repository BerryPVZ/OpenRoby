using UnityEngine;

public class BackBtnManager : MonoBehaviour
{
	private Btn_activation_manager btn_manager;

	public Menu menu;

	private int last_action;

	private new static bool active;

	private Slider slider;

	private void Start()
	{
		active = true;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		menu = GameObject.Find("Camera").GetComponent<Menu>();
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
			return;
		}
		PreloaderManager.PutOffScreen("pack_help_window");
		if (!btn_manager.Button_active())
		{
			return;
		}
		if (GameObject.FindGameObjectWithTag("preloader").transform.position.x.Equals(0f))
		{
			btn_manager.Disactivate(2f);
		}
		else
		{
			if ((bool)GameObject.FindGameObjectWithTag("comics") && (bool)GameObject.FindGameObjectWithTag("menu"))
			{
				return;
			}
			MonoBehaviour.print("Android Back button");
			if ((bool)GameObject.FindGameObjectWithTag("menu"))
			{
				slider = GameObject.Find("pack_tube").GetComponent<Slider>();
				if (Mathf.Abs(GameObject.Find("btn_MM_play").transform.position.y) < 300f)
				{
					Application.Quit();
				}
				else if (Mathf.Abs(GameObject.Find("Select_Level_01").transform.position.y) < 300f)
				{
					if (!slider.GetSlideFinished())
					{
						return;
					}
					StartCoroutine(menu.change_screen("pack1", "select_pack"));
					btn_manager.Disactivate(1.5f);
				}
				else
				{
					if (!slider.GetSlideFinished())
					{
						return;
					}
					StartCoroutine(menu.change_screen("select_pack", "main"));
					btn_manager.Disactivate(1f);
				}
				PreloaderManager.PutOffScreen();
			}
			else if ((bool)GameObject.FindGameObjectWithTag("comics"))
			{
				btn_manager.Disactivate(2f);
				PreloaderManager.PutOnScreen();
				StartCoroutine(PreloaderManager.LoadSelectLevel());
				Object.Destroy(GameObject.FindGameObjectWithTag("comics"));
			}
			else
			{
				if (!GameObject.FindGameObjectWithTag("level") || !active)
				{
					return;
				}
				bool flag = GameObject.Find("level_paused_window").transform.position.x.Equals(0f) && GameObject.Find("level_paused_window").transform.position.y.Equals(0f);
				bool flag2 = GameObject.Find("level_failed_window").transform.position.x.Equals(0f) && GameObject.Find("level_failed_window").transform.position.y.Equals(0f);
				bool flag3 = GameObject.Find("level_complited_window").transform.position.x.Equals(0f) && GameObject.Find("level_complited_window").transform.position.y.Equals(0f);
				if (flag && !flag2 && !flag3 && Time.timeScale.Equals(0f))
				{
					Time.timeScale = 1f;
					StartCoroutine(ModalsManager.PutOffScreen("level_paused_window"));
				}
				else if (!flag && !flag2 && !flag3 && Time.timeScale.Equals(1f))
				{
					ModalsManager.PutPauseOnScreen();
				}
				else if (!flag && flag2 && !flag3 && !last_action.Equals(3))
				{
					if (Time.timeScale.Equals(0f))
					{
						Time.timeScale = 1f;
					}
					btn_manager.Disactivate(2f);
					PreloaderManager.PutOnScreen();
					StartCoroutine(ModalsManager.PutOffScreen("level_failed_window"));
					LevelLoader.DestroyLevelAndBackgr();
					StartCoroutine(PreloaderManager.LoadMainMenu());
				}
				else if (!flag && !flag2 && flag3)
				{
					if (Time.timeScale.Equals(0f))
					{
						Time.timeScale = 1f;
					}
					btn_manager.Disactivate(2f);
					PreloaderManager.PutOnScreen();
					StartCoroutine(ModalsManager.PutOffScreen("level_complited_window"));
					LevelLoader.DestroyLevelAndBackgr();
					StartCoroutine(PreloaderManager.LoadMainMenu());
					if ((bool)GameObject.Find("Congrats(Clone)"))
					{
						Object.Destroy(GameObject.Find("Congrats(Clone)"));
					}
				}
			}
		}
	}
}
