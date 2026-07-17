using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
	public static void SaveFile(string filename, object obj)
	{
		try
		{
			Debug.Log("Writing Stream to Disk.");
			GameObject.Find("gui").GetComponent<TextMesh>().text = "Writing Stream to Disk.";
			MonoBehaviour.print("DONE!");
			Stream stream = File.Open(GetPath() + "/" + filename, FileMode.Create);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, obj);
			stream.Close();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Save.SaveFile(): Failed to serialize object to a file " + filename + " (Reason: " + ex.ToString() + ")");
			MonoBehaviour.print("Failed");
			GameObject.Find("gui").GetComponent<TextMesh>().text = "Reason: " + ex.ToString();
		}
	}

	public static object LoadFile(string filename)
	{
		try
		{
			Debug.Log("Reading Stream from Disk.");
			MonoBehaviour.print("DONE!");
			Stream stream = File.Open(GetPath() + "/" + filename, FileMode.Open, FileAccess.Read);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			object result = binaryFormatter.Deserialize(stream);
			stream.Close();
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("SaveLoad.LoadFile(): Failed to deserialize a file " + filename + " (Reason: " + ex.ToString() + ")");
			MonoBehaviour.print("Failed");
			return null;
		}
	}

	public static string GetPath()
	{
		string text = Application.persistentDataPath.ToString();
		MonoBehaviour.print(text);
		string text2 = string.Empty;
		for (int i = 0; i <= text.Length - 3; i++)
		{
			if (text.Substring(i, 3).Equals("com"))
			{
				text2 = text.Substring(0, i - 1);
			}
		}
		if (!text2.Equals(string.Empty))
		{
			return text2;
		}
		return "C:/Documents and Settings/Admin/Local Settings/Application Data/MissingGames";
	}
}
