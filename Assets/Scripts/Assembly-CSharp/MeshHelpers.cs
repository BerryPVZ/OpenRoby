using UnityEngine;

public static class MeshHelpers
{
	public static Mesh DuplicateMesh(Mesh mesh)
	{
		return DuplicateMesh(mesh, string.Format("{0} (Duplicate)", mesh.name));
	}

	public static Mesh DuplicateMesh(Mesh mesh, string newName)
	{
		Mesh mesh2 = new Mesh();
		mesh2.vertices = mesh.vertices;
		mesh2.colors = mesh.colors;
		mesh2.normals = mesh.normals;
		mesh2.subMeshCount = mesh.subMeshCount;
		for (int i = 0; i < mesh.subMeshCount; i++)
		{
			mesh2.SetTriangles(mesh.GetTriangles(i), i);
		}
		mesh2.tangents = mesh.tangents;
		mesh2.uv = mesh.uv;
		mesh2.uv2 = mesh.uv2;
		mesh2.bindposes = mesh.bindposes;
		mesh2.boneWeights = mesh.boneWeights;
		mesh2.name = newName;
		return mesh2;
	}

	public static void FloodVertexColors(MeshFilter meshFilter, Color color)
	{
		Mesh mesh = meshFilter.mesh;
		FloodVertexColors(mesh, color);
		meshFilter.mesh = mesh;
	}

	public static void FloodVertexColors(SkinnedMeshRenderer skin, Color color)
	{
		Mesh sharedMesh = skin.sharedMesh;
		FloodVertexColors(sharedMesh, color);
		skin.sharedMesh = sharedMesh;
	}

	public static void FloodVertexColors(Mesh mesh, Color color)
	{
		Color[] array = new Color[mesh.vertices.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		mesh.colors = array;
	}

	public static Color[] GetTintedColors(Mesh mesh, Color tint)
	{
		Color[] colors = mesh.colors;
		return TintColors(colors, tint);
	}

	public static Color[] TintColors(Color[] col, Color tint)
	{
		Color[] array = new Color[col.Length];
		for (int i = 0; i < col.Length; i++)
		{
			array[i] = col[i] * tint;
		}
		return array;
	}

	public static Mesh CreateMeshFromParameters(MeshParameters parameters)
	{
		Mesh mesh = new Mesh();
		mesh.vertices = parameters.vertices;
		if (parameters.bindPoses != null && parameters.bindPoses.Length > 0)
		{
			mesh.bindposes = parameters.bindPoses;
		}
		if (parameters.boneWeights != null && parameters.boneWeights.Length > 0)
		{
			mesh.boneWeights = parameters.boneWeights;
		}
		if (parameters.colors != null && parameters.colors.Length > 0)
		{
			mesh.colors = parameters.colors;
		}
		mesh.normals = parameters.normals;
		if (parameters.tangents != null && parameters.tangents.Length > 0)
		{
			mesh.tangents = parameters.tangents;
		}
		mesh.triangles = parameters.triangles;
		mesh.uv = parameters.uv;
		if (parameters.uv2 != null && parameters.uv2.Length > 0)
		{
			mesh.uv2 = parameters.uv2;
		}
		return mesh;
	}

	public static Mesh CreateMirrorMesh(Mesh inMesh)
	{
		Mesh mesh = DuplicateMesh(inMesh, string.Format("{0} (Mirrored)", inMesh.name));
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		for (int i = 0; i < mesh.vertices.Length; i++)
		{
			vertices[i] = mesh.vertices[i];
			vertices[i].x *= -1f;
			normals[i] = mesh.normals[i];
			normals[i].x *= -1f;
		}
		int[] triangles = mesh.triangles;
		for (int j = 0; j < triangles.Length - 2; j += 3)
		{
			int num = triangles[j];
			triangles[j] = triangles[j + 2];
			triangles[j + 2] = num;
		}
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.triangles = triangles;
		return mesh;
	}
}
