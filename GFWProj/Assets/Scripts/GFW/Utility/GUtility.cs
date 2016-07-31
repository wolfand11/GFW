using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GFW
{
	public static class GUtility
	{
		// This is a temp list, before use it ,you should empty it.
		private static List<GameObject> tempList = new List<GameObject> ();

		public static GameObject FindParentInTree (GameObject node, string parentName)
		{
			Transform trans = (node != null) ? node.transform : null;
			while (trans != null) {
				if (trans.name == parentName) {
					return trans.gameObject;
				}
				trans = trans.parent;
			}
			return null;
		}

		public static GameObject FindChildInTree (GameObject root, string childName)
		{
			foreach (Transform child in root.transform) {
				if (child.name == childName) {
					return child.gameObject;
				} else {
					GameObject temp = FindChildInTree (child.gameObject, childName);
					if (temp != null) {
						return temp;
					}
				}
			}
			return null;
		}

		public static void RemoveAllChildren (GameObject gObj)
		{
			if (gObj != null) {
				tempList.Clear ();
				foreach (Transform child in gObj.transform) {
					tempList.Add (child.gameObject);
				}
				foreach (GameObject tempObj in tempList) {
					Transform.DestroyImmediate (tempObj);
				}
				tempList.Clear ();
			}
		}

		public static T GetComponentInSelfAndParent<T> (GameObject gObj)
		{
			Debug.Assert (gObj != null);
			return gObj.GetComponentInParent<T> ();
		}

		public static T GetComponentInParentExceptSelf<T> (GameObject gObj) where T : class
		{
			Debug.Assert (gObj != null);
			if (gObj.transform.parent) {
				return gObj.transform.parent.GetComponentInParent<T> ();	
			}
			return null;
		}
	}
}

