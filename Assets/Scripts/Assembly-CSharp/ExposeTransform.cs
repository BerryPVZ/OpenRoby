using UnityEngine;

[AddComponentMenu("Maya/Utilities/Expose Transform")]
public class ExposeTransform : MayaNode
{
	[SerializeField]
	[HideInInspector]
	public Transform t;

	[HideInInspector]
	[SerializeField]
	public string referenceName;

	public Transform reference;

	public EulerRotationOrder rotateOrder;

	public Vector3 objectAxis = Vector3.forward;

	public Vector3 referenceAxis = Vector3.forward;

	public bool normalizeAxes = true;

	public Vector3 position
	{
		get
		{
			if (reference == null)
			{
				return t.position;
			}
			return reference.InverseTransformPoint(t.position);
		}
	}

	public float distance
	{
		get
		{
			if (reference == null)
			{
				return t.position.magnitude;
			}
			return Vector3.Magnitude(reference.InverseTransformPoint(t.position));
		}
	}

	public Vector3 rotation
	{
		get
		{
			if (reference == null)
			{
				return QuaternionHelpers.ToEulerAngles(t.rotation, rotateOrder);
			}
			return QuaternionHelpers.ToEulerAngles(Quaternion.Inverse(reference.rotation) * t.rotation, rotateOrder);
		}
	}

	public float dot
	{
		get
		{
			if (reference == null)
			{
				if (normalizeAxes)
				{
					return Vector3.Dot(referenceAxis.normalized, t.TransformDirection(objectAxis));
				}
				return Vector3.Dot(referenceAxis, t.TransformPoint(objectAxis));
			}
			if (normalizeAxes)
			{
				return Vector3.Dot(reference.TransformDirection(referenceAxis), t.TransformDirection(objectAxis));
			}
			return Vector3.Dot(reference.TransformPoint(referenceAxis), t.TransformPoint(objectAxis));
		}
	}

	public float angle
	{
		get
		{
			if (reference == null)
			{
				return Vector3.Angle(referenceAxis, t.TransformDirection(objectAxis));
			}
			return Vector3.Angle(reference.TransformDirection(referenceAxis), t.TransformDirection(objectAxis));
		}
	}

	public float dotToTarget
	{
		get
		{
			if (reference == null)
			{
				if (normalizeAxes)
				{
					return Vector3.Dot(-t.position.normalized, t.TransformDirection(objectAxis));
				}
				return Vector3.Dot(-t.position, t.TransformPoint(objectAxis));
			}
			if (normalizeAxes)
			{
				return Vector3.Dot((reference.position - t.position).normalized, t.TransformDirection(objectAxis));
			}
			return Vector3.Dot(reference.position - t.position, t.TransformPoint(objectAxis));
		}
	}

	public float angleToTarget
	{
		get
		{
			if (reference == null)
			{
				return Vector3.Angle(-t.position, t.TransformDirection(objectAxis));
			}
			return Vector3.Angle(reference.position - t.position, t.TransformDirection(objectAxis));
		}
	}

	private void Awake()
	{
		if (t == null)
		{
			t = base.transform;
		}
	}
}
