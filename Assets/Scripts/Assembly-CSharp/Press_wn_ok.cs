using UnityEngine;

public class Press_wn_ok : Press_abstract
{
	public string btn_name;

	public Menu menu;

	private Btn_activation_manager btn_manager;

	public bool is_parent;

	private GameObject parent;

	private void Start()
	{
		menu = GameObject.Find("Camera").GetComponent<Menu>();
		btn_name = base.gameObject.name;
		if (btn_name.Equals("Whats_new"))
		{
			parent = base.gameObject;
		}
		else
		{
			parent = base.transform.parent.gameObject;
		}
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
	}

	public override void action()
	{
		PlayerPrefs.SetInt("what's new 10.01", 1);
		Object.Destroy(parent);
	}
}
