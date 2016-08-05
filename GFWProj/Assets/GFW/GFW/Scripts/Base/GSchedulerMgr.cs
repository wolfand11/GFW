using UnityEngine;
using System.Collections;

namespace GFW
{
	public delegate void GSchedulerFunc (float dt);
	public class GScheduler
	{
		public GSchedulerFunc func_;
		public float interval_;
	}

	public class GSchedulerMgr:GSingleton<GSchedulerMgr>
	{
		public void Schedue ()
		{
		}
	}
}
