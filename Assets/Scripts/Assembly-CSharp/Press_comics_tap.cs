using System.Collections;
using UnityEngine;

public class Press_comics_tap : Press_abstract
{
	public float t = 0.3f;

	private LevelLoader l;

	private void Start()
	{
		GameObject skip = GameObject.Find("Skip_comics");
		if (skip != null)
		{
			Object.Destroy(skip);
		}

		GameObject cameraObject = GameObject.Find("Camera");
		if (cameraObject != null)
		{
			l = cameraObject.GetComponent<LevelLoader>();
		}

		GameObject comics = FindTaggedObject("comics");
		if (comics != null)
		{
			transform.SetParent(comics.transform, true);
		}
	}

	public override void action()
	{
		PreloaderManager.PutOnScreen();
		StartCoroutine(FinishComicsAndLoadLevel());

		GameObject alert = GameObject.Find("alert");
		if (alert != null)
		{
			Object.Destroy(alert);
		}
	}

	private IEnumerator FinishComicsAndLoadLevel()
	{
		yield return new WaitForSeconds(t);

		DestroyOtherComicRoots();
		RestoreGameplayBackgroundDepth();
		DestroyMenu();

		// The level can be created immediately. Comic objects are removed at the
		// end of this frame, while the preloader keeps the transition covered.
		LevelLoader.LoadLevel();
		FlurryAndroidiOSManager.EventSimple("Watched comics " + PlayerPreperencesManager.GetPack());
		Object.Destroy(gameObject);
	}

	private void DestroyOtherComicRoots()
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
			if (comic != null && comic != gameObject)
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

		// Comics.cs only moves the background sideways to X = 2000. The old
		// exit code rebuilt the whole vector as (0, 0, 250), overwriting the
		// prefab's real Z depth and allowing the background to cover the level.
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
