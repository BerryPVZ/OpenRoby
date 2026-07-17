using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Manage_scene_objs : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024move_away_002471 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Manage_scene_objs _0024self__002472;

			public _0024(Manage_scene_objs self_)
			{
				_0024self__002472 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1.3f)) ? 1 : 0);
					break;
				case 2:
					_0024self__002472.transform.position = new Vector3(0f, -2000f, 0f);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Manage_scene_objs _0024self__002473;

		public _0024move_away_002471(Manage_scene_objs self_)
		{
			_0024self__002473 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002473);
		}
	}

	public virtual void Start()
	{
		double num = Screen.width * 300 / Screen.height;
		if (8u != 0)
		{
		}
		GameObject.Find("wall_up").transform.localScale = new Vector3((float)(2.0 * num), 1f, 200f);
		double num2 = 0.0 - num;
		Vector3 position = GameObject.Find("wall_1").transform.position;
		float num3 = (position.x = (float)num2);
		Vector3 vector = (GameObject.Find("wall_1").transform.position = position);
		double num4 = num;
		Vector3 position2 = GameObject.Find("wall_2").transform.position;
		float num5 = (position2.x = (float)num4);
		Vector3 vector3 = (GameObject.Find("wall_2").transform.position = position2);
		StartCoroutine(move_away());
	}

	public virtual IEnumerator move_away()
	{
		return new _0024move_away_002471(this).GetEnumerator();
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
