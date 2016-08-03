using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GFW
{
	public delegate void GEventHandler (int eventType, params Object[] args);
	public class GEventTrigger
	{
		public int eventType;
		public GEventHandler handler;

		public GEventTrigger (int eventType, GEventHandler handler)
		{
			this.eventType = eventType;
			this.handler = handler;
		}

		public void detach ()
		{
			GEventMgr.GetInstance ().UnRegister (this);
		}
	}

	public class GEventMgr : GSingleton<GEventMgr>
	{
		Dictionary<int,List<GEventTrigger>> eventMap_;

		public GEventTrigger Register (int eventType, GEventHandler handler)
		{
			var trigger = new GEventTrigger (eventType, handler);
			if (eventMap_ == null) {
				eventMap_ = new Dictionary<int, List<GEventTrigger>> ();
			}

			List<GEventTrigger> triggerList = null;
			if (eventMap_.ContainsKey (eventType)) {
				triggerList = eventMap_ [eventType];
			} else {
				triggerList = new List<GEventTrigger> ();
				eventMap_ [eventType] = triggerList;
			}
			triggerList.Add (trigger);
			return trigger;
		}

		public void UnRegister (GEventTrigger trigger)
		{
			if (eventMap_ != null) {
				if (eventMap_.ContainsKey (trigger.eventType)) {
					var triggerList = eventMap_ [trigger.eventType];
					for (int i = triggerList.Count - 1; i > -1; i--) {
						if (triggerList [i] == trigger) {
							triggerList.RemoveAt (i);
						}
					}
				}
			}
		}

		public void TriggerEvent (int eventType, params Object[] args)
		{
			if (eventMap_ != null) {
				if (eventMap_.ContainsKey (eventType)) {
					foreach (var tmp in eventMap_ [eventType]) {
						if (tmp.handler != null) {
							tmp.handler (eventType, args);
						}
					}
				}
			}
		}
	}
}
