using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GFW
{
	public class GAppUtility
	{
		#if UNITY_WEBPLAYER
		public static string webplayerQuitURL = "http://google.com";
		#endif
		public static void Quit ()
		{
			// TODO there 's some bug, only quit at first time.
			#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
			#elif UNITY_WEBPLAYER
			Application.OpenURL(webplayerQuitURL);
			#else
			Application.Quit();
			#endif
		}
	}
}


