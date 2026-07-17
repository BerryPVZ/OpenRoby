using System;

[Serializable]
public class PlayerProgressObj
{
	public int[] won_levels_on_pack;

	public int[][] won_scores;

	public int[][] won_stars;

	public void ReadPrefs()
	{
		ReadWonLevels();
		ReadWonScores();
		ReadWonStars();
	}

	public void WritePrefs()
	{
		WriteWonLevels();
		WriteWonScores();
		WriteWonStars();
	}

	public string PrintPrefs()
	{
		string empty = string.Empty;
		empty += PrintWonLevels();
		empty += "===============================\n";
		empty += PrintWonScores();
		return empty + "===============================\n";
	}

	private void ReadWonLevels()
	{
		won_levels_on_pack = new int[4];
		for (int i = 0; i <= 3; i++)
		{
			won_levels_on_pack[i] = PlayerPreperencesManager.GetWonLevels(i + 1);
		}
	}

	private void ReadWonScores()
	{
		won_scores = new int[4][];
		for (int i = 0; i <= 3; i++)
		{
			won_scores[i] = new int[20];
		}
		for (int j = 0; j <= 3; j++)
		{
			for (int k = 0; k <= 19; k++)
			{
				won_scores[j][k] = PlayerPreperencesManager.GetScoresProgress(j + 1, k + 1);
			}
		}
	}

	private void ReadWonStars()
	{
		won_stars = new int[4][];
		for (int i = 0; i <= 3; i++)
		{
			won_stars[i] = new int[20];
		}
		for (int j = 0; j <= 3; j++)
		{
			for (int k = 0; k <= 19; k++)
			{
				won_stars[j][k] = PlayerPreperencesManager.GetStarsProgress(j + 1, k + 1);
			}
		}
	}

	private void WriteWonLevels()
	{
		for (int i = 0; i <= 3; i++)
		{
			PlayerPreperencesManager.SetWonLevels(i + 1, won_levels_on_pack[i]);
		}
	}

	private void WriteWonScores()
	{
		for (int i = 0; i <= 3; i++)
		{
			for (int j = 0; j <= 19; j++)
			{
				PlayerPreperencesManager.SetScoresProgress(i + 1, j + 1, won_scores[i][j]);
			}
		}
	}

	private void WriteWonStars()
	{
		for (int i = 0; i <= 3; i++)
		{
			for (int j = 0; j <= 19; j++)
			{
				PlayerPreperencesManager.SetStarsProgress(i + 1, j + 1, won_stars[i][j]);
			}
		}
	}

	private string PrintWonLevels()
	{
		string text = string.Empty;
		for (int i = 0; i <= 3; i++)
		{
			string text2 = text;
			text = text2 + "pack " + (i + 1) + " = " + won_levels_on_pack[i] + "\n";
		}
		return text;
	}

	private string PrintWonScores()
	{
		string text = string.Empty;
		for (int i = 0; i <= 3; i++)
		{
			for (int j = 0; j <= 19; j++)
			{
				string text2 = text;
				text = text2 + "pack " + (i + 1) + " level " + (j + 1) + " = " + won_scores[i][j] + "\n";
			}
		}
		return text;
	}

	private string PrintWonStars()
	{
		string text = string.Empty;
		for (int i = 0; i <= 3; i++)
		{
			for (int j = 0; j <= 19; j++)
			{
				string text2 = text;
				text = text2 + "pack " + (i + 1) + " level " + (j + 1) + " = " + won_stars[i][j] + "\n";
			}
		}
		return text;
	}
}
