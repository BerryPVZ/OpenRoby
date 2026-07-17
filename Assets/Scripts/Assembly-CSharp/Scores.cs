using System.Collections;
using UnityEngine;

public class Scores : MonoBehaviour
{
	public int t;

	private PlayerScoresManager psm;

	private bool stop;

	private void Start()
	{
		t = 0;
		StartCoroutine(StartTimer());
		psm = GameObject.Find("Camera").GetComponent<PlayerScoresManager>();
		stop = false;
	}

	private IEnumerator StartTimer()
	{
		while (!stop)
		{
			yield return new WaitForSeconds(1f);
			t++;
		}
	}

	public void StopTimer()
	{
		stop = true;
	}

	public void ShowScores()
	{
		int num = PlayerPrefs.GetInt("stars_level_" + PlayerPrefs.GetInt("cur_level") + "_");
		int num2 = 3000;
		num2 += num * 1000;
		num2 -= 150 * t;
		if (num2 < 0)
		{
			num2 = 0;
		}
		SetTextures("n_c_", VictoryScoresToArr(num2));
		PlayerPreperencesManager.SetScoresProgress(num2);
		PlayerPreperencesManager.SetStarsProgress();
		if (PlayerPreperencesManager.GetScoresProgress() <= num2)
		{
			SetTextures("n_c_b_", VictoryScoresToArr(num2));
		}
		else
		{
			SetTextures("n_c_b_", VictoryScoresToArr(PlayerPreperencesManager.GetScoresProgress()));
		}
		SendVictoryFlurry();
	}

	private void SendVictoryFlurry()
	{
		int num = PlayerPrefs.GetInt("cur_pack");
		int num2 = PlayerPrefs.GetInt("cur_level");
		int collectedStars = PlayerPreperencesManager.GetCollectedStars();
		string text = "Won pack " + num + " level " + num2 + " stars " + collectedStars + " time spent";
		if (t < 5)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 0-5 sec");
		}
		else if (t < 10)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 5-10 sec");
		}
		else if (t < 15)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 10-15 sec");
		}
		else if (t < 20)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 15-20 sec");
		}
		else if (t < 25)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 20-25 sec");
		}
		else if (t < 30)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 25-30 sec");
		}
		else if (t < 35)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 30-35 sec");
		}
		else if (t < 40)
		{
			FlurryAndroidiOSManager.EventSimple(text + " 34-40 sec");
		}
		else
		{
			FlurryAndroidiOSManager.EventSimple(text + " 0-5 sec");
		}
	}

	private int[] VictoryScoresToArr(int total_scores)
	{
		int[] array = new int[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = total_scores / (int)Mathf.Pow(10f, 3 - i);
			total_scores -= array[i] * (int)Mathf.Pow(10f, 3 - i);
		}
		return array;
	}

	private void SetTextures(string base_name, int[] ts)
	{
		for (int i = 1; i <= ts.Length; i++)
		{
			GameObject.Find(base_name + i).GetComponent<MeshFilter>().mesh = psm.GetTexture(ts[i - 1]);
		}
	}

	private void Update()
	{
	}
}
