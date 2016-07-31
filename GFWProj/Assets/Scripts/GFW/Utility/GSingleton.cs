using UnityEngine;
using System.Collections;

namespace GFW
{
	// Object Singleton
	public abstract class GSingleton<T> where T : class, new()
	{
		protected static T instance_;

		protected GSingleton ()
		{

		}

		public static T GetInstance ()
		{
			if (instance_ == null) {
				instance_ = new T ();
			}
			return instance_;
		}

		public static void DestoryInstance ()
		{
			instance_ = null;
		}
	}

	// Game Object Singleton
	public abstract class GGOSingleton<T>:MonoBehaviour where T : MonoBehaviour, new()
	{
		protected static T instance_;

		protected GGOSingleton ()
		{
		}

		public static T GetInstance ()
		{
			if (instance_ == null) {
				instance_ = new T ();
			}
			return instance_;
		}

		public static void DestoryInstance ()
		{
			instance_ = null;
		}
	}
}
