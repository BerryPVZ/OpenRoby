using UnityEngine;

public abstract class MayaExpressions : MayaNode
{
	public float importScale = 1f;

	public float oneOverImportScale = 1f;

	public MayaNode[] upstreamDependencies = new MayaNode[0];

	protected Vector3 localPosition;

	protected Vector3 localEulerAngles;

	protected Vector3 localScale;

	private void LateUpdate()
	{
		if (!isInvokedExternally)
		{
			Compute();
		}
	}

	public override void Compute()
	{
		MayaNode[] array = upstreamDependencies;
		foreach (MayaNode mayaNode in array)
		{
			mayaNode.Compute();
		}
	}
}
