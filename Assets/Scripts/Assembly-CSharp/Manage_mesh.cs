using UnityEngine;

public class Manage_mesh : MonoBehaviour
{
	public Mesh p_on;

	public Mesh p_rel;

	public int level_number;

	public AudioClip sound;

	public bool switcher;

	private Btn_activation_manager btn_manager;

	private bool level_icon;

	private void Start()
	{
		base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
		switcher = true;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		if (base.gameObject.tag.Equals("level_icon"))
		{
			level_icon = true;
		}
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
		if ((level_icon && int.Parse(base.gameObject.name) > PlayerPrefs.GetInt("won_levels_pack_" + PlayerPrefs.GetInt("cur_pack"))) || !base.gameObject.GetComponent<Renderer>().enabled || !btn_manager.Button_active() || Input.touchCount <= 0)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (!btn_manager.Button_active())
			{
				return;
			}
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.transform.gameObject == base.gameObject)
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_on;
					switcher = true;
					if (sound != null && PlayerPreperencesManager.GetSound())
					{
						base.GetComponent<AudioSource>().Play();
					}
				}
				else
				{
					switcher = false;
				}
			}
			else
			{
				switcher = false;
			}
		}
		if (Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if (!btn_manager.Button_active())
			{
				return;
			}
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.transform.gameObject == base.gameObject && switcher)
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_on;
				}
				else
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
					switcher = false;
				}
			}
		}
		if (Input.GetTouch(0).phase == TouchPhase.Stationary)
		{
			if (!btn_manager.Button_active())
			{
				return;
			}
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.transform.gameObject == base.gameObject && switcher)
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_on;
				}
				else
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
					switcher = false;
				}
			}
		}
		if (Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
		}
	}

	private void OnMouseDown()
	{
		if ((level_icon && int.Parse(base.gameObject.name) > PlayerPrefs.GetInt("won_levels_pack_" + PlayerPrefs.GetInt("cur_pack"))) || !base.gameObject.GetComponent<Renderer>().enabled || !btn_manager.Button_active())
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out hitInfo))
		{
			return;
		}
		if (hitInfo.transform.gameObject == base.gameObject)
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_on;
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
		}
	}

	private void OnMouseUp()
	{
		if ((!level_icon || int.Parse(base.gameObject.name) <= PlayerPrefs.GetInt("won_levels_pack_" + PlayerPrefs.GetInt("cur_pack"))) && base.gameObject.GetComponent<Renderer>().enabled)
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_rel;
		}
	}

	public void PlaySound()
	{
		if (sound != null && PlayerPreperencesManager.GetSound())
		{
			base.GetComponent<AudioSource>().Play();
		}
	}
}
