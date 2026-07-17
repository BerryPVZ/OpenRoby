using UnityEngine;

public class RunStartEmo : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void CreateStartBubble(string s)
	{
		Object.Instantiate(Resources.Load(s));
		MonoBehaviour.print("created");
	}
}
