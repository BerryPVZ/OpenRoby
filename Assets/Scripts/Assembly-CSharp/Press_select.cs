using System.Collections;
using UnityEngine;

public class Press_select : Press_abstract
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
		}
		if (btn_manager.Button_active())
		{
			btn_manager.Disactivate(2f);
			PreloaderManager.PutOnScreen();
			StartCoroutine(Load());
			Manage_mesh component = base.gameObject.GetComponent<Manage_mesh>();
			component.PlaySound();
			FlurryAndroidiOSManager.EventSimple("Select level button (" + base.transform.parent.name + ")");
		}
	}

	private IEnumerator Load()
	{
		yield return new WaitForSeconds(0.03f);
		StartCoroutine(ModalsManager.PutOffScreen(base.gameObject.transform.parent.name));
		LevelLoader.DestroyLevelAndBackgr();
		StartCoroutine(PreloaderManager.LoadSelectLevel());
	}
}
