using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Preload : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024preloader_002477 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Preload _0024self__002478;

			public _0024(Preload self_)
			{
				_0024self__002478 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				case 2:
					if ((bool)GameObject.FindGameObjectWithTag("menu"))
					{
						UnityEngine.Object.Destroy(_0024self__002478.gameObject);
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Preload _0024self__002479;

		public _0024preloader_002477(Preload self_)
		{
			_0024self__002479 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002479);
		}
	}

	public double time;

	public Preload()
	{
		time = 1.0;
	}

	public virtual void Start()
	{
		StartCoroutine(preloader());
	}

	public virtual void Update()
	{
	}

	public virtual IEnumerator preloader()
	{
		return new _0024preloader_002477(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
