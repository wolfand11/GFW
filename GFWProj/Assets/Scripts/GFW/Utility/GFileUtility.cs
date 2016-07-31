using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace GFW
{
	public class GFileUtility
	{
		public static string GetWriteablePath ()
		{
			#if UNITY_EDITOR
			string writeablePath = Application.dataPath + "/../WriteablePath/";
			#elif UNITY_STANDALONE_WIN
			string writeablePath = Application.dataPath + "/WriteablePath/";
			#elif UNITY_STANDALONE_OSX
			string writeablePath = Application.dataPath + "/WriteablePath/";
			#else
			string writeablePath = Application.persistentDataPath+"/";
			#endif
			return writeablePath;
		}

		private static readonly char[] PathSeperator = new char[2]{ '/', '\\' };

		public static char[] GetPathSeperator ()
		{
			return PathSeperator;
		}

		public static string GetDirPath (string filePath)
		{
			if (filePath != null) {
				int fileNameStartIndex = filePath.LastIndexOfAny (PathSeperator);
				return filePath.Substring (0, fileNameStartIndex + 1);
			}
			return "";
		}

		public static bool IsFileExists (string filePath)
		{
			return File.Exists (filePath);
		}

		public static void WriteFile (string filePath, string content, bool isAppend = true)
		{
			string fullPath = GetWriteablePath () + filePath;
			string dirPath = GetDirPath (fullPath);
			if (!Directory.Exists (dirPath)) {
				if (!CreateDir (dirPath)) {
					return;
				}
			}
			using (StreamWriter file = new StreamWriter (fullPath, isAppend)) {
				file.WriteLine (content);
			}
		}

		public static string ReadFile (string filePath)
		{
			if (IsFileExists (filePath)) {
				return System.IO.File.ReadAllText (filePath);
			}
			return "";
		}

		public static bool CreateDir (string dirPath)
		{
			bool result = false;
			if (dirPath != null) {
				try {
					System.IO.Directory.CreateDirectory (dirPath);
					result = true;
				} catch (Exception e) {
					Debug.Log (e.ToString ());
				} finally {
				}
			}
			return result;
		}
	}
}