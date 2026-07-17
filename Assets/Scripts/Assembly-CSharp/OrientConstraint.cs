using UnityEngine;

[AddComponentMenu("Maya/Constraints/Orient Constraint")]
public class OrientConstraint : MayaConstraint
{
	public QuaternionInterpolationMode interpType = QuaternionInterpolationMode.Average;

	private QuaternionInterpolationTarget[] _targetRotations;

	private Quaternion _desiredRotation;

	private Quaternion[] _cache = new Quaternion[16];

	public override void Compute()
	{
		ProcessTargetList();
		StoreQuaternionTargets();
		_desiredRotation = QuaternionHelpers.Interpolate(_targetRotations, interpType, ref _cache, _oneOverTargetListLength);
		constrainedObject.rotation = _desiredRotation * Quaternion.Euler(offset);
	}

	private void StoreQuaternionTargets()
	{
		_targetRotations = new QuaternionInterpolationTarget[targets.Length];
		for (int i = 0; i < _targetRotations.Length; i++)
		{
			_targetRotations[i] = targets[i].ToQuatInterpTarget(_normalizedWeights[i]);
		}
	}
}
