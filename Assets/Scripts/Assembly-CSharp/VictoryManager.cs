using System.Collections;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
	public float popravka_na_weter;

	private AudioClip[] xvatalki_sounds;

	private int n;

	public bool restarted;

	private Object star_particle;

	public GameObject level;

	private Vector3[] victoryStarLocalPositions;

	private Vector3[] victoryStarLocalScales;

	private bool[] victoryStarSlotsCached;

	private void Start()
	{
		victoryStarLocalPositions = new Vector3[3];
		victoryStarLocalScales = new Vector3[3];
		victoryStarSlotsCached = new bool[3];
		CacheVictoryStarSlots();

		xvatalki_sounds = new AudioClip[2];
		xvatalki_sounds[0] = AudioRuntime.LoadClip("sounds/Lift");
		xvatalki_sounds[1] = AudioRuntime.LoadClip("sounds/Lift_Tech");
		star_particle = Resources.Load("starInGameParticle");
	}

	private IEnumerator w(int pack)
	{
		restarted = false;
		level = GameObject.FindGameObjectWithTag("level");
		yield return new WaitForSeconds(0.1f);
		if (pack.Equals(2) && ((bool)GameObject.FindGameObjectWithTag("ball") || !pack.Equals(2)))
		{
			yield break;
		}
		yield return new WaitForSeconds(2.2f);
		if (pack == 2 && (bool)GameObject.Find("xvatalka_2"))
		{
			while (GameObject.Find("xvatalka_2").GetComponent<Animation>().isPlaying || GameObject.Find("xvatalka_1").GetComponent<Animation>().isPlaying)
			{
				yield return new WaitForSeconds(0.1f);
			}
		}
		if (!(level == null) && (!(Mathf.Abs(GameObject.Find("level_complited_window").transform.position.y) < 100f) || !(Mathf.Abs(GameObject.Find("level_complited_window").transform.position.x) < 100f)) && !GameObject.Find("level_failed_window").GetComponent<Animation>().isPlaying && (!(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.y) < 100f) || !(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.x) < 100f)) && !restarted)
		{
			for (int i = 1; i <= 3; i++)
			{
				GameObject.Find("star_GUI_" + i).GetComponent<Renderer>().enabled = false;
			}
			GameObject.Find("level_complited_window").GetComponent<Animation>().Play("level_completed_01");
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("level_complited_window").GetComponent<AudioSource>().Play();
			}
			Scores s = GameObject.FindGameObjectWithTag("level").GetComponent<Scores>();
			s.ShowScores();
			PlayerPreperencesManager.SetWonLevels();
			yield return new WaitForSeconds(0.5f);
			PrepareVictoryStars();
		}
	}

	public IEnumerator w_3(int pack)
	{
		if ((!(Mathf.Abs(GameObject.Find("level_complited_window").transform.position.y) < 100f) || !(Mathf.Abs(GameObject.Find("level_complited_window").transform.position.x) < 100f)) && !GameObject.Find("level_failed_window").GetComponent<Animation>().isPlaying && (!(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.y) < 100f) || !(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.x) < 100f)) && !restarted)
		{
			for (int i = 1; i <= 3; i++)
			{
				GameObject.Find("star_GUI_" + i).GetComponent<Renderer>().enabled = false;
			}
			GameObject.Find("level_complited_window").GetComponent<Animation>().Play("level_completed_01");
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("level_complited_window").GetComponent<AudioSource>().Play();
			}
			Scores s = GameObject.FindGameObjectWithTag("level").GetComponent<Scores>();
			s.ShowScores();
			PlayerPreperencesManager.SetWonLevels();
			yield return new WaitForSeconds(0.5f);
			PrepareVictoryStars();
		}
	}

	private void PrepareVictoryStars()
	{
		int num = PlayerPrefs.GetInt("stars_level_" + PlayerPrefs.GetInt("cur_level") + "_");
		if (num > 0)
		{
			StartCoroutine(RunVictoryStars(num));
		}
	}

	private IEnumerator RunVictoryStars(int starCount)
	{
		CacheVictoryStarSlots();
		starCount = Mathf.Clamp(starCount, 0, 3);

		for (int i = 1; i <= starCount; i++)
		{
			if (!restarted)
			{
				GameObject star = GameObject.Find("star_GUI_" + i);
				if (star != null)
				{
					Animation legacyAnimation = star.GetComponent<Animation>();
					if (legacyAnimation != null)
					{
						// The recovered code played star_GUI_1 on every icon. That
						// clip contains position curves, so all three icons converged
						// on the first slot. Use a position-safe pop instead.
						legacyAnimation.Stop();
					}

					int index = i - 1;
					if (victoryStarSlotsCached[index])
					{
						star.transform.localPosition = victoryStarLocalPositions[index];
					}

					Renderer starRenderer = star.GetComponent<Renderer>();
					if (starRenderer != null)
					{
						starRenderer.enabled = true;
					}

					StartCoroutine(PopVictoryStar(star, index));

					if (star_particle != null)
					{
						GameObject particle = (GameObject)Object.Instantiate(star_particle);
						particle.transform.position = star.transform.position;
					}
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
	}

	private void CacheVictoryStarSlots()
	{
		if (victoryStarLocalPositions == null || victoryStarLocalPositions.Length != 3)
		{
			victoryStarLocalPositions = new Vector3[3];
			victoryStarLocalScales = new Vector3[3];
			victoryStarSlotsCached = new bool[3];
		}

		for (int i = 0; i < 3; i++)
		{
			if (victoryStarSlotsCached[i])
			{
				continue;
			}

			GameObject star = GameObject.Find("star_GUI_" + (i + 1));
			if (star != null)
			{
				victoryStarLocalPositions[i] = star.transform.localPosition;
				victoryStarLocalScales[i] = star.transform.localScale;
				victoryStarSlotsCached[i] = true;
			}
		}
	}

	private IEnumerator PopVictoryStar(GameObject star, int index)
	{
		if (star == null || index < 0 || index >= 3 || !victoryStarSlotsCached[index])
		{
			yield break;
		}

		Vector3 targetPosition = victoryStarLocalPositions[index];
		Vector3 targetScale = victoryStarLocalScales[index];
		star.transform.localPosition = targetPosition;
		star.transform.localScale = Vector3.zero;

		float elapsed = 0f;
		const float duration = 0.22f;
		while (elapsed < duration && star != null && !restarted)
		{
			elapsed += Time.deltaTime;
			float amount = Mathf.Clamp01(elapsed / duration);
			amount = Mathf.SmoothStep(0f, 1f, amount);
			star.transform.localPosition = targetPosition;
			star.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, amount);
			yield return null;
		}

		if (star != null)
		{
			star.transform.localPosition = targetPosition;
			star.transform.localScale = targetScale;
		}
	}

	public void RunHvatalka(GameObject box, int pack)
	{
		if (box.name.Equals("main_box_bottom"))
		{
			Vector3 position = box.transform.position;
			position.y += popravka_na_weter;
			position.z = -180f;
			GameObject.Find("xvatalka_1").GetComponent<Animation>().Play("xvatalka_1");
			GameObject.Find("xvatalka_1").transform.position = position;
			GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.AddComponent<AudioSource>();
			GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().clip = xvatalki_sounds[0];
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().Play();
			}
		}
		if (box.name.Equals("main_box_bottom_h"))
		{
			Vector3 position2 = box.transform.position;
			position2.y += popravka_na_weter;
			position2.z = -180f;
			GameObject.Find("xvatalka_2").GetComponent<Animation>().Play("xvatalka_1");
			MonoBehaviour.print("2-2");
			GameObject.Find("xvatalka_2").transform.position = position2;
			GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.AddComponent<AudioSource>();
			GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().clip = xvatalki_sounds[0];
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().Play();
			}
		}
		if (box.name.Equals("main_box_4"))
		{
			Vector3 position3 = box.transform.position;
			position3.y += popravka_na_weter;
			position3.z = -180f;
			GameObject.Find("xvatalka_4").GetComponent<Animation>().Play("xvatalka_4");
			GameObject.Find("xvatalka_4").transform.position = position3;
			GameObject.Find("xvatalka_4").transform.Find("teleport").gameObject.AddComponent<AudioSource>();
			GameObject.Find("xvatalka_4").transform.Find("teleport").gameObject.GetComponent<AudioSource>().clip = xvatalki_sounds[1];
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("xvatalka_4").transform.Find("teleport").gameObject.GetComponent<AudioSource>().Play();
			}
		}
		Object.Destroy(box);
		StartCoroutine(w(pack));
	}

	public IEnumerator RunHvatalkaPack2(GameObject box, GameObject box2, int pack)
	{
		MonoBehaviour.print("started coroutine");
		if (box.name.Equals("main_box_bottom"))
		{
			Vector3 tmp = box.transform.position;
			tmp.y += popravka_na_weter;
			tmp.z = -180f;
			GameObject.Find("xvatalka_1").GetComponent<Animation>().Play("xvatalka_1");
			MonoBehaviour.print("1-1");
			GameObject.Find("xvatalka_1").transform.position = tmp;
			GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.AddComponent<AudioSource>();
			GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().clip = xvatalki_sounds[0];
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("xvatalka_1").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().Play();
			}
		}
		if (box.name.Equals("main_box_bottom_h"))
		{
			Vector3 tmp2 = box.transform.position;
			tmp2.y += popravka_na_weter;
			tmp2.z = -180f;
			GameObject.Find("xvatalka_2").GetComponent<Animation>().Play("xvatalka_1");
			MonoBehaviour.print("1-2");
			GameObject.Find("xvatalka_2").transform.position = tmp2;
			GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.AddComponent<AudioSource>();
			GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().clip = xvatalki_sounds[0];
			if (PlayerPreperencesManager.GetSound())
			{
				GameObject.Find("xvatalka_2").transform.Find("atl_1").gameObject.GetComponent<AudioSource>().Play();
			}
		}
		Object.Destroy(box);
		yield return new WaitForSeconds(0.5f);
		RunHvatalka(box2, pack);
	}
}
