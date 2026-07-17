using System.Collections;
using UnityEngine;

public class Manage_mesh_sound_btn : MonoBehaviour
{
	public Mesh p_on_on;

	public Mesh p_on_rel;

	public Mesh p_off_on;

	public Mesh p_off_rel;

	public int level_number;

	public AudioClip sound;

	public bool switcher;

	private Btn_activation_manager btn_manager;

	public bool is_on;

	private void Start()
	{
		StartCoroutine(SetStartTexture());
		switcher = true;
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		if (!base.gameObject.GetComponent<AudioSource>())
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.GetComponent<AudioSource>().clip = sound;
		base.GetComponent<AudioSource>().volume = 0.6f;
		base.GetComponent<AudioSource>().playOnAwake = false;
		base.GetComponent<AudioSource>().spatialBlend = 0f;
	}

	private IEnumerator SetStartTexture()
	{
		yield return new WaitForSeconds(0.1f);
		is_on = PlayerPreperencesManager.GetSound();
		if (is_on)
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
		}
		else
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
		}
	}

	private void Update()
	{
		if (!btn_manager.Button_active() || Input.touchCount <= 0)
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.transform.gameObject == base.gameObject)
				{
					if (is_on)
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_on_on;
					}
					else
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_off_on;
					}
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
		if (Input.GetTouch(0).phase == TouchPhase.Moved && Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.transform.gameObject == base.gameObject && switcher)
			{
				if (is_on)
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_on_on;
				}
				else
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_off_on;
				}
			}
			else
			{
				if (is_on)
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
				}
				else
				{
					base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
				}
				switcher = false;
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
					if (is_on)
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_on_on;
					}
					else
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_off_on;
					}
				}
				else
				{
					if (is_on)
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
					}
					else
					{
						base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
					}
					switcher = false;
				}
			}
		}
		if (Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.gameObject == base.gameObject)
			{
				is_on = !is_on;
			}
			if (is_on)
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
				PlayerPreperencesManager.TurnSoundOn();
			}
			else
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
				PlayerPreperencesManager.TurnSoundOff();
			}
		}
	}

	private void OnMouseDown()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || !btn_manager.Button_active())
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
			if (is_on)
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_on_on;
			}
			else
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_off_on;
			}
			if (sound != null && PlayerPreperencesManager.GetSound())
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
		else if (is_on)
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
		}
		else
		{
			base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
		}
	}

	private void OnMouseUp()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android)
		{
			is_on = !is_on;
			if (is_on)
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_on_rel;
				PlayerPreperencesManager.TurnSoundOn();
			}
			else
			{
				base.gameObject.GetComponent<MeshFilter>().mesh = p_off_rel;
				PlayerPreperencesManager.TurnSoundOff();
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
}
