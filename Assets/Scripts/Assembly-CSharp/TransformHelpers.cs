using UnityEngine;

public static class TransformHelpers
{
	public static Transform GetTransformInHierarchy(Transform root, string name)
	{
		return GetTransformInHierarchy(root.GetComponentsInChildren<Transform>(), name);
	}

	public static Transform GetTransformInHierarchy(Component[] hierarchy, string name)
	{
		for (int i = 0; i < hierarchy.Length; i++)
		{
			Transform transform = (Transform)hierarchy[i];
			if (transform.name == name)
			{
				return transform;
			}
			if (transform.name.Contains(":") && transform.name.Substring(transform.name.LastIndexOf(":") + 1) == name)
			{
				return transform;
			}
		}
		return null;
	}
}
