using System.Collections;
using UnityEngine;

public class Slider : MonoBehaviour
{
	public bool start;

	public Vector3 temp;

	public Vector3 levels;

	public float x;

	public float delta;

	public float levels_x;

	public float D;

	public float E = 30f;

	public float k = 100f;

	public bool play_dovodchick = true;

	private Vector3 touch_0;

	public float limit = 100f;

	public float[] slider_pos;

	public Mesh[] procrutka_meshes;

	public bool slide_finished = true;

	public float h_w;

	public bool play_focus = true;

	public bool started_on_select_level;

	private int counter;

	private Proportion_manager p_m;

	private float start_time;

	private Btn_activation_manager btn_manager;

	private IEnumerator Start()
	{
		btn_manager = GameObject.Find("Camera").GetComponent<Btn_activation_manager>();
		p_m = GameObject.Find("Camera").GetComponent<Proportion_manager>();
		yield return new WaitForSeconds(0.05f);
		h_w = p_m.GetH_w();
		float fi = 0f;
		for (int i = 1; i < slider_pos.Length; i++)
		{
			fi = 1 * i;
			slider_pos[i] = fi * h_w;
		}
		D = GameObject.Find("SelectLevel").transform.position.x;
		start_time = Time.time;
	}

	private void slide_level(float d)
	{
		levels.x = x + delta;
		GameObject.Find("SelectLevel").transform.position = levels;
	}

	private IEnumerator run_new_dovodchick()
	{
		if (slide_finished)
		{
			slide_finished = false;
			float dt = 0.01f;
			Vector3 tmp = GameObject.Find("SelectLevel").transform.position;
			float closest_dist = 10000f;
			int closest_n = 0;
			for (int n = 0; n < slider_pos.Length; n++)
			{
				if (Mathf.Abs(tmp.x - slider_pos[n]) < closest_dist)
				{
					closest_dist = Mathf.Abs(tmp.x - slider_pos[n]);
					closest_n = n;
				}
			}
			int multiplier = 0;
			multiplier = ((!(tmp.x >= slider_pos[closest_n])) ? 1 : (-1));
			while (Mathf.Abs(GameObject.Find("SelectLevel").transform.position.x - slider_pos[closest_n]) > E && GameObject.Find("SelectLevel").transform.position.x < h_w * (float)slider_pos.Length && GameObject.Find("SelectLevel").transform.position.x > 0f - h_w)
			{
				float X1 = Mathf.Abs(GameObject.Find("SelectLevel").transform.position.x - slider_pos[closest_n]) + 20f;
				float dS = 8f * k * X1 * dt * dt;
				tmp.x += dS * (float)multiplier;
				GameObject.Find("SelectLevel").transform.position = tmp;
				yield return new WaitForSeconds(dt);
			}
			yield return new WaitForSeconds(dt);
			play_dovodchick = true;
			yield return new WaitForSeconds(dt);
			slide_finished = true;
		}
		yield return new WaitForSeconds(0.01f);
	}

	public IEnumerator slide_page(bool slide_right)
	{
		if (slide_finished)
		{
			slide_finished = false;
			float dt = 0.01f;
			Vector3 tmp = GameObject.Find("SelectLevel").transform.position;
			float closest_dist = 10000f;
			int closest_n = 0;
			for (int n = 0; n < slider_pos.Length; n++)
			{
				if (Mathf.Abs(tmp.x - slider_pos[n]) < closest_dist)
				{
					closest_dist = Mathf.Abs(tmp.x - slider_pos[n]);
					closest_n = n;
				}
			}
			int multiplier = 0;
			bool stop_function = false;
			if (slide_right)
			{
				if (closest_n + 1 >= slider_pos.Length)
				{
					stop_function = true;
				}
				else
				{
					multiplier = 1;
					closest_n++;
				}
			}
			else if (closest_n - 1 < 0)
			{
				stop_function = true;
			}
			else
			{
				closest_n--;
				multiplier = -1;
			}
			if (!stop_function)
			{
				while (Mathf.Abs(GameObject.Find("SelectLevel").transform.position.x - slider_pos[closest_n]) > E && GameObject.Find("SelectLevel").transform.position.x < h_w * (float)slider_pos.Length && GameObject.Find("SelectLevel").transform.position.x > 0f - h_w)
				{
					float X1 = Mathf.Abs(GameObject.Find("SelectLevel").transform.position.x - slider_pos[closest_n]) + 20f;
					float dS = 8f * k * X1 * dt * dt;
					tmp.x += dS * (float)multiplier;
					GameObject.Find("SelectLevel").transform.position = tmp;
					yield return new WaitForSeconds(dt);
				}
			}
			yield return new WaitForSeconds(dt);
			play_dovodchick = true;
			yield return new WaitForSeconds(dt);
			slide_finished = true;
		}
		yield return new WaitForSeconds(0.01f);
	}

	private void Update()
	{
		if (GameObject.Find("btn_pack_01").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_02").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_03").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_04").GetComponent<Animation>().isPlaying || Mathf.Abs(GameObject.Find("Select_Level_01").transform.position.y) < 120f || Mathf.Abs(GameObject.Find("game_Name").transform.position.y) < 300f || !slide_finished || Input.touchCount <= 0 || !btn_manager.Button_active())
		{
			return;
		}
		RaycastHit hitInfo = default(RaycastHit);
		touch_0.x = Input.GetTouch(0).position.x;
		touch_0.y = Input.GetTouch(0).position.y;
		Ray ray = Camera.main.ScreenPointToRay(touch_0);
		if (!Physics.Raycast(ray, out hitInfo))
		{
			return;
		}
		if (hitInfo.collider.tag.Equals("slider") || hitInfo.collider.tag.Equals("pack"))
		{
			if (GameObject.Find("btn_pack_01").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_02").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_03").GetComponent<Animation>().isPlaying || GameObject.Find("btn_pack_04").GetComponent<Animation>().isPlaying)
			{
				return;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				started_on_select_level = true;
				start = true;
				x = touch_0.x;
				levels = GameObject.Find("SelectLevel").transform.position;
				levels_x = GameObject.Find("SelectLevel").transform.position.x;
				if (hitInfo.collider.tag.Equals("pack") && Mathf.Abs(hitInfo.collider.transform.position.x) > 0.8f * h_w)
				{
					play_focus = true;
				}
				else
				{
					play_focus = false;
				}
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved && start)
			{
				delta = touch_0.x - x;
				levels.x = levels_x - delta;
				if (levels.x < h_w * (float)slider_pos.Length && levels.x > 0f - h_w)
				{
					GameObject.Find("SelectLevel").transform.position = levels;
				}
			}
			if (Input.GetTouch(0).phase != TouchPhase.Ended)
			{
				return;
			}
			start = false;
			if (hitInfo.collider.tag.Equals("pack") && Mathf.Abs(hitInfo.collider.transform.position.x) > 0.8f * h_w && play_focus)
			{
				PackHelp.TurnOff();
				if (hitInfo.collider.transform.position.x < 0f)
				{
					StartCoroutine(slide_page(true));
				}
				else
				{
					StartCoroutine(slide_page(false));
				}
				play_dovodchick = false;
			}
			else if (play_dovodchick)
			{
				StartCoroutine(run_new_dovodchick());
				play_dovodchick = false;
			}
		}
		else if (hitInfo.collider.name.Equals("btn_MM_back") && Input.GetTouch(0).phase == TouchPhase.Ended && GameObject.Find("pack_tube").transform.position.x.Equals(0f))
		{
			MonoBehaviour.print("0");
			if (play_dovodchick)
			{
				MonoBehaviour.print("1");
				if (start)
				{
					StartCoroutine(run_new_dovodchick());
				}
			}
			start = false;
		}
		else
		{
			started_on_select_level = false;
		}
	}

	public void SetPositionToProcrutka(bool run)
	{
		StartCoroutine(PrepareProkrutka(run));
	}

	private IEnumerator PrepareProkrutka(bool run)
	{
		yield return new WaitForSeconds(0.1f);
		int i = PlayerPrefs.GetInt("cur_pack");
		if (i < 1)
		{
			i = 1;
		}
		if (run)
		{
			if (i > 1)
			{
				Set_y_pos("Btn_Pack_0" + i, 600f);
				GameObject.Find("btn_pack_01").GetComponent<Animation>().Play("pack_right_01");
				GameObject.Find("btn_pack_02").GetComponent<Animation>().Play("pack_right_01");
			}
			Vector3 tmp = GameObject.Find("SelectLevel").transform.position;
			tmp.x = slider_pos[i - 1];
			GameObject.Find("SelectLevel").transform.position = tmp;
			Menu.play_animation_pack("01");
		}
		else if (i > 1)
		{
			Vector3 tmp2 = GameObject.Find("SelectLevel").transform.position;
			tmp2.x = slider_pos[i - 1];
			GameObject.Find("SelectLevel").transform.position = tmp2;
			if (i - 1 >= 0)
			{
				Set_y_pos("btn_pack_0" + (i - 1), PlayerPrefs.GetFloat("btn_pack_02"));
				Set_local_x_pos("btn_pack_0" + (i - 1), -165f);
			}
			if (i + 1 <= 4)
			{
				Set_y_pos("btn_pack_0" + (i + 1), PlayerPrefs.GetFloat("btn_pack_02"));
				Set_local_x_pos("btn_pack_0" + (i + 1), 165f);
			}
			if (i > 2)
			{
				Set_y_pos("btn_pack_01", PlayerPrefs.GetFloat("btn_pack_02"));
			}
			if (i > 3)
			{
				GameObject.Find("btn_pack_02").GetComponent<Animation>().Play("pack_right_01");
			}
			Set_y_pos("btn_pack_0" + i, PlayerPrefs.GetFloat("btn_pack_01"));
			if (i == 2)
			{
				Set_x_pos("btn_pack_02", 0f);
			}
		}
	}

	private void Set_y_pos(string name, float pos)
	{
		Vector3 position = GameObject.Find(name).transform.position;
		position.y = pos;
		GameObject.Find(name).transform.position = position;
	}

	private void Set_x_pos(string name, float pos)
	{
		Vector3 position = GameObject.Find(name).transform.position;
		position.x = pos;
		GameObject.Find(name).transform.position = position;
	}

	private void Set_local_x_pos(string name, float pos)
	{
		Vector3 localPosition = GameObject.Find(name).transform.localPosition;
		localPosition.x = pos;
		GameObject.Find(name).transform.localPosition = localPosition;
	}

	public bool GetSlideFinished()
	{
		return slide_finished;
	}

	public void OnApplicationPause(bool pause)
	{
		if (!pause && Time.time - start_time > 2f)
		{
			StartCoroutine(run_new_dovodchick());
			play_dovodchick = false;
			start = false;
		}
	}
}
