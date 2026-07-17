using System.Collections;
using UnityEngine;

public class Hand_bubble2 : MonoBehaviour
{
	public string anim_appear;

	public string anim_move;

	public string anim_disappear;

	public string buble;

	public bool third_hand = true;

	private IEnumerator Start()
	{
		bool animate = true;
		base.GetComponent<Animation>().Play(anim_move);
		while (animate)
		{
			yield return new WaitForSeconds(0.1f);
			if (third_hand)
			{
				if (!base.GetComponent<Animation>().IsPlaying(anim_move) && (bool)GameObject.FindGameObjectWithTag("block"))
				{
					base.GetComponent<Animation>().Play(anim_move);
				}
			}
			else if (!base.GetComponent<Animation>().IsPlaying(anim_move))
			{
				base.GetComponent<Animation>().Play(anim_move);
			}
			if (!third_hand)
			{
				if (!GameObject.Find(buble))
				{
					animate = false;
				}
			}
			else if (!GameObject.FindGameObjectWithTag("block"))
			{
				animate = false;
			}
		}
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
