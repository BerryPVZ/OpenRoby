using UnityEngine;

public class MeshesManager : MonoBehaviour
{
	public Mesh[] meshes;

	public Mesh GetMeshes(int n)
	{
		if (meshes[n] != null)
		{
			return meshes[n];
		}
		MonoBehaviour.print("Error adnaka!!!!");
		return new Mesh();
	}
}
