using System.Collections;
using UnityEngine;

public class Tap_skip_comics : Press_abstract
{
	private float t = 0.2f;
	private LevelLoader l;
	private int n;
	private float[] taps;
	private bool clicked;

	private IEnumerator Start()
	{
		n = 0;
		taps = new float[2];
		yield return new WaitForSeconds(t);
		StartCoroutine(CheckForComics());

		GameObject cameraObject = GameObject.Find("Camera");
		if (cameraObject != null)
		{
			l = cameraObject.GetComponent<LevelLoader>();
		}

		if (FindTaggedObject("comics") == null)
		{
			GameObject skip = GameObject.Find("P_Skip_comics(Clone)");
			if (skip != null)
			{
				Object.Destroy(skip);
			}
		}

		Vector3 position = transform.position;
		position.x = 0f;
		transform.position = position;

		GameObject tapToSkip = GameObject.Find("TapToSkip");
		if (tapToSkip != null)
		{
			position = tapToSkip.transform.position;
			position.x = -(Screen.width * 300f / Screen.height);
			position.y = 260f;
			tapToSkip.transform.position = position;
		}

		DestroyMenu();
	}

	private IEnumerator CheckForComics()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			if (FindTaggedObject("comics") == null)
			{
				GameObject skip = GameObject.Find("P_Skip_comics(Clone)");
				if (skip != null)
				{
					Object.Destroy(skip);
				}
				yield break;
			}
		}
	}

	public override void action()
	{
		if (clicked)
		{
			return;
		}

		n++;
		taps[0] = taps[1];
		taps[1] = Time.time;
		if (taps[1] - taps[0] >= 1f || n <= 1)
		{
			return;
		}

		clicked = true;
		PreloaderManager.PutOnScreen();
		StartCoroutine(FinishComicsAndLoadLevel());

		GameObject skipImage = GameObject.Find("TapToSkip_image");
		Renderer renderer = skipImage != null ? skipImage.GetComponent<Renderer>() : null;
		if (renderer != null)
		{
			renderer.enabled = false;
		}

		GameObject alert = GameObject.Find("alert");
		if (alert != null)
		{
			Object.Destroy(alert);
		}
	}

	private IEnumerator FinishComicsAndLoadLevel()
	{
		yield return new WaitForSeconds(t);

		DestroyComicRoots();
		RestoreGameplayBackgroundDepth();
		DestroyMenu();
		LevelLoader.LoadLevel();

		GameObject parentObject = transform.parent != null ? transform.parent.gameObject : null;
		if (parentObject != null)
		{
			Object.Destroy(parentObject);
		}
		else
		{
			Object.Destroy(gameObject);
		}
	}

	private static void DestroyComicRoots()
	{
		GameObject[] comics;
		try
		{
			comics = GameObject.FindGameObjectsWithTag("comics");
		}
		catch (UnityException)
		{
			return;
		}

		foreach (GameObject comic in comics)
		{
			if (comic != null)
			{
				Object.Destroy(comic);
			}
		}
	}

	private static void RestoreGameplayBackgroundDepth()
	{
		GameObject background = FindTaggedObject("backgr_obj");
		if (background == null)
		{
			return;
		}

		// Preserve the background prefab's original Y and Z values. Hard-coding
		// Z = 250 after a comic can put the full-screen background over the level.
		Vector3 position = background.transform.position;
		position.x = 0f;
		background.transform.position = position;
	}

	private static void DestroyMenu()
	{
		GameObject menu = FindTaggedObject("menu");
		if (menu != null)
		{
			Object.Destroy(menu);
		}
	}

	private static GameObject FindTaggedObject(string tagName)
	{
		try
		{
			return GameObject.FindGameObjectWithTag(tagName);
		}
		catch (UnityException)
		{
			return null;
		}
	}
}
