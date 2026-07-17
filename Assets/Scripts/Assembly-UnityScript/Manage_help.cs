using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Manage_help : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024run_bubbles_002498 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Manage_help _0024self__002499;

			public _0024(Manage_help self_)
			{
				_0024self__002499 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002499.hand = (Transform)UnityEngine.Object.Instantiate(_0024self__002499.bubble_hand3);
					goto case 2;
				case 2:
					if (!GameObject.Find("bubbles_hand_3(Clone)"))
					{
						result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
						break;
					}
					goto case 3;
				case 3:
					if ((bool)GameObject.Find("bubbles_hand_3(Clone)"))
					{
						result = (Yield(3, new WaitForSeconds(0.1f)) ? 1 : 0);
						break;
					}
					UnityEngine.Object.Destroy(_0024self__002499);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Manage_help _0024self__0024100;

		public _0024run_bubbles_002498(Manage_help self_)
		{
			_0024self__0024100 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024100);
		}
	}

	public Transform bubble1;

	public Transform bubble2;

	public Transform bubble3;

	public Transform bubble_hand2;

	public Transform bubble_hand3;

	public Transform cur_bubble;

	public Transform hand;

	public virtual void Start()
	{
		StartCoroutine(run_bubbles());
	}

	public virtual IEnumerator run_bubbles()
	{
		return new _0024run_bubbles_002498(this).GetEnumerator();
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
