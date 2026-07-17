using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class run_robot4_emo : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024run_flying_ctp_0024155 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal bool _0024first_time_0024156;

			internal run_robot4_emo _0024self__0024157;

			public _0024(run_robot4_emo self_)
			{
				_0024self__0024157 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024first_time_0024156 = true;
					goto IL_00e8;
				case 2:
					if (!((double)_0024self__0024157.transform.position.y <= _0024self__0024157.height_ctp))
					{
						_0024self__0024157.time_play = Time.time + 2.8f;
						_0024first_time_0024156 = false;
						if (PlayerPreperencesManager.GetSound())
						{
							if (_0024self__0024157.GetComponent<AudioSource>().isPlaying)
							{
								_0024self__0024157.GetComponent<AudioSource>().Stop();
							}
							_0024self__0024157.GetComponent<AudioSource>().clip = _0024self__0024157.sound_flying_ctp;
							_0024self__0024157.GetComponent<AudioSource>().Play();
						}
					}
					goto IL_00e8;
				case 1:
					{
						result = 0;
						break;
					}
					IL_00e8:
					if (_0024first_time_0024156)
					{
						result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
						break;
					}
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot4_emo _0024self__0024158;

		public _0024run_flying_ctp_0024155(run_robot4_emo self_)
		{
			_0024self__0024158 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024158);
		}
	}

	[Serializable]
	internal sealed class _0024measure_gravity_0024159 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal double _0024v1_0024160;

			internal double _0024v2_0024161;

			internal double _0024y1_0024162;

			internal double _0024y2_0024163;

			internal double _0024y3_0024164;

			internal run_robot4_emo _0024self__0024165;

			public _0024(run_robot4_emo self_)
			{
				_0024self__0024165 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024v1_0024160 = default(double);
					_0024v2_0024161 = default(double);
					_0024y1_0024162 = default(double);
					_0024y2_0024163 = default(double);
					_0024y3_0024164 = default(double);
					goto IL_005c;
				case 2:
					_0024y2_0024163 = _0024self__0024165.transform.position.y;
					result = (Yield(3, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				case 3:
					_0024y3_0024164 = _0024self__0024165.transform.position.y;
					_0024self__0024165.gravity = -100.0 * (_0024y3_0024164 - 2.0 * _0024y2_0024163 + _0024y1_0024162);
					if (!(_0024self__0024165.gravity <= _0024self__0024165.top_gravity))
					{
						_0024self__0024165.top_gravity = _0024self__0024165.gravity;
					}
					if (!(_0024self__0024165.gravity <= 300.0) && _0024self__0024165.mouth_closed)
					{
						_0024self__0024165.GetComponent<Animation>().Play("robot4_emotion_01");
						_0024self__0024165.mouth_closed = false;
						if (PlayerPreperencesManager.GetSound())
						{
							if (_0024self__0024165.GetComponent<AudioSource>().isPlaying)
							{
								_0024self__0024165.GetComponent<AudioSource>().Stop();
							}
							_0024self__0024165.GetComponent<AudioSource>().clip = _0024self__0024165.sound_mouth_open;
							_0024self__0024165.GetComponent<AudioSource>().Play();
						}
						_0024self__0024165.mouth_closed = false;
					}
					goto IL_005c;
				case 1:
					{
						result = 0;
						break;
					}
					IL_005c:
					_0024y1_0024162 = _0024self__0024165.transform.position.y;
					result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot4_emo _0024self__0024166;

		public _0024measure_gravity_0024159(run_robot4_emo self_)
		{
			_0024self__0024166 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024166);
		}
	}

	[Serializable]
	internal sealed class _0024anty_shacker_0024167 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal run_robot4_emo _0024self__0024168;

			public _0024(run_robot4_emo self_)
			{
				_0024self__0024168 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__0024168.p1 = _0024self__0024168.transform.position;
					result = (Yield(2, new WaitForSeconds(0.25f)) ? 1 : 0);
					break;
				case 2:
					_0024self__0024168.p2 = _0024self__0024168.transform.position;
					_0024self__0024168.cur_moving_distance = (_0024self__0024168.p2 - _0024self__0024168.p1).magnitude;
					if (!(_0024self__0024168.cur_moving_distance >= _0024self__0024168.moving_distance_limit))
					{
						_0024self__0024168.animate = false;
					}
					else
					{
						_0024self__0024168.animate = true;
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot4_emo _0024self__0024169;

		public _0024anty_shacker_0024167(run_robot4_emo self_)
		{
			_0024self__0024169 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024169);
		}
	}

	[Serializable]
	internal sealed class _0024run_stay_anim_0024170 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal run_robot4_emo _0024self__0024171;

			public _0024(run_robot4_emo self_)
			{
				_0024self__0024171 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(_0024self__0024171.pause_before_first_emotion)) ? 1 : 0);
					break;
				case 2:
				case 4:
					_0024self__0024171.last_pos = _0024self__0024171.transform.position;
					result = (Yield(3, new WaitForSeconds(0.5f)) ? 1 : 0);
					break;
				case 3:
					_0024self__0024171.last_deviation = (_0024self__0024171.last_pos - _0024self__0024171.current_pos).magnitude;
					if (!(_0024self__0024171.last_deviation >= _0024self__0024171.deviation))
					{
						_0024self__0024171.c_02 = UnityEngine.Random.Range(3, 6);
						if (_0024self__0024171.c_03 == 0)
						{
							_0024self__0024171.prev_emo = _0024self__0024171.c_02;
							_0024self__0024171.c_03++;
							_0024self__0024171.GetComponent<Animation>().Play("robot4_emotion_0" + _0024self__0024171.c_02.ToString());
						}
						else if (_0024self__0024171.c_02 == _0024self__0024171.prev_emo)
						{
							_0024self__0024171.c_03++;
							if (_0024self__0024171.c_03 < 3)
							{
								_0024self__0024171.GetComponent<Animation>().Play("robot4_emotion_0" + _0024self__0024171.c_02.ToString());
							}
							else
							{
								_0024self__0024171.c_03++;
								_0024self__0024171.prev_emo = _0024self__0024171.c_02 + 1;
								_0024self__0024171.GetComponent<Animation>().Play("robot4_emotion_0" + (_0024self__0024171.c_02 + 1).ToString());
							}
						}
						else
						{
							_0024self__0024171.GetComponent<Animation>().Play("robot4_emotion_0" + _0024self__0024171.c_02.ToString());
						}
					}
					_0024self__0024171.pause = UnityEngine.Random.Range(5, 10);
					result = (Yield(4, new WaitForSeconds(_0024self__0024171.pause)) ? 1 : 0);
					break;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robot4_emo _0024self__0024172;

		public _0024run_stay_anim_0024170(run_robot4_emo self_)
		{
			_0024self__0024172 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024172);
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

	public run_robot4_emo()
	{
		play_again = true;
		deviation = 5f;
		pause = 5;
		pause_before_first_emotion = 5;
		first = true;
		moving_distance_limit = 50f;
		mouth_closed = true;
		anty_shacker_dt = 0.25;
	}

	public virtual void Start()
	{
		AudioRuntime.Ensure2DSource(gameObject);
		StartCoroutine(run_stay_anim());
		StartCoroutine(anty_shacker());
		StartCoroutine(measure_gravity());
		run_start_sound();
		if (play_ctp)
		{
			StartCoroutine(run_flying_ctp());
		}
		if (PlayerPrefs.GetInt("cur_level").Equals(1))
		{
			gameObject.GetComponent<Animation>().Play("robo4_startEmo");
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
			GetComponent<Animation>().Play("robot4_emotion_03");
		}
	}

	public virtual IEnumerator run_flying_ctp()
	{
		return new _0024run_flying_ctp_0024155(this).GetEnumerator();
	}

	public virtual IEnumerator measure_gravity()
	{
		return new _0024measure_gravity_0024159(this).GetEnumerator();
	}

	public virtual IEnumerator anty_shacker()
	{
		return new _0024anty_shacker_0024167(this).GetEnumerator();
	}

	public virtual void Update()
	{
		current_pos = transform.position;
	}

	public virtual void OnCollisionExit(Collision collision)
	{
		if (animate)
		{
			GetComponent<Animation>().Play("robot4_emotion_01");
			mouth_closed = false;
			if (PlayerPreperencesManager.GetSound() && !(Time.time <= time_play) && !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = sound_mouth_open;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		n++;
		ContactPoint contactPoint = collision.contacts[0];
		if (contactPoint.otherCollider.name.Equals("floor") && !fail)
		{
			GetComponent<Animation>().Play("robot4_alarm");
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
			GetComponent<Animation>().Play("robot4_emotion_02");
			mouth_closed = true;
			if (PlayerPreperencesManager.GetSound() && !GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip != sound_fail)
			{
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = sound_mouth_open;
				GetComponent<AudioSource>().Play();
			}
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
		return new _0024run_stay_anim_0024170(this).GetEnumerator();
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
