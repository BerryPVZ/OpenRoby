using System;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class MayaConstraint : MayaNode
{
	[Serializable]
	public class Target
	{
		[HideInInspector]
		public string targetName;

		public Transform target;

		public float weight = 1f;

		public Vector3 position
		{
			get
			{
				return target.position;
			}
		}

		public Quaternion rotation
		{
			get
			{
				return target.rotation;
			}
		}

		public QuaternionInterpolationTarget ToQuatInterpTarget(float normalizedWeight)
		{
			return new QuaternionInterpolationTarget
			{
				quaternion = target.rotation,
				weight = normalizedWeight
			};
		}
	}

	public Transform constrainedObject;

	public Target[] targets = new Target[1];

	protected float _oneOverTargetListLength;

	protected float[] _normalizedWeights;

	protected float _sumOfAllWeights;

	protected float _oneOverSumOfAllWeights;

	public Vector3 offset = Vector3.zero;

	private void Awake()
	{
		if (constrainedObject == null)
		{
			constrainedObject = base.transform;
		}
	}

	private void LateUpdate()
	{
		if (!isInvokedExternally)
		{
			Compute();
		}
	}

	public virtual void ProcessTargetList()
	{
		_sumOfAllWeights = 0f;
		for (int i = 0; i < targets.Length; i++)
		{
			_sumOfAllWeights += targets[i].weight;
		}
		if (Mathf.Approximately(_sumOfAllWeights, 0f))
		{
			_oneOverSumOfAllWeights = 0f;
		}
		else
		{
			_oneOverSumOfAllWeights = 1f / _sumOfAllWeights;
		}
		_normalizedWeights = new float[targets.Length];
		for (int j = 0; j < targets.Length; j++)
		{
			_normalizedWeights[j] = targets[j].weight * _oneOverSumOfAllWeights;
		}
		_oneOverTargetListLength = 1f / (float)targets.Length;
	}

	public void InsertTargetUsingWeightAttribute(string attributeName, float weight)
	{
		int num = int.Parse(new Regex("(?<=W)\\d+$").Match(attributeName).Value);
		if (targets == null)
		{
			targets = new Target[num + 1];
		}
		if (targets.Length <= num)
		{
			Target[] array = new Target[num + 1];
			for (int i = 0; i < targets.Length; i++)
			{
				array[i] = targets[i];
			}
			targets = array;
		}
		if (targets[num] == null)
		{
			targets[num] = new Target();
		}
		targets[num].weight = weight;
	}

	public void InsertTargetUsingTargetAttribute(string attributeName, string targetName)
	{
		int num = int.Parse(new Regex("(?<=target)\\d+").Match(attributeName).Value);
		if (targets == null)
		{
			targets = new Target[num + 1];
		}
		if (targets.Length <= num)
		{
			Target[] array = new Target[num + 1];
			for (int i = 0; i < targets.Length; i++)
			{
				array[i] = targets[i];
			}
			targets = array;
		}
		if (targets[num] == null)
		{
			targets[num] = new Target();
		}
		targets[num].targetName = targetName;
	}
}
