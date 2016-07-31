using UnityEngine;
using System.Collections;
using GFW;

namespace GFW
{
	public class GTimeUtility:GSingleton<GTimeUtility>
	{
		public static string GetCurDateStr ()
		{
			return System.DateTime.Now.ToString ("yyyy-MM-dd_HH-mm-ss");
		}
	}
}