using UnityEngine;

public class Manage_texture : MonoBehaviour
{
	public Texture2D p_on;

	public Texture2D p_rel;

	public int level_number;

	public AudioClip sound;

	public bool switcher;

	private void Start()
	{
		base.GetComponent<Renderer>().material.mainTexture = p_rel;
		switcher = true;
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
		if (Input.touchCount <= 0 || PlayerPrefs.GetInt("levels_total_pack_" + PlayerPrefs.GetInt("pack")) < level_number)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.transform.gameObject == base.gameObject)
			{
				if (base.tag.Equals("level_icon"))
				{
					if (PlayerPrefs.GetInt("levels_won_pack_" + PlayerPrefs.GetInt("pack")) > level_number)
					{
						base.GetComponent<Renderer>().material.mainTexture = p_on;
						if (sound != null && switcher && PlayerPreperencesManager.GetSound())
						{
							base.GetComponent<AudioSource>().Play();
						}
						switcher = false;
					}
				}
				else
				{
					base.GetComponent<Renderer>().material.mainTexture = p_on;
					if (sound != null && switcher)
					{
						AudioRuntime.PlayOneShot(sound);
					}
					switcher = false;
				}
			}
			else if (base.tag.Equals("level_icon"))
			{
				if (PlayerPrefs.GetInt("levels_won_pack_" + PlayerPrefs.GetInt("pack")) > level_number)
				{
					base.GetComponent<Renderer>().material.mainTexture = p_rel;
					switcher = true;
				}
			}
			else
			{
				base.GetComponent<Renderer>().material.mainTexture = p_rel;
				switcher = true;
			}
		}
		if (Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			return;
		}
		if (base.tag.Equals("level_icon"))
		{
			if (PlayerPrefs.GetInt("levels_won_pack_" + PlayerPrefs.GetInt("pack")) > level_number)
			{
				base.GetComponent<Renderer>().material.mainTexture = p_rel;
				switcher = true;
			}
		}
		else
		{
			base.GetComponent<Renderer>().material.mainTexture = p_rel;
			switcher = true;
		}
	}

	private void OnMouseDown()
	{
		if (PlayerPrefs.GetInt("levels_total_pack_" + PlayerPrefs.GetInt("pack")) < level_number)
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
			base.GetComponent<Renderer>().material.mainTexture = p_on;
			if (sound != null && PlayerPrefs.GetInt("levels_won_pack_" + PlayerPrefs.GetInt("pack")) >= level_number && PlayerPreperencesManager.GetSound())
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			base.GetComponent<Renderer>().material.mainTexture = p_rel;
		}
	}

	private void OnMouseUp()
	{
		if (PlayerPrefs.GetInt("levels_won_pack_" + PlayerPrefs.GetInt("pack")) < level_number)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.transform.gameObject == base.gameObject)
			{
				base.GetComponent<Renderer>().material.mainTexture = p_rel;
			}
			else
			{
				base.GetComponent<Renderer>().material.mainTexture = p_rel;
			}
		}
	}
}
