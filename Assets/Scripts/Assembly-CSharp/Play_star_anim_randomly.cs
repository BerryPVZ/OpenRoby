using System.Collections;
using UnityEngine;

public class Play_star_anim_randomly : MonoBehaviour
{
	public int n;

	private int n1;

	public float time;

	public bool collided;

	public Vector3 tmp;

	public float distance_to_Char = 55f;

	public Object star_blink_obj;

	public GameObject star_blink;

	public int pack;

	public GameObject[] balls;

	public GameObject ball1;

	public GameObject ball2;

	public AudioClip[] sounds;

	public AudioSource audio_source;

	private void Start()
	{
		sounds = new AudioClip[3];
		for (int i = 0; i < 3; i++)
		{
			sounds[i] = AudioRuntime.LoadClip("pic 9.2." + (i + 1));
		}
		audio_source = AudioRuntime.Ensure2DSource(base.gameObject);
		star_blink_obj = Resources.Load("starBlink");
		if ((bool)GameObject.FindGameObjectWithTag("ball"))
		{
			balls = GameObject.FindGameObjectsWithTag("ball");
		}
		else
		{
			balls = new GameObject[1];
			balls[0] = GameObject.Find("robot4");
		}
		StartCoroutine(check());
	}

	private void Update()
	{
	}

	private IEnumerator play_anim()
	{
		n = Random.Range(1, 3);
		base.GetComponent<Animation>().Play("starInGame" + n);
		yield return new WaitForSeconds(time);
		while (!collided)
		{
			yield return new WaitForSeconds(0.1f);
			if (!base.GetComponent<Animation>().IsPlaying("starInGame" + n))
			{
				n = Random.Range(1, 3);
				base.GetComponent<Animation>().Play("starInGame" + n);
			}
		}
	}

	private IEnumerator buble()
	{
		yield return new WaitForSeconds(0.1f);
		star_blink = (GameObject)Object.Instantiate(star_blink_obj);
		star_blink.transform.position = base.gameObject.transform.position;
	}

	public void restart()
	{
		collided = false;
		StartCoroutine(play_anim());
		StartCoroutine(check());
		base.gameObject.GetComponent<Renderer>().enabled = true;
	}

	private IEnumerator check()
	{
		while (!collided)
		{
			GameObject[] array = balls;
			foreach (GameObject g in array)
			{
				if (g.Equals(null) || !((g.transform.position - base.gameObject.transform.position).magnitude < distance_to_Char))
				{
					continue;
				}
				collided = true;
				StartCoroutine(buble());
				foreach (Transform t in base.gameObject.transform.Find("gearInGame_"))
				{
					t.GetComponent<Renderer>().enabled = false;
				}
				int soundIndex = Mathf.Clamp(PlayerPreperencesManager.GetCollectedStars(), 0, sounds.Length - 1);
				AudioRuntime.Play(audio_source, sounds[soundIndex]);
				PlayerPreperencesManager.AddStar();
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
