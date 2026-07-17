using System;
using UnityEngine;

[Serializable]
public class xvatalka_destroy_boxes : MonoBehaviour
{
	public Transform puff_box;

	public Transform puff_box_block;

	public Transform puff_balk;

	public Transform puff_explode;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag.Equals("box"))
		{
			Transform transform = (Transform)UnityEngine.Object.Instantiate(puff_box, collision.collider.transform.position, Quaternion.identity);
			transform.rotation = collision.collider.transform.rotation;
			transform.GetComponent<Animation>().Play("puff_box_small");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
		else if (collision.collider.name.Equals("box_metal"))
		{
			Transform transform2 = (Transform)UnityEngine.Object.Instantiate(puff_box, collision.collider.transform.position, Quaternion.identity);
			transform2.rotation = collision.collider.transform.rotation;
			transform2.GetComponent<Animation>().Play("puff_box_small");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
		else if (collision.collider.tag.Equals("block"))
		{
			Transform transform3 = (Transform)UnityEngine.Object.Instantiate(puff_box_block, collision.collider.transform.position, Quaternion.identity);
			transform3.rotation = collision.collider.transform.rotation;
			transform3.GetComponent<Animation>().Play("puff_box_big");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
		else if (collision.collider.tag.Equals("balk"))
		{
			Transform transform4 = (Transform)UnityEngine.Object.Instantiate(puff_balk, collision.collider.transform.position, Quaternion.identity);
			transform4.rotation = collision.collider.transform.rotation;
			transform4.GetComponent<Animation>().Play("puff_balk_1");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
		else if (collision.collider.name.Equals("box_explode"))
		{
			Transform transform5 = (Transform)UnityEngine.Object.Instantiate(puff_explode, collision.collider.transform.position, Quaternion.identity);
			transform5.rotation = collision.collider.transform.rotation;
			transform5.GetComponent<Animation>().Play("puff_bomb_box");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
		else if (collision.collider.name.Length > 10 && collision.collider.name.Substring(0, 10).Equals("balk_metal"))
		{
			Transform transform6 = (Transform)UnityEngine.Object.Instantiate(puff_balk, collision.collider.transform.position, Quaternion.identity);
			transform6.rotation = collision.collider.transform.rotation;
			transform6.GetComponent<Animation>().Play("puff_balk_1");
			UnityEngine.Object.Destroy(collision.gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
