using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class run_robo_emo : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024run_flying_ctp_0024104 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal bool _0024first_time_0024105;

			internal run_robo_emo _0024self__0024106;

			public _0024(run_robo_emo self_)
			{
				_0024self__0024106 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024first_time_0024105 = true;
					goto IL_00e8;
				case 2:
					if (!((double)_0024self__0024106.transform.position.y <= _0024self__0024106.height_ctp))
					{
						_0024self__0024106.time_play = Time.time + 2.8f;
						_0024first_time_0024105 = false;
						if (PlayerPreperencesManager.GetSound())
						{
							if (_0024self__0024106.GetComponent<AudioSource>().isPlaying)
							{
								_0024self__0024106.GetComponent<AudioSource>().Stop();
							}
							_0024self__0024106.GetComponent<AudioSource>().clip = _0024self__0024106.sound_flying_ctp;
							_0024self__0024106.GetComponent<AudioSource>().Play();
						}
					}
					goto IL_00e8;
				case 1:
					{
						result = 0;
						break;
					}
					IL_00e8:
					if (_0024first_time_0024105)
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

		internal run_robo_emo _0024self__0024107;

		public _0024run_flying_ctp_0024104(run_robo_emo self_)
		{
			_0024self__0024107 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024107);
		}
	}

	[Serializable]
	internal sealed class _0024measure_gravity_0024108 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal double _0024v1_0024109;

			internal double _0024v2_0024110;

			internal double _0024y1_0024111;

			internal double _0024y2_0024112;

			internal double _0024y3_0024113;

			internal run_robo_emo _0024self__0024114;

			public _0024(run_robo_emo self_)
			{
				_0024self__0024114 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024v1_0024109 = default(double);
					_0024v2_0024110 = default(double);
					_0024y1_0024111 = default(double);
					_0024y2_0024112 = default(double);
					_0024y3_0024113 = default(double);
					goto IL_005c;
				case 2:
					_0024y2_0024112 = _0024self__0024114.transform.position.y;
					result = (Yield(3, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				case 3:
					_0024y3_0024113 = _0024self__0024114.transform.position.y;
					_0024self__0024114.gravity = -100.0 * (_0024y3_0024113 - 2.0 * _0024y2_0024112 + _0024y1_0024111);
					if (!(_0024self__0024114.gravity <= _0024self__0024114.top_gravity))
					{
						_0024self__0024114.top_gravity = _0024self__0024114.gravity;
					}
					if (!(_0024self__0024114.gravity <= 300.0) && _0024self__0024114.mouth_closed)
					{
						if (!_0024self__0024114.found_hearts && !_0024self__0024114.transform.Find("roby").transform.GetComponent<Animation>().IsPlaying("love"))
						{
							_0024self__0024114.PlayEmo("emo_1");
						}
						if (PlayerPreperencesManager.GetSound())
						{
							if (_0024self__0024114.GetComponent<AudioSource>().isPlaying)
							{
								_0024self__0024114.GetComponent<AudioSource>().Stop();
							}
							_0024self__0024114.GetComponent<AudioSource>().clip = _0024self__0024114.sound_mouth_open;
							_0024self__0024114.GetComponent<AudioSource>().Play();
						}
						_0024self__0024114.mouth_closed = false;
					}
					goto IL_005c;
				case 1:
					{
						result = 0;
						break;
					}
					IL_005c:
					_0024y1_0024111 = _0024self__0024114.transform.position.y;
					result = (Yield(2, new WaitForSeconds(0.1f)) ? 1 : 0);
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robo_emo _0024self__0024115;

		public _0024measure_gravity_0024108(run_robo_emo self_)
		{
			_0024self__0024115 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024115);
		}
	}

	[Serializable]
	internal sealed class _0024anty_shacker_0024116 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal run_robo_emo _0024self__0024117;

			public _0024(run_robo_emo self_)
			{
				_0024self__0024117 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__0024117.p1 = _0024self__0024117.transform.position;
					result = (Yield(2, new WaitForSeconds((float)_0024self__0024117.anty_shacker_dt)) ? 1 : 0);
					break;
				case 2:
					_0024self__0024117.p2 = _0024self__0024117.transform.position;
					_0024self__0024117.cur_moving_distance = (_0024self__0024117.p2 - _0024self__0024117.p1).magnitude;
					if (!(_0024self__0024117.cur_moving_distance >= _0024self__0024117.moving_distance_limit))
					{
						_0024self__0024117.animate = false;
					}
					else
					{
						_0024self__0024117.animate = true;
					}
					goto default;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robo_emo _0024self__0024118;

		public _0024anty_shacker_0024116(run_robo_emo self_)
		{
			_0024self__0024118 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024118);
		}
	}

	[Serializable]
	internal sealed class _0024run_stay_anim_0024119 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal int _0024n_0024120;

			internal run_robo_emo _0024self__0024121;

			public _0024(run_robo_emo self_)
			{
				_0024self__0024121 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(_0024self__0024121.pause_before_first_emotion)) ? 1 : 0);
					break;
				case 2:
					_0024n_0024120 = 0;
					goto case 4;
				case 4:
					_0024self__0024121.last_pos = _0024self__0024121.transform.position;
					result = (Yield(3, new WaitForSeconds(0.5f)) ? 1 : 0);
					break;
				case 3:
					if ((bool)GameObject.Find("love_hearts"))
					{
						_0024self__0024121.found_hearts = true;
					}
					_0024self__0024121.last_deviation = (_0024self__0024121.last_pos - _0024self__0024121.current_pos).magnitude;
					if (!(_0024self__0024121.last_deviation >= _0024self__0024121.deviation) && !_0024self__0024121.found_hearts)
					{
						_0024n_0024120 = UnityEngine.Random.Range(1, 13);
						if (!GameObject.Find("ball").GetComponent<Rigidbody>().isKinematic && _0024n_0024120 == 7)
						{
							_0024n_0024120 = 8;
						}
						_0024self__0024121.PlayEmo(_0024n_0024120);
						_0024self__0024121.pause = UnityEngine.Random.Range(5, 10);
						result = (Yield(4, new WaitForSeconds(_0024self__0024121.pause)) ? 1 : 0);
						break;
					}
					goto case 4;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal run_robo_emo _0024self__0024122;

		public _0024run_stay_anim_0024119(run_robo_emo self_)
		{
			_0024self__0024122 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024122);
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

	public AudioClip hit_wood;

	public AudioClip hit_metal_box;

	public AudioClip hit_metal_balk;

	public float time_wood;

	public float time_metal_box;

	public float time_metal_balk;

	public Collider last_collided;

	public bool first;

	public bool first_exit;

	public float moving_distance_limit;

	public float cur_moving_distance;

	public Vector3 p1;

	public Vector3 p2;

	public double anty_shacker_dt;

	public double gravity;

	public double top_gravity;

	public bool mouth_closed;

	public AudioClip start_sound;

	public AudioClip sound_mouth_open;

	public AudioClip sound_flying_ctp;

	public AudioClip sound_fail;

	public bool play_ctp;

	public double height_ctp;

	public float time_play;

	public bool fail;

	public AnimationClip[] emotions;

	public bool found_hearts;

	public run_robo_emo()
	{
		play_again = true;
		deviation = 5f;
		pause = 5;
		pause_before_first_emotion = 5;
		first = true;
		first_exit = true;
		moving_distance_limit = 2f;
		anty_shacker_dt = 0.25;
		mouth_closed = true;
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
		emotions = new AnimationClip[18];
		if (!PlayerPrefs.GetInt("cur_pack").Equals(3))
		{
			emotions[0] = Resources.Load("emotions/Robo/Emotion_Start") as AnimationClip;
		}
		else
		{
			emotions[0] = Resources.Load("emotions/Robo/robo1_p3_startEmotion") as AnimationClip;
		}
		if (!(Animation)transform.Find("roby").gameObject.GetComponent(typeof(Animation)))
		{
			transform.Find("roby").gameObject.AddComponent(typeof(Animation));
		}
		transform.Find("roby").gameObject.GetComponent<Animation>().playAutomatically = false;
		transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[0], "start");
		if (PlayerPrefs.GetInt("cur_level").Equals(1))
		{
			PlayEmo("start");
		}
		for (int i = 2; i <= 14; i++)
		{
			emotions[i - 1] = Resources.Load("emotions/Robo/emotion_" + i.ToString()) as AnimationClip;
			transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[i - 1], (i - 1).ToString());
		}
		emotions[14] = Resources.Load("emotions/Robo/emotion_1") as AnimationClip;
		emotions[15] = Resources.Load("emotions/Robo/emotion_1_2") as AnimationClip;
		emotions[16] = Resources.Load("emotions/Robo/emotion_Fall") as AnimationClip;
		emotions[17] = Resources.Load("emotions/Robo/emotion_Love") as AnimationClip;
		transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[14], "emo_1");
		transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[15], "emo_1_2");
		transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[16], "fall");
		transform.Find("roby").transform.GetComponent<Animation>().AddClip(emotions[17], "love");
	}

	public virtual void PlayEmo(int n)
	{
		transform.Find("roby").transform.GetComponent<Animation>().Play(n.ToString());
	}

	public virtual void PlayEmo(string name)
	{
		transform.Find("roby").transform.GetComponent<Animation>().Play(name);
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
		}
	}

	public virtual IEnumerator run_flying_ctp()
	{
		return new _0024run_flying_ctp_0024104(this).GetEnumerator();
	}

	public virtual IEnumerator measure_gravity()
	{
		return new _0024measure_gravity_0024108(this).GetEnumerator();
	}

	public virtual IEnumerator anty_shacker()
	{
		return new _0024anty_shacker_0024116(this).GetEnumerator();
	}

	public virtual void Update()
	{
		current_pos = transform.position;
	}

	public virtual void OnCollisionExit(Collision collision)
	{
		if ((!GameObject.Find("level_failed_window") || !GameObject.Find("level_failed_window").GetComponent<Animation>().isPlaying) && animate)
		{
			if (!found_hearts && !transform.Find("roby").transform.GetComponent<Animation>().IsPlaying("love"))
			{
				PlayEmo("emo_1");
			}
			if (PlayerPreperencesManager.GetSound() && !(Time.time <= time_play) && !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = sound_mouth_open;
				GetComponent<AudioSource>().Play();
			}
			mouth_closed = false;
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if ((bool)GameObject.Find("level_failed_window") && GameObject.Find("level_failed_window").GetComponent<Animation>().isPlaying)
		{
			return;
		}
		n++;
		ContactPoint contactPoint = collision.contacts[0];
		if (contactPoint.otherCollider.name.Equals("floor") && !fail)
		{
			if (!found_hearts)
			{
				PlayEmo("fall");
			}
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
			if (!found_hearts && !transform.Find("roby").transform.GetComponent<Animation>().IsPlaying("love"))
			{
				PlayEmo("emo_1_2");
			}
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
		return new _0024run_stay_anim_0024119(this).GetEnumerator();
	}

	public virtual void CallFailModal()
	{
		if ((bool)GameObject.Find("level_failed_window") && (Mathf.Abs(GameObject.Find("level_failed_window").transform.position.y) >= 100f || !(Mathf.Abs(GameObject.Find("level_failed_window").transform.position.x) < 100f)))
		{
			GameObject.Find("level_failed_window").transform.position = new Vector3(0f, 0f, 200f);
			GameObject.Find("level_failed_window").GetComponent<Animation>().Play("level_failed_01");
			if ((bool)GameObject.Find("m3"))
			{
				GameObject.Find("m3").GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
