using UnityEngine;

[AddComponentMenu("Maya/Constraints/Hip Constraint")]
public class HipConstraint : MayaNode
{
	[HideInInspector]
	[SerializeField]
	public Transform constrainedObject;

	[SerializeField]
	private Vector3 _hipAimAxis = Vector3.forward;

	[SerializeField]
	private Vector3 _hipFrontAxis = Vector3.up;

	public Transform hipObject;

	[HideInInspector]
	[SerializeField]
	public string hipObjectName;

	[SerializeField]
	private Vector3 _pelvisAimAxis = Vector3.up;

	[SerializeField]
	private Vector3 _pelvisFrontAxis = Vector3.forward;

	public Transform pelvisObject;

	[HideInInspector]
	[SerializeField]
	public string pelvisObjectName;

	public OrientConstraint[] twistJoints = new OrientConstraint[0];

	private Vector3 _hipRightAxis;

	private Vector3 _pelvisRightAxis;

	private Vector3 _hipAimRotated;

	private Vector3 _pelvisAimRotated;

	private Vector3 _targetUpVector;

	private Quaternion _fromNormalRotation;

	public Vector3 hipAimAxis
	{
		get
		{
			return _hipAimAxis;
		}
		set
		{
			_hipAimAxis = value.normalized;
			OrthonormalizeHipAxes();
			ComputeFromNormalRotation();
		}
	}

	public Vector3 hipFrontAxis
	{
		get
		{
			return _hipFrontAxis;
		}
		set
		{
			_hipFrontAxis = value.normalized;
			OrthonormalizeHipAxes();
			ComputeFromNormalRotation();
		}
	}

	public Vector3 pelvisAimAxis
	{
		get
		{
			return _pelvisAimAxis;
		}
		set
		{
			_pelvisAimAxis = value.normalized;
			OrthonormalizePelvisAxes();
		}
	}

	public Vector3 pelvisFrontAxis
	{
		get
		{
			return _pelvisFrontAxis;
		}
		set
		{
			_pelvisFrontAxis = value.normalized;
			OrthonormalizePelvisAxes();
		}
	}

	public float elevationAngle
	{
		get
		{
			return 180f - Vector3.Angle(_pelvisAimRotated, _hipAimRotated);
		}
	}

	public float elevationDot
	{
		get
		{
			return Vector3.Dot(_pelvisAimRotated, _hipAimRotated);
		}
	}

	private void Awake()
	{
		if (constrainedObject == null)
		{
			constrainedObject = base.transform;
		}
	}

	private void Start()
	{
		OrthonormalizeHipAxes();
		OrthonormalizePelvisAxes();
		ComputeFromNormalRotation();
	}

	private void LateUpdate()
	{
		if (!isInvokedExternally)
		{
			Compute();
		}
	}

	public override void Compute()
	{
		_hipAimRotated = hipObject.rotation * hipAimAxis;
		_pelvisAimRotated = pelvisObject.rotation * pelvisAimAxis;
		float num = elevationDot;
		float f = Vector3.Dot(pelvisObject.rotation * pelvisFrontAxis, (_hipAimRotated - pelvisObject.rotation * pelvisAimAxis * num).normalized);
		float num2 = (0f - (1f + elevationDot)) * Mathf.Abs(f);
		_targetUpVector = Quaternion.AngleAxis(Mathf.Sign(f) * num2 * 90f, _pelvisRightAxis) * pelvisFrontAxis;
		constrainedObject.rotation = Quaternion.LookRotation(_hipAimRotated, pelvisObject.rotation * _targetUpVector) * _fromNormalRotation;
		OrientConstraint[] array = twistJoints;
		foreach (OrientConstraint orientConstraint in array)
		{
			orientConstraint.Compute();
		}
	}

	private void ComputeFromNormalRotation()
	{
		_fromNormalRotation = Quaternion.Inverse(Quaternion.LookRotation(hipAimAxis, hipFrontAxis));
	}

	private void OrthonormalizeHipAxes()
	{
		Vector3.OrthoNormalize(ref _hipAimAxis, ref _hipFrontAxis, ref _hipRightAxis);
	}

	private void OrthonormalizePelvisAxes()
	{
		Vector3.OrthoNormalize(ref _pelvisAimAxis, ref _pelvisFrontAxis, ref _pelvisRightAxis);
	}

	public void AppendTwistJoint(OrientConstraint twistJoint)
	{
		OrientConstraint[] array = new OrientConstraint[twistJoints.Length + 1];
		for (int i = 0; i < twistJoints.Length; i++)
		{
			array[i] = twistJoints[i];
		}
		array[array.Length - 1] = twistJoint;
		twistJoints = array;
		OrientConstraint[] array2 = twistJoints;
		foreach (OrientConstraint orientConstraint in array2)
		{
			orientConstraint.isInvokedExternally = true;
		}
	}
}
