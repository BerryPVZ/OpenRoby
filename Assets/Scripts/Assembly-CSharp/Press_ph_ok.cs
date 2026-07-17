using UnityEngine;

public class Press_ph_ok : Press_abstract
{
	public string btn_name;

	public Menu menu;

	private Btn_activation_manager btn_manager;

	public bool is_parent;

	private void Start()
	{
		menu = GameObject.Find("Camera").GetComponent<Menu>();
		btn_name = base.gameObject.name;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
	}

	public override void action()
	{
		if (!is_parent)
		{
			PreloaderManager.PutOffScreen(base.transform.parent.gameObject.name);
		}
		else
		{
			PreloaderManager.PutOffScreen(base.gameObject.name);
		}
	}
}
