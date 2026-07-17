using UnityEngine;

[AddComponentMenu("Maya/Constraints/Aim Constraint")]
public class AimConstraint : MayaConstraint
{
	public enum WorldUpType
	{
		SceneUp = 0,
		ObjectUp = 1,
		ObjectRotationUp = 2,
		Vector = 3,
		None = 4
	}

	public Vector3 aimVector = Vector3.forward;

	public Vector3 upVector = Vector3.up;

	public Transform worldUpObject;

	[HideInInspector]
	public string worldUpObjectName;

	public WorldUpType worldUpType;

	public Vector3 worldUpVector = Vector3.up;

	private Vector3 _desiredAimVector;

	private Vector3 _desiredUpVector;

	private Vector3 _constrainedObjectPosition;

	public override void ProcessTargetList()
	{
		_oneOverTargetListLength = 1f / (float)targets.Length;
	}

	public override void Compute()
	{
		ProcessTargetList();
		switch (worldUpType)
		{
		case WorldUpType.SceneUp:
			_desiredUpVector = Vector3.up;
			break;
		case WorldUpType.ObjectUp:
			_desiredUpVector = worldUpObject.position - constrainedObject.position;
			break;
		case WorldUpType.ObjectRotationUp:
			_desiredUpVector = worldUpObject.TransformDirection(worldUpVector);
			break;
		case WorldUpType.Vector:
			_desiredUpVector = worldUpVector;
			break;
		case WorldUpType.None:
			_desiredUpVector = Vector3.up;
			break;
		}
		_desiredAimVector = Vector3.zero;
		_constrainedObjectPosition = constrainedObject.position;
		for (int i = 0; i < targets.Length; i++)
		{
			_desiredAimVector += targets[i].weight * (targets[i].position - _constrainedObjectPosition);
		}
		_desiredAimVector *= _oneOverTargetListLength;
		constrainedObject.rotation = QuaternionHelpers.CustomLookRotation(_desiredAimVector, _desiredUpVector, aimVector, upVector) * Quaternion.Euler(offset);
	}
}
