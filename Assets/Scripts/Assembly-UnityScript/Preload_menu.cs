using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Preload_menu : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024preloader_002480 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Preload_menu _0024self__002481;

			public _0024(Preload_menu self_)
			{
				_0024self__002481 = self_;
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
					if ((bool)GameObject.FindGameObjectWithTag("level") && (bool)GameObject.FindGameObjectWithTag("backgr_obj"))
					{
						UnityEngine.Object.Destroy(_0024self__002481.gameObject);
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Preload_menu _0024self__002482;

		public _0024preloader_002480(Preload_menu self_)
		{
			_0024self__002482 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002482);
		}
	}

	public double time;

	public Preload_menu()
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
		return new _0024preloader_002480(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
