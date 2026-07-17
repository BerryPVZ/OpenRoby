using UnityEngine;

public class Press_restart_corner : Press_abstract
{
	private Btn_activation_manager btn_manager;

	private VictoryManager vm;

	private void Start()
	{
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		vm = GameObject.Find("Camera").GetComponent<VictoryManager>();
	}

	public override void action()
	{
		if (btn_manager.Button_active())
		{
			StartCoroutine(LevelLoader.ReLoadLevel());
			btn_manager.Disactivate(1f);
			vm.restarted = true;
			FlurryAndroidiOSManager.EventSimple("Restart button");
		}
	}
}
