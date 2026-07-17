using System.Collections;
using UnityEngine;

public class Press_congrats : Press_abstract
{
	public string btn_name;

	public Menu menu;

	private Btn_activation_manager btn_manager;

	private void Start()
	{
		menu = GameObject.Find("Camera").GetComponent<Menu>();
		btn_name = base.gameObject.name;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
	}

	public override void action()
	{
		if (Time.timeScale.Equals(0f))
		{
			Time.timeScale = 1f;
		}
		btn_manager.Disactivate(2f);
		PreloaderManager.PutOnScreen();
		StartCoroutine(ModalsManager.PutOffScreen("level_complited_window"));
		LevelLoader.DestroyLevelAndBackgr();
		StartCoroutine(LoadMainMenu());
	}

	private IEnumerator LoadMainMenu()
	{
		Object.Instantiate(Resources.Load("Menu"));
		GameObject.Find("btn_MM_play").transform.position = new Vector3(0f, -1000f, 0f);
		GameObject.Find("game_Name").transform.position = new Vector3(0f, -1000f, 0f);
		GameObject.Find("stoika").transform.position = new Vector3(0f, -1000f, 0f);
		GameObject.Find("Social").transform.position = new Vector3(0f, -1000f, 0f);
		GameObject.Find("Sound").transform.position = new Vector3(0f, -1000f, 0f);
		yield return new WaitForSeconds(0.5f);
		PreloaderManager.PutOffScreen();
		GameObject.Find("btn_MM_play").GetComponent<Animation>().Play("btn_MM_Play_01");
		GameObject.Find("game_Name").GetComponent<Animation>().Play("game_name_01");
		GameObject.Find("stoika").GetComponent<Animation>().Play("stoika_01");
		GameObject.Find("Social").GetComponent<Animation>().Play("social_01");
		GameObject.Find("Sound").GetComponent<Animation>().Play("option_01");
		Slider s = GameObject.Find("pack_tube").GetComponent<Slider>();
		s.SetPositionToProcrutka(false);
		switch (btn_name)
		{
		case "cong_facebook":
			RunFaceBook();
			break;
		case "cong_twitter":
			RunTwitter();
			break;
		}
		Object.Destroy(GameObject.Find("Congrats(Clone)"));
	}

	private void RunTwitter()
	{
		Application.OpenURL("https://twitter.com/intent/tweet?status=I%20am%20playing%20this%20really%20well-made%2Caddicting%20app%2Ccalled%20Rescue%20Roby%3A%20http%3A%2F%2Fplay.google.com/store/apps/details?id=com.missingames.rescueroby.lite");
	}

	private void RunFaceBook()
	{
		Application.OpenURL("http://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fplay.google.com/store/apps/details?id=com.missingames.rescueroby.lite");
	}
}
