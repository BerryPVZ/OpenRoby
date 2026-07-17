using System.Collections;
using UnityEngine;

public class Press_social : Press_abstract
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
		if (!btn_manager.Button_active())
		{
			return;
		}
		switch (btn_name)
		{
		case "btnFacebookRl":
			if (GameObject.Find("btnSocialRl").transform.position.y > -150f)
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_hide");
			}
			else
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_show");
			}
			btn_manager.Disactivate(0.67f);
			StartCoroutine(RunFaceBook());
			break;
		case "btnSocialRl":
			if (GameObject.Find("btnSoundRl").transform.position.y > -150f)
			{
				GameObject.Find("Sound").GetComponent<Animation>().Play("option_hide");
			}
			if (GameObject.Find("btnSocialRl").transform.position.y > -150f)
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_hide");
			}
			else
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_show");
			}
			btn_manager.Disactivate(0.67f);
			break;
		case "btnTwitterRl":
			if (GameObject.Find("btnSocialRl").transform.position.y > -150f)
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_hide");
			}
			else
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_show");
			}
			btn_manager.Disactivate(0.67f);
			StartCoroutine(RunTwitter());
			break;
		case "btnSoundRl":
			if (GameObject.Find("btnSocialRl").transform.position.y > -150f)
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_hide");
			}
			if (GameObject.Find("btnSoundRl").transform.position.y > -150f)
			{
				GameObject.Find("Sound").GetComponent<Animation>().Play("option_hide");
			}
			else
			{
				GameObject.Find("Sound").GetComponent<Animation>().Play("option_show");
			}
			btn_manager.Disactivate(0.67f);
			break;
		case "btnInfoRl":
			if (GameObject.Find("btnSocialRl").transform.position.y > -150f)
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_hide");
			}
			else
			{
				GameObject.Find("Social").GetComponent<Animation>().Play("social_show");
			}
			btn_manager.Disactivate(0.67f);
			StartCoroutine(RunInfo());
			break;
		}
	}

	private IEnumerator RunTwitter()
	{
		yield return new WaitForSeconds(0.67f);
		Application.OpenURL("https://twitter.com/intent/tweet?status=I%20am%20playing%20this%20really%20well-made%2C%20addicting%20app%2C%20called%20Rescue%20Roby%3A%20http%3A%2F%2Fwww.amazon.com/gp/product/B00B29J0I6");
	}

	private IEnumerator RunFaceBook()
	{
		yield return new WaitForSeconds(0.67f);
		Application.OpenURL("http://www.facebook.com/RescueRoby");
	}

	private IEnumerator RunInfo()
	{
		yield return new WaitForSeconds(0.67f);
		Application.OpenURL("http://www.runawaystudiosinc.com");
	}
}
