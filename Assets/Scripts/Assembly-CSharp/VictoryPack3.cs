using System.Collections;
using UnityEngine;

public class VictoryPack3 : MonoBehaviour
{
	public Object hearts;

	private Object clone_hearts;

	public AudioClip sound_kiss;

	public bool victory;

	private void Start()
	{
		victory = false;
	}

	private void Update()
	{
	}

	public bool GetVictory()
	{
		return victory;
	}

	private void run_emo()
	{
		if (!base.transform.Find("roby").transform.GetComponent<Animation>().IsPlaying("love"))
		{
			base.transform.Find("roby").transform.GetComponent<Animation>().Play("love");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag.Equals("ball") && !victory)
		{
			victory = true;
			StartCoroutine(V(collision));
			base.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private IEnumerator V(Collision collision)
	{
		if (PlayerPreperencesManager.GetSound())
		{
			AudioRuntime.PlayOneShot(sound_kiss);
		}
		double x_ball = base.transform.position.x;
		double x_ball_h = collision.transform.position.x;
		run_emo();
		if (x_ball < x_ball_h)
		{
			base.transform.eulerAngles = new Vector3(0f, 180f, -15f);
			collision.transform.eulerAngles = new Vector3(0f, 180f, 15f);
		}
		else
		{
			base.transform.eulerAngles = new Vector3(0f, 180f, 15f);
			collision.transform.eulerAngles = new Vector3(0f, 180f, -15f);
		}
		Scores s = GameObject.FindGameObjectWithTag("level").GetComponent<Scores>();
		s.StopTimer();
		yield return new WaitForSeconds(0.3f);
		collision.rigidbody.isKinematic = true;
		float x = collision.contacts[0].point.x;
		clone_hearts = Object.Instantiate(position: new Vector3(x, collision.contacts[0].point.y, -100f), original: hearts, rotation: Quaternion.identity);
		destroy_hearts();
		GameObject g = new GameObject();
		g.name = "love_hearts";
		g.transform.parent = GameObject.FindGameObjectWithTag("level").transform;
	}

	private void destroy_hearts()
	{
		StartCoroutine(d());
	}

	private IEnumerator d()
	{
		yield return new WaitForSeconds(2f);
		Object.Destroy(clone_hearts);
		e();
	}

	private void e()
	{
		VictoryManager component = GameObject.Find("Camera").GetComponent<VictoryManager>();
		StartCoroutine(component.w_3(3));
	}
}
