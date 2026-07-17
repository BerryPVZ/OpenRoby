using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[AddComponentMenu("Maya/Deformers/BlendShape")]
public class BlendShape : MayaNode
{
	[Serializable]
	public class Target
	{
		public string name;

		public float weight;

		private float _prevWeight;

		public int[] vertices = new int[0];

		public Vector3[] deltaPositions = new Vector3[0];

		public float previousWeight
		{
			get
			{
				return _prevWeight;
			}
		}

		public void RecordWeightForNextFrame()
		{
			_prevWeight = weight;
		}
	}

	public Mesh seamlessBaseMesh;

	public Renderer meshRenderer;

	[SerializeField]
	[HideInInspector]
	private Mesh _outMesh;

	public int[] indexMap;

	public Target[] targets = new Target[0];

	public bool isSmoothingNormals = true;

	private Vector3[] _defaultNormals;

	private Vector3[] _seamlessNormals;

	private Vector3[] _seamlessVertices;

	private Vector3[] _outNormals;

	private Vector3[] _outVertices;

	private Hashtable _targetsByName = new Hashtable();

	public Mesh outMesh
	{
		get
		{
			return _outMesh;
		}
	}

	public string[] targetNames
	{
		get
		{
			string[] array = new string[targets.Length];
			for (int i = 0; i < targets.Length; i++)
			{
				array[i] = targets[i].name;
			}
			return array;
		}
	}

	public Target this[string s]
	{
		get
		{
			return _targetsByName[s] as Target;
		}
		set
		{
			_targetsByName[s] = value;
		}
	}

	public void Awake()
	{
		Target[] array = targets;
		foreach (Target target in array)
		{
			_targetsByName.Add(target.name, target);
		}
		bool flag = base.GetComponent<Renderer>().GetType() == typeof(SkinnedMeshRenderer);
		if (_outMesh == null)
		{
			if (flag)
			{
				_outMesh = (meshRenderer as SkinnedMeshRenderer).sharedMesh;
			}
			else
			{
				_outMesh = GetComponent<MeshFilter>().mesh;
			}
		}
		_outMesh = MeshHelpers.DuplicateMesh(_outMesh, string.Format("{0} (Duplicate)", _outMesh.name));
		if (flag)
		{
			(meshRenderer as SkinnedMeshRenderer).sharedMesh = _outMesh;
		}
		else
		{
			GetComponent<MeshFilter>().mesh = _outMesh;
		}
		seamlessBaseMesh = MeshHelpers.DuplicateMesh(seamlessBaseMesh, string.Format("{0} (Duplicate)", seamlessBaseMesh.name));
		_defaultNormals = _outMesh.normals;
		_seamlessNormals = seamlessBaseMesh.normals;
		_seamlessVertices = seamlessBaseMesh.vertices;
		_outNormals = _outMesh.normals;
		_outVertices = _outMesh.vertices;
	}

	private void Update()
	{
		if (!isInvokedExternally)
		{
			Compute();
		}
	}

	public override void Compute()
	{
		_seamlessVertices = seamlessBaseMesh.vertices;
		for (int i = 0; i < targets.Length; i++)
		{
			float num = targets[i].weight - targets[i].previousWeight;
			for (int j = 0; j < targets[i].vertices.Length; j++)
			{
				_seamlessVertices[targets[i].vertices[j]] += targets[i].deltaPositions[j] * num;
			}
			targets[i].RecordWeightForNextFrame();
		}
		seamlessBaseMesh.vertices = _seamlessVertices;
		if (isSmoothingNormals)
		{
			seamlessBaseMesh.RecalculateNormals();
			_seamlessNormals = seamlessBaseMesh.normals;
			for (int k = 0; k < indexMap.Length; k++)
			{
				_outVertices[k] = _seamlessVertices[indexMap[k]];
				_outNormals[k] = _seamlessNormals[indexMap[k]];
			}
		}
		else
		{
			for (int l = 0; l < indexMap.Length; l++)
			{
				_outVertices[l] = _seamlessVertices[indexMap[l]];
			}
			_defaultNormals.CopyTo(_outNormals, 0);
		}
		_outMesh.vertices = _outVertices;
		_outMesh.normals = _outNormals;
	}

	public void RecalculateBounds()
	{
		_outMesh.RecalculateBounds();
	}

	public void OverrideOutMesh(Mesh mesh)
	{
		_outMesh = mesh;
		_outNormals = mesh.normals;
		_outVertices = mesh.vertices;
	}
}
