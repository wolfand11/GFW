using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GFW;

namespace GFW
{
	public class GUIViewMgr:GViewMgrBase
	{
		private static GUIViewMgr instance_;

		public static GUIViewMgr GetInstance ()
		{
			if (instance_ == null) {
				instance_ = new GUIViewMgr ();
			}
			return instance_;
		}

		public static void DestoryInstance ()
		{
			instance_ = null;
		}
	}
}
