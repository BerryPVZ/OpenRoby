using System;
using System.Text;
using UnityEngine;

public class FloatMatrix
{
	private float[,] data;

	public static readonly FloatMatrix identity2x2 = new FloatMatrix(new Vector2[2]
	{
		Vector2.right,
		Vector2.up
	});

	public static readonly FloatMatrix identity3x3 = new FloatMatrix(new Vector3[3]
	{
		Vector3.right,
		Vector3.up,
		Vector3.forward
	});

	public static readonly FloatMatrix _identity4x4 = new FloatMatrix(new Vector4[4]
	{
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(0f, 1f, 0f, 0f),
		new Vector4(0f, 0f, 1f, 0f),
		new Vector4(0f, 0f, 0f, 1f)
	});

	public int rows
	{
		get
		{
			return data.GetUpperBound(0) + 1;
		}
	}

	public int columns
	{
		get
		{
			return data.GetUpperBound(1) + 1;
		}
	}

	public float this[int r, int c]
	{
		get
		{
			return data[r, c];
		}
		set
		{
			data[r, c] = value;
		}
	}

	public bool isSquare
	{
		get
		{
			return rows == columns;
		}
	}

	public float determinant
	{
		get
		{
			if (!isSquare)
			{
				IsNotSquare();
			}
			switch (rows)
			{
			case 1:
				return this[0, 0];
			case 2:
				return this[0, 0] * this[1, 1] - this[1, 0] * this[0, 1];
			case 3:
				return this[0, 0] * this[1, 1] * this[2, 2] + this[0, 1] * this[1, 2] * this[2, 0] + this[0, 2] * this[1, 0] * this[2, 1] - this[0, 0] * this[1, 2] * this[2, 1] - this[0, 1] * this[1, 0] * this[2, 2] - this[0, 2] * this[1, 1] * this[2, 0];
			default:
			{
				float num = 0f;
				for (int i = 0; i < rows; i++)
				{
					float[,] array = new float[rows - 1, rows - 1];
					for (int j = 1; j < rows; j++)
					{
						int num2 = 0;
						for (int k = 0; k < rows; k++)
						{
							if (k != i)
							{
								array[j - 1, num2] = this[j, k];
								num2++;
							}
						}
					}
					FloatMatrix floatMatrix = new FloatMatrix(array);
					num += Mathf.Pow(-1f, 1f + (float)i + 1f) * this[0, i] * floatMatrix.determinant;
				}
				return num;
			}
			}
		}
	}

	public FloatMatrix transpose
	{
		get
		{
			float[,] array = new float[columns, rows];
			for (int i = 0; i < columns; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					array[i, j] = this[j, i];
				}
			}
			return new FloatMatrix(array);
		}
	}

	public float trace
	{
		get
		{
			if (!isSquare)
			{
				IsNotSquare();
			}
			float num = 0f;
			for (int i = 0; i < rows; i++)
			{
				num += this[i, i];
			}
			return num;
		}
	}

	public FloatMatrix(int rows, int columns)
	{
		if (rows < 1 || columns < 1)
		{
			throw new ArgumentOutOfRangeException("Must specify positive, non-zero rows and columns.");
		}
		data = new float[rows, columns];
	}

	public FloatMatrix(float[,] data)
	{
		if (data == null)
		{
			throw new NullReferenceException();
		}
		if (data.GetUpperBound(0) < 1 || data.GetUpperBound(1) < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		this.data = data;
	}

	public FloatMatrix(float[] data)
	{
		if (data == null)
		{
			throw new NullReferenceException();
		}
		if (data.Length < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		this.data = new float[data.Length, 1];
		for (int i = 0; i < data.Length; i++)
		{
			this.data[i, 0] = data[i];
		}
	}

	public FloatMatrix(Vector2[] vectors)
	{
		if (vectors == null)
		{
			throw new NullReferenceException();
		}
		if (vectors.Length < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		data = new float[vectors.Length, 2];
		for (int i = 0; i < vectors.Length; i++)
		{
			data[i, 0] = vectors[i].x;
			data[i, 1] = vectors[i].y;
		}
	}

	public FloatMatrix(Vector3[] vectors)
	{
		if (vectors == null)
		{
			throw new NullReferenceException();
		}
		if (vectors.Length < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		data = new float[vectors.Length, 3];
		for (int i = 0; i < vectors.Length; i++)
		{
			data[i, 0] = vectors[i].x;
			data[i, 1] = vectors[i].y;
			data[i, 2] = vectors[i].z;
		}
	}

	public FloatMatrix(Vector4[] vectors)
	{
		if (vectors == null)
		{
			throw new NullReferenceException();
		}
		if (vectors.Length < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		data = new float[vectors.Length, 4];
		for (int i = 0; i < vectors.Length; i++)
		{
			data[i, 0] = vectors[i].x;
			data[i, 1] = vectors[i].y;
			data[i, 2] = vectors[i].z;
			data[i, 3] = vectors[i].w;
		}
	}

	public FloatMatrix(Quaternion[] quaternions)
	{
		if (quaternions == null)
		{
			throw new NullReferenceException();
		}
		if (quaternions.Length < 1)
		{
			throw new ArgumentOutOfRangeException();
		}
		data = new float[quaternions.Length, 4];
		for (int i = 0; i < quaternions.Length; i++)
		{
			data[i, 0] = quaternions[i].x;
			data[i, 1] = quaternions[i].y;
			data[i, 2] = quaternions[i].z;
			data[i, 3] = quaternions[i].w;
		}
	}

	public FloatMatrix(FloatMatrix m)
	{
		if (m == null)
		{
			throw new NullReferenceException();
		}
		data = new float[m.rows, m.columns];
		for (int i = 0; i < m.rows; i++)
		{
			for (int j = 0; j < m.columns; j++)
			{
				data[i, j] = m[i, j];
			}
		}
	}

	public FloatMatrix(Matrix4x4 m)
	{
		data = new float[4, 4];
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				data[i, j] = m[i, j];
			}
		}
	}

	private void IsNotSquare()
	{
		throw new MemberAccessException(string.Format("Cannot compute determinant of a non-square matrix. Found {0} x {1}", rows, columns));
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < rows; i++)
		{
			stringBuilder.Append("[");
			for (int j = 0; j < columns; j++)
			{
				stringBuilder.Append(string.Format("{0:0.00000}", data[i, j]));
				if (j < columns - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			stringBuilder.Append("]");
			if (i < rows - 1)
			{
				stringBuilder.Append("\n");
			}
		}
		return stringBuilder.ToString();
	}

	public static FloatMatrix operator *(FloatMatrix m1, FloatMatrix m2)
	{
		if (m1.columns != m2.rows)
		{
			throw new ArithmeticException("m1 columns must be the same as m2 rows");
		}
		float[,] array = new float[m1.rows, m2.columns];
		for (int i = 0; i < m1.rows; i++)
		{
			for (int j = 0; j < m2.columns; j++)
			{
				for (int k = 0; k < m1.columns; k++)
				{
					array[i, j] += m1[i, k] * m2[k, j];
				}
			}
		}
		return new FloatMatrix(array);
	}

	public static FloatMatrix operator *(FloatMatrix m, float s)
	{
		for (int i = 0; i < m.rows; i++)
		{
			for (int j = 0; j < m.columns; j++)
			{
				FloatMatrix floatMatrix2;
				FloatMatrix floatMatrix = (floatMatrix2 = m);
				int r2;
				int r = (r2 = i);
				int c2;
				int c = (c2 = j);
				float num = floatMatrix2[r2, c2];
				floatMatrix[r, c] = num * s;
			}
		}
		return m;
	}

	public static FloatMatrix operator /(FloatMatrix m, float s)
	{
		return m * (1f / s);
	}
}
