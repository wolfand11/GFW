using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GFW;

namespace GFW
{
	public class GModalViewMgr:GViewMgrBase
	{
		private static GModalViewMgr instance_;

		public static GModalViewMgr GetInstance ()
		{
			if (instance_ == null) {
				instance_ = new GModalViewMgr ();
			}
			return instance_;
		}

		public static void DestoryInstance ()
		{
			instance_ = null;
		}
	}
}
