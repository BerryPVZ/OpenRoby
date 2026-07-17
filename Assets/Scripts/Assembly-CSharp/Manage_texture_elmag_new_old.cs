using UnityEngine;

public class Manage_texture_elmag_new_old : MonoBehaviour
{
	public Texture2D p_on;

	public Texture2D p_rel;

	public int level_number;

	public AudioClip sound;

	public bool switcher;

	private Btn_activation_manager btn_manager;

	private bool level_icon;

	public bool power;

	private bool touch;

	private Vector3 touch_0;

	private void Start()
	{
		if (!power)
		{
			base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
		}
		else
		{
			base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
		}
		switcher = true;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		if (base.gameObject.tag.Equals("level_icon"))
		{
			level_icon = true;
		}
		ChangeWaves();
	}

	private void Update()
	{
		if (!btn_manager.Button_active() || Input.touchCount <= 0)
		{
			return;
		}
		touch = true;
		RaycastHit hitInfo = default(RaycastHit);
		touch_0.x = Input.GetTouch(0).position.x;
		touch_0.y = Input.GetTouch(0).position.y;
		Ray ray = Camera.main.ScreenPointToRay(touch_0);
		if (Input.GetTouch(0).phase == TouchPhase.Began && btn_manager.Button_active() && Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
		{
			power = !power;
			ChangeWaves();
			if (power)
			{
				base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
			}
			else
			{
				base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
			}
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound);
			}
		}
	}

	private void OnMouseDown()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || !btn_manager.Button_active() || touch)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		touch_0.x = Input.mousePosition.x;
		touch_0.y = Input.mousePosition.y;
		Ray ray = Camera.main.ScreenPointToRay(touch_0);
		if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
		{
			power = !power;
			ChangeWaves();
			if (power)
			{
				base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_on;
			}
			else
			{
				base.transform.Find("electromagnet_btn").GetComponent<Renderer>().material.mainTexture = p_rel;
			}
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				AudioRuntime.PlayOneShot(sound);
			}
		}
	}

	public void PlaySound()
	{
		if (sound != null && PlayerPreperencesManager.GetSound())
		{
			AudioRuntime.PlayOneShot(sound);
		}
	}

	private void ChangeWaves()
	{
		for (int i = 3; i <= 7; i++)
		{
			if ((bool)GameObject.Find("wave_" + i))
			{
				base.transform.Find("wave_" + i).GetComponent<Renderer>().enabled = power;
			}
		}
	}
}
