using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
	public GraphicsSource gs;

	public bool run_on_start;

	private void Start()
	{
		if (run_on_start)
		{
			Go();
		}
	}

	public void Go()
	{
		if (!GameObject.Find("GraphicsSource"))
		{
			return;
		}
		gs = GameObject.Find("GraphicsSource").GetComponent<GraphicsSource>();
		if (gs.GraphicsHD())
		{
			if (base.gameObject.name.Equals("Scene"))
			{
				MonoBehaviour.print("here");
			}
			Seek(base.gameObject.transform);
		}
	}

	public void Seek(Transform transf)
	{
		if ((bool)transf.GetComponent<Renderer>() && (bool)transf.GetComponent<Renderer>().material)
		{
			ChangeMaterial(transf);
		}
		foreach (Transform item in transf)
		{
			if (base.gameObject.name.Equals("pack_1_level_3(Clone)"))
			{
				MonoBehaviour.print(item.name);
			}
			if ((bool)item.GetComponent<Renderer>() && (bool)item.GetComponent<Renderer>().material)
			{
				ChangeMaterial(item);
			}
			if (item.childCount >= 1)
			{
				Seek(item);
			}
		}
	}

	private void ChangeMaterial(Transform t)
	{
		for (int i = 0; i < gs.dif_materials; i++)
		{
			Material material = gs.materials[0][i];
			Material material2 = t.GetComponent<Renderer>().material;
			string text = material.name.ToString() + " (Instance)";
			if (text.Equals(material2.name.ToString()))
			{
				t.GetComponent<Renderer>().material = gs.materials[1][i];
				if (base.gameObject.name.Equals("pack_1_level_3(Clone)"))
				{
					MonoBehaviour.print("Changed at " + t.name);
				}
			}
		}
	}
}
