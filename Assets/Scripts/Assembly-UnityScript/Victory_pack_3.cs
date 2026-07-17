using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Victory_pack_3 : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024run_emo_002483 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Victory_pack_3 _0024self__002484;

			public _0024(Victory_pack_3 self_)
			{
				_0024self__002484 = self_;
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
					if (!_0024self__002484.GetComponent<Animation>().IsPlaying("emotion_03"))
					{
						_0024self__002484.GetComponent<Animation>().Play("emotion_03");
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Victory_pack_3 _0024self__002485;

		public _0024run_emo_002483(Victory_pack_3 self_)
		{
			_0024self__002485 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002485);
		}
	}

	[Serializable]
	internal sealed class _0024OnCollisionEnter_002486 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal double _0024x_ball_002487;

			internal double _0024x_ball_h_002488;

			internal double _0024x_002489;

			internal double _0024y_002490;

			internal Collision _0024collision_002491;

			internal Victory_pack_3 _0024self__002492;

			public _0024(Collision collision, Victory_pack_3 self_)
			{
				_0024collision_002491 = collision;
				_0024self__002492 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024collision_002491.transform.tag.Equals("ball"))
					{
						if (PlayerPreperencesManager.GetSound() && !_0024self__002492.victory)
						{
							AudioRuntime.PlayOneShot(_0024self__002492.sound_kiss);
						}
						_0024self__002492.victory = true;
						_0024self__002492.GetComponent<Rigidbody>().isKinematic = true;
						_0024x_ball_002487 = _0024self__002492.transform.position.x;
						_0024x_ball_h_002488 = _0024collision_002491.transform.position.x;
						_0024self__002492.StartCoroutine(_0024self__002492.run_emo());
						if (!(_0024x_ball_002487 >= _0024x_ball_h_002488))
						{
							_0024self__002492.transform.eulerAngles = new Vector3(0f, 180f, -15f);
							_0024collision_002491.transform.eulerAngles = new Vector3(0f, 180f, 15f);
						}
						else
						{
							_0024self__002492.transform.eulerAngles = new Vector3(0f, 180f, 15f);
							_0024collision_002491.transform.eulerAngles = new Vector3(0f, 180f, -15f);
						}
						result = (Yield(2, new WaitForSeconds(0.3f)) ? 1 : 0);
						break;
					}
					goto IL_024b;
				case 2:
					_0024collision_002491.rigidbody.isKinematic = true;
					_0024x_002489 = _0024collision_002491.contacts[0].point.x;
					_0024y_002490 = _0024collision_002491.contacts[0].point.y;
					_0024self__002492.clone_hearts = (Transform)UnityEngine.Object.Instantiate(_0024self__002492.hearts, new Vector3((float)_0024x_002489, (float)_0024y_002490, -100f), Quaternion.identity);
					_0024self__002492.StartCoroutine(_0024self__002492.destroy_hearts());
					goto IL_024b;
				case 1:
					{
						result = 0;
						break;
					}
					IL_024b:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal Collision _0024collision_002493;

		internal Victory_pack_3 _0024self__002494;

		public _0024OnCollisionEnter_002486(Collision collision, Victory_pack_3 self_)
		{
			_0024collision_002493 = collision;
			_0024self__002494 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024collision_002493, _0024self__002494);
		}
	}

	[Serializable]
	internal sealed class _0024destroy_hearts_002495 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Victory_pack_3 _0024self__002496;

			public _0024(Victory_pack_3 self_)
			{
				_0024self__002496 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(2f)) ? 1 : 0);
					break;
				case 2:
				{
					UnityEngine.Object obj = _0024self__002496.clone_hearts as UnityEngine.Object;
					if (obj != null)
					{
						UnityEngine.Object.Destroy(obj);
					}
					YieldDefault(1);
					goto case 1;
				}
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Victory_pack_3 _0024self__002497;

		public _0024destroy_hearts_002495(Victory_pack_3 self_)
		{
			_0024self__002497 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002497);
		}
	}

	public Transform hearts;

	public object clone_hearts;

	public AudioClip sound_kiss;

	public bool victory;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual IEnumerator run_emo()
	{
		return new _0024run_emo_002483(this).GetEnumerator();
	}

	public virtual IEnumerator OnCollisionEnter(Collision collision)
	{
		return new _0024OnCollisionEnter_002486(collision, this).GetEnumerator();
	}

	public virtual IEnumerator destroy_hearts()
	{
		return new _0024destroy_hearts_002495(this).GetEnumerator();
	}

	public virtual void get_fail()
	{
	}
}
