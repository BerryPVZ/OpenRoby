using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class destroy_puff : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024destroy_0024101 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal destroy_puff _0024self__0024102;

			public _0024(destroy_puff self_)
			{
				_0024self__0024102 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(0.33f)) ? 1 : 0);
					break;
				case 2:
					UnityEngine.Object.Destroy(_0024self__0024102.gameObject);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal destroy_puff _0024self__0024103;

		public _0024destroy_0024101(destroy_puff self_)
		{
			_0024self__0024103 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024103);
		}
	}

	public virtual void Start()
	{
		StartCoroutine(destroy());
	}

	public virtual IEnumerator destroy()
	{
		return new _0024destroy_0024101(this).GetEnumerator();
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
