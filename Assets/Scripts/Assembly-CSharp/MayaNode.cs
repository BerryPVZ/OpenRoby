using UnityEngine;

public abstract class MayaNode : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	public string nodeName;

	public bool isInvokedExternally;

	public virtual void Compute()
	{
	}
}
