using UnityEngine;

public class StartLevel : MonoBehaviour
{
	private VictoryManager vm;

	private void Start()
	{
		PreloaderManager.PutOffScreen();
		PlayerPreperencesManager.RefreshStarsCounter();
		vm = GameObject.Find("Camera").GetComponent<VictoryManager>();
		vm.restarted = false;
		GameObject[] array = GameObject.FindGameObjectsWithTag("complete_star_particle");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (!gameObject.Equals(null))
			{
				Object.Destroy(gameObject);
			}
		}
	}

	private void Update()
	{
	}
}
