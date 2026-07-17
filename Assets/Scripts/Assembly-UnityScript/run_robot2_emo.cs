using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class run_robot2_emo : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024measure_gravity_0024123 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal double _0024v1_0024124;

			internal double _0024v2_0024125;

			internal double _0024y1_0024126;

			internal double _0024y2_0024127;

			internal double _0024y3_0024128;

			internal run_robot2_emo _0024self__0024129;

			public _0024(run_robot2_emo self_)
			{
				_0024self__0024129 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024v1_0024124 = default(double);
					_0024v2_0024125 = default(double);
					_0024y1_0024126 = default(double);
					_0024y2_0024127 = default(double);
					_0024y3_0024128 = default(double);
					goto IL_005c;
				case 2:
					_0024y2_0024127 = _0024self__0024129.transform.position.y;
					result = (Yield(3, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				case 3:
					_0024y3_0024128 = _0024self__0024129.transform.position.y;
					_0024self__0024129.gravity = -100.0 * (_0024y3_0024128 - 2.0 * _0024y2_0024127 + _0024y1_0024126);
					if (!(_0024self__0024129.gravity <= _0024self__0024129.top_gravity))
					{
						_0024self__0024129.top_gravity = _0024self__0024129.gravity;
					}
					if (!(_0024self__0024129.gravity <= 300.0) && _0024self__0024129.mouth_closed)
					{
						_0024self__0024129.GetComponent<Animation>().Play("robot2_emotion_01");
						_0024self__0024129.mouth_closed = false;
						if (PlayerPreperencesManager.GetSound())
						{
							if (_0024self__0024129.GetComponent<AudioSource>().isPlaying)
							{
								_0024self__0024129.GetComponent<AudioSource>().Stop();
							}
							_0024self__0024129.GetComponent<AudioSource>().clip = _0024self__0024129.sound_mouth_open;
							_0024self__0024129.GetComponent<AudioSource>().Play();
						}
						_0024self__0024129.mouth_closed = false;
					}
					goto IL_005c;
				case 1:
					{
						result = 0;
						break;
					}
					IL_005c:
					_0024y1_0024126 = _0024self__0024129.transform.position.y;
					result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot2_emo _0024self__0024130;

		public _0024measure_gravity_0024123(run_robot2_emo self_)
		{
			_0024self__0024130 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024130);
		}
	}

	[Serializable]
	internal sealed class _0024anty_shacker_0024131 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal run_robot2_emo _0024self__0024132;

			public _0024(run_robot2_emo self_)
			{
				_0024self__0024132 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__0024132.p1 = _0024self__0024132.transform.position;
					result = (Yield(2, new WaitForSeconds(0.25f)) ? 1 : 0);
					break;
				case 2:
					_0024self__0024132.p2 = _0024self__0024132.transform.position;
					_0024self__0024132.cur_moving_distance = (_0024self__0024132.p2 - _0024self__0024132.p1).magnitude;
					if (!(_0024self__0024132.cur_moving_distance >= _0024self__0024132.moving_distance_limit))
					{
						_0024self__0024132.animate = false;
					}
					else
					{
						_0024self__0024132.animate = true;
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot2_emo _0024self__0024133;

		public _0024anty_shacker_0024131(run_robot2_emo self_)
		{
			_0024self__0024133 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024133);
		}
	}

	[Serializable]
	internal sealed class _0024run_stay_anim_0024134 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal run_robot2_emo _0024self__0024135;

			public _0024(run_robot2_emo self_)
			{
				_0024self__0024135 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(_0024self__0024135.pause_before_first_emotion)) ? 1 : 0);
					break;
				case 2:
				case 4:
					_0024self__0024135.last_pos = _0024self__0024135.transform.position;
					result = (Yield(3, new WaitForSeconds(0.5f)) ? 1 : 0);
					break;
				case 3:
					_0024self__0024135.last_deviation = (_0024self__0024135.last_pos - _0024self__0024135.current_pos).magnitude;
					if (!(_0024self__0024135.last_deviation >= _0024self__0024135.deviation))
					{
						_0024self__0024135.c_02 = UnityEngine.Random.Range(3, 6);
						if (_0024self__0024135.c_03 == 0)
						{
							_0024self__0024135.prev_emo = _0024self__0024135.c_02;
							_0024self__0024135.c_03++;
							_0024self__0024135.GetComponent<Animation>().Play("robot2_emotion_0" + _0024self__0024135.c_02.ToString());
						}
						else if (_0024self__0024135.c_02 == _0024self__0024135.prev_emo)
						{
							_0024self__0024135.c_03++;
							if (_0024self__0024135.c_03 < 3)
							{
								_0024self__0024135.GetComponent<Animation>().Play("robot2_emotion_0" + _0024self__0024135.c_02.ToString());
							}
							else
							{
								_0024self__0024135.c_03++;
								_0024self__0024135.prev_emo = _0024self__0024135.c_02 + 1;
								_0024self__0024135.GetComponent<Animation>().Play("robot2_emotion_0" + (_0024self__0024135.c_02 + 1).ToString());
							}
						}
						else
						{
							_0024self__0024135.GetComponent<Animation>().Play("robot2_emotion_0" + _0024self__0024135.c_02.ToString());
						}
					}
					_0024self__0024135.pause = UnityEngine.Random.Range(5, 10);
					result = (Yield(4, new WaitForSeconds(_0024self__0024135.pause)) ? 1 : 0);
					break;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot2_emo _0024self__0024136;

		public _0024run_stay_anim_0024134(run_robot2_emo self_)
		{
			_0024self__0024136 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024136);
		}
	}

	public bool open_mouth;

	public bool play_again;

	public double count;

	public float deviation;

	public Vector3 current_pos;

	public Vector3 last_pos;

	public float last_deviation;

	public int pause;

	public int c_02;

	public int c_03;

	public int pause_before_first_emotion;

	public int n;

	public bool animate;

	public int prev_emo;

	public AudioClip hit_wood;

	public AudioClip hit_metal_box;

	public AudioClip hit_metal_balk;

	public float time_wood;

	public float time_metal_box;

	public float time_metal_balk;

	public Collider last_collided;

	public bool first;

	public float moving_distance_limit;

	public float cur_moving_distance;

	public Vector3 p1;

	public Vector3 p2;

	public double gravity;

	public double top_gravity;

	public bool mouth_closed;

	public double anty_shacker_dt;

	public AudioClip start_sound;

	public AudioClip sound_mouth_open;

	public AudioClip sound_flying_ctp;

	public AudioClip sound_fail;

	public bool play_ctp;

	public double height_ctp;

	public float time_play;

	public bool fail;

	public bool victory;

	public run_robot2_emo()
	{
		play_again = true;
		deviation = 5f;
		pause = 5;
		pause_before_first_emotion = 5;
		first = true;
		moving_distance_limit = 2f;
		mouth_closed = true;
		anty_shacker_dt = 0.05000000074505806;
	}

	public virtual void Start()
	{
		AudioRuntime.Ensure2DSource(gameObject);
		StartCoroutine(run_stay_anim());
		StartCoroutine(anty_shacker());
		StartCoroutine(measure_gravity());
		run_start_sound();
		if (PlayerPrefs.GetInt("cur_level").Equals(1))
		{
			gameObject.GetComponent<Animation>().Play("robo2_startEmo");
		}
	}

	public virtual void run_start_sound()
	{
		if (PlayerPreperencesManager.GetSound())
		{
			if (GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Stop();
			}
			GetComponent<AudioSource>().clip = start_sound;
			GetComponent<AudioSource>().Play();
			GetComponent<Animation>().Play("robot2_emotion_04");
		}
	}

	public virtual IEnumerator measure_gravity()
	{
		return new _0024measure_gravity_0024123(this).GetEnumerator();
	}

	public virtual IEnumerator anty_shacker()
	{
		return new _0024anty_shacker_0024131(this).GetEnumerator();
	}

	public virtual void Update()
	{
		current_pos = transform.position;
	}

	public virtual void OnCollisionExit(Collision collision)
	{
		if (!victory && !collision.transform.tag.Equals("conveertag") && animate)
		{
			GetComponent<Animation>().Play("robot2_emotion_01");
			mouth_closed = false;
			if (PlayerPreperencesManager.GetSound() && Time.time >= time_play && !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = sound_mouth_open;
				GetComponent<AudioSource>().Play();
				MonoBehaviour.print("play open mouth robot3");
			}
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (victory)
		{
			return;
		}
		n++;
		ContactPoint contactPoint = collision.contacts[0];
		if (contactPoint.otherCollider.name.Equals("ball"))
		{
			GetComponent<Animation>().Play("robot2_emotion_06");
			victory = true;
		}
		if (victory)
		{
			return;
		}
		if (contactPoint.otherCollider.name.Equals("floor") && !fail)
		{
			GetComponent<Animation>().Play("robot2_alarm");
			if (PlayerPreperencesManager.GetSound())
			{
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = sound_fail;
				GetComponent<AudioSource>().Play();
				fail = true;
			}
			CallFailModal();
		}
		else if (animate)
		{
			GetComponent<Animation>().Play("robot2_emotion_02");
			mouth_closed = true;
		}
		if (first)
		{
			first = false;
			last_collided = contactPoint.otherCollider;
		}
		else
		{
			if (contactPoint.otherCollider.Equals(last_collided))
			{
				return;
			}
			last_collided = contactPoint.otherCollider;
			string text = contactPoint.otherCollider.material.ToString();
			if (text == "Wood (Instance) (UnityEngine.PhysicMaterial)")
			{
				if (Time.time >= time_wood + 1f)
				{
					time_wood = Time.time;
					if (PlayerPreperencesManager.GetSound() && !fail)
					{
						AudioRuntime.PlayOneShot(hit_wood);
					}
				}
			}
			else
			{
				if (!(text == "Metal (Instance) (UnityEngine.PhysicMaterial)"))
				{
					return;
				}
				if (contactPoint.otherCollider.name.Equals("box_metal"))
				{
					if (Time.time >= time_metal_box + 1f)
					{
						time_metal_box = Time.time;
						if (PlayerPreperencesManager.GetSound() && !fail)
						{
							AudioRuntime.PlayOneShot(hit_metal_box);
						}
					}
				}
				else if (Time.time >= time_metal_balk + 1f)
				{
					time_metal_balk = Time.time;
					if (PlayerPreperencesManager.GetSound() && !fail)
					{
						AudioRuntime.PlayOneShot(hit_metal_balk);
					}
				}
			}
		}
	}

	public virtual IEnumerator run_stay_anim()
	{
		return new _0024run_stay_anim_0024134(this).GetEnumerator();
	}

	public virtual void CallFailModal()
	{
		if ((bool)GameObject.Find("level_failed_window") && (Mathf.Abs(GameObject.Find("level_failed_window").transform.position.y) >= 100f || !(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.x) < 100f)))
		{
			GameObject.Find("level_failed_window").transform.position = new Vector3(0f, 0f, 200f);
			GameObject.Find("level_failed_window").GetComponent<Animation>().Play("level_failed_01");
		}
	}
}
