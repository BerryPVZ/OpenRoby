using UnityEngine;

[AddComponentMenu("Maya/Constraints/Shoulder Constraint")]
public class ShoulderConstraint : MayaNode
{
	[SerializeField]
	[HideInInspector]
	public Transform constrainedObject;

	[SerializeField]
	private Vector3 _shoulderAimAxis = Vector3.forward;

	[SerializeField]
	private Vector3 _shoulderFrontAxis = Vector3.right;

	public Transform shoulderObject;

	[HideInInspector]
	[SerializeField]
	public string shoulderObjectName;

	[SerializeField]
	private Vector3 _spineAimAxis = Vector3.forward;

	[SerializeField]
	private Vector3 _spineFrontAxis = Vector3.up;

	public Transform spineObject;

	[SerializeField]
	[HideInInspector]
	public string spineObjectName;

	public float raisedAngleOffset = 45f;

	public OrientConstraint[] twistJoints = new OrientConstraint[0];

	private Vector3 _shoulderUpAxis;

	private Vector3 _spineRightAxis;

	private Vector3 _shoulderAimRotated;

	private Vector3 _spineAimRotated;

	private Vector3 _targetUpVector;

	private Vector3 _targetUpRaised;

	private Vector3 _targetUpRest;

	private Vector3 _targetUpLowered;

	private float _bodySideScalar;

	private Quaternion _fromNormalRotation;

	public Vector3 shoulderAimAxis
	{
		get
		{
			return _shoulderAimAxis;
		}
		set
		{
			_shoulderAimAxis = value.normalized;
			OrthonormalizeShoulderAxes();
			ComputeFromNormalRotation();
		}
	}

	public Vector3 shoulderFrontAxis
	{
		get
		{
			return _shoulderFrontAxis;
		}
		set
		{
			_shoulderFrontAxis = value.normalized;
			OrthonormalizeShoulderAxes();
			ComputeFromNormalRotation();
		}
	}

	public Vector3 spineAimAxis
	{
		get
		{
			return _spineAimAxis;
		}
		set
		{
			_spineAimAxis = value.normalized;
			OrthonormalizeSpineAxes();
			DetermineBodySide();
			ComputeTargetVectors();
		}
	}

	public Vector3 spineFrontAxis
	{
		get
		{
			return _spineFrontAxis;
		}
		set
		{
			_spineFrontAxis = value.normalized;
			OrthonormalizeSpineAxes();
			DetermineBodySide();
			ComputeTargetVectors();
		}
	}

	public float elevationAngle
	{
		get
		{
			return 180f - Vector3.Angle(_spineAimRotated, _shoulderAimRotated);
		}
	}

	public float elevationDot
	{
		get
		{
			return Vector3.Dot(_spineAimRotated, _shoulderAimRotated);
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
		OrthonormalizeShoulderAxes();
		OrthonormalizeSpineAxes();
		ComputeFromNormalRotation();
		DetermineBodySide();
		ComputeTargetVectors();
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
		_shoulderAimRotated = shoulderObject.rotation * shoulderAimAxis;
		_spineAimRotated = spineObject.rotation * spineAimAxis;
		float num = elevationDot;
		if (num < 0f)
		{
			_targetUpVector = Vector3.Lerp(_targetUpRest, _targetUpLowered, 0f - num);
		}
		else
		{
			_targetUpVector = Vector3.Lerp(_targetUpRest, _targetUpRaised, num);
		}
		constrainedObject.rotation = Quaternion.LookRotation(_shoulderAimRotated, spineObject.rotation * _targetUpVector) * _fromNormalRotation;
		OrientConstraint[] array = twistJoints;
		foreach (OrientConstraint orientConstraint in array)
		{
			orientConstraint.Compute();
		}
	}

	private void DetermineBodySide()
	{
		if (!(shoulderObject == null) && !(spineObject == null))
		{
			_bodySideScalar = Mathf.Sign(Vector3.Dot(shoulderObject.position - spineObject.position, spineObject.rotation * _spineRightAxis));
		}
	}

	private void ComputeFromNormalRotation()
	{
		_fromNormalRotation = Quaternion.Inverse(Quaternion.LookRotation(shoulderAimAxis, _shoulderUpAxis));
	}

	private void OrthonormalizeShoulderAxes()
	{
		_shoulderUpAxis = Vector3.Cross(shoulderAimAxis, shoulderFrontAxis);
	}

	private void OrthonormalizeSpineAxes()
	{
		_spineRightAxis = Vector3.Cross(spineFrontAxis, spineAimAxis);
	}

	private void ComputeTargetVectors()
	{
		_targetUpRest = spineAimAxis * _bodySideScalar;
		_targetUpLowered = -Vector3.Cross(spineAimAxis, spineFrontAxis);
		_targetUpRaised = Quaternion.AngleAxis(raisedAngleOffset, spineAimAxis * _bodySideScalar) * -_spineRightAxis;
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
