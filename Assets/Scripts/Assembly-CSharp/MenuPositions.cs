using UnityEngine;

public class MenuPositions : MonoBehaviour
{
	private Proportion_manager pm;

	private PlayerScoresManager psm;

	private void Start()
	{
		pm = GameObject.Find("Camera").GetComponent<Proportion_manager>();
		pm.SetMenu();
		psm = GameObject.Find("Camera").GetComponent<PlayerScoresManager>();
		psm.UpdateSelectPack();
	}
}
