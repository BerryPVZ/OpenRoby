using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Manage_backgr_obj : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024move_away_002461 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal double _0024x_002462;

			internal double _0024_002437_002463;

			internal Vector3 _0024_002438_002464;

			internal double _0024_002439_002465;

			internal Vector3 _0024_002440_002466;

			internal int _0024_002441_002467;

			internal Vector3 _0024_002442_002468;

			internal Manage_backgr_obj _0024self__002469;

			public _0024(Manage_backgr_obj self_)
			{
				_0024self__002469 = self_;
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
				{
					_0024x_002462 = Screen.width * 300 / Screen.height;
					double num = (_0024_002437_002463 = 0.0 - _0024x_002462);
					Vector3 vector = (_0024_002438_002464 = GameObject.Find("pack_" + _0024self__002469.pack.ToString() + "_left").transform.position);
					float num2 = (_0024_002438_002464.x = (float)_0024_002437_002463);
					Vector3 vector2 = (GameObject.Find("pack_" + _0024self__002469.pack.ToString() + "_left").transform.position = _0024_002438_002464);
					double num3 = (_0024_002439_002465 = _0024x_002462);
					Vector3 vector4 = (_0024_002440_002466 = GameObject.Find("pack_" + _0024self__002469.pack.ToString() + "_right").transform.position);
					float num4 = (_0024_002440_002466.x = (float)_0024_002439_002465);
					Vector3 vector5 = (GameObject.Find("pack_" + _0024self__002469.pack.ToString() + "_right").transform.position = _0024_002440_002466);
					if (_0024self__002469.pack == 1)
					{
						int num5 = (_0024_002441_002467 = 300);
						Vector3 vector7 = (_0024_002442_002468 = GameObject.Find("lamps").transform.position);
						float num6 = (_0024_002442_002468.y = _0024_002441_002467);
						Vector3 vector8 = (GameObject.Find("lamps").transform.position = _0024_002442_002468);
					}
					result = (Yield(3, new WaitForSeconds(0.2f)) ? 1 : 0);
					break;
				}
				case 3:
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Manage_backgr_obj _0024self__002470;

		public _0024move_away_002461(Manage_backgr_obj self_)
		{
			_0024self__002470 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002470);
		}
	}

	public int pack;

	public virtual void Start()
	{
		StartCoroutine(move_away());
	}

	public virtual IEnumerator move_away()
	{
		return new _0024move_away_002461(this).GetEnumerator();
	}

	public virtual void Update()
	{
	}
}
