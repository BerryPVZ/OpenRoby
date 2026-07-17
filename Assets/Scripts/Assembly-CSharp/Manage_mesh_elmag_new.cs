using UnityEngine;

public class Manage_mesh_elmag_new : MonoBehaviour
{
	public Mesh p_on;

	public Mesh p_rel;

	public int level_number;

	public AudioClip sound;

	public bool switcher;

	private Btn_activation_manager btn_manager;

	private bool level_icon;

	public bool power;

	private bool touch;

	private Vector3 touch_0;

	private MeshesManager mm;

	private void Start()
	{
		mm = GameObject.Find("Camera").GetComponent<MeshesManager>();
		p_on = mm.GetMeshes(1);
		p_rel = mm.GetMeshes(0);
		if (!power)
		{
			base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_rel;
		}
		else
		{
			base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_on;
		}
		switcher = true;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		if (base.gameObject.tag.Equals("level_icon"))
		{
			level_icon = true;
		}
		ChangeWaves();
		if (!base.gameObject.GetComponent<AudioSource>())
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.GetComponent<AudioSource>().clip = sound;
		base.GetComponent<AudioSource>().volume = 0.6f;
		base.GetComponent<AudioSource>().playOnAwake = false;
		base.GetComponent<AudioSource>().spatialBlend = 0f;
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
				base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_on;
			}
			else
			{
				base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_rel;
			}
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				base.GetComponent<AudioSource>().Play();
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
				base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_on;
			}
			else
			{
				base.transform.Find("electromagnet_btn").GetComponent<MeshFilter>().mesh = p_rel;
			}
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
	}

	public void PlaySound()
	{
		if (sound != null && PlayerPreperencesManager.GetSound())
		{
			base.GetComponent<AudioSource>().Play();
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
