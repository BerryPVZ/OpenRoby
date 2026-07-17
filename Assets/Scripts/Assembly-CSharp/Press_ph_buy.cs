using UnityEngine;

public class Press_ph_buy : Press_abstract
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
	}
}
