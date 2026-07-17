using UnityEngine;

public class Press_pause_corner : Press_abstract
{
	private Btn_activation_manager btn_manager;

	private void Start()
	{
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
	}

	public override void action()
	{
		if (btn_manager.Button_active())
		{
			ModalsManager.PutPauseOnScreen();
			FlurryAndroidiOSManager.EventSimple("Pause button");
		}
	}
}
