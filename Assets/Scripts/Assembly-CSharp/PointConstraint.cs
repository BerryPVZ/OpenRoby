using UnityEngine;

[AddComponentMenu("Maya/Constraints/Point Constraint")]
public class PointConstraint : MayaConstraint
{
	public float constraintOffsetPolarity = 1f;

	private Vector3 _desiredPosition;

	public override void Compute()
	{
		ProcessTargetList();
		_desiredPosition = Vector3.zero;
		for (int i = 0; i < targets.Length; i++)
		{
			_desiredPosition += _normalizedWeights[i] * targets[i].position;
		}
		constrainedObject.position = _desiredPosition + constrainedObject.TransformDirection(offset);
	}
}
