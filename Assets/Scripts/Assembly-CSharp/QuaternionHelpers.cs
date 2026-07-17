using System;
using UnityEngine;

public static class QuaternionHelpers
{
	private static int[] yawAxes = new int[6] { 0, 1, 2, 0, 1, 2 };

	private static int[] pitchAxes = new int[6] { 1, 2, 0, 2, 0, 1 };

	private static int[] rollAxes = new int[6] { 2, 0, 1, 1, 2, 0 };

	private static float[] pitchScalars = new float[6] { -1f, -1f, -1f, 1f, 1f, 1f };

	private static float yawAngle = 0f;

	private static float pitchAngle = 0f;

	private static float rollAngle = 0f;

	private static FloatMatrix[] rotationMatrices = new FloatMatrix[3]
	{
		new FloatMatrix(new Vector3[3]
		{
			Vector3.right,
			Vector3.zero,
			Vector3.zero
		}),
		new FloatMatrix(new Vector3[3]
		{
			Vector3.zero,
			Vector3.up,
			Vector3.zero
		}),
		new FloatMatrix(new Vector3[3]
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.forward
		})
	};

	private static readonly FloatMatrix firstGuess = new FloatMatrix(new float[4] { 0.5f, 0.5f, 0.5f, 0.5f });

	public static Quaternion CustomLookRotation(Vector3 forward, Vector3 upwards, Vector3 aimVector, Vector3 upVector)
	{
		return Quaternion.LookRotation(forward, upwards) * Quaternion.Inverse(Quaternion.LookRotation(aimVector, upVector));
	}

	public static Vector3 ToEulerAngles(Quaternion q, EulerRotationOrder order)
	{
		FloatMatrix floatMatrix = new FloatMatrix(new Vector3[3]
		{
			q * Vector3.right,
			q * Vector3.up,
			q * Vector3.forward
		});
		pitchAngle = 57.29578f * Mathf.Asin(pitchScalars[(int)order] * floatMatrix[yawAxes[(int)order], rollAxes[(int)order]]);
		if (pitchAngle < 90f)
		{
			if (pitchAngle > -90f)
			{
				yawAngle = 57.29578f * Mathf.Atan2((0f - pitchScalars[(int)order]) * floatMatrix[pitchAxes[(int)order], rollAxes[(int)order]], floatMatrix[rollAxes[(int)order], rollAxes[(int)order]]);
				rollAngle = 57.29578f * Mathf.Atan2((0f - pitchScalars[(int)order]) * floatMatrix[yawAxes[(int)order], pitchAxes[(int)order]], floatMatrix[yawAxes[(int)order], yawAxes[(int)order]]);
			}
			else
			{
				rollAngle = 0f;
				yawAngle = rollAngle - 57.29578f * Mathf.Atan2(pitchScalars[(int)order] * floatMatrix[pitchAxes[(int)order], yawAxes[(int)order]], floatMatrix[pitchAxes[(int)order], pitchAxes[(int)order]]);
			}
		}
		else
		{
			rollAngle = 0f;
			yawAngle = 57.29578f * Mathf.Atan2(pitchScalars[(int)order] * floatMatrix[pitchAxes[(int)order], yawAxes[(int)order]], floatMatrix[pitchAxes[(int)order], pitchAxes[(int)order]]) - rollAngle;
		}
		Vector3 zero = Vector3.zero;
		zero[rollAxes[(int)order]] = rollAngle;
		zero[yawAxes[(int)order]] = yawAngle;
		zero[pitchAxes[(int)order]] = pitchAngle;
		return zero;
	}

	public static Quaternion FromEulerAngles(Vector3 eulerAngles, EulerRotationOrder order)
	{
		rotationMatrices[0][1, 1] = Mathf.Cos((float)Math.PI / 180f * eulerAngles.x);
		rotationMatrices[0][2, 2] = rotationMatrices[0][1, 1];
		rotationMatrices[0][1, 2] = Mathf.Sin((float)Math.PI / 180f * eulerAngles.x);
		rotationMatrices[0][2, 1] = 0f - rotationMatrices[0][1, 2];
		rotationMatrices[1][2, 2] = Mathf.Cos((float)Math.PI / 180f * eulerAngles.y);
		rotationMatrices[1][0, 0] = rotationMatrices[1][2, 2];
		rotationMatrices[1][2, 0] = Mathf.Sin((float)Math.PI / 180f * eulerAngles.y);
		rotationMatrices[1][0, 2] = 0f - rotationMatrices[1][2, 0];
		rotationMatrices[2][0, 0] = Mathf.Cos((float)Math.PI / 180f * eulerAngles.z);
		rotationMatrices[2][1, 1] = rotationMatrices[2][0, 0];
		rotationMatrices[2][0, 1] = Mathf.Sin((float)Math.PI / 180f * eulerAngles.z);
		rotationMatrices[2][1, 0] = 0f - rotationMatrices[2][0, 1];
		FloatMatrix floatMatrix = rotationMatrices[yawAxes[(int)order]] * rotationMatrices[pitchAxes[(int)order]] * rotationMatrices[rollAxes[(int)order]];
		float trace = floatMatrix.trace;
		float num = 0f;
		Vector4 zero = Vector4.zero;
		if (trace > 0f)
		{
			num = Mathf.Sqrt(trace + 1f);
			zero.w = 0.5f * num;
			num = 0.5f / num;
			zero.x = (floatMatrix[2, 1] - floatMatrix[1, 2]) * num;
			zero.y = (floatMatrix[0, 2] - floatMatrix[2, 0]) * num;
			zero.z = (floatMatrix[1, 0] - floatMatrix[0, 1]) * num;
		}
		else
		{
			int[] array = new int[3] { 1, 2, 0 };
			int num2 = 0;
			if (floatMatrix[1, 1] > floatMatrix[0, 0])
			{
				num2 = 1;
			}
			if (floatMatrix[2, 2] > floatMatrix[num2, num2])
			{
				num2 = 2;
			}
			int num3 = array[num2];
			int num4 = array[num3];
			num = Mathf.Sqrt(floatMatrix[num2, num2] - floatMatrix[num3, num3] - floatMatrix[num4, num4] + 1f);
			zero[num2] = 0.5f * num;
			num = 0.5f / num;
			zero[3] = (floatMatrix[num4, num3] - floatMatrix[num3, num4]) * num;
			zero[num3] = (floatMatrix[num3, num2] + floatMatrix[num2, num3]) * num;
			zero[num4] = (floatMatrix[num4, num2] + floatMatrix[num2, num4]) * num;
		}
		return new Quaternion(zero.x, zero.y, zero.z, 0f - zero.w);
	}

	public static Quaternion SlerpLong(Quaternion from, Quaternion to, float t)
	{
		float num = Quaternion.Dot(from, to);
		if ((double)Mathf.Abs(num) >= 1.0)
		{
			return from;
		}
		Quaternion quaternion;
		if (num > 0f)
		{
			num *= -1f;
			quaternion = new Quaternion(0f - from.x, 0f - from.y, 0f - from.z, 0f - from.w);
		}
		else
		{
			quaternion = from;
		}
		float num2 = Mathf.Acos(num);
		float num3 = Mathf.Sqrt(1f - num * num);
		if (Mathf.Abs(num3) == 0f)
		{
			return new Quaternion(from.x * 0.5f + to.x * 0.5f, from.y * 0.5f + to.y * 0.5f, from.z * 0.5f + to.z * 0.5f, from.w * 0.5f + to.w * 0.5f);
		}
		float num4 = 1f / num3;
		float num5 = Mathf.Sin((1f - t) * num2) * num4;
		float num6 = Mathf.Sin(t * num2) * num4;
		return new Quaternion(quaternion.x * num5 + to.x * num6, quaternion.y * num5 + to.y * num6, quaternion.z * num5 + to.z * num6, quaternion.w * num5 + to.w * num6);
	}

	public static QuaternionInterpolationTarget[] NormalizeTargetWeights(QuaternionInterpolationTarget[] targets)
	{
		float num = 0f;
		for (int i = 0; i < targets.Length; i++)
		{
			num += targets[i].weight;
		}
		float num2 = 1f / num;
		for (int j = 0; j < targets.Length; j++)
		{
			targets[j].weight *= num2;
		}
		return targets;
	}

	public static Quaternion Interpolate(QuaternionInterpolationTarget[] normalizedTargets, QuaternionInterpolationMode interpType, ref Quaternion[] cache)
	{
		return Interpolate(normalizedTargets, interpType, ref cache, 1f / (float)normalizedTargets.Length);
	}

	public static Quaternion Interpolate(QuaternionInterpolationTarget[] normalizedTargets, QuaternionInterpolationMode interpType, ref Quaternion[] cache, float oneOverListLength)
	{
		switch (interpType)
		{
		case QuaternionInterpolationMode.Average:
			return InterpolateAverage(normalizedTargets);
		case QuaternionInterpolationMode.Slime:
			return InterpolateSlime(normalizedTargets);
		case QuaternionInterpolationMode.Longest:
			return SequentialSlerp(normalizedTargets, false, ref cache);
		default:
			return SequentialSlerp(normalizedTargets, true, ref cache);
		}
	}

	public static Quaternion SequentialSlerp(QuaternionInterpolationTarget[] normalizedTargets, bool isShortPath, ref Quaternion[] cache)
	{
		Quaternion quaternion = normalizedTargets[0].quaternion;
		for (int i = 1; i < normalizedTargets.Length; i++)
		{
			quaternion = ((!isShortPath) ? SlerpLong(quaternion, normalizedTargets[i].quaternion, normalizedTargets[i].weight) : Quaternion.Slerp(quaternion, normalizedTargets[i].quaternion, normalizedTargets[i].weight));
		}
		for (int j = 1; j < cache.Length; j++)
		{
			cache[j - 1] = cache[j];
		}
		cache[cache.Length - 1] = quaternion;
		return quaternion;
	}

	public static Quaternion InterpolateAverage(QuaternionInterpolationTarget[] normalizedTargets)
	{
		Quaternion quaternion = normalizedTargets[0].quaternion;
		for (int i = 1; i < normalizedTargets.Length; i++)
		{
			quaternion = Quaternion.Lerp(quaternion, normalizedTargets[i].quaternion, normalizedTargets[i].weight);
		}
		return quaternion;
	}

	public static Quaternion InterpolateSlime(QuaternionInterpolationTarget[] targets)
	{
		return InterpolateSlime(NormalizeTargetWeights(targets), 1f / (float)targets.Length);
	}

	public static Quaternion InterpolateSlime(QuaternionInterpolationTarget[] normalizedTargets, float oneOverListLength)
	{
		Quaternion quaternion = Mean(normalizedTargets);
		Vector4 zero = Vector4.zero;
		for (int i = 0; i < normalizedTargets.Length; i++)
		{
			zero += normalizedTargets[i].weight * Log(Quaternion.Inverse(quaternion) * normalizedTargets[i].quaternion);
		}
		return quaternion * Exp(zero);
	}

	public static Quaternion Mean(QuaternionInterpolationTarget[] targets)
	{
		Quaternion[] array = new Quaternion[targets.Length];
		for (int i = 0; i < targets.Length; i++)
		{
			array[i] = targets[i].quaternion;
		}
		return Mean(array);
	}

	public static Quaternion Mean(Quaternion[] quaternions)
	{
		FloatMatrix floatMatrix = new FloatMatrix(quaternions);
		FloatMatrix transpose = floatMatrix.transpose;
		FloatMatrix floatMatrix2 = transpose * floatMatrix;
		FloatMatrix floatMatrix3 = firstGuess;
		floatMatrix3 *= floatMatrix2;
		floatMatrix3 *= 1f / new Vector4(floatMatrix3[0, 0], floatMatrix3[1, 0], floatMatrix3[2, 0], floatMatrix3[3, 0]).magnitude;
		return new Quaternion(floatMatrix3[0, 0], floatMatrix3[1, 0], floatMatrix3[2, 0], floatMatrix3[3, 0]);
	}

	public static Quaternion Exp(Quaternion q)
	{
		return Exp(new Vector4(q.x, q.y, q.z, q.w));
	}

	public static Quaternion Exp(Vector4 q)
	{
		float magnitude = new Vector3(q.x, q.y, q.z).magnitude;
		float num = Mathf.Sin(magnitude);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = Mathf.Cos(magnitude);
		if (Mathf.Abs(magnitude) > 0f)
		{
			float num2 = 1f / magnitude;
			x = num * q.x * num2;
			y = num * q.y * num2;
			z = num * q.z * num2;
		}
		return new Quaternion(x, y, z, w);
	}

	public static Vector4 Log(Quaternion q)
	{
		float num = Mathf.Acos(q.w);
		float num2 = Mathf.Sin(num);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = 0f;
		if (Mathf.Abs(num2) > 0f)
		{
			float num3 = 1f / num2;
			x = num * q.x * num3;
			y = num * q.y * num3;
			z = num * q.z * num3;
		}
		return new Vector4(x, y, z, w);
	}
}
