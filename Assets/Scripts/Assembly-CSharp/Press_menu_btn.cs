using UnityEngine;

public class Press_menu_btn : Press_abstract
{
	public string btn_name;
	public Menu menu;
	private Btn_activation_manager btn_manager;
	private Slider slider;
	public AudioClip sound;
	public PlayerScoresManager psm;
	public PlayerPreperencesManager ppm;

	private void Start()
	{
		GameObject cameraObject = GameObject.Find("Camera");
		if (cameraObject != null)
		{
			menu = cameraObject.GetComponent<Menu>();
			btn_manager = cameraObject.GetComponent<Btn_activation_manager>();
			psm = cameraObject.GetComponent<PlayerScoresManager>();
			ppm = cameraObject.GetComponent<PlayerPreperencesManager>();
		}
		btn_name = gameObject.name;
		sound = AudioRuntime.LoadClip("sounds/Btns/But 2");
		AudioSource source = GetComponent<AudioSource>();
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
		}
		source.clip = sound;
		source.volume = 0.6f;
		source.playOnAwake = false;
		source.spatialBlend = 0f;
	}

	public override void action()
	{
		if (btn_manager == null || !btn_manager.Button_active())
		{
			return;
		}

		switch (btn_name)
		{
		case "btn_MM_play":
			if (menu != null)
			{
				StartCoroutine(menu.change_screen("main", "select_pack"));
			}
			if (psm != null)
			{
				psm.UpdateSelectPack();
			}
			btn_manager.Disactivate(1f);
			break;

		case "btn_MM_back":
			PackHelp.TurnOff();
			GameObject tube = GameObject.Find("pack_tube");
			slider = tube != null ? tube.GetComponent<Slider>() : null;
			if (slider != null && slider.GetSlideFinished() && PacksReady() && menu != null)
			{
				if (menu.screen == "select_pack")
				{
					StartCoroutine(menu.change_screen("select_pack", "main"));
					btn_manager.Disactivate(1f);
				}
				else
				{
					StartCoroutine(menu.change_screen(menu.screen, "select_pack"));
					if (psm != null)
					{
						psm.UpdateSelectPack();
					}
					btn_manager.Disactivate(1.5f);
				}
			}
			break;

		case "btn_pack_obj_01":
			SelectPack(1, true, 0);
			break;
		case "btn_pack_obj_02":
			SelectPack(2, psm != null && psm.IsPackUnLocked(2), 50);
			break;
		case "btn_pack_obj_03":
			SelectPack(3, psm != null && psm.IsPackUnLocked(3), 100);
			break;
		case "btn_pack_obj_04":
			SelectPack(4, psm != null && psm.IsPackUnLocked(4), 145);
			break;
		}
	}

	private void SelectPack(int pack, bool unlocked, int requiredStars)
	{
		if (!unlocked)
		{
			if (Mathf.Abs(transform.position.x) < 50f && psm != null)
			{
				PreloaderManager.PutOnScreen("pack_help_window", 900f);
				psm.SetPHNumber(requiredStars);
			}
			return;
		}
		if (!IsCentered())
		{
			return;
		}

		if (ppm != null)
		{
			ppm.SetPack(pack);
		}
		else
		{
			PlayerPrefs.SetInt("cur_pack", pack);
		}
		LevelLoader.SyncLevelCountsToPrefs();
		LevelLoader.LoadEpisodeBackground(pack, true);
		if (psm != null)
		{
			psm.UpdateSelectLevel(pack);
		}
		PressPackAnimation(pack);
	}

	private bool IsCentered()
	{
		return Mathf.Abs(transform.position.x) < 10f;
	}

	private void PressPackAnimation(int pack)
	{
		if (menu != null)
		{
			StartCoroutine(menu.change_screen("select_pack", "pack" + pack));
		}
		Animation parentAnimation = transform.parent != null ? transform.parent.GetComponent<Animation>() : null;
		if (parentAnimation != null && parentAnimation.GetClip("pack_click") != null)
		{
			parentAnimation.Play("pack_click");
		}
		btn_manager.Disactivate(1.5f);
		AudioSource source = GetComponent<AudioSource>();
		if (PlayerPreperencesManager.GetSound() && source != null && source.clip != null)
		{
			source.Play();
		}
	}

	private bool PacksReady()
	{
		float closest = 1000f;
		for (int i = 1; i <= 4; i++)
		{
			GameObject packObject = GameObject.Find("Btn_Pack_0" + i);
			if (packObject != null)
			{
				closest = Mathf.Min(closest, Mathf.Abs(packObject.transform.position.x));
			}
		}
		return closest <= 30f;
	}
}
